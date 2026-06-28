using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
using DataUtils;

namespace BZEditor
{
    public partial class PrepareZoneToSendForm : BaseDialog
    {
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
            string s = zoneNum + " " + zoneName + ".zip";
            foreach (char ic in Path.GetInvalidFileNameChars())
                s = s.Replace(ic, '_').Replace(" ", "_");
            tbArcName.Text = s;

            // A YAML zone is the whole zones/<n>/ directory (sketches live inside the
            // room files, there is no separate map/sketch file), so these legacy
            // "include map / include sketches" toggles no longer mean anything.
            cbMapIncluding.Visible = false;
            cbSktIncluding.Visible = false;
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
                return;
            }

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

            tbArcName.ReadOnly = true;
            Cursor = Cursors.WaitCursor;
            try
            {
                listBoxOutput.Items.Clear();
                CreateZip(filePath);
                lStatus.Text = "Статус: Упаковка завершена успешно.";
                bContinuePreparing.Text = "Открыть папку с архивом";
                bContinuePreparing.Tag = 1;
            }
            catch (Exception ex)
            {
                lStatus.Text = "Статус: Ошибка упаковки.";
                listBoxOutput.Items.Add("Ошибка: " + ex.Message);
                MessageBox.Show("Не удалось создать архив:\n" + ex.Message, "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbArcName.ReadOnly = false;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Packs the zone's whole zones/&lt;n&gt;/ directory into a .zip, with each
        /// entry prefixed by the zone number so it extracts into a &lt;n&gt;/ folder.
        /// </summary>
        private void CreateZip(string zipPath)
        {
            string zoneDir = Path.Combine(StaticData.WorldFolderPath, "zones", zoneNum);
            if (!Directory.Exists(zoneDir))
                throw new DirectoryNotFoundException("Каталог зоны не найден: " + zoneDir);

            if (File.Exists(zipPath)) File.Delete(zipPath);

            using (ZipArchive zip = ZipFile.Open(zipPath, ZipArchiveMode.Create))
            {
                foreach (string file in Directory.GetFiles(zoneDir, "*", SearchOption.AllDirectories))
                {
                    string rel = file.Substring(zoneDir.Length)
                                     .TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
                                     .Replace(Path.DirectorySeparatorChar, '/');
                    string entryName = zoneNum + "/" + rel;
                    zip.CreateEntryFromFile(file, entryName, CompressionLevel.Optimal);
                    listBoxOutput.Items.Add("Упакован: " + entryName);
                }
            }
            listBoxOutput.Items.Add("Архив: " + zipPath);
            listBoxOutput.TopIndex = listBoxOutput.Items.Count - 1;
        }

        private void bCancelPreparing_Click(object sender, EventArgs e)
        {
            DialogResult = bContinuePreparing.Tag.ToString() == "1" ? DialogResult.OK : DialogResult.Cancel;
        }
    }
}
