using System.Windows.Forms;
using ExtControls;
using Word;

namespace BZEditor
{
    public class SpellingChecker
    {
        public static string CheckGrammar(string text)
        {
            Word.Application converter = new ApplicationClass();
            bool res = converter.CheckGrammar(text);
            return !res ? "Текст содержит грамматические ошибки!" : string.Empty;
        }

        public static string SpellingCheck(TextBox postBox)
        {
            string errors = string.Empty;
            object nul = null;

            Word.Application chk = new ApplicationClass();

            int cur = 0;
            string word = string.Empty;
            int cntr = 1;
            while (cur < postBox.Text.Length)
            {
                if (char.IsLetter(postBox.Text[cur]) || postBox.Text[cur].Equals('\''))
                    word += postBox.Text[cur];
                else
                {
                    if (word.Length > 0)
                    {
                        if (!chk.CheckSpelling(word, ref nul, ref nul, ref nul, ref nul, ref nul,
                                               ref nul, ref nul, ref nul, ref nul, ref nul, ref nul, ref nul))
                            errors += "\r\n" + cntr++ + "." + word;
                        word = string.Empty;
                    }
                }
                if (cur == postBox.Text.Length - 1)
                {
                    if (!chk.CheckSpelling(word, ref nul, ref nul, ref nul, ref nul, ref nul,
                                           ref nul, ref nul, ref nul, ref nul, ref nul, ref nul, ref nul))
                        errors += "\r\n" + cntr++ + "." + word;
                }

                cur++;
            }
            postBox.Select(0, 0);
            chk.Quit(ref nul, ref nul, ref nul);
            if (errors.Length > 0)
                errors = "Текст содержит орфографические ошибки:" + errors;
            return errors;
        }

        public static string SpellingCheck(CExtRichTextBox postBox)
        {
            string errors = string.Empty;
            object nul = null;

            Word.Application chk = new ApplicationClass();

            int cur = 0;
            string word = string.Empty;
            int cntr = 1;
            while (cur < postBox.Text.Length)
            {
                if (char.IsLetter(postBox.Text[cur]) || postBox.Text[cur].Equals('\''))
                    word += postBox.Text[cur];
                else
                {
                    if (word.Length > 0)
                    {
                        if (!chk.CheckSpelling(word, ref nul, ref nul, ref nul, ref nul, ref nul,
                                               ref nul, ref nul, ref nul, ref nul, ref nul, ref nul, ref nul))
                            errors += "\r\n" + cntr++ + "." + word;
                        word = string.Empty;
                    }
                }
                if (cur == postBox.Text.Length - 1)
                {
                    if (!chk.CheckSpelling(word, ref nul, ref nul, ref nul, ref nul, ref nul,
                                           ref nul, ref nul, ref nul, ref nul, ref nul, ref nul, ref nul))
                        errors += "\r\n" + cntr++ + "." + word;
                }

                cur++;
            }
            postBox.Select(0, 0);
            chk.Quit(ref nul, ref nul, ref nul);
            if (errors.Length > 0)
                errors = "Текст содержит орфографические ошибки:" + errors;
            return errors;
        }
    }
}