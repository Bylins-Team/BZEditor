using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ScintillaNET;

namespace BZEditor
{
    /// <summary>
    /// DG-script code editor built on ScintillaNET. Subclasses Scintilla and exposes the
    /// same method surface the form used with the old Fireball control (SetText/GetText,
    /// Selection, bookmarks, Go to line, Find/Replace, autocomplete) so the rest of the UI
    /// keeps working unchanged. Replaces the legacy Fireball.CodeEditorControl.
    /// </summary>
    public class DgScriptEditor : Scintilla
    {
        private const int StyleKeyword = 1;
        private const int StyleNumber = 2;
        private const int StyleString = 3;
        private const int StyleComment = 4;
        private const int StyleVar = 5;
        private const int BookmarkMarker = 3;

        private static readonly HashSet<string> Keywords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "if", "then", "else", "elseif", "end", "while", "foreach", "switch", "case", "break", "done",
            "set", "unset", "eval", "global", "return", "halt", "extract", "wait", "nop"
        };

        private static readonly Regex Token = new Regex(
            @"(?<comment>\bnop\b[^\r\n]*)" +
            @"|(?<str>""(?:\\.|[^""\\])*"")" +
            @"|(?<var>%[^%\r\n]*%)" +
            @"|(?<num>\b0x[0-9a-fA-F]+\b|\b\d+\b)" +
            @"|(?<word>[A-Za-z_]\w*)",
            RegexOptions.Compiled);

        private string[] _autoItems = new string[0];
        private string _lastSearch = "";

        public DgScriptEditor()
        {
            ConfigureStyles();

            Lexer = Lexer.Container;
            StyleNeeded += OnStyleNeeded;
            CharAdded += OnCharAdded;

            // line-number margin + a clickable bookmark margin
            Margins[0].Type = MarginType.Number;
            Margins[0].Width = 32;
            Margins[1].Type = MarginType.Symbol;
            Margins[1].Width = 16;
            Margins[1].Mask = 1 << BookmarkMarker;
            Margins[1].Sensitive = true;
            Markers[BookmarkMarker].Symbol = MarkerSymbol.Circle;
            Markers[BookmarkMarker].SetBackColor(Color.RoyalBlue);
            Markers[BookmarkMarker].SetForeColor(Color.White);
            MarginClick += (s, e) =>
            {
                if (e.Margin == 1)
                {
                    int line = LineFromPosition(e.Position);
                    ToggleBookmarkOnLine(line);
                }
            };
        }

        private void ConfigureStyles()
        {
            StyleResetDefault();
            Styles[Style.Default].Font = "Consolas";
            Styles[Style.Default].Size = 10;
            StyleClearAll();
            Styles[StyleKeyword].ForeColor = Color.Blue;
            Styles[StyleKeyword].Bold = true;
            Styles[StyleNumber].ForeColor = Color.FromArgb(0x80, 0x00, 0x00);
            Styles[StyleString].ForeColor = Color.FromArgb(0xA0, 0x30, 0x00);
            Styles[StyleComment].ForeColor = Color.Green;
            Styles[StyleComment].Italic = true;
            Styles[StyleVar].ForeColor = Color.Purple;
        }

        private void OnStyleNeeded(object sender, StyleNeededEventArgs e)
        {
            int startLine = LineFromPosition(GetEndStyled());
            int from = Lines[startLine].Position;
            int endLine = LineFromPosition(e.Position);
            int to = Lines[endLine].EndPosition;
            string text = GetTextRange(from, to - from);

            StartStyling(from);
            int last = 0;
            foreach (Match m in Token.Matches(text))
            {
                if (m.Index > last)
                    SetStyling(m.Index - last, Style.Default);

                int style = Style.Default;
                if (m.Groups["comment"].Success) style = StyleComment;
                else if (m.Groups["str"].Success) style = StyleString;
                else if (m.Groups["var"].Success) style = StyleVar;
                else if (m.Groups["num"].Success) style = StyleNumber;
                else if (m.Groups["word"].Success && Keywords.Contains(m.Value)) style = StyleKeyword;

                SetStyling(m.Length, style);
                last = m.Index + m.Length;
            }
            if (text.Length > last)
                SetStyling(text.Length - last, Style.Default);
        }

        private void OnCharAdded(object sender, CharAddedEventArgs e)
        {
            if (_autoItems.Length == 0) return;
            if (!char.IsLetterOrDigit((char)e.Char) && e.Char != '_' && e.Char != '.') return;

            int pos = CurrentPosition;
            int wordStart = WordStartPosition(pos, true);
            int len = pos - wordStart;
            if (len <= 0) return;

            string prefix = GetTextRange(wordStart, len);
            var matches = _autoItems
                .Where(i => i.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                .OrderBy(i => i, StringComparer.OrdinalIgnoreCase)
                .ToArray();
            if (matches.Length > 0)
                AutoCShow(len, string.Join(" ", matches));
        }

        // ---- API kept compatible with the previous (Fireball) editor ----

        public void SetText(string text)
        {
            Text = text ?? "";
            EmptyUndoBuffer();
        }

        public string GetText()
        {
            return Text;
        }

        public void SetListItemsArray(System.Collections.IEnumerable items)
        {
            var list = new List<string>();
            if (items != null)
                foreach (var it in items)
                    if (it != null) list.Add(it.ToString());
            _autoItems = list.ToArray();
        }

        public EditorSelection Selection
        {
            get { return new EditorSelection(this); }
        }

        public void ToggleBookmark()
        {
            ToggleBookmarkOnLine(CurrentLine);
        }

        private void ToggleBookmarkOnLine(int line)
        {
            const uint mask = 1u << BookmarkMarker;
            if ((Lines[line].MarkerGet() & mask) != 0)
                Lines[line].MarkerDelete(BookmarkMarker);
            else
                Lines[line].MarkerAdd(BookmarkMarker);
        }

        public void GotoNextBookmark()
        {
            const uint mask = 1u << BookmarkMarker;
            int total = Lines.Count;
            for (int off = 1; off <= total; off++)
            {
                int i = (CurrentLine + off) % total;
                if ((Lines[i].MarkerGet() & mask) != 0) { GotoLine(i); return; }
            }
        }

        public void GotoPreviousBookmark()
        {
            const uint mask = 1u << BookmarkMarker;
            int total = Lines.Count;
            for (int off = 1; off <= total; off++)
            {
                int i = ((CurrentLine - off) % total + total) % total;
                if ((Lines[i].MarkerGet() & mask) != 0) { GotoLine(i); return; }
            }
        }

        private void GotoLine(int line)
        {
            line = Math.Max(0, Math.Min(line, Lines.Count - 1));
            Lines[line].Goto();
            ScrollCaret();
        }

        public void ShowGotoLine()
        {
            using (var dlg = new SimpleInputForm("Go to line", "Line number:", (CurrentLine + 1).ToString()))
            {
                if (dlg.ShowDialog(FindForm()) == DialogResult.OK)
                {
                    int n;
                    if (int.TryParse(dlg.Value, out n) && n >= 1)
                        GotoLine(n - 1);
                }
            }
        }

        public void ShowFind()
        {
            using (var dlg = new FindReplaceForm(this, false))
                dlg.ShowDialog(FindForm());
        }

        public void ShowReplace()
        {
            using (var dlg = new FindReplaceForm(this, true))
                dlg.ShowDialog(FindForm());
        }

        public void FindNext()
        {
            if (string.IsNullOrEmpty(_lastSearch)) { ShowFind(); return; }
            FindAndSelect(_lastSearch, false);
        }

        /// <summary>Searches forward from the caret, wraps around. Returns true if found.</summary>
        public bool FindAndSelect(string needle, bool matchCase)
        {
            if (string.IsNullOrEmpty(needle)) return false;
            _lastSearch = needle;
            SearchFlags = matchCase ? SearchFlags.MatchCase : SearchFlags.None;

            TargetStart = SelectionEnd;
            TargetEnd = TextLength;
            int pos = SearchInTarget(needle);
            if (pos < 0)
            {
                TargetStart = 0;
                TargetEnd = TextLength;
                pos = SearchInTarget(needle);
            }
            if (pos < 0) return false;
            SetSelection(TargetStart, TargetEnd);
            ScrollCaret();
            return true;
        }
    }

    /// <summary>Thin wrapper exposing the few selection operations the form used.</summary>
    public class EditorSelection
    {
        private readonly Scintilla _e;
        public EditorSelection(Scintilla e) { _e = e; }
        public void Indent() { ChangeIndent(+1); }
        public void Outdent() { ChangeIndent(-1); }

        private void ChangeIndent(int direction)
        {
            int firstLine = _e.LineFromPosition(_e.SelectionStart);
            int lastLine = _e.LineFromPosition(_e.SelectionEnd);
            int step = _e.TabWidth <= 0 ? 4 : _e.TabWidth;
            for (int i = firstLine; i <= lastLine; i++)
                _e.Lines[i].Indentation = Math.Max(0, _e.Lines[i].Indentation + direction * step);
        }
        public string Text
        {
            get { return _e.SelectedText; }
            set { _e.ReplaceSelection(value ?? ""); }
        }
    }

    internal class SimpleInputForm : Form
    {
        private readonly TextBox _tb = new TextBox();
        public string Value { get { return _tb.Text; } }

        public SimpleInputForm(string title, string prompt, string initial)
        {
            Text = title;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MinimizeBox = false; MaximizeBox = false;
            ClientSize = new Size(280, 96);

            var lbl = new Label { Text = prompt, Left = 8, Top = 12, AutoSize = true };
            _tb.SetBounds(8, 32, 264, 22);
            _tb.Text = initial;
            var ok = new Button { Text = "OK", DialogResult = DialogResult.OK, Left = 116, Top = 62, Width = 75 };
            var cancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Left = 197, Top = 62, Width = 75 };
            Controls.Add(lbl); Controls.Add(_tb); Controls.Add(ok); Controls.Add(cancel);
            AcceptButton = ok; CancelButton = cancel;
        }
    }

    internal class FindReplaceForm : Form
    {
        private readonly DgScriptEditor _editor;
        private readonly TextBox _find = new TextBox();
        private readonly TextBox _replace = new TextBox();
        private readonly CheckBox _case = new CheckBox { Text = "Match case", AutoSize = true };

        public FindReplaceForm(DgScriptEditor editor, bool withReplace)
        {
            _editor = editor;
            Text = withReplace ? "Replace" : "Find";
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(360, withReplace ? 132 : 100);

            Controls.Add(new Label { Text = "Find:", Left = 8, Top = 12, AutoSize = true });
            _find.SetBounds(70, 9, 200, 22);
            _find.Text = _editor.SelectedText;
            Controls.Add(_find);

            var findNext = new Button { Text = "Find next", Left = 276, Top = 8, Width = 76 };
            findNext.Click += (s, e) => DoFind();
            Controls.Add(findNext);

            int y = 38;
            if (withReplace)
            {
                Controls.Add(new Label { Text = "Replace:", Left = 8, Top = y + 3, AutoSize = true });
                _replace.SetBounds(70, y, 200, 22);
                Controls.Add(_replace);
                var rep = new Button { Text = "Replace", Left = 276, Top = y - 1, Width = 76 };
                rep.Click += (s, e) => { DoReplaceCurrent(); DoFind(); };
                Controls.Add(rep);
                var repAll = new Button { Text = "Replace all", Left = 276, Top = y + 27, Width = 76 };
                repAll.Click += (s, e) => DoReplaceAll();
                Controls.Add(repAll);
                y += 30;
            }

            _case.SetBounds(70, y + 6, 120, 20);
            Controls.Add(_case);
            AcceptButton = findNext;
        }

        private void DoFind()
        {
            if (!_editor.FindAndSelect(_find.Text, _case.Checked))
                MessageBox.Show(this, "Not found.", Text);
        }

        private void DoReplaceCurrent()
        {
            if (_editor.SelectedText.Length > 0)
                _editor.ReplaceSelection(_replace.Text);
        }

        private void DoReplaceAll()
        {
            string needle = _find.Text;
            if (string.IsNullOrEmpty(needle)) return;
            _editor.SelectionStart = 0;
            _editor.SelectionEnd = 0;
            int count = 0;
            while (_editor.FindAndSelect(needle, _case.Checked))
            {
                _editor.ReplaceSelection(_replace.Text);
                count++;
                if (count > 100000) break;
            }
            MessageBox.Show(this, count + " replaced.", Text);
        }
    }
}
