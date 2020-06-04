#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using SystemFrameworks;
using DataUtils;
using ExtControls;
using WeifenLuo.WinFormsUI.Docking;

#endregion

namespace BZEditor
{
    /// <summary>
    ///   Summary description for MainForm.
    /// </summary>
    public class MainForm : Form
    {
        private const string HomepageUrl = "http://www.udomlya.ru/~scream/home/";
        private const string ChangesUrl = "http://www.udomlya.ru/~scream/Editor/Last/changes.txt";
        private const string UpdateUrl = "http://www.udomlya.ru/~scream/Editor/Last/lastver.txt";
        private const string VersionName = "";

        private readonly bool clearSketchAfterGeneratingRooms = true;
        private readonly ArrayList dmArray = new ArrayList();
        private readonly Configuration settings = new Configuration();
        private CBasesDataManager basesDm;
        private IContainer components;
        private DockPanel dockContainerMain;
        private ImageList imageListSmallButtons;

        private MenuStrip menuStrip;
        private readonly List<string> openedWindowsList = new List<string>();

        private StatusStrip statusStrip;
        private TemplatesDataManager templatesDm;
        private TemplatesForm templatesForm;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripSeparator tsSplitter2;
        private ToolStripSeparator toolStripMenuItem4;
        private ToolStripSeparator toolStripMenuItem5;
        private ToolStripMenuItem tsmiAbout;
        private ToolStripMenuItem tsmiCheckUpdates;
        private ToolStripMenuItem tsmiCreateNewZone;
        private ToolStripMenuItem tsmiExitApp;
        private ToolStripMenuItem tsmiHelp;
        private ToolStripMenuItem tsmiSaveAllZones;
        private ToolStripMenuItem tsmiWhatsNew;
        private ToolStripProgressBar tspbSaveProgress;
        private ToolStripStatusLabel tsslSaveIco;
        private ToolStripStatusLabel tsslSaveZoneName;
        private ToolStripStatusLabel tsslTextPosStatus;
        private ToolStripStatusLabel tsslWorlFolderPath;
        public FileListsDataManager FileListsDm;
        private ZonesListForm zonesListForm;
        private ToolStripMenuItem видToolStripMenuItem;
        private ToolStripMenuItem списокЗонToolStripMenuItem;
        private ToolStripMenuItem справкаToolStripMenuItem;
        private ToolStripMenuItem файлToolStripMenuItem;
        private ToolStripMenuItem tsmiOptions;
        private ToolStripMenuItem tsmiSameOptionsForAllZones;
        private ToolStripMenuItem tsmiCheckUpdatesOnStartup;
        private ToolStripMenuItem tsmiGoHomePage;
        private ToolStripSeparator tsSplitter1;
        private ToolStripMenuItem tsmiCreateSketch;
        private ToolStripMenuItem tsmiPathToWorldFolder;
        private ToolStripMenuItem tsmiBackupZones;
        private ToolStripSeparator toolStripMenuItemNoConflict;
        private ToolStripMenuItem browseZonesToSend;
        private ToolStripMenuItem шаблоныToolStripMenuItem;

        public MainForm()
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;
            StaticData.ConfigFolder = Path.Combine(Application.StartupPath, "Configurations");
            StaticData.CurrentEncoding = Encoding.GetEncoding("koi8-r");

            settings.Open();
            clearSketchAfterGeneratingRooms =
                settings.Read("clearSketchAfterGeneratingRooms", clearSketchAfterGeneratingRooms);
            Size = settings.Read("Size", new Size(200, 100));
            Location = settings.Read("Location", new Point(0, 0));
            WindowState = settings.Read("WindowState", FormWindowState.Maximized);
            StaticData.WorldFolderPath =
                settings.Read("PathToWorldFolder", Path.Combine(Application.StartupPath, "world"));
            string ozl = settings.Read("OpenedZonesList", "");
            try
            {
                foreach (string wname in ozl.Split(','))
                    openedWindowsList.Add(wname);
            }
            catch
            {
                
            }
            tsmiSameOptionsForAllZones.Checked = settings.Read("tsmiSameOptionsForAllZones", tsmiSameOptionsForAllZones.Checked);
            tsmiCheckUpdatesOnStartup.Checked = settings.Read("tsmiCheckUpdatesOnStartup", tsmiCheckUpdatesOnStartup.Checked);
            tsmiBackupZones.Checked = settings.Read("tsmiBackupZones", tsmiBackupZones.Checked);

            var sf = new SplashForm
                         {
                             Version = ("\n\nv." + Application.ProductVersion + " " + VersionName)
                         };
            //DateTime StartTime = DateTime.Now;

            if (!ValidateDirStruct())
                Close();
#if !DEBUG
            sf.Show();
#endif
            sf.TopLevel = true;
            if (tsmiCheckUpdatesOnStartup.Checked)
            {
#if !DEBUG
                sf.SetNextState(0, "Поиск обновлений");
                Application.DoEvents();
#endif
                CheckUpdates(true);
            }
#if !DEBUG
            sf.SetNextState(5, "Загрузка базовых данных");
            Application.DoEvents();
#endif
            basesDm = new CBasesDataManager(Application.StartupPath);
            basesDm.LoadData();
#if !DEBUG
            sf.SetNextState(7, "Загрузка списка зон");
            Application.DoEvents();
#endif
            FileListsDm = new FileListsDataManager();
            FileListsDm.LoadData();
#if !DEBUG
            sf.SetNextState(10, "Загрузка шаблонов");
            float step = 90/(float) FileListsDm.LoadedZonesCount;
            float incr = 0;
#endif
            templatesDm = new TemplatesDataManager(Path.Combine(Application.StartupPath, "Templates"));
            templatesDm.LoadData();
            foreach (ZoneData zd in FileListsDm.ZonesDataList)
            {
                if (!zd.Preloading) continue;
#if !DEBUG
                incr += step;
                if (incr > 1)
                {
                    sf.SetNextState((int) Math.Floor(sf.Position + Math.Floor(incr)), "Зона №" + zd.FileName);
                    incr -= (float) Math.Floor(incr);
                }
                else
                    sf.SetNextState("Зона №" + zd.FileName);
                Application.DoEvents();
#endif
                var zoneDm = new ZoneDataManager(zd.FileName, StaticData.CurrentEncoding);
                zoneDm.ExceptionThrowed += ZoneDmExceptionThrowed;
                if (zoneDm.LoadData())
                {
                    zoneDm.Changed += ZoneDmChanged;
                    zoneDm.Saved += ZoneDmSaved;
                    dmArray.Add(zoneDm);
                    zd.State = ZoneState.Loaded;
                }
            }

            var templatesFormState = settings.Read("templatesFormDockState", DockState.DockLeftAutoHide);
            if (templatesFormState != DockState.Hidden && templatesFormState != DockState.Unknown)
            {
                templatesForm = new TemplatesForm(ref templatesDm);
                templatesForm.Show(dockContainerMain, templatesFormState);
                templatesForm.TemplateApplyingFired += TemplatesFormTemplateApplyingFired;
            }

            var zonesListFormState = settings.Read("zonesListFormDockState", DockState.DockLeftAutoHide);
            ShowZoneListForm(zonesListFormState);

#if !DEBUG
            sf.SetNextState(100, "Инициализация...");
            Application.DoEvents();
#endif
            foreach (string wname in openedWindowsList)
            {
                if (StringUtils.IsUnsignedNumber(wname))
                    OpenZoneWindow(wname, zonesListForm.GetZoneGuidByZoneNum(wname));
                else
                    OpenSketchWindow(wname, zonesListForm.GetSketchGuidByFileName(wname));
            }

            sf.Hide();
            sf.Dispose();
            Invalidate(true);
            dockContainerMain.ActiveDocumentChanged += DockContainerMainActiveDocumentChanged;
            SetText();
            string activeTabName = settings.Read("ActiveTabName", "");
            if (!string.IsNullOrEmpty(activeTabName))
            {
                //Проверка, а не создана ли уже эта форма, если создана, то передаем ей фокус
                foreach (var w in dockContainerMain.Documents)
                {
                    if (w is WldForm wldForm)
                    {
                        if (wldForm.ZoneDm.Zone.Number.ToString() == activeTabName)
                            wldForm.DockHandler.Activate();
                    }
                    else if (w is SketchForm form)
                    {
                        if (form.SketchName == activeTabName)
                            form.DockHandler.Activate();
                    }
                }
            }
        }

        private void SetText()
        {
            Text = $"BZ Editor [v.{Application.ProductVersion} {VersionName}]";
            tsslWorlFolderPath.Text = $"Путь к зонам [{StaticData.WorldFolderPath}]";
        }

        private static void ZoneDmExceptionThrowed(string message, Exception exception, EventLogEntryType type)
        {
            ExceptionForm.ExceptionCatcher(message, exception, type);
        }

        private bool ValidateDirStruct()
        {
            if (!Directory.Exists(StaticData.WorldFolderPath))
            {
                if (
                    MessageBox.Show(
                        "Не найдена дирректория с зонами (обычно назвается world)!\nУкажите пожалуйста путь к директории, содержащей зоны для редактирования.",
                        "Неверно указан путь", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) ==
                    DialogResult.Yes)
                    TsmiPathToWorldFolderClick(null, null);
                else
                    return false;
            }
            if (!Directory.Exists(Path.Combine(StaticData.WorldFolderPath, "ZON")))
            {
                if (
                    MessageBox.Show("Не найдена дирректория \"ZON\"! Будем создавать?", "Не найден путь",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    Directory.CreateDirectory(Path.Combine(StaticData.WorldFolderPath, "ZON"));
                else
                    return false;
            }
            if (!Directory.Exists(Path.Combine(StaticData.WorldFolderPath, "WLD")))
            {
                if (
                    MessageBox.Show("Не найдена дирректория \"WLD\"! Будем создавать?", "Не найден путь",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    Directory.CreateDirectory(Path.Combine(StaticData.WorldFolderPath, "WLD"));
                else
                    return false;
            }
            if (!Directory.Exists(Path.Combine(StaticData.WorldFolderPath, "MOB")))
            {
                if (
                    MessageBox.Show("Не найдена дирректория \"MOB\"! Будем создавать?", "Не найден путь",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    Directory.CreateDirectory(Path.Combine(StaticData.WorldFolderPath, "MOB"));
                else
                    return false;
            }
            if (!Directory.Exists(Path.Combine(StaticData.WorldFolderPath, "OBJ")))
            {
                if (
                    MessageBox.Show("Не найдена дирректория \"OBJ\"! Будем создавать?", "Не найден путь",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    Directory.CreateDirectory(Path.Combine(StaticData.WorldFolderPath, "OBJ"));
                else
                    return false;
            }
            if (!Directory.Exists(Path.Combine(StaticData.WorldFolderPath, "TRG")))
            {
                if (
                    MessageBox.Show("Не найдена дирректория \"TRG\"! Будем создавать?", "Не найден путь",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    Directory.CreateDirectory(Path.Combine(StaticData.WorldFolderPath, "TRG"));
                else
                    return false;
            }
            return true;
        }

        private void DockContainerMainActiveDocumentChanged(object sender, EventArgs e)
        {
            //Guid GUID = new Guid(dockContainerMain.ActiveDocument.DockHandler.Form.Tag.ToString());
            StaticData.CanFireChangeEvent = true;
        }

        /// <summary>
        ///   Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        ///   The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
//#if !DEBUG
            Application.ThreadException += ExceptionForm.ExceptionCatcher;
            AppDomain.CurrentDomain.UnhandledException += ExceptionForm.UnhandledException;
//#endif
            Application.Run(new MainForm());
        }

        private void TsmiSaveAllZonesClick(object sender, EventArgs e)
        {
            SaveAllZones();
        }

        private void TemplatesFormTemplateApplyingFired(Guid guid, TemplatesDataManager.TemplatesType templatesType)
        {
            ((WldForm) (dockContainerMain.ActiveDocument)).ApplyTemplate(guid, templatesType);
        }

        private void TsmiCreateNewZoneClick(object sender, EventArgs e)
        {
            var czf = new CreateZoneForm();
            DialogResult dres = czf.ShowDialog();
            if (dres == DialogResult.Cancel) return;
            var zdm = new ZoneDataManager(czf.ZoneNum.ToString(), StaticData.CurrentEncoding);
            zdm.Changed += ZoneDmChanged;
            zdm.Saved += ZoneDmSaved;
            dmArray.Add(zdm);
            zdm.Zone.Name = "Новая зона";
            zdm.Zone.Number = czf.ZoneNum;
            zdm.Zone.RepopType = 0;
            zdm.Zone.RepopTimer = 30;
            FileListsDm.AddZoneToList(czf.ZoneNum.ToString());
            Guid g = zonesListForm.AddItem(czf.ZoneNum.ToString(), zdm.Zone.Name);
            if (czf.OpenNewZone)
                OpenZoneWindow(czf.ZoneNum.ToString(), g);
        }

        private void CreateSketchClick(object sender, EventArgs e)
        {
            CreateSketchForm csf = new CreateSketchForm();
            if (csf.ShowDialog(this) == DialogResult.OK)
            {
                OpenSketchWindow(csf.Sketch, Guid.NewGuid());
            }
        }

        private void ZonesListFormSketchCreated(GlobalSketch sketch)
        {
            OpenSketchWindow(sketch, Guid.NewGuid());
        }

        private void TsmiPathToWorldFolderClick(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog {ShowNewFolderButton = false};

            if (fbd.ShowDialog() != DialogResult.OK) return;

            StaticData.WorldFolderPath = fbd.SelectedPath;
            MessageBox.Show("Для вступления изменений в силу необходимо перезагрузить редактор.", "Предупреждение",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void TsmiAboutClick(object sender, EventArgs e)
        {
            var af = new AboutForm
                         {
                             Version = ("v." + Application.ProductVersion + " " + VersionName)
                         };
            af.ShowDialog();
            af.Dispose();
        }

        private void TsmiHelpClick(object sender, EventArgs e)
        {
            //ToDo:Переход на сайт в раздел помощи
        }

        private void TsmiWhatsNewClick(object sender, EventArgs e)
        {
            try
            {
                Process.Start(Path.Combine(Application.StartupPath, "WhatsNew.txt"));
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Файл WhatsNew.txt не найден.", "Файл не найден", MessageBoxButtons.OK,
                                MessageBoxIcon.Asterisk);
            }
        }

        private void TsmiExitAppClick(object sender, EventArgs e)
        {
            Close();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowClose())
            {
                e.Cancel = true;
                return;
            }
            SaveSettings();
            FileListsDm.SaveData();
        }

        private bool AllowClose()
        {
            string unsavedZones = "";
            foreach (ZoneDataManager zdm in dmArray)
            {
                if (zdm.Zone.Modifyed)
                    unsavedZones += zdm.Zone.Number + ",";
            }
            string unsavedSketches = zonesListForm.FindUnsavedSketches().TrimEnd(',');
            string msg = string.Empty;
            if (unsavedZones.Length > 0)
            {
                msg += "Изменения в зон";
                msg += (unsavedZones.TrimEnd(',').IndexOf(',') < 0) ? "е: " : "ах: ";
                msg += unsavedZones.TrimEnd(',') + " не сохранены.\n";
            } if (unsavedSketches.Length > 0)
            {
                msg += "Изменения в эскиз";
                msg += (unsavedSketches.IndexOf(',') < 0) ? "е: " : "ах: ";
                msg += unsavedSketches + " не сохранены.\n";
            }
            if (!string.IsNullOrEmpty(msg))
                return MessageBox.Show(msg+"\nВыход из редактора приведет к потере изменений!\nЗавершить работу редактора изменений?", "Несохраненные изменения", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK;
            return true;
        }

        private void SaveSettings()
        {
            settings.Write("templatesFormDockState", templatesForm?.DockState ?? DockState.Unknown); 
            settings.Write("zonesListFormDockState", zonesListForm.DockState);
            foreach (var t in dockContainerMain.Contents)
            {
                if (t is WldForm form)
                    form.SaveConfig();
            }
            if (WindowState == FormWindowState.Normal)
            {
                settings.Write("Size", Size);
                settings.Write("Location", Location);
            }
            settings.Write("WindowState", WindowState);
            settings.Write("PathToWorldFolder", StaticData.WorldFolderPath);
            if (dockContainerMain.ActiveDocument != null)
            {
                if (dockContainerMain.ActiveDocument.DockHandler.Form is WldForm form)
                    settings.Write("ActiveTabName", form.ZoneDm.Zone.Number);
                else if (dockContainerMain.ActiveDocument.DockHandler.Form is SketchForm sketchForm)
                    settings.Write("ActiveTabName", sketchForm.SketchName);
            }
            else
                settings.Write("ActiveZoneNum", -1);
            string ozl = string.Empty;
            foreach (string wname in openedWindowsList)
                ozl += wname + ",";
            settings.Write("OpenedZonesList", ozl.TrimEnd(','));
            settings.Write("clearSketchAfterGeneratingRooms", clearSketchAfterGeneratingRooms);
            settings.Write("tsmiSameOptionsForAllZones", tsmiSameOptionsForAllZones.Checked);
            settings.Write("tsmiCheckUpdatesOnStartup", tsmiCheckUpdatesOnStartup.Checked);
            settings.Write("tsmiBackupZones", tsmiBackupZones.Checked);
            
            settings.Save();
        }

        private void СписокЗонToolStripMenuItemClick(object sender, EventArgs e) =>
           ShowZoneListForm(DockState.DockLeftAutoHide);

        private void ShowZoneListForm(DockState dockState)
        {
            if (zonesListForm == null)
            {
                zonesListForm = new ZonesListForm(ref FileListsDm);
                zonesListForm.ItemDoubleClicked += TreeFormItemDoubleClicked;
                zonesListForm.ZoneLoadingActivated += TreeFormZoneLoadingActivated;
                zonesListForm.ZoneSavingActivated += ZonesListFormZoneSavingActivated;
                zonesListForm.ZoneUnloadingActivated += TreeFormZoneUnloadingActivated;
                zonesListForm.ZonePrepareToSendActivated += ZonesListFormZonePrepareToSendActivated;
                zonesListForm.SketchCreated += ZonesListFormSketchCreated;
                zonesListForm.SketchSavingActivated += ZonesListFormSketchSavingActivated;
                zonesListForm.SketchRemoved += ZonesListFormSketchRemoved;
            }

            if (dockState != DockState.Hidden && dockState != DockState.Unknown)
                zonesListForm.Show(dockContainerMain, dockState);
        }

        private void ШаблоныToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (!dockContainerMain.Contents.Contains(templatesForm))
            {
                templatesForm = new TemplatesForm(ref templatesDm);
                templatesForm.TemplateApplyingFired +=
                    TemplatesFormTemplateApplyingFired;
            }
            templatesForm.Show(dockContainerMain, DockState.DockLeftAutoHide);
        }

        private void TsmiCheckUpdatesClick(object sender, EventArgs e)
        {
            CheckUpdates(false);
        }

        private static void CheckUpdates(bool quiet)
        {
            try
            {
                if (DLoadFromHttp(UpdateUrl, out var memStream))
                {
                    var sr = new StreamReader(memStream);
                    string res = sr.ReadToEnd();
                    string[] parts = res.Split('.');
                    int verMajor = int.Parse(parts[0]);
                    int verMinor = int.Parse(parts[1]);
                    int verFix = (parts.Length > 2) ? int.Parse(parts[1]) : 0;
                    sr.Close();
                    string[] partsCurr = Application.ProductVersion.Split('.');
                    int verMajorCurr = int.Parse(partsCurr[0]);
                    int verMinorCurr = int.Parse(partsCurr[1]);
                    int verFixCurr = (partsCurr.Length > 2) ? int.Parse(partsCurr[1]) : 0;
                    if (verMajor * 10000 + verMinor * 100 + verFix > verMajorCurr * 10000 + verMinorCurr * 100 + verFixCurr)
                    {
                        if (MessageBox.Show(
                                string.Format($"Доступно обновление до версии {res} \nЖелаете ознакомиться со списком изменений этой версии?"), "Доступно обновление", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            //BrowseHomepage();
                            string path = Path.Combine(Path.GetTempPath(), "changes.txt");
                            if (DLoadFromHttp(ChangesUrl, path))
                            {
                                Process.Start(path);
                            }
                        }
                    }
                    else if (!quiet)
                    {
                        MessageBox.Show("Вы пользуетесь самой свежей версией редактора!", "Обновления отсутствуют",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    }
                }
                else if (!quiet)
                {
                    MessageBox.Show("Не удалось загрузить информацию об обновлениях!", "Ошибка при проверке обновлений",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Asterisk);
                }
                memStream.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка при получении данных о доступном обновлении", "Ошибка при проверке обновлений", 
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);
            }
        }

        #region работа с HTTP.

        public static bool DLoadFromHttp(string url, string localFileName)
        {
            try
            {
                var wrq = (HttpWebRequest) WebRequest.Create(url);
                wrq.SendChunked = false;
                wrq.KeepAlive = false;
                WebResponse wr = wrq.GetResponse();
                FileStream fs = File.Create(localFileName);
                const int bufLen = 1024;
                var b = new byte[bufLen];
                int readIn;
                do
                {
                    readIn = wr.GetResponseStream().Read(b, 0, bufLen);
                    fs.Write(b, 0, readIn);
                } while (readIn != 0);
                fs.Close();
                GC.Collect();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static bool DLoadFromHttp(string url, out MemoryStream memStream)
        {
            memStream = new MemoryStream();
            try
            {
                var wrq = (HttpWebRequest) WebRequest.Create(url);
                wrq.SendChunked = false;
                wrq.KeepAlive = false;
                WebResponse wr = wrq.GetResponse();
                memStream.Seek(0, SeekOrigin.Begin);
                const int bufLen = 1024;
                var b = new byte[bufLen];
                int readIn;
                do
                {
                    readIn = wr.GetResponseStream().Read(b, 0, bufLen);
                    memStream.Write(b, 0, readIn);
                } while (readIn != 0);
                //fs.Close();
                memStream.Seek(0, SeekOrigin.Begin);
                GC.Collect();
            }
            catch
            {
                return false;
            }
            return true;
        }

        #endregion

        #region Работа со списком зон

        private static void ZonesListFormZonePrepareToSendActivated(string zoneNum, string zoneName)
        {
            var pzsf = new PrepareZoneToSendForm(zoneNum, zoneName);
            pzsf.ShowDialog();
        }

        private void ZonesListFormZoneSavingActivated(string zoneNum)
        {
            ZoneDataManager zdm = GetDataManagerByName(zoneNum);
            SaveData(zdm);
        }

        private void TreeFormZoneUnloadingActivated(string zoneNum)
        {
            ZoneDataManager zdm = GetDataManagerByName(zoneNum);
            if (zdm == null) return;
            dmArray.Remove(zdm);
        }

        private void TreeFormZoneLoadingActivated(string zoneNum, Encoding enc, bool resave)
        {
            if (enc == null) enc = StaticData.CurrentEncoding;
            ZoneDataManager zoneDm = new ZoneDataManager(zoneNum, enc);
            zoneDm.ExceptionThrowed += ZoneDmExceptionThrowed;
            if (!zoneDm.LoadData())
                return;
            zoneDm.Changed += ZoneDmChanged;
            zoneDm.Saved += ZoneDmSaved;
            zonesListForm.AddZoneToLoadedList(zoneDm.Zone.Number, zoneDm.Zone.Name);
            if (resave)
                //Предложить сохранить зону сразу после открытия уже в кои8
                if (MessageBox.Show(this,
                                    "Если Вы уверены что загруженная зона была именно в кодировке Win1251,\nто вы можете сразу сохранить ее в кодировке koi-8r для дальнейшего использования.\n\nСохранить зону в кодировке koi-8r?",
                                    "Зона загружена", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                    DialogResult.Yes)
                {
                    zoneDm.SaveData();
                }

            dmArray.Add(zoneDm);
        }

        private void TreeFormItemDoubleClicked(object sender, EventArgs e)
        {
            var etn = ((ExtListViewItem) (((ListView) sender).SelectedItems[0]));
            //Проверка, а не создана ли уже эта форма, если создана, то передаем ей фокус
            /*foreach (WldForm f in _dockContainerMain.Documents)
            {
                if (f.ZoneDm.Zone.Number.ToString() == etn.Num)
                    return;
            }*/
            foreach (var w in dockContainerMain.Documents)
            {
                if (w is WldForm && ((WldForm)w).ZoneDm.Zone.Number.ToString() == etn.FileName)
                {
                    w.DockHandler.Activate();
                    return;
                }
                if (w is SketchForm && ((SketchForm)w).Sketch.FileName == etn.FileName)
                {
                    w.DockHandler.Activate();
                    return;
                }
            }      
            //Создаем новую форму
            switch (etn.Type.ToLower())
            {
                case "wld":
                    OpenZoneWindow(etn.FileName, etn.ItemGuid);
                    break;
                case "skt":
                    OpenSketchWindow(etn.FileName, etn.ItemGuid);
                    break;
            }
        }

        private void OpenZoneWindow(string num, Guid itemGuid)
        {
            ZoneDataManager zdm = GetDataManagerByName(num);
            if (zdm == null) return;
            var wf = new WldForm(templatesDm, ref zdm, ref basesDm, this, clearSketchAfterGeneratingRooms, tsmiSameOptionsForAllZones.Checked);
            if (!openedWindowsList.Contains(zdm.Zone.Number.ToString()))
                openedWindowsList.Add(zdm.Zone.Number.ToString());
            wf.Tag = itemGuid;
            wf.TabText = string.Format("[{0}] {1}", zdm.Zone.Number, zdm.Zone.Name);
            wf.CursorPositionChanged += WldFormCursorPositionChanged;
            wf.SelectedPageChanged += WldFormSelectedPageChanged;
            wf.ZoneRenamed += WldFormZoneRenamed;
            wf.ZoneNumberChanged += WldFormZoneNumberChanged;
            wf.FormClosing += MdiFormFormClosing;
            wf.Show(dockContainerMain, DockState.Document);
            zonesListForm.SetLoadedZoneState(num, ZoneState.Opened);
        }

        private void WldFormZoneRenamed(int vnum, string newName)
        {
            FileListsDm.RenameZone(vnum.ToString(), newName);
            zonesListForm.RefreshZonesList();
        }

        private void WldFormZoneNumberChanged(int oldvnum, int newvnum)
        {
            FileListsDm.ChangeZoneNumber(oldvnum, newvnum);
            zonesListForm.RefreshZonesList();
            if (MessageBox.Show("Удалить старые файлы зоны с номером " + oldvnum + " ?", "Удаление старых файлов",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                FileListsDm.RemoveZone(oldvnum.ToString());
        }

        private void MdiFormFormClosing(object sender, FormClosingEventArgs e)
        {            
            if (sender is WldForm)
            {
                if (zonesListForm.GetZoneState((Guid) (((WldForm) sender).Tag)) != 3)
                    zonesListForm.SetZoneState((Guid) (((WldForm) sender).Tag), 0);
                openedWindowsList.Remove(((WldForm) sender).ZoneDm.Zone.Number.ToString());
            }
            else if (sender is SketchForm)
            {
                if (zonesListForm.GetSketchState((Guid)(((SketchForm)sender).Tag)) != 8)
                    zonesListForm.SetSketchState((Guid)(((SketchForm)sender).Tag), 6);
                //ToDo: Удаление из списка открытых окон
                openedWindowsList.Remove(((SketchForm)sender).SketchName);                
            }
        }

        private void WldFormSelectedPageChanged(string name)
        {
            switch (name)
            {
                case "tpObjects":
                    templatesForm.SetSelectedTab(0);
                    break;
                case "tpMobs":
                    templatesForm.SetSelectedTab(1);
                    break;
            }
        }

        private void WldFormCursorPositionChanged(int col, int row)
        {
            tsslTextPosStatus.Text = "Строка: " + (row + 1) + " | Позиция: " + col;
            if (col > StaticData.MaxTextWidth) tsslTextPosStatus.ForeColor = Color.Red;
            else if (col > StaticData.OptimalTextWidth) tsslTextPosStatus.ForeColor = Color.DarkGreen;
            else tsslTextPosStatus.ForeColor = SystemColors.ControlText;
        }

        private void OpenSketchWindow(string sketchFileName, Guid itemGuid)
        {
            string sketchName = FileListsDm.GetSketchName(sketchFileName);
            if (string.IsNullOrEmpty(sketchName)) return;
            if (!openedWindowsList.Contains(sketchFileName))
                openedWindowsList.Add(sketchFileName);
            GlobalSketch sketch = new GlobalSketch { Name = sketchName, FileName = sketchFileName };
            OpenSketchWindow(sketch, itemGuid);
        }

        private void OpenSketchWindow(GlobalSketch sketch, Guid itemGuid)
        {
            sketch.LoadData();
            SketchForm sf = new SketchForm(sketch) { SketchName = sketch.Name, Tag = itemGuid, TabText = string.Format("[Э] {0}", sketch.Name) };
            sf.Changed += SketchChanged;
            sf.Saved += SketchSaved;
            sf.ZonesGenerated += SketchZonesGenerated;
            sf.FormClosing += MdiFormFormClosing;
            sf.Show(dockContainerMain, DockState.Document);
            zonesListForm.SetSketchState(itemGuid, 7);
        }

        private void SketchZonesGenerated(Guid windowId, List<ZoneDataManager> zones)
        {
            string msg = string.Empty;
            List<int> existed = new List<int>(zones.Count);
            foreach (ZoneDataManager zone in zones)
            {
                string name = FileListsDm.ZoneName(zone.Zone.Number.ToString());
                if (!string.IsNullOrEmpty(name))
                {
                    msg += "\n[" + zone.Zone.Number + "] " + zone.Zone.Name + " <=> " + name;
                    existed.Add(zone.Zone.Number);
                }
            }
            if (!string.IsNullOrEmpty(msg))
            {
                if (
                    MessageBox.Show(this,
                                    "Номер зоны совпадает с уже имеющимся:" + msg +
                                    "\nПересоздать зоны с совпадающими номерами?", "Пересоздать",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //Удалить из списках зон и списак датаменеджеров старые зоны, добавить новые в список и в датаменеджер, запрос на открытие окон с зонами
                    foreach (int vnum in existed)
                    {
                        foreach (ZoneDataManager zdm in dmArray)
                            if (zdm.Zone.Number == vnum)
                            {
                                zdm.Changed -= ZoneDmChanged;
                                zdm.Saved -= ZoneDmSaved;
                                foreach (var w in dockContainerMain.Documents)
                                    if (w is WldForm && ((WldForm)w).ZoneDm == zdm)
                                    {
                                        ((WldForm)w).Close();
                                        ((WldForm)w).Dispose();
                                    }
                            }
                        FileListsDm.RemoveZoneFromList(vnum.ToString());
                    }
                }
                else
                    return;
            }
            FinishCreatengZones(zones);
        }

        private void FinishCreatengZones(IEnumerable<ZoneDataManager> zones)
        {
            bool openCreatedZones =
                MessageBox.Show(this, "Открывать сгенерированные зоны на редактирование?", "Генерация зон",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
            foreach (ZoneDataManager zdm in zones)
            {
                zdm.Changed += ZoneDmChanged;
                zdm.Saved += ZoneDmSaved;
                dmArray.Add(zdm);
                zdm.Zone.RepopType = 0;
                zdm.Zone.RepopTimer = 30;
                FileListsDm.AddZoneToList(zdm.Zone.Number.ToString());
                Guid g = zonesListForm.AddItem(zdm.Zone.Number.ToString(), zdm.Zone.Name);
                if (openCreatedZones)
                    OpenZoneWindow(zdm.Zone.Number.ToString(), g);
            }
        }

        private void SketchSaved(Guid windowId, GlobalSketch sketch)
        {
            zonesListForm.SetSketchState(windowId, 7);
        }

        private void SketchChanged(Guid windowId, GlobalSketch sketch)
        {
            zonesListForm.SetSketchState(windowId, 8);
        }

        private void ZonesListFormSketchSavingActivated(string sketchName)
        {
            foreach (var w in dockContainerMain.Documents)
                if (w is SketchForm && ((SketchForm)w).SketchName == sketchName)
                {
                    ((SketchForm)w).Sketch.SaveData();
                }
        }

        private void ZonesListFormSketchRemoved(string sketchName)
        {
            foreach (var w in dockContainerMain.Documents)
                if (w is SketchForm && ((SketchForm)w).SketchName == sketchName)
                {
                    ((SketchForm) w).Close();
                    ((SketchForm) w).Dispose();
                }
        }

        #endregion

        #region Работа с массивом датаменеджеров

        private void ZoneDmChanged(string dmName, object changedClass, object sender)
        {
            zonesListForm.SetLoadedZoneState(dmName, ZoneState.Changed);
        }

        private void ZoneDmSaved(string dmName)
        {
            foreach (var f in dockContainerMain.Documents)
            {
                if (f is WldForm form)
                {
                    if (form.ZoneDm.Zone.Number.ToString() == dmName)
                    {
                        zonesListForm.SetLoadedZoneState(dmName, ZoneState.Opened);
                        return;
                    }
                }
                else if (f is SketchForm)
                {
                    //ToDo: Сохранение скетча отрисовать, а ждя этого реализовать в эскизе механизм отслеживания изменений
                }
            }
            zonesListForm.SetLoadedZoneState(dmName, 0);
        }

        private void SaveAllZones()
        {
            tspbSaveProgress.Value = 0;
            tsslSaveZoneName.Text = "";
            tspbSaveProgress.Visible = true;
            tsslSaveZoneName.Visible = true;
            tsslSaveIco.Visible = true;
            tspbSaveProgress.Maximum = dmArray.Count;
            foreach (ZoneDataManager dm in dmArray)
            {
                tsslSaveZoneName.Text = "[" + dm.Zone.Number + "]" + dm.Zone.Name;
                SaveData(dm);
                tspbSaveProgress.Value++;
                Application.DoEvents();
            }
            tspbSaveProgress.Visible = false;
            tsslSaveZoneName.Visible = false;
            tsslSaveIco.Visible = false;
        }

        private ZoneDataManager GetDataManagerByName(string number)
        {
            foreach (ZoneDataManager dm in dmArray)
                if (dm.Zone.Number.ToString() == number) return dm;
            return null;
        }

        public MobsCollection GetAllKnownMobs()
        {
            var resCollection = new MobsCollection();
            foreach (ZoneDataManager dm in dmArray)
            {
                foreach (Mob mob in dm.Mobs)
                    resCollection.Add(mob);
            }
            return resCollection;
        }

        public ObjsCollection GetAllKnownObjects()
        {
            var resCollection = new ObjsCollection();
            foreach (ZoneDataManager dm in dmArray)
            {
                foreach (Obj obj in dm.Objects)
                    resCollection.Add(obj);
            }
            return resCollection;
        }

        public ObjsCollection GetAllKnownObjects(int objType)
        {
            var resCollection = new ObjsCollection();
            foreach (ZoneDataManager dm in dmArray)
            {
                foreach (Obj obj in dm.Objects)
                {
                    if (obj.Type == objType)
                        resCollection.Add(obj);
                }
            }
            return resCollection;
        }

        /// <summary></summary>
        /// <param name="trgClass">
        ///   0 - для мобов, 1 - для объектов, 2 - для комнат
        /// </param>
        /// <returns></returns>
        public TriggersCollection GetAllKnownTriggers(int trgClass)
        {
            var resCollection = new TriggersCollection();
            foreach (ZoneDataManager dm in dmArray)
            {
                foreach (Trigger trigger in dm.Triggers)
                {
                    if (trigger.Class == trgClass)
                        resCollection.Add(trigger);
                }
            }
            return resCollection;
        }

        public bool IsZoneExists(int vNum)
        {
            //Сделана проверка среди всех зон в списке, а не только среди загруженных
            foreach (ZoneData zd in FileListsDm.ZonesDataList)
                if (zd.VNum == vNum) return true;
            /*foreach (CZoneDataManager dm in _dmArray)
                if (dm.Zone.Number == vNum) return true;*/
            return false;
        }

        public TriggersCollection GetAllKnownTriggers()
        {
            var resCollection = new TriggersCollection();
            foreach (ZoneDataManager dm in dmArray)
            {
                foreach (Trigger trigger in dm.Triggers)
                    resCollection.Add(trigger);
            }
            return resCollection;
        }

        public RoomsCollection GetAllKnownRooms()
        {
            var resCollection = new RoomsCollection();
            foreach (ZoneDataManager dm in dmArray)
            {
                foreach (Room room in dm.Rooms)
                    resCollection.Add(room);
            }
            return resCollection;
        }

        #endregion

        #region Windows Form Designer generated code

        /// <summary>
        ///   Required method for Designer support - do not modify
        ///   the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.imageListSmallButtons = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCreateNewZone = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCreateSketch = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemNoConflict = new System.Windows.Forms.ToolStripSeparator();
            this.browseZonesToSend = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiSaveAllZones = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExitApp = new System.Windows.Forms.ToolStripMenuItem();
            this.видToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.списокЗонToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.шаблоныToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPathToWorldFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSameOptionsForAllZones = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiBackupZones = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCheckUpdatesOnStartup = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiWhatsNew = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCheckUpdates = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSplitter2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiGoHomePage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSplitter1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tsslTextPosStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslSaveIco = new System.Windows.Forms.ToolStripStatusLabel();
            this.tspbSaveProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.tsslSaveZoneName = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslWorlFolderPath = new System.Windows.Forms.ToolStripStatusLabel();
            this.dockContainerMain = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageListSmallButtons
            // 
            this.imageListSmallButtons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListSmallButtons.ImageStream")));
            this.imageListSmallButtons.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListSmallButtons.Images.SetKeyName(0, "");
            this.imageListSmallButtons.Images.SetKeyName(1, "");
            this.imageListSmallButtons.Images.SetKeyName(2, "");
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.видToolStripMenuItem,
            this.tsmiOptions,
            this.справкаToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(757, 24);
            this.menuStrip.TabIndex = 7;
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCreateNewZone,
            this.tsmiCreateSketch,
            this.toolStripMenuItemNoConflict,
            this.browseZonesToSend,
            this.toolStripMenuItem2,
            this.tsmiSaveAllZones,
            this.toolStripMenuItem1,
            this.tsmiExitApp});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // tsmiCreateNewZone
            // 
            this.tsmiCreateNewZone.Image = global::BZEditor.Properties.Resources.button_addzone;
            this.tsmiCreateNewZone.Name = "tsmiCreateNewZone";
            this.tsmiCreateNewZone.Size = new System.Drawing.Size(250, 22);
            this.tsmiCreateNewZone.Text = "Создать зону";
            this.tsmiCreateNewZone.Click += new System.EventHandler(this.TsmiCreateNewZoneClick);
            // 
            // tsmiCreateSketch
            // 
            this.tsmiCreateSketch.Image = global::BZEditor.Properties.Resources.button_addsketch;
            this.tsmiCreateSketch.Name = "tsmiCreateSketch";
            this.tsmiCreateSketch.Size = new System.Drawing.Size(250, 22);
            this.tsmiCreateSketch.Text = "Создать эскиз комплекса зон";
            this.tsmiCreateSketch.Click += new System.EventHandler(this.CreateSketchClick);
            // 
            // toolStripMenuItemNoConflict
            // 
            this.toolStripMenuItemNoConflict.Name = "toolStripMenuItemNoConflict";
            this.toolStripMenuItemNoConflict.Size = new System.Drawing.Size(247, 6);
            // 
            // browseZonesToSend
            // 
            this.browseZonesToSend.Image = global::BZEditor.Properties.Resources.zonesend;
            this.browseZonesToSend.Name = "browseZonesToSend";
            this.browseZonesToSend.Size = new System.Drawing.Size(250, 22);
            this.browseZonesToSend.Text = "Открыть каталог зон к отправке";
            this.browseZonesToSend.Click += new System.EventHandler(this.BrowseZonesToSendClick);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(247, 6);
            // 
            // tsmiSaveAllZones
            // 
            this.tsmiSaveAllZones.Image = global::BZEditor.Properties.Resources.button_saveall;
            this.tsmiSaveAllZones.Name = "tsmiSaveAllZones";
            this.tsmiSaveAllZones.Size = new System.Drawing.Size(250, 22);
            this.tsmiSaveAllZones.Text = "Сохранить всё";
            this.tsmiSaveAllZones.Click += new System.EventHandler(this.TsmiSaveAllZonesClick);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(247, 6);
            // 
            // tsmiExitApp
            // 
            this.tsmiExitApp.Image = global::BZEditor.Properties.Resources.button_exitapp;
            this.tsmiExitApp.Name = "tsmiExitApp";
            this.tsmiExitApp.Size = new System.Drawing.Size(250, 22);
            this.tsmiExitApp.Text = "Выход";
            this.tsmiExitApp.Click += new System.EventHandler(this.TsmiExitAppClick);
            // 
            // видToolStripMenuItem
            // 
            this.видToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.списокЗонToolStripMenuItem,
            this.шаблоныToolStripMenuItem});
            this.видToolStripMenuItem.Name = "видToolStripMenuItem";
            this.видToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.видToolStripMenuItem.Text = "Вид";
            // 
            // списокЗонToolStripMenuItem
            // 
            this.списокЗонToolStripMenuItem.Name = "списокЗонToolStripMenuItem";
            this.списокЗонToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.списокЗонToolStripMenuItem.Text = "Список зон";
            this.списокЗонToolStripMenuItem.Click += new System.EventHandler(this.СписокЗонToolStripMenuItemClick);
            // 
            // шаблоныToolStripMenuItem
            // 
            this.шаблоныToolStripMenuItem.Name = "шаблоныToolStripMenuItem";
            this.шаблоныToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.шаблоныToolStripMenuItem.Text = "Шаблоны";
            this.шаблоныToolStripMenuItem.Click += new System.EventHandler(this.ШаблоныToolStripMenuItemClick);
            // 
            // tsmiOptions
            // 
            this.tsmiOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiPathToWorldFolder,
            this.tsmiSameOptionsForAllZones,
            this.toolStripMenuItem5,
            this.tsmiBackupZones,
            this.tsmiCheckUpdatesOnStartup});
            this.tsmiOptions.Name = "tsmiOptions";
            this.tsmiOptions.Size = new System.Drawing.Size(78, 20);
            this.tsmiOptions.Text = "Настройка";
            // 
            // tsmiPathToWorldFolder
            // 
            this.tsmiPathToWorldFolder.Image = global::BZEditor.Properties.Resources.button_setworldfolder;
            this.tsmiPathToWorldFolder.Name = "tsmiPathToWorldFolder";
            this.tsmiPathToWorldFolder.Size = new System.Drawing.Size(289, 22);
            this.tsmiPathToWorldFolder.Text = "Изменить путь к папке \"world\"";
            this.tsmiPathToWorldFolder.Click += new System.EventHandler(this.TsmiPathToWorldFolderClick);
            // 
            // tsmiSameOptionsForAllZones
            // 
            this.tsmiSameOptionsForAllZones.CheckOnClick = true;
            this.tsmiSameOptionsForAllZones.Name = "tsmiSameOptionsForAllZones";
            this.tsmiSameOptionsForAllZones.Size = new System.Drawing.Size(289, 22);
            this.tsmiSameOptionsForAllZones.Text = "Общие настройки окон для всех зон";
            this.tsmiSameOptionsForAllZones.Click += new System.EventHandler(this.TsmiSameOptionsForAllZonesClick);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(286, 6);
            // 
            // tsmiBackupZones
            // 
            this.tsmiBackupZones.CheckOnClick = true;
            this.tsmiBackupZones.Name = "tsmiBackupZones";
            this.tsmiBackupZones.Size = new System.Drawing.Size(289, 22);
            this.tsmiBackupZones.Text = "Всегда сохранять резервные копии зон";
            this.tsmiBackupZones.CheckedChanged += new System.EventHandler(this.BackupZonesCheckedChanged);
            // 
            // tsmiCheckUpdatesOnStartup
            // 
            this.tsmiCheckUpdatesOnStartup.Checked = true;
            this.tsmiCheckUpdatesOnStartup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmiCheckUpdatesOnStartup.Name = "tsmiCheckUpdatesOnStartup";
            this.tsmiCheckUpdatesOnStartup.Size = new System.Drawing.Size(289, 22);
            this.tsmiCheckUpdatesOnStartup.Text = "Проверять обновления при старте";
            this.tsmiCheckUpdatesOnStartup.Click += new System.EventHandler(this.TsmiCheckUpdatesOnStartupClick);
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiHelp,
            this.toolStripMenuItem4,
            this.tsmiWhatsNew,
            this.tsmiCheckUpdates,
            this.tsSplitter2,
            this.tsmiGoHomePage,
            this.tsSplitter1,
            this.tsmiAbout});
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.справкаToolStripMenuItem.Text = "Справка";
            // 
            // tsmiHelp
            // 
            this.tsmiHelp.Image = global::BZEditor.Properties.Resources.help_content;
            this.tsmiHelp.Name = "tsmiHelp";
            this.tsmiHelp.Size = new System.Drawing.Size(198, 22);
            this.tsmiHelp.Text = "Web - справка";
            this.tsmiHelp.Visible = false;
            this.tsmiHelp.Click += new System.EventHandler(this.TsmiHelpClick);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(195, 6);
            // 
            // tsmiWhatsNew
            // 
            this.tsmiWhatsNew.Image = global::BZEditor.Properties.Resources.button_whatsnew;
            this.tsmiWhatsNew.Name = "tsmiWhatsNew";
            this.tsmiWhatsNew.Size = new System.Drawing.Size(198, 22);
            this.tsmiWhatsNew.Text = "Список изменений";
            this.tsmiWhatsNew.Click += new System.EventHandler(this.TsmiWhatsNewClick);
            // 
            // tsmiCheckUpdates
            // 
            this.tsmiCheckUpdates.Image = global::BZEditor.Properties.Resources.CheckUpdates;
            this.tsmiCheckUpdates.Name = "tsmiCheckUpdates";
            this.tsmiCheckUpdates.Size = new System.Drawing.Size(198, 22);
            this.tsmiCheckUpdates.Text = "Проверка обновления";
            this.tsmiCheckUpdates.Click += new System.EventHandler(this.TsmiCheckUpdatesClick);
            // 
            // tsSplitter2
            // 
            this.tsSplitter2.Name = "tsSplitter2";
            this.tsSplitter2.Size = new System.Drawing.Size(195, 6);
            // 
            // tsmiGoHomePage
            // 
            this.tsmiGoHomePage.Image = global::BZEditor.Properties.Resources.button_home_page;
            this.tsmiGoHomePage.Name = "tsmiGoHomePage";
            this.tsmiGoHomePage.Size = new System.Drawing.Size(198, 22);
            this.tsmiGoHomePage.Text = "Сайт программы";
            this.tsmiGoHomePage.Click += new System.EventHandler(this.TsmiGoHomePageClick);
            // 
            // tsSplitter1
            // 
            this.tsSplitter1.Name = "tsSplitter1";
            this.tsSplitter1.Size = new System.Drawing.Size(195, 6);
            // 
            // tsmiAbout
            // 
            this.tsmiAbout.Image = global::BZEditor.Properties.Resources.ktip1;
            this.tsmiAbout.Name = "tsmiAbout";
            this.tsmiAbout.Size = new System.Drawing.Size(198, 22);
            this.tsmiAbout.Text = "О программе...";
            this.tsmiAbout.Click += new System.EventHandler(this.TsmiAboutClick);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslTextPosStatus,
            this.tsslSaveIco,
            this.tspbSaveProgress,
            this.tsslSaveZoneName,
            this.tsslWorlFolderPath});
            this.statusStrip.Location = new System.Drawing.Point(0, 493);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.ShowItemToolTips = true;
            this.statusStrip.Size = new System.Drawing.Size(757, 24);
            this.statusStrip.TabIndex = 8;
            // 
            // tsslTextPosStatus
            // 
            this.tsslTextPosStatus.Name = "tsslTextPosStatus";
            this.tsslTextPosStatus.Size = new System.Drawing.Size(103, 19);
            this.tsslTextPosStatus.Text = "Позиция курсора";
            this.tsslTextPosStatus.ToolTipText = "Текущая позиция курсора";
            // 
            // tsslSaveIco
            // 
            this.tsslSaveIco.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tsslSaveIco.Image = global::BZEditor.Properties.Resources.button_savesmall;
            this.tsslSaveIco.Name = "tsslSaveIco";
            this.tsslSaveIco.Size = new System.Drawing.Size(20, 20);
            this.tsslSaveIco.Visible = false;
            // 
            // tspbSaveProgress
            // 
            this.tspbSaveProgress.Name = "tspbSaveProgress";
            this.tspbSaveProgress.Size = new System.Drawing.Size(100, 19);
            this.tspbSaveProgress.Step = 1;
            this.tspbSaveProgress.Visible = false;
            // 
            // tsslSaveZoneName
            // 
            this.tsslSaveZoneName.Enabled = false;
            this.tsslSaveZoneName.Name = "tsslSaveZoneName";
            this.tsslSaveZoneName.Size = new System.Drawing.Size(13, 19);
            this.tsslSaveZoneName.Text = "1";
            this.tsslSaveZoneName.Visible = false;
            // 
            // tsslWorlFolderPath
            // 
            this.tsslWorlFolderPath.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tsslWorlFolderPath.Name = "tsslWorlFolderPath";
            this.tsslWorlFolderPath.Size = new System.Drawing.Size(55, 19);
            this.tsslWorlFolderPath.Text = "c:\\world";
            // 
            // dockContainerMain
            // 
            this.dockContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockContainerMain.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingWindow;
            this.dockContainerMain.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.dockContainerMain.Location = new System.Drawing.Point(0, 24);
            this.dockContainerMain.Name = "dockContainerMain";
            this.dockContainerMain.Size = new System.Drawing.Size(757, 469);
            this.dockContainerMain.TabIndex = 9;
            this.dockContainerMain.ActiveDocumentChanged += new System.EventHandler(this.DockContainerMainActiveDocumentChanged);
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(757, 517);
            this.Controls.Add(this.dockContainerMain);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.statusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private void TsmiSameOptionsForAllZonesClick(object sender, EventArgs e)
        {
            //Загрузка во все открытые окна общих настроек
            foreach (var w in dockContainerMain.Documents)
            {
                ((WldForm) w).ReloadSettings(tsmiSameOptionsForAllZones.Checked);
            }
        }

        private void TsmiCheckUpdatesOnStartupClick(object sender, EventArgs e)
        {
            tsmiCheckUpdatesOnStartup.Checked = !tsmiCheckUpdatesOnStartup.Checked;
        }

        private void TsmiGoHomePageClick(object sender, EventArgs e)
        {
            BrowseHomepage();
        }

        private static void BrowseHomepage()
        {
            try
            {
                Process.Start(HomepageUrl);
            }
            catch
            {
                MessageBox.Show("При попытке открыть домашнюю страницу произошла ошибка.", "Нет возможности открыть добашнюю страницу", MessageBoxButtons.OK,
                                MessageBoxIcon.Asterisk);
            }
        }

        private void SaveData(ZoneDataManager zdm)
        {
            if (zdm == null) return;
            if (StaticData.BackupZones)
            {
                BackupManager bm = new BackupManager();
                bm.Backup(zdm);
                bm.BackupFinished += BackupFinished;
            }
            else
                zdm.SaveData();
        }

        private void BackupFinished(bool cucces, ZoneDataManager zdm)
        {
            if (!cucces)
            {
                if(MessageBox.Show(this, string.Format("Не удалось создать архивную копию зоны \"[{0}]{1}\" перед сохранением, продолжить сохранение зоны ?", zdm.Zone.Number, zdm.Zone.Name), "Резервное копирование", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    zdm.SaveData();
            }
            else
                zdm.SaveData();
        }

        private void BackupZonesCheckedChanged(object sender, EventArgs e)
        {
            StaticData.BackupZones = tsmiBackupZones.Checked;
        }

        private void BrowseZonesToSendClick(object sender, EventArgs e)
        {
            Process.Start(Path.Combine(Application.StartupPath, "ZonesToSend"));
        }
    }
}