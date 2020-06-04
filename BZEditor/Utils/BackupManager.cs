namespace BZEditor
{
    using System.Windows.Forms;
    using System.IO;
    using DataUtils;
    using System;

    public class BackupManager
    {
        #region Delegates & Events

        public delegate void BackupFinishEvent(bool cucces, ZoneDataManager zdm);

        public event BackupFinishEvent BackupFinished;

        #endregion

        private ZoneDataManager zdm;

        public void Backup(ZoneDataManager zdm)
        {
            this.zdm = zdm;
            string backupPath = Path.Combine(StaticData.WorldFolderPath, "ZonesBackup");
            Utils.EnsureDirectory(backupPath);
            string filePath = Path.Combine(backupPath, this.zdm.Zone.Number + "__" + DateTime.Now.ToString("dd.MM.yyyy_hh.mm.ss") + ".7z");

            if (File.Exists(filePath))
                File.Delete(filePath);

            ProcessCaller processCaller = new ProcessCaller(null)
            {
                FileName = Path.Combine(Application.StartupPath, "7z.exe"),
                Arguments = GetArguments(filePath, this.zdm.Zone.Number.ToString())
            };
            processCaller.StdErrReceived += WriteStreamInfo;
            processCaller.StdOutReceived += WriteStreamInfo;
            processCaller.Completed += ProcessCompleted;
            processCaller.Failed += ProcessFailed;
            processCaller.Start();
        }

        private static string GetArguments(string filePath, string zoneNum)
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
            if (File.Exists(fpath))
                res += " \"" + fpath + "\"";
            fpath = Path.Combine(StaticData.WorldFolderPath, @"wld\" + zoneNum + ".skt");
            if (File.Exists(fpath))
                res += " \"" + fpath + "\"";
            fpath = Path.Combine(StaticData.WorldFolderPath, @"zon\" + zoneNum + ".zon");
            if (File.Exists(fpath))
                res += " \"" + fpath + "\"";
            res += " -y -mx9 -ms -mmt=on";
            return res;
        }
        private void WriteStreamInfo(object sender, DataReceivedEventArgs e)
        {

        }

        private void ProcessCompleted(object sender, EventArgs e)
        {
            BackupFinished?.Invoke(true, zdm);
        }

        private void ProcessFailed(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            BackupFinished?.Invoke(false, zdm);
        }
    }
}
