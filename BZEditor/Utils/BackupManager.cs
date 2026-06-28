namespace BZEditor
{
    using System.IO;
    using System.IO.Compression;
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
            try
            {
                string backupPath = Path.Combine(StaticData.WorldFolderPath, "ZonesBackup");
                Utils.EnsureDirectory(backupPath);
                string zoneNum = zdm.Zone.Number.ToString();
                string filePath = Path.Combine(backupPath,
                    zoneNum + "__" + DateTime.Now.ToString("dd.MM.yyyy_hh.mm.ss") + ".zip");
                if (File.Exists(filePath))
                    File.Delete(filePath);

                // A YAML zone is the whole zones/<n>/ directory. On the very first
                // save the directory may not exist yet -- that is not a failure,
                // there is simply nothing to back up.
                string zoneDir = Path.Combine(StaticData.WorldFolderPath, "zones", zoneNum);
                if (Directory.Exists(zoneDir))
                {
                    using (ZipArchive zip = ZipFile.Open(filePath, ZipArchiveMode.Create))
                    {
                        foreach (string file in Directory.GetFiles(zoneDir, "*", SearchOption.AllDirectories))
                        {
                            string rel = file.Substring(zoneDir.Length)
                                             .TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
                                             .Replace(Path.DirectorySeparatorChar, '/');
                            zip.CreateEntryFromFile(file, zoneNum + "/" + rel, CompressionLevel.Optimal);
                        }
                    }
                }

                BackupFinished?.Invoke(true, zdm);
            }
            catch
            {
                BackupFinished?.Invoke(false, zdm);
            }
        }
    }
}
