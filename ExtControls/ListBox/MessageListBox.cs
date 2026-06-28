using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ExtControls
{
    /// <summary>
    /// 
    /// </summary>
    public class MessageListBox : ResizableListBox
    {
        private const int MMainTextOffset = 30;
        private IContainer components;
        private ImageList iconList;
        private Font mHeadingFont;

        /// <summary>
        /// Constructor.
        /// </summary>
        public MessageListBox()
        {
            InitializeComponent();
            mHeadingFont = new Font(Font, FontStyle.Bold);
            MeasureItem += new MeasureItemEventHandler(MeasureItemHandler);
        }

        /// <summary>
        /// Windows-Init.
        /// </summary>
        private void InitializeComponent()
        {
            components = new Container();
            ComponentResourceManager resources = new ComponentResourceManager(typeof (MessageListBox));
            iconList = new ImageList(components);
            SuspendLayout();
            // 
            // IconList
            // 
            iconList.ImageStream = (ImageListStreamer) resources.GetObject("iconList.ImageStream");
            iconList.TransparentColor = Color.Transparent;
            iconList.Images.SetKeyName(0, "info.png");
            iconList.Images.SetKeyName(1, "alert16.png");
            iconList.Images.SetKeyName(2, "file_close1.png");
            iconList.Images.SetKeyName(3, "");
            ResumeLayout(false);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
            }

            mHeadingFont.Dispose();

            base.Dispose(disposing);
        }

        #region overrides

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawFocusRectangle();
            ParseMessageEventArgs item;
            Rectangle bounds = e.Bounds;
            Color textColor = SystemColors.ControlText;

            item = Items[e.Index];

            //draw selected item background
            if (e.State == DrawItemState.Selected)
            {
                using (Brush b = new SolidBrush(SystemColors.Highlight))
                {
                    // Fill background;
                    e.Graphics.FillRectangle(b, e.Bounds);
                }
                textColor = SystemColors.HighlightText;
            }

            //draw image
            if (item.MessageType != ParseMessageType.Отсутствует)
            {
                iconList.Draw(
                    e.Graphics,
                    bounds.Left + 1,
                    bounds.Top + 2,
                    (int) item.MessageType);
            }

            using (SolidBrush textBrush = new SolidBrush(textColor))
            {
                //draw Headline
                e.Graphics.DrawString(
                    item.LineHeader,
                    mHeadingFont,
                    textBrush,
                    bounds.Left + iconList.ImageSize.Width + 5,
                    bounds.Top + iconList.ImageSize.Height - mHeadingFont.Height);

                //draw main text
                int top;

                // Draw layout, 2 times the offset (left & right)
                Size oneLine = new Size(Width - MMainTextOffset*2, Font.Height);

                StringBuilder sbTextToDraw = new StringBuilder(item.MessageText);
                string strLineToDraw;
                int index1 = 0;
                top = bounds.Top + iconList.ImageSize.Height + 2;

                while (sbTextToDraw.Length > 0)
                {
                    // Break string into more lines when an end-of-line character is found
                    int index2;
                    int index2New;
                    if ((index2 = sbTextToDraw.ToString().IndexOf('\n')) > 0)
                    {
                        strLineToDraw = sbTextToDraw.ToString(index1, index2 - index1);
                        index2New = index2 + 1;
                    }
                    else
                    {
                        index2 = sbTextToDraw.Length;
                        index2New = index2;
                        strLineToDraw = sbTextToDraw.ToString();
                    }

                    e.Graphics.MeasureString(
                        strLineToDraw,
                        Font,
                        oneLine,
                        StringFormat.GenericDefault,
                        out var charsFitted,
                        out _);

                    // There's no knowledge about words, so just don't split words up if possible
                    if (charsFitted < index2)
                    {
                        int index = strLineToDraw.LastIndexOf(' ', charsFitted - 1, charsFitted);
                        if (index != -1)
                            index2New = index + 1;
                        else
                            index2New = charsFitted;
                        strLineToDraw = sbTextToDraw.ToString(index1, index2New - index1);
                    }

                    // Draw the text
                    e.Graphics.DrawString(
                        strLineToDraw,
                        Font,
                        textBrush,
                        bounds.Left + MMainTextOffset,
                        top);

                    // Adjust top
                    top += Font.Height;

                    // Next line
                    sbTextToDraw = sbTextToDraw.Remove(index1, index2New);
                }
            }
        }

        private void MeasureItemHandler(object sender, MeasureItemEventArgs e)
        {
            int mainTextHeight;
            ParseMessageEventArgs item;
            item = Items[e.Index];

            // Draw layout, 2 times the offset (left & right)
            Size sz = new Size(Width - MMainTextOffset*2, Font.Height);

            StringBuilder sbTextToDraw = new StringBuilder(item.MessageText);
            string strLineToDraw;
            int index1 = 0;
            int lines = 0;

            while (sbTextToDraw.Length > 0)
            {
                // Break string into more lines when an end-of-line character is found
                int index2;
                int index2New;
                if ((index2 = sbTextToDraw.ToString().IndexOf('\n')) > 0)
                {
                    strLineToDraw = sbTextToDraw.ToString(index1, index2 - index1);
                    index2New = index2 + 1;
                }
                else
                {
                    index2 = sbTextToDraw.Length;
                    index2New = index2;
                    strLineToDraw = sbTextToDraw.ToString();
                }

                e.Graphics.MeasureString(
                    strLineToDraw,
                    Font,
                    sz,
                    StringFormat.GenericDefault,
                    out var charsFitted,
                    out var linesFilled);

                // There's no knowledge about words, so just don't split words up if possible
                if (charsFitted < index2)
                {
                    int index = strLineToDraw.LastIndexOf(' ', charsFitted - 1, charsFitted);
                    if (index != -1)
                        index2New = index + 1;
                    else
                        index2New = charsFitted;
                }

                lines += linesFilled;
                sbTextToDraw = sbTextToDraw.Remove(index1, index2New);
            }

            mainTextHeight = lines*Font.Height;

            e.ItemHeight = iconList.ImageSize.Height + mainTextHeight + 4;
            e.ItemWidth = sz.Width;
        }

        #endregion
    }
}