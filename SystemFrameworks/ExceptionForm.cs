using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace SystemFrameworks
{
    public class ExceptionForm : Form
    {
        #region Declaration

        private Button btnOK;
        private Button btnSendBagReport;
        private Button buttonClose;
        private Button buttonMoreInfo;
        private IContainer components;
        private ContextMenu contextMenu1;
        private ContextMenu contextMenu2;
        private int FormHeight;
        private GroupBox groupBox;
        private ImageList imageList;
        private Label labelPicture;
        private MenuItem menuItem1;
        private MenuItem menuItem2;
        private MenuItem menuItem3;
        private MenuItem menuItem4;
        private Panel panelMain;
        private RichTextBox rTBMoreInfo;
        private RichTextBox rTBWhatHappened;

        #endregion

        public ExceptionForm()
        {
            InitializeComponent();

            AddMenuShortcuts();

            ChangeFormSize(false);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }

        public static void ExceptionCatcher(object source, ThreadExceptionEventArgs e)
        {
            ExceptionForm ef = new ExceptionForm();

            BZedExceptionCatcher tec = new BZedExceptionCatcher(e.Exception);

            // краткая информация
            ef.rTBWhatHappened.Text = tec.GetUserExceptionInfo();
            // подробная информация
            ef.rTBMoreInfo.Text = tec.GetFullExceptionInfo();
            // заголовок формы (тип сообщения)
            ef.Text = GetFormTitle(tec.GetExceptionIcon());
            // иконка сообщения (ти сообщения)
            ef.labelPicture.ImageIndex = GetExceptionIcon(tec.GetExceptionIcon());
            // проверка типа сообщения
            ef.CheckInfoType(GetExceptionIcon(tec.GetExceptionIcon()));
            // запись в лог
            tec.WriteToErrorLog(GetExceptionIcon(tec.GetExceptionIcon()));
            // отображение диалога
            ef.ShowDialog();
        }

        public static void ExceptionCatcher(Exception ex)
        {
            ExceptionForm ef = new ExceptionForm();

            BZedExceptionCatcher tec = new BZedExceptionCatcher(ex);

            // краткая информация
            ef.rTBWhatHappened.Text = tec.GetUserExceptionInfo();
            // подробная информация
            ef.rTBMoreInfo.Text = tec.GetFullExceptionInfo();
            // заголовок формы (тип сообщения)
            ef.Text = GetFormTitle(tec.GetExceptionIcon());
            // иконка сообщения (ти сообщения)
            ef.labelPicture.ImageIndex = GetExceptionIcon(tec.GetExceptionIcon());
            // проверка типа сообщения
            ef.CheckInfoType(GetExceptionIcon(tec.GetExceptionIcon()));
            // запись в лог
            tec.WriteToErrorLog(GetExceptionIcon(tec.GetExceptionIcon()));
            // отображение диалога
            ef.ShowDialog();
        }

        public static void ExceptionCatcher(string message, Exception ex, EventLogEntryType type)
        {
            ExceptionForm ef = new ExceptionForm();

            // краткая информация
            ef.rTBWhatHappened.Text = message;
            // подробная информация
            ef.rTBMoreInfo.Text = "";
            // заголовок формы (тип сообщения)
            switch (type)
            {
                case EventLogEntryType.Error:
                    ef.rTBMoreInfo.Text = ex.StackTrace;
                    ef.labelPicture.ImageIndex = 2;
                    ef.Text = "Ошибка";
                    break;
                case EventLogEntryType.Information:
                    ef.labelPicture.ImageIndex = 1;
                    ef.Text = "Информация";
                    ef.buttonMoreInfo.Hide();
                    ef.btnSendBagReport.Hide();
                    break;
                case EventLogEntryType.Warning:
                    ef.labelPicture.ImageIndex = 0;
                    ef.Text = "Предупреждение";
                    ef.buttonMoreInfo.Hide();
                    ef.btnSendBagReport.Hide();
                    break;
            }
            // отображение диалога
            ef.ShowDialog();
        }

        private static string _src;
        private static string _stck;

        public static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ExceptionForm ef = new ExceptionForm();

            BZedExceptionCatcher tec = new BZedExceptionCatcher((Exception) (e.ExceptionObject));

            _src = tec.GetExceptionSource();
            _stck = tec.GetExceptionStack();

            // краткая информация
            ef.rTBWhatHappened.Text = tec.GetUserExceptionInfo();
            // подробная информация
            ef.rTBMoreInfo.Text = tec.GetFullExceptionInfo();
            // заголовок формы (тип сообщения)
            ef.Text = GetFormTitle(tec.GetExceptionIcon());
            // иконка сообщения (ти сообщения)
            ef.labelPicture.ImageIndex = GetExceptionIcon(tec.GetExceptionIcon());
            // проверка типа сообщения
            ef.CheckInfoType(GetExceptionIcon(tec.GetExceptionIcon()));
            // запись в лог
            tec.WriteToErrorLog(GetExceptionIcon(tec.GetExceptionIcon()));
            // отображение диалога
            ef.ShowDialog();
        }

        /// <summary>
        /// получение иконки сообщения
        /// </summary>
        private static int GetExceptionIcon(MessageBoxIcon inIcon)
        {
            int iconName = 0;

            switch (inIcon.ToString())
            {
                case "Warning":
                case "Exclamation":
                    iconName = 0;
                    break;

                case "Asterisk":
                case "Information":
                    iconName = 1;
                    break;

                case "Error":
                case "Hand":
                case "Stop":
                    iconName = 2;
                    break;

                case "Question":
                    iconName = 3;
                    break;
            }
            return iconName;
        }

        /// <summary>
        /// получение заголовка формы
        /// </summary>
        private static string GetFormTitle(MessageBoxIcon inIcon)
        {
            string formTitle = "";

            switch (inIcon.ToString())
            {
                case "Warning":
                case "Exclamation":
                    formTitle = "Warning";
                    break;

                case "Asterisk":
                case "Information":
                    formTitle = "Information";
                    break;

                case "Error":
                case "Hand":
                case "Stop":
                    formTitle = "Message";
                    break;

                case "Question":
                    formTitle = "Question";
                    break;
            }
            return formTitle;
        }

        private void MoreInfoHandler()
        {
            groupBox.Visible = !groupBox.Visible;
            ChangeFormSize(groupBox.Visible);
        }

        /// <summary>
        /// изменение размера формы
        /// </summary>
        private void ChangeFormSize(bool moreInfoIsVisible)
        {
            if (moreInfoIsVisible)
            {
                MinimumSize = new Size(492, 311);
                MaximumSize = new Size(800, 600);
                Height = FormHeight;
                buttonMoreInfo.Text = "Скрыть <<<";
            }

            else
            {
                MinimumSize = new Size(492, 166);
                FormHeight = Height;
                MaximumSize = new Size(800, 166);
                Height = 166;
                buttonMoreInfo.Text = "Подробнее >>>";
            }
        }

        /// <summary>
        /// проверка типа сообщения и изменение вида формы в зависимости от этого
        /// </summary>
        private void CheckInfoType(int inIconType)
        {
            if (inIconType == 1)
                buttonMoreInfo.Hide();
        }

        private void buttonMoreInfo_Click(object sender, EventArgs e)
        {
            MoreInfoHandler();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            if (rTBWhatHappened.SelectedText != "")
                Clipboard.SetDataObject(rTBWhatHappened.SelectedText);
            else
                MessageBox.Show("There is no text selected ", "Attention", MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(rTBWhatHappened.Text);
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            if (rTBMoreInfo.SelectedText != "")
                Clipboard.SetDataObject(rTBMoreInfo.SelectedText);
            else
                MessageBox.Show("There is no text selected ", "Attention", MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
        }

        private void menuItem4_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(rTBMoreInfo.Text);
        }

        private void AddMenuShortcuts()
        {
            menuItem1.Shortcut = Shortcut.CtrlC;
            menuItem2.Shortcut = Shortcut.CtrlA;
            menuItem3.Shortcut = Shortcut.CtrlC;
            menuItem4.Shortcut = Shortcut.CtrlA;
            menuItem1.ShowShortcut = true;
            menuItem2.ShowShortcut = true;
            menuItem3.ShowShortcut = true;
            menuItem4.ShowShortcut = true;
        }

        private void btnSendBagReport_Click(object sender, EventArgs e)
        {
            string s = "mailto:screamu@pisem.net?subject=BZEditor v."+Application.ProductVersion+" BUG";
            s += "&body=BZ Editor v." + Application.ProductVersion;
            s += "%0A%0DErr%20message:%0D";
            s += rTBWhatHappened.Text.Replace(" ", "%20");
            s += "%0A%0DSource:%0D";
            s += _src.Replace(" ", "%20");
            s += "%0A%0DStack:%0D";
            s += _stck.Replace("\r", "").Replace("\n", "%0D").Replace(" ", "%20");
            s += "%0A%0DКомментарий:%0D";
            Process.Start(s);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExceptionForm));
            this.rTBMoreInfo = new System.Windows.Forms.RichTextBox();
            this.contextMenu2 = new System.Windows.Forms.ContextMenu();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.panelMain = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.rTBWhatHappened = new System.Windows.Forms.RichTextBox();
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.labelPicture = new System.Windows.Forms.Label();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.buttonClose = new System.Windows.Forms.Button();
            this.btnSendBagReport = new System.Windows.Forms.Button();
            this.buttonMoreInfo = new System.Windows.Forms.Button();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.panelMain.SuspendLayout();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // rTBMoreInfo
            // 
            this.rTBMoreInfo.BackColor = System.Drawing.SystemColors.Control;
            this.rTBMoreInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rTBMoreInfo.ContextMenu = this.contextMenu2;
            this.rTBMoreInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rTBMoreInfo.HideSelection = false;
            this.rTBMoreInfo.Location = new System.Drawing.Point(3, 16);
            this.rTBMoreInfo.Name = "rTBMoreInfo";
            this.rTBMoreInfo.Size = new System.Drawing.Size(485, 115);
            this.rTBMoreInfo.TabIndex = 0;
            this.rTBMoreInfo.Text = "";
            // 
            // contextMenu2
            // 
            this.contextMenu2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem3,
            this.menuItem4});
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 0;
            this.menuItem3.Text = "Копировать выделенное";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 1;
            this.menuItem4.Text = "Копировать все";
            this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.btnOK);
            this.panelMain.Controls.Add(this.rTBWhatHappened);
            this.panelMain.Controls.Add(this.labelPicture);
            this.panelMain.Controls.Add(this.buttonClose);
            this.panelMain.Controls.Add(this.btnSendBagReport);
            this.panelMain.Controls.Add(this.buttonMoreInfo);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMain.Location = new System.Drawing.Point(2, 2);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(491, 133);
            this.panelMain.TabIndex = 2;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(331, 98);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 16;
            this.btnOK.Text = "OK";
            this.btnOK.Visible = false;
            // 
            // rTBWhatHappened
            // 
            this.rTBWhatHappened.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rTBWhatHappened.BackColor = System.Drawing.SystemColors.Control;
            this.rTBWhatHappened.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rTBWhatHappened.ContextMenu = this.contextMenu1;
            this.rTBWhatHappened.Location = new System.Drawing.Point(60, 8);
            this.rTBWhatHappened.Name = "rTBWhatHappened";
            this.rTBWhatHappened.ReadOnly = true;
            this.rTBWhatHappened.Size = new System.Drawing.Size(427, 88);
            this.rTBWhatHappened.TabIndex = 15;
            this.rTBWhatHappened.TabStop = false;
            this.rTBWhatHappened.Text = "";
            // 
            // contextMenu1
            // 
            this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem2});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.Text = "Копировать выделенное";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 1;
            this.menuItem2.Text = "Копировать все";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // labelPicture
            // 
            this.labelPicture.ImageIndex = 0;
            this.labelPicture.ImageList = this.imageList;
            this.labelPicture.Location = new System.Drawing.Point(8, 8);
            this.labelPicture.Name = "labelPicture";
            this.labelPicture.Size = new System.Drawing.Size(48, 48);
            this.labelPicture.TabIndex = 7;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "");
            this.imageList.Images.SetKeyName(1, "");
            this.imageList.Images.SetKeyName(2, "");
            this.imageList.Images.SetKeyName(3, "");
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(411, 98);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 9;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // btnSendBagReport
            // 
            this.btnSendBagReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSendBagReport.Location = new System.Drawing.Point(118, 98);
            this.btnSendBagReport.Name = "btnSendBagReport";
            this.btnSendBagReport.Size = new System.Drawing.Size(138, 23);
            this.btnSendBagReport.TabIndex = 10;
            this.btnSendBagReport.Text = "Подготовить багрепорт";
            this.btnSendBagReport.Click += new System.EventHandler(this.btnSendBagReport_Click);
            // 
            // buttonMoreInfo
            // 
            this.buttonMoreInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonMoreInfo.Location = new System.Drawing.Point(4, 98);
            this.buttonMoreInfo.Name = "buttonMoreInfo";
            this.buttonMoreInfo.Size = new System.Drawing.Size(108, 23);
            this.buttonMoreInfo.TabIndex = 10;
            this.buttonMoreInfo.Click += new System.EventHandler(this.buttonMoreInfo_Click);
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.rTBMoreInfo);
            this.groupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox.Location = new System.Drawing.Point(2, 135);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(491, 134);
            this.groupBox.TabIndex = 1;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Содержимое стека";
            this.groupBox.Visible = false;
            // 
            // ExceptionForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(495, 271);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.panelMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(492, 156);
            this.Name = "ExceptionForm";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Сообщение:";
            this.TopMost = true;
            this.panelMain.ResumeLayout(false);
            this.groupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
    }
}