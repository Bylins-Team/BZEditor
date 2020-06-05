using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DataUtils;
using ExtControls;
using Fireball.CodeEditor.SyntaxFiles;
using Fireball.Windows.Forms;
using System.Data;
using System.Diagnostics;
using System.Text.RegularExpressions;
using SystemFrameworks;
using Object = DataUtils.Obj;

namespace BZEditor
{
    public partial class WldForm : BaseMdiChildForm
    {
        #region Delegates

        public delegate void CursorPositionChangeEvent(int col, int row);

        public delegate void SelectedPageChangeEvent(string tabName);

        public delegate void ZoneRenameEvent(int vnum, string newName);

        public delegate void ZoneNumberChangeEvent(int oldVnum, int newVnum);

        #endregion

        public Room ActiveRoom;

        public bool MustRefreshTypeSpecParams = true;
        public bool MustUpdateTypeSpecParams = true;
        public bool MustUpdateMobData;
        public bool MustUpdateObjData;
        public bool IgnoreExitDirChanged;
        public RoomsCollection SelectedRooms = new RoomsCollection();
        public MainForm WindowParentForm;
        public int LastSelectedMob;
        public int LastSelectedObj;
        public int LastSelectedShp;
        public int LastSelectedTrg;
        public int LastTriggerEditedVNum;

        #region Внутренние флаги

        private string filterMob = "";
        private string filterObj = "";
        private string filterRoom = "";
        private string filterShp = "";
        private string filterTrg = "";
        private readonly RoomsCollection lastSelectedRooms = new RoomsCollection();

        private bool mustUpdateMobInRoomData;
        private bool mustUpdateMobVirtualInRoomData;

        private bool canDolvMainListSelectedIndexChanged = true;
        private bool canUpdateLastSelectedRooms = true;
        private ComboBox cboxMobObjState;
        private ComboBox cboxObjectLoadType;
        private ComboBox cboxObjectLoadSpecParam;

        private readonly ColorDialog colorDlg = new ColorDialog();
        private readonly Configuration zoneConfig;
        private Configuration config;
        private string exitDir = "";

        private readonly ArrayList history = new ArrayList(10);
        private int historyPosition = -1;
        private readonly ListItemDescCollection descColl = new ListItemDescCollection(); //Для раскраски клеток и комнат
        private bool dragDataValid;
        //private object lastFirstItemTag = new object();
        //private int lastMobListCondition;
        //private int lastObjListCondition;
        private int lastRoomListCondition = 1;
        //private int lastShpListCondition;
        private int lastTrgListCondition;
        private bool mustApplyObjTimerChanges = true;
        private bool mustStartDragging;
        //private int prevTabNum;
        private TemplatesDataManager templatesDm;

        #endregion

        public WldForm(TemplatesDataManager templatesDm, ref ZoneDataManager ZoneDm, ref CBasesDataManager BasesDm, MainForm parentForm, bool clearSketchAfterGeneratingRooms, bool commonSettings)
            :
                base(ZoneDm, BasesDm)
        {
            InitializeComponent();
            vertSBMap.Minimum = -100;
            vertSBMap.Maximum = 100;
            horizSBMap.Minimum = -100;
            horizSBMap.Maximum = 100;
            wldMap.CenterRoomXValueChanged += WldMapCenterRoomXValueChanged;
            wldMap.CenterRoomYValueChanged += WldMapCenterRoomYValueChanged;
            wldMap.ClearSketchAfterGeneratingRooms = clearSketchAfterGeneratingRooms;
            wldMap.ZoneDm = ZoneDm;
            ZoneDm.Saved += ZoneSaved;
            ZoneDm.Changed += ZoneChangedHandler;

            StaticData.CanFireChangeEvent = false;
            this.templatesDm = templatesDm;
            WindowParentForm = parentForm;
            //подготовка скриптэдитора
            CodeEditorSyntaxLoader.SetSyntax(codeEditor, SyntaxLanguage.DGScript);
            codeEditor.SetListItemsArray(BasesDm.ListItemsArray);

            this.ZoneDm = ZoneDm;
            lvZoneInfo.BringToFront();
            lvZoneInfo.Groups.Add("ZoneInfo", "Информация о зоне:");
            lvZoneInfo.Visible = true;
            RefreshZoneInfo();
            RefreshDetails(ZoneDm);
            RefreshVirtualRoomMobsList(ZoneDm);
            BindUi();

            wldMap.SetRoomsCollection(ref ZoneDm.Rooms, ref ZoneDm.SketchRooms,
                                      ZoneDm.Zone.Number);
            StaticData.CanFireChangeEvent = true;

            ErrVisibilityChanged(null, null);
            zoneConfig = new Configuration(Path.Combine(Application.StartupPath,
                                                         @"Configurations\" + ZoneDm.Zone.Number + "Config.xml"));
            zoneConfig.Open();
            LoadSettings(commonSettings);
        }

        #region Main

        private void LoadSettings(bool commonSettings)
        {
            config = !commonSettings
                          ? new Configuration(Path.Combine(Application.StartupPath,
                                                           @"Configurations\" + ZoneDm.Zone.Number + "Config.xml"))
                          : new Configuration(Path.Combine(Application.StartupPath, @"Configurations\CommonConfig.xml"));
            config.Open();
        }

        public void ReloadSettings(bool commonSettings)
        {
            SaveConfig();
            LoadSettings(commonSettings);
            ApplyConfig();
            configLoaded = true;
        }

        private bool configLoaded;
        protected override void OnLoad(EventArgs e)
        {
            if (configLoaded) return;
            ApplyConfig();
            configLoaded = true;
            base.OnLoad(e);
        }

        public event CursorPositionChangeEvent CursorPositionChanged;
        public event SelectedPageChangeEvent SelectedPageChanged;
        public event ZoneRenameEvent ZoneRenamed;
        public event ZoneNumberChangeEvent ZoneNumberChanged;

        private void ApplyConfig()
        {
            tcMain.SelectedIndex = zoneConfig.Read("tcMain.SelectedIndex", tcMain.SelectedIndex);
            TcMainSelectedIndexChanged(this, null);
            int index = zoneConfig.Read("lvMainList.SelectedIndex", -1);
            if (index != -1 && lvMainList.Items.Count > 0)
                try
                {
                    lvMainList.Items[index].Selected = true;
                    lvMainList.TopItem = lvMainList.Items[index];
                }
                catch
                {
                    lvMainList.Items[0].Selected = true;
                    lvMainList.TopItem = lvMainList.Items[0];
                }
            tcRoom.SelectedIndex = zoneConfig.Read("tcRoom.SelectedIndex", tcRoom.SelectedIndex);
            tcObject.SelectedIndex = zoneConfig.Read("tcObject.SelectedIndex", tcObject.SelectedIndex);
            tcMobs.SelectedIndex = zoneConfig.Read("tcMobs.SelectedIndex", tcMobs.SelectedIndex);
            tcTriggers.SelectedIndex = zoneConfig.Read("tcTriggers.SelectedIndex", tcTriggers.SelectedIndex);

            tsbShowRoomDetails.Checked = zoneConfig.Read("tsbShowRoomDetails.Checked", tsbShowRoomDetails.Checked);
            tsbShowRoomNumbers.Checked = zoneConfig.Read("tsbShowRoomNumbers.Checked", tsbShowRoomNumbers.Checked);
            tsbShowRoomMobs.Checked = zoneConfig.Read("tsbShowRoomMobs.Checked", tsbShowRoomMobs.Checked);
            tsbShowRoomObjects.Checked = zoneConfig.Read("tsbShowRoomObjects.Checked", tsbShowRoomObjects.Checked);
            tsbShowRoomTriggers.Checked = zoneConfig.Read("tsbShowRoomTriggers.Checked", tsbShowRoomTriggers.Checked);
            tsbSetOppositeExit.Checked = zoneConfig.Read("tsbSetOppositeExit.Checked", tsbSetOppositeExit.Checked);
            tsbAutolinkingX.Checked = zoneConfig.Read("tsbAutolinkingX.Checked", tsbAutolinkingX.Checked);
            wldMap.AutolinkingX = tsbAutolinkingX.Checked;
            tsbAutolinkingY.Checked = zoneConfig.Read("tsbAutolinkingY.Checked", tsbAutolinkingY.Checked);
            wldMap.AutolinkingY = tsbAutolinkingY.Checked;
            tsbAutolinkingZ.Checked = zoneConfig.Read("tsbAutolinkingZ.Checked", tsbAutolinkingZ.Checked);
            wldMap.AutolinkingZ = tsbAutolinkingZ.Checked;
            tsbBrush.Checked = zoneConfig.Read("tsbBrush.Checked", tsbBrush.Checked);

            wldMap.SketchCurrentColor = zoneConfig.Read("wldMap.SketchCurrentColor", wldMap.SketchCurrentColor);
            colorDlg.Color = wldMap.SketchCurrentColor;
            wldMap.CenterRoomX = zoneConfig.Read("wldMap.CenterRoomX", wldMap.CenterRoomX);
            wldMap.CenterRoomY = zoneConfig.Read("wldMap.CenterRoomY", wldMap.CenterRoomY);
            wldMap.CenterRoomZ = zoneConfig.Read("wldMap.CenterRoomZ", wldMap.CenterRoomZ);
            wldMap.MapScale = zoneConfig.Read("wldMap.MapScale", wldMap.MapScale);

            cbShowErrors.Checked = zoneConfig.Read("ShowErrors", cbShowErrors.Checked);
            cbShowWarnings.Checked = zoneConfig.Read("ShowWarnings", cbShowWarnings.Checked);
            cbShowInfo.Checked = zoneConfig.Read("ShowInfo", cbShowInfo.Checked);

            try
            {
                splitContainerBase.SplitterDistance = splitContainerBase.Height -
                                                      (int)
                                                      Math.Round(
                                                          config.Read(
                                                              "splitContainerBase",
                                                              (splitContainerBase.Height -
                                                               splitContainerBase.SplitterDistance) /
                                                              (double)splitContainerBase.Height) *
                                                          splitContainerBase.Height);
                splitContainerMap.SplitterDistance = splitContainerMap.Width -
                                                     (int)
                                                     Math.Round(
                                                         config.Read(
                                                             "splitContainerMap",
                                                             (splitContainerMap.Width -
                                                              splitContainerMap.SplitterDistance) /
                                                             (double)splitContainerMap.Width) * splitContainerMap.Width);
                splitContainerZon.SplitterDistance = splitContainerZon.Width -
                                                     (int)
                                                     Math.Round(
                                                         config.Read(
                                                             "splitContainerZon",
                                                             (splitContainerZon.Width -
                                                              splitContainerZon.SplitterDistance) /
                                                             (double)splitContainerZon.Width) * splitContainerZon.Width);
                //Комнаты
                splitContainerRooms.SplitterDistance = splitContainerRooms.Width -
                                                       (int)
                                                       Math.Round(
                                                           config.Read(
                                                               "splitContainerRooms",
                                                               (splitContainerRooms.Width -
                                                                splitContainerRooms.SplitterDistance) /
                                                               (double)splitContainerRooms.Width) *
                                                           splitContainerRooms.Width);
                splitContainerRoomsDesc.SplitterDistance = splitContainerRoomsDesc.Height -
                                                           (int)
                                                           Math.Round(
                                                               config.Read(
                                                                   "splitContainerRoomsDesc",
                                                                   (splitContainerRoomsDesc.Height -
                                                                    splitContainerRoomsDesc.SplitterDistance) /
                                                                   (double)splitContainerRoomsDesc.Height) *
                                                               splitContainerRoomsDesc.Height);
                splitContainerRoomObjects.SplitterDistance = splitContainerRoomObjects.Height -
                                                             (int)
                                                             Math.Round(
                                                                 config.Read(
                                                                     "splitContainerRoomObjects",
                                                                     (splitContainerRoomObjects.Height -
                                                                      splitContainerRoomObjects.SplitterDistance) /
                                                                     (double)splitContainerRoomObjects.Height) *
                                                                 splitContainerRoomObjects.Height);
                splitContainerRoomMobs.SplitterDistance = splitContainerRoomMobs.Height -
                                                          (int)
                                                          Math.Round(
                                                              config.Read(
                                                                  "splitContainerRoomMobs",
                                                                  (splitContainerRoomMobs.Height -
                                                                   splitContainerRoomMobs.SplitterDistance) /
                                                                  (double)splitContainerRoomMobs.Height) *
                                                              splitContainerRoomMobs.Height);
                //Объект
                splitContainerObj.SplitterDistance = splitContainerObj.Width -
                                                     (int)
                                                     Math.Round(
                                                         config.Read(
                                                             "splitContainerObj",
                                                             (splitContainerObj.Width -
                                                              splitContainerObj.SplitterDistance) /
                                                             (double)splitContainerObj.Width) * splitContainerObj.Width);

                tplObjEffects.SplitterDistance = tplObjEffects.Width -
                                                 (int)
                                                 Math.Round(
                                                     config.Read(
                                                         "tplObjEffects",
                                                         (tplObjEffects.Width - tplObjEffects.SplitterDistance) /
                                                         (double)tplObjEffects.Width) * tplObjEffects.Width);
                tplObjAffects.SplitterDistance = tplObjAffects.Width -
                                                 (int)
                                                 Math.Round(
                                                     config.Read(
                                                         "tplObjAffects",
                                                         (tplObjAffects.Width - tplObjAffects.SplitterDistance) /
                                                         (double)tplObjAffects.Width) * tplObjAffects.Width);
                tplObjWearTo.SplitterDistance = tplObjWearTo.Width -
                                                (int)
                                                Math.Round(
                                                    config.Read(
                                                        "tplObjWearTo",
                                                        (tplObjWearTo.Width - tplObjWearTo.SplitterDistance) /
                                                        (double)tplObjWearTo.Width) * tplObjWearTo.Width);
                tplObjCantTouch.SplitterDistance = tplObjCantTouch.Width -
                                                   (int)
                                                   Math.Round(
                                                       config.Read(
                                                           "tplObjCantTouch",
                                                           (tplObjCantTouch.Width - tplObjCantTouch.SplitterDistance) /
                                                           (double)tplObjCantTouch.Width) * tplObjCantTouch.Width);
                tplObjCantUse.SplitterDistance = tplObjCantUse.Width -
                                                 (int)
                                                 Math.Round(
                                                     config.Read(
                                                         "tplObjCantUse",
                                                         (tplObjCantUse.Width - tplObjCantUse.SplitterDistance) /
                                                         (double)tplObjCantUse.Width) * tplObjCantUse.Width);
                splitContainerAddAff.SplitterDistance = splitContainerAddAff.Width -
                                                        (int)
                                                        Math.Round(
                                                            config.Read(
                                                                "splitContainerAddAff",
                                                                (splitContainerAddAff.Width -
                                                                 splitContainerAddAff.SplitterDistance) /
                                                                (double)splitContainerAddAff.Width) *
                                                            splitContainerAddAff.Width);
                splitContainerSkillBonus.SplitterDistance = splitContainerSkillBonus.Width -
                                                            (int)
                                                            Math.Round(
                                                                config.Read(
                                                                    "splitContainerSkillBonus",
                                                                    (splitContainerSkillBonus.Width -
                                                                     splitContainerSkillBonus.SplitterDistance) /
                                                                    (double)splitContainerSkillBonus.Width) *
                                                                splitContainerSkillBonus.Width);
                //Моб
                splitContainerMob.SplitterDistance = splitContainerMob.Width -
                                                     (int)
                                                     Math.Round(
                                                         config.Read(
                                                             "splitContainerMob",
                                                             (splitContainerMob.Width -
                                                              splitContainerMob.SplitterDistance) /
                                                             (double)splitContainerMob.Width) * splitContainerMob.Width);
                tplMobFeats.SplitterDistance = tplMobFeats.Width -
                                               (int)
                                               Math.Round(
                                                   config.Read(
                                                       "tplMobFeats",
                                                       (tplMobFeats.Width - tplMobFeats.SplitterDistance) /
                                                       (double)tplMobFeats.Width) * tplMobFeats.Width);
                tplMobAffects.SplitterDistance = tplMobAffects.Width -
                                                 (int)
                                                 Math.Round(
                                                     config.Read(
                                                         "tplMobAffects",
                                                         (tplMobAffects.Width - tplMobAffects.SplitterDistance) /
                                                         (double)tplMobAffects.Width) * tplMobAffects.Width);
                tplMobFlags.SplitterDistance = tplMobFlags.Width -
                                               (int)
                                               Math.Round(
                                                   config.Read(
                                                       "tplMobFlags",
                                                       (tplMobFlags.Width - tplMobFlags.SplitterDistance) /
                                                       (double)tplMobFlags.Width) * tplMobFlags.Width);
                tplMobSpecFlags.SplitterDistance = tplMobSpecFlags.Width -
                                                   (int)
                                                   Math.Round(
                                                       config.Read(
                                                           "tplMobSpecFlags",
                                                           (tplMobSpecFlags.Width - tplMobSpecFlags.SplitterDistance) /
                                                           (double)tplMobSpecFlags.Width) * tplMobSpecFlags.Width);
                //Триггуры
                splitContainerTrg.SplitterDistance = splitContainerTrg.Width -
                                                     (int)
                                                     Math.Round(
                                                         config.Read(
                                                             "splitContainerTrg",
                                                             (splitContainerTrg.Width -
                                                              splitContainerTrg.SplitterDistance) /
                                                             (double)splitContainerTrg.Width) * splitContainerTrg.Width);
                tplMobRoles.SplitterDistance = tplMobRoles.Width -
                                               (int)
                                               Math.Round(
                                                   config.Read(
                                                       "tplMobRoles",
                                                       (tplMobRoles.Width - tplMobRoles.SplitterDistance) /
                                                       (double)tplMobRoles.Width) * tplMobRoles.Width);

            }
            // ReSharper disable EmptyGeneralCatchClause
            catch
            // ReSharper restore EmptyGeneralCatchClause
            { }
        }

        private void WldForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveConfig();
        }

        public void SaveConfig()
        {
            zoneConfig.Write("tcMain.SelectedIndex", tcMain.SelectedIndex);
            if (lvMainList.SelectedIndices.Count > 0)
                zoneConfig.Write("lvMainList.SelectedIndex", lvMainList.SelectedIndices[0]);
            else
                zoneConfig.Write("lvMainList.SelectedIndex", -1);
            zoneConfig.Write("tcRoom.SelectedIndex", tcRoom.SelectedIndex);
            zoneConfig.Write("tcObject.SelectedIndex", tcObject.SelectedIndex);
            zoneConfig.Write("tcMobs.SelectedIndex", tcMobs.SelectedIndex);
            zoneConfig.Write("tcTriggers.SelectedIndex", tcTriggers.SelectedIndex);

            zoneConfig.Write("tsbShowRoomDetails.Checked", tsbShowRoomDetails.Checked);
            zoneConfig.Write("tsbShowRoomNumbers.Checked", tsbShowRoomNumbers.Checked);
            zoneConfig.Write("tsbShowRoomMobs.Checked", tsbShowRoomMobs.Checked);
            zoneConfig.Write("tsbShowRoomObjects.Checked", tsbShowRoomObjects.Checked);
            zoneConfig.Write("tsbShowRoomTriggers.Checked", tsbShowRoomTriggers.Checked);
            zoneConfig.Write("tsbSetOppositeExit.Checked", tsbSetOppositeExit.Checked);
            zoneConfig.Write("tsbBrush.Checked", tsbBrush.Checked);

            zoneConfig.Write("wldMap.SketchCurrentColor", wldMap.SketchCurrentColor);
            zoneConfig.Write("wldMap.CenterRoomX", wldMap.CenterRoomX);
            zoneConfig.Write("wldMap.CenterRoomY", wldMap.CenterRoomY);
            zoneConfig.Write("wldMap.CenterRoomZ", wldMap.CenterRoomZ);
            zoneConfig.Write("wldMap.MapScale", wldMap.MapScale);

            zoneConfig.Write("tsbAutolinkingX.Checked", tsbAutolinkingX.Checked);
            zoneConfig.Write("tsbAutolinkingY.Checked", tsbAutolinkingY.Checked);
            zoneConfig.Write("tsbAutolinkingZ.Checked", tsbAutolinkingZ.Checked);

            zoneConfig.Write("ShowErrors", cbShowErrors.Checked);
            zoneConfig.Write("ShowWarnings", cbShowWarnings.Checked);
            zoneConfig.Write("ShowInfo", cbShowInfo.Checked);

            config.Write("splitContainerBase", (splitContainerBase.Height - splitContainerBase.SplitterDistance) / (double)splitContainerBase.Height);
            config.Write("splitContainerMap", (splitContainerMap.Width - splitContainerMap.SplitterDistance) / (double)splitContainerMap.Width);
            config.Write("splitContainerZon", (splitContainerZon.Width - splitContainerZon.SplitterDistance) / (double)splitContainerZon.Width);
            config.Write("splitContainerRooms", (splitContainerRooms.Width - splitContainerRooms.SplitterDistance) / (double)splitContainerRooms.Width);
            config.Write("splitContainerRoomsDesc", (splitContainerRoomsDesc.Height - splitContainerRoomsDesc.SplitterDistance) / (double)splitContainerRoomsDesc.Height);
            config.Write("splitContainerRoomObjects", (splitContainerRoomObjects.Height - splitContainerRoomObjects.SplitterDistance) / (double)splitContainerRoomObjects.Height);
            config.Write("splitContainerRoomMobs", (splitContainerRoomMobs.Height - splitContainerRoomMobs.SplitterDistance) / (double)splitContainerRoomMobs.Height);
            config.Write("splitContainerObj", (splitContainerObj.Width - splitContainerObj.SplitterDistance) / (double)splitContainerObj.Width);


            config.Write("tplObjEffects", (tplObjEffects.Width - tplObjEffects.SplitterDistance) / (double)tplObjEffects.Width);
            config.Write("tplObjAffects", (tplObjAffects.Width - tplObjAffects.SplitterDistance) / (double)tplObjAffects.Width);
            config.Write("tplObjWearTo", (tplObjWearTo.Width - tplObjWearTo.SplitterDistance) / (double)tplObjWearTo.Width);
            config.Write("tplObjCantTouch", (tplObjCantTouch.Width - tplObjCantTouch.SplitterDistance) / (double)tplObjCantTouch.Width);
            config.Write("tplObjCantUse", (tplObjCantUse.Width - tplObjCantUse.SplitterDistance) / (double)tplObjCantUse.Width);
            config.Write("splitContainerAddAff", (splitContainerAddAff.Width - splitContainerAddAff.SplitterDistance) / (double)splitContainerAddAff.Width);
            config.Write("splitContainerSkillBonus", (splitContainerSkillBonus.Width - splitContainerSkillBonus.SplitterDistance) / (double)splitContainerSkillBonus.Width);

            config.Write("splitContainerMob", (splitContainerMob.Width - splitContainerMob.SplitterDistance) / (double)splitContainerMob.Width);
            config.Write("tplMobFeats", (tplMobFeats.Width - tplMobFeats.SplitterDistance) / (double)tplMobFeats.Width);
            config.Write("tplMobAffects", (tplMobAffects.Width - tplMobAffects.SplitterDistance) / (double)tplMobAffects.Width);
            config.Write("tplMobFlags", (tplMobFlags.Width - tplMobFlags.SplitterDistance) / (double)tplMobFlags.Width);
            config.Write("tplMobSpecFlags", (tplMobSpecFlags.Width - tplMobSpecFlags.SplitterDistance) / (double)tplMobSpecFlags.Width);
            config.Write("tplMobRoles", (tplMobRoles.Width - tplMobRoles.SplitterDistance) / (double)tplMobRoles.Width);

            config.Write("splitContainerTrg", (splitContainerTrg.Width - splitContainerTrg.SplitterDistance) / (double)splitContainerTrg.Width);

            config.Save();
        }

        private void ZoneChangedHandler(string zoneNname, object changedClass, object sender)
        {
            /*if (!tsbSaveZone.Enabled)
                tsbSaveZone.Enabled = true;*/
            string className = changedClass.GetType().Name;
            TabPage changedTab = new TabPage();
            switch (className)
            {
                case "Zone":
                    tpZone.ImageIndex = 47;
                    changedTab = tpZone;
                    break;
                case "SketchRooms":
                case "Rooms":
                    tpRooms.ImageIndex = 47;
                    changedTab = tpRooms;
                    break;
                case "ObjsCollection":
                    tpObjects.ImageIndex = 47;
                    changedTab = tpObjects;
                    break;
                case "Mobs":
                    tpMobs.ImageIndex = 47;
                    changedTab = tpMobs;
                    break;
                case "Triggers":
                    tpTriggers.ImageIndex = 47;
                    changedTab = tpTriggers;
                    break;
                default:
                    Debug.WriteLine("Unhandled changes in: " + changedClass.GetType().Name);
                    break;
            }
            if (tcMain.SelectedTab == changedTab)
                ColorizeMainListViewItem(sender as BaseDataObject);
        }

        private void ColorizeMainListViewItem(BaseDataObject modifiedObject)
        {
            if (modifiedObject == null) return;//Бывает при добавлении нового объекта в список
            foreach (ListViewItem lvi in lvMainList.Items)
            {
                if (lvi.Tag.ToString() == modifiedObject.VNum.ToString())
                {
                    //lvi.BackColor = Color.FromArgb(255, 234, 234);
                    lvi.ImageIndex = 47;
                    break;
                }
            }
        }

        /// <summary>
        /// Обработка хотекеев
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {

                case Keys.Control | Keys.T:
                    if (lvMainList.Focused)
                    {
                        AddTemplate();
                        return true;
                    }
                    break;
                case Keys.Control | Keys.C:
                    if (lvMainList.Focused)
                    {
                        CreateClones();
                        return true;
                    }
                    break;
                case Keys.Control | Keys.N://Создается новый объект
                    CreateNewEntity();
                    return true;
                case Keys.Control | Keys.S:
                    SaveCurrentTabData();
                    return true;
                case Keys.Control | Keys.Shift | Keys.S:
                    SaveAllZoneData();
                    return true;
            }
            return false;
        }

        private void BindUi()
        {
            ComboBox cboxBooleanVal = new ComboBox();
            cboxBooleanVal.Items.Add("Нет");
            cboxBooleanVal.Items.Add("Да");

            #region Zon

            nudZoneNumber.Value = ZoneDm.Zone.Number;
            tbZoneName.Text = ZoneDm.Zone.Name;
            tbZoneComment.Text = ZoneDm.Zone.Comment;
            tbZoneLocation.Text = ZoneDm.Zone.Location;
            tbZoneDescription.Text = ZoneDm.Zone.Description;
            tbZoneAuthor.Text = ZoneDm.Zone.Author;
            nudOptimalCharsInGroup.Value = ZoneDm.Zone.OptimalCharsInGroup;
            nudRepopTimer.Value = ZoneDm.Zone.RepopTimer;
            BindComboBox(cbZoneReopopType, BasesDm.ZonRepop);
            BindComboBox(cboxZonType, BasesDm.ZonType);
            cbZoneReopopType.SelectedIndex = ZoneDm.Zone.RepopType;
            cboxZonType.SelectedIndex = ZoneDm.Zone.Type;
            //Закладка ZON-файл
            nudZoneLevel.Value = ZoneDm.Zone.Level;

            elvVitrualRoomMobObjects.MySortBrush = SystemBrushes.ControlLight;
            NumericUpDown nudObjLoadInMobProb = new NumericUpDown { Minimum = (-1) };
            elvVitrualRoomMobObjects.Columns.Add(new ExEditableColumnHeader("Вероятн.", nudObjLoadInMobProb, 65));
            elvVitrualRoomMobObjects.Columns.Add(new ExColumnHeader("VNum", 65));
            elvVitrualRoomMobObjects.Columns.Add(new ExColumnHeader("Название", 250));
            cboxMobObjState = new ComboBox();
            /*TaggedComboBoxItem tcbi = new TaggedComboBoxItem { Text = "Предмет в инвентаре у моба" };
            cboxMobObjState.Items.Add(tcbi);*/
            BindComboBox(cboxMobObjState, BasesDm.ZonEquipped, false);
            elvVitrualRoomMobObjects.Columns.Add(new ExEditableColumnHeader("Положение", cboxMobObjState, 250));

            elvVitrualRoomMobObjectsAfterDeath.MySortBrush = SystemBrushes.ControlLight;
            elvVitrualRoomMobObjectsAfterDeath.Columns.Add(new ExEditableColumnHeader("Вероятн.", nudObjLoadInMobProb, 65));
            elvVitrualRoomMobObjectsAfterDeath.Columns.Add(new ExColumnHeader("VNum", 65));
            elvVitrualRoomMobObjectsAfterDeath.Columns.Add(new ExColumnHeader("Название", 250));
            cboxObjectLoadType = new ComboBox();
            BindComboBox(cboxObjectLoadType, BasesDm.ZonObjLoadType, false);
            elvVitrualRoomMobObjectsAfterDeath.Columns.Add(new ExEditableColumnHeader("Тип загрузки", cboxObjectLoadType, 300));
            cboxObjectLoadSpecParam = new ComboBox();
            BindComboBox(cboxObjectLoadSpecParam, BasesDm.ZonObjLoadSpecParam, false);
            elvVitrualRoomMobObjectsAfterDeath.Columns.Add(new ExEditableColumnHeader("Спец. параметр", cboxObjectLoadSpecParam, 300));

            #endregion

            #region Комнаты

            BindComboBox(cboxSectorType, BasesDm.SectorType);

            //Раскраска флагов комнат (вынести в файл)
            foreach (DataRow dr in BasesDm.RoomVector.Rows)
            {
                Color fc = (!dr["forecolor"].ToString().ToLower().Contains("def")) ? Color.FromName(dr["forecolor"].ToString()) : SystemColors.WindowText;

                Color bc = (!dr["backcolor"].ToString().ToLower().Contains("def")) ? Color.FromName(dr["backcolor"].ToString()) : SystemColors.Window;
                descColl.Add(
                    new ListItemDesc(dr["val"].ToString(), fc, bc, int.Parse(dr["order"].ToString())));
            }

            tplRoomFlags.Descriptors = descColl;
            wldMap.ColorDescriptors = descColl;

            elvObjectsInRoom.MySortBrush = SystemBrushes.ControlLight;
            elvObjectsInRoom.Columns.Add(new ExEditableColumnHeader("Вероятн.", new NumericUpDown(), 65));
            elvObjectsInRoom.Columns.Add(new ExColumnHeader("VNum", 65));
            elvObjectsInRoom.Columns.Add(new ExColumnHeader("Название", 300));
            elvObjectsInRoom.Columns.Add(new ExEditableColumnHeader("Тип загрузки", cboxObjectLoadType, 300));

            elvRoomObjInObj.MySortBrush = SystemBrushes.ControlLight;
            elvRoomObjInObj.Columns.Add(new ExEditableColumnHeader("Вероятн.", new NumericUpDown(), 65));
            elvRoomObjInObj.Columns.Add(new ExColumnHeader("VNum", 65));
            elvRoomObjInObj.Columns.Add(new ExColumnHeader("Название", 300));
            elvRoomObjInObj.Columns.Add(new ExEditableColumnHeader("Тип загрузки", cboxObjectLoadType, 300));

            elvRoomMobObjects.MySortBrush = SystemBrushes.ControlLight;
            //Определено для zon NumericUpDown nudObjLoadInMobProb = new NumericUpDown { Minimum = (-1) };
            elvRoomMobObjects.Columns.Add(new ExEditableColumnHeader("Вероятн.", nudObjLoadInMobProb, 65));
            elvRoomMobObjects.Columns.Add(new ExColumnHeader("VNum", 65));
            elvRoomMobObjects.Columns.Add(new ExColumnHeader("Название", 200));
            elvRoomMobObjects.Columns.Add(new ExEditableColumnHeader("Положение", cboxMobObjState, 100));

            elvRoomMobObjectsLoadingAfterDeath.MySortBrush = SystemBrushes.ControlLight;
            elvRoomMobObjectsLoadingAfterDeath.Columns.Add(new ExEditableColumnHeader("Вероятн.", nudObjLoadInMobProb, 65));
            elvRoomMobObjectsLoadingAfterDeath.Columns.Add(new ExColumnHeader("VNum", 65));
            elvRoomMobObjectsLoadingAfterDeath.Columns.Add(new ExColumnHeader("Название", 250));
            elvRoomMobObjectsLoadingAfterDeath.Columns.Add(new ExEditableColumnHeader("Тип загрузки", cboxObjectLoadType, 300));
            elvRoomMobObjectsLoadingAfterDeath.Columns.Add(new ExEditableColumnHeader("Спец. параметр", cboxObjectLoadSpecParam, 300));

            elvRoomIngredients.MySortBrush = SystemBrushes.ControlLight;
            elvRoomIngredients.Columns.Add(new ExEditableColumnHeader("Вероятн.", nudObjLoadInMobProb, 65));
            elvRoomIngredients.Columns.Add(new ExEditableColumnHeader("Сила", nudObjLoadInMobProb, 65));
            elvRoomIngredients.Columns.Add(new ExEditableColumnHeader("Сила авто", cboxBooleanVal, 65));
            elvRoomIngredients.Columns.Add(new ExColumnHeader("Название", 300));

            #endregion

            #region Объекты

            BindComboBox(cboxObjectGender, BasesDm.Sex);
            BindComboBox(cboxObjSkill, BasesDm.Skills);
            BindComboBox(cboxObjMatherial, BasesDm.Material);
            BindComboBox(cboxObjMaxStructHits, BasesDm.ObjectsStructurehits);

            //Панели по типам объектов
            BindComboBox(cboxObjFontanDrinkType, BasesDm.DrinkTypes);
            BindComboBox(cboxObjLiquidContainerDrinkType, BasesDm.DrinkTypes);
            BindComboBox(cboxObjMagBookSpell, BasesDm.Spells);
            BindComboBox(cboxObjMagScrollSpell1, BasesDm.Spells);
            BindComboBox(cboxObjMagScrollSpell2, BasesDm.Spells);
            BindComboBox(cboxObjMagScrollSpell3, BasesDm.Spells);
            BindComboBox(cboxObjMagStaffSpell, BasesDm.Spells);
            BindComboBox(cboxObjMagWandSpell, BasesDm.Spells);
            BindComboBox(cboxObjPotionSpell1, BasesDm.Spells);
            BindComboBox(cboxObjPotionSpell2, BasesDm.Spells);
            BindComboBox(cboxObjPotionSpell3, BasesDm.Spells);
            BindComboBox(cboxObjWeaponSrikeType, BasesDm.Weapons);
            BindListView(lvObjContainerFlags, BasesDm.Container);
            BindListView(lvObjMagIngrFlags, BasesDm.MagicFlags);
            BindComboBox(cboxMoneyCurrency, BasesDm.MoneyCurrency);

            BindComboBox(cboxObjType, BasesDm.Type);
            /*cboxObjType.Items.Add(new ImageComboItem("Источник света", 41));//0
            cboxObjType.Items.Add(new ImageComboItem("Магический свиток", 38));//1
            cboxObjType.Items.Add(new ImageComboItem("Волшебная палочка", 2));//2
            cboxObjType.Items.Add(new ImageComboItem("Магичекий посох", 39));//3
            cboxObjType.Items.Add(new ImageComboItem("Оружие", 17));//4
            cboxObjType.Items.Add(new ImageComboItem("FIREWEAPON (недоработано)", 36));//5
            cboxObjType.Items.Add(new ImageComboItem("MISSILE  (недоработано)", -1));//6
            cboxObjType.Items.Add(new ImageComboItem("Сокровища кроме монет (драгоценные камни и т.д.)", 3));//7
            cboxObjType.Items.Add(new ImageComboItem("Броня (доспехи, шлем и т.п.)", 34));//8
            cboxObjType.Items.Add(new ImageComboItem("Магический напиток", 0));//9
            cboxObjType.Items.Add(new ImageComboItem("Просто одежда (не дает +AC)", 40));//10
            cboxObjType.Items.Add(new ImageComboItem("Обычные (misc) предметы без всяких особенностей", 28));//11
            cboxObjType.Items.Add(new ImageComboItem("Мусор (уничтожается дворниками, не продается)", 35));//12
            cboxObjType.Items.Add(new ImageComboItem("TRAP (недоработано)", -1));//13
            cboxObjType.Items.Add(new ImageComboItem("Контейнер", 29));//14
            cboxObjType.Items.Add(new ImageComboItem("Заметка (на нем можно писать)", 37));//15
            cboxObjType.Items.Add(new ImageComboItem("Контейнер для жидкостей", 1));//16
            cboxObjType.Items.Add(new ImageComboItem("Ключ", 43));//17
            cboxObjType.Items.Add(new ImageComboItem("Пища", 22));//18
            cboxObjType.Items.Add(new ImageComboItem("Валюта", 19));//19
            cboxObjType.Items.Add(new ImageComboItem("Ручка", 24));//20
            cboxObjType.Items.Add(new ImageComboItem("Лодка; позволяет ходить по SECT_WATER_NOSWIM", -1));//21
            cboxObjType.Items.Add(new ImageComboItem("Фонтан", 44));//22
            cboxObjType.Items.Add(new ImageComboItem("Магическая книга", 9));//23
            cboxObjType.Items.Add(new ImageComboItem("Магический ингредиент", -1));//24
            cboxObjType.Items.Add(new ImageComboItem("Ингредиент для отвара", 7));//25
            cboxObjType.Items.Add(new ImageComboItem("Бинт", 49));//26*/        

            #endregion

            #region Мобы

            BindComboBox(cboxMobSex, BasesDm.Sex);
            BindComboBox(cboxMobAlign, BasesDm.Align);
            BindComboBox(cboxMobAttackType, BasesDm.Weapons);
            BindComboBox(cboxMobStartPosition, BasesDm.Position);
            BindComboBox(cboxMobDefPosition, BasesDm.Position);
            BindComboBox(cboxMobClass, BasesDm.MobClass);
            BindComboBox(cboxMobRace, BasesDm.MobType);

            elvMobIngredients.MySortBrush = SystemBrushes.ControlLight;
            elvMobIngredients.Columns.Add(new ExEditableColumnHeader("Вероятн.", nudObjLoadInMobProb, 65));
            elvMobIngredients.Columns.Add(new ExEditableColumnHeader("Сила", nudObjLoadInMobProb, 65));
            elvMobIngredients.Columns.Add(new ExEditableColumnHeader("Сила авто", cboxBooleanVal, 65));
            elvMobIngredients.Columns.Add(new ExColumnHeader("Название", 300));

            #endregion

            #region Триггеры

            BindComboBox(cboxTrgClass, BasesDm.TriggerType);

            #endregion
        }

        private void TsbReloadWithoutSaveClick(object sender, EventArgs e)
        {
            if (
                MessageBox.Show(
                    "Сейчас данная зона будет перезагружена без сохранения изменений.\nВ результате перезагрузки все несохраненные изменения будут отменены!\nВы подтверждаете перезагрузку зоны?",
                    "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            if (!ZoneDm.LoadData())
                return;
            wldMap.SetRoomsCollection(ref ZoneDm.Rooms, ref ZoneDm.SketchRooms, ZoneDm.Zone.Number);
            wldMap.RecreateAllRoomsBitmaps();
            wldMap.RedrawBitmap();
            RefreshMainList();
            RefreshZoneInfo();
            //tsbSaveZone.Enabled = false;
            if (lvMainList.Items.Count > 0)
                lvMainList.Items[0].Selected = true;
        }

        private void TsbSaveZoneClick(object sender, EventArgs e)
        {
            wldMap.Focus();//Трик для предварительного сохранения редактируемого поля
            SaveAllZoneData();
            RefreshMainList();
        }

        private void TsbSaveCurTabDataClick(object sender, EventArgs e)
        {
            wldMap.Focus();//Трик для предварительного сохранения редактируемого поля
            SaveCurrentTabData();
            RefreshMainList();
        }

        #region Навигация

        private void NavigatedControl_MouseUp(object sender, MouseEventArgs e)
        {
            //tsmiGoTo.Enabled = false;
            cmsGridMenu.Tag = ((Control)sender).Name;
        }

        private void TsmiAddRowClick(object sender, EventArgs e)
        {
            switch (cmsGridMenu.Tag.ToString())
            {
                case "lvRoomTriggers":
                    BtnAddRoomTriggerClick(sender, e);
                    break;
                case "elvObjectsInRoom":
                    BtnRoomAddObjClick(sender, e);
                    break;
                case "elvRoomObjInObj":
                    BtnRoomAddObjInObjClick(sender, e);
                    break;
                case "lvMobsInRoom":
                    BtnRoomAddMobClick(sender, e);
                    break;
                case "elvRoomMobObjects":
                    BtnRoomAddObjToMobClick(sender, e);
                    break;
                case "elvRoomMobObjectsLoadingAfterDeath":
                    BtnRoomAddObjToMobAfterDeathClick(sender, e);
                    break;
                case "lvObjectsToRemove":
                    BtnAddRoomObjectToRemoveClick(sender, e);                    
                    break;
                case "lvObjTriggers":
                    BtnAddObjTriggerClick(sender, e);
                    break;
                case "lvMobHelpers":
                    btnMobAddHelper_Click(sender, e);
                    break;
                case "lvMobTriggers":
                    btnAddMobTrigger_Click(sender, e);
                    break;
                case "lvMobsInVitrualRoom":
                    bdtAddMobInVirtualRoom_Click(sender, e);
                    break;
                case "elvVitrualRoomMobObjects":
                    btnAddItemToMobInVirtualRoomClick(sender, e);
                    break;
                case "elvVitrualRoomMobObjectsAfterDeath":                    
                    btnAddItemToMobInVirtualRoomAfterDeathClick(sender, e);
                    break;
            }
        }

        private void TsmiRemoveRowClick(object sender, EventArgs e)
        {
            switch (cmsGridMenu.Tag.ToString())
            {
                case "lvRoomTriggers":
                    BtnRemoveRoomTriggerClick(sender, e);
                    break;
                case "elvObjectsInRoom":
                    BtnRoomRemoveObjClick(sender, e);
                    break;
                case "elvRoomObjInObj":
                    BtnRoomRemoveObjFromObjClick(sender, e);
                    break;
                case "lvMobsInRoom":
                    BtnRoomRemoveMobClick(sender, e);
                    break;
                case "elvRoomMobObjects":
                    BtnRoomRomoveObjFromMobClick(sender, e);
                    break;
                case "elvRoomMobObjectsLoadingAfterDeath":
                    BtnRemoveRoomObjFromMobAfterDeathClick(sender, e);
                    break;
                case "lvObjectsToRemove":
                    BtnRemoveRoomObjectToRemoveClick(sender, e);
                    break;
                case "lvObjTriggers":
                    BtnObjRemoveTriggerClick(sender, e);
                    break;
                case "lvMobHelpers":
                    btnRemoveHelpersList_Click(sender, e);
                    break;
                case "lvMobTriggers":
                    btnMobRemoveTrigger_Click(sender, e);
                    break;
                case "lvMobsInVitrualRoom":
                    btnRemoveMobFromVitrualRoom_Click(sender, e);
                    break;
                case "elvVitrualRoomMobObjects":
                    btnRemoveItemFromMobInVirtualRoomClick(sender, e);
                    break;
                case "elvVitrualRoomMobObjectsAfterDeath":
                    btnRemoveItemFromMobInVirtualRoomAfterDeathClick(sender, e);
                    break;
            }
        }

        private void TsmiGoToClick(object sender, EventArgs e)
        {
            switch (cmsGridMenu.Tag.ToString())
            {
                case "lvRoomTriggers":
                    if (lvRoomTriggers.SelectedItems.Count > 0)
                    {
                        tcMain.SelectedTab = tpTriggers;
                        RefreshTriggersList();
                        SelectMainListItem(lvRoomTriggers.SelectedItems[0].Tag, false);
                        string[] s = new[] { ActiveRoom.VNum.ToString(), "tpRooms" };
                        AddHistory(s);
                    }
                    break;
                case "elvObjectsInRoom":
                    if (elvObjectsInRoom.SelectedItems.Count > 0)
                    {
                        tcMain.SelectedTab = tpObjects;
                        RefreshObjectsList();
                        SelectMainListItem(elvObjectsInRoom.SelectedItems[0].SubItems[1].Text, false);
                        string[] s = new[] { ActiveRoom.VNum.ToString(), "tpRooms" };
                        AddHistory(s);
                    }
                    break;
                case "elvRoomObjInObj":
                    if (elvRoomObjInObj.SelectedItems.Count > 0)
                    {
                        tcMain.SelectedTab = tpObjects;
                        RefreshObjectsList();
                        SelectMainListItem(elvRoomObjInObj.SelectedItems[0].SubItems[1].Text, false);
                        string[] s = new[] { ActiveRoom.VNum.ToString(), "tpRooms" };
                        AddHistory(s);
                    }
                    break;
                case "lvMobsInRoom":
                    if (lvMobsInRoom.SelectedItems.Count > 0)
                    {
                        tcMain.SelectedTab = tpMobs;
                        RefreshMobsList();
                        SelectMainListItem(lvMobsInRoom.SelectedItems[0].Tag, false);
                        string[] s = new[] { ActiveRoom.VNum.ToString(), "tpRooms" };
                        AddHistory(s);
                    }
                    break;
                case "elvRoomMobObjects":
                    if (elvRoomMobObjects.SelectedItems.Count > 0)
                    {
                        tcMain.SelectedTab = tpObjects;
                        RefreshObjectsList();
                        SelectMainListItem(elvRoomMobObjects.SelectedItems[0].SubItems[1].Text, false);
                        string[] s = new[] { ActiveRoom.VNum.ToString(), "tpRooms" };
                        AddHistory(s);
                    }
                    break;
                case "elvRoomMobObjectsLoadingAfterDeath":
                    if (elvRoomMobObjectsLoadingAfterDeath.SelectedItems.Count > 0)
                    {
                        tcMain.SelectedTab = tpObjects;
                        RefreshObjectsList();
                        SelectMainListItem(elvRoomMobObjectsLoadingAfterDeath.SelectedItems[0].SubItems[1].Text, false);
                        string[] s = new[] { ActiveRoom.VNum.ToString(), "tpRooms" };
                        AddHistory(s);
                    }
                    break;
                case "lvObjectsToRemove":
                    if (lvObjectsToRemove.SelectedItems.Count > 0)
                    {
                        tcMain.SelectedTab = tpObjects;
                        RefreshObjectsList();
                        SelectMainListItem(lvObjectsToRemove.SelectedItems[0].Tag, false);
                        string[] s = new[] { ActiveRoom.VNum.ToString(), "tpRooms" };
                        AddHistory(s);
                    }
                    break;
                case "lvObjTriggers":
                    if (lvObjTriggers.SelectedItems.Count > 0)
                    {
                        tcMain.SelectedTab = tpTriggers;
                        RefreshTriggersList();
                        SelectMainListItem(lvObjTriggers.SelectedItems[0].Tag, false);
                        string[] s = new[] { lvMainList.SelectedItems[0].Tag.ToString(), "tpObjects" };
                        AddHistory(s);
                    }
                    break;
                case "lvMobHelpers":
                    if (lvMobHelpers.SelectedItems.Count > 0)
                    {
                        tcMain.SelectedTab = tpMobs;
                        RefreshMobsList();
                        SelectMainListItem(lvMobHelpers.SelectedItems[0].Tag, false);
                        string[] s = new[] { lvMainList.SelectedItems[0].Tag.ToString(), "tpMobs" };
                        AddHistory(s);
                    }
                    break;
                case "lvMobTriggers":
                    if (lvMobTriggers.SelectedItems.Count > 0)
                    {
                        tcMain.SelectedTab = tpTriggers;
                        RefreshTriggersList();
                        SelectMainListItem(lvMobTriggers.SelectedItems[0].Tag, false);
                        string[] s = new[] { lvMainList.SelectedItems[0].Tag.ToString(), "tpMobs" };
                        AddHistory(s);
                    }
                    break;
                case "lvMobsInVitrualRoom":
                    if (lvMobsInVitrualRoom.SelectedItems.Count > 0)
                    {
                        tcMain.SelectedTab = tpMobs;
                        RefreshMobsList();
                        SelectMainListItem(lvMobsInVitrualRoom.SelectedItems[0].Tag, false);
                        string[] s = new[] { "-1", "tpZone" };
                        AddHistory(s);
                    }
                    break;
                case "elvVitrualRoomMobObjects":
                    if (elvVitrualRoomMobObjects.SelectedItems.Count > 0)
                    {
                        tcMain.SelectedTab = tpObjects;
                        RefreshObjectsList();
                        SelectMainListItem(elvVitrualRoomMobObjects.SelectedItems[0].SubItems[1].Text, false);
                        string[] s = new[] { "-1", "tpZone" };
                        AddHistory(s);
                    }
                    break;
                case "elvVitrualRoomMobObjectsAfterDeath":
                    if (elvVitrualRoomMobObjectsAfterDeath.SelectedItems.Count > 0)
                    {
                        tcMain.SelectedTab = tpObjects;
                        RefreshObjectsList();
                        SelectMainListItem(elvVitrualRoomMobObjectsAfterDeath.SelectedItems[0].SubItems[1].Text, false);
                        string[] s = new[] { "-1", "tpZone" };
                        AddHistory(s);
                    }
                    break;
            }
        }

        private void SelectMainListItem(object vNum, bool isRoom)
        {
            foreach (ListViewItem lvi in lvMainList.Items)
            {
                if (lvi.Tag.ToString() == vNum.ToString())
                {
                    lvi.Selected = true;
                    SetMainListTopItem(lvi);
                    if (isRoom)
                    {
                        SelectedRoomsCollection csrc = new SelectedRoomsCollection { Convert.ToInt32(vNum) };
                        wldMap.SelectedRooms = csrc;
                        RefreshRoomData();
                    }
                    return;
                }
            }
        }

        public void SetMainListTopItem(ListViewItem lvi)
        {
            lvMainList.TopItem = lvi.Index > 1 ? lvMainList.Items[lvi.Index - 1] : lvi;
        }

        private void AddHistory(string[] data)
        {
            history.Add(data);
            historyPosition = history.Count - 1;
            RefreshHistory();
        }

        private void RefreshHistory()
        {
            tsbHistoryBack.Enabled = (historyPosition > -1);
            tsbHistoryForward.Enabled = (historyPosition < history.Count - 1 && history.Count > 0);
        }

        private void SetHistoryPos(int pos)
        {
            if (pos < 0 && pos > history.Count - 1) return;
            switch (((string[])(history[pos]))[1])
            {
                case "tpTriggers":
                    tcMain.SelectedTab = tpTriggers;
                    RefreshTriggersList();
                    SelectMainListItem(((string[])(history[pos]))[0], false);
                    break;
                case "tpObjects":
                    tcMain.SelectedTab = tpObjects;
                    RefreshObjectsList();
                    SelectMainListItem(((string[])(history[pos]))[0], false);
                    break;
                case "tpMobs":
                    tcMain.SelectedTab = tpMobs;
                    RefreshMobsList();
                    SelectMainListItem(((string[])(history[pos]))[0], false);
                    break;
                case "tpRooms":
                    tcMain.SelectedTab = tpRooms;
                    RefreshRoomsList();
                    SelectMainListItem(((string[])(history[pos]))[0], true);
                    break;
            }
            RefreshHistory();
        }

        private void TsbHistoryBackClick(object sender, EventArgs e)
        {
            SetHistoryPos(historyPosition--);
        }

        private void TsbHistoryForwardClick(object sender, EventArgs e)
        {
            SetHistoryPos(historyPosition++);
        }

        #endregion

        #region DragNDrop

        private int roomDraggedIndex = -1;

        private void LvMainListMouseMove(object sender, MouseEventArgs e)
        {
            if (mustStartDragging)
            {
                if (tcMain.SelectedTab == tpRooms)
                {
                    if (ActiveRoom == null) return;
                    if (ActiveRoom.PlacedOnMap) return;
                    roomDraggedIndex = lvMainList.SelectedItems[0].Index;
                    DoDragDrop(new DataObject(DataFormats.CommaSeparatedValue, ActiveRoom.VNum + "," + "room"),
                               DragDropEffects.Move);
                }
                else if (tcMain.SelectedTab == tpMobs)
                {
                    Mob activeMob = ZoneDm.Mobs[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
                    if (activeMob == null) return;
                    DoDragDrop(new DataObject(DataFormats.CommaSeparatedValue, activeMob.VNum + "," + "mob"),
                               DragDropEffects.Move);
                }
                else if (tcMain.SelectedTab == tpObjects)
                {
                    Obj activeObj = ZoneDm.Objects[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
                    if (activeObj == null) return;
                    DoDragDrop(new DataObject(DataFormats.CommaSeparatedValue, activeObj.VNum + "," + "obj"),
                               DragDropEffects.Move);
                }
            }
            mustStartDragging = false;
        }

        private void LvMainListMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                mustStartDragging = true;
        }

        private void LvMainListMouseUp(object sender, MouseEventArgs e)
        {
            if (mustStartDragging)
                mustStartDragging = false;
        }

        private void WldMapRoomDroped()
        {
            RefreshMainList();
            if (roomDraggedIndex >= lvMainList.Items.Count) roomDraggedIndex = lvMainList.Items.Count - 1;
            if (roomDraggedIndex < 0) return;
            lvMainList.EnsureVisible(roomDraggedIndex);
        }

        private void WldMapMobDroped(int mobVnum, Room trgRoom)
        {
            if (trgRoom == null) return;

            DropMobParamsForm dmpf = new DropMobParamsForm();
            if (dmpf.ShowDialog(wldMap) != DialogResult.OK)
            {
                dmpf.Dispose();
                return;
            }

            trgRoom.LoadingMobsCollection.Add(mobVnum, false, dmpf.MaxInRoom);
            if (wldMap.ShowMob)
            {
                wldMap.RecreateRoomBitmap(trgRoom);
                wldMap.Invalidate();
            }
        }

        private void WldMapObjDroped(int objVnum, Room trgRoom)
        {
            DropObjectParamsForm dopf = new DropObjectParamsForm();
            if (dopf.ShowDialog(wldMap) != DialogResult.OK)
            {
                dopf.Dispose();
                return;
            }
            trgRoom.LoadingObjectsCollection.Add(objVnum, dopf.Probability, 0);
            if (wldMap.ShowObj)
            {
                wldMap.RecreateRoomBitmap(trgRoom);
                wldMap.Invalidate();
            }
        }

        private void LvMainListDragEnter(object sender, DragEventArgs drgevent)
        {
            dragDataValid = false;
            foreach (string s in drgevent.Data.GetFormats())
                if (s == DataFormats.CommaSeparatedValue) dragDataValid = true;
            if (dragDataValid)
            {
                //string[] data = drgevent.Data.GetData(DataFormats.CommaSeparatedValue).ToString().Split(',');
                drgevent.Effect = tcMain.SelectedTab != tpRooms ? DragDropEffects.None : DragDropEffects.Move;
            }
            else
                drgevent.Effect = DragDropEffects.None;
        }

        private void LvMainListDragDrop(object sender, DragEventArgs drgevent)
        {
            if (!dragDataValid) return;
            string[] data = drgevent.Data.GetData(DataFormats.CommaSeparatedValue).ToString().Split(',');
            Room room = ZoneDm.Rooms[Convert.ToInt32(data[0]), 0];
            if (room != null)
            {
                if (wldMap.AutolinkingX)
                {
                    if (room.ExitEast.RoomVNum != -1)
                    {
                        Room r = ZoneDm.Rooms[room.ExitEast.RoomVNum, 0];
                        if (r != null)
                        {
                            r.ExitWest.RoomVNum = -1;
                            r.ExitWest.ExitFlag = 0;
                            wldMap.RecreateRoomBitmap(r);
                        }
                    }
                    if (room.ExitWest.RoomVNum != -1)
                    {
                        Room r = ZoneDm.Rooms[room.ExitWest.RoomVNum, 0];
                        if (r != null)
                        {
                            r.ExitEast.RoomVNum = -1;
                            r.ExitEast.ExitFlag = 0;
                            wldMap.RecreateRoomBitmap(r);
                        }
                    }
                }
                if (wldMap.AutolinkingY)
                {
                    if (room.ExitNorth.RoomVNum != -1)
                    {
                        Room r = ZoneDm.Rooms[room.ExitNorth.RoomVNum, 0];
                        if (r != null)
                        {
                            r.ExitSouth.RoomVNum = -1;
                            r.ExitSouth.ExitFlag = 0;
                            wldMap.RecreateRoomBitmap(r);
                        }
                    }
                    if (room.ExitSouth.RoomVNum != -1)
                    {
                        Room r = ZoneDm.Rooms[room.ExitSouth.RoomVNum, 0];
                        if (r != null)
                        {
                            r.ExitNorth.RoomVNum = -1;
                            r.ExitNorth.ExitFlag = 0;
                            wldMap.RecreateRoomBitmap(r);
                        }
                    }
                }
                if (wldMap.AutolinkingZ)
                {
                    if (room.ExitUp.RoomVNum != -1)
                    {
                        Room r = ZoneDm.Rooms[room.ExitUp.RoomVNum, 0];
                        if (r != null)
                        {
                            r.ExitDown.RoomVNum = -1;
                            r.ExitDown.ExitFlag = 0;
                            wldMap.RecreateRoomBitmap(r);
                        }
                    }
                    if (room.ExitDown.RoomVNum != -1)
                    {
                        Room r = ZoneDm.Rooms[room.ExitDown.RoomVNum, 0];
                        if (r != null)
                        {
                            r.ExitUp.RoomVNum = -1;
                            r.ExitUp.ExitFlag = 0;
                            wldMap.RecreateRoomBitmap(r);
                        }
                    }
                }
                room.PlacedOnMap = false;
                room.ExitDown.RoomVNum = -1;
                room.ExitEast.RoomVNum = -1;
                room.ExitNorth.RoomVNum = -1;
                room.ExitSouth.RoomVNum = -1;
                room.ExitUp.RoomVNum = -1;
                room.ExitWest.RoomVNum = -1;
                room.X = -10000;
                room.Y = -10000;
                room.Z = -10000;
                wldMap.RedrawBitmap();
                RefreshMainList();
            }
        }

        #endregion

        #region Обратная навигация

        private void LvDetailsDoubleClick(object sender, EventArgs e)
        {
            if (lvDetails.SelectedItems.Count > 0)
                Navigate((ExListViewItem)(lvDetails.SelectedItems[0]));
        }

        private void TsmiNavigateToClick(object sender, EventArgs e)
        {
            LvDetailsDoubleClick(lvDetails, null);
        }

        #endregion

        #region [!] Common [!]

        #region Методы

        /*private ListViewGroup GetGroup(ListView lv, string groupName)
        {
            foreach (ListViewGroup gr in lv.Groups)
            {
                if (gr.Name == groupName)
                    return gr;
            }
            ListViewGroup group = new ListViewGroup(groupName, HorizontalAlignment.Left);
            group.Name = groupName;
            lv.Groups.Add(group);
            return group;
        }*/

        /*private void ReselectMainListRooms()
        {
            canDolvMainListSelectedIndexChanged = false;
            foreach (CRoom r in SelectedRooms)
            {
                foreach (ListViewItem lvi in lvMainList.Items)
                {
                    if (lvi.Tag.ToString() == r.VNum.ToString())
                        lvi.Selected = true;
                }
            }
            canDolvMainListSelectedIndexChanged = true;
        }*/

        private void CheckSpelling(object sender)
        {
            string err = "";
            string grammarerr = "";
            try
            {
                if (sender is TextBox)
                {
                    err = SpellingChecker.SpellingCheck((TextBox)sender);
                    grammarerr = SpellingChecker.CheckGrammar(((TextBox)sender).Text);
                }
                else if (sender is CExtRichTextBox)
                {
                    err = SpellingChecker.SpellingCheck((CExtRichTextBox)sender);
                    grammarerr = SpellingChecker.CheckGrammar(((CExtRichTextBox)sender).Text);
                }
                string reserr = err;
                if (grammarerr.Length > 0)
                    reserr += "\r\n" + grammarerr;
                else
                    reserr += grammarerr;
                errorProvider.SetError((Control)sender, reserr);
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалась иницализация правописания от Microsoft Office. Microsoft Office не установлен, или версия слишком старая/новая.");
            }
        }

        private void RefreshMainList()
        {
            RefreshMainList(true);
        }

        //Индекс выбранной вкладки при предыдущем обновлении списка
        private int prevSelectedTab = -1;

        private void RefreshMainList(bool keepListSelectedPosition)
        {
            string selectedVNum = string.Empty;
            if (prevSelectedTab != tcMain.SelectedIndex)
                prevSelectedTab = tcMain.SelectedIndex;
            else if (lvMainList.SelectedItems.Count > 0)
                selectedVNum = lvMainList.SelectedItems[0].Tag.ToString();
            cboxMainListConditions.BeginUpdate();
            cboxMainListConditions.Items.Clear();
            if (tcMain.SelectedTab == tpZone)
            {
                lvMainList.Items.Clear();
                cboxMainListConditions.EndUpdate();
                RefreshDetails(ZoneDm);
                RefreshVirtualRoomMobsList(ZoneDm);
                //ZTLogic.RefreshErrList(ZoneDm);
                return;
            }
            bool state = (tcMain.SelectedTab.Name == "tpRooms" || tcMain.SelectedTab.Name == "tpObjects" ||
                          tcMain.SelectedTab.Name == "tpMobs");
            tsbAddTemplate.Enabled = (tcMain.SelectedTab.Name == "tpObjects" || tcMain.SelectedTab.Name == "tpMobs");
            tsbCreateClones.Enabled = state;
            //tsbCopy.Enabled = state;
            //tsbPaste.Enabled = state;
            tsbPasteAsTemplate.Enabled = state;
            tsmiAddTemplate.Enabled = state;
            tsmiCreateClones.Enabled = state;
            //tsmiCopy.Enabled = state;
            //tsmiPaste.Enabled = state;
            tsmiPasteAsTemplate.Enabled = state;
            switch (tcMain.SelectedTab.Name)
            {
                case "tpRooms":
                    lvMainList.MultiSelect = true;
                    cboxMainListConditions.Items.Add("Все комнаты");
                    cboxMainListConditions.Items.Add("Только нерасставленные");
                    cboxMainListConditions.SelectedIndex = lastRoomListCondition;
                    tboxMainListFilter.Text = filterRoom;
                    RefreshRoomsList();
                    break;
                case "tpObjects":
                    //cboxMainListConditions.SelectedIndex = LastObjListCondition;
                    lvMainList.MultiSelect = false;
                    tboxMainListFilter.Text = filterObj;
                    RefreshObjectsList();
                    break;
                case "tpMobs":
                    //cboxMainListConditions.SelectedIndex = LastMobListCondition;
                    lvMainList.MultiSelect = false;
                    tboxMainListFilter.Text = filterMob;
                    RefreshMobsList();
                    break;
                case "tpTriggers":
                    lvMainList.MultiSelect = false;
                    cboxMainListConditions.Items.Add("Все триггеры");
                    cboxMainListConditions.Items.Add("Только для мобов");
                    cboxMainListConditions.Items.Add("Только для объектов");
                    cboxMainListConditions.Items.Add("Только для комнат");
                    cboxMainListConditions.SelectedIndex = lastTrgListCondition;
                    tboxMainListFilter.Text = filterTrg;
                    RefreshTriggersList();
                    break;
            }
            if (keepListSelectedPosition && !string.IsNullOrEmpty(selectedVNum))
                foreach (ListViewItem lvi in lvMainList.Items)
                    if (lvi.Tag.ToString() == selectedVNum)
                    {
                        lvi.Selected = true;
                        break;
                    }

            cboxMainListConditions.EndUpdate();
        }

        #endregion

        private void FocusToControl(object sender, EventArgs e)
        {
            ((Control)sender).Focus();
        }

        private void LastColumnAutosize(object sender, EventArgs e)
        {
            if (((ListView)sender).Columns.Count < 1) return;
            int delta = 0;
            if (((ListView)sender).Columns.Count > 1)
            {
                for (int i = 0; i < ((ListView)sender).Columns.Count - 1; i++)
                    delta += ((ListView)sender).Columns[i].Width;
            }
            // ReSharper disable RedundantCheckBeforeAssignment
            if (((ListView)sender).Columns[((ListView)sender).Columns.Count - 1].Width !=
                ((ListView)sender).Width - 22 - delta)
            // ReSharper restore RedundantCheckBeforeAssignment
            {
                ((ListView)sender).Columns[((ListView)sender).Columns.Count - 1].Width = ((ListView)sender).Width -
                                                                                           22 - delta;
            }
        }

        #endregion

        #region Создание и удаление

        private void CreateRooms()
        {
            CreateNewEntityForm cnef = new CreateNewEntityForm(ref ZoneDm, BasesDm.SectorType, EntityType.Room);
            cnef.RefreshData();
            DialogResult dres = cnef.ShowDialog();
            if (dres == DialogResult.OK)
            {
                RefreshMainList();
                SelectMainListItem(cnef.FirstCreatedNum, true);
            }
            cnef.Dispose();
        }

        private void CreateObject()
        {
            CreateNewEntityForm cnef = new CreateNewEntityForm(ref ZoneDm, ref templatesDm, EntityType.Object);
            cnef.RefreshData();
            DialogResult dres = cnef.ShowDialog();
            if (dres == DialogResult.OK)
            {
                RefreshMainList();
                SelectMainListItem(cnef.FirstCreatedNum, false);
            }
            cnef.Dispose();
        }

        private void CreateMob()
        {
            CreateNewEntityForm cnef = new CreateNewEntityForm(ref ZoneDm, ref templatesDm, EntityType.Mob);
            cnef.RefreshData();
            DialogResult dres = cnef.ShowDialog();
            if (dres == DialogResult.OK)
            {
                RefreshMainList();
                SelectMainListItem(cnef.FirstCreatedNum, false);
            }
            cnef.Dispose();
        }

        private void CreateTrigger()
        {
            int res = ZoneDm.Triggers.AddTrigger(ZoneDm.Zone.Number);
            if (res < 0) return;
            RefreshMainList();
            SelectMainListItem(res, false);
        }

        private void TsbRemoveItemsClick(object sender, EventArgs e)
        {
            int firstSelectedIndex = 0;
            if (lvMainList.SelectedItems.Count <= 0 && wldMap.SelectedRooms.Count <= 0)
                MessageBox.Show("Не выбрано ни одной строки в списке и ни одной комнаты на карте для удаления!", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (lvMainList.SelectedItems.Count <= 0 && wldMap.SelectedRooms.Count > 0)
            {
                if (
                        MessageBox.Show("Подтверждаете удаление " + wldMap.SelectedRooms.Count + " комнат(ы)?", "Удаление", MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    foreach (int vNum in wldMap.SelectedRooms)
                        ZoneDm.RemoveRoom(vNum);
                    RefreshMainList();
                    wldMap.RedrawBitmap();
                }
                return;
            }
            if (lvMainList.SelectedItems.Count <= 0 && tcMain.SelectedTab.Name != "tpRooms")
                firstSelectedIndex = lvMainList.Items[0].Index;
            switch (tcMain.SelectedTab.Name)
            {
                case "tpRooms":
                    if (
                        MessageBox.Show("Подтверждаете удаление " + SelectedRooms.Count + " комнат(ы)?", "Удаление", MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        foreach (Room r in SelectedRooms)
                            ZoneDm.RemoveRoom(r.VNum);
                        RefreshMainList();
                        wldMap.RedrawBitmap();
                    }
                    break;
                case "tpObjects":
                    if (
                        MessageBox.Show(
                            "Подтверждаете удаление предмета " + lvMainList.SelectedItems[0].SubItems[0].Text + " ?",
                            "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        ZoneDm.RemoveObject(Convert.ToInt32(lvMainList.SelectedItems[0].Tag));
                        RefreshMainList();
                    }
                    break;
                case "tpMobs":
                    if (
                        MessageBox.Show(
                            "Подтверждаете удаление моба " + lvMainList.SelectedItems[0].SubItems[0].Text + " ?",
                            "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        ZoneDm.RemoveMob(Convert.ToInt32(lvMainList.SelectedItems[0].Tag));
                        RefreshMainList();
                    }
                    break;
                case "tpTriggers":
                    if (
                        MessageBox.Show(
                            "Подтверждаете удаление триггера " + lvMainList.SelectedItems[0].SubItems[0].Text + " ?",
                            "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        ZoneDm.RemoveTrigger(Convert.ToInt32(lvMainList.SelectedItems[0].Tag));
                        RefreshMainList();
                    }
                    break;
            }
            if (lvMainList.Items.Count <= 0)
            {
            }
            else if (lvMainList.Items.Count - 1 < firstSelectedIndex)
                lvMainList.Items[lvMainList.Items.Count - 1].Selected = true;
            else if (lvMainList.Items.Count - 1 >= firstSelectedIndex)
                lvMainList.Items[firstSelectedIndex].Selected = true;
            SetEnabled();
        }

        private void TsbAddItemsClick(object sender, EventArgs e)
        {
            CreateNewEntity();
        }

        private void CreateNewEntity()
        {
            switch (tcMain.SelectedTab.Name)
            {
                case "tpRooms":
                    break;
                case "tpObjects":
                    CreateObject();
                    break;
                case "tpMobs":
                    CreateMob();
                    break;
                case "tpTriggers":
                    CreateTrigger();
                    break;
            }
            SetEnabled();
        }

        #endregion

        private void CmsMainTreeOpening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            tsmiShowRoomOnMap.Visible = tcMain.SelectedTab.Name == "tpRooms";
        }

        #region Сохранение данных

        private void SaveCurrentTabData()
        {
            switch (tcMain.SelectedTab.Name)
            {
                case "tpZone":
                    ZoneDm.SaveZone();
                    tpZone.ImageIndex = -1;
                    break;
                case "tpRooms":
                    ZoneDm.SaveRooms();
                    tpRooms.ImageIndex = -1;
                    break;
                case "tpObjects":
                    ZoneDm.SaveObjects();
                    tpObjects.ImageIndex = -1;
                    break;
                case "tpMobs":
                    ZoneDm.SaveMobs();
                    tpMobs.ImageIndex = -1;
                    break;
                case "tpTriggers":
                    WriteTrigger();
                    ZoneDm.SaveTriggers();
                    tpTriggers.ImageIndex = -1;
                    break;
            }
        }

        private void SaveAllZoneData()
        {
            if (StaticData.BackupZones)
            {
                if (
                MessageBox.Show(
                    "Сейчас данная зона будет сохранена.\nАрхив с текущим состоянием файлов зоны будет сохранен в каталоге \"ZonesBackup\"\nВы подтверждаете сохранение зоны?",
                    "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
                SaveData(ZoneDm, true);
            }
            else
            {
                DialogResult res = MessageBox.Show(
                    "Сейчас данная зона будет сохранена.\nАвтоматическое сохранение архивов отключено!" +
                    "\nСделать резервную копию текущего состояния файлов зоны?\nДА - Создать резервную копию и сохранить\nНЕТ - сохранить не создавая резервную уопию\nОТМЕНА - не сохранять зону",
                    "Подтверждение", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                    SaveData(ZoneDm, true);
                else if (res == DialogResult.No)
                    SaveData(ZoneDm, false);
            }
        }

        private void SaveData(ZoneDataManager zdm, bool backup)
        {
            if (zdm == null) return;
            if (backup)
            {
                BackupManager bm = new BackupManager();
                bm.BackupFinished += BackupFinished;
                bm.Backup(zdm);
            } 
            zdm.SaveData();
        }

        private void BackupFinished(bool cucces, ZoneDataManager zdm)
        {
            if (!cucces)
            {
                if (MessageBox.Show(this, string.Format("Не удалось создать архивную копию зоны \"[{0}]{1}\" перед сохранением, продолжить сохранение зоны ?", zdm.Zone.Number, zdm.Zone.Name), "Резервное копирование", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    zdm.SaveData();
            }
            else
                zdm.SaveData();
        }

        private void ZoneSaved(string name)
        {
            tpZone.ImageIndex = -1;
            tpRooms.ImageIndex = -1;
            tpMobs.ImageIndex = -1;
            tpObjects.ImageIndex = -1;
            tpTriggers.ImageIndex = -1;
            //tsbSaveZone.Enabled = false;
        }

        #endregion

        #region События

        private void CbDescReplaceCheckedChanged(object sender, EventArgs e)
        {
            SetDescReplaceState();
        }

        private void TsbPasteAsTemplateClick(object sender, EventArgs e)
        {
            PastAsTemplate();
        }

        private void TsbCopyClick(object sender, EventArgs e)
        {
            DoCopy();
        }

        private void TsbPasteClick(object sender, EventArgs e)
        {
            DoPaste();
        }

        private void TsbAddTemplateClick(object sender, EventArgs e)
        {
            AddTemplate();
        }

        private void TsbCreateClonesClick(object sender, EventArgs e)
        {
            CreateClones();
        }

        private void TsbShowRoomDetailsCheckedChanged(object sender, EventArgs e)
        {
            wldMap.RoomDetailsVisible = tsbShowRoomDetails.Checked;
        }

        private void TsbShowRoomObjectsCheckedChanged(object sender, EventArgs e)
        {
            wldMap.ShowObj = tsbShowRoomObjects.Checked;
        }

        private void TsbShowRoomMobsCheckedChanged(object sender, EventArgs e)
        {
            wldMap.ShowMob = tsbShowRoomMobs.Checked;
        }

        private void TsbShowRoomTriggersCheckedChanged(object sender, EventArgs e)
        {
            wldMap.ShowTrg = tsbShowRoomTriggers.Checked;
        }

        private void TsbShowRoomNumbersCheckedChanged(object sender, EventArgs e)
        {
            wldMap.ShowVNums = tsbShowRoomNumbers.Checked;
        }

        private void TsbMapZIncClick(object sender, EventArgs e)
        {
            if (wldMap.CenterRoomZ < StaticData.MaxZ)
                wldMap.CenterRoomZ++;
        }

        private void ToMapCenterClick(object sender, EventArgs e)
        {
            wldMap.ToZeroPoint();
        }

        private void TsbMapZDecClick(object sender, EventArgs e)
        {
            if (wldMap.CenterRoomZ > StaticData.MinZ)
                wldMap.CenterRoomZ--;
        }

        private void TsbMapZoomInClick(object sender, EventArgs e)
        {
            wldMap.MapScale++;
            if (wldMap.MapScale == 25)
                tsbMapZoomIn.Enabled = false;
            if (wldMap.MapScale > 1 && !tsbMapZoomOut.Enabled)
                tsbMapZoomOut.Enabled = true;
        }

        private void TsbMapZoomOutClick(object sender, EventArgs e)
        {
            wldMap.MapScale--;
            if (wldMap.MapScale == 1)
                tsbMapZoomOut.Enabled = false;
            if (wldMap.MapScale < 25 && !tsbMapZoomIn.Enabled)
                tsbMapZoomIn.Enabled = true;
        }

        private void RefreshZoneInfo()
        {
            lvZoneInfo.Items.Clear();
            lvZoneInfo.Items.Add(
                new ListViewItem(new[] { "Всего комнат", ZoneDm.Rooms.Count.ToString() },
                                 lvZoneInfo.Groups[0]));
            lvZoneInfo.Items.Add(
                new ListViewItem(new[] { "Всего объектров", ZoneDm.Objects.Count.ToString() },
                                 lvZoneInfo.Groups[0]));
            lvZoneInfo.Items.Add(
                new ListViewItem(new[] { "Всего мобов", ZoneDm.Mobs.Count.ToString() },
                                 lvZoneInfo.Groups[0]));
            lvZoneInfo.Items.Add(
                new ListViewItem(new[] { "Всего триггеров", ZoneDm.Triggers.Count.ToString() },
                                 lvZoneInfo.Groups[0]));
            chParamName.Width = -1;
            chParamVal.Width = -2;
        }

        private void TcMainSelectedIndexChanged(object sender, EventArgs e)
        {
            lvDetails.Items.Clear();
            //Отключено ввиду того, что теперь оно и не надо вовсе :)
            /*if (prevTabNum == 5) //Сохранение триггера видимо из-за того, что текст триггера не сохраняется автоматически а после захода на вкладку он перечитается из переменной
                WriteTrigger();*/
            RefreshMainList();
            //lastFirstItemTag = new object();
            if (tcMain.SelectedTab == tpZone)
            {
                lvZoneInfo.Visible = true;
                RefreshZoneInfo();
                RefreshAbLists(ZoneDm);
            }
            else
                lvZoneInfo.Visible = false;
            /*if (lvMainList.Items.Count > 0)
                lvMainList.Items[0].Selected = true;*/
            SetSelectedItems();
            SetEnabled();
            //prevTabNum = tcMain.SelectedIndex;
        }

        private void SetSelectedItems()
        {
            if (lvMainList.Items.Count == 0)
                return;
            lvMainList.BeginUpdate();
            lvMainList.SelectedItems.Clear();
            switch (tcMain.SelectedTab.Name)
            {
                case "tpRooms":
                    canUpdateLastSelectedRooms = false;
                    if (lastSelectedRooms.Count > 0)
                    {
                        foreach (ListViewItem lvi in lvMainList.Items)
                        {
                            foreach (Room r in lastSelectedRooms)
                            {
                                if (r.VNum != Convert.ToInt32(lvi.Tag)) continue;
                                lvi.Selected = true;
                                SetMainListTopItem(lvi);
                            }
                        }
                    }
                    else
                        lvMainList.Items[0].Selected = true;
                    canUpdateLastSelectedRooms = true;
                    break;
                case "tpObjects":
                    if (LastSelectedObj != 0)
                        SetSelectedItems(LastSelectedObj);
                    else
                        lvMainList.Items[0].Selected = true;
                    break;
                case "tpMobs":
                    if (LastSelectedMob != 0)
                        SetSelectedItems(LastSelectedMob);
                    else
                        lvMainList.Items[0].Selected = true;
                    break;
                case "tpShops":
                    if (LastSelectedShp != 0)
                        SetSelectedItems(LastSelectedShp);
                    else
                        lvMainList.Items[0].Selected = true;
                    break;
                case "tpTriggers":
                    if (LastSelectedTrg != 0)
                        SetSelectedItems(LastSelectedTrg);
                    else
                        lvMainList.Items[0].Selected = true;
                    break;
            }
            lvMainList.EndUpdate();
            LvMainListSelectedIndexChanged(lvMainList, null);
        }

        private void SetEnabled()
        {
            switch (tcMain.SelectedTab.Name)
            {
                case "tpRooms":
                    splitContainerRooms.Panel1.Enabled = SelectedRooms.Count > 0;
                    splitContainerRooms.Panel2.Enabled = SelectedRooms.Count > 0;
                    splitContainerRoomsDesc.Panel2.Enabled = SelectedRooms.Count > 0;
                    break;
                case "tpObjects":
                    splitContainerObj.Panel1.Enabled = lvMainList.SelectedItems.Count > 0;
                    splitContainerObj.Panel2.Enabled = lvMainList.SelectedItems.Count > 0;
                    break;
                case "tpMobs":
                    splitContainerMob.Panel1.Enabled = lvMainList.SelectedItems.Count > 0;
                    splitContainerMob.Panel2.Enabled = lvMainList.SelectedItems.Count > 0;
                    break;
                case "tpTriggers":
                    splitContainerTrg.Panel1.Enabled = lvMainList.SelectedItems.Count > 0;
                    splitContainerTrg.Panel2.Enabled = lvMainList.SelectedItems.Count > 0;
                    break;
            }
        }

        private void SetSelectedItems(int vNum)
        {
            foreach (ListViewItem lvi in lvMainList.Items)
            {
                if (lvi.Tag.ToString() != vNum.ToString()) continue;
                lvi.Selected = true;
                SetMainListTopItem(lvi);
            }
        }

        /// <summary>
        /// Кнопка управления автолинковкой по X на тулбаре
        /// </summary>
        private void TsbAutolinkingXClick(object sender, EventArgs e)
        {
            wldMap.AutolinkingX = tsbAutolinkingX.Checked;
        }

        /// <summary>
        /// Кнопка управления автолинковкой по Y на тулбаре
        /// </summary>
        private void TsbAutolinkingYClick(object sender, EventArgs e)
        {
            wldMap.AutolinkingY = tsbAutolinkingY.Checked;
        }

        /// <summary>
        /// Кнопка управления автолинковкой по Z на тулбаре
        /// </summary>
        private void TsbAutolinkingZClick(object sender, EventArgs e)
        {
            wldMap.AutolinkingZ = tsbAutolinkingZ.Checked;
        }

        private void TboxMainListFilterTextChanged(object sender, EventArgs e)
        {
            switch (tcMain.SelectedTab.Name)
            {
                case "tpRooms":
                    filterRoom = tboxMainListFilter.Text;
                    break;
                case "tpObjects":
                    filterObj = tboxMainListFilter.Text;
                    break;
                case "tpMobs":
                    filterMob = tboxMainListFilter.Text;
                    break;
                case "tpShops":
                    filterShp = tboxMainListFilter.Text;
                    break;
                case "tpTriggers":
                    filterTrg = tboxMainListFilter.Text;
                    break;
            }
            RefreshMainList();
        }

        private void LvMainListSelectedIndexChanged(object sender, EventArgs e)
        {
            StaticData.CanFireChangeEvent = false;
            if (!canDolvMainListSelectedIndexChanged) return;
            if (lvMainList.SelectedItems.Count == 0) return;
            switch (tcMain.SelectedTab.Name)
            {
                case "tpRooms":
                    splitContainerRooms.Panel1.Enabled = true;
                    splitContainerRooms.Panel2.Enabled = true;
                    splitContainerRoomsDesc.Panel2.Enabled = true;
                    SetRoomReadOnly();
                    break;
                /*case "tpObjects":
                //SetObjectReadOnly();
                break;
            case "tpMobs":
                //SetMobReadOnly();
                break;
            case "tpShops":
                //SetShopReadOnly();
                break;*/
                case "tpTriggers":
                    WriteTrigger();
                    break;
            }
            //if (LastFirstItemTag.Equals(lvMainList.SelectedItems[0].Tag) && lvMainList.SelectedItems.Count <= 1)
            //    return;
            //lastFirstItemTag = lvMainList.SelectedItems[0].Tag;
            //wldMap.HighlightedRooms.Clear();
            switch (tcMain.SelectedTab.Name)
            {
                case "tpRooms":
                    SelectedRooms.Clear();
                    if (canUpdateLastSelectedRooms)
                        lastSelectedRooms.Clear();
                    wldMap.SelectedRooms.Clear();
                    foreach (ListViewItem elvi in lvMainList.SelectedItems)
                    {
                        SelectedRooms.Add(ZoneDm.Rooms[Convert.ToInt32(elvi.Tag), 0]);
                        if (canUpdateLastSelectedRooms)
                            lastSelectedRooms.Add(ZoneDm.Rooms[Convert.ToInt32(elvi.Tag), 0]);
                        wldMap.SelectedRooms.Add(Convert.ToInt32(elvi.Tag));
                        //wldMap.HighlightedRooms.Add(Convert.ToInt32(elvi.Tag));
                    }
                    wldMap.RedrawBitmap();
                    ActiveRoom = ZoneDm.Rooms[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
                    RefreshRoomData();
                    break;
                case "tpObjects":
                    RefreshObjectData();
                    break;
                case "tpMobs":
                    RefreshMobData();
                    break;
                case "tpTriggers":
                    RefreshTriggerData();
                    break;
            }
            SelectedPageChanged?.Invoke(tcMain.SelectedTab.Name);
            SetEnabled();
            if (lvDetails.Items.Count > 0)
                tpInfo.ImageIndex = 45;
            else
                tpInfo.ImageIndex = -1;
            StaticData.CanFireChangeEvent = true;
        }

        private void CboxMainListConditionsSelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tcMain.SelectedTab.Name)
            {
                case "tpRooms":
                    lastRoomListCondition = cboxMainListConditions.SelectedIndex;
                    //lvMainList.MultiSelect = true;
                    RefreshRoomsList();
                    break;
                case "tpObjects":
                    //lastObjListCondition = cboxMainListConditions.SelectedIndex;
                    RefreshObjectsList();
                    break;
                case "tpMobs":
                    //lastMobListCondition = cboxMainListConditions.SelectedIndex;
                    //lvMainList.MultiSelect = true;
                    RefreshMobsList();
                    break;
                case "tpTriggers":
                    lastTrgListCondition = cboxMainListConditions.SelectedIndex;
                    //lvMainList.MultiSelect = false;
                    RefreshTriggersList();
                    break;
            }
        }

        private void CExtRichTextBoxKeyUp(object sender, KeyEventArgs e)
        {
            if (((CExtRichTextBox)sender).SelectedText.Length > 0) return;
            int col = ((CExtRichTextBox)sender).SelectionStart -
                      ((CExtRichTextBox)sender).GetFirstCharIndexOfCurrentLine();
            int row = ((CExtRichTextBox)sender).GetLineFromCharIndex(((CExtRichTextBox)sender).SelectionStart);
            CursorPositionChanged?.Invoke(col, row);
        }

        private void CExtRichTextBoxMouseUp(object sender, MouseEventArgs e)
        {
            if (((CExtRichTextBox)sender).SelectedText.Length > 0) return;
            int col = ((CExtRichTextBox)sender).SelectionStart -
                      ((CExtRichTextBox)sender).GetFirstCharIndexOfCurrentLine();
            int row = ((CExtRichTextBox)sender).GetLineFromCharIndex(((CExtRichTextBox)sender).SelectionStart);
            CursorPositionChanged?.Invoke(col, row);
        }

        private void BtnAddAZonesClick(object sender, EventArgs e)
        {
            ZoneSelectForm zsf = new ZoneSelectForm(WindowParentForm.FileListsDm);
            DialogResult dr = zsf.ShowDialog();
            if (dr == DialogResult.OK)
            {
                foreach (int vnum in zsf.SelectedZones)
                {
                    if (!ZoneDm.Zone.ResetA.Contains(vnum))
                        ZoneDm.Zone.ResetA.Add(vnum);
                }
                RefreshAList(ZoneDm);
            }
            zsf.Dispose();
        }

        private void BtnRemoveAZonesClick(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in lvAZones.SelectedItems)
                ZoneDm.Zone.ResetA.Remove(Convert.ToInt32(lvi.Tag));
            RefreshAList(ZoneDm);
            if (lvAZones.Items.Count <= 0) return;
            lvAZones.Items[lvAZones.Items.Count - 1].Selected = true;
        }

        private void BtnAddBZonesClick(object sender, EventArgs e)
        {
            ZoneSelectForm zsf = new ZoneSelectForm(WindowParentForm.FileListsDm);
            DialogResult dr = zsf.ShowDialog();
            if (dr == DialogResult.OK)
            {
                foreach (int vnum in zsf.SelectedZones)
                {
                    if (!ZoneDm.Zone.ResetB.Contains(vnum))
                        ZoneDm.Zone.ResetB.Add(vnum);
                }
                RefreshBList(ZoneDm);
            }
            zsf.Dispose();
        }

        private void BtnRemoveBZonesClick(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in lvBZones.SelectedItems)
                ZoneDm.Zone.ResetB.Remove(Convert.ToInt32(lvi.Tag));
            RefreshBList(ZoneDm);
            if (lvBZones.Items.Count <= 0) return;
            lvBZones.Items[lvBZones.Items.Count - 1].Selected = true;
        }

        private void TsbBrushCheckedChanged(object sender, EventArgs e)
        {
            wldMap.DrawSketchMode = tsbBrush.Checked;
            tsbSketchColor.Visible = tsbBrush.Checked;
        }

        private void TsbSketchColorClick(object sender, EventArgs e)
        {
            DialogResult dr = colorDlg.ShowDialog(this);
            if (dr == DialogResult.OK)
                wldMap.SketchCurrentColor = colorDlg.Color;
        }

        private void TsbClearSketchClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить эскиз полностью?", "Очистка эскиза", MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Question) == DialogResult.OK)
                wldMap.EraseSketch();
        }

        private void TsbCreateRoomsBySketchClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("Сгенерировать комнаты по эскизу?", "Автогенерация комнат", MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Question) != DialogResult.OK) return;
            //Генерим комнаты пока не упрется
            wldMap.CreateRoomsBySketch();
            //Спрашиваем номер и название новой зоны окном создания зоны
            //Создаем новое окно зоны
            //Переносим туда остатки эскиза
        }

        private void TsbGenerateMapClick(object sender, EventArgs e)
        {
            //int unplaced = 0;
            RoomSelectForm rsf =
                new RoomSelectForm("Выберите центральную комнату зоны", ZoneDm.Rooms, ZoneDm.Zone.Number,
                                   false, true);

            if (rsf.ShowDialog() == DialogResult.OK)
            {
                wldMap.GenerateMap((rsf.SelectedRooms[0]).VNum);
            }
            rsf.Dispose();
            MessageBox.Show(this,
                            string.Format("Неразмещенными остались {0} локаця(ий)",
                                          ZoneDm.Rooms.CountByState(false)), "Автогенерация завершена.");
        }

        private void RoomDescValidated(object sender, EventArgs e)
        {
            SetRoomDescription();
        }

        private void TabControlRoomDescriptionsSelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshRoomDescription();
        }

        private void BtnRoomSpellCheckCommonDescClick(object sender, EventArgs e)
        {
            CheckSpelling(rtbRoomDesc);
        }

        private void BtnRoomFormatClick(object sender, EventArgs e)
        {
            FormatRoomDescription((((Control)sender).Name == "btnRoomSpecFormatCommonDesc"));
        }


        #endregion

        private void TsmiCopyDescClick(object sender, EventArgs e)
        {
            Clipboard.SetText(rtbRoomDesc.SelectedText);
        }

        private void TsmiCutDescClick(object sender, EventArgs e)
        {
            rtbRoomDesc.Cut();
        }

        private void TsmiPasteDescClick(object sender, EventArgs e)
        {
            rtbRoomDesc.Paste();
        }

        private bool canUpdateHorizSbMapValue = true;
        private bool canUpdateVertSbMapValue = true;

        private void HorizSbMapValueChanged(object sender, EventArgs e)
        {
            canUpdateHorizSbMapValue = false;
            wldMap.CenterRoomX = horizSBMap.Value;
            canUpdateHorizSbMapValue = true;
        }

        private void VertSbMapValueChanged(object sender, EventArgs e)
        {
            canUpdateVertSbMapValue = false;
            wldMap.CenterRoomY = vertSBMap.Value;
            canUpdateVertSbMapValue = true;
        }

        private void WldMapCenterRoomXValueChanged(int newX)
        {
            if (canUpdateHorizSbMapValue)
                horizSBMap.Value = newX;
        }

        private void WldMapCenterRoomYValueChanged(int newY)
        {
            if (canUpdateVertSbMapValue)
                vertSBMap.Value = newY;
        }

        #endregion       

        #region Templates

        private void AddTemplate()
        {
            switch (tcMain.SelectedTab.Name)
            {
                case "tpRooms":
                    if (lvMainList.SelectedItems.Count > 0)
                    {
                        //CObject Object = ZoneDm.Objects[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
                        //CtreateTemplateForm ctf = new CtreateTemplateForm("На основе объекта [" + Object.VNum.ToString() + "] " + Object.Cases.Imen);
                        //DialogResult dres = ctf.ShowDialog();
                        //if (dres == DialogResult.OK)
                        //    TemplatesDM.AddTemplate(Object, ctf.TemplateName);
                        //ctf.Dispose();
                    }
                    break;
                case "tpObjects":
                    if (lvMainList.SelectedItems.Count > 0)
                    {
                        Obj obj = ZoneDm.Objects[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
                        CtreateTemplateForm ctf =
                            new CtreateTemplateForm(string.Format("На основе объекта [{0}] {1}", obj.VNum, obj.Cases.Imen));
                        DialogResult dres = ctf.ShowDialog();
                        if (dres == DialogResult.OK)
                            templatesDm.AddTemplate(obj, ctf.TemplateName);
                        ctf.Dispose();
                    }
                    break;
                case "tpMobs":
                    if (lvMainList.SelectedItems.Count > 0)
                    {
                        Mob mob = ZoneDm.Mobs[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
                        CtreateTemplateForm ctf =
                            new CtreateTemplateForm(string.Format("На основе моба [{0}] {1}", mob.VNum, mob.Cases.Imen));
                        DialogResult dres = ctf.ShowDialog();
                        if (dres == DialogResult.OK)
                            templatesDm.AddTemplate(mob, ctf.TemplateName);
                        ctf.Dispose();
                    }
                    break;
            }
        }

        private void CreateClones()
        {
            DoCopy();
            CreateClonesForm ccf = null;
            bool isRoom = false;
            switch (tcMain.SelectedTab.Name)
            {
                case "tpRooms":
                    if (SelectedRooms.Count > 0)
                        ccf = new CreateClonesForm(ref ZoneDm, ref templatesDm, EntityType.Room);
                    isRoom = true;
                    break;
                case "tpObjects":
                    if (lvMainList.SelectedItems.Count > 0)
                        ccf = new CreateClonesForm(ref ZoneDm, ref templatesDm, EntityType.Object);
                    break;
                case "tpMobs":
                    if (lvMainList.SelectedItems.Count > 0)
                        ccf = new CreateClonesForm(ref ZoneDm, ref templatesDm, EntityType.Mob);
                    break;
            }
            if (ccf == null) return;
            if (ccf.ShowDialog() == DialogResult.OK)
            {
                RefreshMainList(false);
                SelectMainListItem(ccf.FirstCreatedNum, isRoom);
            }
        }

        private void DoCopy()
        {
            switch (tcMain.SelectedTab.Name)
            {
                case "tpRooms":
                    if (SelectedRooms.Count > 0)
                    {
                        Room room = (Room)(SelectedRooms[0]);
                        /*if (lvMainList.SelectedItems.Count != 0) 
                            Room = ZoneDm.Rooms[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
                        else
                            Room = ZoneDm.Rooms[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];*/
                        templatesDm.AddToClipboard(room);
                    }
                    break;
                case "tpObjects":
                    if (lvMainList.SelectedItems.Count > 0)
                    {
                        Obj obj = ZoneDm.Objects[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
                        templatesDm.AddToClipboard(obj);
                    }
                    break;
                case "tpMobs":
                    if (lvMainList.SelectedItems.Count > 0)
                    {
                        Mob mob = ZoneDm.Mobs[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
                        templatesDm.AddToClipboard(mob);
                    }
                    break;
                case "tpTriggers":
                    if (lvMainList.SelectedItems.Count > 0)
                    {
                        Trigger trg = ZoneDm.Triggers[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
                        templatesDm.AddToClipboard(trg);
                    }
                    break;
            }
        }

        private void DoPaste()
        {
            CreateClonesForm ccf = new CreateClonesForm();
            switch (tcMain.SelectedTab.Name)
            {
                case "tpRooms":
                    ccf = new CreateClonesForm(ref ZoneDm, ref templatesDm, EntityType.Room);
                    break;
                case "tpObjects":
                    ccf = new CreateClonesForm(ref ZoneDm, ref templatesDm, EntityType.Object);
                    break;
                case "tpMobs":
                    ccf = new CreateClonesForm(ref ZoneDm, ref templatesDm, EntityType.Mob);
                    break;
                case "tpTriggers":
                    Trigger newTrigger = ZoneDm.Triggers[ZoneDm.Triggers.AddTrigger(ZoneDm.Zone.Number), 0];
                    templatesDm.ApplyClipAsTemplate(ref newTrigger);
                    RefreshMainList();
                    return;
            }
            if (ccf.ShowDialog() == DialogResult.OK)
                RefreshMainList();
        }

        private void PastAsTemplate()
        {
            switch (tcMain.SelectedTab.Name)
            {
                case "tpRooms":
                    break;
                case "tpObjects":
                    if (lvMainList.SelectedItems.Count > 0)
                    {
                        Obj obj = ZoneDm.Objects[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
                        if (obj != null)
                            templatesDm.ApplyClipAsTemplate(ref obj, false);
                        RefreshObjectData();
                    }
                    break;
                case "tpMobs":
                    if (lvMainList.SelectedItems.Count > 0)
                    {
                        if (lvMainList.SelectedItems.Count > 0)
                        {
                            Mob mob = ZoneDm.Mobs[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
                            if (mob != null)
                                templatesDm.ApplyClipAsTemplate(ref mob, false);
                            RefreshMobData();
                        }
                    }
                    break;
            }
        }

        public void ApplyTemplate(Guid guid, TemplatesDataManager.TemplatesType type)
        {
            switch (type)
            {
                case TemplatesDataManager.TemplatesType.Object:
                    if (lvMainList.SelectedItems.Count > 0 && tcMain.SelectedTab.Name == "tpObjects")
                    {
                        Obj obj = ZoneDm.Objects[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
                        if (obj != null)
                            templatesDm.ApplyTemplate(ref obj, guid);
                        RefreshObjectData();
                    }
                    break;
                case TemplatesDataManager.TemplatesType.Mob:
                    Mob mob = ZoneDm.Mobs[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
                    if (lvMainList.SelectedItems.Count > 0 && tcMain.SelectedTab.Name == "tpMobs")
                    {
                        if (mob != null)
                            templatesDm.ApplyTemplate(ref mob, guid);
                        RefreshMobData();
                    }
                    break;
            }
        }

        private void TsmiInfoClick(object sender, EventArgs e)
        {
            switch (tcMain.SelectedTab.Name)
            {
                case "tpRooms":
                    if (SelectedRooms.Count == 1)
                    {
                    }
                    if (SelectedRooms.Count > 1)
                    {
                    }
                    break;
                case "tpObjects":
                    if (lvMainList.SelectedItems.Count > 0)
                    {
                        Obj obj = ZoneDm.Objects[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
                        if (obj != null)
                            RefreshDetailsAndLocations(obj);
                        tcListAndInfo.SelectedIndex = 1;
                    }
                    break;
                case "tpMobs":
                    Mob mob = ZoneDm.Mobs[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
                    if (lvMainList.SelectedItems.Count > 0)
                    {
                        if (mob != null)
                            RefreshDetailsAndLocations(mob);
                        tcListAndInfo.SelectedIndex = 1;
                    }
                    break;
            }
        }

        #endregion

        #region Triggers

        /// <summary>
        /// Раскраска выбранный условий срабатывания триггера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LvTrgActivationConditionsItemChecked(object sender, ItemCheckedEventArgs e)
        {
            e.Item.BackColor = e.Item.Checked ? Color.LightGreen : SystemColors.Window;
        }

        public bool BlockCodeEditorTextChanging;
        private void CodeEditorTextChanged(object sender, EventArgs e)
        {
            if (!BlockCodeEditorTextChanging)
                WriteTrigger();
        }

        private void TsbTrgClearClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы подтверждаете очистку текста триггера?", "Подтверждение очистки",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                codeEditor.SetText("");
        }

        private void TsbTrgUndoClick(object sender, EventArgs e)
        {
            codeEditor.Undo();
        }

        private void TsbTrgRedoClick(object sender, EventArgs e)
        {
            codeEditor.Redo();
        }

        private void TsbTrgToggleBookmarkClick(object sender, EventArgs e)
        {
            codeEditor.ToggleBookmark();
        }

        private void TsbTrgGoToPrevBookmarkClick(object sender, EventArgs e)
        {
            codeEditor.GotoNextBookmark();
        }

        private void TsbTrgGoToNextBookmarkClick(object sender, EventArgs e)
        {
            codeEditor.GotoPreviousBookmark();
        }

        private void TsbTrgGoToLineClick(object sender, EventArgs e)
        {
            codeEditor.ShowGotoLine();
        }

        private void TsbTrgSearchClick(object sender, EventArgs e)
        {
            codeEditor.ShowFind();
        }

        private void TsbTrgSearchNextClick(object sender, EventArgs e)
        {
            codeEditor.FindNext();
        }

        private void TsbTrgReplaceClick(object sender, EventArgs e)
        {
            codeEditor.ShowReplace();
        }

        private void TsbTrgIndentClick(object sender, EventArgs e)
        {
            codeEditor.Selection.Indent();
        }

        private void TsbTrgOutdentClick(object sender, EventArgs e)
        {
            codeEditor.Selection.Outdent();
        }

        private void TsbTrgCopyClick(object sender, EventArgs e)
        {
            codeEditor.Copy();
        }

        private void TsbTrgCutClick(object sender, EventArgs e)
        {
            codeEditor.Cut();
        }

        private void TsbTrgPasteClick(object sender, EventArgs e)
        {
            codeEditor.Paste();
        }

        private void tsmiCodeEditorCopy_Click(object sender, EventArgs e)
        {
            codeEditor.Copy();
        }

        private void tsmiCodeEditorCut_Click(object sender, EventArgs e)
        {
            codeEditor.Cut();
        }

        private void tsmiCodeEditorPaste_Click(object sender, EventArgs e)
        {
            codeEditor.Paste();
        }
        private void CboxTrgClassSelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshTrgActCondList(cboxTrgClass.SelectedIndex);
            if (lvMainList.SelectedItems.Count <= 0) return;
            Trigger trigger = ZoneDm.Triggers[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            trigger.Class = cboxTrgClass.SelectedIndex;
        }

        private void TbTrgNameValidated(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Trigger trigger = ZoneDm.Triggers[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            trigger.Name = tbTrgName.Text;
            lvMainList.SelectedItems[0].SubItems[1].Text = trigger.Name;
        }

        private void TbTrgArgsValidated(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Trigger trigger = ZoneDm.Triggers[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            trigger.Arg = tbTrgArgs.Text;
        }

        private void NudTrgNumArgValidated(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Trigger trigger = ZoneDm.Triggers[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            trigger.NumArg = Convert.ToInt32(nudTrgNumArg.Value);
        }

        private void LvTrgActivationConditionsLeave(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Trigger trigger = ZoneDm.Triggers[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            trigger.Type = "";
            foreach (ListViewItem lvi in lvTrgActivationConditions.CheckedItems)
            {
                if (lvi != null)
                    trigger.Type += lvi.Tag.ToString();
            }
        }

        private void WriteTrigger()
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Trigger trigger = ZoneDm.Triggers[LastTriggerEditedVNum, 0];
            if (trigger == null) return;
            trigger.Body = codeEditor.GetText();
        }

        #region Refresh

        public void RefreshTriggersList()
        {
            if (cboxMainListConditions.SelectedIndex == -1) return;
            lvMainList.BeginUpdate();
            lvMainList.Items.Clear();
            foreach (Trigger trigger in ZoneDm.Triggers)
            {
                if (trigger.Class != cboxMainListConditions.SelectedIndex - 1 &&
                    cboxMainListConditions.SelectedIndex != 0) continue;
                ListViewItem lvi = new ListViewItem(new[] { trigger.VNum.ToString(), trigger.Name }) { Tag = trigger.VNum };
                if (trigger.Modifyed)
                    lvi.ImageIndex = 47;
                if (tboxMainListFilter.Text.Length > 0)
                {
                    if (
                        (trigger.Name.ToUpper() + trigger.VNum.ToString().ToUpper()).IndexOf(
                            tboxMainListFilter.Text.ToUpper()) >= 0)
                        lvMainList.Items.Add(lvi);
                }
                else
                    lvMainList.Items.Add(lvi);
            }
            lvMainList.EndUpdate();
        }

        public void RefreshTrgActCondList(int trgClass)
        {
            switch (trgClass)
            {
                case 0: //Тpиггеp для монстpов
                    BindListView(lvTrgActivationConditions, BasesDm.MobTriggerBitvector);
                    break;
                case 1: //Тpиггеp для обьектов
                    BindListView(lvTrgActivationConditions, BasesDm.ObjTriggerBitvector);
                    break;
                case 2: //Тpиггеp для комнат
                    BindListView(lvTrgActivationConditions, BasesDm.WldTriggerBitvector);
                    break;
            }
        }

        public void RefreshTriggerData()
        {
            Trigger trigger = ZoneDm.Triggers[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            cboxTrgClass.SelectedIndex = trigger.Class;
            nudTrgNumArg.Value = trigger.NumArg;
            tbTrgName.Text = trigger.Name;
            tbTrgArgs.Text = trigger.Arg;
            BlockCodeEditorTextChanging = true;
            codeEditor.SetText(trigger.Body);
            BlockCodeEditorTextChanging = false;
            RefreshTrgActCondList(trigger.Class);
            lvTrgActivationConditions.BeginUpdate();
            foreach (ListViewItem lvi in lvTrgActivationConditions.Items)
                lvi.Checked = trigger.Type.IndexOf(lvi.Tag.ToString()) >= 0;
            LastTriggerEditedVNum = trigger.VNum;
            lvTrgActivationConditions.EndUpdate();

            LastSelectedTrg = trigger.VNum;
            RefreshDetailsAndLocations(trigger);
        }

        internal void RefreshDetailsAndLocations(Trigger trigger)
        {
            lvDetails.BeginUpdate();
            ClearDetails();
            wldMap.HighlightedRooms.Clear();
            lvDetails.Groups.Add(new ListViewGroup("Установлен для комнат", HorizontalAlignment.Left));
            foreach (Room r in ZoneDm.Rooms)
            {
                if (!r.TriggersList.Contains(trigger.VNum)) continue;
                lvDetails.Items.Add(new ExListViewItem("[" + r.VNum + "] " + r.Name)
                {
                    Tag = r.VNum,
                    Action = ActionType.GoToRoom,
                    Group = lvDetails.Groups[0]
                });
                wldMap.HighlightedRooms.Add(Convert.ToInt32(r.VNum));
            }
            wldMap.RedrawBitmap();

            lvDetails.Groups.Add(new ListViewGroup("Назначен мобам", HorizontalAlignment.Left));
            foreach (Mob m in ZoneDm.Mobs)
            {
                if (!m.TriggersList.Contains(trigger.VNum)) continue;
                lvDetails.Items.Add(new ExListViewItem("[" + m.VNum + "] " + m.Cases.Imen)
                {
                    Tag = m.VNum,
                    Action = ActionType.GoToMob,
                    Group = lvDetails.Groups[1]
                });
            }

            lvDetails.Groups.Add(new ListViewGroup("Назначен объектам", HorizontalAlignment.Left));
            foreach (Obj o in ZoneDm.Objects)
            {
                if (!o.TriggersList.Contains(trigger.VNum)) continue;
                lvDetails.Items.Add(new ExListViewItem("[" + o.VNum + "] " + o.Cases.Imen)
                {
                    Tag = o.VNum,
                    Action = ActionType.GoToObject,
                    Group = lvDetails.Groups[1]
                });
            }

            lvDetails.EndUpdate();
        }

        #endregion

        private void TsbInsertSpellNumberClick(object sender, EventArgs e)
        {
            SpellSelectForm ssf = new SpellSelectForm(BasesDm);
            if (ssf.ShowDialog() == DialogResult.OK)
            {
                var val = ssf.SelectedSpell;
                codeEditor.Selection.Text = val.ToString();
            }
            ssf.Dispose();
        }

        #endregion

        #region Wld

        private void SetRoomReadOnly()
        {
            tbDoorAlias.Enabled = false;
            tbDoorNameVin.Enabled = false;
            tbDoorDesc.Enabled = false;
            gbDoorType.Enabled = false;
            nudDoorKeyVNum.Enabled = false;

            bool enabledFlag;
            enabledFlag = lvMainList.SelectedItems.Count == 0
                              ? SelectedRooms.Count == 1
                              : lvMainList.SelectedItems.Count == 1;

            #region Установка ридонли

            gboxExits.Enabled = enabledFlag;
            //ОБЪЕКТЫ
            splitContainerRoomObjects.Enabled = enabledFlag;
            /*lvObjectsInRoom.Enabled = DisabledFlag;
            btnAddObjInRoom.Enabled = DisabledFlag;
            btnEditObjInRoom.Enabled = DisabledFlag;
            btnRemoveObjFromRoom.Enabled = DisabledFlag;
            lvObjInObj.Enabled = DisabledFlag;
            btnAddObiInObj.Enabled = DisabledFlag;
            btnEditObjInObj.Enabled = DisabledFlag;
            btnRomoveObjFromObj.Enabled = DisabledFlag;*/
            //МОБЫ
            splitContainerRoomMobs.Enabled = enabledFlag;
            /*lvMobsInRoom.Enabled = DisabledFlag;
            btnAddMobInRoom.Enabled = DisabledFlag;
            btnEditMobInRoom.Enabled = DisabledFlag;
            btnRemoveMobFromRoom.Enabled = DisabledFlag;
            cboxMobFollowBy.Enabled = DisabledFlag;
            nudMaxInRoom.Enabled = DisabledFlag;
            lvObjInMob.Enabled = DisabledFlag;
            btnAddObjInMob.Enabled = DisabledFlag;
            btnEditObjInMob.Enabled = DisabledFlag;
            btnRemoveObjFromMob.Enabled = DisabledFlag;*/
            //ТРИГГЕРЫ
            lvRoomTriggers.Enabled = enabledFlag;
            btnAddRoomTrigger.Enabled = enabledFlag;
            btnRemoveRoomTrigger.Enabled = enabledFlag;
            //ИНГРЕДИЕНТЫ
            elvRoomIngredients.Enabled = enabledFlag;
            btnAddRoomIngredient.Enabled = enabledFlag;
            btnRemoveRoomIngredient.Enabled = enabledFlag;
            //УДАЛЯЕМЫЕ ОБЪЕКТЫ
            lvObjectsToRemove.Enabled = enabledFlag;
            btnAddRoomObjectToRemove.Enabled = enabledFlag;
            btnRemoveRoomObjectToRemove.Enabled = enabledFlag;
            //ДОП.ОПИСАНИЯ
            tbRoomAddDescAliases.Enabled = enabledFlag;
            cbRoomAddDescWordwrap.Enabled = enabledFlag;
            rtbRoomAddDescText.Enabled = enabledFlag;
            btnAddRoomAddDesc.Enabled = enabledFlag;
            btnRemoveRoomAddDesc.Enabled = enabledFlag;
            lvRoomAddDescriptions.Enabled = enabledFlag;
            //ДВЕРИ
            pDoors.Enabled = enabledFlag;

            #endregion
        }

        private void TcRoomSelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshSelectedRoomTabData();
        }

        private void WldMapRoomsSelectionChanged(SelectedRoomsCollection rooms)
        {
            if (tcMain.SelectedIndex != 1) return;
            if (rooms.Count == 0)
            {
                if (tcMain.SelectedTab.Name == "tpRooms")
                {
                    lvMainList.SelectedItems.Clear();
                    splitContainerRooms.Panel1.Enabled = false;
                    splitContainerRooms.Panel2.Enabled = false;
                    splitContainerRoomsDesc.Panel2.Enabled = false;
                }
                return;
            }
            if (tcMain.SelectedTab.Name == "tpRooms")
            {
                splitContainerRooms.Panel1.Enabled = (rooms.Count > 0 || lvMainList.SelectedItems.Count > 0);
                splitContainerRooms.Panel2.Enabled = splitContainerRooms.Panel1.Enabled;
                splitContainerRoomsDesc.Panel2.Enabled = splitContainerRooms.Panel1.Enabled;
                nudMaxInRoom.Value = 1;
                elvRoomMobObjects.Items.Clear();
                elvRoomMobObjectsLoadingAfterDeath.Items.Clear();
                cboxMobFollowBy.Items.Clear();
            }
            lvMainList.BeginUpdate();
            lvMainList.SelectedItems.Clear();

            if (lvMainList.Items.Count > 0)
                foreach (ListViewItem lvi in lvMainList.Items)
                {
                    lvi.Selected = false;
                }
            SelectedRooms.Clear();

            canDolvMainListSelectedIndexChanged = false;
            foreach (int r in rooms)
            {
                foreach (ListViewItem lvi in lvMainList.Items)
                {
                    if (lvi.Tag.ToString() == r.ToString())
                        lvi.Selected = true;
                }
                SelectedRooms.Add(ZoneDm.Rooms[r, 0]);
            }
            if (lvMainList.SelectedItems.Count > 0)
                lvMainList.TopItem = lvMainList.SelectedItems[0];
            canDolvMainListSelectedIndexChanged = true;
            LvMainListSelectedIndexChanged(lvMainList, null);
            ActiveRoom = ZoneDm.Rooms[((int)(rooms[0])), 0];
            if (lvMainList.Items.Count == 0)
                SetRoomReadOnly();
            lvMainList.EndUpdate();
            RefreshRoomData();
        }

        private void BtnAddRoomTriggerClick(object sender, EventArgs e)
        {
            if (WindowParentForm == null) return;
            TriggersCollection allTriggers = WindowParentForm.GetAllKnownTriggers(2);
            var tsf =
                new TrgSelectForm("Выберите триггеры для комнаты", allTriggers, ZoneDm.Zone.Number, true, false);
            DialogResult dres = tsf.ShowDialog();
            if (dres == DialogResult.OK)
            {
                if (ActiveRoom != null)
                {
                    foreach (Trigger trigger in tsf.SelectedTriggers)
                        ActiveRoom.TriggersList.Add(trigger.VNum);
                    RefreshRoomTriggersList(ActiveRoom);
                    wldMap.RecreateRoomBitmap(ActiveRoom);
                    wldMap.RedrawBitmap();
                }
            }
            tsf.Dispose();
        }

        private void BtnRemoveRoomTriggerClick(object sender, EventArgs e)
        {
            if (ActiveRoom == null) return;
            foreach (ListViewItem lvi in lvRoomTriggers.SelectedItems)
                ActiveRoom.TriggersList.Remove(Convert.ToInt32(lvi.Tag));
            wldMap.RecreateRoomBitmap(ActiveRoom);
            wldMap.RedrawBitmap();
            RefreshRoomTriggersList(ActiveRoom);
            if (lvRoomTriggers.Items.Count <= 0) return;
            lvRoomTriggers.Items[lvRoomTriggers.Items.Count - 1].Selected = true;
        }

        private void BtnAddRoomIngredientClick(object sender, EventArgs e)
        {
            if (WindowParentForm == null || ActiveRoom == null) return;
            var isf = new IngrSelectForm("Выберите ингредиенты для комнаты", BasesDm.RoomIngredients);
            DialogResult dres = isf.ShowDialog();
            if (dres == DialogResult.OK)
            {
                foreach (var ingr in isf.SelectedIngredients)
                    ActiveRoom.Ingredients.Add(ingr, 1, 1);
                RefreshRoomIngredientsList(ActiveRoom);
                /*wldMap.RecreateRoomBitmap(ActiveRoom);
                wldMap.RedrawBitmap();*/ // Возможно потом будут окрашиваться комнаты с инграми
            }
            isf.Dispose();
        }

        private void BtnRemoveRoomIngredientClick(object sender, EventArgs e)
        {
            if (ActiveRoom == null) return;
            foreach (ListViewItem lvi in elvRoomIngredients.SelectedItems)
                ActiveRoom.Ingredients.Remove(lvi.Tag.ToString());
            RefreshRoomIngredientsList(ActiveRoom);
            if (elvRoomIngredients.Items.Count <= 0) return;
            elvRoomIngredients.Items[elvRoomIngredients.Items.Count - 1].Selected = true;
        }

        public void RefreshRoomIngredientsList(Room room)
        {
            elvRoomIngredients.Items.Clear();
            foreach (Ingredient ingrdient in room.Ingredients)
            {
                var desc = "";
                foreach (DataRow ingrRow in BasesDm.RoomIngredients.Rows)
                {
                    if (ingrRow["val"].ToString() == ingrdient.TypeName)
                    {
                        desc = ingrRow["desc"].ToString();
                        break;
                    }
                }
                ExListViewItem elvi = new ExListViewItem(ingrdient.Probability.ToString())
                {
                    Tag = ingrdient.TypeName,
                    Guid = ingrdient.InternaGuid
                };
                elvi.SubItems.Add(new ExListViewSubItem(ingrdient.Power.ToString()));
                elvi.SubItems.Add(new ExListViewSubItem(ingrdient.PowerAuto ? "Да" : "Нет"));
                elvi.SubItems.Add(new ExListViewSubItem(desc));
                elvRoomIngredients.Items.Add(elvi);
            }
        }

        private void ElvRoomIngredientsItemValueChanged(ExListViewItem item, int number, string prevVal, string newVal)
        {
            if (ActiveRoom == null) return;
            switch (number)
            {
                case 0:
                    ActiveRoom.Ingredients.ReplaceProbability(item.Guid, StringUtils.ToIntFast(newVal));
                    break;
                case 1:
                    ActiveRoom.Ingredients.ReplacePower(item.Guid, StringUtils.ToIntFast(newVal));
                    RefreshRoomIngredientsList(ActiveRoom);//PowerAuto сбрасывается при установке силы вручную
                    break;
                case 2:
                    ActiveRoom.Ingredients.ReplacePowerAuto(item.Guid, newVal == "Да");
                    RefreshRoomIngredientsList(ActiveRoom);
                    break;
            }
        }

        private void BtnRoomAddObjClick(object sender, EventArgs e)
        {
            if (WindowParentForm == null) return;
            ObjsCollection allObjects = WindowParentForm.GetAllKnownObjects();
            var osf = new ObjSelectForm("Выберите предмет", allObjects, ZoneDm.Zone.Number, false, false);
            DialogResult dres = osf.ShowDialog();
            if (dres == DialogResult.OK)
            {
                if (ActiveRoom != null)
                {
                    //CRoom Room = ZoneDm.Rooms[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
                    if (!ActiveRoom.LoadingObjectsCollection.Contains(((Object)(osf.SelectedObjects[0])).VNum))
                        ActiveRoom.LoadingObjectsCollection.Add(((Object)(osf.SelectedObjects[0])).VNum, 100, 0);
                    RefreshRoomObjectsList(ActiveRoom);
                    wldMap.RecreateRoomBitmap(ActiveRoom);
                    wldMap.RedrawBitmap();
                }
            }
            osf.Dispose();
        }

        private void BtnRoomRemoveObjClick(object sender, EventArgs e)
        {
            if (ActiveRoom == null) return;
            //CRoom Room = ZoneDm.Rooms[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            foreach (ExListViewItem elvi in elvObjectsInRoom.SelectedItems)
            {
                ActiveRoom.LoadingObjectsCollection.RemooveObj(Convert.ToInt32(elvi.SubItems[1].Text));
                wldMap.RecreateRoomBitmap(ActiveRoom);
                wldMap.RedrawBitmap();
            }
            RefreshRoomObjectsList(ActiveRoom);
            if (elvObjectsInRoom.Items.Count <= 0)
            {
                gbObjInObj.Enabled = false;
                return;
            }
            elvObjectsInRoom.Items[elvObjectsInRoom.Items.Count - 1].Selected = true;
        }

        private void ElvObjectsInRoomItemValueChanged(ListViewItem item, int subItemNum, string prevVal, string newVal)
        {
            if (ActiveRoom == null) return;
            switch (subItemNum)
            {
                case 0:
                    ActiveRoom.LoadingObjectsCollection.ReplaceObjProb(StringUtils.ToIntFast(item.SubItems[1].Text), StringUtils.ToIntFast(prevVal), StringUtils.ToIntFast(newVal));
                    break;
                case 3:
                    ActiveRoom.LoadingObjectsCollection.ReplaceLoadType(StringUtils.ToIntFast(item.SubItems[1].Text), BasesDm.GetObjLoadTypeNumByName(prevVal), BasesDm.GetObjLoadTypeNumByName(newVal));
                    break;
            }
            RefreshRoomObjectsList(ActiveRoom);
        }

        private void BtnRoomAddObjInObjClick(object sender, EventArgs e)
        {
            if (ActiveRoom == null) return;
            if (WindowParentForm == null) return;
            ObjsCollection allObjects = WindowParentForm.GetAllKnownObjects();
            var osf = new ObjSelectForm("Выберите предмет", allObjects, ZoneDm.Zone.Number, false, false);
            DialogResult dres = osf.ShowDialog();
            if (dres == DialogResult.OK)
            {
                OperatedObj lObj = ActiveRoom.LoadingObjectsCollection[Convert.ToInt32(elvObjectsInRoom.SelectedItems[0].SubItems[1].Text), 0];
                if (lObj != null)
                {
                    lObj.ObjectsInObject.Add(((Object)osf.SelectedObjects[0]).VNum, 100, 1);
                    RefreshObjInObjList(lObj);
                }
            }
            osf.Dispose();
        }

        private void BtnRoomRemoveObjFromObjClick(object sender, EventArgs e)
        {
            if (elvObjectsInRoom.SelectedItems.Count <= 0) return;
            if (ActiveRoom == null) return;
            OperatedObj roomObj =
                ActiveRoom.LoadingObjectsCollection[
                    Convert.ToInt32(elvObjectsInRoom.SelectedItems[0].SubItems[1].Text), 0];
            foreach (ExListViewItem elvi in elvRoomObjInObj.SelectedItems)
                roomObj.ObjectsInObject.RemooveObj(Convert.ToInt32(elvi.SubItems[1].Text));
            RefreshObjInObjList(roomObj);
            if (elvRoomObjInObj.Items.Count <= 0)
                return;
            elvRoomObjInObj.Items[elvRoomObjInObj.Items.Count - 1].Selected = true;
        }

        private void ElvObjectsInRoomSelectedIndexChanged(object sender, EventArgs e)
        {
            gbObjInObj.Enabled = false;
            elvRoomObjInObj.Items.Clear();
            if (elvObjectsInRoom.SelectedItems.Count <= 0 || WindowParentForm == null)
                return;
            //Загрузка списка объектов выбранного объекта по тагу
            ObjsCollection allObjects = WindowParentForm.GetAllKnownObjects();
            //Проверка, в контейнер ли добавляем объект
            Obj lObj = allObjects[Convert.ToInt32(elvObjectsInRoom.SelectedItems[0].SubItems[1].Text), 0];
            if (lObj != null)
            {
                if (lObj.Type == 15)
                    gbObjInObj.Enabled = true;
                else
                {
                    gbObjInObj.Enabled = false;
                    return;
                }
            }
            else
            {
                gbObjInObj.Enabled = false;
                return;
            }

            gbObjInObj.Text = "Объекты загружаемые в объект [" + elvObjectsInRoom.SelectedItems[0].SubItems[1].Text +
                              "] " + elvObjectsInRoom.SelectedItems[0].SubItems[1].Text;
            if (ActiveRoom != null)
            {
                RefreshObjInObjList(
                    ActiveRoom.LoadingObjectsCollection[
                        Convert.ToInt32(elvObjectsInRoom.SelectedItems[0].SubItems[1].Text), 0]);
            }
        }

        private void ElvRoomObjInObjItemValueChanged(ListViewItem item, int subItemNum, string prevVal, string newVal)
        {
            if (elvObjectsInRoom.SelectedItems.Count <= 0) return;
            if (ActiveRoom == null) return;
            OperatedObj roomObj = ActiveRoom.LoadingObjectsCollection[Convert.ToInt32(elvObjectsInRoom.SelectedItems[0].SubItems[1].Text), 0];
            switch (subItemNum)
            {
                case 0:
                    roomObj.ObjectsInObject.ReplaceObjProb(Convert.ToInt32(item.SubItems[1].Text), Convert.ToInt32(prevVal), Convert.ToInt32(newVal));
                    break;
                case 3:
                    roomObj.ObjectsInObject.ReplaceLoadType(Convert.ToInt32(item.SubItems[1].Text), BasesDm.GetObjLoadTypeNumByName(prevVal), BasesDm.GetObjLoadTypeNumByName(newVal));
                    RefreshObjInObjList(roomObj);
                    break;
            }

            if (roomObj == null) return;
            
            RefreshObjInObjList(roomObj);
        }

        private void TplRoomFlagsValueChanged(object args)
        {
            if (ActiveRoom == null) return;
            ActiveRoom.Flags = ((string)args);
            wldMap.RecreateRoomBitmap(ActiveRoom);
            wldMap.RedrawBitmap();
        }

        private void TplRoomFlagsAdded(string[] val)
        {
            AddRoomFlags(val);
        }

        private void TplRoomFlagsRemoved(string[] val)
        {
            RemoveRoomFlags(val);
        }

        private void TplRoomFlagsSelectionChanged(object args)
        {
            if (!cbShowRoomsWithFlags.Checked) return;
            wldMap.HighlightedRooms.Clear();
            foreach (Room room in ZoneDm.Rooms)
            {
                if (room.Flags.Contains(args.ToString()))
                    wldMap.HighlightedRooms.AddRoom(room.VNum);
            }
            wldMap.RedrawBitmap();
        }

        private void CbShowRoomsWithFlagsCheckedChanged(object sender, EventArgs e)
        {
            if (cbShowRoomsWithFlags.Checked) return;
            wldMap.HighlightedRooms.Clear();
            wldMap.RedrawBitmap();
        }

        private void FormatRoomDescription(bool oneParagraph)
        {
            var tf = new TextFormater(StaticData.OptimalTextWidth);
            rtbRoomDesc.Text = tf.GetFormatedText(rtbRoomDesc.Text, oneParagraph, cbAllowHyp.Checked,
                                                  cbInsertSpaces.Checked);
            SetRoomDescription();
        }

        private void LvMobsInRoomSelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvMobsInRoom.SelectedItems.Count <= 0)
            {
                splitContainerRoomMobs.Panel2.Enabled = false;
                nudMaxInRoom.Value = 1;
                nudMaxInRoom.Enabled = false;
                elvRoomMobObjects.Items.Clear();
                elvRoomMobObjectsLoadingAfterDeath.Items.Clear();
                cboxMobFollowBy.Items.Clear();
                return;
            }
            nudMaxInRoom.Enabled = true;
            mustUpdateMobInRoomData = false;
            cboxMobFollowBy.BeginUpdate();
            splitContainerRoomMobs.Panel2.Enabled = true;
            if (ActiveRoom != null)
            {
                OperatedMob lMob =
                    ActiveRoom.LoadingMobsCollection[((ExListViewItem)(lvMobsInRoom.SelectedItems[0])).Guid];

                //Готовим список лидеров
                cboxMobFollowBy.Items.Add(new TaggedComboBoxItem(-1, "[-1] Сам по себе", -1));
                for (int i = 0; i < lvMobsInRoom.Items.Count; i++)
                {
                    ListViewItem lvi = lvMobsInRoom.Items[i];
                    if (lvi.Selected) continue;
                    var tcbi =
                        new TaggedComboBoxItem(lvi.Tag, "[" + lvi.Text + "] " + lvi.SubItems[1].Text, i);
                    cboxMobFollowBy.Items.Add(tcbi);
                }
                SetCBoxsSelectedItem(cboxMobFollowBy, lMob.FollowsBy);
                //Отключено из за нелепости параметра максимум_в_мире
                /*nudMaxInRoom.Maximum = 10000;
                if (Mob != null)
                {
                    nudMaxInRoom.Maximum = Mob.MaxInWorld;
                }
                else if (LMob != null)
                {
                    nudMaxInRoom.Maximum = LMob.MaxInWorld;
                }
                nudMaxInRoom.Value = LMob.MaxInRoom;*/
                RefreshMobsObjList(lMob);
                RefreshMobsObjLoadingAfterDeathList(lMob);
            }
            cboxMobFollowBy.EndUpdate();
            mustUpdateMobInRoomData = true;
        }

        private void ElvRoomMobObjectsItemValueChanged(ListViewItem item, int number, string prevVal, string newVal)
        {
            if (ActiveRoom == null) return;
            OperatedMob lMob = ActiveRoom.LoadingMobsCollection[((ExListViewItem)(lvMobsInRoom.SelectedItems[0])).Guid];
            //CLoadedMob lMob = ActiveRoom.LoadingMobsCollection[Convert.ToInt32(lvMobsInRoom.SelectedItems[0].Tag), 0];
            switch (number)
            {
                case 0:
                    lMob.Items.ReplaceObjProb(Convert.ToInt32(item.SubItems[1].Text), Convert.ToInt32(prevVal),
                                                Convert.ToInt32(newVal));
                    break;
                case 3:
                    int newV = BasesDm.GetObjPosNumByName(newVal);
                    if (CanSetPosition(newV, Convert.ToInt32(item.SubItems[1].Text)))
                        lMob.Items.ReplaceObjPos(Convert.ToInt32(item.SubItems[1].Text), BasesDm.GetObjPosNumByName(prevVal), newV);
                    RefreshMobsObjList(lMob);
                    break;
            }
        }

        /// <summary>
        /// Проверить, можно ли это экипировать туда куда пытаюсь экипировать
        /// Не запрещать выбирать любую позицию если объект не найден в списке и нет возможности проверить
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="vNum"></param>
        /// <returns></returns>
        private bool CanSetPosition(int pos, int vNum)
        {
            if (WindowParentForm == null) return true;
            ObjsCollection allObjects = WindowParentForm.GetAllKnownObjects();
            Obj curObject = allObjects[vNum, 0];
            if (curObject == null) return true;
            switch (pos)
            {
                case -1: //В инвентаре
                    return true;
                case 0: //Свет
                    return (curObject.Type == 1);
                case 1: //Одето на пальце правой руки
                case 2: //Одето на пальце левой руки
                    return curObject.WearFlags.ToLower().Contains("b0");
                case 3: //Первый предмет, надетый вокруг шеи
                case 4: //Второй предмет, надетый вокруг шеи
                    return curObject.WearFlags.ToLower().Contains("с0");
                case 5: //Одето на теле
                    return curObject.WearFlags.ToLower().Contains("d0");
                case 6: //Одето на голове
                    return curObject.WearFlags.ToLower().Contains("e0");
                case 7: //Одето на ногах
                    return curObject.WearFlags.ToLower().Contains("f0");
                case 8: //Одето на ступнях
                    return curObject.WearFlags.ToLower().Contains("g0");
                case 9: //Одето на кистях рук
                    return curObject.WearFlags.ToLower().Contains("h0");
                case 10: //Одето на руках
                    return curObject.WearFlags.ToLower().Contains("i0");
                case 11: //Используется как щит
                    return curObject.WearFlags.ToLower().Contains("j0");
                case 12: //Накинуто на плечи
                    return curObject.WearFlags.ToLower().Contains("k0");
                case 13: //Одето вокруг талии
                    return curObject.WearFlags.ToLower().Contains("l0");
                case 14: //Одето вокруг правого запястья
                case 15: //Одето вокруг левого запяться
                    return curObject.WearFlags.ToLower().Contains("m0");
                case 16: //Моб вооружен предметом в правой руке
                    return curObject.WearFlags.ToLower().Contains("n0");
                case 17: //Моб держит предмет в левой руке
                    return (
                               curObject.WearFlags.ToLower().Contains("o0") ||
                               curObject.Type == 2 || //Магический свиток
                               curObject.Type == 3 || //Волшебная палочка
                               curObject.Type == 10 //Магический напиток
                           );
                case 18: //Моб вооружен предметом в обеих руках
                    return curObject.WearFlags.ToLower().Contains("p0");
            }
            return true;
        }

        private void BtnRoomAddMobClick(object sender, EventArgs e)
        {
            if (WindowParentForm == null) return;
            MobsCollection allMobs = WindowParentForm.GetAllKnownMobs();
            var msf = new MobSelectForm("Выберите моба", allMobs, ZoneDm.Zone.Number, true, false);
            DialogResult dres = msf.ShowDialog();
            if (dres == DialogResult.OK)
            {
                if (ActiveRoom == null) return;
                foreach (Mob mob in msf.SelectedMobs)
                {
                    ActiveRoom.LoadingMobsCollection.Add(mob.VNum, false, mob.MaxInWorld);
                    //пока по умолчанию макс в комнате равно макс в мире
                }
                RefreshRoomMobsList(ActiveRoom);
                lvMobsInRoom.Items[lvMobsInRoom.Items.Count - 1].Selected = true;
                wldMap.RecreateRoomBitmap(ActiveRoom);
                wldMap.RedrawBitmap();
            }
            msf.Dispose();
        }

        private void BtnRoomRemoveMobClick(object sender, EventArgs e)
        {
            if (ActiveRoom == null) return;
            if (lvMobsInRoom.SelectedItems.Count > 0)
            {
                foreach (OperatedMob lm in ActiveRoom.LoadingMobsCollection)
                {
                    if (lm.FollowsBy == Convert.ToInt32(lvMobsInRoom.SelectedItems[0].Tag))
                        lm.FollowsBy = -1;
                }
                ActiveRoom.LoadingMobsCollection.RemoveMob(((ExListViewItem)(lvMobsInRoom.SelectedItems[0])).Guid);
                wldMap.RecreateRoomBitmap(ActiveRoom);
                wldMap.RedrawBitmap();
            }
            RefreshRoomMobsList(ActiveRoom);
            if (lvMobsInRoom.Items.Count <= 0)
            {
                splitContainerRoomMobs.Panel2.Enabled = false;
                return;
            }
            lvMobsInRoom.Items[lvMobsInRoom.Items.Count - 1].Selected = true;
        }

        private void BtnRoomAddObjToMobClick(object sender, EventArgs e)
        {
            if (ActiveRoom == null) return;
            if (WindowParentForm == null) return;
            ObjsCollection allObjects = WindowParentForm.GetAllKnownObjects();
            var osf = new ObjSelectForm("Выберите предмет", allObjects, ZoneDm.Zone.Number, true, false);
            DialogResult dres = osf.ShowDialog();
            if (dres == DialogResult.OK)
            {
                OperatedMob lMob = ActiveRoom.LoadingMobsCollection[((ExListViewItem)(lvMobsInRoom.SelectedItems[0])).Guid];
                if (lMob != null)
                {
                    foreach (Obj selectedObject in osf.SelectedObjects)
                        lMob.Items.Add(selectedObject.VNum, true, -1, 100);
                    RefreshMobsObjList(lMob);
                }
            }
            osf.Dispose();
        }

        private void BtnRoomRomoveObjFromMobClick(object sender, EventArgs e)
        {
            if (lvMobsInRoom.SelectedItems.Count <= 0) return;
            if (ActiveRoom == null) return;
            OperatedMob roomMob = ActiveRoom.LoadingMobsCollection[((ExListViewItem)(lvMobsInRoom.SelectedItems[0])).Guid];
            foreach (ExListViewItem elvi in elvRoomMobObjects.SelectedItems)
                roomMob.Items.RemoveObj(elvi.Guid);
            RefreshMobsObjList(roomMob);
            if (elvRoomMobObjects.Items.Count <= 0)
                return;
            elvRoomMobObjects.Items[elvRoomMobObjects.Items.Count - 1].Selected = true;
        }

        private void BtnRoomAddObjToMobAfterDeathClick(object sender, EventArgs e)
        {
            if (ActiveRoom == null) return;
            if (WindowParentForm == null) return;
            ObjsCollection allObjects = WindowParentForm.GetAllKnownObjects();
            var osf = new ObjSelectForm("Выберите предмет", allObjects, ZoneDm.Zone.Number, true, false);
            DialogResult dres = osf.ShowDialog();
            if (dres == DialogResult.OK)
            {
                OperatedMob lMob = ActiveRoom.LoadingMobsCollection[((ExListViewItem)(lvMobsInRoom.SelectedItems[0])).Guid];
                if (lMob != null)
                {
                    foreach (Obj selectedObject in osf.SelectedObjects)
                        lMob.ItemsAfterDeath.Add(selectedObject.VNum, 100, 0, 0);
                    RefreshMobsObjLoadingAfterDeathList(lMob);
                }
            }
            osf.Dispose();
        }

        private void ElvRoomMobObjectsLoadingAfterDeathItemValueChanged(ListViewItem item, int number, string prevVal, string newVal)
        {
            if (ActiveRoom == null) return;
            OperatedMob lMob = ActiveRoom.LoadingMobsCollection[((ExListViewItem)(lvMobsInRoom.SelectedItems[0])).Guid];
            switch (number)
            {
                case 0:
                    lMob.ItemsAfterDeath.ReplaceObjProb(Convert.ToInt32(item.SubItems[1].Text), Convert.ToInt32(prevVal), Convert.ToInt32(newVal));
                    break;
                case 3:
                    lMob.ItemsAfterDeath.ReplaceLoadType(Convert.ToInt32(item.SubItems[1].Text), BasesDm.GetObjLoadTypeNumByName(prevVal), BasesDm.GetObjLoadTypeNumByName(newVal));
                    RefreshMobsObjLoadingAfterDeathList(lMob);
                    break;
                case 4:
                    lMob.ItemsAfterDeath.ReplaceSpecParam(Convert.ToInt32(item.SubItems[1].Text), BasesDm.GetObjLoadSpecParamNumByName(prevVal), BasesDm.GetObjLoadSpecParamNumByName(newVal));
                    RefreshMobsObjLoadingAfterDeathList(lMob);
                    break;
            }
        }

        private void BtnRemoveRoomObjFromMobAfterDeathClick(object sender, EventArgs e)
        {
            if (lvMobsInRoom.SelectedItems.Count <= 0) return;
            if (ActiveRoom == null) return;
            OperatedMob roomMob =
                ActiveRoom.LoadingMobsCollection[((ExListViewItem)(lvMobsInRoom.SelectedItems[0])).Guid];
            foreach (ExListViewItem elvi in elvRoomMobObjectsLoadingAfterDeath.SelectedItems)
                roomMob.ItemsAfterDeath.RemoveObj(elvi.Guid);
            RefreshMobsObjLoadingAfterDeathList(roomMob);
            if (elvRoomMobObjectsLoadingAfterDeath.Items.Count <= 0)
                return;
            elvRoomMobObjectsLoadingAfterDeath.Items[elvRoomMobObjectsLoadingAfterDeath.Items.Count - 1].Selected = true;
        }

        private void NudMaxInRoomValidated(object sender, EventArgs e)
        {
            if (!mustUpdateMobInRoomData) return;
            if (ActiveRoom == null) return;
            if (lvMobsInRoom.SelectedItems.Count == 0) return;
            //CLoadedMob LMob = ActiveRoom.LoadingMobsCollection[Convert.ToInt32(lvMobsInRoom.SelectedItems[0].Tag), 0];
            OperatedMob lMob = ActiveRoom.LoadingMobsCollection[((ExListViewItem)(lvMobsInRoom.SelectedItems[0])).Guid];
            lMob.MaxInRoom = Convert.ToInt32(nudMaxInRoom.Value);
        }

        /*private void NudMaxInWorldForRoomValidated(object sender, EventArgs e)
        {
            if (!mustUpdateMobInRoomData) return;
            if (ActiveRoom == null) return;
            if (lvMobsInRoom.SelectedItems.Count == 0) return;
            //CLoadedMob LMob = ActiveRoom.LoadingMobsCollection[Convert.ToInt32(lvMobsInRoom.SelectedItems[0].Tag), 0];
            OperatedMob lMob = ActiveRoom.LoadingMobsCollection[((EXListViewItem)(lvMobsInRoom.SelectedItems[0])).GUID];
            lMob.MaxInWorld = Convert.ToInt32(nudMaxInWorldForRoom.Value);
        }*/

        private void BtnAddRoomObjectToRemoveClick(object sender, EventArgs e)
        {
            if (ActiveRoom == null) return;
            if (WindowParentForm == null) return;
            ObjsCollection allObjects = WindowParentForm.GetAllKnownObjects();
            var osf =
                new ObjSelectForm("Выберите предмет для удаления", allObjects, ZoneDm.Zone.Number, true, false);
            DialogResult dres = osf.ShowDialog();
            if (dres == DialogResult.OK)
            {
                foreach (Obj selectedObject in osf.SelectedObjects)
                    ActiveRoom.RemoovingObjects.Add(selectedObject.VNum, -1, 0);
                RefreshRoomRemovingObjectsList(ActiveRoom);
            }
            osf.Dispose();
        }

        private void BtnRemoveRoomObjectToRemoveClick(object sender, EventArgs e)
        {
            if (ActiveRoom == null) return;
            foreach (ListViewItem lvi in lvObjectsToRemove.SelectedItems)
                ActiveRoom.RemoovingObjects.RemooveObj(Convert.ToInt32(lvi.Tag));
            RefreshRoomRemovingObjectsList(ActiveRoom);
            if (lvObjectsToRemove.Items.Count <= 0) return;
            lvObjectsToRemove.Items[lvObjectsToRemove.Items.Count - 1].Selected = true;
        }

        private void CbRoomAddDescWordwrapCheckedChanged(object sender, EventArgs e)
        {
            rtbRoomAddDescText.WordWrap = cbRoomAddDescWordwrap.Checked;
        }

        private void BtnAddRoomAddDescClick(object sender, EventArgs e)
        {
            if (tbRoomAddDescAliases.Text.Length <= 0 || rtbRoomAddDescText.Text.Length <= 0)
                return;
            //CRoom Room = ZoneDm.Rooms[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            if (ActiveRoom == null) return;
            ActiveRoom.ExtraDescriptions.Add(tbRoomAddDescAliases.Text, rtbRoomAddDescText.Text);
            tbRoomAddDescAliases.Text = "";
            rtbRoomAddDescText.Text = "";
            RefreshRoomAddDescList(ActiveRoom);
        }

        private void BtnRemoveRoomAddDescClick(object sender, EventArgs e)
        {
            if (lvRoomAddDescriptions.SelectedItems.Count <= 0) return;
            //CRoom Room = ZoneDm.Rooms[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            if (ActiveRoom == null) return;
            ActiveRoom.ExtraDescriptions.Remove(lvRoomAddDescriptions.SelectedItems[0].SubItems[0].Text);
            RefreshRoomAddDescList(ActiveRoom);
        }

        private void LvRoomAddDescriptionsDoubleClick(object sender, EventArgs e)
        {
            if (lvRoomAddDescriptions.SelectedItems.Count <= 0) return;
            tbRoomAddDescAliases.Text = lvRoomAddDescriptions.SelectedItems[0].SubItems[0].Text;
            rtbRoomAddDescText.Text = lvRoomAddDescriptions.SelectedItems[0].SubItems[1].Text;
        }

        private void BSelectExitNorthClick(object sender, EventArgs e)
        {
            if (wldMap.ExternalRoomRoomSelection) return;
            wldMap.RoomSelected += WldMapExitNorthSelected;
            wldMap.ExternalRoomRoomSelection = true;
        }

        private void WldMapExitNorthSelected(int vNum)
        {
            tbExitNorth.Text = vNum.ToString();
            ExitDirChanged(tbExitNorth, null);
            wldMap.RoomSelected -= WldMapExitNorthSelected;
        }

        private void BSelectExitSouthClick(object sender, EventArgs e)
        {
            if (wldMap.ExternalRoomRoomSelection) return;
            wldMap.RoomSelected += WldMapExitSouthSelected;
            wldMap.ExternalRoomRoomSelection = true;
        }

        private void WldMapExitSouthSelected(int vNum)
        {
            tbExitSouth.Text = vNum.ToString();
            ExitDirChanged(tbExitSouth, null);
            wldMap.RoomSelected -= WldMapExitSouthSelected;
        }

        private void BSelectExitEastClick(object sender, EventArgs e)
        {
            if (wldMap.ExternalRoomRoomSelection) return;
            wldMap.RoomSelected += WldMapExitEastSelected;
            wldMap.ExternalRoomRoomSelection = true;
        }

        private void WldMapExitEastSelected(int vNum)
        {
            tbExitEast.Text = vNum.ToString();
            ExitDirChanged(tbExitEast, null);
            wldMap.RoomSelected -= WldMapExitEastSelected;
        }

        private void BSelectExitWestClick(object sender, EventArgs e)
        {
            if (wldMap.ExternalRoomRoomSelection) return;
            wldMap.RoomSelected += WldMapExitWestSelected;
            wldMap.ExternalRoomRoomSelection = true;
        }

        private void WldMapExitWestSelected(int vNum)
        {
            tbExitWest.Text = vNum.ToString();
            ExitDirChanged(tbExitWest, null);
            wldMap.RoomSelected -= WldMapExitWestSelected;
        }

        private void BSelectExitUpClick(object sender, EventArgs e)
        {
            if (wldMap.ExternalRoomRoomSelection) return;
            wldMap.RoomSelected += WldMapExitUpSelected;
            wldMap.ExternalRoomRoomSelection = true;
        }

        private void WldMapExitUpSelected(int vNum)
        {
            tbExitUp.Text = vNum.ToString();
            ExitDirChanged(tbExitUp, null);
            wldMap.RoomSelected -= WldMapExitUpSelected;
        }

        private void BSelectExitDownClick(object sender, EventArgs e)
        {
            if (wldMap.ExternalRoomRoomSelection) return;
            wldMap.RoomSelected += WldMapExitDownSelected;
            wldMap.ExternalRoomRoomSelection = true;
        }

        private void WldMapExitDownSelected(int vNum)
        {
            tbExitDown.Text = vNum.ToString();
            ExitDirChanged(tbExitDown, null);
            wldMap.RoomSelected -= WldMapExitDownSelected;
        }

        private void ExitDirChanged(object sender, EventArgs e)
        {
            if (IgnoreExitDirChanged) return;
            if (ActiveRoom == null) return;
            int newVNum = (((NumericBox)sender).Text.Length > 0) ? Convert.ToInt32(((NumericBox)sender).Text) : -1;
            Room trgRoom = ZoneDm.Rooms[newVNum, 0];
            if (tsbSetOppositeExit.Checked && trgRoom != null)
            {
                var sbdf = new SelectBackDirectionForm(trgRoom);
                DialogResult dres = sbdf.ShowDialog();
                if (dres == DialogResult.OK)
                {
                    switch (sbdf.Res)
                    {
                        case "North":
                            trgRoom.ExitNorth.RoomVNum = ActiveRoom.VNum;
                            break;
                        case "South":
                            trgRoom.ExitSouth.RoomVNum = ActiveRoom.VNum;
                            break;
                        case "West":
                            trgRoom.ExitWest.RoomVNum = ActiveRoom.VNum;
                            break;
                        case "East":
                            trgRoom.ExitEast.RoomVNum = ActiveRoom.VNum;
                            break;
                        case "Up":
                            trgRoom.ExitUp.RoomVNum = ActiveRoom.VNum;
                            break;
                        case "Down":
                            trgRoom.ExitDown.RoomVNum = ActiveRoom.VNum;
                            break;
                    }
                    wldMap.RecreateRoomBitmap(trgRoom);
                }
                /*else
                    return; //Выход если нажата кнопка ОТМЕНА*/
                sbdf.Dispose();
            }
            Exit exit = null;
            switch (((NumericBox)sender).Name)
            {
                case "tbExitNorth":
                    exit = ActiveRoom.ExitNorth;
                    break;
                case "tbExitSouth":
                    exit = ActiveRoom.ExitSouth;
                    break;
                case "tbExitEast":
                    exit = ActiveRoom.ExitEast;
                    break;
                case "tbExitWest":
                    exit = ActiveRoom.ExitWest;
                    break;
                case "tbExitUp":
                    exit = ActiveRoom.ExitUp;
                    break;
                case "tbExitDown":
                    exit = ActiveRoom.ExitDown;
                    break;
            }
            if (exit != null && exit.RoomVNum != newVNum)
            {
                exit.RoomVNum = newVNum;
                wldMap.RecreateRoomBitmap(ActiveRoom);
                wldMap.RedrawBitmap();
            }
            RefreshExitsDirections(ActiveRoom);
        }

        private void TbRoomNameValidated(object sender, EventArgs e)
        {
            if (ActiveRoom == null) return;
            foreach (Room r in SelectedRooms)
            {
                r.Name = tbRoomName.Text;
                UpdateRoomInList(r);
            }
            lRoomDesc.Text = "[" + ActiveRoom.VNum + "] " + ActiveRoom.Name;
            //RefreshRoomsList();
            //ReselectMainListRooms();
        }

        private void CboxSectorTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveRoom == null) return;
            if (cboxSectorType.SelectedItem == null) return;
            foreach (Room r in SelectedRooms)
                r.SectorType = Convert.ToInt32(((TaggedComboBoxItem)(cboxSectorType.SelectedItem)).Tag);
        }

        private void BtnSelectDoorKeyClick(object sender, EventArgs e)
        {
            if (WindowParentForm == null) return;
            ObjsCollection allObjects = WindowParentForm.GetAllKnownObjects(18); //Только ключи
            var osf = new ObjSelectForm("Выберите ключ", allObjects, ZoneDm.Zone.Number, false, false);
            DialogResult dres = osf.ShowDialog();
            if (dres == DialogResult.OK)
            {
                nudDoorKeyVNum.Value = ((Object)osf.SelectedObjects[0]).VNum;
                tbRoomDoorKeyName.Text = ((Object)osf.SelectedObjects[0]).Cases.Imen;
            }
            osf.Dispose();
        }

        private void NudDoorKeyVNumValueChanged(object sender, EventArgs e)
        {
            if (WindowParentForm == null) return;
            ObjsCollection allObjects = WindowParentForm.GetAllKnownObjects(18); //Только ключи
            Obj keyObject = allObjects[Convert.ToInt32(nudDoorKeyVNum.Value), 0];
            if (nudDoorKeyVNum.Value == -1)
                tbRoomDoorKeyName.Text = "Ключ здесь не надо";
            else tbRoomDoorKeyName.Text = keyObject != null ? keyObject.Cases.Imen : "!!!Ключа с таким номером не найдено.";
            /*if (lvMainList.SelectedItems.Count <= 0) return;
            CRoom Room = ZoneDm.Rooms[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];*/
            if (ActiveRoom == null) return;
            switch (exitDir)
            {
                case "btnConfigExitNorth":
                    ActiveRoom.ExitNorth.Key = Convert.ToInt32(nudDoorKeyVNum.Value);
                    break;
                case "btnConfigExitSouth":
                    ActiveRoom.ExitSouth.Key = Convert.ToInt32(nudDoorKeyVNum.Value);
                    break;
                case "btnConfigExitEast":
                    ActiveRoom.ExitEast.Key = Convert.ToInt32(nudDoorKeyVNum.Value);
                    break;
                case "btnConfigExitWest":
                    ActiveRoom.ExitWest.Key = Convert.ToInt32(nudDoorKeyVNum.Value);
                    break;
                case "btnConfigExitUp":
                    ActiveRoom.ExitUp.Key = Convert.ToInt32(nudDoorKeyVNum.Value);
                    break;
                case "btnConfigExitDown":
                    ActiveRoom.ExitDown.Key = Convert.ToInt32(nudDoorKeyVNum.Value);
                    break;
            }
        }

        private void DoorDirectionSelect(object sender, EventArgs e)
        {
            tbDoorAlias.Enabled = true;
            tbDoorAlias.Text = string.Empty;
            tbDoorNameVin.Enabled = true;
            tbDoorNameVin.Text = string.Empty;
            tbDoorDesc.Enabled = true;
            tbDoorDesc.Text = string.Empty;
            gbDoorType.Enabled = true;
            nudDoorKeyVNum.Enabled = true;
            btnConfigExitNorth.Enabled = true;
            btnConfigExitSouth.Enabled = true;
            btnConfigExitWest.Enabled = true;
            btnConfigExitEast.Enabled = true;
            btnConfigExitUp.Enabled = true;
            btnConfigExitDown.Enabled = true;
            blockApplying = true;
            cbExitVisible.Checked = false;
            cbExitHidden.Checked = false;
            cbExitDoor.Checked = false;
            cbDoorClosed.Checked = false;
            cbDoorLocked.Checked = false;
            cbDoorPeekproof.Checked = false;
            blockApplying = false;
            if (ActiveRoom == null) return;
            ((Button)sender).Enabled = false;
            exitDir = ((Control)sender).Name;
            Exit curExit = null;
            switch (exitDir)
            {
                case "btnConfigExitNorth":
                    curExit = ActiveRoom.ExitNorth;
                    break;
                case "btnConfigExitSouth":
                    curExit = ActiveRoom.ExitSouth;
                    break;
                case "btnConfigExitEast":
                    curExit = ActiveRoom.ExitEast;
                    break;
                case "btnConfigExitWest":
                    curExit = ActiveRoom.ExitWest;
                    break;
                case "btnConfigExitUp":
                    curExit = ActiveRoom.ExitUp;
                    break;
                case "btnConfigExitDown":
                    curExit = ActiveRoom.ExitDown;
                    break;
            }
            if (curExit == null) return;
            tbDoorAlias.Text = curExit.Aliases;
            tbDoorNameVin.Text = curExit.ExinNameVin;
            tbDoorDesc.Text = curExit.Description;
            SetDoorTypeAndStateFlags(curExit);
            nudDoorKeyVNum.Value = curExit.Key;
            nudLockLevel.Value = curExit.LockLevel;
        }

        public void SetDoorTypeAndStateFlags(Exit exit)
        {
            blockApplying = true;
            cbExitDoor.Checked = false;
            cbDoorPeekproof.Checked = false;
            cbExitHidden.Checked = false;
            int flagval = 16;
            int flag = exit.ExitFlag;
            while (flag > 0)
            {
                if (flag - flagval >= 0)
                {
                    switch (flagval)
                    {
                        case 1:
                            cbExitDoor.Checked = true;
                            break;
                        case 8:
                            cbDoorPeekproof.Checked = true;
                            break;
                        case 16:
                            cbExitHidden.Checked = true;
                            break;
                    }
                    flag -= flagval;
                }
                flagval = (flagval > 1) ? flagval / 2 : 0;
            }
            if (exit.Visibility == 3)
                cbExitHidden.Checked = true;
            else if (exit.Visibility == 4)
                cbExitVisible.Checked = true;
            if (exit.DoorState == 1)
                cbDoorClosed.Checked = true;
            else if (exit.DoorState == 2)
                cbDoorLocked.Checked = true;
            blockApplying = false;
        }

        private void ExitHiddenCheckedChanged(object sender, EventArgs e)
        {
            if (cbExitHidden.Checked)
                cbExitVisible.Checked = false;
            ApplyDoorTypeAndDefStateFlags();
        }

        private void ExitVisibleCheckedChanged(object sender, EventArgs e)
        {
            if (cbExitVisible.Checked)
                cbExitHidden.Checked = false;
            ApplyDoorTypeAndDefStateFlags();
        }

        private bool dontUpdateExit;

        private void ExitDoorCheckedChanged(object sender, EventArgs e)
        {
            ApplyDoorTypeAndDefStateFlags();
            if (!cbExitDoor.Checked)
            {
                cbDoorClosed.Checked = false;
                dontUpdateExit = true;
                nudLockLevel.Value = 0;
                dontUpdateExit = false;
            }
        }

        private void DoorClosedCheckedChanged(object sender, EventArgs e)
        {
            if (cbDoorClosed.Checked)
                cbExitDoor.Checked = true;
            else
                cbDoorLocked.Checked = false;
            ApplyDoorTypeAndDefStateFlags();
        }

        private void DoorLockedCheckedChanged(object sender, EventArgs e)
        {
            if (cbDoorLocked.Checked)
                cbDoorClosed.Checked = true;
            else
                cbDoorPeekproof.Checked = false;
            ApplyDoorTypeAndDefStateFlags();
        }

        private void DoorPeekproofCheckedChanged(object sender, EventArgs e)
        {
            if (cbDoorPeekproof.Checked)
                cbDoorLocked.Checked = true;
            ApplyDoorTypeAndDefStateFlags();
        }

        private void LockLevelValueChanged(object sender, EventArgs e)
        {
            if (dontUpdateExit) return;
            Exit curExit = null;
            switch (exitDir)
            {
                case "btnConfigExitNorth":
                    curExit = ActiveRoom.ExitNorth;
                    break;
                case "btnConfigExitSouth":
                    curExit = ActiveRoom.ExitSouth;
                    break;
                case "btnConfigExitEast":
                    curExit = ActiveRoom.ExitEast;
                    break;
                case "btnConfigExitWest":
                    curExit = ActiveRoom.ExitWest;
                    break;
                case "btnConfigExitUp":
                    curExit = ActiveRoom.ExitUp;
                    break;
                case "btnConfigExitDown":
                    curExit = ActiveRoom.ExitDown;
                    break;
            }
            if (curExit != null)
                curExit.LockLevel = Convert.ToInt32(nudLockLevel.Value);

            if (nudLockLevel.Value > 0 && !cbExitDoor.Checked)
                cbExitDoor.Checked = true;
        }

        bool blockApplying;

        private void ApplyDoorTypeAndDefStateFlags()
        {
            if (ActiveRoom == null || blockApplying) return;

            if (!cbExitDoor.Checked)
                nudDoorKeyVNum.Value = -1;
            nudDoorKeyVNum.Enabled = cbExitDoor.Checked;
            btnSelectDoorKey.Enabled = cbExitDoor.Checked;

            //Обработка изменений типа и состояния по умолчанию для выхода

            int doorFlag = 0;
            int doorDefaultValue = -1;
            int doorVisibility = -1;
            if (cbExitHidden.Checked)
            {
                doorFlag += 16;
                doorVisibility = 3;
            }
            if (cbExitVisible.Checked)
                doorVisibility = 4;
            if (cbExitDoor.Checked)
            {
                doorFlag += 1;
                doorDefaultValue = 0;
            }
            if (cbDoorClosed.Checked)
                doorDefaultValue = 1;
            if (cbDoorLocked.Checked)
                doorDefaultValue = 2;
            if (cbDoorPeekproof.Checked)
                doorFlag += 8;
#if DEBUG
            gbDoorType.Text = "Тип выхода:: doorFlag|" + doorFlag + " doorDefaultValue|" + doorDefaultValue;
#endif
            Exit curExit = null;
            switch (exitDir)
            {
                case "btnConfigExitNorth":
                    curExit = ActiveRoom.ExitNorth;
                    break;
                case "btnConfigExitSouth":
                    curExit = ActiveRoom.ExitSouth;
                    break;
                case "btnConfigExitEast":
                    curExit = ActiveRoom.ExitEast;
                    break;
                case "btnConfigExitWest":
                    curExit = ActiveRoom.ExitWest;
                    break;
                case "btnConfigExitUp":
                    curExit = ActiveRoom.ExitUp;
                    break;
                case "btnConfigExitDown":
                    curExit = ActiveRoom.ExitDown;
                    break;
            }
            if (curExit != null)
            {
                curExit.ExitFlag = Convert.ToInt32(doorFlag);
                curExit.DoorState = doorDefaultValue;
                curExit.Visibility = doorVisibility;
            }
        }

        private void TbDoorAliasValidated(object sender, EventArgs e)
        {
            if (ActiveRoom == null) return;
            string value = tbDoorAlias.Text;
            switch (exitDir)
            {
                case "btnConfigExitNorth":
                    ActiveRoom.ExitNorth.Aliases = value;
                    break;
                case "btnConfigExitSouth":
                    ActiveRoom.ExitSouth.Aliases = value;
                    break;
                case "btnConfigExitEast":
                    ActiveRoom.ExitEast.Aliases = value;
                    break;
                case "btnConfigExitWest":
                    ActiveRoom.ExitWest.Aliases = value;
                    break;
                case "btnConfigExitUp":
                    ActiveRoom.ExitUp.Aliases = value;
                    break;
                case "btnConfigExitDown":
                    ActiveRoom.ExitDown.Aliases = value;
                    break;
            }
        }

        private void TbDoorNameVinValidated(object sender, EventArgs e)
        {
            if (ActiveRoom == null) return;
            string value = tbDoorNameVin.Text;
            switch (exitDir)
            {
                case "btnConfigExitNorth":
                    ActiveRoom.ExitNorth.ExinNameVin = value;
                    break;
                case "btnConfigExitSouth":
                    ActiveRoom.ExitSouth.ExinNameVin = value;
                    break;
                case "btnConfigExitEast":
                    ActiveRoom.ExitEast.ExinNameVin = value;
                    break;
                case "btnConfigExitWest":
                    ActiveRoom.ExitWest.ExinNameVin = value;
                    break;
                case "btnConfigExitUp":
                    ActiveRoom.ExitUp.ExinNameVin = value;
                    break;
                case "btnConfigExitDown":
                    ActiveRoom.ExitDown.ExinNameVin = value;
                    break;
            }
        }

        private void TbDoorDescValidated(object sender, EventArgs e)
        {
            if (ActiveRoom == null) return;
            string value = tbDoorDesc.Text;
            switch (exitDir)
            {
                case "btnConfigExitNorth":
                    ActiveRoom.ExitNorth.Description = value;
                    break;
                case "btnConfigExitSouth":
                    ActiveRoom.ExitSouth.Description = value;
                    break;
                case "btnConfigExitEast":
                    ActiveRoom.ExitEast.Description = value;
                    break;
                case "btnConfigExitWest":
                    ActiveRoom.ExitWest.Description = value;
                    break;
                case "btnConfigExitUp":
                    ActiveRoom.ExitUp.Description = value;
                    break;
                case "btnConfigExitDown":
                    ActiveRoom.ExitDown.Description = value;
                    break;
            }
        }

        private void BtnSelectPotionProtoClick(object sender, EventArgs e)
        {
            if (WindowParentForm == null) return;
            ObjsCollection allObjects = WindowParentForm.GetAllKnownObjects(10); //10.Магический напиток
            var osf =
                new ObjSelectForm("Выберите прототип напитка", allObjects, ZoneDm.Zone.Number, false, true);
            DialogResult dres = osf.ShowDialog();
            if (dres == DialogResult.OK)
            {
                switch (((Control)sender).Name)
                {
                    case "btnSelectFontPorionProto":
                        nudFontPorionProtoVNum.Value = ((Object)osf.SelectedObjects[0]).VNum;
                        break;
                    case "btnSelectPotionProtoVNum":
                        nudPotionProtoVNum.Value = ((Object)osf.SelectedObjects[0]).VNum;
                        break;
                }
            }
            osf.Dispose();
        }

        private void SetRoomDescription()
        {
            //if (ActiveRoom == null) return;
            foreach (Room r in SelectedRooms)
            {
                switch (tabControlRoomDescriptions.SelectedTab.Name)
                {
                    case "tpRoomDesc":
                        r.Description.Main = rtbRoomDesc.Text;
                        break;
                    case "tpRoomDescDay":
                        r.Description.Day = rtbRoomDesc.Text;
                        break;
                    case "tpRoomDescNight":
                        r.Description.Night = rtbRoomDesc.Text;
                        break;
                    case "tpRoomDescSpringDay":
                        r.Description.SpringDay = rtbRoomDesc.Text;
                        break;
                    case "tpRoomDescSpringNight":
                        r.Description.SpringNight = rtbRoomDesc.Text;
                        break;
                    case "tpRoomDescAutumnDay":
                        r.Description.AutumnDay = rtbRoomDesc.Text;
                        break;
                    case "tpRoomDescAutumnNight":
                        r.Description.AutumnNight = rtbRoomDesc.Text;
                        break;
                    case "tpRoomDescSummerDay":
                        r.Description.SummerDay = rtbRoomDesc.Text;
                        break;
                    case "tpRoomDescSummerNight":
                        r.Description.SummerNight = rtbRoomDesc.Text;
                        break;
                    case "tpRoomDescWinterDay":
                        r.Description.WinterDay = rtbRoomDesc.Text;
                        break;
                    case "tpRoomDescWinterNight":
                        r.Description.WinterNight = rtbRoomDesc.Text;
                        break;
                }
            }
        }

        public void RefreshRoomDescription()
        {
            if (ActiveRoom == null) return;
            cbDescReplace.Visible = tabControlRoomDescriptions.SelectedTab.Name != "tpRoomDesc";
            switch (tabControlRoomDescriptions.SelectedTab.Name)
            {
                case "tpRoomDesc":
                    rtbRoomDesc.Text = ActiveRoom.Description.Main;
                    break;
                case "tpRoomDescDay":
                    rtbRoomDesc.Text = ActiveRoom.Description.Day;
                    cbDescReplace.Checked = ActiveRoom.Description.DayR;
                    break;
                case "tpRoomDescNight":
                    rtbRoomDesc.Text = ActiveRoom.Description.Night;
                    cbDescReplace.Checked = ActiveRoom.Description.NightR;
                    break;
                case "tpRoomDescSpringDay":
                    rtbRoomDesc.Text = ActiveRoom.Description.SpringDay;
                    cbDescReplace.Checked = ActiveRoom.Description.SpringDayR;
                    break;
                case "tpRoomDescSpringNight":
                    rtbRoomDesc.Text = ActiveRoom.Description.SpringNight;
                    cbDescReplace.Checked = ActiveRoom.Description.SpringNightR;
                    break;
                case "tpRoomDescAutumnDay":
                    rtbRoomDesc.Text = ActiveRoom.Description.AutumnDay;
                    cbDescReplace.Checked = ActiveRoom.Description.AutumnDayR;
                    break;
                case "tpRoomDescAutumnNight":
                    rtbRoomDesc.Text = ActiveRoom.Description.AutumnNight;
                    cbDescReplace.Checked = ActiveRoom.Description.AutumnNightR;
                    break;
                case "tpRoomDescSummerDay":
                    rtbRoomDesc.Text = ActiveRoom.Description.SummerDay;
                    cbDescReplace.Checked = ActiveRoom.Description.SummerDayR;
                    break;
                case "tpRoomDescSummerNight":
                    rtbRoomDesc.Text = ActiveRoom.Description.SummerNight;
                    cbDescReplace.Checked = ActiveRoom.Description.SummerNightR;
                    break;
                case "tpRoomDescWinterDay":
                    rtbRoomDesc.Text = ActiveRoom.Description.WinterDay;
                    cbDescReplace.Checked = ActiveRoom.Description.WinterDayR;
                    break;
                case "tpRoomDescWinterNight":
                    rtbRoomDesc.Text = ActiveRoom.Description.WinterNight;
                    cbDescReplace.Checked = ActiveRoom.Description.WinterNightR;
                    break;
            }
        }

        public void RefreshDescriptionTabsIcons()
        {
            tpRoomDesc.ImageIndex = !string.IsNullOrEmpty(ActiveRoom.Description.Main) ? 46 : -1;
            tpRoomDescDay.ImageIndex = !string.IsNullOrEmpty(ActiveRoom.Description.Day) ? 46 : -1;
            tpRoomDescNight.ImageIndex = !string.IsNullOrEmpty(ActiveRoom.Description.Night) ? 46 : -1;
            tpRoomDescSpringDay.ImageIndex = !string.IsNullOrEmpty(ActiveRoom.Description.SpringDay) ? 46 : -1;
            tpRoomDescSpringNight.ImageIndex = !string.IsNullOrEmpty(ActiveRoom.Description.SpringNight) ? 46 : -1;
            tpRoomDescAutumnDay.ImageIndex = !string.IsNullOrEmpty(ActiveRoom.Description.AutumnDay) ? 46 : -1;
            tpRoomDescAutumnNight.ImageIndex = !string.IsNullOrEmpty(ActiveRoom.Description.AutumnNight) ? 46 : -1;
            tpRoomDescSummerDay.ImageIndex = !string.IsNullOrEmpty(ActiveRoom.Description.SummerDay) ? 46 : -1;
            tpRoomDescSummerNight.ImageIndex = !string.IsNullOrEmpty(ActiveRoom.Description.SummerNight) ? 46 : -1;
            tpRoomDescWinterDay.ImageIndex = !string.IsNullOrEmpty(ActiveRoom.Description.WinterDay) ? 46 : -1;
            tpRoomDescWinterNight.ImageIndex = !string.IsNullOrEmpty(ActiveRoom.Description.WinterNight) ? 46 : -1;
        }

        public void RefreshRoomTabsData()
        {
            switch (tcRoom.SelectedTab.Name)
            {
                case "tpRoomFlags":
                    RefreshRoomFlagsList(ActiveRoom);
                    break;
                case "tpRoomDoors":
                    btnConfigExitNorth.Enabled = true;
                    btnConfigExitSouth.Enabled = true;
                    btnConfigExitWest.Enabled = true;
                    btnConfigExitEast.Enabled = true;
                    btnConfigExitUp.Enabled = true;
                    btnConfigExitDown.Enabled = true;
                    RefreshExitsDirections(ActiveRoom);
                    break;
                case "tpRoomTrgs":
                    RefreshRoomTriggersList(ActiveRoom);
                    break;
                case "tpRoomObjs":
                    RefreshRoomObjectsList(ActiveRoom);
                    break;
                case "tpRoomMobs":
                    RefreshRoomMobsList(ActiveRoom);
                    nudMaxInRoom.Enabled = false;
                    break;
                case "tpRoomDelObjs":
                    RefreshRoomRemovingObjectsList(ActiveRoom);
                    break;
                case "tpRoomAddDescrs":
                    RefreshRoomAddDescList(ActiveRoom);
                    break;
                case "tpRoomIngrediens":
                    RefreshRoomIngredientsList(ActiveRoom);
                    break;
            }
            IgnoreExitDirChanged = false;
        }

        private void SetDescReplaceState()
        {
            if (ActiveRoom == null) return;
            foreach (Room r in SelectedRooms)
            {
                switch (tabControlRoomDescriptions.SelectedTab.Name)
                {
                    case "tpRoomDescDay":
                        r.Description.DayR = cbDescReplace.Checked;
                        break;
                    case "tpRoomDescNight":
                        r.Description.NightR = cbDescReplace.Checked;
                        break;
                    case "tpRoomDescSpringDay":
                        r.Description.SpringDayR = cbDescReplace.Checked;
                        break;
                    case "tpRoomDescSpringNight":
                        r.Description.SpringNightR = cbDescReplace.Checked;
                        break;
                    case "tpRoomDescAutumnDay":
                        r.Description.AutumnDayR = cbDescReplace.Checked;
                        break;
                    case "tpRoomDescAutumnNight":
                        r.Description.AutumnNightR = cbDescReplace.Checked;
                        break;
                    case "tpRoomDescSummerDay":
                        r.Description.SummerDayR = cbDescReplace.Checked;
                        break;
                    case "tpRoomDescSummerNight":
                        r.Description.SummerNightR = cbDescReplace.Checked;
                        break;
                    case "tpRoomDescWinterDay":
                        r.Description.WinterDayR = cbDescReplace.Checked;
                        break;
                    case "tpRoomDescWinterNight":
                        r.Description.WinterNightR = cbDescReplace.Checked;
                        break;
                }
            }
        }

        private void CboxMobFollowBySelectedValueChanged(object sender, EventArgs e)
        {
            if (!mustUpdateMobInRoomData) return;
            if (ActiveRoom == null) return;
            OperatedMob lMob = ActiveRoom.LoadingMobsCollection[((ExListViewItem)(lvMobsInRoom.SelectedItems[0])).Guid];
            if (lMob == null) return;
            //ToDo: надо ли следующую строчку?
            lMob.Leader = false;
            lMob.FollowsBy = Convert.ToInt32(((TaggedComboBoxItem)(cboxMobFollowBy.SelectedItem)).Tag);
        }

        private void TsmiShowRoomOnMapClick(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count == 1)
            {
                Room room = ZoneDm.Rooms[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
                if (room.PlacedOnMap)
                    wldMap.CenterRoomCoord = room.Location;
                else
                    MessageBox.Show(this, "Комната \"" + room.Name + "\" не размещена на карте.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (lvMainList.SelectedItems.Count > 1)
                MessageBox.Show(this, "Перейти на карте к нескольким выбранным комнатам невозможно!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public void RefreshRoomsList()
        {
            lvMainList.BeginUpdate();
            lvMainList.Items.Clear();
            foreach (Room room in ZoneDm.Rooms)
            {
                if (tboxMainListFilter.Text.Length > 0)
                {
                    if (room.Name.ToUpper().IndexOf(tboxMainListFilter.Text.ToUpper()) >= 0 ||
                        room.VNum.ToString().ToUpper().IndexOf(tboxMainListFilter.Text.ToUpper()) >= 0)
                    {
                        if (cboxMainListConditions.SelectedIndex == 0 || !room.PlacedOnMap)
                        {
                            ListViewItem lvi = new ListViewItem(new[] { room.VNum.ToString(), room.Name })
                            { Tag = room.VNum.ToString() };
                            if (room.Modifyed)
                                lvi.ImageIndex = 47;
                            lvMainList.Items.Add(lvi);
                        }
                    }
                }
                else
                {
                    if (cboxMainListConditions.SelectedIndex == 0 || !room.PlacedOnMap)
                    {
                        ListViewItem lvi = new ListViewItem(new[] { room.VNum.ToString(), room.Name })
                        { Tag = room.VNum.ToString() };
                        if (room.Modifyed)
                            lvi.ImageIndex = 47;
                        lvMainList.Items.Add(lvi);
                    }
                }
            }
            lvMainList.EndUpdate();
        }

        public void UpdateRoomInList(Room room)
        {
            lvMainList.BeginUpdate();
            foreach (ListViewItem lvi in lvMainList.Items)
                if (lvi.Tag.ToString() == room.VNum.ToString())
                {
                    lvi.SubItems[1].Text = room.Name;
                }
            lvMainList.EndUpdate();
        }

        public void RefreshRoomFlagsList(Room room)
        {
            bool multiRoomsMode = SelectedRooms.Count > 1;
            tplRoomFlags.MultiRoomsMode = multiRoomsMode;
            string allFlags = "";
            if (!multiRoomsMode)
                allFlags = room.Flags;
            else
            {
                foreach (Room r in SelectedRooms)
                    allFlags += r.Flags;
            }
            tplRoomFlags.SetData(allFlags, BasesDm.RoomVector);
        }

        public void RefreshRoomTriggersList(Room room)
        {
            lvRoomTriggers.Items.Clear();
            TriggersCollection allTriggers = WindowParentForm.GetAllKnownTriggers(2);
            foreach (int vNum in room.TriggersList)
            {
                Trigger t = allTriggers.GetTrigger(vNum);
                string triggerName = (t != null) ? t.Name : "Триггер из незагруженной зоны";
                //string TriggerName = AllTriggers.GetTrigger(VNum).Name;
                ListViewItem lvi = new ListViewItem(new[] { vNum.ToString(), triggerName }) { Tag = vNum };
                lvRoomTriggers.Items.Add(lvi);
            }
        }

        public void RefreshRoomRemovingObjectsList(Room room)
        {
            lvObjectsToRemove.Items.Clear();
            ObjsCollection allObjects = WindowParentForm.GetAllKnownObjects();
            foreach (OperatedObj obj in room.RemoovingObjects)
            {
                Obj o = allObjects.GetObject(obj.VNum);
                string objectName = (o != null) ? o.Cases.Imen : "Объект из незагруженной зоны";
                //string ObjectName = AllObjects.GetObject(Object.VNum).Cases.Imen;
                ListViewItem lvi = new ListViewItem(new[] { obj.VNum.ToString(), objectName }) { Tag = obj.VNum };
                lvObjectsToRemove.Items.Add(lvi);
            }
        }

        public void RefreshRoomObjectsList(Room room)
        {
            gbObjInObj.Enabled = false;
            elvRoomObjInObj.Items.Clear();
            elvObjectsInRoom.Items.Clear();
            ObjsCollection allObjects = WindowParentForm.GetAllKnownObjects();
            foreach (OperatedObj obj in room.LoadingObjectsCollection)
            {
                Obj o = allObjects.GetObject(obj.VNum);
                string objectName = (o != null) ? o.Cases.Imen : "Объект из незагруженной зоны";
                //string ObjectName = AllObjects.GetObject(Object.VNum).Cases.Imen;
                ExListViewItem elvi = new ExListViewItem(obj.Probability.ToString()) { Tag = obj.VNum };
                elvi.SubItems.Add(new ExListViewSubItem(obj.VNum.ToString()));
                elvi.SubItems.Add(new ExListViewSubItem(objectName));
                elvi.SubItems.Add(new ExListViewSubItem(BasesDm.GetObjLoadTypeNameByNum(obj.LoadType)));
                elvObjectsInRoom.Items.Add(elvi);
            }
        }

        public void RefreshObjInObjList(OperatedObj operatedObject)
        {
            elvRoomObjInObj.Items.Clear();
            ObjsCollection allObjects = WindowParentForm.GetAllKnownObjects();
            foreach (OperatedObj obj in operatedObject.ObjectsInObject)
            {
                string objectName = allObjects.GetObject(obj.VNum).Cases.Imen;
                ExListViewItem elvi = new ExListViewItem(obj.Probability.ToString()) { Tag = obj.VNum };
                elvi.SubItems.Add(new ExListViewSubItem(obj.VNum.ToString()));
                elvi.SubItems.Add(new ExListViewSubItem(objectName));
                elvi.SubItems.Add(new ExListViewSubItem(BasesDm.GetObjLoadTypeNameByNum(obj.LoadType)));
                elvRoomObjInObj.Items.Add(elvi);
            }
        }

        public void RefreshRoomMobsList(Room room)
        {
            elvRoomMobObjects.Items.Clear();
            elvRoomMobObjectsLoadingAfterDeath.Items.Clear();
            lvMobsInRoom.Items.Clear();
            cboxMobFollowBy.Items.Clear();
            nudMaxInRoom.Value = 1;
            MobsCollection allMobs = WindowParentForm.GetAllKnownMobs();
            foreach (OperatedMob mob in room.LoadingMobsCollection)
            {
                Mob m = allMobs[mob.VNum, 0];
                string mobName = (m != null) ? m.Cases.Imen : "Моб из незагруженной зоны";
                ExListViewItem lvi = new ExListViewItem(new[] { mob.VNum.ToString(), mobName })
                {
                    Guid = mob.Guid,
                    Tag = mob.VNum
                };
                lvMobsInRoom.Items.Add(lvi);
            }
        }

        public void RefreshMobsObjList(OperatedMob operatedMob)
        {
            elvRoomMobObjects.Items.Clear();
            nudMaxInRoom.Value = operatedMob.MaxInRoom;
            //nudMaxInWorldForRoom.Value = operatedMob.MaxInWorld;
            ObjsCollection allObjects = WindowParentForm.GetAllKnownObjects();
            foreach (MobObj obj in operatedMob.Items)
            {
                Obj o = allObjects.GetObject(obj.VNum);
                string objectName = (o != null) ? o.Cases.Imen : "Объект из незагруженной зоны";
                ExListViewItem elvi = new ExListViewItem(obj.Probability.ToString())
                {
                    Tag = obj.VNum,
                    Guid = obj.Guid
                };
                elvi.SubItems.Add(new ExListViewSubItem(obj.VNum.ToString()));
                elvi.SubItems.Add(new ExListViewSubItem(objectName));
                elvi.SubItems.Add(new ExListViewSubItem(BasesDm.GetObjPosNameByNum(obj.ObjPos)));
                elvRoomMobObjects.Items.Add(elvi);
            }
        }

        public void RefreshMobsObjLoadingAfterDeathList(OperatedMob operatedMob)
        {
            elvRoomMobObjectsLoadingAfterDeath.Items.Clear();
            nudMaxInRoom.Value = operatedMob.MaxInRoom;
            //nudMaxInWorldForRoom.Value = operatedMob.MaxInWorld;
            ObjsCollection allObjects = WindowParentForm.GetAllKnownObjects();
            foreach (MobObjAfterDeath obj in operatedMob.ItemsAfterDeath)
            {
                Obj o = allObjects.GetObject(obj.VNum);
                string objectName = (o != null) ? o.Cases.Imen : "Объект из незагруженной зоны";
                ExListViewItem elvi = new ExListViewItem(obj.Probability.ToString())
                {
                    Tag = obj.VNum,
                    Guid = obj.Guid
                };
                elvi.SubItems.Add(new ExListViewSubItem(obj.VNum.ToString()));
                elvi.SubItems.Add(new ExListViewSubItem(objectName));
                elvi.SubItems.Add(new ExListViewSubItem(BasesDm.GetObjLoadTypeNameByNum(obj.LoadType)));
                elvi.SubItems.Add(new ExListViewSubItem(BasesDm.GetObjLoadSpecParamNameByNum(obj.SpecParam)));
                elvRoomMobObjectsLoadingAfterDeath.Items.Add(elvi);
            }
        }

        public void RefreshRoomAddDescList(Room room)
        {
            lvRoomAddDescriptions.Items.Clear();
            foreach (ExtraDesc extraDesc in room.ExtraDescriptions)
            {
                ListViewItem lvi = new ListViewItem(new[] { extraDesc.Aliases, extraDesc.Description }) { Tag = extraDesc.Aliases };
                lvRoomAddDescriptions.Items.Add(lvi);
            }
        }

        public void RefreshExitsDirections(Room room)
        {
            btnConfigExitNorth.Visible = (room.ExitNorth.RoomVNum != -1);
            btnConfigExitSouth.Visible = (room.ExitSouth.RoomVNum != -1);
            btnConfigExitWest.Visible = (room.ExitWest.RoomVNum != -1);
            btnConfigExitEast.Visible = (room.ExitEast.RoomVNum != -1);
            btnConfigExitUp.Visible = (room.ExitUp.RoomVNum != -1);
            btnConfigExitDown.Visible = (room.ExitDown.RoomVNum != -1);
            tbDoorAlias.Text = string.Empty;
            tbDoorNameVin.Text = string.Empty;
            tbDoorDesc.Text = string.Empty;
            nudDoorKeyVNum.Value = -1;
            blockApplying = true;
            cbExitVisible.Checked = false;
            cbExitHidden.Checked = false;
            cbExitDoor.Checked = false;
            cbDoorClosed.Checked = false;
            cbDoorLocked.Checked = false;
            cbDoorPeekproof.Checked = false;
            blockApplying = false;
        }

        public void RefreshRoomData()
        {
            IgnoreExitDirChanged = true;
            //для упрощения кода сначала выставляю ридонли, а потом полный рефреш всех данных по первому выбранному номеру комнаты
            //Параметры, загружаемые всегда (по первой выбранной комнате)
            //CRoom Room = ZoneDm.Rooms[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            if (ActiveRoom == null) return;

            //Следующий лейбл добавлен по просьбе Свентовита
            lRoomDesc.Text = "[" + ActiveRoom.VNum + "] " + ActiveRoom.Name;
            tbRoomName.Text = ActiveRoom.Name;
            cboxSectorType.SelectedIndex = ActiveRoom.SectorType;

            //Описание комнаты по сезонам
            RefreshRoomDescription();
            RefreshDescriptionTabsIcons();

            tbExitNorth.Text = (ActiveRoom.ExitNorth.RoomVNum != -1)
                                        ? ActiveRoom.ExitNorth.RoomVNum.ToString()
                                        : "";
            tbExitEast.Text = (ActiveRoom.ExitEast.RoomVNum != -1)
                                       ? ActiveRoom.ExitEast.RoomVNum.ToString()
                                       : "";
            tbExitSouth.Text = (ActiveRoom.ExitSouth.RoomVNum != -1)
                                        ? ActiveRoom.ExitSouth.RoomVNum.ToString()
                                        : "";
            tbExitWest.Text = (ActiveRoom.ExitWest.RoomVNum != -1)
                                       ? ActiveRoom.ExitWest.RoomVNum.ToString()
                                       : "";
            tbExitUp.Text = (ActiveRoom.ExitUp.RoomVNum != -1)
                                     ? ActiveRoom.ExitUp.RoomVNum.ToString()
                                     : "";
            tbExitDown.Text = (ActiveRoom.ExitDown.RoomVNum != -1)
                                       ? ActiveRoom.ExitDown.RoomVNum.ToString()
                                       : "";

            RefreshRoomTabsData();

            IgnoreExitDirChanged = false;
        }

        public void RefreshSelectedRoomTabData()
        {
            if (ActiveRoom == null) return;
            IgnoreExitDirChanged = true;
            RefreshRoomTabsData();
            IgnoreExitDirChanged = false;
        }

        public void RemoveRoomFlags(string[] values)
        {
            foreach (Room r in SelectedRooms)
            {
                foreach (string s in values)
                    r.Flags = r.Flags.Replace(s, "");
                wldMap.RecreateRoomBitmap(r);
            }
            wldMap.RedrawBitmap();
        }

        public void AddRoomFlags(string[] values)
        {
            foreach (Room r in SelectedRooms)
            {
                foreach (string s in values)
                {
                    if (!r.Flags.Contains(s))
                        r.Flags += s;
                }
                wldMap.RecreateRoomBitmap(r);
            }
            wldMap.RedrawBitmap();
        }

        private void WldMapRoomCreated()
        {
            RefreshMainList();
        }

        #endregion

        #region Zon

        private List<ZoneError> errors;

        private void BtnValidateClick(object sender, EventArgs e)
        {
            ZoneValidator validator = new ZoneValidator();
            errors =
                validator.Validate(ZoneDm, WindowParentForm.GetAllKnownMobs(), WindowParentForm.GetAllKnownObjects());
            RefreshErrorsList();
        }

        private void ErrVisibilityChanged(object sender, EventArgs e)
        {
            if (errors == null)
            {
                ZoneValidator validator = new ZoneValidator();
                errors =
                    validator.Validate(ZoneDm, WindowParentForm.GetAllKnownMobs(), WindowParentForm.GetAllKnownObjects());
            }
            RefreshErrorsList();
        }

        private void RefreshErrorsList()
        {
            btnValidate.Enabled = false;
            mlbValidationResult.Items.Clear();
            int cntr = 0;
            foreach (ZoneError err in errors)
            {
                if ((err.ErrType == ParseMessageType.Ошибка && cbShowErrors.Checked)
                    || (err.ErrType == ParseMessageType.Предупреждение && cbShowWarnings.Checked)
                    || (err.ErrType == ParseMessageType.Информация && cbShowInfo.Checked))
                {
                    mlbValidationResult.Items.Add(new ParseMessageEventArgs(err.ErrType,
                                                                            "#" + cntr++ + " " + err.ErrCaption,
                                                                            err.ErrMessage, err.VNum, err.Action));
                }
            }
            mlbValidationResult.Invalidate();

            btnValidate.Enabled = true;
        }

        private void MlbValidationResultDoubleClick(object sender, EventArgs e)
        {
            if (mlbValidationResult.SelectedItems.Count > 0)
                Navigate((ParseMessageEventArgs)(mlbValidationResult.SelectedItems[0]));

        }

        private void NudZoneLevelValueChanged(object sender, EventArgs e)
        {
            ZoneDm.Zone.Level = Convert.ToInt32(nudZoneLevel.Value);
        }

        private void BtnChangeZoneNumberClick(object sender, EventArgs e)
        {
            if (!nudZoneNumber.Enabled)
            {
                nudZoneNumber.Enabled = true;
                btnChangeZoneNumber.Text = "Применить";
            }
            else
            {
                if (Convert.ToInt32(nudZoneNumber.Value) == ZoneDm.Zone.Number) return;
                if (WindowParentForm.IsZoneExists(Convert.ToInt32(nudZoneNumber.Value)))
                {
                    errorProvider.SetError(nudZoneNumber, "Зона с таким номером уже сужествует.\nВ случае необходимости, Вы можете удалить файлы имеющейся зоны с номером " + ZoneDm.Zone.Number + ", а зетем обновить список доступных зон.");
                    return;
                }
                nudZoneNumber.Enabled = false;
                errorProvider.SetError(nudZoneNumber, "");
                btnChangeZoneNumber.Text = "Изменить";
                int oldVnum = ZoneDm.Zone.Number;
                int newVnum = Convert.ToInt32(nudZoneNumber.Value);
                ZoneDm.ChangeZoneNumber(newVnum);
                TabText = string.Format("[{0}] {1}", newVnum, ZoneDm.Zone.Name);
                ZoneNumberChanged?.Invoke(oldVnum, newVnum);
                //надо как-то хитро менять все хранимые VNumы 
                //причем менять не только VNumы в этой зоне но и в других зонах, если в них
                //используется что-то из этой зоны
            }
        }

        private void TbZoneNameValidated(object sender, EventArgs e)
        {
            if (tbZoneName.Text == "") return;
            ZoneDm.Zone.Name = tbZoneName.Text;
            TabText = string.Format("[{0}] {1}", ZoneDm.Zone.Number, ZoneDm.Zone.Name);
            ZoneRenamed?.Invoke(ZoneDm.Zone.Number, ZoneDm.Zone.Name);
        }

        private void TbZoneCommentValidated(object sender, EventArgs e)
        {
            ZoneDm.Zone.Comment = tbZoneComment.Text;
        }

        private void TbZoneLocationValidated(object sender, EventArgs e)
        {
            ZoneDm.Zone.Location = tbZoneLocation.Text;
        }

        private void TbZoneDescriptionValidated(object sender, EventArgs e)
        {
            ZoneDm.Zone.Description = tbZoneDescription.Text;
        }

        private void TbZoneAuthorValidated(object sender, EventArgs e)
        {
            ZoneDm.Zone.Author = tbZoneAuthor.Text;
        }

        private void CboxZonTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboxZonType.SelectedItem != null)
                ZoneDm.Zone.Type = Convert.ToInt32(((TaggedComboBoxItem)(cboxZonType.SelectedItem)).Tag);
        }

        private void NudOptimalCharsInGroupValueChanged(object sender, EventArgs e)
        {
            ZoneDm.Zone.OptimalCharsInGroup = (int)nudOptimalCharsInGroup.Value;
        }

        private void CbZoneReopopTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbZoneReopopType.SelectedItem != null)
            {
                ZoneDm.Zone.RepopType = Convert.ToInt32(((TaggedComboBoxItem)(cbZoneReopopType.SelectedItem)).Tag);
                gbResetRelatedZones.Visible = ZoneDm.Zone.RepopType == 3;
            }
        }

        private void NudRepopTimerValueChanged(object sender, EventArgs e)
        {
            ZoneDm.Zone.RepopTimer = (int)(nudRepopTimer.Value);
        }

        internal void RefreshAbLists(ZoneDataManager ZoneDm)
        {
            RefreshAList(ZoneDm);
            RefreshBList(ZoneDm);
        }

        internal void RefreshAList(ZoneDataManager ZoneDm)
        {
            lvAZones.BeginUpdate();
            lvAZones.Items.Clear();
            foreach (int relZoneNum in ZoneDm.Zone.ResetA)
            {
                ListViewItem lvi = new ListViewItem(relZoneNum.ToString()) { Tag = relZoneNum };
                lvAZones.Items.Add(lvi);
            }
            lvAZones.EndUpdate();
        }

        internal void RefreshBList(ZoneDataManager ZoneDm)
        {
            lvBZones.BeginUpdate();
            lvBZones.Items.Clear();
            foreach (int relZoneNum in ZoneDm.Zone.ResetB)
            {
                ListViewItem lvi = new ListViewItem(relZoneNum.ToString()) { Tag = relZoneNum };
                lvBZones.Items.Add(lvi);
            }
            lvBZones.EndUpdate();
        }

        internal void RefreshDetails(ZoneDataManager ZoneDm)
        {
            MobsCollection allmobs = WindowParentForm.GetAllKnownMobs();
            ObjsCollection allobjects = WindowParentForm.GetAllKnownObjects();

            lvDetails.BeginUpdate();
            ClearDetails();

            lvDetails.Groups.Add(new ListViewGroup("Мобы, удаляемые при ребуте", HorizontalAlignment.Left));
            foreach (OperatedMob m in ZoneDm.Zone.MobsToRemove)
            {
                Mob curMob = allmobs[m.VNum, 0];
                lvDetails.Items.Add(new ExListViewItem("[" + m.VNum + "] " + ((curMob != null) ? curMob.Cases.Imen : ""))
                {
                    Tag = m.VNum,
                    Action = ActionType.GoToMob,
                    Group = lvDetails.Groups[0]
                });
            }

            lvDetails.Groups.Add(new ListViewGroup("Преметы, рассыпающиеся при репопе зоны", HorizontalAlignment.Left));
            foreach (Obj o in ZoneDm.Objects)
            {
                if (!o.Affects.Contains("D0")) continue;
                Obj curObj = allobjects[o.VNum, 0];
                lvDetails.Items.Add(new ExListViewItem("[" + o.VNum + "] " + ((curObj != null) ? curObj.Cases.Imen : ""))
                {
                    Tag = o.VNum,
                    Action = ActionType.GoToObject,
                    Group = lvDetails.Groups[1]
                });
            }

            lvDetails.EndUpdate();
        }

        public void RefreshVirtualRoomMobsList(ZoneDataManager ZoneDm)
        {
            elvVitrualRoomMobObjects.Items.Clear();
            elvVitrualRoomMobObjectsAfterDeath.Items.Clear();
            lvMobsInVitrualRoom.Items.Clear();
            cboxVitrualRoomMobFollowBy.Items.Clear();
            nudVirtualRoomMobMaxInRoom.Value = 1;
            MobsCollection allMobs = WindowParentForm.GetAllKnownMobs();
            foreach (OperatedMob mob in ZoneDm.Zone.MobsLoadedInVirtualRoom)
            {
                var m = allMobs[mob.VNum, 0];
                string mobName = (m != null) ? m.Cases.Imen : "Моб из незагруженной зоны";
                var lvi = new ExListViewItem(new[] { mob.VNum.ToString(), mobName })
                {
                    Guid = mob.Guid,
                    Tag = mob.VNum
                };
                lvMobsInVitrualRoom.Items.Add(lvi);
            }
        }

        public void RefreshVirtualRoomMobsObjList(OperatedMob operatedMob)
        {
            elvVitrualRoomMobObjects.Items.Clear();
            nudVirtualRoomMobMaxInRoom.Value = operatedMob.MaxInRoom;
            //nudVirtualRoomMobMaxInWorld.Value = operatedMob.MaxInWorld;
            ObjsCollection allObjects = WindowParentForm.GetAllKnownObjects();
            foreach (MobObj obj in operatedMob.Items)
            {
                var o = allObjects.GetObject(obj.VNum);
                string objectName = (o != null) ? o.Cases.Imen : "Объект из незагруженной зоны";
                var elvi = new ExListViewItem(obj.Probability.ToString())
                {
                    Tag = obj.VNum,
                    Guid = obj.Guid
                };
                elvi.SubItems.Add(new ExListViewSubItem(obj.VNum.ToString()));
                elvi.SubItems.Add(new ExListViewSubItem(objectName));
                elvi.SubItems.Add(new ExListViewSubItem(BasesDm.GetObjPosNameByNum(obj.ObjPos)));
                elvVitrualRoomMobObjects.Items.Add(elvi);
            }
        }

        private void lvMobsInVitrualRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvMobsInVitrualRoom.SelectedItems.Count <= 0)
            {
                scontMobInVitrualRoomLoadedObjects.Enabled = false;
                nudVirtualRoomMobMaxInRoom.Value = 1;
                nudVirtualRoomMobMaxInRoom.Enabled = false;
                elvVitrualRoomMobObjects.Items.Clear();
                elvVitrualRoomMobObjectsAfterDeath.Items.Clear();
                cboxVitrualRoomMobFollowBy.Items.Clear();
                return;
            }
            scontMobInVitrualRoomLoadedObjects.Enabled = true;
            nudVirtualRoomMobMaxInRoom.Enabled = true;
            mustUpdateMobVirtualInRoomData = false;
            cboxVitrualRoomMobFollowBy.BeginUpdate();

            if (ZoneDm.Zone.MobsLoadedInVirtualRoom.Count > 0)
            {
                OperatedMob lMob = ZoneDm.Zone.MobsLoadedInVirtualRoom[((ExListViewItem)(lvMobsInVitrualRoom.SelectedItems[0])).Guid];

                //Готовим список лидеров
                cboxVitrualRoomMobFollowBy.Items.Add(new TaggedComboBoxItem(-1, "[-1] Сам по себе", -1));
                for (int i = 0; i < lvMobsInVitrualRoom.Items.Count; i++)
                {
                    ListViewItem lvi = lvMobsInVitrualRoom.Items[i];
                    if (lvi.Selected) continue;
                    var tcbi =
                        new TaggedComboBoxItem(lvi.Tag, "[" + lvi.Text + "] " + lvi.SubItems[1].Text, i);
                    cboxVitrualRoomMobFollowBy.Items.Add(tcbi);
                }
                SetCBoxsSelectedItem(cboxVitrualRoomMobFollowBy, lMob.FollowsBy);
                //Отключено из за нелепости параметра максимум_в_мире
                /*nudMaxInRoom.Maximum = 10000;
                if (Mob != null)
                {
                    nudMaxInRoom.Maximum = Mob.MaxInWorld;
                }
                else if (LMob != null)
                {
                    nudMaxInRoom.Maximum = LMob.MaxInWorld;
                }
                nudMaxInRoom.Value = LMob.MaxInRoom;*/
                RefreshVirtualRoomMobsObjList(lMob);
                RefreshVirtualRoomMobsObjAfterDeathList(lMob);
            }
            cboxVitrualRoomMobFollowBy.EndUpdate();
            mustUpdateMobVirtualInRoomData = true;
        }

        private void bdtAddMobInVirtualRoom_Click(object sender, EventArgs e)
        {
            if (WindowParentForm == null) return;
            MobsCollection allMobs = WindowParentForm.GetAllKnownMobs();
            var msf = new MobSelectForm("Выберите моба", allMobs, ZoneDm.Zone.Number, true, false);
            DialogResult dres = msf.ShowDialog();
            if (dres == DialogResult.OK)
            {
                foreach (Mob mob in msf.SelectedMobs)
                {
                    ZoneDm.Zone.MobsLoadedInVirtualRoom.Add(mob.VNum, false, mob.MaxInWorld);
                    //пока по умолчанию макс в комнате равно макс в мире
                }
                RefreshVirtualRoomMobsList(ZoneDm);
                lvMobsInVitrualRoom.Items[lvMobsInVitrualRoom.Items.Count - 1].Selected = true;
            }
            msf.Dispose();
        }

        private void btnRemoveMobFromVitrualRoom_Click(object sender, EventArgs e)
        {
            if (lvMobsInVitrualRoom.SelectedItems.Count > 0)
            {
                foreach (OperatedMob lm in ZoneDm.Zone.MobsLoadedInVirtualRoom)
                {
                    if (lm.FollowsBy == Convert.ToInt32(lvMobsInVitrualRoom.SelectedItems[0].Tag))
                        lm.FollowsBy = -1;
                }
                ZoneDm.Zone.MobsLoadedInVirtualRoom.RemoveMob(((ExListViewItem)(lvMobsInVitrualRoom.SelectedItems[0])).Guid);

            }
            RefreshVirtualRoomMobsList(ZoneDm);
            if (lvMobsInVitrualRoom.Items.Count <= 0)
            {
                splitContainerVirtualRoomMobs.Panel2.Enabled = false;
                return;
            }
            lvMobsInVitrualRoom.Items[lvMobsInVitrualRoom.Items.Count - 1].Selected = true;
        }

        private void cboxVitrualRoomMobFollowBySelectedIndexChanged(object sender, EventArgs e)
        {
            if (!mustUpdateMobVirtualInRoomData) return;
            OperatedMob lMob = ZoneDm.Zone.MobsLoadedInVirtualRoom[((ExListViewItem)(lvMobsInVitrualRoom.SelectedItems[0])).Guid];
            if (lMob == null) return;
            //ToDo: надо ли следующую строчку?
            lMob.Leader = false;
            lMob.FollowsBy = Convert.ToInt32(((TaggedComboBoxItem)(cboxVitrualRoomMobFollowBy.SelectedItem)).Tag);
        }

        private void nudVirtualRoomMobMaxInRoomValidated(object sender, EventArgs e)
        {
            if (!mustUpdateMobVirtualInRoomData) return;
            if (lvMobsInVitrualRoom.SelectedItems.Count == 0) return;
            //CLoadedMob LMob = ActiveRoom.LoadingMobsCollection[Convert.ToInt32(lvMobsInRoom.SelectedItems[0].Tag), 0];
            OperatedMob lMob = ZoneDm.Zone.MobsLoadedInVirtualRoom[((ExListViewItem)(lvMobsInVitrualRoom.SelectedItems[0])).Guid];
            lMob.MaxInRoom = Convert.ToInt32(nudVirtualRoomMobMaxInRoom.Value);
        }

        /*private void nudVirtualRoomMobMaxInWorldValidated(object sender, EventArgs e)
        {
            if (!mustUpdateMobVirtualInRoomData) return;
            if (lvMobsInVitrualRoom.SelectedItems.Count == 0) return;
            //CLoadedMob LMob = ActiveRoom.LoadingMobsCollection[Convert.ToInt32(lvMobsInRoom.SelectedItems[0].Tag), 0];
            OperatedMob lMob = ZoneDm.Zone.MobsLoadedInVirtualRoom[((EXListViewItem)(lvMobsInVitrualRoom.SelectedItems[0])).GUID];
            lMob.MaxInWorld = Convert.ToInt32(nudVirtualRoomMobMaxInWorld.Value);
        }*/

        private void btnAddItemToMobInVirtualRoomClick(object sender, EventArgs e)
        {
            if (WindowParentForm == null) return;
            ObjsCollection allObjects = WindowParentForm.GetAllKnownObjects();
            var osf = new ObjSelectForm("Выберите предмет", allObjects, ZoneDm.Zone.Number, true, false);
            DialogResult dres = osf.ShowDialog();
            if (dres == DialogResult.OK)
            {
                //CLoadedMob LMob = ActiveRoom.LoadingMobsCollection[Convert.ToInt32(lvMobsInRoom.SelectedItems[0].Tag), 0];
                OperatedMob lMob = ZoneDm.Zone.MobsLoadedInVirtualRoom[((ExListViewItem)(lvMobsInVitrualRoom.SelectedItems[0])).Guid];
                if (lMob != null)
                {
                    foreach (Obj selectedObject in osf.SelectedObjects)
                        lMob.Items.Add(selectedObject.VNum, true, -1, 100);
                    RefreshVirtualRoomMobsObjList(lMob);
                }
            }
            osf.Dispose();
        }

        private void elvVitrualRoomMobObjects_ItemValueChanged(ListViewItem item, int subItemNumber, string prevVal, string newVal)
        {
            OperatedMob lMob = ZoneDm.Zone.MobsLoadedInVirtualRoom[((ExListViewItem)(lvMobsInVitrualRoom.SelectedItems[0])).Guid];
            //CLoadedMob lMob = ActiveRoom.LoadingMobsCollection[Convert.ToInt32(lvMobsInRoom.SelectedItems[0].Tag), 0];
            switch (subItemNumber)
            {
                case 0:
                    lMob.Items.ReplaceObjProb(Convert.ToInt32(item.SubItems[1].Text), Convert.ToInt32(prevVal),
                        Convert.ToInt32(newVal));
                    break;
                case 3:
                    int newV = BasesDm.GetObjPosNumByName(newVal);
                    if (CanSetPosition(newV, Convert.ToInt32(item.SubItems[1].Text)))
                        lMob.Items.ReplaceObjPos(Convert.ToInt32(item.SubItems[1].Text), BasesDm.GetObjPosNumByName(prevVal), newV);
                    RefreshMobsObjList(lMob);
                    break;
            }
        }

        private void btnRemoveItemFromMobInVirtualRoomClick(object sender, EventArgs e)
        {
            if (lvMobsInRoom.SelectedItems.Count <= 0) return;
            OperatedMob roomMob = ZoneDm.Zone.MobsLoadedInVirtualRoom[((ExListViewItem)lvMobsInVitrualRoom.SelectedItems[0]).Guid];
            foreach (ExListViewItem elvi in elvVitrualRoomMobObjects.SelectedItems)
                roomMob.Items.RemoveObj(elvi.Guid);
            RefreshVirtualRoomMobsObjList(roomMob);
            if (elvVitrualRoomMobObjects.Items.Count <= 0)
                return;
            elvVitrualRoomMobObjects.Items[elvVitrualRoomMobObjects.Items.Count - 1].Selected = true;
        }

        private void elvVitrualRoomMobObjectsAfterDeath_ItemValueChanged(ListViewItem item, int subItemNumber, string prevVal, string newVal)
        {
            OperatedMob lMob = ZoneDm.Zone.MobsLoadedInVirtualRoom[((ExListViewItem)lvMobsInVitrualRoom.SelectedItems[0]).Guid];
            //int newV;
            switch (subItemNumber)
            {
                case 0:
                    lMob.ItemsAfterDeath.ReplaceObjProb(Convert.ToInt32(item.SubItems[1].Text), Convert.ToInt32(prevVal),
                        Convert.ToInt32(newVal));
                    break;
                case 3:
                    lMob.ItemsAfterDeath.ReplaceLoadType(Convert.ToInt32(item.SubItems[1].Text), BasesDm.GetObjLoadTypeNumByName(prevVal), BasesDm.GetObjLoadTypeNumByName(newVal));
                    RefreshVirtualRoomMobsObjAfterDeathList(lMob);
                    break;
                case 4:
                    lMob.ItemsAfterDeath.ReplaceSpecParam(Convert.ToInt32(item.SubItems[1].Text), BasesDm.GetObjLoadSpecParamNumByName(prevVal), BasesDm.GetObjLoadSpecParamNumByName(newVal));
                    RefreshVirtualRoomMobsObjAfterDeathList(lMob);
                    break;
            }
        }
        private void btnAddItemToMobInVirtualRoomAfterDeathClick(object sender, EventArgs e)
        {
            if (WindowParentForm == null) return;
            ObjsCollection allObjects = WindowParentForm.GetAllKnownObjects();
            var osf = new ObjSelectForm("Выберите предмет", allObjects, ZoneDm.Zone.Number, true, false);
            DialogResult dres = osf.ShowDialog();
            if (dres == DialogResult.OK)
            {
                OperatedMob lMob = ZoneDm.Zone.MobsLoadedInVirtualRoom[((ExListViewItem)(lvMobsInVitrualRoom.SelectedItems[0])).Guid];
                if (lMob != null)
                {
                    foreach (Obj selectedObject in osf.SelectedObjects)
                        lMob.ItemsAfterDeath.Add(selectedObject.VNum, 100, 0, 0);
                    RefreshVirtualRoomMobsObjAfterDeathList(lMob);
                }
            }
            osf.Dispose();
        }

        private void btnRemoveItemFromMobInVirtualRoomAfterDeathClick(object sender, EventArgs e)
        {
            if (lvMobsInRoom.SelectedItems.Count <= 0) return;
            OperatedMob roomMob = ZoneDm.Zone.MobsLoadedInVirtualRoom[((ExListViewItem)(lvMobsInVitrualRoom.SelectedItems[0])).Guid];
            foreach (ExListViewItem elvi in elvVitrualRoomMobObjectsAfterDeath.SelectedItems)
                roomMob.ItemsAfterDeath.RemoveObj(elvi.Guid);
            RefreshVirtualRoomMobsObjAfterDeathList(roomMob);
            if (elvVitrualRoomMobObjectsAfterDeath.Items.Count <= 0)
                return;
            elvVitrualRoomMobObjectsAfterDeath.Items[elvVitrualRoomMobObjectsAfterDeath.Items.Count - 1].Selected = true;
        }
        public void RefreshVirtualRoomMobsObjAfterDeathList(OperatedMob operatedMob)
        {
            elvVitrualRoomMobObjectsAfterDeath.Items.Clear();
            ObjsCollection allObjects = WindowParentForm.GetAllKnownObjects();
            foreach (MobObjAfterDeath obj in operatedMob.ItemsAfterDeath)
            {
                var o = allObjects.GetObject(obj.VNum);
                string objectName = (o != null) ? o.Cases.Imen : "Объект из незагруженной зоны";
                var elvi = new ExListViewItem(obj.Probability.ToString())
                {
                    Tag = obj.VNum,
                    Guid = obj.Guid
                };
                elvi.SubItems.Add(new ExListViewSubItem(obj.VNum.ToString()));
                elvi.SubItems.Add(new ExListViewSubItem(objectName));
                elvi.SubItems.Add(new ExListViewSubItem(BasesDm.GetObjLoadTypeNameByNum(obj.LoadType)));
                elvi.SubItems.Add(new ExListViewSubItem(BasesDm.GetObjLoadSpecParamNameByNum(obj.SpecParam)));
                elvVitrualRoomMobObjectsAfterDeath.Items.Add(elvi);
            }
        }

        #endregion

        #region Obj

        private Object CurrentObject
        {
            get
            {
                if (lvMainList.SelectedItems.Count == 0) return null;
                return ZoneDm.Objects[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            }
        }

        private void TabControlObjectSelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshSelectedObjectTabData();
        }

        private void CBoxObjTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Obj curObject = ZoneDm.Objects[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            curObject.Type = cboxObjType.SelectedIndex + 1;
            foreach (Control c in gbObjType.Controls)
            {
                if (c is Panel)
                    c.Visible = false;
            }
            tboxObjActionDesc.Visible = false;
            label35.Visible = false;
            cboxObjSkill.Enabled = true;

            if (!MustRefreshTypeSpecParams) return;
            MustUpdateTypeSpecParams = false;

            RefreshDefaultSpecParams(curObject);

            MustUpdateTypeSpecParams = true;
        }

        private void ObjValueValidated(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0 || !MustUpdateObjData) return;
            Obj curObject = ZoneDm.Objects[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            switch (((Control)sender).Name)
            {
                case "cboxObjectGender":
                    curObject.Sex = Convert.ToInt32(((TaggedComboBoxItem)(cboxObjectGender.SelectedItem)).Tag);
                    break;
                case "tboxObjAliases":
                    curObject.Alias = tboxObjAliases.Text;
                    break;
                case "tboxObjImen":
                    curObject.Cases.Imen = tboxObjImen.Text;
                    lvMainList.SelectedItems[0].SubItems[1].Text = curObject.Cases.Imen;
                    break;
                case "tboxObjRod":
                    curObject.Cases.Rod = tboxObjRod.Text;
                    break;
                case "tboxObjDat":
                    curObject.Cases.Dat = tboxObjDat.Text;
                    break;
                case "tboxObjVin":
                    curObject.Cases.Vin = tboxObjVin.Text;
                    break;
                case "tboxObjTvor":
                    curObject.Cases.Tvor = tboxObjTvor.Text;
                    break;
                case "tboxObjPredl":
                    curObject.Cases.Pred = tboxObjPredl.Text;
                    break;
                case "tboxObjDesc":
                    curObject.Desc = tboxObjDesc.Text;
                    break;
                case "tboxObjActionDesc":
                    curObject.ActionDesc = tboxObjActionDesc.Text;
                    break;
                case "nudObjRentPriceEquip":
                    curObject.RentWear = Convert.ToInt32(nudObjRentPriceEquip.Value);
                    break;
                case "nudObjRentPriceInv":
                    curObject.RentInv = Convert.ToInt32(nudObjRentPriceInv.Value);
                    break;
                case "nudObjPrice":
                    curObject.Price = Convert.ToInt32(nudObjPrice.Value);
                    break;
                case "nudObjWeight":
                    curObject.Weight = Convert.ToInt32(nudObjWeight.Value);
                    break;
                case "nudObjMaxInWorld":
                    curObject.MaxInWorld = Convert.ToInt32(nudObjMaxInWorld.Value);
                    break;
                case "nudObjMinRemorts":
                    curObject.MinimumRemorts = Convert.ToInt32(nudObjMinRemorts.Value);
                    break;
                //case "nudObjTimer":
                //    Object.Timer = Convert.ToInt32(nudObjTimer.Value);
                //    break;
                case "cboxObjMaxStructHits":
                    curObject.MaxDurab = Convert.ToInt32(GetCBoxsSelectedValue(cboxObjMaxStructHits));
                    break;
                case "nudObjCurStructHits":
                    curObject.CurrDurab = Convert.ToInt32(nudObjCurStructHits.Value);
                    break;
                case "cboxObjSkill":
                    curObject.TrenSkill = Convert.ToInt32(GetCBoxsSelectedValue(cboxObjSkill));
                    break;
                case "cboxObjMatherial":
                    curObject.Material = Convert.ToInt32(GetCBoxsSelectedValue(cboxObjMatherial));
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private void CboxObjTimerUomSelectedIndexChanged(object sender, EventArgs e)
        {
            mustApplyObjTimerChanges = false;
            if (lvMainList.SelectedItems.Count <= 0) return;
            Obj curObject = ZoneDm.Objects[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            switch (cboxObjTimerUOM.SelectedIndex)
            {
                case 0:
                    nudObjTimer.Value = curObject.Timer;
                    break;
                case 1:
                    // ReSharper disable PossibleLossOfFraction
                    nudObjTimer.Value = Math.Floor((decimal)(curObject.Timer / 60));
                    // ReSharper restore PossibleLossOfFraction
                    break;
                case 2:
                    // ReSharper disable PossibleLossOfFraction
                    nudObjTimer.Value = Math.Floor((decimal)(curObject.Timer / 1440));
                    // ReSharper restore PossibleLossOfFraction
                    break;
            }
            mustApplyObjTimerChanges = true;
        }

        private void NudObjTimerValueChanged(object sender, EventArgs e)
        {
            if (!mustApplyObjTimerChanges) return;
            if (lvMainList.SelectedItems.Count <= 0) return;
            Obj curObject = ZoneDm.Objects[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            switch (cboxObjTimerUOM.SelectedIndex)
            {
                case 0:
                    curObject.Timer = Convert.ToInt32(nudObjTimer.Value);
                    break;
                case 1:
                    curObject.Timer = Convert.ToInt32(nudObjTimer.Value * 60);
                    break;
                case 2:
                    curObject.Timer = Convert.ToInt32(nudObjTimer.Value * 1440);
                    break;
            }
        }

        private void BtnObjSetAutoCasesClick(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Obj curObject = ZoneDm.Objects[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            AutoCases ac = new AutoCases();
            bool edChislo = true;
            int gender = cboxObjectGender.SelectedIndex - 1;
            switch (gender)
            {
                case -1:
                    gender = 2;
                    break;
                case 2:
                    gender = 0;
                    edChislo = false;
                    break;
            }
            //if (tboxObjAliases.Text == "")
            {
                tboxObjAliases.Text = Utils.RemovePredlog(tboxObjImen.Text);
                curObject.Alias = tboxObjAliases.Text;
            }
            //if (tboxObjRod.Text == "")
            {
                tboxObjRod.Text = ac.Rpad(tboxObjImen.Text, edChislo, gender);
                curObject.Cases.Rod = tboxObjRod.Text;
            }
            //if (tboxObjDat.Text == "")
            {
                tboxObjDat.Text = ac.Dpad(tboxObjImen.Text, edChislo, gender);
                curObject.Cases.Dat = tboxObjDat.Text;
            }
            //if (tboxObjVin.Text == "")
            {
                tboxObjVin.Text = ac.Vpad(tboxObjImen.Text, edChislo, false, gender);
                curObject.Cases.Vin = tboxObjVin.Text;
            }
            //if (tboxObjTvor.Text == "")
            {
                tboxObjTvor.Text = ac.Tpad(tboxObjImen.Text, edChislo, gender);
                curObject.Cases.Tvor = tboxObjTvor.Text;
            }
            //if (tboxObjPredl.Text == "")
            {
                tboxObjPredl.Text = ac.Ppad(tboxObjImen.Text, edChislo, gender);
                curObject.Cases.Pred = tboxObjPredl.Text;
            }
        }

        private void TplObjEffectsValueChanged(object args)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Obj curObject = ZoneDm.Objects[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            curObject.ExctraEffects = ((string)args);
        }

        private void TplObjAffectsValueChanged(object args)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Obj curObject = ZoneDm.Objects[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            curObject.Affects = ((string)args);
        }

        private void TplObjWearToValueChanged(object args)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Obj curObject = ZoneDm.Objects[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            curObject.WearFlags = ((string)args);
        }

        private void TplObjCantTouchValueChanged(object args)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Obj curObject = ZoneDm.Objects[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            curObject.CantTouch = ((string)args);
        }

        private void TplObjCantUseValueChanged(object args)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Obj curObject = ZoneDm.Objects[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            curObject.CantUse = ((string)args);
        }

        private void BtnAddObjTriggerClick(object sender, EventArgs e)
        {
            TriggersCollection allTriggers = WindowParentForm.GetAllKnownTriggers(1);
            TrgSelectForm tsf =
                new TrgSelectForm("Выберите триггеры для объекта", allTriggers, ZoneDm.Zone.Number, true, false);
            DialogResult dres = tsf.ShowDialog();
            if (dres == DialogResult.OK)
            {
                if (lvMainList.SelectedItems.Count > 0)
                {
                    Obj curObject = ZoneDm.Objects[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
                    foreach (Trigger trigger in tsf.SelectedTriggers)
                        curObject.TriggersList.Add(trigger.VNum);
                    RefreshObjTriggersList(curObject);
                }
            }
            tsf.Dispose();
        }

        private void BtnObjRemoveTriggerClick(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Obj curObject = ZoneDm.Objects[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            foreach (ListViewItem lvi in lvObjTriggers.SelectedItems)
                curObject.TriggersList.Remove(Convert.ToInt32(lvi.Tag));
            RefreshObjTriggersList(curObject);
            if (lvObjTriggers.Items.Count <= 0) return;
            lvObjTriggers.Items[lvObjTriggers.Items.Count - 1].Selected = true;
        }

        private void LvObjTriggersKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
                BtnObjRemoveTriggerClick(null, null);
        }

        private void TsbObjAdditAffectAddClick(object sender, EventArgs e)
        {
            AddBonuses();
            if (lvObjBonuses.Items.Count <= 0) return;
            lvObjBonuses.Items[lvObjBonuses.Items.Count - 1].Selected = true;
        }

        private void lvAvailAddAffects_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            switch (lvAvailAddAffects.Sorting)
            {
                case SortOrder.None:
                    lvAvailAddAffects.Sorting = SortOrder.Descending;
                    lvAvailAddAffects.Sorting = SortOrder.Ascending;
                    lvAvailAddAffects.Sorting = SortOrder.Descending;
                    chObjAddAffectAvail.Text = "Доступные доп.аффекты v";
                    break;
                case SortOrder.Descending:
                    lvAvailAddAffects.Sorting = SortOrder.Ascending;
                    chObjAddAffectAvail.Text = "Доступные доп.аффекты ^";
                    break;
                case SortOrder.Ascending:
                    lvAvailAddAffects.Sorting = SortOrder.None;
                    chObjAddAffectAvail.Text = "Доступные доп.аффекты";
                    if (lvMainList.SelectedItems.Count <= 0) return;
                    Obj curObject = ZoneDm.Objects[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
                    lvAvailAddAffects.BeginUpdate();
                    lvAvailAddAffects.Items.Clear();
                    foreach (DataRow dr in BasesDm.Bonus.Rows)
                    {
                        Bonus b = curObject.BonusesCollection.Get(Convert.ToInt32(dr["val"]));
                        if (b != null) continue;

                        ListViewItem lvi = new ListViewItem { Text = dr["desc"].ToString(), Tag = dr["val"].ToString() };
                        string group = dr["group"].ToString();
                        ListViewGroup lvg = lvAvailAddAffects.Groups[group];
                        if (lvg == null)
                        {
                            lvg = new ListViewGroup(group, group);
                            lvAvailAddAffects.Groups.Add(lvg);
                        }
                        lvi.Group = lvg;
                        lvAvailAddAffects.Items.Add(lvi);
                    }
                    lvAvailAddAffects.EndUpdate();
                    break;
            }
        }

        private void TsbObjAdditAffectRemoveClick(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Obj curObject = ZoneDm.Objects[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            foreach (ListViewItem lvi in lvObjBonuses.SelectedItems)
                curObject.BonusesCollection.Remove(Convert.ToInt32(lvi.Tag));
            RefreshObjBonusesList(curObject);
            if (lvObjBonuses.Items.Count <= 0) return;
            lvObjBonuses.Items[lvObjBonuses.Items.Count - 1].Selected = true;
        }

        private void tsbEditAddAffectValue_Click(object sender, EventArgs e)
        {
            EditLineOnDoubleClick(sender);
        }

        private void LvObjAdditionalAffectKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
                TsbObjAdditAffectRemoveClick(null, null);
        }

        private void LvObjBonusesAfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (e.Label == null) return;
            if (e.Label.Length <= 0)
            {
                e.CancelEdit = true;
                return;
            }
            if (lvMainList.SelectedItems.Count <= 0 || !IsInteger(e.Label)) return;

            Obj curObject = ZoneDm.Objects[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            curObject.BonusesCollection.Replace(Convert.ToInt32(lvObjBonuses.Items[e.Item].Tag),
                                                Convert.ToInt32(e.Label));
        }

        private void LvAvailAddAffectsDoubleClick(object sender, EventArgs e)
        {
            AddBonuses();
        }

        public void AddBonuses()
        {
            if (lvMainList.SelectedItems.Count == 0 || lvAvailAddAffects.SelectedItems.Count == 0) return;
            Obj curObject = ZoneDm.Objects[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            if (curObject == null) return;
            foreach (ListViewItem lvi in lvAvailAddAffects.SelectedItems)
            {
                int bonus = Convert.ToInt32(lvi.Tag);
                curObject.AddBonus(bonus, 0);
            }
            RefreshObjBonusesList(curObject);
        }

        private void ToolStripButton1Click(object sender, EventArgs e)
        {
            if (lvObjBonuses.SelectedItems.Count == 0) return;
            lvObjBonuses.SelectedItems[0].BeginEdit();
        }

        private void BtnObjAddAddDescClick(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count == 0) return;
            Obj curObject = ZoneDm.Objects[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            curObject.ExtraDescriptions.Add(tboxAddDescAliases.Text, rtbObjAddDesc.Text);
            RefreshObjAddDescList(curObject);
            tboxAddDescAliases.Text = "";
            rtbObjAddDesc.Text = "";
        }

        private void BtnObjReplaceAddDescClick(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0 || lvObjAddDesc.SelectedItems.Count <= 0) return;
            Obj curObject = ZoneDm.Objects[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            ListViewItem lvi = lvObjAddDesc.SelectedItems[0];
            curObject.ExtraDescriptions.Replace(lvi.Tag.ToString(), tboxAddDescAliases.Text, rtbObjAddDesc.Text);
            RefreshObjAddDescList(curObject);
        }

        private void BtnObjRemoveAddDescClick(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Obj curObject = ZoneDm.Objects[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            foreach (ListViewItem lvi in lvObjAddDesc.SelectedItems)
                curObject.ExtraDescriptions.Remove(lvi.Tag.ToString());
            RefreshObjAddDescList(curObject);
            if (lvObjAddDesc.Items.Count <= 0) return;
            lvObjAddDesc.Items[lvObjAddDesc.Items.Count - 1].Selected = true;
        }

        private void LvObjAddDescKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
                BtnObjRemoveAddDescClick(null, null);
        }

        private void LvObjAddDescSelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvObjAddDesc.SelectedItems.Count != 1) return;
            tboxAddDescAliases.Text = lvObjAddDesc.SelectedItems[0].SubItems[0].Text;
            rtbObjAddDesc.Text = lvObjAddDesc.SelectedItems[0].SubItems[1].Text;
        }

        private void TboxObjImenKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Enter) return;
            if (lvMainList.SelectedItems.Count <= 0 || !MustUpdateTypeSpecParams) return;
            Obj curObject = ZoneDm.Objects[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            tboxObjRod.Text = tboxObjImen.Text;
            curObject.Cases.Rod = tboxObjImen.Text;
            tboxObjDat.Text = tboxObjImen.Text;
            curObject.Cases.Dat = tboxObjImen.Text;
            tboxObjTvor.Text = tboxObjImen.Text;
            curObject.Cases.Tvor = tboxObjImen.Text;
            tboxObjVin.Text = tboxObjImen.Text;
            curObject.Cases.Vin = tboxObjImen.Text;
            tboxObjPredl.Text = tboxObjImen.Text;
            curObject.Cases.Pred = tboxObjImen.Text;
        }

        private void ObjTypeSpecParamChanged(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0 || !MustUpdateTypeSpecParams) return;
            Obj curObject = ZoneDm.Objects[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            switch (((Control)sender).Name)
            {
                //Источник света
                case "nudObjLighterValue":
                    curObject.Param1 = nudObjLighterValue.Value.ToString();
                    break;
                //Магический свиток
                case "nudObjMagScrollMinLev":
                    curObject.Param1 = nudObjMagScrollMinLev.Value.ToString();
                    break;
                case "cboxObjMagScrollSpell1":
                    curObject.Param2 = GetCBoxsSelectedValue(cboxObjMagScrollSpell1).ToString();
                    break;
                case "cboxObjMagScrollSpell2":
                    curObject.Param3 = GetCBoxsSelectedValue(cboxObjMagScrollSpell2).ToString();
                    break;
                case "cboxObjMagScrollSpell3":
                    curObject.Param4 = GetCBoxsSelectedValue(cboxObjMagScrollSpell3).ToString();
                    break;
                //Волшебная палочка
                case "nudObjMagWandMinLev":
                    curObject.Param1 = nudObjMagWandMinLev.Value.ToString();
                    break;
                case "nudObjMagWandZarCnt":
                    curObject.Param2 = nudObjMagWandZarCnt.Value.ToString();
                    break;
                case "nudObjMagWandZarCntCurr":
                    curObject.Param3 = nudObjMagWandZarCntCurr.Value.ToString();
                    break;
                case "cboxObjMagWandSpell":
                    curObject.Param4 = GetCBoxsSelectedValue(cboxObjMagWandSpell).ToString();
                    break;
                //Магический посох
                case "nudObjMagStaffMinLev":
                    curObject.Param1 = nudObjMagStaffMinLev.Value.ToString();
                    break;
                case "nudObjMagStaffZarCnt":
                    curObject.Param2 = nudObjMagStaffZarCnt.Value.ToString();
                    break;
                case "nudObjMagStaffZarCntCur":
                    curObject.Param3 = nudObjMagStaffZarCntCur.Value.ToString();
                    break;
                case "cboxObjMagStaffSpell":
                    curObject.Param4 = GetCBoxsSelectedValue(cboxObjMagStaffSpell).ToString();
                    break;
                //Оружие
                case "nudObjWeaponD1":
                    curObject.Param2 = nudObjWeaponD1.Value.ToString();
                    lObjAverageDam.Text = "Ср: " +
                                          (((nudObjWeaponD1.Value * nudObjWeaponD2.Value) - nudObjWeaponD1.Value) / 2 +
                                           nudObjWeaponD1.Value);
                    break;
                case "nudObjWeaponD2":
                    curObject.Param3 = nudObjWeaponD2.Value.ToString();
                    lObjAverageDam.Text = "Ср: " +
                                          (((nudObjWeaponD1.Value * nudObjWeaponD2.Value) - nudObjWeaponD1.Value) / 2 +
                                           nudObjWeaponD1.Value);
                    break;
                case "cboxObjWeaponSrikeType":
                    curObject.Param4 = GetCBoxsSelectedValue(cboxObjWeaponSrikeType).ToString();
                    break;
                //Броня
                case "nudObjArmorAC":
                    curObject.Param1 = nudObjArmorAC.Value.ToString();
                    break;
                case "nudObjArmorArm":
                    curObject.Param2 = nudObjArmorArm.Value.ToString();
                    break;
                //Магический напиток
                case "nudObjPotionMinLev":
                    curObject.Param1 = nudObjPotionMinLev.Value.ToString();
                    break;
                case "cboxObjPotionSpell1":
                    curObject.Param2 = GetCBoxsSelectedValue(cboxObjPotionSpell1).ToString();
                    break;
                case "cboxObjPotionSpell2":
                    curObject.Param3 = GetCBoxsSelectedValue(cboxObjPotionSpell2).ToString();
                    break;
                case "cboxObjPotionSpell3":
                    curObject.Param4 = GetCBoxsSelectedValue(cboxObjPotionSpell3).ToString();
                    break;
                //Контейнер
                case "nudObjContainerValue":
                    curObject.Param1 = nudObjContainerValue.Value.ToString();
                    break;
                case "nudObjContainerKeyVNum":
                    curObject.Param3 = nudObjContainerKeyVNum.Value.ToString();
                    break;
                case "nudObjLockVal":
                    curObject.Param4 = nudObjLockVal.Value.ToString();
                    break;
                case "lvObjContainerFlags":
                    int param = 0;
                    foreach (ListViewItem lvi in lvObjContainerFlags.CheckedItems)
                        param += Convert.ToInt32(lvi.Tag);
                    curObject.Param2 = param.ToString();
                    break;
                //Контейнер для жидкостей
                case "nudObjLiquidContainerMaxVal":
                    curObject.Param1 = nudObjLiquidContainerMaxVal.Value.ToString();
                    break;
                case "nudObjLiquidContainerCurVal":
                    curObject.Param2 = nudObjLiquidContainerCurVal.Value.ToString();
                    break;
                case "cboxObjLiquidContainerDrinkType":
                    curObject.Param3 = GetCBoxsSelectedValue(cboxObjLiquidContainerDrinkType).ToString();
                    btnSelectPotionProtoVNum.Visible =
                        (Convert.ToInt32(GetCBoxsSelectedValue(cboxObjLiquidContainerDrinkType)) >= 16);
                    nudPotionProtoVNum.Visible = btnSelectPotionProtoVNum.Visible;
                    if (!nudPotionProtoVNum.Visible)
                        nudPotionProtoVNum.Value = 0;
                    break;
                case "nudObjLiquidContainerPoison":
                    curObject.Param4 = nudObjLiquidContainerPoison.Value.ToString();
                    break;
                case "nudPotionProtoVNum":
                    curObject.TrenSkill = Convert.ToInt32(nudPotionProtoVNum.Value); //Хранится вместо тренируемого скила
                    break;
                //Корм
                case "nudObjFoodVal":
                    curObject.Param1 = nudObjFoodVal.Value.ToString();
                    break;
                case "nudObjFoodPoison":
                    curObject.Param2 = nudObjFoodPoison.Value.ToString();
                    break;
                //Бабло
                case "nudObjMoneyValue":
                    curObject.Param1 = nudObjMoneyValue.Value.ToString();
                    break;
                //Фонтан
                case "nudObjFontanMaxVal":
                    curObject.Param1 = nudObjFontanMaxVal.Value.ToString();
                    break;
                case "nudObjFontanCurVal":
                    curObject.Param2 = nudObjFontanCurVal.Value.ToString();
                    break;
                case "cboxMoneyCurrency":
                    curObject.Param2 = GetCBoxsSelectedValue(cboxMoneyCurrency).ToString();
                    break;
                case "cboxObjFontanDrinkType":
                    curObject.Param3 = GetCBoxsSelectedValue(cboxObjFontanDrinkType).ToString();
                    btnSelectFontPorionProto.Visible = Convert.ToInt32(GetCBoxsSelectedValue(cboxObjFontanDrinkType)) >=
                                                       16;
                    nudFontPorionProtoVNum.Visible = btnSelectFontPorionProto.Visible;
                    if (!nudFontPorionProtoVNum.Visible)
                        nudFontPorionProtoVNum.Value = 0;
                    break;
                case "nudObjFontanPoison":
                    curObject.Param4 = nudObjFontanPoison.Value.ToString();
                    break;
                case "nudFontPorionProtoVNum":
                    curObject.TrenSkill = Convert.ToInt32(nudFontPorionProtoVNum.Value);
                    //Хранится вместо тренируемого скила
                    break;
                //Магическая книга
                case "cboxObjMagBookSpell":
                    curObject.Param1 = GetCBoxsSelectedValue(cboxObjMagBookSpell).ToString();
                    break;
                //Магический ингредиент
                case "lvObjMagIngrFlags":
                    curObject.MagicFlags = "";
                    foreach (ListViewItem lvi in lvObjMagIngrFlags.CheckedItems)
                        curObject.MagicFlags += lvi.Tag.ToString();
                    break;
                case "nudObjMagIngrLag":
                case "nudObjMagIngrMinLev":
                    curObject.Param1 =
                        CombineLagAndLevel(Convert.ToInt32(nudObjMagIngrLag.Value),
                                           Convert.ToInt32(nudObjMagIngrMinLev.Value)).ToString();
                    break;
                case "nudObjMagIngrPrototype":
                    curObject.Param2 = nudObjMagIngrPrototype.Value.ToString();
                    break;
                case "nudObjMagIngrUseRemain":
                    curObject.Param3 = nudObjMagIngrUseRemain.Value.ToString();
                    break;
                //Бинт
                case "nudObjBandageValue":
                    curObject.Param1 = nudObjBandageValue.Value.ToString();
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private void LvSkillBonusesKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
                TsbRemoveSkillBonusClick(null, null);
        }

        private void TsbAddSkillBonusClick(object sender, EventArgs e)
        {
            AddSkillBonusesToObject();
        }

        private void LvAvailSkillBonusesDoubleClick(object sender, EventArgs e)
        {
            AddSkillBonusesToObject();
        }

        private void TsbRemoveSkillBonusClick(object sender, EventArgs e)
        {
            Obj curObject = CurrentObject;
            if (CurrentObject == null) return;
            foreach (ListViewItem lvi in lvSkillBonuses.SelectedItems)
                curObject.SkillBonusesCollection.Remove(Convert.ToInt32(lvi.Tag));
            RefreshObjSkillBonusesList(curObject);
            if (lvSkillBonuses.Items.Count <= 0) return;
            lvSkillBonuses.Items[lvSkillBonuses.Items.Count - 1].Selected = true;
        }

        private void LvSkillBonusesAfterLabelEdit(object sender, LabelEditEventArgs e)
        {

            if (e.Label == null) return;
            if (e.Label.Length <= 0)
            {
                e.CancelEdit = true;
                return;
            }
            int val = Convert.ToInt32(e.Label);
            if (val > 200 || val < -200)
            {
                MessageBox.Show("Значение бонуса к уровню владения умением должно быть в пределах от -200 до +200!",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.CancelEdit = true;
            }
            if (!IsInteger(e.Label)) return;
            Obj curObject = CurrentObject;
            if (curObject == null) return;
            curObject.SkillBonusesCollection.Replace(Convert.ToInt32(lvSkillBonuses.Items[e.Item].Tag),
                                                 val);
        }

        public void AddSkillBonusesToObject()
        {
            if (lvAvailSkillBonuses.SelectedItems.Count == 0) return;
            Obj curObject = CurrentObject;
            if (curObject == null) return;
            foreach (ListViewItem lvi in lvAvailSkillBonuses.SelectedItems)
                curObject.AddSkillBonus(Convert.ToInt32(lvi.Tag), 0);
            RefreshObjSkillBonusesList(curObject);
        }

        private void tsbEditSkillBonus_Click(object sender, EventArgs e)
        {
            EditLineOnDoubleClick(sender);
        }

        private static void EditLineOnDoubleClick(object sender)
        {
            ListView lv = sender as ListView;
            if (lv == null) return;
            if (lv.SelectedItems.Count > 0)
                lv.SelectedItems[0].BeginEdit();

        }

        private void LvObjBonusesMouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvObjBonuses.SelectedItems.Count > 0)
                lvObjBonuses.SelectedItems[0].BeginEdit();
        }

        private void LvSkillBonusesMouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvSkillBonuses.SelectedItems.Count > 0)
                lvSkillBonuses.SelectedItems[0].BeginEdit();
        }

        #region Refresh

        internal void RefreshObjectsList()
        {
            lvMainList.BeginUpdate();
            lvMainList.Items.Clear();
            foreach (Obj obj in ZoneDm.Objects)
            {
                ListViewItem lvi = new ListViewItem(new[] { obj.VNum.ToString(), obj.Cases.Imen }) { Tag = obj.VNum };
                if (obj.Modifyed)
                    //lvi.BackColor = Color.FromArgb(255, 234, 234);
                    lvi.ImageIndex = 47;
                if (tboxMainListFilter.Text.Length > 0)
                {
                    if (
                        (obj.Cases.Imen.ToUpper() + obj.VNum.ToString().ToUpper()).IndexOf(
                            tboxMainListFilter.Text.ToUpper()) >= 0)
                        lvMainList.Items.Add(lvi);
                }
                else
                    lvMainList.Items.Add(lvi);
            }
            lvMainList.EndUpdate();
        }

        internal void RefreshSpecParams(Object obj)
        {
            switch (cboxObjType.SelectedIndex)
            {
                case 0: //Источник света
                    SetNumericParam(nudObjLighterValue, obj.Param1);
                    pObjLighter.Visible = true;
                    break;
                case 1: //Магический свиток
                    SetNumericParam(nudObjMagScrollMinLev, obj.Param1);
                    SetCBoxsSelectedItem(cboxObjMagScrollSpell1, obj.Param2);
                    SetCBoxsSelectedItem(cboxObjMagScrollSpell2, obj.Param3);
                    SetCBoxsSelectedItem(cboxObjMagScrollSpell3, obj.Param4);
                    pObjMagicScroll.Visible = true;
                    tboxObjActionDesc.Visible = true;
                    label35.Visible = true;
                    break;
                case 2: //Волшебная палочка
                    SetNumericParam(nudObjMagWandMinLev, obj.Param1);
                    SetNumericParam(nudObjMagWandZarCnt, obj.Param2);
                    SetNumericParam(nudObjMagWandZarCntCurr, obj.Param3);
                    SetCBoxsSelectedItem(cboxObjMagWandSpell, obj.Param4);
                    pObjMagWand.Visible = true;
                    tboxObjActionDesc.Visible = true;
                    label35.Visible = true;
                    break;
                case 3: //Магический посох
                    SetNumericParam(nudObjMagStaffMinLev, obj.Param1);
                    SetNumericParam(nudObjMagStaffZarCnt, obj.Param2);
                    SetNumericParam(nudObjMagStaffZarCntCur, obj.Param3);
                    SetCBoxsSelectedItem(cboxObjMagStaffSpell, obj.Param4);
                    pObjMagStaff.Visible = true;
                    tboxObjActionDesc.Visible = true;
                    label35.Visible = true;
                    break;
                case 4: //Оружие
                    SetNumericParam(nudObjWeaponD1, obj.Param2);
                    SetNumericParam(nudObjWeaponD2, obj.Param3);
                    lObjAverageDam.Text = "Ср: " +
                                               (((nudObjWeaponD1.Value * nudObjWeaponD2.Value) -
                                                 nudObjWeaponD1.Value) / 2 + nudObjWeaponD1.Value);
                    SetCBoxsSelectedItem(cboxObjWeaponSrikeType, obj.Param4);
                    pObjWeapon.Visible = true;
                    break;
                case 8: //Броня
                    SetNumericParam(nudObjArmorAC, obj.Param1);
                    SetNumericParam(nudObjArmorArm, obj.Param2);
                    pObjArmor.Visible = true;
                    break;
                case 9: //Магический напиток
                    SetNumericParam(nudObjPotionMinLev, obj.Param1);
                    SetCBoxsSelectedItem(cboxObjPotionSpell1, obj.Param2);
                    SetCBoxsSelectedItem(cboxObjPotionSpell2, obj.Param3);
                    SetCBoxsSelectedItem(cboxObjPotionSpell3, obj.Param4);
                    pObjPotion.Visible = true;
                    tboxObjActionDesc.Visible = true;
                    label35.Visible = true;
                    break;
                case 14: //Контейнер
                    SetNumericParam(nudObjContainerValue, obj.Param1);
                    SetNumericParam(nudObjLockVal, obj.Param4);
                    SetNumericParam(nudObjContainerKeyVNum, obj.Param3);
                    int param = Convert.ToInt32(obj.Param2);
                    if ((param & 1) == 1)
                        SetListViewItemChecked(lvObjContainerFlags, 1, true);
                    else
                        SetListViewItemChecked(lvObjContainerFlags, 1, false);
                    if ((param & 2) == 2)
                        SetListViewItemChecked(lvObjContainerFlags, 2, true);
                    else
                        SetListViewItemChecked(lvObjContainerFlags, 2, false);
                    if ((param & 4) == 4)
                        SetListViewItemChecked(lvObjContainerFlags, 4, true);
                    else
                        SetListViewItemChecked(lvObjContainerFlags, 4, false);
                    if ((param & 8) == 8)
                        SetListViewItemChecked(lvObjContainerFlags, 8, true);
                    else
                        SetListViewItemChecked(lvObjContainerFlags, 8, false);
                    pObjContainer.Visible = true;
                    break;
                case 16: //Контейнер для жидкостей
                    SetNumericParam(nudObjLiquidContainerMaxVal, obj.Param1);
                    SetNumericParam(nudObjLiquidContainerCurVal, obj.Param2);
                    SetCBoxsSelectedItem(cboxObjLiquidContainerDrinkType, obj.Param3);
                    SetNumericParam(nudObjLiquidContainerPoison, obj.Param4);
                    //Хранится вместо тренируемого скила
                    //cboxObjLiquidContainerDrink - vnum зелья, например некоторое зелье из какой-то зоны
                    nudPotionProtoVNum.Value = obj.TrenSkill;
                    cboxObjSkill.Enabled = false;
                    pObjLiquidContainer.Visible = true;
                    break;
                case 18: //Корм
                    SetNumericParam(nudObjFoodVal, obj.Param1);
                    SetNumericParam(nudObjFoodPoison, obj.Param2);
                    pObjFood.Visible = true;
                    break;
                case 19: //Бабло
                    SetNumericParam(nudObjMoneyValue, obj.Param1);
                    SetCBoxsSelectedItem(cboxMoneyCurrency, obj.Param2);
                    pObjMoney.Visible = true;
                    break;
                case 22: //Фонтан
                    SetNumericParam(nudObjFontanMaxVal, obj.Param1);
                    SetNumericParam(nudObjFontanCurVal, obj.Param2);
                    SetCBoxsSelectedItem(cboxObjFontanDrinkType, obj.Param3);
                    SetNumericParam(nudObjFontanPoison, obj.Param4);
                    pObjFontan.Visible = true;
                    break;
                case 23: //Магическая книга
                    SetCBoxsSelectedItem(cboxObjMagBookSpell, obj.Param2);
                    pObjMagBook.Visible = true;
                    break;
                case 24: //Магический ингредиент
                    foreach (ListViewItem lvi in lvObjMagIngrFlags.Items)
                        lvi.Checked = obj.MagicFlags.ToLower().Contains(lvi.Tag.ToString().ToLower());
                    string param1 = obj.Param1;
                    if (param1 == "")
                        param1 = "0";
                    string param2 = obj.Param2;
                    if (param2 == "")
                        param2 = "0";
                    string param3 = obj.Param3;
                    if (param3 == "")
                        param3 = "0";
                    SetNumericParam(nudObjMagIngrLag, GetLag(Convert.ToInt32(param1)));
                    SetNumericParam(nudObjMagIngrMinLev, GetLevel(Convert.ToInt32(param1)));
                    SetNumericParam(nudObjMagIngrPrototype, param2);
                    SetNumericParam(nudObjMagIngrUseRemain, param3);
                    pObjMagIngr.Visible = true;
                    break;
                case 25: //Ингридиент для отвара
                    break;
                case 26: //Бинт
                    SetNumericParam(nudObjBandageValue, obj.Param1);
                    pObjBandage.Visible = true;
                    break;
            }
        }

        internal void RefreshDefaultSpecParams(Object obj)
        {
            switch (cboxObjType.SelectedIndex)
            {
                case 0: //Источник света
                    /*SetNumericParam(nudObjLighterValue, Object.Param1);
                    pObjLighter.Visible = true;*/
                    break;
                case 1: //Магический свиток
                    /*SetNumericParam(nudObjMagScrollMinLev, Object.Param1);
                    SetCBoxsSelectedItem(cboxObjMagScrollSpell1, Object.Param2);
                    SetCBoxsSelectedItem(cboxObjMagScrollSpell2, Object.Param3);
                    SetCBoxsSelectedItem(cboxObjMagScrollSpell3, Object.Param4);
                    pObjMagicScroll.Visible = true;
                    tboxObjActionDesc.Visible = true;
                    label35.Visible = true;*/
                    break;
                case 2: //Волшебная палочка
                    /*SetNumericParam(nudObjMagWandMinLev, Object.Param1);
                    SetNumericParam(nudObjMagWandZarCnt, Object.Param2);
                    SetNumericParam(nudObjMagWandZarCntCurr, Object.Param3);
                    SetCBoxsSelectedItem(cboxObjMagWandSpell, Object.Param4);
                    pObjMagWand.Visible = true;
                    tboxObjActionDesc.Visible = true;
                    label35.Visible = true;*/
                    break;
                case 3: //Магический посох
                    /*SetNumericParam(nudObjMagStaffMinLev, Object.Param1);
                    SetNumericParam(nudObjMagStaffZarCnt, Object.Param2);
                    SetNumericParam(nudObjMagStaffZarCntCur, Object.Param3);
                    SetCBoxsSelectedItem(cboxObjMagStaffSpell, Object.Param4);
                    pObjMagStaff.Visible = true;
                    tboxObjActionDesc.Visible = true;
                    label35.Visible = true;*/
                    break;
                case 4: //Оружие
                    /*SetNumericParam(nudObjWeaponD1, Object.Param2);
                    SetNumericParam(nudObjWeaponD2, Object.Param3);
                    lObjAverageDam.Text = "Ср: " + (((nudObjWeaponD1.Value * nudObjWeaponD2.Value) - nudObjWeaponD1.Value) / 2 + nudObjWeaponD1.Value).ToString();
                    SetCBoxsSelectedItem(cboxObjWeaponSrikeType, Object.Param4);
                    pObjWeapon.Visible = true;*/
                    break;
                case 8: //Броня
                    /*SetNumericParam(nudObjArmorAC, Object.Param1);
                    SetNumericParam(nudObjArmorArm, Object.Param2);
                    pObjArmor.Visible = true;*/
                    break;
                case 9: //Магический напиток
                    /*SetNumericParam(nudObjPotionMinLev, Object.Param1);
                    SetCBoxsSelectedItem(cboxObjPotionSpell1, Object.Param2);
                    SetCBoxsSelectedItem(cboxObjPotionSpell2, Object.Param3);
                    SetCBoxsSelectedItem(cboxObjPotionSpell3, Object.Param4);
                    pObjPotion.Visible = true;
                    tboxObjActionDesc.Visible = true;
                    label35.Visible = true;*/
                    break;
                case 14: //Контейнер
                    obj.Param2 = "0";
                    obj.Param1 = "0";
                    obj.Param3 = "-1";
                    //int Param = Convert.ToInt32(Object.Param2);
                    break;
                case 16: //Контейнер для жидкостей
                    /*SetNumericParam(nudObjLiquidContainerMaxVal, Object.Param1);
                    SetNumericParam(nudObjLiquidContainerCurVal, Object.Param2);
                    SetCBoxsSelectedItem(cboxObjLiquidContainerDrinkType, Object.Param3);
                    SetNumericParam(nudObjLiquidContainerPoison, Object.Param4);
                    //Хранится вместо тренируемого скила
                    //cboxObjLiquidContainerDrink - vnum зелья, например некоторое зелье из какой-то зоны
                    nudPotionProtoVNum.Value = Object.TrenSkill;
                    cboxObjSkill.Enabled = false;
                    pObjLiquidContainer.Visible = true;*/
                    break;
                case 18: //Корм
                    /*SetNumericParam(nudObjFoodVal, Object.Param1);
                    SetNumericParam(nudObjFoodPoison, Object.Param2);
                    pObjFood.Visible = true;*/
                    break;
                case 19: //Бабло
                    /*SetNumericParam(nudObjMoneyValue, Object.Param1);
                    pObjMoney.Visible = true;*/
                    break;
                case 22: //Фонтан
                    /*SetNumericParam(nudObjFontanMaxVal, Object.Param1);
                    SetNumericParam(nudObjFontanCurVal, Object.Param2);
                    SetCBoxsSelectedItem(cboxObjFontanDrinkType, Object.Param3);
                    SetNumericParam(nudObjFontanPoison, Object.Param4);
                    pObjFontan.Visible = true;*/
                    break;
                case 23: //Магическая книга
                    /*SetCBoxsSelectedItem(cboxObjMagBookSpell, Object.Param2);
                    pObjMagBook.Visible = true;*/
                    break;
                case 24: //Магический ингредиент
                    /*foreach (ListViewItem lvi in lvObjMagIngrFlags.Items)
                    {
                        if (Object.MagicFlags.ToLower().Contains(lvi.Tag.ToString().ToLower()))
                            lvi.Checked = true;
                        else
                            lvi.Checked = false;
                    }
                    string Param1 = Object.Param1;
                    if (Param1 == "")
                        Param1 = "0";
                    string Param2 = Object.Param2;
                    if (Param2 == "")
                        Param2 = "0";
                    string Param3 = Object.Param3;
                    if (Param3 == "")
                        Param3 = "0";
                    SetNumericParam(nudObjMagIngrLag, GetLag(Convert.ToInt32(Param1)));
                    SetNumericParam(nudObjMagIngrMinLev, GetLevel(Convert.ToInt32(Param1)));
                    SetNumericParam(nudObjMagIngrPrototype, Param2);
                    SetNumericParam(nudObjMagIngrUseRemain, Param3);
                    pObjMagIngr.Visible = true;*/
                    break;
            }
            RefreshSpecParams(obj);
        }

        internal void RefreshObjEffectsList(Object obj)
        {
            tplObjEffects.SetData(obj.ExctraEffects, BasesDm.ExtraEffect);
        }

        internal void RefreshObjAffectsList(Object obj)
        {
            tplObjAffects.SetData(obj.Affects, BasesDm.Affect);
        }

        internal void RefreshObjWearToList(Object obj)
        {
            tplObjWearTo.SetData(obj.WearFlags, BasesDm.Wear);
        }

        internal void RefreshObjCantTouchList(Object obj)
        {
            tplObjCantTouch.SetData(obj.CantTouch, BasesDm.NoTake);
        }

        internal void RefreshObjCantUseList(Object obj)
        {
            tplObjCantUse.SetData(obj.CantUse, BasesDm.NoUse);
        }

        public void RefreshObjTriggersList(Object obj)
        {
            lvObjTriggers.Items.Clear();
            TriggersCollection allTriggers = WindowParentForm.GetAllKnownTriggers(1);
            foreach (int vNum in obj.TriggersList)
            {
                Trigger t = allTriggers.GetTrigger(vNum);
                string triggerName = (t != null) ? t.Name : "Триггер из незагруженной зоны";
                //string TriggerName = AllTriggers.GetTrigger(VNum).Name;
                ListViewItem lvi = new ListViewItem(new[] { vNum.ToString(), triggerName }) { Tag = vNum };
                lvObjTriggers.Items.Add(lvi);
            }
        }

        internal void RefreshObjBonusesList(Object trgObject)
        {
            lvAvailAddAffects.BeginUpdate();
            lvObjBonuses.BeginUpdate();
            lvAvailAddAffects.Items.Clear();
            lvAvailAddAffects.Groups.Clear();
            lvObjBonuses.Items.Clear();
            foreach (Bonus bonus in trgObject.BonusesCollection)
            {
                ListViewItem lvi =
                    new ListViewItem(new[] { bonus.Value.ToString(), BasesDm.GetBonusNameByNum(bonus.VNum) }) { Tag = bonus.VNum };
                lvObjBonuses.Items.Add(lvi);
            }
            foreach (DataRow dr in BasesDm.Bonus.Rows)
            {
                Bonus b = trgObject.BonusesCollection.Get(Convert.ToInt32(dr["val"]));
                if (b != null) continue;

                ListViewItem lvi = new ListViewItem { Text = dr["desc"].ToString(), Tag = dr["val"].ToString() };
                string group = dr["group"].ToString();
                ListViewGroup lvg = lvAvailAddAffects.Groups[group];
                if (lvg == null)
                {
                    lvg = new ListViewGroup(group, group);
                    lvAvailAddAffects.Groups.Add(lvg);
                }
                lvi.Group = lvg;
                lvAvailAddAffects.Items.Add(lvi);
            }
            lvAvailAddAffects.EndUpdate();
            lvObjBonuses.EndUpdate();
        }

        private void lvAvailSkillBonuses_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            switch (lvAvailSkillBonuses.Sorting)
            {
                case SortOrder.None:
                    lvAvailSkillBonuses.Sorting = SortOrder.Descending;
                    lvAvailSkillBonuses.Sorting = SortOrder.Ascending;
                    lvAvailSkillBonuses.Sorting = SortOrder.Descending;
                    chObjAddSkillAvail.Text = "Доступные умения v";
                    break;
                case SortOrder.Descending:
                    lvAvailSkillBonuses.Sorting = SortOrder.Ascending;
                    chObjAddSkillAvail.Text = "Доступные умения ^";
                    break;
                case SortOrder.Ascending:
                    lvAvailSkillBonuses.Sorting = SortOrder.None;
                    chObjAddSkillAvail.Text = "Доступные умения";
                    lvAvailSkillBonuses.BeginUpdate();
                    lvAvailSkillBonuses.Items.Clear();
                    lvAvailSkillBonuses.Groups.Clear();
                    Obj curObject = CurrentObject;
                    if (CurrentObject == null) return;
                    foreach (DataRow dr in BasesDm.CharSkills.Rows)
                    {
                        Bonus b = curObject.SkillBonusesCollection.Get(Convert.ToInt32(dr["val"]));
                        if (b != null) continue;

                        ListViewItem lvi = new ListViewItem { Text = dr["desc"].ToString(), Tag = dr["val"].ToString() };
                        string group = dr["group"].ToString();
                        ListViewGroup lvg = lvAvailSkillBonuses.Groups[group];
                        if (lvg == null)
                        {
                            lvg = new ListViewGroup(group, group);
                            lvAvailSkillBonuses.Groups.Add(lvg);
                        }
                        lvi.Group = lvg;
                        lvAvailSkillBonuses.Items.Add(lvi);
                    }
                    lvAvailSkillBonuses.EndUpdate();
                    break;
            }
        }

        internal void RefreshObjSkillBonusesList(Object trgObject)
        {
            lvAvailSkillBonuses.BeginUpdate();
            lvSkillBonuses.BeginUpdate();
            lvAvailSkillBonuses.Items.Clear();
            lvAvailSkillBonuses.Groups.Clear();
            lvSkillBonuses.Items.Clear();
            foreach (Bonus bonus in trgObject.SkillBonusesCollection)
            {
                ListViewItem lvi =
                    new ListViewItem(new[] { bonus.Value.ToString(), BasesDm.GetSkillBonusNameByNum(bonus.VNum) }) { Tag = bonus.VNum };
                lvSkillBonuses.Items.Add(lvi);
            }
            foreach (DataRow dr in BasesDm.CharSkills.Rows)
            {
                Bonus b = trgObject.SkillBonusesCollection.Get(Convert.ToInt32(dr["val"]));
                if (b != null) continue;

                ListViewItem lvi = new ListViewItem { Text = dr["desc"].ToString(), Tag = dr["val"].ToString() };
                string group = dr["group"].ToString();
                ListViewGroup lvg = lvAvailSkillBonuses.Groups[group];
                if (lvg == null)
                {
                    lvg = new ListViewGroup(group, group);
                    lvAvailSkillBonuses.Groups.Add(lvg);
                }
                lvi.Group = lvg;
                lvAvailSkillBonuses.Items.Add(lvi);
            }
            lvAvailSkillBonuses.EndUpdate();
            lvSkillBonuses.EndUpdate();
        }

        internal void RefreshObjAddDescList(Object trgObject)
        {
            lvObjAddDesc.Items.Clear();
            foreach (ExtraDesc extraDesc in trgObject.ExtraDescriptions)
            {
                ListViewItem lvi = new ListViewItem(new[] { extraDesc.Aliases, extraDesc.Description }) { Tag = extraDesc.Aliases };
                lvObjAddDesc.Items.Add(lvi);
            }
        }

        internal void RefreshObjectData()
        {
            MustUpdateObjData = false;
            Obj curObject = ZoneDm.Objects[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            if (curObject == null) return;
            SetCBoxsSelectedItem(cboxObjectGender, curObject.Sex);
            MustRefreshTypeSpecParams = false;
            cboxObjType.SelectedIndex = curObject.Type - 1;
            MustRefreshTypeSpecParams = true;
            RefreshSpecParams(curObject);
            if (curObject.Type - 1 != 16)
                SetCBoxsSelectedItem(cboxObjSkill, curObject.TrenSkill);
            SetCBoxsSelectedItem(cboxObjMatherial, curObject.Material);
            SetCBoxsSelectedItem(cboxObjMaxStructHits, curObject.MaxDurab);
            nudObjCurStructHits.Value = curObject.CurrDurab;

            tboxObjAliases.Text = curObject.Alias;
            tboxObjImen.Text = curObject.Cases.Imen;
            tboxObjRod.Text = curObject.Cases.Rod;
            tboxObjDat.Text = curObject.Cases.Dat;
            tboxObjVin.Text = curObject.Cases.Vin;
            tboxObjTvor.Text = curObject.Cases.Tvor;
            tboxObjPredl.Text = curObject.Cases.Pred;
            tboxObjDesc.Text = curObject.Desc;
            tboxObjActionDesc.Text = curObject.ActionDesc;

            RefreshObjectTabsData(curObject);

            RefreshDetailsAndLocations(curObject);

            LastSelectedObj = curObject.VNum;

            MustUpdateObjData = true;
        }

        internal void RefreshSelectedObjectTabData()
        {
            if (lvMainList.SelectedItems.Count == 0) return;
            Obj obj = ZoneDm.Objects[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            if (obj == null) return;
            MustUpdateObjData = false;
            RefreshObjectTabsData(obj);
            MustUpdateObjData = true;
        }

        private void RefreshObjectTabsData(Object obj)
        {
            switch (tcObject.SelectedTab.Name)
            {
                case "tpObjParams":
                    nudObjRentPriceEquip.Value = obj.RentWear;
                    nudObjRentPriceInv.Value = obj.RentInv;
                    nudObjPrice.Value = obj.Price;
                    nudObjWeight.Value = obj.Weight;
                    nudObjMaxInWorld.Value = obj.MaxInWorld;
                    nudObjMinRemorts.Value = obj.MinimumRemorts;
                    //Тут обработку для автовыбора единиц измерения
                    if (obj.Timer > 1440)
                    {
                        cboxObjTimerUOM.SelectedIndex = 2;
                        // ReSharper disable PossibleLossOfFraction
                        nudObjTimer.Value = Math.Floor((decimal)(obj.Timer / 1440));
                        // ReSharper restore PossibleLossOfFraction
                    }
                    else if (obj.Timer > 60)
                    {
                        cboxObjTimerUOM.SelectedIndex = 1;
                        // ReSharper disable PossibleLossOfFraction
                        nudObjTimer.Value = Math.Floor((decimal)(obj.Timer / 60));
                        // ReSharper restore PossibleLossOfFraction
                    }
                    else
                    {
                        cboxObjTimerUOM.SelectedIndex = 0;
                        nudObjTimer.Value = obj.Timer;
                    }
                    //nudObjTimer.Value = Object.Timer;
                    break;
                case "tpObjEffects":
                    RefreshObjEffectsList(obj);
                    break;
                case "tpObjAffects":
                    RefreshObjAffectsList(obj);
                    break;
                case "tpObjWearTo":
                    RefreshObjWearToList(obj);
                    break;
                case "tpObjCantTouch":
                    RefreshObjCantTouchList(obj);
                    break;
                case "tpObjCantUse":
                    RefreshObjCantUseList(obj);
                    break;
                case "tpObjTriggers":
                    RefreshObjTriggersList(obj);
                    break;
                case "tpObjAddDescs":
                    RefreshObjAddDescList(obj);
                    break;
                case "tpObjAddAffects":
                    RefreshObjBonusesList(obj);
                    break;
                case "tpObjSkillBonus":
                    RefreshObjSkillBonusesList(obj);
                    break;
            }
        }

        #endregion

        internal void RefreshDetailsAndLocations(Object curObject)
        {
            lvDetails.BeginUpdate();
            ClearDetails();
            wldMap.HighlightedRooms.Clear();
            lvDetails.Groups.Add(new ListViewGroup("Загружается в комнаты", HorizontalAlignment.Left));
            lvDetails.Groups.Add(new ListViewGroup("Помещается в контейнеры в комнатах", HorizontalAlignment.Left));
            foreach (Room r in ZoneDm.Rooms)
            {
                foreach (OperatedObj lo in r.LoadingObjectsCollection)
                {
                    if (lo.VNum == curObject.VNum)
                    {
                        lvDetails.Items.Add(new ExListViewItem("[" + r.VNum + "] " + r.Name + " <" + lo.Probability + "%>")
                        {
                            Tag = r.VNum,
                            Action = ActionType.GoToRoomLoadedObjects,
                            Group = lvDetails.Groups[0]
                        });
                        wldMap.HighlightedRooms.Add(r.VNum);
                    }
                    foreach (OperatedObj lino in lo.ObjectsInObject)
                    {
                        if (lino.VNum != curObject.VNum) continue;
                        lvDetails.Items.Add(new ExListViewItem("[" + r.VNum + "] " + r.Name + ", в контейнер [" + lo.VNum + "] <" +
                                               lino.Probability + "%>")
                        {
                            Tag = r.VNum,
                            Action = ActionType.GoToRoomLoadedObjects,
                            Group = lvDetails.Groups[1]
                        });
                        wldMap.HighlightedRooms.Add(r.VNum);
                    }
                }
            }

            lvDetails.Groups.Add(new ListViewGroup("Помещается в мобов в комнатах", HorizontalAlignment.Left));
            foreach (Room r in ZoneDm.Rooms)
            {
                foreach (OperatedMob lm in r.LoadingMobsCollection)
                {
                    foreach (MobObj mo in lm.Items)
                    {
                        if (mo.VNum != curObject.VNum) continue;
                        lvDetails.Items.Add(
                            new ExListViewItem("[" + r.VNum + "] " + r.Name + ", в моба [" + lm.VNum + "] <" +
                                               mo.Probability + "%>")
                            {
                                TrgVNum = lm.VNum,
                                TrgVNum2 = mo.VNum,
                                Tag = r.VNum,
                                Action = ActionType.GoToRoomLoadedMobs,
                                Group = lvDetails.Groups[2]
                            });
                        wldMap.HighlightedRooms.Add(r.VNum);
                    }
                }
            }
            wldMap.RedrawBitmap();

            lvDetails.Groups.Add(new ListViewGroup("Удаляется из комнат", HorizontalAlignment.Left));
            foreach (Room r in ZoneDm.Rooms)
            {
                foreach (OperatedObj ro in r.RemoovingObjects)
                {
                    if (ro.VNum != curObject.VNum) continue;
                    lvDetails.Items.Add(new ExListViewItem("[" + r.VNum + "] " + r.Name)
                    {
                        Tag = r.VNum,
                        Action = ActionType.GoToRoomUnloadedObjects,
                        Group = lvDetails.Groups[3]
                    });
                }
            }

            lvDetails.Groups.Add(new ListViewGroup("Триггеры", HorizontalAlignment.Left));
            foreach (int vnum in curObject.TriggersList)
            {
                Trigger t = ZoneDm.Triggers[vnum, 0];
                if (t == null) continue;
                lvDetails.Items.Add(new ExListViewItem("[" + t.VNum + "] " + t.Name)
                {
                    Tag = t.VNum,
                    Action = ActionType.GoToTrigger,
                    Group = lvDetails.Groups[4]
                });
            }

            lvDetails.EndUpdate();
        }

        #endregion

        #region Mob

        private void tcMobs_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshSelectedMobTabData();
        }

        private void cborMobRemoveOnReload_MouseClick(object sender, MouseEventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Mob mob = ZoneDm.Mobs[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            if (cborMobRemoveOnReload.Checked)
                ZoneDm.Zone.MobsToRemove.Add(mob.VNum, false, -1);
            else
                ZoneDm.Zone.MobsToRemove.RemoveMob(mob.VNum);
        }

        private void btnMobSpellCheckCommonDesc_Click(object sender, EventArgs e)
        {
            CheckSpelling(ertbMobDescription);
        }

        private void btnMobSpecFormatCommonDesc_Click(object sender, EventArgs e)
        {
            FormatMobDescription(false);
        }

        private void btnMobFormatCommonDesc_Click(object sender, EventArgs e)
        {
            FormatMobDescription(true);
        }
        private void FormatMobDescription(bool oneParagraph)
        {
            var tf = new TextFormater(StaticData.OptimalTextWidth);
            ertbMobDescription.Text = tf.GetFormatedText(ertbMobDescription.Text, oneParagraph, cbAllowHyp.Checked, cbInsertSpaces.Checked);
            SetMobDescription();
        }

        private void btnSetAutoCases_Click(object sender, EventArgs e)
        {
            AutoCases ac = new AutoCases();
            bool edChislo = true;
            int gender = cboxMobSex.SelectedIndex - 1;
            if (gender == -1)
                gender = 2;
            else if (gender == 2)
            {
                gender = 0;
                edChislo = false;
            }
            //if (tboxMobAliases.Text == "")
            tboxMobAliases.Text = Utils.RemovePredlog(tboxMobNameImen.Text);
            //if (tboxMobNameRod.Text == "")
            tboxMobNameRod.Text = ac.Rpad(tboxMobNameImen.Text, edChislo, gender);
            //if (tboxMobNameDat.Text == "")
            tboxMobNameDat.Text = ac.Dpad(tboxMobNameImen.Text, edChislo, gender);
            //if (tboxMobNameVin.Text == "")
            tboxMobNameVin.Text = ac.Vpad(tboxMobNameImen.Text, edChislo, true, gender);
            //if (tboxMobNameTvor.Text == "")
            tboxMobNameTvor.Text = ac.Tpad(tboxMobNameImen.Text, edChislo, gender);
            //if (tboxMobNamePred.Text == "")
            tboxMobNamePred.Text = ac.Ppad(tboxMobNameImen.Text, edChislo, gender);
        }

        private void tboxMobNameImen_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Enter) return;
            if (lvMainList.SelectedItems.Count <= 0 || !MustUpdateMobData) return;
            Mob mob = ZoneDm.Mobs[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            tboxMobNameRod.Text = tboxMobNameImen.Text;
            mob.Cases.Rod = tboxMobNameImen.Text;
            tboxMobNameDat.Text = tboxMobNameImen.Text;
            mob.Cases.Dat = tboxMobNameImen.Text;
            tboxMobNameTvor.Text = tboxMobNameImen.Text;
            mob.Cases.Tvor = tboxMobNameImen.Text;
            tboxMobNameVin.Text = tboxMobNameImen.Text;
            mob.Cases.Vin = tboxMobNameImen.Text;
            tboxMobNamePred.Text = tboxMobNameImen.Text;
            mob.Cases.Pred = tboxMobNameImen.Text;
        }

        private void cmsMobDescription_Validated(object sender, EventArgs e)
        {
            SetMobDescription();
        }

        private void SetMobDescription()
        {
            if (lvMainList.SelectedItems.Count <= 0 || !MustUpdateMobData) return;
            Mob mob = ZoneDm.Mobs[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            mob.DetailDescr = ertbMobDescription.Text;
        }

        private void MobValueChanged(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0 || !MustUpdateMobData) return;
            Mob mob = ZoneDm.Mobs[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            switch (((Control)sender).Name)
            {
                case "tboxMobAliases":
                    mob.Alias = tboxMobAliases.Text;
                    break;
                case "cboxMobSex":
                    mob.Sex = Convert.ToInt32(((TaggedComboBoxItem)(cboxMobSex.SelectedItem)).Tag);
                    break;
                case "cboxMobClass":
                    mob.Class = Convert.ToInt32(((TaggedComboBoxItem)(cboxMobClass.SelectedItem)).Tag);
                    break;
                case "cboxMobRace":
                    mob.Race = Convert.ToInt32(((TaggedComboBoxItem)(cboxMobRace.SelectedItem)).Tag);
                    break;
                case "tboxMobNameImen":
                    mob.Cases.Imen = tboxMobNameImen.Text;
                    lvMainList.SelectedItems[0].SubItems[1].Text = mob.Cases.Imen;
                    break;
                case "tboxMobNameRod":
                    mob.Cases.Rod = tboxMobNameRod.Text;
                    break;
                case "tboxMobNameDat":
                    mob.Cases.Dat = tboxMobNameDat.Text;
                    break;
                case "tboxMobNameVin":
                    mob.Cases.Vin = tboxMobNameVin.Text;
                    break;
                case "tboxMobNameTvor":
                    mob.Cases.Tvor = tboxMobNameTvor.Text;
                    break;
                case "tboxMobNamePred":
                    mob.Cases.Pred = tboxMobNamePred.Text;
                    break;
                case "tboxMobDesc":
                    mob.Desc = tboxMobDesc.Text;
                    break;
                case "nudMobStr":
                    mob.Stats.Str = Convert.ToInt32(nudMobStr.Value);
                    break;
                case "nudMobInt":
                    mob.Stats.Int = Convert.ToInt32(nudMobInt.Value);
                    break;
                case "nudMobWis":
                    mob.Stats.Wis = Convert.ToInt32(nudMobWis.Value);
                    break;
                case "nudMobDex":
                    mob.Stats.Dex = Convert.ToInt32(nudMobDex.Value);
                    break;
                case "nudMobCon":
                    mob.Stats.Con = Convert.ToInt32(nudMobCon.Value);
                    break;
                case "nudMobCha":
                    mob.Stats.Cha = Convert.ToInt32(nudMobCha.Value);
                    break;
                case "nudMobLevel":
                    mob.Level = Convert.ToInt32(nudMobLevel.Value);
                    break;
                case "nudMobSize":
                    mob.Stats.Size = Convert.ToInt32(nudMobSize.Value);
                    break;
                case "nudMobHeight":
                    mob.Stats.Height = Convert.ToInt32(nudMobHeight.Value);
                    break;
                case "nudMobWeight":
                    mob.Stats.Weight = Convert.ToInt32(nudMobWeight.Value);
                    break;
                case "nudMobAC":
                    mob.Ac = Convert.ToInt32(nudMobAC.Value);
                    break;
                case "nudMobHitroll":
                    mob.Hitroll = Convert.ToInt32(nudMobHitroll.Value);
                    break;
                /*case "nudMobMaxInWorld":
                    mob.MaxInWorld = Convert.ToInt32(nudMobMaxInWorld.Value);
                    break;*/
                case "dctrlMobHP":
                    mob.Hits = dctrlMobHP.Value;
                    break;
                case "dctrlMobAttack":
                    mob.Damage = dctrlMobAttack.Value;
                    break;
                case "cboxMobAlign":
                    mob.Align = Convert.ToInt32(((TaggedComboBoxItem)(cboxMobAlign.SelectedItem)).Tag);
                    break;
                case "cboxMobAttackType":
                    mob.BareHandAttack = Convert.ToInt32(((TaggedComboBoxItem)(cboxMobAttackType.SelectedItem)).Tag);
                    break;
                case "nudMobExtraAttack":
                    mob.ExtraAttack = Convert.ToInt32(nudMobExtraAttack.Value);
                    break;
                case "nudMobLikeWork":
                    mob.LikeWork = Convert.ToInt32(nudMobLikeWork.Value);
                    break;
                case "cboxMobStartPosition":
                    mob.PosLoad = Convert.ToInt32(((TaggedComboBoxItem)(cboxMobStartPosition.SelectedItem)).Tag);
                    break;
                case "cboxMobDefPosition":
                    mob.PosDefault = Convert.ToInt32(((TaggedComboBoxItem)(cboxMobDefPosition.SelectedItem)).Tag);
                    break;
                case "nudMobExpa":
                    mob.Exp = Convert.ToInt32(nudMobExpa.Value);
                    break;
                case "nudMobMaxFactor":
                    mob.MaxFactor = Convert.ToInt32(nudMobMaxFactor.Value);
                    break;
                case "dctrlMobMoney":
                    mob.Money = dctrlMobMoney.Value;
                    break;
                case "nudSaveParalyze":
                    mob.SaveParalyzeCast = Convert.ToInt32(nudSaveParalyze.Value);
                    break;
                case "nudSaveMagDam":
                    mob.SaveMagDamages = Convert.ToInt32(nudSaveMagDam.Value);
                    break;
                case "nudSaveMagBreathe":
                    mob.SaveMagBreathes = Convert.ToInt32(nudSaveMagBreathe.Value);
                    break;
                case "nudSaveFightSkills":
                    mob.SaveFightSkills = Convert.ToInt32(nudSaveFightSkills.Value);
                    break;
                case "nudResFire":
                    mob.ResistFromFire = Convert.ToInt32(nudResFire.Value);
                    break;
                case "nudResAir":
                    mob.ResistFromAir = Convert.ToInt32(nudResAir.Value);
                    break;
                case "nudResWater":
                    mob.ResistFromWater = Convert.ToInt32(nudResWater.Value);
                    break;
                case "nudResEarth":
                    mob.ResistFromEarth = Convert.ToInt32(nudResEarth.Value);
                    break;
                case "nudVitality":
                    mob.Vitality = Convert.ToInt32(nudVitality.Value);
                    break;
                case "nudRegeneration":
                    mob.HPreg = Convert.ToInt32(nudRegeneration.Value);
                    break;
                case "nudArmour":
                    mob.Armour = Convert.ToInt32(nudArmour.Value);
                    break;
                case "nudAdsorb":
                    mob.Absorbe = Convert.ToInt32(nudAdsorb.Value);
                    break;
                case "nudMind":
                    mob.Mind = Convert.ToInt32(nudMind.Value);
                    break;
                case "nudMem":
                    mob.PlusMem = Convert.ToInt32(nudMem.Value);
                    break;
                case "nudCastSuccess":
                    mob.CastSuccess = Convert.ToInt32(nudCastSuccess.Value);
                    break;
                case "nudInitiative":
                    mob.Initiative = Convert.ToInt32(nudInitiative.Value);
                    break;
                case "nudSuccess":
                    mob.Luck = Convert.ToInt32(nudSuccess.Value);
                    break;
                case "nudImmun":
                    mob.Immunitet = Convert.ToInt32(nudImmun.Value);
                    break;
                case "nudResistDark":                    
                    mob.ResistDark = Convert.ToInt32(nudResistDark.Value);
                    break;
                case "nudAResist":
                    mob.AResist = Convert.ToInt32(nudAResist.Value);
                    break;
                case "nudMResist":
                    mob.MResist = Convert.ToInt32(nudMResist.Value);
                    break;
                case "nudPResist":
                    mob.PResist = Convert.ToInt32(nudPResist.Value);
                    break;

                default: //nudMobExpa
                    throw new NotImplementedException();
            }
        }

        private void lvMobSkills_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (e.Label == null) return;
            if (e.Label.Length <= 0)
            {
                e.CancelEdit = true;
                return;
            }
            if (lvMainList.SelectedItems.Count <= 0 || !IsInteger(e.Label)) return;
            Mob mob = ZoneDm.Mobs[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            mob.Skills.Update(Convert.ToInt32(lvMobSkills.Items[e.Item].Tag),
                              Convert.ToInt32(e.Label.Replace("%", "")));
            RefreshMobSkillsList(mob);
        }

        private void tsbMobEditSkill_Click(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0 || lvMobSkills.SelectedItems.Count <= 0) return;
            lvMobSkills.SelectedItems[0].BeginEdit();
        }

        private void tsbMobRemoveSkill_Click(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Mob mob = ZoneDm.Mobs[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            foreach (ListViewItem lvi in lvMobSkills.SelectedItems)
                mob.Skills.Remove(Convert.ToInt32(lvi.Tag));
            RefreshMobSkillsList(mob);
            if (lvMobSkills.Items.Count <= 0) return;
            lvMobSkills.Items[lvMobSkills.Items.Count - 1].Selected = true;
        }

        private void tsbMobAddSkill_Click(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Mob mob = ZoneDm.Mobs[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            foreach (ListViewItem lvi in lvAvailMobSkills.SelectedItems)
                mob.Skills.Add(Convert.ToInt32(lvi.Tag.ToString()), 0);
            RefreshMobSkillsList(mob);
            if (lvMobSkills.Items.Count <= 0) return;
            lvMobSkills.Items[lvMobSkills.Items.Count - 1].Selected = true;
        }

        private void lvMobSkills_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
                tsbMobRemoveSkill_Click(null, null);
        }

        private void lvMobSkills_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0 || lvMobSkills.SelectedItems.Count <= 0) return;
            lvMobSkills.SelectedItems[0].BeginEdit();
        }

        private void lvAvailMobSkills_DoubleClick(object sender, EventArgs e)
        {
            tsbMobAddSkill_Click(null, null);
        }

        private void lvAvailMobSkills_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            switch (lvAvailMobSkills.Sorting)
            {
                case SortOrder.None:
                    lvAvailMobSkills.Sorting = SortOrder.Ascending;
                    chMobSkillAvail.Text = "Доступные умения v";
                    break;
                case SortOrder.Ascending:
                    lvAvailMobSkills.Sorting = SortOrder.Descending;
                    chMobSkillAvail.Text = "Доступные умения ^";
                    break;
                case SortOrder.Descending:
                    lvAvailMobSkills.Sorting = SortOrder.None;
                    chMobSkillAvail.Text = "Доступные умения";
                    if (lvMainList.SelectedItems.Count <= 0) break;
                    lvAvailMobSkills.BeginUpdate();
                    lvAvailMobSkills.Items.Clear();
                    Mob mob = ZoneDm.Mobs[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
                    foreach (DataRow dr in BasesDm.MobSkills.Rows)
                    {
                        MobSkill skill = mob.Skills.Get(Convert.ToInt32(dr["val"]));
                        if (skill != null) continue;
                        ListViewItem lvi = new ListViewItem { Text = dr["desc"].ToString(), Tag = dr["val"].ToString() };
                        lvAvailMobSkills.Items.Add(lvi);
                    }
                    lvAvailMobSkills.EndUpdate();
                    break;
            }
        }

        private void lvMobSpells_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (e.Label == null) return;
            if (e.Label.Length <= 0)
            {
                e.CancelEdit = true;
                return;
            }
            if (lvMainList.SelectedItems.Count <= 0 || !IsInteger(e.Label)) return;
            Mob mob = ZoneDm.Mobs[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            mob.Spells.Replace(Convert.ToInt32(lvMobSpells.Items[e.Item].Tag),
                               Convert.ToInt32(e.Label.Replace("%", "")));
            RefreshMobSpellsList(mob);
        }

        private void lvMobSpells_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
                tsbMobRemoveSpell_Click(null, null);
        }


        private void lvMobSpells_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tsbMobRemoveSpell_Click(null, null);
        }

        private void lvMobAvailSpells_DoubleClick(object sender, EventArgs e)
        {
            tsbMobAddSpell_Click(null, null);
        }

        private void tsbMobAddSpell_Click(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Mob mob = ZoneDm.Mobs[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            foreach (ListViewItem lvi in lvMobAvailSpells.SelectedItems)
            {
                MobSpell spell = mob.Spells.Get(Convert.ToInt32(lvi.Tag));
                if (spell != null)
                    spell.Count++;
                else
                    mob.Spells.Add(Convert.ToInt32(lvi.Tag), 1);
            }
            RefreshMobSpellsList(mob);
            if (lvMobSpells.Items.Count <= 0) return;
            lvMobSpells.Items[lvMobSpells.Items.Count - 1].Selected = true;
        }

        private void tsbMobRemoveSpell_Click(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Mob mob = ZoneDm.Mobs[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            foreach (ListViewItem lvi in lvMobSpells.SelectedItems)
            {
                MobSpell spell = mob.Spells.Get(Convert.ToInt32(lvi.Tag));
                if (spell != null)
                {
                    if (spell.Count > 1)
                        spell.Count--;
                    else
                        mob.Spells.Remove(Convert.ToInt32(lvi.Tag));
                }
            }
            RefreshMobSpellsList(mob);
            if (lvMobSpells.Items.Count <= 0) return;
            lvMobSpells.Items[lvMobSpells.Items.Count - 1].Selected = true;
        }

        private void tsbMobEditSpell_Click(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0 || lvMobSpells.SelectedItems.Count <= 0) return;
            lvMobSpells.SelectedItems[0].BeginEdit();
        }

        private void lvMobAvailSpells_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            switch (lvMobAvailSpells.Sorting)
            {
                case SortOrder.None:
                    lvMobAvailSpells.Sorting = SortOrder.Ascending;
                    chAvailMobSpellName.Text = "Заклинание v";
                    break;
                case SortOrder.Ascending:
                    lvMobAvailSpells.Sorting = SortOrder.Descending;
                    chAvailMobSpellName.Text = "Заклинание ^";
                    break;
                case SortOrder.Descending:
                    lvMobAvailSpells.Sorting = SortOrder.None;
                    chAvailMobSpellName.Text = "Заклинание";
                    lvMobAvailSpells.BeginUpdate();
                    lvMobAvailSpells.Items.Clear();
                    foreach (DataRow dr in BasesDm.MobSpells.Rows)
                    {
                        ListViewItem lvi = new ListViewItem { Text = dr["desc"].ToString(), Tag = dr["val"].ToString() };
                        lvMobAvailSpells.Items.Add(lvi);
                    }
                    lvMobAvailSpells.EndUpdate();
                    break;
            }
        }

        private void tplMobFeats_ValueChanged(object args)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Mob mob = ZoneDm.Mobs[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            mob.Feats = ((BaseDataArrayList)args);
        }

        private void tplMobAffects_ValueChanged(object args)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Mob mob = ZoneDm.Mobs[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            mob.Affects = ((string)args);
        }

        private void tplMobFlags_ValueChanged(object args)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Mob mob = ZoneDm.Mobs[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            mob.Flags = ((string)args);
            RecolorizeMobsList();
        }

        private void tplMobSpecFlags_ValueChanged(object args)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Mob mob = ZoneDm.Mobs[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            mob.SpecialBitvector = ((string)args);
            RecolorizeMobsList();
        }

        private void btnMobAddHelper_Click(object sender, EventArgs e)
        {
            MobsCollection allMobs = WindowParentForm.GetAllKnownMobs();
            MobSelectForm msf = new MobSelectForm("Выберите мобов-помощников", allMobs, ZoneDm.Zone.Number, true, false);
            DialogResult dres = msf.ShowDialog();
            if (dres == DialogResult.OK)
            {
                if (lvMainList.SelectedItems.Count > 0)
                {
                    Mob mob = ZoneDm.Mobs[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
                    foreach (Mob mobHelper in msf.SelectedMobs)
                        mob.Helpers.Add(mobHelper.VNum);
                    RefreshMobHelpersList(mob);
                }
            }
            msf.Dispose();
        }

        private void btnRemoveHelpersList_Click(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Mob mob = ZoneDm.Mobs[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            foreach (ListViewItem lvi in lvMobHelpers.SelectedItems)
                mob.Helpers.Remove(Convert.ToInt32(lvi.Tag));
            RefreshMobHelpersList(mob);
            if (lvMobHelpers.Items.Count <= 0) return;
            lvMobHelpers.Items[lvMobHelpers.Items.Count - 1].Selected = true;
        }

        private void lvMobHelpers_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
                btnRemoveHelpersList_Click(null, null);
        }

        private void btnAddMobTrigger_Click(object sender, EventArgs e)
        {
            TriggersCollection allTriggers = WindowParentForm.GetAllKnownTriggers(0);
            TrgSelectForm tsf =
                new TrgSelectForm("Выберите триггеры для моба", allTriggers, ZoneDm.Zone.Number, true, false);
            DialogResult dres = tsf.ShowDialog();
            if (dres == DialogResult.OK)
            {
                if (lvMainList.SelectedItems.Count > 0)
                {
                    Mob mob = ZoneDm.Mobs[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
                    foreach (Trigger trigger in tsf.SelectedTriggers)
                        mob.TriggersList.Add(trigger.VNum);
                    RefreshMobTriggersList(mob);
                }
            }
            tsf.Dispose();
        }

        private void btnMobRemoveTrigger_Click(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Mob mob = ZoneDm.Mobs[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            foreach (ListViewItem lvi in lvMobTriggers.SelectedItems)
                mob.TriggersList.Remove(Convert.ToInt32(lvi.Tag));
            RefreshMobTriggersList(mob);
            if (lvMobTriggers.Items.Count <= 0) return;
            lvMobTriggers.Items[lvMobTriggers.Items.Count - 1].Selected = true;
        }

        private void lvMobTriggers_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
                btnMobRemoveTrigger_Click(null, null);
        }

        public void btnSelectMobPath_Click(object sender, EventArgs e)
        {
            if (btnSelectMobPath.Text == "Изменить")
            {
                btnSelectMobPath.Text = "Сохранить";
                if (wldMap.ExternalPathSelection == false)
                {
                    wldMap.PathChanged += wldMap_PathChanged;
                    wldMap.ExternalPathSelection = true;
                    wldMap.SelectedPath = tboxMobDestination.Text;
                    tboxMobDestination.ReadOnly = false;
                }
            }
            else
            {
                btnSelectMobPath.Text = "Изменить";
                if (wldMap.ExternalPathSelection)
                {
                    wldMap.PathChanged -= wldMap_PathChanged;
                    wldMap.ExternalPathSelection = false;
                    if (lvMainList.SelectedItems.Count <= 0) return;
                    Mob mob = ZoneDm.Mobs[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
                    if (mob != null)
                    {
                        //Тут делать валидацию пути
                        mob.Destination.Clear();
                        string[] parts = tboxMobDestination.Text.Split('/');
                        foreach (string s in parts)
                        {
                            if (StringUtils.IsUnsignedNumber(s))
                                mob.Destination.Add(StringUtils.ToUnsignedIntFast(s));
                            /*if (Regex.Match(s, "\\d+").Success)
                                mob.Destination.Add(Convert.ToInt32(s));*/
                        }
                    }
                    tboxMobDestination.ReadOnly = true;
                }
            }
        }

        private void wldMap_PathChanged()
        {
            tboxMobDestination.Text = wldMap.SelectedPath;
        }

        private void tboxMobDestination_TextChanged(object sender, EventArgs e)
        {
            if (wldMap.ExternalPathSelection)
                wldMap.SelectedPath = tboxMobDestination.Text;
        }

        private void BtnAddMobIngredientClick(object sender, EventArgs e)
        {
            if (WindowParentForm == null) return;
            if (lvMainList.SelectedItems.Count <= 0) return;
            Mob mob = ZoneDm.Mobs[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            var isf = new IngrSelectForm("Выберите ингредиенты для моба", BasesDm.MobIngredients);
            DialogResult dres = isf.ShowDialog();
            if (dres == DialogResult.OK)
            {
                foreach (var ingr in isf.SelectedIngredients)
                    mob.Ingredients.Add(ingr, 1, 1);
                RefreshMobIngredientsList(mob);
            }
            isf.Dispose();
        }

        private void BtnRemoveMobIngredientClick(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Mob mob = ZoneDm.Mobs[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            foreach (ListViewItem lvi in elvMobIngredients.SelectedItems)
                mob.Ingredients.Remove(lvi.Tag.ToString());
            RefreshMobIngredientsList(mob);
            if (elvMobIngredients.Items.Count <= 0) return;
            elvMobIngredients.Items[elvMobIngredients.Items.Count - 1].Selected = true;
        }

        public void RefreshMobIngredientsList(Mob mob)
        {
            elvMobIngredients.Items.Clear();
            foreach (Ingredient ingrdient in mob.Ingredients)
            {
                var desc = "";
                foreach (DataRow ingrRow in BasesDm.MobIngredients.Rows)
                {
                    if (ingrRow["val"].ToString() == ingrdient.TypeName)
                    {
                        desc = ingrRow["desc"].ToString();
                        break;
                    }
                }
                ExListViewItem elvi = new ExListViewItem(ingrdient.Probability.ToString())
                {
                    Tag = ingrdient.TypeName,
                    Guid = ingrdient.InternaGuid
                };
                elvi.SubItems.Add(new ExListViewSubItem(ingrdient.Power.ToString()));
                elvi.SubItems.Add(new ExListViewSubItem(ingrdient.PowerAuto?"Да":"Нет"));
                elvi.SubItems.Add(new ExListViewSubItem(desc));
                elvMobIngredients.Items.Add(elvi);
            }
        }

        private void ElvMobIngredientsItemValueChanged(ExListViewItem item, int number, string prevVal, string newVal)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Mob mob = ZoneDm.Mobs[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            switch (number)
            {
                case 0:
                    mob.Ingredients.ReplaceProbability(item.Guid, StringUtils.ToIntFast(newVal));
                    break;
                case 1:
                    mob.Ingredients.ReplacePower(item.Guid, StringUtils.ToIntFast(newVal));
                    RefreshMobIngredientsList(mob);//PowerAuto сбрасывается при установке силы вручную
                    break;
                case 2:                    
                    mob.Ingredients.ReplacePowerAuto(item.Guid, newVal == "Да");
                    RefreshMobIngredientsList(mob);//Power сбрасывается на 0 при включении авто
                    break;
            }
        }

        #region Refresh

        public void RefreshMobsList()
        {
            lvMainList.BeginUpdate();
            lvMainList.Items.Clear();
            foreach (Mob mob in ZoneDm.Mobs)
            {
                var lvi = new ListViewItem(new[] { mob.VNum.ToString(), mob.Cases.Imen }) { Tag = mob.VNum };

                if (mob.Modifyed)
                    lvi.ImageIndex = 47;

                if (mob.Flags.Contains("f0") ||
                    mob.Flags.Contains("v0") || mob.Flags.Contains("w0") ||
                    mob.Flags.Contains("r2") || mob.Flags.Contains("s2") || mob.Flags.Contains("t2") ||
                    mob.Flags.Contains("i0") || mob.Flags.Contains("j0") || mob.Flags.Contains("k0") ||
                    mob.Flags.Contains("d1") || mob.Flags.Contains("e1") || mob.Flags.Contains("f1") ||
                    mob.Flags.Contains("g1"))
                    lvi.BackColor = Color.FromArgb(255, 115, 115);
                else if (mob.SpecialBitvector.Contains("y0"))
                    lvi.BackColor = Color.FromArgb(115, 255, 115);
                else if (mob.SpecialBitvector.Contains("i3"))
                    lvi.BackColor = Color.FromArgb(115, 115, 255);
                else if (mob.TriggersList.Count > 0)
                    lvi.BackColor = Color.FromArgb(255, 250, 115);

                if (tboxMainListFilter.Text.Length > 0)
                {
                    if (
                        (mob.Cases.Imen.ToUpper() + mob.VNum.ToString().ToUpper()).IndexOf(
                            tboxMainListFilter.Text.ToUpper()) >= 0)
                        lvMainList.Items.Add(lvi);
                }
                else
                    lvMainList.Items.Add(lvi);
            }
            lvMainList.EndUpdate();
        }

        public void RecolorizeMobsList()
        {
            lvMainList.BeginUpdate();
            foreach (ListViewItem lvi in lvMainList.Items)
            {
                int vNum = int.Parse(lvi.Tag.ToString());
                Mob mob = ZoneDm.Mobs[vNum, 0];
                if (mob == null) continue;
                if (mob.Flags.Contains("f0") ||
                    mob.Flags.Contains("v0") || mob.Flags.Contains("w0") ||
                    mob.Flags.Contains("r2") || mob.Flags.Contains("s2") || mob.Flags.Contains("t2") ||
                    mob.Flags.Contains("i0") || mob.Flags.Contains("j0") || mob.Flags.Contains("k0") ||
                    mob.Flags.Contains("d1") || mob.Flags.Contains("e1") || mob.Flags.Contains("f1") ||
                    mob.Flags.Contains("g1"))
                    lvi.BackColor = Color.FromArgb(255, 115, 115);
                else if (mob.SpecialBitvector.Contains("y0"))
                    lvi.BackColor = Color.FromArgb(115, 255, 115);
                else if (mob.SpecialBitvector.Contains("i3"))
                    lvi.BackColor = Color.FromArgb(115, 115, 255);
                else if (mob.TriggersList.Count > 0)
                    lvi.BackColor = Color.FromArgb(255, 250, 115);
            }
            lvMainList.EndUpdate();
        }

        public void RefreshMobHelpersList(Mob mob)
        {
            lvMobHelpers.Items.Clear();
            //этот поиск названия моба по номеру можно выкинуть в датаменеджер и делать 1 раз
            MobsCollection mobs = WindowParentForm.GetAllKnownMobs();
            foreach (int vNum in mob.Helpers)
            {
                string name = "";
                if (mobs[vNum, 0] != null) name = mobs[vNum, 0].Cases.Imen;
                var lvi = new ListViewItem(new[] { vNum.ToString(), name }) { Tag = vNum };
                lvMobHelpers.Items.Add(lvi);
            }
        }

        public void RefreshMobSkillsList(Mob mob)
        {
            lvMobSkills.BeginUpdate();
            lvAvailMobSkills.BeginUpdate();
            lvMobSkills.Items.Clear();
            lvAvailMobSkills.Items.Clear();
            foreach (MobSkill skill in mob.Skills)
            {
                var lvi =
                    new ListViewItem(new[] { skill.Percent.ToString(), BasesDm.GetSkillNameByNum(skill.VNum) }) { Tag = skill.VNum };
                lvMobSkills.Items.Add(lvi);
            }
            foreach (DataRow dr in BasesDm.MobSkills.Rows)
            {
                MobSkill skill = mob.Skills.Get(Convert.ToInt32(dr["val"]));
                if (skill != null) continue;
                ListViewItem lvi = new ListViewItem { Text = dr["desc"].ToString(), Tag = dr["val"].ToString() };
                lvAvailMobSkills.Items.Add(lvi);
            }
            lvMobSkills.EndUpdate();
            lvAvailMobSkills.EndUpdate();
        }

        public void RefreshMobSpellsList(Mob mob)
        {
            lvMobSpells.BeginUpdate();
            lvMobSpells.Items.Clear();
            foreach (MobSpell spell in mob.Spells)
            {
                var lvi =
                    new ListViewItem(new[] { spell.Count.ToString(), BasesDm.GetSpellNameByNum(spell.VNum) }) { Tag = spell.VNum };
                lvMobSpells.Items.Add(lvi);
            }
            lvMobSpells.EndUpdate();
            if (lvMobAvailSpells.Items.Count == 0)
            {
                lvMobAvailSpells.BeginUpdate();
                foreach (DataRow dr in BasesDm.MobSpells.Rows)
                {
                    /*MobSpell spell = mob.Spells.Get(Convert.ToInt32(dr["val"]));
                    if (spell != null) continue;*/
                    ListViewItem lvi = new ListViewItem { Text = dr["desc"].ToString(), Tag = dr["val"].ToString() };
                    lvMobAvailSpells.Items.Add(lvi);
                }
                lvMobAvailSpells.EndUpdate();
            }
        }

        public void RefreshMobFeatsList(Mob mob)
        {
            tplMobFeats.SetData(mob.Feats, BasesDm.MobFeatures);
        }

        public void RefreshMobRolesList(Mob mob)
        {
            tplMobRoles.SetData(mob.Roles, BasesDm.MobRoles);
        }

        public void RefreshMobAffectsList(Mob mob)
        {
            tplMobAffects.SetData(mob.Affects, BasesDm.MobAffects);
        }

        public void RefreshMobFlagsList(Mob mob)
        {
            tplMobFlags.SetData(mob.Flags, BasesDm.MobFlags);
        }

        public void RefreshMobSpecFlagsList(Mob mob)
        {
            tplMobSpecFlags.SetData(mob.SpecialBitvector, BasesDm.MobSpecBitvector);
        }

        public void RefreshMobTriggersList(Mob mob)
        {
            lvMobTriggers.Items.Clear();
            TriggersCollection allTriggers = WindowParentForm.GetAllKnownTriggers(0);
            foreach (int vNum in mob.TriggersList)
            {
                string triggerName = "!!!Триггер с таким номером не найден";
                Trigger trg = allTriggers.GetTrigger(vNum);
                if (trg != null)
                    triggerName = trg.Name;
                var lvi = new ListViewItem(new[] { vNum.ToString(), triggerName }) { Tag = vNum };
                lvMobTriggers.Items.Add(lvi);
            }
        }

        public void RefreshMobData()
        {
            Mob mob = ZoneDm.Mobs[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            if (mob == null) return;
            cborMobRemoveOnReload.Checked = ZoneDm.Zone.MobsToRemove.MobExists(mob.VNum);
            cboxMobSex.SelectedIndex = mob.Sex;
            SetCBoxsSelectedItem(cboxMobClass, mob.Class);
            SetCBoxsSelectedItem(cboxMobRace, mob.Race);
            tboxMobAliases.Text = mob.Alias;
            tboxMobNameImen.Text = mob.Cases.Imen;
            tboxMobNameRod.Text = mob.Cases.Rod;
            tboxMobNameDat.Text = mob.Cases.Dat;
            tboxMobNameVin.Text = mob.Cases.Vin;
            tboxMobNameTvor.Text = mob.Cases.Tvor;
            tboxMobNamePred.Text = mob.Cases.Pred;
            tboxMobDesc.Text = mob.Desc;
            ertbMobDescription.Text = mob.DetailDescr;

            RefreshDetailsAndLocations(mob);

            RefreshMobTabsData(mob);

            LastSelectedMob = mob.VNum;
        }

        public void RefreshSelectedMobTabData()
        {
            if (lvMainList.SelectedItems.Count == 0) return;
            Mob mob = ZoneDm.Mobs[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            if (mob == null) return;
            RefreshMobTabsData(mob);
        }

        private void RefreshMobTabsData(Mob mob)
        {
            MustUpdateMobData = false;
            switch (tcMobs.SelectedTab.Name)
            {
                case "tpMobParameters":
                    if (btnSelectMobPath.Text == "Сохранить")
                        btnSelectMobPath_Click(null, null);
                    //MustUpdateMobData = false;
                    nudMobStr.Value = mob.Stats.Str;
                    nudMobInt.Value = mob.Stats.Int;
                    nudMobWis.Value = mob.Stats.Wis;
                    nudMobDex.Value = mob.Stats.Dex;
                    nudMobCon.Value = mob.Stats.Con;
                    nudMobCha.Value = mob.Stats.Cha;
                    nudMobLevel.Value = mob.Level;
                    nudMobSize.Value = mob.Stats.Size;
                    nudMobHeight.Value = mob.Stats.Height;
                    nudMobWeight.Value = mob.Stats.Weight;
                    nudMobAC.Value = mob.Ac;
                    nudMobHitroll.Value = mob.Hitroll;
                    //nudMobMaxInWorld.Value = mob.MaxInWorld; //скрыто на странице моба
                    if (mob.Align < 0)
                        cboxMobAlign.SelectedIndex = 0;
                    else cboxMobAlign.SelectedIndex = mob.Align == 0 ? 1 : 2;
                    cboxMobAttackType.SelectedIndex = mob.BareHandAttack;
                    nudMobExtraAttack.Value = mob.ExtraAttack;
                    nudMobLikeWork.Value = mob.LikeWork;
                    cboxMobStartPosition.SelectedIndex = mob.PosLoad;
                    cboxMobDefPosition.SelectedIndex = mob.PosDefault;
                    nudMobExpa.Value = mob.Exp;
                    nudMobMaxFactor.Value = mob.MaxFactor;
                    dctrlMobAttack.Value = mob.Damage;
                    dctrlMobHP.Value = mob.Hits;
                    dctrlMobMoney.Value = mob.Money;
                    tboxMobDestination.Text = "";
                    foreach (int i in mob.Destination)
                    {
                        tboxMobDestination.Text = (tboxMobDestination.Text.Length > 0)
                                                           ? tboxMobDestination.Text + "/" + i
                                                           : tboxMobDestination.Text + i;
                    }
                    //MustUpdateMobData = true;
                    break;
                case "tpMobSkills":
                    RefreshMobSkillsList(mob);
                    break;
                case "tpMobSpells":
                    RefreshMobSpellsList(mob);
                    break;
                case "tpMobFeatures":
                    RefreshMobFeatsList(mob);
                    break;
                case "tpMobRoles":
                    RefreshMobRolesList(mob);
                    break;
                case "tpMobAffects":
                    RefreshMobAffectsList(mob);
                    break;
                case "tpMobFlags":
                    RefreshMobFlagsList(mob);
                    break;
                case "tpMobSpecFlags":
                    RefreshMobSpecFlagsList(mob);
                    break;
                case "tpMobHelpers":
                    RefreshMobHelpersList(mob);
                    break;
                case "tpMobTriggers":
                    RefreshMobTriggersList(mob);
                    break;
                case "tpMobIngredients":                    
                    RefreshMobIngredientsList(mob);
                    break;
                case "tpMobResists":
                    nudSaveParalyze.Value = mob.SaveParalyzeCast;
                    nudSaveMagBreathe.Value = mob.SaveMagBreathes;
                    nudSaveMagDam.Value = mob.SaveMagDamages;
                    nudSaveFightSkills.Value = mob.SaveFightSkills;
                    nudResFire.Value = mob.ResistFromFire;
                    nudResAir.Value = mob.ResistFromAir;
                    nudResWater.Value = mob.ResistFromWater;
                    nudResEarth.Value = mob.ResistFromEarth;

                    nudVitality.Value = mob.Vitality;
                    nudRegeneration.Value = mob.HPreg;
                    nudArmour.Value = mob.Armour;
                    nudAdsorb.Value = mob.Absorbe;
                    nudMind.Value = mob.Mind;
                    nudMem.Value = mob.PlusMem;
                    nudCastSuccess.Value = mob.CastSuccess;
                    nudInitiative.Value = mob.Initiative;
                    nudSuccess.Value = mob.Luck;
                    nudImmun.Value = mob.Immunitet;
                    nudResistDark.Value = mob.ResistDark;
                    nudAResist.Value = mob.AResist;
                    nudMResist.Value = mob.MResist;
                    nudPResist.Value = mob.PResist;
                    break;
            }
            MustUpdateMobData = true;
        }

        #endregion

        internal void RefreshDetailsAndLocations(Mob mob)
        {
            lvDetails.BeginUpdate();
            ClearDetails();
            wldMap.HighlightedRooms.Clear();
            lvDetails.Groups.Add(new ListViewGroup("Загружается в комнаты", HorizontalAlignment.Left));
            foreach (Room r in ZoneDm.Rooms)
            {
                foreach (OperatedMob lm in r.LoadingMobsCollection)
                {
                    if (lm.VNum != mob.VNum) continue;
                    lvDetails.Items.Add(new ExListViewItem("[" + r.VNum + "] " + r.Name)
                    {
                        Tag = r.VNum,
                        Action = ActionType.GoToRoomLoadedMobs,
                        Group = lvDetails.Groups[0]
                    });
                    wldMap.HighlightedRooms.Add(Convert.ToInt32(r.VNum));
                }
            }
            wldMap.RedrawBitmap();

            lvDetails.Groups.Add(new ListViewGroup("Асистит мобам", HorizontalAlignment.Left));
            foreach (Mob m in ZoneDm.Mobs)
            {
                if (!m.Helpers.Contains(mob.VNum)) continue;
                lvDetails.Items.Add(new ExListViewItem("[" + m.VNum + "] " + m.Cases.Imen)
                {
                    Tag = m.VNum,
                    Action = ActionType.GoToMob,
                    Group = lvDetails.Groups[1]
                });
            }

            lvDetails.Groups.Add(new ListViewGroup("Триггеры", HorizontalAlignment.Left));
            foreach (int vnum in mob.TriggersList)
            {
                Trigger t = ZoneDm.Triggers[vnum, 0];
                if (t == null) continue;
                lvDetails.Items.Add(new ExListViewItem("[" + t.VNum + "] " + t.Name)
                {
                    Tag = t.VNum,
                    Action = ActionType.GoToTrigger,
                    Group = lvDetails.Groups[2]
                });
            }

            lvDetails.EndUpdate();
        }

        private void tplMobRolesValueChanged(object args)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Mob mob = ZoneDm.Mobs[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            mob.Roles = ((BaseDataArrayList)args);
        }


        
        #endregion

        #region Navigator

        internal void ClearDetails()
        {
            lvDetails.Items.Clear();
            lvDetails.Groups.Clear();
        }

        internal void Navigate(ExListViewItem item)
        {
            tcListAndInfo.SelectedIndex = 0;
            switch (item.Action)
            {
                case ActionType.GoToMob:
                    tcMain.SelectedIndex = 3;
                    SelectInList(item.Tag);
                    tcMobs.SelectedIndex = 0;
                    break;
                case ActionType.GoToObject:
                    tcMain.SelectedIndex = 2;
                    SelectInList(item.Tag);
                    tcObject.SelectedIndex = 0;
                    break;
                case ActionType.GoToRoom:
                    SelectRoomInList(item.Tag);
                    tcRoom.SelectedIndex = 0;
                    break;
                case ActionType.GoToTrigger:
                    tcMain.SelectedIndex = 5;
                    SelectInList(item.Tag);
                    break;
                case ActionType.GoToRoomLoadedMobs:
                    SelectRoomInList(item.Tag);
                    tcRoom.SelectedIndex = 3;
                    tcRoom.Enabled = true;
                    tcRoom.Focus();
                    RefreshRoomData();
                    SelectInList(lvMobsInRoom, item.TrgVNum);
                    SelectInList(elvRoomMobObjects, item.TrgVNum2);
                    break;
                case ActionType.GoToRoomLoadedObjects:
                    SelectRoomInList(item.Tag);
                    tcRoom.SelectedIndex = 2;
                    RefreshRoomData();
                    break;
                case ActionType.GoToRoomUnloadedObjects:
                    SelectRoomInList(item.Tag);
                    tcRoom.SelectedIndex = 5;
                    RefreshRoomData();
                    break;
            }
        }

        internal void Navigate(ParseMessageEventArgs item)
        {
            tcListAndInfo.SelectedIndex = 0;
            switch (item.Action)
            {
                case ActionType.GoToMob:
                    tcMain.SelectedIndex = 3;
                    SelectInList(item.VNum);
                    tcMobs.SelectedIndex = 0;
                    break;
                case ActionType.GoToObject:
                    tcMain.SelectedIndex = 2;
                    SelectInList(item.VNum);
                    tcObject.SelectedIndex = 0;
                    break;
                case ActionType.GoToRoom:
                    tcMain.SelectedIndex = 1;
                    SelectRoomInList(item.VNum);
                    tcRoom.SelectedIndex = 0;
                    break;
                case ActionType.GoToTrigger:
                    tcMain.SelectedIndex = 5;
                    SelectInList(item.VNum);
                    break;
            }
        }

        private void SelectInList(object vNum)
        {
            SelectInList(vNum.ToString());
        }

        private void SelectInList(int vNum)
        {
            SelectInList(vNum.ToString());
        }

        private void SelectInList(string vNum)
        {
            if (lvMainList.Items.Count <= 0) return;
            foreach (ListViewItem lvi in lvMainList.Items)
            {
                if (lvi.Tag.ToString() != vNum) continue;
                lvMainList.TopItem = lvi;
                lvi.Selected = true;
                return;
            }
        }

        private static void SelectInList(ListView listView, int vNum)
        {
            if (listView.Items.Count <= 0) return;
            foreach (ListViewItem lvi in listView.Items)
            {
                if (lvi.Tag.ToString() != vNum.ToString()) continue;
                listView.TopItem = lvi;
                lvi.Selected = true;
                return;
            }
        }

        private void SelectRoomInList(object vNum)
        {
            SelectRoomInList(vNum.ToString());
        }

        private void SelectRoomInList(int vNum)
        {
            SelectRoomInList(vNum.ToString());
        }

        private void SelectRoomInList(string vNum)
        {
            lvMainList.SelectedItems.Clear();
            wldMap.SelectedRooms.Clear();
            foreach (ListViewItem lvi in lvMainList.Items)
            {
                if (lvi.Tag.ToString() != vNum) continue;
                lvMainList.TopItem = lvi;
                lvi.Selected = true;
                Room room = ZoneDm.Rooms[Convert.ToInt32(vNum), 0];
                wldMap.SelectedRooms.Add(room.VNum);
                wldMap.CenterRoomCoord = new Point3D(room.X, room.Y, room.Z);
                return;
            }
            ActiveRoom = ZoneDm.Rooms[Convert.ToInt32(vNum), 0];
            wldMap.AddRoomToSelection(ActiveRoom.VNum);
            wldMap.CenterRoomCoord = new Point3D(ActiveRoom.X, ActiveRoom.Y, ActiveRoom.Z);
        }

        #endregion

    }
}