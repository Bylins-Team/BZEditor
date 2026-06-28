using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DataUtils;
using System.Text;

namespace BZEditor
{
    public partial class PrepareZoneToSendForm : BaseDialog
    {
        private ProcessCaller processCaller;
        private readonly string zoneNum;
        private readonly string zonesToSendPath;

        public PrepareZoneToSendForm()
        {
            InitializeComponent();
        }

        public PrepareZoneToSendForm(string zoneNum, string zoneName)
            : this()
        {
            
            zonesToSendPath = Path.Combine(Application.StartupPath, "ZonesToSend");
            this.zoneNum = zoneNum;
            lInfo.Text = "Зона: [" + zoneNum + "] " + zoneName;
            string s = zoneNum + " " + zoneName + ".7z";
            foreach (char ic in Path.GetInvalidFileNameChars())
                s = s.Replace(ic, '_').Replace(" ", "_");
            tbArcName.Text = s;
        }

        private bool ValidateName()
        {
            string s = "";
            foreach (char ic in Path.GetInvalidFileNameChars())
            {
                if (tbArcName.Text.Contains(ic.ToString()))
                    s += (s.Length > 0) ? ", " + ic : ic.ToString();
            }
            if (s.Length > 0)
            {
                errorProvider.SetError(tbArcName, "Название содержит недопустимые символы:" + s);
                return false;
            }
            errorProvider.SetError(tbArcName, "");
            return true;
        }

        private void tbArcName_TextChanged(object sender, EventArgs e)
        {
            ValidateName();
        }

        private void bContinuePreparing_Click(object sender, EventArgs e)
        {
            if (bContinuePreparing.Tag.ToString() == "1")
            {
                try
                {
                    Process.Start(zonesToSendPath);
                    Close();
                }
                catch
                {
                }
            }
            else
            {
                if (!ValidateName()) return;
                Utils.EnsureDirectory(zonesToSendPath);
                string filePath = Path.Combine(zonesToSendPath, tbArcName.Text);
                if (File.Exists(filePath))
                {
                    DialogResult dr =
                        MessageBox.Show($"Файл {filePath} уже существует! Перезаписать?", "Файл уже существует",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == DialogResult.No) return;
                }
                bCancelPreparing.Text = "Отмена";
                bCancelPreparing.Tag = 1;
                Cursor = Cursors.AppStarting;
                
                processCaller = new ProcessCaller(this)
                                    {                                        
                                        FileName = Path.Combine(Application.StartupPath, "7z.exe"),
                                        Arguments = GetArguments(filePath)
                                    };
                processCaller.StdErrReceived += WriteStreamInfo;
                processCaller.StdOutReceived += WriteStreamInfo;
                processCaller.Completed += ProcessCompleted;
                processCaller.Cancelled += ProcessCanceled;
                //processCaller.Failed += no event handler for this one, yet.
                tbArcName.ReadOnly = true;
                processCaller.Start();
            }
        }

        private string GetArguments(string filePath)
        {
            string res = "a \"" + filePath + "\"";
            string fpath = Path.Combine(StaticData.WorldFolderPath, @"mob\" + zoneNum + ".mob");
            if (File.Exists(fpath))
                res += " \"" + fpath + "\"";
            fpath = Path.Combine(StaticData.WorldFolderPath, @"obj\" + zoneNum + ".obj");
            if (File.Exists(fpath))
                res += " \"" + fpath + "\"";
            fpath = Path.Combine(StaticData.WorldFolderPath, @"shp\" + zoneNum + ".shp");
            if (File.Exists(fpath))
                res += " \"" + fpath + "\"";
            fpath = Path.Combine(StaticData.WorldFolderPath, @"trg\" + zoneNum + ".trg");
            if (File.Exists(fpath))
                res += " \"" + fpath + "\"";
            fpath = Path.Combine(StaticData.WorldFolderPath, @"wld\" + zoneNum + ".wld");
            if (File.Exists(fpath))
                res += " \"" + fpath + "\"";
            fpath = Path.Combine(StaticData.WorldFolderPath, @"wld\" + zoneNum + ".map");
            if (cbMapIncluding.Checked && File.Exists(fpath))
                res += " \"" + fpath + "\"";
            fpath = Path.Combine(StaticData.WorldFolderPath, @"wld\" + zoneNum + ".skt");
            if (cbSktIncluding.Checked && File.Exists(fpath))
                res += " \"" + fpath + "\"";
            fpath = Path.Combine(StaticData.WorldFolderPath, @"zon\" + zoneNum + ".zon");
            if (File.Exists(fpath))
                res += " \"" + fpath + "\"";
            res += " -y -mx9 -ms -mmt=on";
            return res;
        }

        private void WriteStreamInfo(object sender, DataReceivedEventArgs e)
        {            
            string res = Convert(e.Text, Encoding.GetEncoding("cp866"), Encoding.Default);

            listBoxOutput.Items.Add(res);
            listBoxOutput.TopIndex = listBoxOutput.Items.Count - 1;
            Regex r = new Regex("Compressing  \\b(?<filename>\\d+)");
            Match m = r.Match(e.Text);
            if (m.Success)
            {
                lStatus.Text = "Статус: Упаковывается " + m.Groups["filename"].Value;
                Application.DoEvents();
            }
            if (e.Text == "Everything is Ok")
                lStatus.Text = "Статус: Упаковка завершена успешно.";
        }

        public static string Convert(string value, Encoding src, Encoding trg)
        {
            Decoder dec = src.GetDecoder();
            byte[] ba = trg.GetBytes(value);
            int len = dec.GetCharCount(ba, 0, ba.Length);
            char[] ca = new char[len];
            dec.GetChars(ba, 0, ba.Length, ca, 0);
            return new string(ca);
        }
        
        private void ProcessCanceled(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            bCancelPreparing.Text = "Закрыть";
            bCancelPreparing.Tag = 0;
            Application.DoEvents();
        }

        private void ProcessCompleted(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            bContinuePreparing.Text = "Открыть папку с архивом";
            bContinuePreparing.Tag = 1;
            bCancelPreparing.Text = "Закрыть";
            bCancelPreparing.Tag = 0;
            Application.DoEvents();
        }

        private void bCancelPreparing_Click(object sender, EventArgs e)
        {
            if (bCancelPreparing.Tag.ToString() == "1")
            {
                processCaller?.Cancel();
                bCancelPreparing.Text = "Закрыть";
                bCancelPreparing.Tag = 0;
            }
            else
            {
                DialogResult = bContinuePreparing.Tag.ToString() == "1" ? DialogResult.OK : DialogResult.Cancel;
            }
        }
    }
}