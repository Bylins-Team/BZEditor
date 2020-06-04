using ExtControls;
using DataUtils;
using System.Windows.Forms;
namespace BZEditor
{
    partial class WldForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
                colorDlg.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label label152;
            System.Windows.Forms.Label label172;
            System.Windows.Forms.Label label87;
            System.Windows.Forms.Label label88;
            System.Windows.Forms.Label label45;
            System.Windows.Forms.Label label42;
            System.Windows.Forms.Label label41;
            System.Windows.Forms.Label label33;
            System.Windows.Forms.Label label39;
            System.Windows.Forms.Label label38;
            System.Windows.Forms.Label label28;
            System.Windows.Forms.Label label37;
            System.Windows.Forms.Label label36;
            System.Windows.Forms.Label label49;
            System.Windows.Forms.Label label46;
            System.Windows.Forms.Label label47;
            System.Windows.Forms.Label label48;
            System.Windows.Forms.Label label50;
            System.Windows.Forms.Label label52;
            System.Windows.Forms.Label label53;
            System.Windows.Forms.Label label55;
            System.Windows.Forms.Label label56;
            System.Windows.Forms.Label label54;
            System.Windows.Forms.Label label171;
            System.Windows.Forms.Label label139;
            System.Windows.Forms.Label label140;
            System.Windows.Forms.Label label150;
            System.Windows.Forms.Label label142;
            System.Windows.Forms.Label label147;
            System.Windows.Forms.Label label143;
            System.Windows.Forms.Label label145;
            System.Windows.Forms.Label label157;
            System.Windows.Forms.Label label158;
            System.Windows.Forms.Label label160;
            System.Windows.Forms.Label label166;
            System.Windows.Forms.Label label167;
            System.Windows.Forms.Label label169;
            System.Windows.Forms.Label label154;
            System.Windows.Forms.Label label161;
            System.Windows.Forms.Label label162;
            System.Windows.Forms.Label label128;
            System.Windows.Forms.Label label129;
            System.Windows.Forms.Label label130;
            System.Windows.Forms.Label label131;
            System.Windows.Forms.Label label132;
            System.Windows.Forms.Label label133;
            System.Windows.Forms.Label label134;
            System.Windows.Forms.Label label135;
            System.Windows.Forms.Label label136;
            System.Windows.Forms.Label label137;
            System.Windows.Forms.Label label138;
            System.Windows.Forms.Label label1111;
            System.Windows.Forms.Label label163;
            System.Windows.Forms.Label label164;
            System.Windows.Forms.Label label165;
            System.Windows.Forms.Label label123;
            System.Windows.Forms.Label label125;
            System.Windows.Forms.Label label127;
            System.Windows.Forms.Label label170;
            System.Windows.Forms.Label label27;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label31;
            System.Windows.Forms.Label label24;
            System.Windows.Forms.Label label25;
            System.Windows.Forms.Label label26;
            System.Windows.Forms.Label label32;
            System.Windows.Forms.Label label59;
            System.Windows.Forms.Label label60;
            System.Windows.Forms.Label label11;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label23;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label19;
            System.Windows.Forms.Label label14;
            System.Windows.Forms.Label label21;
            System.Windows.Forms.Label label20;
            System.Windows.Forms.Label label22;
            System.Windows.Forms.Label label10;
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label label9;
            System.Windows.Forms.Label label18;
            System.Windows.Forms.Label label17;
            System.Windows.Forms.Label label8;
            System.Windows.Forms.Label label7;
            System.Windows.Forms.Label label16;
            System.Windows.Forms.Label label12;
            System.Windows.Forms.Label label13;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.GroupBox gbOthers;
            System.Windows.Forms.Label label121;
            System.Windows.Forms.Label label113;
            System.Windows.Forms.Label label117;
            System.Windows.Forms.Label label94;
            System.Windows.Forms.Label label124;
            System.Windows.Forms.Label label115;
            System.Windows.Forms.Label label114;
            System.Windows.Forms.Label label116;
            System.Windows.Forms.Label label122;
            System.Windows.Forms.Label label120;
            System.Windows.Forms.GroupBox gbResists;
            System.Windows.Forms.Label label71;
            System.Windows.Forms.Label label119;
            System.Windows.Forms.Label label111;
            System.Windows.Forms.Label label109;
            System.Windows.Forms.Label label110;
            System.Windows.Forms.Label label108;
            System.Windows.Forms.Label label112;
            System.Windows.Forms.Label label118;
            System.Windows.Forms.Label label107;
            System.Windows.Forms.Label label104;
            System.Windows.Forms.Label label105;
            System.Windows.Forms.Label label106;
            System.Windows.Forms.Label label68;
            System.Windows.Forms.Label label74;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WldForm));
            Fireball.Windows.Forms.LineMarginRender lineMarginRender1 = new Fireball.Windows.Forms.LineMarginRender();
            this.nudPResist = new System.Windows.Forms.NumericUpDown();
            this.nudMResist = new System.Windows.Forms.NumericUpDown();
            this.nudAResist = new System.Windows.Forms.NumericUpDown();
            this.nudArmour = new System.Windows.Forms.NumericUpDown();
            this.nudAdsorb = new System.Windows.Forms.NumericUpDown();
            this.nudInitiative = new System.Windows.Forms.NumericUpDown();
            this.nudMem = new System.Windows.Forms.NumericUpDown();
            this.nudSuccess = new System.Windows.Forms.NumericUpDown();
            this.nudCastSuccess = new System.Windows.Forms.NumericUpDown();
            this.nudRegeneration = new System.Windows.Forms.NumericUpDown();
            this.nudResistDark = new System.Windows.Forms.NumericUpDown();
            this.nudResEarth = new System.Windows.Forms.NumericUpDown();
            this.nudResAir = new System.Windows.Forms.NumericUpDown();
            this.nudResWater = new System.Windows.Forms.NumericUpDown();
            this.nudResFire = new System.Windows.Forms.NumericUpDown();
            this.nudVitality = new System.Windows.Forms.NumericUpDown();
            this.nudMind = new System.Windows.Forms.NumericUpDown();
            this.nudImmun = new System.Windows.Forms.NumericUpDown();
            this.cmsMainTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiAddItems = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRemoveItems = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiAddTemplate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCreateClones = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPasteAsTemplate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiShowRoomOnMap = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsNavigation = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiNavigateTo = new System.Windows.Forms.ToolStripMenuItem();
            this.iListIcons16 = new System.Windows.Forms.ImageList(this.components);
            this.cmsGridMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiAddRow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRemoveRow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiGoTo = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsRoomsDescription = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiCopyDesc = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCutDesc = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiPasteDesc = new System.Windows.Forms.ToolStripMenuItem();
            this.syntaxDocument = new Fireball.Syntax.SyntaxDocument(this.components);
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.cbIsertSpaces = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.nudOptimalCharsInGroup = new System.Windows.Forms.NumericUpDown();
            this.btnAddAZones = new System.Windows.Forms.Button();
            this.btnRemoveAZones = new System.Windows.Forms.Button();
            this.btnAddBZones = new System.Windows.Forms.Button();
            this.btnRemoveBZones = new System.Windows.Forms.Button();
            this.btnValidate = new System.Windows.Forms.Button();
            this.tbExitSouth = new Fireball.Windows.Forms.NumericBox();
            this.tbExitNorth = new Fireball.Windows.Forms.NumericBox();
            this.tbExitDown = new Fireball.Windows.Forms.NumericBox();
            this.tbExitUp = new Fireball.Windows.Forms.NumericBox();
            this.tbExitEast = new Fireball.Windows.Forms.NumericBox();
            this.tbExitWest = new Fireball.Windows.Forms.NumericBox();
            this.label93 = new System.Windows.Forms.Label();
            this.btnSelectDoorKey = new System.Windows.Forms.Button();
            this.tbDoorNameVin = new System.Windows.Forms.TextBox();
            this.btnRoomAddObj = new System.Windows.Forms.Button();
            this.btnRoomRemoveObj = new System.Windows.Forms.Button();
            this.elvRoomObjInObj = new ExtControls.ExtListView();
            this.btnRoomAddObjInObj = new System.Windows.Forms.Button();
            this.btnRoomRemoveObjFromObj = new System.Windows.Forms.Button();
            this.btnRoomAddMob = new System.Windows.Forms.Button();
            this.btnRoomRemoveMob = new System.Windows.Forms.Button();
            this.nudMaxInRoom = new System.Windows.Forms.NumericUpDown();
            this.label85 = new System.Windows.Forms.Label();
            this.btnRoomSpecFormatCommonDesc = new System.Windows.Forms.Button();
            this.btnRoomSpellCheckCommonDesc = new System.Windows.Forms.Button();
            this.cbInsertSpaces = new System.Windows.Forms.CheckBox();
            this.btnRoomFormatCommonDesc = new System.Windows.Forms.Button();
            this.btnObjSetAutoCases = new System.Windows.Forms.Button();
            this.tboxObjAliases = new System.Windows.Forms.TextBox();
            this.tboxObjTvor = new System.Windows.Forms.TextBox();
            this.tboxObjVin = new System.Windows.Forms.TextBox();
            this.tboxObjDat = new System.Windows.Forms.TextBox();
            this.tboxObjPredl = new System.Windows.Forms.TextBox();
            this.tboxObjImen = new System.Windows.Forms.TextBox();
            this.tboxObjRod = new System.Windows.Forms.TextBox();
            this.nudObjContainerKeyVNum = new System.Windows.Forms.NumericUpDown();
            this.nudObjContainerValue = new System.Windows.Forms.NumericUpDown();
            this.btnSelectFontPorionProto = new System.Windows.Forms.Button();
            this.btnSelectPotionProtoVNum = new System.Windows.Forms.Button();
            this.btnAddObjTrigger = new System.Windows.Forms.Button();
            this.btnObjRemoveTrigger = new System.Windows.Forms.Button();
            this.btnObjReplaceAddDesc = new System.Windows.Forms.Button();
            this.btnObjAddAddDesc = new System.Windows.Forms.Button();
            this.btnObjRemoveAddDesc = new System.Windows.Forms.Button();
            this.btnMobSetAutoCases = new System.Windows.Forms.Button();
            this.tboxMobNameTvor = new System.Windows.Forms.TextBox();
            this.cboxMobSex = new System.Windows.Forms.ComboBox();
            this.tboxMobAliases = new System.Windows.Forms.TextBox();
            this.tboxMobNameVin = new System.Windows.Forms.TextBox();
            this.tboxMobNameDat = new System.Windows.Forms.TextBox();
            this.tboxMobNameRod = new System.Windows.Forms.TextBox();
            this.tboxMobNameImen = new System.Windows.Forms.TextBox();
            this.tboxMobNamePred = new System.Windows.Forms.TextBox();
            this.tboxMobDesc = new System.Windows.Forms.TextBox();
            this.nudMobHitroll = new System.Windows.Forms.NumericUpDown();
            this.nudMobAC = new System.Windows.Forms.NumericUpDown();
            this.nudMobMaxInWorld = new System.Windows.Forms.NumericUpDown();
            this.dctrlMobHP = new BZEditor.UcDiceControl();
            this.dctrlMobAttack = new BZEditor.UcDiceControl();
            this.nudMobWeight = new System.Windows.Forms.NumericUpDown();
            this.nudMobSize = new System.Windows.Forms.NumericUpDown();
            this.nudMobExpa = new System.Windows.Forms.NumericUpDown();
            this.btnSelectMobPath = new System.Windows.Forms.Button();
            this.tboxMobDestination = new System.Windows.Forms.TextBox();
            this.nudMobHeight = new System.Windows.Forms.NumericUpDown();
            this.nudMobLevel = new System.Windows.Forms.NumericUpDown();
            this.cboxMobAlign = new System.Windows.Forms.ComboBox();
            this.cboxMobAttackType = new System.Windows.Forms.ComboBox();
            this.btnMobAddHelper = new System.Windows.Forms.Button();
            this.btnRemoveHelpersList = new System.Windows.Forms.Button();
            this.btnAddMobTrigger = new System.Windows.Forms.Button();
            this.btnMobRemoveTrigger = new System.Windows.Forms.Button();
            this.nudSaveFightSkills = new System.Windows.Forms.NumericUpDown();
            this.nudSaveMagDam = new System.Windows.Forms.NumericUpDown();
            this.nudSaveParalyze = new System.Windows.Forms.NumericUpDown();
            this.nudSaveMagBreathe = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.bdtAddMobInVirtualRoom = new System.Windows.Forms.Button();
            this.btnRemoveMobFromVitrualRoom = new System.Windows.Forms.Button();
            this.elvVitrualRoomMobObjects = new ExtControls.ExtListView();
            this.btnAddItemToMobInVirtualRoom = new System.Windows.Forms.Button();
            this.btnRemoveItemFromMobInVirtualRoom = new System.Windows.Forms.Button();
            this.nudVirtualRoomMobMaxInRoom = new System.Windows.Forms.NumericUpDown();
            this.label40 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.elvRoomMobObjectsLoadingAfterDeath = new ExtControls.ExtListView();
            this.btnRoomRomoveObjFromMobAfterDeath = new System.Windows.Forms.Button();
            this.btnRoomRomoveObjFromMob = new System.Windows.Forms.Button();
            this.btnRoomAddObjToMob = new System.Windows.Forms.Button();
            this.elvRoomMobObjects = new ExtControls.ExtListView();
            this.btnMobSpecFormatCommonDesc = new System.Windows.Forms.Button();
            this.btnMobSpellCheckCommonDesc = new System.Windows.Forms.Button();
            this.btnMobFormatCommonDesc = new System.Windows.Forms.Button();
            this.nudObjMinRemorts = new System.Windows.Forms.NumericUpDown();
            this.btnAddRoomIngredient = new System.Windows.Forms.Button();
            this.btnRemoveRoomIngredient = new System.Windows.Forms.Button();
            this.elvRoomIngredients = new ExtControls.ExtListView();
            this.elvMobIngredients = new ExtControls.ExtListView();
            this.btnAddMobIngredient = new System.Windows.Forms.Button();
            this.btnRemoveMobIngredient = new System.Windows.Forms.Button();
            this.cbRoomDescAllowHyp = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.CExtRichTextBox2 = new ExtControls.CExtRichTextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.CExtRichTextBox3 = new ExtControls.CExtRichTextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.CExtRichTextBox4 = new ExtControls.CExtRichTextBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.checkBox7 = new System.Windows.Forms.CheckBox();
            this.CExtRichTextBox5 = new ExtControls.CExtRichTextBox();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.checkBox8 = new System.Windows.Forms.CheckBox();
            this.CExtRichTextBox6 = new ExtControls.CExtRichTextBox();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.checkBox9 = new System.Windows.Forms.CheckBox();
            this.CExtRichTextBox7 = new ExtControls.CExtRichTextBox();
            this.tabPage16 = new System.Windows.Forms.TabPage();
            this.checkBox10 = new System.Windows.Forms.CheckBox();
            this.CExtRichTextBox8 = new ExtControls.CExtRichTextBox();
            this.tabPage17 = new System.Windows.Forms.TabPage();
            this.checkBox11 = new System.Windows.Forms.CheckBox();
            this.CExtRichTextBox9 = new ExtControls.CExtRichTextBox();
            this.tabPage18 = new System.Windows.Forms.TabPage();
            this.checkBox12 = new System.Windows.Forms.CheckBox();
            this.CExtRichTextBox10 = new ExtControls.CExtRichTextBox();
            this.tabPage19 = new System.Windows.Forms.TabPage();
            this.checkBox13 = new System.Windows.Forms.CheckBox();
            this.CExtRichTextBox11 = new ExtControls.CExtRichTextBox();
            this.tabPage20 = new System.Windows.Forms.TabPage();
            this.checkBox14 = new System.Windows.Forms.CheckBox();
            this.CExtRichTextBox12 = new ExtControls.CExtRichTextBox();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton18 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.cmsCodeEditor = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiCodeEditorCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCodeEditorCut = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiCodeEditorPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainerBase = new System.Windows.Forms.SplitContainer();
            this.splitContainerMap = new System.Windows.Forms.SplitContainer();
            this.tcListAndInfo = new System.Windows.Forms.TabControl();
            this.tpList = new System.Windows.Forms.TabPage();
            this.tboxMainListFilter = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.lvMainList = new ExtControls.SICFListView();
            this.chMainListVNum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMainListItemName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cboxMainListConditions = new System.Windows.Forms.ComboBox();
            this.label51 = new System.Windows.Forms.Label();
            this.lvZoneInfo = new System.Windows.Forms.ListView();
            this.chParamName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chParamVal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tpInfo = new System.Windows.Forms.TabPage();
            this.lvDetails = new ExtControls.SICFListView();
            this.columnHeader29 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.wldMap = new ExtControls.WldMap();
            this.vertSBMap = new BZEditor.VerticalScrollBar();
            this.pnlMapHorizSplitter = new System.Windows.Forms.Panel();
            this.horizSBMap = new BZEditor.HorizontalScrollBar();
            this.btnToMapCenter = new System.Windows.Forms.Button();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpZone = new System.Windows.Forms.TabPage();
            this.splitContainerZon = new System.Windows.Forms.SplitContainer();
            this.btnChangeZoneNumber = new System.Windows.Forms.Button();
            this.nudZoneLevel = new System.Windows.Forms.NumericUpDown();
            this.nudZoneNumber = new System.Windows.Forms.NumericUpDown();
            this.nudRepopTimer = new System.Windows.Forms.NumericUpDown();
            this.label69 = new System.Windows.Forms.Label();
            this.label63 = new System.Windows.Forms.Label();
            this.label65 = new System.Windows.Forms.Label();
            this.cboxZonType = new System.Windows.Forms.ComboBox();
            this.tbZoneAuthor = new System.Windows.Forms.TextBox();
            this.tbZoneDescription = new System.Windows.Forms.TextBox();
            this.tbZoneLocation = new System.Windows.Forms.TextBox();
            this.tbZoneComment = new System.Windows.Forms.TextBox();
            this.label66 = new System.Windows.Forms.Label();
            this.label73 = new System.Windows.Forms.Label();
            this.label72 = new System.Windows.Forms.Label();
            this.tbZoneName = new System.Windows.Forms.TextBox();
            this.label91 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label96 = new System.Windows.Forms.Label();
            this.label64 = new System.Windows.Forms.Label();
            this.tcZon = new System.Windows.Forms.TabControl();
            this.tpVitrualRoom = new System.Windows.Forms.TabPage();
            this.splitContainerVirtualRoomMobs = new System.Windows.Forms.SplitContainer();
            this.lvMobsInVitrualRoom = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.scontMobInVitrualRoomLoadedObjects = new System.Windows.Forms.SplitContainer();
            this.cboxVitrualRoomMobFollowBy = new System.Windows.Forms.ComboBox();
            this.label43 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.elvVitrualRoomMobObjectsAfterDeath = new ExtControls.ExtListView();
            this.btnRemoveItemFromMobInVirtualRoomAfterDeath = new System.Windows.Forms.Button();
            this.btnAddItemToMobInVirtualRoomAfterDeath = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.tpResetCondition = new System.Windows.Forms.TabPage();
            this.gbResetRelatedZones = new System.Windows.Forms.GroupBox();
            this.splitContainerRepop = new System.Windows.Forms.SplitContainer();
            this.label82 = new System.Windows.Forms.Label();
            this.lvAZones = new System.Windows.Forms.ListView();
            this.columnHeader30 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader31 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvBZones = new System.Windows.Forms.ListView();
            this.columnHeader32 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader33 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label97 = new System.Windows.Forms.Label();
            this.cbZoneReopopType = new System.Windows.Forms.ComboBox();
            this.tpStatistics = new System.Windows.Forms.TabPage();
            this.mlbValidationResult = new ExtControls.MessageListBox();
            this.cbShowInfo = new System.Windows.Forms.CheckBox();
            this.cbShowWarnings = new System.Windows.Forms.CheckBox();
            this.cbShowErrors = new System.Windows.Forms.CheckBox();
            this.label126 = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.tpRooms = new System.Windows.Forms.TabPage();
            this.splitContainerRoomsDesc = new System.Windows.Forms.SplitContainer();
            this.splitContainerRooms = new System.Windows.Forms.SplitContainer();
            this.gboxExits = new System.Windows.Forms.GroupBox();
            this.bSelectExitDown = new System.Windows.Forms.Button();
            this.bSelectExitSouth = new System.Windows.Forms.Button();
            this.bSelectExitEast = new System.Windows.Forms.Button();
            this.bSelectExitWest = new System.Windows.Forms.Button();
            this.bSelectExitUp = new System.Windows.Forms.Button();
            this.bSelectExitNorth = new System.Windows.Forms.Button();
            this.cboxSectorType = new System.Windows.Forms.ComboBox();
            this.label81 = new System.Windows.Forms.Label();
            this.tbRoomName = new System.Windows.Forms.TextBox();
            this.label80 = new System.Windows.Forms.Label();
            this.lRoomDesc = new System.Windows.Forms.Label();
            this.tcRoom = new System.Windows.Forms.TabControl();
            this.tpRoomDoors = new System.Windows.Forms.TabPage();
            this.pDoors = new System.Windows.Forms.Panel();
            this.gbDoorType = new System.Windows.Forms.GroupBox();
            this.nudLockLevel = new System.Windows.Forms.NumericUpDown();
            this.cbDoorLocked = new System.Windows.Forms.CheckBox();
            this.cbDoorPeekproof = new System.Windows.Forms.CheckBox();
            this.cbDoorClosed = new System.Windows.Forms.CheckBox();
            this.cbExitDoor = new System.Windows.Forms.CheckBox();
            this.cbExitVisible = new System.Windows.Forms.CheckBox();
            this.cbExitHidden = new System.Windows.Forms.CheckBox();
            this.tbRoomDoorKeyName = new System.Windows.Forms.TextBox();
            this.nudDoorKeyVNum = new System.Windows.Forms.NumericUpDown();
            this.label92 = new System.Windows.Forms.Label();
            this.btnConfigExitDown = new System.Windows.Forms.Button();
            this.btnConfigExitSouth = new System.Windows.Forms.Button();
            this.btnConfigExitEast = new System.Windows.Forms.Button();
            this.btnConfigExitWest = new System.Windows.Forms.Button();
            this.btnConfigExitUp = new System.Windows.Forms.Button();
            this.btnConfigExitNorth = new System.Windows.Forms.Button();
            this.tbDoorDesc = new System.Windows.Forms.TextBox();
            this.tbDoorAlias = new System.Windows.Forms.TextBox();
            this.label99 = new System.Windows.Forms.Label();
            this.label86 = new System.Windows.Forms.Label();
            this.label89 = new System.Windows.Forms.Label();
            this.label90 = new System.Windows.Forms.Label();
            this.tpRoomFlags = new System.Windows.Forms.TabPage();
            this.tplRoomFlags = new BZEditor.UcTwoPanelsList();
            this.cbShowRoomsWithFlags = new System.Windows.Forms.CheckBox();
            this.tpRoomObjs = new System.Windows.Forms.TabPage();
            this.splitContainerRoomObjects = new System.Windows.Forms.SplitContainer();
            this.elvObjectsInRoom = new ExtControls.ExtListView();
            this.gbObjInObj = new System.Windows.Forms.GroupBox();
            this.tpRoomMobs = new System.Windows.Forms.TabPage();
            this.splitContainerRoomMobs = new System.Windows.Forms.SplitContainer();
            this.lvMobsInRoom = new System.Windows.Forms.ListView();
            this.columnHeader20 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader21 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label84 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpMobObjectsLoaded = new System.Windows.Forms.TabPage();
            this.tpMobObjectsLoadedAfterDeath = new System.Windows.Forms.TabPage();
            this.cboxMobFollowBy = new System.Windows.Forms.ComboBox();
            this.tpRoomTrgs = new System.Windows.Forms.TabPage();
            this.btnAddRoomTrigger = new System.Windows.Forms.Button();
            this.btnRemoveRoomTrigger = new System.Windows.Forms.Button();
            this.lvRoomTriggers = new System.Windows.Forms.ListView();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tpRoomDelObjs = new System.Windows.Forms.TabPage();
            this.btnAddRoomObjectToRemove = new System.Windows.Forms.Button();
            this.btnRemoveRoomObjectToRemove = new System.Windows.Forms.Button();
            this.lvObjectsToRemove = new System.Windows.Forms.ListView();
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tpRoomAddDescrs = new System.Windows.Forms.TabPage();
            this.rtbRoomAddDescText = new ExtControls.CExtRichTextBox();
            this.cbRoomAddDescWordwrap = new System.Windows.Forms.CheckBox();
            this.btnAddRoomAddDesc = new System.Windows.Forms.Button();
            this.btnRemoveRoomAddDesc = new System.Windows.Forms.Button();
            this.lvRoomAddDescriptions = new System.Windows.Forms.ListView();
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbRoomAddDescAliases = new System.Windows.Forms.TextBox();
            this.tpRoomIngrediens = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rtbRoomDesc = new ExtControls.CExtRichTextBox();
            this.tabControlRoomDescriptions = new System.Windows.Forms.TabControl();
            this.tpRoomDesc = new System.Windows.Forms.TabPage();
            this.tpRoomDescDay = new System.Windows.Forms.TabPage();
            this.tpRoomDescNight = new System.Windows.Forms.TabPage();
            this.tpRoomDescWinterDay = new System.Windows.Forms.TabPage();
            this.tpRoomDescWinterNight = new System.Windows.Forms.TabPage();
            this.tpRoomDescSpringDay = new System.Windows.Forms.TabPage();
            this.tpRoomDescSpringNight = new System.Windows.Forms.TabPage();
            this.tpRoomDescSummerDay = new System.Windows.Forms.TabPage();
            this.tpRoomDescSummerNight = new System.Windows.Forms.TabPage();
            this.tpRoomDescAutumnDay = new System.Windows.Forms.TabPage();
            this.tpRoomDescAutumnNight = new System.Windows.Forms.TabPage();
            this.pnlFormating = new System.Windows.Forms.Panel();
            this.cbDescReplace = new System.Windows.Forms.CheckBox();
            this.cbAllowHyp = new System.Windows.Forms.CheckBox();
            this.tpObjects = new System.Windows.Forms.TabPage();
            this.splitContainerObj = new System.Windows.Forms.SplitContainer();
            this.cboxObjectGender = new System.Windows.Forms.ComboBox();
            this.tboxObjDesc = new System.Windows.Forms.TextBox();
            this.tboxObjActionDesc = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.tcObject = new System.Windows.Forms.TabControl();
            this.tpObjParams = new System.Windows.Forms.TabPage();
            this.cboxObjSkill = new System.Windows.Forms.ComboBox();
            this.nudObjMaxInWorld = new System.Windows.Forms.NumericUpDown();
            this.nudObjRentPriceInv = new System.Windows.Forms.NumericUpDown();
            this.cboxObjMatherial = new System.Windows.Forms.ComboBox();
            this.cboxObjTimerUOM = new System.Windows.Forms.ComboBox();
            this.nudObjTimer = new System.Windows.Forms.NumericUpDown();
            this.nudObjWeight = new System.Windows.Forms.NumericUpDown();
            this.nudObjPrice = new System.Windows.Forms.NumericUpDown();
            this.nudObjRentPriceEquip = new System.Windows.Forms.NumericUpDown();
            this.cboxObjMaxStructHits = new System.Windows.Forms.ComboBox();
            this.nudObjCurStructHits = new System.Windows.Forms.NumericUpDown();
            this.gbObjType = new System.Windows.Forms.GroupBox();
            this.pObjMagIngr = new System.Windows.Forms.Panel();
            this.lvObjMagIngrFlags = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.nudObjMagIngrUseRemain = new System.Windows.Forms.NumericUpDown();
            this.nudObjMagIngrPrototype = new System.Windows.Forms.NumericUpDown();
            this.nudObjMagIngrMinLev = new System.Windows.Forms.NumericUpDown();
            this.nudObjMagIngrLag = new System.Windows.Forms.NumericUpDown();
            this.cboxObjType = new System.Windows.Forms.ComboBox();
            this.pObjMagWand = new System.Windows.Forms.Panel();
            this.cboxObjMagWandSpell = new System.Windows.Forms.ComboBox();
            this.nudObjMagWandZarCntCurr = new System.Windows.Forms.NumericUpDown();
            this.nudObjMagWandZarCnt = new System.Windows.Forms.NumericUpDown();
            this.nudObjMagWandMinLev = new System.Windows.Forms.NumericUpDown();
            this.label168 = new System.Windows.Forms.Label();
            this.pObjWeapon = new System.Windows.Forms.Panel();
            this.lObjAverageDam = new System.Windows.Forms.Label();
            this.cboxObjWeaponSrikeType = new System.Windows.Forms.ComboBox();
            this.nudObjWeaponD2 = new System.Windows.Forms.NumericUpDown();
            this.nudObjWeaponD1 = new System.Windows.Forms.NumericUpDown();
            this.label144 = new System.Windows.Forms.Label();
            this.pObjPotion = new System.Windows.Forms.Panel();
            this.cboxObjPotionSpell2 = new System.Windows.Forms.ComboBox();
            this.cboxObjPotionSpell3 = new System.Windows.Forms.ComboBox();
            this.cboxObjPotionSpell1 = new System.Windows.Forms.ComboBox();
            this.nudObjPotionMinLev = new System.Windows.Forms.NumericUpDown();
            this.label148 = new System.Windows.Forms.Label();
            this.label149 = new System.Windows.Forms.Label();
            this.label151 = new System.Windows.Forms.Label();
            this.pObjMoney = new System.Windows.Forms.Panel();
            this.cboxMoneyCurrency = new System.Windows.Forms.ComboBox();
            this.nudObjMoneyValue = new System.Windows.Forms.NumericUpDown();
            this.pObjMagStaff = new System.Windows.Forms.Panel();
            this.cboxObjMagStaffSpell = new System.Windows.Forms.ComboBox();
            this.nudObjMagStaffZarCntCur = new System.Windows.Forms.NumericUpDown();
            this.nudObjMagStaffZarCnt = new System.Windows.Forms.NumericUpDown();
            this.nudObjMagStaffMinLev = new System.Windows.Forms.NumericUpDown();
            this.label159 = new System.Windows.Forms.Label();
            this.pObjMagicScroll = new System.Windows.Forms.Panel();
            this.cboxObjMagScrollSpell2 = new System.Windows.Forms.ComboBox();
            this.cboxObjMagScrollSpell3 = new System.Windows.Forms.ComboBox();
            this.cboxObjMagScrollSpell1 = new System.Windows.Forms.ComboBox();
            this.nudObjMagScrollMinLev = new System.Windows.Forms.NumericUpDown();
            this.label141 = new System.Windows.Forms.Label();
            this.label153 = new System.Windows.Forms.Label();
            this.label155 = new System.Windows.Forms.Label();
            this.pObjMagBook = new System.Windows.Forms.Panel();
            this.cboxObjMagBookSpell = new System.Windows.Forms.ComboBox();
            this.label156 = new System.Windows.Forms.Label();
            this.pObjLiquidContainer = new System.Windows.Forms.Panel();
            this.cboxObjLiquidContainerDrinkType = new System.Windows.Forms.ComboBox();
            this.nudPotionProtoVNum = new System.Windows.Forms.NumericUpDown();
            this.nudObjLiquidContainerCurVal = new System.Windows.Forms.NumericUpDown();
            this.nudObjLiquidContainerMaxVal = new System.Windows.Forms.NumericUpDown();
            this.nudObjLiquidContainerPoison = new System.Windows.Forms.NumericUpDown();
            this.pObjLighter = new System.Windows.Forms.Panel();
            this.nudObjLighterValue = new System.Windows.Forms.NumericUpDown();
            this.pObjFood = new System.Windows.Forms.Panel();
            this.nudObjFoodPoison = new System.Windows.Forms.NumericUpDown();
            this.nudObjFoodVal = new System.Windows.Forms.NumericUpDown();
            this.pObjFontan = new System.Windows.Forms.Panel();
            this.nudFontPorionProtoVNum = new System.Windows.Forms.NumericUpDown();
            this.cboxObjFontanDrinkType = new System.Windows.Forms.ComboBox();
            this.nudObjFontanCurVal = new System.Windows.Forms.NumericUpDown();
            this.nudObjFontanMaxVal = new System.Windows.Forms.NumericUpDown();
            this.nudObjFontanPoison = new System.Windows.Forms.NumericUpDown();
            this.pObjContainer = new System.Windows.Forms.Panel();
            this.nudObjLockVal = new System.Windows.Forms.NumericUpDown();
            this.lvObjContainerFlags = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pObjBandage = new System.Windows.Forms.Panel();
            this.label173 = new System.Windows.Forms.Label();
            this.nudObjBandageValue = new System.Windows.Forms.NumericUpDown();
            this.pObjArmor = new System.Windows.Forms.Panel();
            this.nudObjArmorArm = new System.Windows.Forms.NumericUpDown();
            this.nudObjArmorAC = new System.Windows.Forms.NumericUpDown();
            this.label146 = new System.Windows.Forms.Label();
            this.tpObjEffects = new System.Windows.Forms.TabPage();
            this.tplObjEffects = new BZEditor.UcTwoPanelsList();
            this.tpObjAffects = new System.Windows.Forms.TabPage();
            this.tplObjAffects = new BZEditor.UcTwoPanelsList();
            this.tpObjWearTo = new System.Windows.Forms.TabPage();
            this.tplObjWearTo = new BZEditor.UcTwoPanelsList();
            this.tpObjCantTouch = new System.Windows.Forms.TabPage();
            this.tplObjCantTouch = new BZEditor.UcTwoPanelsList();
            this.tpObjCantUse = new System.Windows.Forms.TabPage();
            this.tplObjCantUse = new BZEditor.UcTwoPanelsList();
            this.tpObjTriggers = new System.Windows.Forms.TabPage();
            this.lvObjTriggers = new System.Windows.Forms.ListView();
            this.columnHeader18 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader19 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tpObjAddDescs = new System.Windows.Forms.TabPage();
            this.rtbObjAddDesc = new ExtControls.CExtRichTextBox();
            this.cbMustWordwrapAddDesc = new System.Windows.Forms.CheckBox();
            this.lvObjAddDesc = new System.Windows.Forms.ListView();
            this.columnHeader24 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader25 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tboxAddDescAliases = new System.Windows.Forms.TextBox();
            this.tpObjAddAffects = new System.Windows.Forms.TabPage();
            this.splitContainerAddAff = new System.Windows.Forms.SplitContainer();
            this.lvObjBonuses = new System.Windows.Forms.ListView();
            this.chObjAddAffectPercent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chObjAddAffectName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvAvailAddAffects = new System.Windows.Forms.ListView();
            this.chObjAddAffectAvail = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStripObjAddBonuses = new System.Windows.Forms.ToolStrip();
            this.tsbObjAdditAffectAdd = new System.Windows.Forms.ToolStripButton();
            this.tsbObjAdditAffectRemove = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbEditAddAffectValue = new System.Windows.Forms.ToolStripButton();
            this.tpObjSkillBonus = new System.Windows.Forms.TabPage();
            this.splitContainerSkillBonus = new System.Windows.Forms.SplitContainer();
            this.lvSkillBonuses = new System.Windows.Forms.ListView();
            this.chObjAddSkillPercent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chObjAddSkill = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvAvailSkillBonuses = new System.Windows.Forms.ListView();
            this.chObjAddSkillAvail = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStripObjSkillBonuses = new System.Windows.Forms.ToolStrip();
            this.tsbAddSkillBonus = new System.Windows.Forms.ToolStripButton();
            this.tsbRemoveSkillBonus = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbEditSkillBonus = new System.Windows.Forms.ToolStripButton();
            this.tpMobs = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainerMob = new System.Windows.Forms.SplitContainer();
            this.cboxMobRace = new System.Windows.Forms.ComboBox();
            this.cboxMobClass = new System.Windows.Forms.ComboBox();
            this.cborMobRemoveOnReload = new System.Windows.Forms.CheckBox();
            this.tcMobs = new System.Windows.Forms.TabControl();
            this.tpMobParameters = new System.Windows.Forms.TabPage();
            this.cboxMobDefPosition = new System.Windows.Forms.ComboBox();
            this.cboxMobStartPosition = new System.Windows.Forms.ComboBox();
            this.nudMobMaxFactor = new System.Windows.Forms.NumericUpDown();
            this.nudMobLikeWork = new System.Windows.Forms.NumericUpDown();
            this.nudMobCha = new System.Windows.Forms.NumericUpDown();
            this.nudMobDex = new System.Windows.Forms.NumericUpDown();
            this.nudMobInt = new System.Windows.Forms.NumericUpDown();
            this.nudMobExtraAttack = new System.Windows.Forms.NumericUpDown();
            this.nudMobCon = new System.Windows.Forms.NumericUpDown();
            this.nudMobWis = new System.Windows.Forms.NumericUpDown();
            this.label34 = new System.Windows.Forms.Label();
            this.nudMobStr = new System.Windows.Forms.NumericUpDown();
            this.dctrlMobMoney = new BZEditor.UcDiceControl();
            this.tpMobSkills = new System.Windows.Forms.TabPage();
            this.splitContainerMobSkills = new System.Windows.Forms.SplitContainer();
            this.lvMobSkills = new System.Windows.Forms.ListView();
            this.chMobSkillPercent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMobSkillName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvAvailMobSkills = new System.Windows.Forms.ListView();
            this.chMobSkillAvail = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStripMobSkills = new System.Windows.Forms.ToolStrip();
            this.tsbMobAddSkill = new System.Windows.Forms.ToolStripButton();
            this.tsbMobRemoveSkill = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbMobEditSkill = new System.Windows.Forms.ToolStripButton();
            this.tpMobSpells = new System.Windows.Forms.TabPage();
            this.splitContainerMobSpells = new System.Windows.Forms.SplitContainer();
            this.lvMobSpells = new System.Windows.Forms.ListView();
            this.chMobSpellCnt = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMobSpellName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvMobAvailSpells = new System.Windows.Forms.ListView();
            this.chAvailMobSpellName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStripMobSpells = new System.Windows.Forms.ToolStrip();
            this.tsbMobAddSpell = new System.Windows.Forms.ToolStripButton();
            this.tsbMobRemoveSpell = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator19 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbMobEditSpell = new System.Windows.Forms.ToolStripButton();
            this.tpMobFeatures = new System.Windows.Forms.TabPage();
            this.tplMobFeats = new BZEditor.UcTwoPanelsList();
            this.tpMobAffects = new System.Windows.Forms.TabPage();
            this.tplMobAffects = new BZEditor.UcTwoPanelsList();
            this.tpMobFlags = new System.Windows.Forms.TabPage();
            this.tplMobFlags = new BZEditor.UcTwoPanelsList();
            this.tpMobSpecFlags = new System.Windows.Forms.TabPage();
            this.tplMobSpecFlags = new BZEditor.UcTwoPanelsList();
            this.tpMobHelpers = new System.Windows.Forms.TabPage();
            this.lvMobHelpers = new System.Windows.Forms.ListView();
            this.chMobHelperVNum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMobHelperName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tpMobTriggers = new System.Windows.Forms.TabPage();
            this.lvMobTriggers = new System.Windows.Forms.ListView();
            this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader17 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tpMobResists = new System.Windows.Forms.TabPage();
            this.gbSaves = new System.Windows.Forms.GroupBox();
            this.tpMobRoles = new System.Windows.Forms.TabPage();
            this.tplMobRoles = new BZEditor.UcTwoPanelsList();
            this.tpMobIngredients = new System.Windows.Forms.TabPage();
            this.pnlAddMobDesc = new System.Windows.Forms.Panel();
            this.ertbMobDescription = new ExtControls.CExtRichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label67 = new System.Windows.Forms.Label();
            this.tpTriggers = new System.Windows.Forms.TabPage();
            this.splitContainerTrg = new System.Windows.Forms.SplitContainer();
            this.tcTriggers = new System.Windows.Forms.TabControl();
            this.tpTrgParams = new System.Windows.Forms.TabPage();
            this.cboxTrgClass = new System.Windows.Forms.ComboBox();
            this.gbObjectsToCreate = new System.Windows.Forms.GroupBox();
            this.lvTrgActivationConditions = new System.Windows.Forms.ListView();
            this.chTrgActCond = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbTrgArgs = new System.Windows.Forms.TextBox();
            this.nudTrgNumArg = new System.Windows.Forms.NumericUpDown();
            this.tbTrgName = new System.Windows.Forms.TextBox();
            this.label61 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.label62 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.tpTrgGlobalVars = new System.Windows.Forms.TabPage();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader22 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader23 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.codeEditor = new Fireball.Windows.Forms.CodeEditorControl();
            this.toolStripPanelTrgTop = new System.Windows.Forms.ToolStripPanel();
            this.toolStripTrgEditor = new System.Windows.Forms.ToolStrip();
            this.tsbTrgClear = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbTrgUndo = new System.Windows.Forms.ToolStripButton();
            this.tsbTrgRedo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbTrgToggleBookmark = new System.Windows.Forms.ToolStripButton();
            this.tsbTrgGoToPrevBookmark = new System.Windows.Forms.ToolStripButton();
            this.tsbTrgGoToNextBookmark = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbTrgGoToLine = new System.Windows.Forms.ToolStripButton();
            this.tsbTrgSearch = new System.Windows.Forms.ToolStripButton();
            this.tsbTrgSearchNext = new System.Windows.Forms.ToolStripButton();
            this.tsbTrgReplace = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbTrgIndent = new System.Windows.Forms.ToolStripButton();
            this.tsbTrgOutdent = new System.Windows.Forms.ToolStripButton();
            this.tsbTrgCopy = new System.Windows.Forms.ToolStripButton();
            this.tsbTrgCut = new System.Windows.Forms.ToolStripButton();
            this.tsbTrgPaste = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbInsertSpellNumber = new System.Windows.Forms.ToolStripButton();
            this.toolStripPanelTrgRight = new System.Windows.Forms.ToolStripPanel();
            this.toolStripPanelTrgBottom = new System.Windows.Forms.ToolStripPanel();
            this.toolStripPanelTrgLeft = new System.Windows.Forms.ToolStripPanel();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.tsbReloadWithoutSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSaveZone = new System.Windows.Forms.ToolStripButton();
            this.tsbSaveCurTabData = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbAddItems = new System.Windows.Forms.ToolStripButton();
            this.tsbRemoveItems = new System.Windows.Forms.ToolStripButton();
            this.tsbAddTemplate = new System.Windows.Forms.ToolStripButton();
            this.tsbAutolinkingZ = new System.Windows.Forms.ToolStripButton();
            this.tsbAutolinkingY = new System.Windows.Forms.ToolStripButton();
            this.tsbAutolinkingX = new System.Windows.Forms.ToolStripButton();
            this.tsbCreateClones = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbCopy = new System.Windows.Forms.ToolStripButton();
            this.tsbPaste = new System.Windows.Forms.ToolStripButton();
            this.tsbPasteAsTemplate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSetOppositeExit = new System.Windows.Forms.ToolStripButton();
            this.tsbHistoryBack = new System.Windows.Forms.ToolStripButton();
            this.tsbHistoryForward = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbMapZoomOut = new System.Windows.Forms.ToolStripButton();
            this.tsbMapZoomIn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbMapZDec = new System.Windows.Forms.ToolStripButton();
            this.tsbMapToZeroRom = new System.Windows.Forms.ToolStripButton();
            this.tsbMapZInc = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbShowRoomTriggers = new System.Windows.Forms.ToolStripButton();
            this.tsbShowRoomMobs = new System.Windows.Forms.ToolStripButton();
            this.tsbShowRoomObjects = new System.Windows.Forms.ToolStripButton();
            this.tsbShowRoomNumbers = new System.Windows.Forms.ToolStripButton();
            this.tsbShowRoomDetails = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSketchColor = new System.Windows.Forms.ToolStripButton();
            this.tsbBrush = new System.Windows.Forms.ToolStripButton();
            this.tsbCreateRoomsBySketch = new System.Windows.Forms.ToolStripButton();
            this.tsbClearSketch = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbGenerateMap = new System.Windows.Forms.ToolStripButton();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            label152 = new System.Windows.Forms.Label();
            label172 = new System.Windows.Forms.Label();
            label87 = new System.Windows.Forms.Label();
            label88 = new System.Windows.Forms.Label();
            label45 = new System.Windows.Forms.Label();
            label42 = new System.Windows.Forms.Label();
            label41 = new System.Windows.Forms.Label();
            label33 = new System.Windows.Forms.Label();
            label39 = new System.Windows.Forms.Label();
            label38 = new System.Windows.Forms.Label();
            label28 = new System.Windows.Forms.Label();
            label37 = new System.Windows.Forms.Label();
            label36 = new System.Windows.Forms.Label();
            label49 = new System.Windows.Forms.Label();
            label46 = new System.Windows.Forms.Label();
            label47 = new System.Windows.Forms.Label();
            label48 = new System.Windows.Forms.Label();
            label50 = new System.Windows.Forms.Label();
            label52 = new System.Windows.Forms.Label();
            label53 = new System.Windows.Forms.Label();
            label55 = new System.Windows.Forms.Label();
            label56 = new System.Windows.Forms.Label();
            label54 = new System.Windows.Forms.Label();
            label171 = new System.Windows.Forms.Label();
            label139 = new System.Windows.Forms.Label();
            label140 = new System.Windows.Forms.Label();
            label150 = new System.Windows.Forms.Label();
            label142 = new System.Windows.Forms.Label();
            label147 = new System.Windows.Forms.Label();
            label143 = new System.Windows.Forms.Label();
            label145 = new System.Windows.Forms.Label();
            label157 = new System.Windows.Forms.Label();
            label158 = new System.Windows.Forms.Label();
            label160 = new System.Windows.Forms.Label();
            label166 = new System.Windows.Forms.Label();
            label167 = new System.Windows.Forms.Label();
            label169 = new System.Windows.Forms.Label();
            label154 = new System.Windows.Forms.Label();
            label161 = new System.Windows.Forms.Label();
            label162 = new System.Windows.Forms.Label();
            label128 = new System.Windows.Forms.Label();
            label129 = new System.Windows.Forms.Label();
            label130 = new System.Windows.Forms.Label();
            label131 = new System.Windows.Forms.Label();
            label132 = new System.Windows.Forms.Label();
            label133 = new System.Windows.Forms.Label();
            label134 = new System.Windows.Forms.Label();
            label135 = new System.Windows.Forms.Label();
            label136 = new System.Windows.Forms.Label();
            label137 = new System.Windows.Forms.Label();
            label138 = new System.Windows.Forms.Label();
            label1111 = new System.Windows.Forms.Label();
            label163 = new System.Windows.Forms.Label();
            label164 = new System.Windows.Forms.Label();
            label165 = new System.Windows.Forms.Label();
            label123 = new System.Windows.Forms.Label();
            label125 = new System.Windows.Forms.Label();
            label127 = new System.Windows.Forms.Label();
            label170 = new System.Windows.Forms.Label();
            label27 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label31 = new System.Windows.Forms.Label();
            label24 = new System.Windows.Forms.Label();
            label25 = new System.Windows.Forms.Label();
            label26 = new System.Windows.Forms.Label();
            label32 = new System.Windows.Forms.Label();
            label59 = new System.Windows.Forms.Label();
            label60 = new System.Windows.Forms.Label();
            label11 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label23 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label19 = new System.Windows.Forms.Label();
            label14 = new System.Windows.Forms.Label();
            label21 = new System.Windows.Forms.Label();
            label20 = new System.Windows.Forms.Label();
            label22 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            label18 = new System.Windows.Forms.Label();
            label17 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label16 = new System.Windows.Forms.Label();
            label12 = new System.Windows.Forms.Label();
            label13 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            gbOthers = new System.Windows.Forms.GroupBox();
            label121 = new System.Windows.Forms.Label();
            label113 = new System.Windows.Forms.Label();
            label117 = new System.Windows.Forms.Label();
            label94 = new System.Windows.Forms.Label();
            label124 = new System.Windows.Forms.Label();
            label115 = new System.Windows.Forms.Label();
            label114 = new System.Windows.Forms.Label();
            label116 = new System.Windows.Forms.Label();
            label122 = new System.Windows.Forms.Label();
            label120 = new System.Windows.Forms.Label();
            gbResists = new System.Windows.Forms.GroupBox();
            label71 = new System.Windows.Forms.Label();
            label119 = new System.Windows.Forms.Label();
            label111 = new System.Windows.Forms.Label();
            label109 = new System.Windows.Forms.Label();
            label110 = new System.Windows.Forms.Label();
            label108 = new System.Windows.Forms.Label();
            label112 = new System.Windows.Forms.Label();
            label118 = new System.Windows.Forms.Label();
            label107 = new System.Windows.Forms.Label();
            label104 = new System.Windows.Forms.Label();
            label105 = new System.Windows.Forms.Label();
            label106 = new System.Windows.Forms.Label();
            label68 = new System.Windows.Forms.Label();
            label74 = new System.Windows.Forms.Label();
            gbOthers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPResist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMResist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAResist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudArmour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdsorb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudInitiative)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSuccess)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCastSuccess)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRegeneration)).BeginInit();
            gbResists.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudResistDark)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudResEarth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudResAir)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudResWater)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudResFire)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVitality)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMind)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudImmun)).BeginInit();
            this.cmsMainTree.SuspendLayout();
            this.cmsNavigation.SuspendLayout();
            this.cmsGridMenu.SuspendLayout();
            this.cmsRoomsDescription.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudOptimalCharsInGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxInRoom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjContainerKeyVNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjContainerValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMobHitroll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMobAC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMobMaxInWorld)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMobWeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMobSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMobExpa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMobHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMobLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSaveFightSkills)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSaveMagDam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSaveParalyze)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSaveMagBreathe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVirtualRoomMobMaxInRoom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjMinRemorts)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.tabPage16.SuspendLayout();
            this.tabPage17.SuspendLayout();
            this.tabPage18.SuspendLayout();
            this.tabPage19.SuspendLayout();
            this.tabPage20.SuspendLayout();
            this.cmsCodeEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerBase)).BeginInit();
            this.splitContainerBase.Panel1.SuspendLayout();
            this.splitContainerBase.Panel2.SuspendLayout();
            this.splitContainerBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMap)).BeginInit();
            this.splitContainerMap.Panel1.SuspendLayout();
            this.splitContainerMap.Panel2.SuspendLayout();
            this.splitContainerMap.SuspendLayout();
            this.tcListAndInfo.SuspendLayout();
            this.tpList.SuspendLayout();
            this.tpInfo.SuspendLayout();
            this.pnlMapHorizSplitter.SuspendLayout();
            this.tcMain.SuspendLayout();
            this.tpZone.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerZon)).BeginInit();
            this.splitContainerZon.Panel1.SuspendLayout();
            this.splitContainerZon.Panel2.SuspendLayout();
            this.splitContainerZon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudZoneLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudZoneNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRepopTimer)).BeginInit();
            this.tcZon.SuspendLayout();
            this.tpVitrualRoom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerVirtualRoomMobs)).BeginInit();
            this.splitContainerVirtualRoomMobs.Panel1.SuspendLayout();
            this.splitContainerVirtualRoomMobs.Panel2.SuspendLayout();
            this.splitContainerVirtualRoomMobs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scontMobInVitrualRoomLoadedObjects)).BeginInit();
            this.scontMobInVitrualRoomLoadedObjects.Panel1.SuspendLayout();
            this.scontMobInVitrualRoomLoadedObjects.Panel2.SuspendLayout();
            this.scontMobInVitrualRoomLoadedObjects.SuspendLayout();
            this.tpResetCondition.SuspendLayout();
            this.gbResetRelatedZones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRepop)).BeginInit();
            this.splitContainerRepop.Panel1.SuspendLayout();
            this.splitContainerRepop.Panel2.SuspendLayout();
            this.splitContainerRepop.SuspendLayout();
            this.tpStatistics.SuspendLayout();
            this.tpRooms.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRoomsDesc)).BeginInit();
            this.splitContainerRoomsDesc.Panel1.SuspendLayout();
            this.splitContainerRoomsDesc.Panel2.SuspendLayout();
            this.splitContainerRoomsDesc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRooms)).BeginInit();
            this.splitContainerRooms.Panel1.SuspendLayout();
            this.splitContainerRooms.Panel2.SuspendLayout();
            this.splitContainerRooms.SuspendLayout();
            this.gboxExits.SuspendLayout();
            this.tcRoom.SuspendLayout();
            this.tpRoomDoors.SuspendLayout();
            this.pDoors.SuspendLayout();
            this.gbDoorType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLockLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDoorKeyVNum)).BeginInit();
            this.tpRoomFlags.SuspendLayout();
            this.tpRoomObjs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRoomObjects)).BeginInit();
            this.splitContainerRoomObjects.Panel1.SuspendLayout();
            this.splitContainerRoomObjects.Panel2.SuspendLayout();
            this.splitContainerRoomObjects.SuspendLayout();
            this.gbObjInObj.SuspendLayout();
            this.tpRoomMobs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRoomMobs)).BeginInit();
            this.splitContainerRoomMobs.Panel1.SuspendLayout();
            this.splitContainerRoomMobs.Panel2.SuspendLayout();
            this.splitContainerRoomMobs.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpMobObjectsLoaded.SuspendLayout();
            this.tpMobObjectsLoadedAfterDeath.SuspendLayout();
            this.tpRoomTrgs.SuspendLayout();
            this.tpRoomDelObjs.SuspendLayout();
            this.tpRoomAddDescrs.SuspendLayout();
            this.tpRoomIngrediens.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControlRoomDescriptions.SuspendLayout();
            this.pnlFormating.SuspendLayout();
            this.tpObjects.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerObj)).BeginInit();
            this.splitContainerObj.Panel1.SuspendLayout();
            this.splitContainerObj.Panel2.SuspendLayout();
            this.splitContainerObj.SuspendLayout();
            this.tcObject.SuspendLayout();
            this.tpObjParams.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjMaxInWorld)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjRentPriceInv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjTimer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjWeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjRentPriceEquip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjCurStructHits)).BeginInit();
            this.gbObjType.SuspendLayout();
            this.pObjMagIngr.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjMagIngrUseRemain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjMagIngrPrototype)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjMagIngrMinLev)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjMagIngrLag)).BeginInit();
            this.pObjMagWand.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjMagWandZarCntCurr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjMagWandZarCnt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjMagWandMinLev)).BeginInit();
            this.pObjWeapon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjWeaponD2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjWeaponD1)).BeginInit();
            this.pObjPotion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjPotionMinLev)).BeginInit();
            this.pObjMoney.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjMoneyValue)).BeginInit();
            this.pObjMagStaff.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjMagStaffZarCntCur)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjMagStaffZarCnt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjMagStaffMinLev)).BeginInit();
            this.pObjMagicScroll.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjMagScrollMinLev)).BeginInit();
            this.pObjMagBook.SuspendLayout();
            this.pObjLiquidContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPotionProtoVNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjLiquidContainerCurVal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjLiquidContainerMaxVal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjLiquidContainerPoison)).BeginInit();
            this.pObjLighter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjLighterValue)).BeginInit();
            this.pObjFood.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjFoodPoison)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjFoodVal)).BeginInit();
            this.pObjFontan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFontPorionProtoVNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjFontanCurVal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjFontanMaxVal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjFontanPoison)).BeginInit();
            this.pObjContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjLockVal)).BeginInit();
            this.pObjBandage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjBandageValue)).BeginInit();
            this.pObjArmor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjArmorArm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjArmorAC)).BeginInit();
            this.tpObjEffects.SuspendLayout();
            this.tpObjAffects.SuspendLayout();
            this.tpObjWearTo.SuspendLayout();
            this.tpObjCantTouch.SuspendLayout();
            this.tpObjCantUse.SuspendLayout();
            this.tpObjTriggers.SuspendLayout();
            this.tpObjAddDescs.SuspendLayout();
            this.tpObjAddAffects.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerAddAff)).BeginInit();
            this.splitContainerAddAff.Panel1.SuspendLayout();
            this.splitContainerAddAff.Panel2.SuspendLayout();
            this.splitContainerAddAff.SuspendLayout();
            this.toolStripObjAddBonuses.SuspendLayout();
            this.tpObjSkillBonus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerSkillBonus)).BeginInit();
            this.splitContainerSkillBonus.Panel1.SuspendLayout();
            this.splitContainerSkillBonus.Panel2.SuspendLayout();
            this.splitContainerSkillBonus.SuspendLayout();
            this.toolStripObjSkillBonuses.SuspendLayout();
            this.tpMobs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMob)).BeginInit();
            this.splitContainerMob.Panel1.SuspendLayout();
            this.splitContainerMob.Panel2.SuspendLayout();
            this.splitContainerMob.SuspendLayout();
            this.tcMobs.SuspendLayout();
            this.tpMobParameters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMobMaxFactor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMobLikeWork)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMobCha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMobDex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMobInt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMobExtraAttack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMobCon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMobWis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMobStr)).BeginInit();
            this.tpMobSkills.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMobSkills)).BeginInit();
            this.splitContainerMobSkills.Panel1.SuspendLayout();
            this.splitContainerMobSkills.Panel2.SuspendLayout();
            this.splitContainerMobSkills.SuspendLayout();
            this.toolStripMobSkills.SuspendLayout();
            this.tpMobSpells.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMobSpells)).BeginInit();
            this.splitContainerMobSpells.Panel1.SuspendLayout();
            this.splitContainerMobSpells.Panel2.SuspendLayout();
            this.splitContainerMobSpells.SuspendLayout();
            this.toolStripMobSpells.SuspendLayout();
            this.tpMobFeatures.SuspendLayout();
            this.tpMobAffects.SuspendLayout();
            this.tpMobFlags.SuspendLayout();
            this.tpMobSpecFlags.SuspendLayout();
            this.tpMobHelpers.SuspendLayout();
            this.tpMobTriggers.SuspendLayout();
            this.tpMobResists.SuspendLayout();
            this.gbSaves.SuspendLayout();
            this.tpMobRoles.SuspendLayout();
            this.tpMobIngredients.SuspendLayout();
            this.pnlAddMobDesc.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tpTriggers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerTrg)).BeginInit();
            this.splitContainerTrg.Panel1.SuspendLayout();
            this.splitContainerTrg.Panel2.SuspendLayout();
            this.splitContainerTrg.SuspendLayout();
            this.tcTriggers.SuspendLayout();
            this.tpTrgParams.SuspendLayout();
            this.gbObjectsToCreate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTrgNumArg)).BeginInit();
            this.tpTrgGlobalVars.SuspendLayout();
            this.toolStripPanelTrgTop.SuspendLayout();
            this.toolStripTrgEditor.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // label152
            // 
            label152.Location = new System.Drawing.Point(1, 267);
            label152.Name = "label152";
            label152.Size = new System.Drawing.Size(63, 20);
            label152.TabIndex = 0;
            label152.Text = "Уровень";
            label152.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label172
            // 
            label172.Location = new System.Drawing.Point(198, 69);
            label172.Name = "label172";
            label172.Size = new System.Drawing.Size(103, 17);
            label172.TabIndex = 125;
            label172.Text = "Сложность замка";
            label172.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label87
            // 
            label87.Location = new System.Drawing.Point(2, 36);
            label87.Name = "label87";
            label87.Size = new System.Drawing.Size(71, 16);
            label87.TabIndex = 90;
            label87.Text = "Описание";
            label87.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label88
            // 
            label88.Location = new System.Drawing.Point(0, -1);
            label88.Name = "label88";
            label88.Size = new System.Drawing.Size(95, 16);
            label88.TabIndex = 89;
            label88.Text = "Список альясов";
            label88.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label45
            // 
            label45.Location = new System.Drawing.Point(2, 4);
            label45.Name = "label45";
            label45.Size = new System.Drawing.Size(59, 16);
            label45.TabIndex = 64;
            label45.Text = "Род";
            label45.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label42
            // 
            label42.Location = new System.Drawing.Point(2, 143);
            label42.Name = "label42";
            label42.Size = new System.Drawing.Size(49, 16);
            label42.TabIndex = 84;
            label42.Text = "Твор.";
            label42.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label41
            // 
            label41.Location = new System.Drawing.Point(2, 166);
            label41.Name = "label41";
            label41.Size = new System.Drawing.Size(49, 16);
            label41.TabIndex = 89;
            label41.Text = "Пред.";
            label41.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label33
            // 
            label33.Location = new System.Drawing.Point(2, 187);
            label33.Name = "label33";
            label33.Size = new System.Drawing.Size(261, 16);
            label33.TabIndex = 81;
            label33.Text = "Описание предмета, если он лежит в комнате";
            label33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label39
            // 
            label39.Location = new System.Drawing.Point(2, 51);
            label39.Name = "label39";
            label39.Size = new System.Drawing.Size(49, 16);
            label39.TabIndex = 87;
            label39.Text = "Имен.";
            label39.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label38
            // 
            label38.Location = new System.Drawing.Point(2, 74);
            label38.Name = "label38";
            label38.Size = new System.Drawing.Size(49, 16);
            label38.TabIndex = 86;
            label38.Text = "Род.";
            label38.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label28
            // 
            label28.Location = new System.Drawing.Point(2, 27);
            label28.Name = "label28";
            label28.Size = new System.Drawing.Size(58, 16);
            label28.TabIndex = 78;
            label28.Text = "Алиасы";
            label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label37
            // 
            label37.Location = new System.Drawing.Point(2, 97);
            label37.Name = "label37";
            label37.Size = new System.Drawing.Size(49, 16);
            label37.TabIndex = 85;
            label37.Text = "Дат.";
            label37.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label36
            // 
            label36.Location = new System.Drawing.Point(2, 120);
            label36.Name = "label36";
            label36.Size = new System.Drawing.Size(49, 16);
            label36.TabIndex = 88;
            label36.Text = "Вин.";
            label36.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label49
            // 
            label49.Location = new System.Drawing.Point(91, 2);
            label49.Name = "label49";
            label49.Size = new System.Drawing.Size(83, 18);
            label49.TabIndex = 68;
            label49.Text = "Рента (в инв.)";
            label49.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label46
            // 
            label46.Location = new System.Drawing.Point(263, 83);
            label46.Name = "label46";
            label46.Size = new System.Drawing.Size(52, 16);
            label46.TabIndex = 65;
            label46.Text = "Таймер";
            label46.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label47
            // 
            label47.Location = new System.Drawing.Point(263, 45);
            label47.Name = "label47";
            label47.Size = new System.Drawing.Size(28, 16);
            label47.TabIndex = 66;
            label47.Text = "Вес";
            label47.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label48
            // 
            label48.Location = new System.Drawing.Point(180, 4);
            label48.Name = "label48";
            label48.Size = new System.Drawing.Size(52, 16);
            label48.TabIndex = 67;
            label48.Text = "Цена";
            label48.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label50
            // 
            label50.Location = new System.Drawing.Point(5, 3);
            label50.Name = "label50";
            label50.Size = new System.Drawing.Size(79, 17);
            label50.TabIndex = 73;
            label50.Text = "Рента (экип.)";
            label50.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label52
            // 
            label52.Location = new System.Drawing.Point(5, 83);
            label52.Name = "label52";
            label52.Size = new System.Drawing.Size(75, 16);
            label52.TabIndex = 74;
            label52.Text = "Материал";
            label52.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label53
            // 
            label53.Location = new System.Drawing.Point(5, 44);
            label53.Name = "label53";
            label53.Size = new System.Drawing.Size(123, 14);
            label53.TabIndex = 75;
            label53.Text = "Тренируемый скилл";
            label53.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label55
            // 
            label55.Location = new System.Drawing.Point(349, 123);
            label55.Name = "label55";
            label55.Size = new System.Drawing.Size(83, 16);
            label55.TabIndex = 69;
            label55.Text = "Текущ. знач.";
            label55.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label56
            // 
            label56.Location = new System.Drawing.Point(349, 4);
            label56.Name = "label56";
            label56.Size = new System.Drawing.Size(76, 16);
            label56.TabIndex = 70;
            label56.Text = "Макс. в мире";
            label56.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label54
            // 
            label54.Location = new System.Drawing.Point(5, 124);
            label54.Name = "label54";
            label54.Size = new System.Drawing.Size(169, 15);
            label54.TabIndex = 72;
            label54.Text = "Максимум структурных хитов";
            label54.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label171
            // 
            label171.Location = new System.Drawing.Point(133, 6);
            label171.Name = "label171";
            label171.Size = new System.Drawing.Size(103, 17);
            label171.TabIndex = 69;
            label171.Text = "Сложность замка";
            label171.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label139
            // 
            label139.Location = new System.Drawing.Point(2, 32);
            label139.Name = "label139";
            label139.Size = new System.Drawing.Size(74, 16);
            label139.TabIndex = 67;
            label139.Text = "Ключ";
            label139.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label140
            // 
            label140.Location = new System.Drawing.Point(2, 6);
            label140.Name = "label140";
            label140.Size = new System.Drawing.Size(78, 17);
            label140.TabIndex = 67;
            label140.Text = "Вместимость";
            label140.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label150
            // 
            label150.Location = new System.Drawing.Point(72, 1);
            label150.Name = "label150";
            label150.Size = new System.Drawing.Size(123, 21);
            label150.TabIndex = 67;
            label150.Text = "Уровень заклинания";
            label150.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label142
            // 
            label142.Location = new System.Drawing.Point(-2, 25);
            label142.Name = "label142";
            label142.Size = new System.Drawing.Size(48, 20);
            label142.TabIndex = 67;
            label142.Text = "Броня";
            label142.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label147
            // 
            label147.Location = new System.Drawing.Point(-3, -1);
            label147.Name = "label147";
            label147.Size = new System.Drawing.Size(33, 20);
            label147.TabIndex = 67;
            label147.Text = "AC";
            label147.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label143
            // 
            label143.Location = new System.Drawing.Point(133, 3);
            label143.Name = "label143";
            label143.Size = new System.Drawing.Size(17, 20);
            label143.TabIndex = 67;
            label143.Text = "d";
            label143.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label145
            // 
            label145.Location = new System.Drawing.Point(-2, 3);
            label145.Name = "label145";
            label145.Size = new System.Drawing.Size(84, 20);
            label145.TabIndex = 67;
            label145.Text = "Повреждения";
            label145.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label157
            // 
            label157.Location = new System.Drawing.Point(70, 52);
            label157.Name = "label157";
            label157.Size = new System.Drawing.Size(210, 20);
            label157.TabIndex = 67;
            label157.Text = "Текущее количество зарядов";
            label157.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label158
            // 
            label158.Location = new System.Drawing.Point(70, 26);
            label158.Name = "label158";
            label158.Size = new System.Drawing.Size(210, 20);
            label158.TabIndex = 67;
            label158.Text = "Количество зарядов посоха";
            label158.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label160
            // 
            label160.Location = new System.Drawing.Point(70, 0);
            label160.Name = "label160";
            label160.Size = new System.Drawing.Size(210, 20);
            label160.TabIndex = 67;
            label160.Text = "Необходимый минимальный уровень";
            label160.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label166
            // 
            label166.Location = new System.Drawing.Point(70, 52);
            label166.Name = "label166";
            label166.Size = new System.Drawing.Size(210, 20);
            label166.TabIndex = 67;
            label166.Text = "Текущее количество зарядов";
            label166.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label167
            // 
            label167.Location = new System.Drawing.Point(70, 26);
            label167.Name = "label167";
            label167.Size = new System.Drawing.Size(210, 20);
            label167.TabIndex = 67;
            label167.Text = "Количество зарядов палочки";
            label167.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label169
            // 
            label169.Location = new System.Drawing.Point(70, 0);
            label169.Name = "label169";
            label169.Size = new System.Drawing.Size(210, 20);
            label169.TabIndex = 67;
            label169.Text = "Необходимый минимальный уровень";
            label169.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label154
            // 
            label154.Location = new System.Drawing.Point(-3, 4);
            label154.Name = "label154";
            label154.Size = new System.Drawing.Size(204, 23);
            label154.TabIndex = 67;
            label154.Text = "Необходимый минимальный уровень";
            label154.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label161
            // 
            label161.Location = new System.Drawing.Point(71, 35);
            label161.Name = "label161";
            label161.Size = new System.Drawing.Size(152, 33);
            label161.TabIndex = 67;
            label161.Text = "-1 - вечный источник света\r\n0  - выжженый источник";
            label161.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label162
            // 
            label162.Location = new System.Drawing.Point(71, 1);
            label162.Name = "label162";
            label162.Size = new System.Drawing.Size(152, 33);
            label162.TabIndex = 67;
            label162.Text = "Запас свечения по времени (в игровых часах)";
            label162.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label128
            // 
            label128.Location = new System.Drawing.Point(250, 26);
            label128.Name = "label128";
            label128.Size = new System.Drawing.Size(132, 24);
            label128.TabIndex = 67;
            label128.Text = "Осталось применений";
            label128.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label129
            // 
            label129.Location = new System.Drawing.Point(250, 2);
            label129.Name = "label129";
            label129.Size = new System.Drawing.Size(74, 19);
            label129.TabIndex = 67;
            label129.Text = "Прототип";
            label129.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label130
            // 
            label130.Location = new System.Drawing.Point(84, 25);
            label130.Name = "label130";
            label130.Size = new System.Drawing.Size(74, 20);
            label130.TabIndex = 67;
            label130.Text = "Мин.уровень";
            label130.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label131
            // 
            label131.Location = new System.Drawing.Point(86, 0);
            label131.Name = "label131";
            label131.Size = new System.Drawing.Size(74, 20);
            label131.TabIndex = 67;
            label131.Text = "Лаг(сек.)";
            label131.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label132
            // 
            label132.Location = new System.Drawing.Point(-3, 26);
            label132.Name = "label132";
            label132.Size = new System.Drawing.Size(210, 20);
            label132.TabIndex = 67;
            label132.Text = "Текущее количество жидкости";
            label132.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label133
            // 
            label133.Location = new System.Drawing.Point(-3, 0);
            label133.Name = "label133";
            label133.Size = new System.Drawing.Size(210, 20);
            label133.TabIndex = 67;
            label133.Text = "Максимальный объем жидкости";
            label133.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label134
            // 
            label134.Location = new System.Drawing.Point(-3, 49);
            label134.Name = "label134";
            label134.Size = new System.Drawing.Size(120, 14);
            label134.TabIndex = 75;
            label134.Text = "Тип жидкости";
            label134.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label135
            // 
            label135.Location = new System.Drawing.Point(-3, 91);
            label135.Name = "label135";
            label135.Size = new System.Drawing.Size(210, 20);
            label135.TabIndex = 67;
            label135.Text = "Уровень яда (0 если не отравлено)";
            label135.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label136
            // 
            label136.Location = new System.Drawing.Point(109, 2);
            label136.Name = "label136";
            label136.Size = new System.Drawing.Size(104, 16);
            label136.TabIndex = 67;
            label136.Text = "Количество";
            label136.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label137
            // 
            label137.Location = new System.Drawing.Point(55, 22);
            label137.Name = "label137";
            label137.Size = new System.Drawing.Size(304, 33);
            label137.TabIndex = 67;
            label137.Text = "Уровень яда (0 - не отравлено, 1 - отравлено, >1 - таймер через сколько протухнет" +
    " в минутах)";
            label137.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label138
            // 
            label138.Location = new System.Drawing.Point(55, 1);
            label138.Name = "label138";
            label138.Size = new System.Drawing.Size(225, 20);
            label138.TabIndex = 67;
            label138.Text = "Количесво удовлетвояемых часов голода";
            label138.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1111
            // 
            label1111.Location = new System.Drawing.Point(-3, 26);
            label1111.Name = "label1111";
            label1111.Size = new System.Drawing.Size(210, 20);
            label1111.TabIndex = 67;
            label1111.Text = "Текущее количество жидкости";
            label1111.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label163
            // 
            label163.Location = new System.Drawing.Point(-3, 1);
            label163.Name = "label163";
            label163.Size = new System.Drawing.Size(210, 20);
            label163.TabIndex = 67;
            label163.Text = "Максимальный объем жидкости";
            label163.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label164
            // 
            label164.Location = new System.Drawing.Point(-3, 49);
            label164.Name = "label164";
            label164.Size = new System.Drawing.Size(120, 14);
            label164.TabIndex = 75;
            label164.Text = "Тип жидкости";
            label164.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label165
            // 
            label165.Location = new System.Drawing.Point(-3, 91);
            label165.Name = "label165";
            label165.Size = new System.Drawing.Size(210, 20);
            label165.TabIndex = 67;
            label165.Text = "Уровень яда (0 если не отравлено)";
            label165.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label123
            // 
            label123.Location = new System.Drawing.Point(-1, 40);
            label123.Name = "label123";
            label123.Size = new System.Drawing.Size(71, 16);
            label123.TabIndex = 41;
            label123.Text = "Описание";
            label123.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label125
            // 
            label125.Location = new System.Drawing.Point(-2, 3);
            label125.Name = "label125";
            label125.Size = new System.Drawing.Size(95, 16);
            label125.TabIndex = 40;
            label125.Text = "Список алиасов";
            label125.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label127
            // 
            label127.Location = new System.Drawing.Point(194, 26);
            label127.Name = "label127";
            label127.Size = new System.Drawing.Size(31, 16);
            label127.TabIndex = 11;
            label127.Text = "Тип";
            label127.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label170
            // 
            label170.Location = new System.Drawing.Point(78, 26);
            label170.Name = "label170";
            label170.Size = new System.Drawing.Size(43, 16);
            label170.TabIndex = 11;
            label170.Text = "Класс";
            label170.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label27
            // 
            label27.Location = new System.Drawing.Point(1, 49);
            label27.Name = "label27";
            label27.Size = new System.Drawing.Size(58, 16);
            label27.TabIndex = 5;
            label27.Text = "Алиасы";
            label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            label1.Location = new System.Drawing.Point(1, 26);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(31, 16);
            label1.TabIndex = 11;
            label1.Text = "Пол";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label31
            // 
            label31.Location = new System.Drawing.Point(1, 204);
            label31.Name = "label31";
            label31.Size = new System.Drawing.Size(84, 16);
            label31.TabIndex = 8;
            label31.Text = "Описание";
            label31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label24
            // 
            label24.Location = new System.Drawing.Point(1, 138);
            label24.Name = "label24";
            label24.Size = new System.Drawing.Size(49, 16);
            label24.TabIndex = 68;
            label24.Text = "Вин.";
            label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label25
            // 
            label25.Location = new System.Drawing.Point(1, 116);
            label25.Name = "label25";
            label25.Size = new System.Drawing.Size(49, 16);
            label25.TabIndex = 64;
            label25.Text = "Дат.";
            label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label26
            // 
            label26.Location = new System.Drawing.Point(1, 94);
            label26.Name = "label26";
            label26.Size = new System.Drawing.Size(49, 16);
            label26.TabIndex = 66;
            label26.Text = "Род.";
            label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label32
            // 
            label32.Location = new System.Drawing.Point(1, 72);
            label32.Name = "label32";
            label32.Size = new System.Drawing.Size(49, 16);
            label32.TabIndex = 67;
            label32.Text = "Имен.";
            label32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label59
            // 
            label59.Location = new System.Drawing.Point(1, 182);
            label59.Name = "label59";
            label59.Size = new System.Drawing.Size(49, 16);
            label59.TabIndex = 69;
            label59.Text = "Пред.";
            label59.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label60
            // 
            label60.Location = new System.Drawing.Point(1, 160);
            label60.Name = "label60";
            label60.Size = new System.Drawing.Size(49, 16);
            label60.TabIndex = 63;
            label60.Text = "Твор.";
            label60.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            label11.Location = new System.Drawing.Point(3, 44);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(52, 16);
            label11.TabIndex = 20;
            label11.Text = "Уровень";
            label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            label4.Location = new System.Drawing.Point(198, 194);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(172, 16);
            label4.TabIndex = 15;
            label4.Text = "Положение по умолчанию";
            label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            label3.Location = new System.Drawing.Point(2, 194);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(172, 16);
            label3.TabIndex = 14;
            label3.Text = "Положение при загрузке";
            label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label23
            // 
            label23.Location = new System.Drawing.Point(283, 44);
            label23.Name = "label23";
            label23.Size = new System.Drawing.Size(48, 16);
            label23.TabIndex = 8;
            label23.Text = "Хтролл";
            label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            label2.Location = new System.Drawing.Point(227, 45);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(25, 16);
            label2.TabIndex = 8;
            label2.Text = "AC";
            label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label19
            // 
            label19.Location = new System.Drawing.Point(3, 126);
            label19.Name = "label19";
            label19.Size = new System.Drawing.Size(104, 16);
            label19.TabIndex = 15;
            label19.Text = "Наклонность моба";
            label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            label14.Location = new System.Drawing.Point(4, 278);
            label14.Name = "label14";
            label14.Size = new System.Drawing.Size(38, 16);
            label14.TabIndex = 8;
            label14.Text = "Опыт";
            label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label21
            // 
            label21.Location = new System.Drawing.Point(199, 127);
            label21.Name = "label21";
            label21.Size = new System.Drawing.Size(104, 16);
            label21.TabIndex = 15;
            label21.Text = "Тип удара";
            label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label20
            // 
            label20.Location = new System.Drawing.Point(115, 278);
            label20.Name = "label20";
            label20.Size = new System.Drawing.Size(58, 16);
            label20.TabIndex = 15;
            label20.Text = "MaxFactor";
            label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label22
            // 
            label22.Location = new System.Drawing.Point(338, 36);
            label22.Name = "label22";
            label22.Size = new System.Drawing.Size(52, 27);
            label22.TabIndex = 8;
            label22.Text = "Макс.в мире";
            label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            label22.Visible = false;
            // 
            // label10
            // 
            label10.Location = new System.Drawing.Point(227, 3);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(52, 16);
            label10.TabIndex = 16;
            label10.Text = "Тело";
            label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            label6.Location = new System.Drawing.Point(59, 4);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(52, 16);
            label6.TabIndex = 22;
            label6.Text = "Инт.";
            label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            label9.Location = new System.Drawing.Point(283, 3);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(52, 16);
            label9.TabIndex = 17;
            label9.Text = "Обаяние";
            label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label18
            // 
            label18.Location = new System.Drawing.Point(177, 44);
            label18.Name = "label18";
            label18.Size = new System.Drawing.Size(52, 16);
            label18.TabIndex = 8;
            label18.Text = "Вес";
            label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label17
            // 
            label17.Location = new System.Drawing.Point(118, 44);
            label17.Name = "label17";
            label17.Size = new System.Drawing.Size(52, 16);
            label17.TabIndex = 8;
            label17.Text = "Рост";
            label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            label8.Location = new System.Drawing.Point(171, 3);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(52, 16);
            label8.TabIndex = 18;
            label8.Text = "Ловк.";
            label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            label7.Location = new System.Drawing.Point(115, 3);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(52, 16);
            label7.TabIndex = 21;
            label7.Text = "Мудр.";
            label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label16
            // 
            label16.Location = new System.Drawing.Point(61, 44);
            label16.Name = "label16";
            label16.Size = new System.Drawing.Size(52, 16);
            label16.TabIndex = 8;
            label16.Text = "Размер";
            label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            label12.Location = new System.Drawing.Point(182, 171);
            label12.Name = "label12";
            label12.Size = new System.Drawing.Size(188, 16);
            label12.TabIndex = 15;
            label12.Text = "Вер.примен. умения или спела";
            label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            label13.Location = new System.Drawing.Point(3, 171);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(120, 16);
            label13.TabIndex = 15;
            label13.Text = "Количество доп.атак";
            label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            label5.Location = new System.Drawing.Point(3, 3);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(52, 16);
            label5.TabIndex = 19;
            label5.Text = "Сила";
            label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gbOthers
            // 
            gbOthers.Controls.Add(label121);
            gbOthers.Controls.Add(label113);
            gbOthers.Controls.Add(label117);
            gbOthers.Controls.Add(label94);
            gbOthers.Controls.Add(label124);
            gbOthers.Controls.Add(label115);
            gbOthers.Controls.Add(label114);
            gbOthers.Controls.Add(this.nudPResist);
            gbOthers.Controls.Add(label116);
            gbOthers.Controls.Add(this.nudMResist);
            gbOthers.Controls.Add(label122);
            gbOthers.Controls.Add(label120);
            gbOthers.Controls.Add(this.nudAResist);
            gbOthers.Controls.Add(this.nudArmour);
            gbOthers.Controls.Add(this.nudAdsorb);
            gbOthers.Controls.Add(this.nudInitiative);
            gbOthers.Controls.Add(this.nudMem);
            gbOthers.Controls.Add(this.nudSuccess);
            gbOthers.Controls.Add(this.nudCastSuccess);
            gbOthers.Controls.Add(this.nudRegeneration);
            gbOthers.Dock = System.Windows.Forms.DockStyle.Top;
            gbOthers.Location = new System.Drawing.Point(2, 165);
            gbOthers.Name = "gbOthers";
            gbOthers.Size = new System.Drawing.Size(455, 143);
            gbOthers.TabIndex = 35;
            gbOthers.TabStop = false;
            gbOthers.Text = "Прочее";
            // 
            // label121
            // 
            label121.Location = new System.Drawing.Point(215, 18);
            label121.Name = "label121";
            label121.Size = new System.Drawing.Size(139, 13);
            label121.TabIndex = 39;
            label121.Text = "Инициатива";
            label121.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label113
            // 
            label113.Location = new System.Drawing.Point(4, 68);
            label113.Name = "label113";
            label113.Size = new System.Drawing.Size(139, 13);
            label113.TabIndex = 39;
            label113.Text = "Запоминание";
            label113.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label117
            // 
            label117.Location = new System.Drawing.Point(217, 44);
            label117.Name = "label117";
            label117.Size = new System.Drawing.Size(139, 13);
            label117.TabIndex = 39;
            label117.Text = "Поглощение";
            label117.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label94
            // 
            label94.Location = new System.Drawing.Point(215, 120);
            label94.Name = "label94";
            label94.Size = new System.Drawing.Size(139, 13);
            label94.TabIndex = 46;
            label94.Text = "Иммунитет к физ. повр-м";
            label94.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label124
            // 
            label124.Location = new System.Drawing.Point(215, 95);
            label124.Name = "label124";
            label124.Size = new System.Drawing.Size(139, 13);
            label124.TabIndex = 46;
            label124.Text = "Иммунитет к маг. повр-м";
            label124.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label115
            // 
            label115.Location = new System.Drawing.Point(4, 18);
            label115.Name = "label115";
            label115.Size = new System.Drawing.Size(139, 13);
            label115.TabIndex = 39;
            label115.Text = "Регенерация";
            label115.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label114
            // 
            label114.Location = new System.Drawing.Point(216, 68);
            label114.Name = "label114";
            label114.Size = new System.Drawing.Size(139, 13);
            label114.TabIndex = 46;
            label114.Text = "Иммунитет к маг. афф-м";
            label114.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudPResist
            // 
            this.nudPResist.Location = new System.Drawing.Point(366, 116);
            this.nudPResist.Name = "nudPResist";
            this.nudPResist.Size = new System.Drawing.Size(51, 20);
            this.nudPResist.TabIndex = 20;
            this.nudPResist.ValueChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // label116
            // 
            label116.Location = new System.Drawing.Point(3, 41);
            label116.Name = "label116";
            label116.Size = new System.Drawing.Size(139, 17);
            label116.TabIndex = 46;
            label116.Text = "Броня";
            label116.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudMResist
            // 
            this.nudMResist.Location = new System.Drawing.Point(366, 91);
            this.nudMResist.Name = "nudMResist";
            this.nudMResist.Size = new System.Drawing.Size(51, 20);
            this.nudMResist.TabIndex = 20;
            this.nudMResist.ValueChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // label122
            // 
            label122.Location = new System.Drawing.Point(3, 91);
            label122.Name = "label122";
            label122.Size = new System.Drawing.Size(139, 17);
            label122.TabIndex = 46;
            label122.Text = "Успех колдовства";
            label122.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label120
            // 
            label120.Location = new System.Drawing.Point(3, 116);
            label120.Name = "label120";
            label120.Size = new System.Drawing.Size(139, 17);
            label120.TabIndex = 46;
            label120.Text = "Удача";
            label120.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudAResist
            // 
            this.nudAResist.Location = new System.Drawing.Point(366, 65);
            this.nudAResist.Name = "nudAResist";
            this.nudAResist.Size = new System.Drawing.Size(51, 20);
            this.nudAResist.TabIndex = 19;
            this.nudAResist.ValueChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // nudArmour
            // 
            this.nudArmour.Location = new System.Drawing.Point(149, 41);
            this.nudArmour.Name = "nudArmour";
            this.nudArmour.Size = new System.Drawing.Size(51, 20);
            this.nudArmour.TabIndex = 13;
            this.nudArmour.ValueChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // nudAdsorb
            // 
            this.nudAdsorb.Location = new System.Drawing.Point(366, 41);
            this.nudAdsorb.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nudAdsorb.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.nudAdsorb.Name = "nudAdsorb";
            this.nudAdsorb.Size = new System.Drawing.Size(51, 20);
            this.nudAdsorb.TabIndex = 18;
            this.nudAdsorb.ValueChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // nudInitiative
            // 
            this.nudInitiative.Location = new System.Drawing.Point(366, 15);
            this.nudInitiative.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nudInitiative.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.nudInitiative.Name = "nudInitiative";
            this.nudInitiative.Size = new System.Drawing.Size(51, 20);
            this.nudInitiative.TabIndex = 17;
            this.nudInitiative.ValueChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // nudMem
            // 
            this.nudMem.Location = new System.Drawing.Point(149, 65);
            this.nudMem.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nudMem.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.nudMem.Name = "nudMem";
            this.nudMem.Size = new System.Drawing.Size(51, 20);
            this.nudMem.TabIndex = 14;
            this.nudMem.ValueChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // nudSuccess
            // 
            this.nudSuccess.Location = new System.Drawing.Point(149, 116);
            this.nudSuccess.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nudSuccess.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.nudSuccess.Name = "nudSuccess";
            this.nudSuccess.Size = new System.Drawing.Size(51, 20);
            this.nudSuccess.TabIndex = 16;
            this.nudSuccess.ValueChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // nudCastSuccess
            // 
            this.nudCastSuccess.Location = new System.Drawing.Point(149, 91);
            this.nudCastSuccess.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nudCastSuccess.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.nudCastSuccess.Name = "nudCastSuccess";
            this.nudCastSuccess.Size = new System.Drawing.Size(51, 20);
            this.nudCastSuccess.TabIndex = 15;
            this.nudCastSuccess.ValueChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // nudRegeneration
            // 
            this.nudRegeneration.Location = new System.Drawing.Point(149, 15);
            this.nudRegeneration.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nudRegeneration.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.nudRegeneration.Name = "nudRegeneration";
            this.nudRegeneration.Size = new System.Drawing.Size(51, 20);
            this.nudRegeneration.TabIndex = 12;
            this.nudRegeneration.ValueChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // gbResists
            // 
            gbResists.Controls.Add(label71);
            gbResists.Controls.Add(this.nudResistDark);
            gbResists.Controls.Add(label119);
            gbResists.Controls.Add(label111);
            gbResists.Controls.Add(label109);
            gbResists.Controls.Add(this.nudResEarth);
            gbResists.Controls.Add(this.nudResAir);
            gbResists.Controls.Add(label110);
            gbResists.Controls.Add(label108);
            gbResists.Controls.Add(this.nudResWater);
            gbResists.Controls.Add(this.nudResFire);
            gbResists.Controls.Add(label112);
            gbResists.Controls.Add(this.nudVitality);
            gbResists.Controls.Add(this.nudMind);
            gbResists.Controls.Add(label118);
            gbResists.Controls.Add(this.nudImmun);
            gbResists.Dock = System.Windows.Forms.DockStyle.Top;
            gbResists.Location = new System.Drawing.Point(2, 46);
            gbResists.Name = "gbResists";
            gbResists.Size = new System.Drawing.Size(455, 119);
            gbResists.TabIndex = 34;
            gbResists.TabStop = false;
            gbResists.Text = "Резисты";
            // 
            // label71
            // 
            label71.Location = new System.Drawing.Point(216, 94);
            label71.Name = "label71";
            label71.Size = new System.Drawing.Size(139, 13);
            label71.TabIndex = 48;
            label71.Text = "Тьмы";
            label71.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudResistDark
            // 
            this.nudResistDark.Location = new System.Drawing.Point(366, 93);
            this.nudResistDark.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.nudResistDark.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.nudResistDark.Name = "nudResistDark";
            this.nudResistDark.Size = new System.Drawing.Size(51, 20);
            this.nudResistDark.TabIndex = 47;
            this.nudResistDark.ValueChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // label119
            // 
            label119.Location = new System.Drawing.Point(216, 69);
            label119.Name = "label119";
            label119.Size = new System.Drawing.Size(139, 13);
            label119.TabIndex = 39;
            label119.Text = "Иммунитет";
            label119.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label111
            // 
            label111.Location = new System.Drawing.Point(3, 93);
            label111.Name = "label111";
            label111.Size = new System.Drawing.Size(139, 13);
            label111.TabIndex = 39;
            label111.Text = "Стихии земли";
            label111.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label109
            // 
            label109.Location = new System.Drawing.Point(3, 68);
            label109.Name = "label109";
            label109.Size = new System.Drawing.Size(139, 13);
            label109.TabIndex = 39;
            label109.Text = "Стихии воздуха";
            label109.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudResEarth
            // 
            this.nudResEarth.Location = new System.Drawing.Point(149, 93);
            this.nudResEarth.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.nudResEarth.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.nudResEarth.Name = "nudResEarth";
            this.nudResEarth.Size = new System.Drawing.Size(51, 20);
            this.nudResEarth.TabIndex = 8;
            this.nudResEarth.ValueChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // nudResAir
            // 
            this.nudResAir.Location = new System.Drawing.Point(149, 67);
            this.nudResAir.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.nudResAir.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.nudResAir.Name = "nudResAir";
            this.nudResAir.Size = new System.Drawing.Size(51, 20);
            this.nudResAir.TabIndex = 7;
            this.nudResAir.ValueChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // label110
            // 
            label110.Location = new System.Drawing.Point(3, 40);
            label110.Name = "label110";
            label110.Size = new System.Drawing.Size(139, 16);
            label110.TabIndex = 40;
            label110.Text = "Стихии воды";
            label110.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label108
            // 
            label108.Location = new System.Drawing.Point(3, 15);
            label108.Name = "label108";
            label108.Size = new System.Drawing.Size(139, 16);
            label108.TabIndex = 40;
            label108.Text = "Стихии огня";
            label108.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudResWater
            // 
            this.nudResWater.Location = new System.Drawing.Point(149, 41);
            this.nudResWater.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.nudResWater.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.nudResWater.Name = "nudResWater";
            this.nudResWater.Size = new System.Drawing.Size(51, 20);
            this.nudResWater.TabIndex = 6;
            this.nudResWater.ValueChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // nudResFire
            // 
            this.nudResFire.Location = new System.Drawing.Point(149, 15);
            this.nudResFire.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.nudResFire.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.nudResFire.Name = "nudResFire";
            this.nudResFire.Size = new System.Drawing.Size(51, 20);
            this.nudResFire.TabIndex = 5;
            this.nudResFire.ValueChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // label112
            // 
            label112.Location = new System.Drawing.Point(216, 15);
            label112.Name = "label112";
            label112.Size = new System.Drawing.Size(139, 17);
            label112.TabIndex = 46;
            label112.Text = "Живучесть";
            label112.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudVitality
            // 
            this.nudVitality.Location = new System.Drawing.Point(366, 15);
            this.nudVitality.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.nudVitality.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.nudVitality.Name = "nudVitality";
            this.nudVitality.Size = new System.Drawing.Size(51, 20);
            this.nudVitality.TabIndex = 9;
            this.nudVitality.ValueChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // nudMind
            // 
            this.nudMind.Location = new System.Drawing.Point(366, 41);
            this.nudMind.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.nudMind.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.nudMind.Name = "nudMind";
            this.nudMind.Size = new System.Drawing.Size(51, 20);
            this.nudMind.TabIndex = 10;
            this.nudMind.ValueChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // label118
            // 
            label118.Location = new System.Drawing.Point(216, 41);
            label118.Name = "label118";
            label118.Size = new System.Drawing.Size(139, 17);
            label118.TabIndex = 46;
            label118.Text = "Разум";
            label118.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudImmun
            // 
            this.nudImmun.Location = new System.Drawing.Point(366, 67);
            this.nudImmun.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.nudImmun.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.nudImmun.Name = "nudImmun";
            this.nudImmun.Size = new System.Drawing.Size(51, 20);
            this.nudImmun.TabIndex = 11;
            this.nudImmun.ValueChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // label107
            // 
            label107.Location = new System.Drawing.Point(318, 18);
            label107.Name = "label107";
            label107.Size = new System.Drawing.Size(54, 17);
            label107.TabIndex = 39;
            label107.Text = "Реакция";
            label107.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label104
            // 
            label104.Location = new System.Drawing.Point(3, 18);
            label104.Name = "label104";
            label104.Size = new System.Drawing.Size(46, 17);
            label104.TabIndex = 41;
            label104.Text = "Воля";
            label104.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label105
            // 
            label105.Location = new System.Drawing.Point(100, 18);
            label105.Name = "label105";
            label105.Size = new System.Drawing.Size(61, 17);
            label105.TabIndex = 40;
            label105.Text = "Здоровье";
            label105.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label106
            // 
            label106.Location = new System.Drawing.Point(208, 18);
            label106.Name = "label106";
            label106.Size = new System.Drawing.Size(71, 17);
            label106.TabIndex = 38;
            label106.Text = "Стойкость";
            label106.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label68
            // 
            label68.Location = new System.Drawing.Point(109, 26);
            label68.Name = "label68";
            label68.Size = new System.Drawing.Size(83, 22);
            label68.TabIndex = 68;
            label68.Text = "Валюта";
            label68.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label74
            // 
            label74.Location = new System.Drawing.Point(263, 4);
            label74.Name = "label74";
            label74.Size = new System.Drawing.Size(83, 16);
            label74.TabIndex = 67;
            label74.Text = "Ремортов";
            label74.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmsMainTree
            // 
            this.cmsMainTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAddItems,
            this.tsmiRemoveItems,
            this.toolStripMenuItem3,
            this.tsmiAddTemplate,
            this.tsmiCreateClones,
            this.toolStripMenuItem2,
            this.tsmiCopy,
            this.tsmiPaste,
            this.tsmiPasteAsTemplate,
            this.toolStripMenuItem4,
            this.tsmiInfo,
            this.tsmiShowRoomOnMap});
            this.cmsMainTree.Name = "cmsMainTree";
            this.cmsMainTree.Size = new System.Drawing.Size(260, 220);
            this.cmsMainTree.Opening += new System.ComponentModel.CancelEventHandler(this.CmsMainTreeOpening);
            // 
            // tsmiAddItems
            // 
            this.tsmiAddItems.Image = global::BZEditor.Properties.Resources.button_createsmth;
            this.tsmiAddItems.Name = "tsmiAddItems";
            this.tsmiAddItems.Size = new System.Drawing.Size(259, 22);
            this.tsmiAddItems.Text = "Создать";
            this.tsmiAddItems.Click += new System.EventHandler(this.TsbAddItemsClick);
            // 
            // tsmiRemoveItems
            // 
            this.tsmiRemoveItems.Image = global::BZEditor.Properties.Resources.button_removesmth;
            this.tsmiRemoveItems.Name = "tsmiRemoveItems";
            this.tsmiRemoveItems.Size = new System.Drawing.Size(259, 22);
            this.tsmiRemoveItems.Text = "Удалить";
            this.tsmiRemoveItems.Click += new System.EventHandler(this.TsbRemoveItemsClick);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(256, 6);
            // 
            // tsmiAddTemplate
            // 
            this.tsmiAddTemplate.Image = global::BZEditor.Properties.Resources.button_addtemplate;
            this.tsmiAddTemplate.Name = "tsmiAddTemplate";
            this.tsmiAddTemplate.Size = new System.Drawing.Size(259, 22);
            this.tsmiAddTemplate.Text = "Создать шаблон";
            this.tsmiAddTemplate.Click += new System.EventHandler(this.TsbAddTemplateClick);
            // 
            // tsmiCreateClones
            // 
            this.tsmiCreateClones.Image = global::BZEditor.Properties.Resources.button_clone;
            this.tsmiCreateClones.Name = "tsmiCreateClones";
            this.tsmiCreateClones.Size = new System.Drawing.Size(259, 22);
            this.tsmiCreateClones.Text = "Клонировать";
            this.tsmiCreateClones.Click += new System.EventHandler(this.TsbCreateClonesClick);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(256, 6);
            // 
            // tsmiCopy
            // 
            this.tsmiCopy.Image = global::BZEditor.Properties.Resources.button_copy;
            this.tsmiCopy.Name = "tsmiCopy";
            this.tsmiCopy.Size = new System.Drawing.Size(259, 22);
            this.tsmiCopy.Text = "Копировать";
            this.tsmiCopy.Click += new System.EventHandler(this.TsbCopyClick);
            // 
            // tsmiPaste
            // 
            this.tsmiPaste.Image = global::BZEditor.Properties.Resources.button_paste;
            this.tsmiPaste.Name = "tsmiPaste";
            this.tsmiPaste.Size = new System.Drawing.Size(259, 22);
            this.tsmiPaste.Text = "Вставить скопированное";
            this.tsmiPaste.Click += new System.EventHandler(this.TsbPasteClick);
            // 
            // tsmiPasteAsTemplate
            // 
            this.tsmiPasteAsTemplate.Image = global::BZEditor.Properties.Resources.button_pastetemplate;
            this.tsmiPasteAsTemplate.Name = "tsmiPasteAsTemplate";
            this.tsmiPasteAsTemplate.Size = new System.Drawing.Size(259, 22);
            this.tsmiPasteAsTemplate.Text = "Применить как шаблон";
            this.tsmiPasteAsTemplate.Click += new System.EventHandler(this.TsbPasteAsTemplateClick);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(256, 6);
            // 
            // tsmiInfo
            // 
            this.tsmiInfo.Image = global::BZEditor.Properties.Resources.button_info;
            this.tsmiInfo.Name = "tsmiInfo";
            this.tsmiInfo.Size = new System.Drawing.Size(259, 22);
            this.tsmiInfo.Text = "Навигация/Сводная информация";
            this.tsmiInfo.Click += new System.EventHandler(this.TsmiInfoClick);
            // 
            // tsmiShowRoomOnMap
            // 
            this.tsmiShowRoomOnMap.Image = global::BZEditor.Properties.Resources.button_show_room_on_map;
            this.tsmiShowRoomOnMap.Name = "tsmiShowRoomOnMap";
            this.tsmiShowRoomOnMap.Size = new System.Drawing.Size(259, 22);
            this.tsmiShowRoomOnMap.Text = "Показать комнату на карте";
            this.tsmiShowRoomOnMap.Click += new System.EventHandler(this.TsmiShowRoomOnMapClick);
            // 
            // cmsNavigation
            // 
            this.cmsNavigation.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiNavigateTo});
            this.cmsNavigation.Name = "cmsNavigation";
            this.cmsNavigation.Size = new System.Drawing.Size(140, 26);
            // 
            // tsmiNavigateTo
            // 
            this.tsmiNavigateTo.Image = global::BZEditor.Properties.Resources.button_gotosomething;
            this.tsmiNavigateTo.Name = "tsmiNavigateTo";
            this.tsmiNavigateTo.Size = new System.Drawing.Size(139, 22);
            this.tsmiNavigateTo.Text = "Перейти к...";
            this.tsmiNavigateTo.Click += new System.EventHandler(this.TsmiNavigateToClick);
            // 
            // iListIcons16
            // 
            this.iListIcons16.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iListIcons16.ImageStream")));
            this.iListIcons16.TransparentColor = System.Drawing.Color.Transparent;
            this.iListIcons16.Images.SetKeyName(0, "");
            this.iListIcons16.Images.SetKeyName(1, "");
            this.iListIcons16.Images.SetKeyName(2, "");
            this.iListIcons16.Images.SetKeyName(3, "");
            this.iListIcons16.Images.SetKeyName(4, "");
            this.iListIcons16.Images.SetKeyName(5, "");
            this.iListIcons16.Images.SetKeyName(6, "");
            this.iListIcons16.Images.SetKeyName(7, "");
            this.iListIcons16.Images.SetKeyName(8, "");
            this.iListIcons16.Images.SetKeyName(9, "");
            this.iListIcons16.Images.SetKeyName(10, "");
            this.iListIcons16.Images.SetKeyName(11, "");
            this.iListIcons16.Images.SetKeyName(12, "");
            this.iListIcons16.Images.SetKeyName(13, "");
            this.iListIcons16.Images.SetKeyName(14, "");
            this.iListIcons16.Images.SetKeyName(15, "");
            this.iListIcons16.Images.SetKeyName(16, "");
            this.iListIcons16.Images.SetKeyName(17, "");
            this.iListIcons16.Images.SetKeyName(18, "");
            this.iListIcons16.Images.SetKeyName(19, "монета.png");
            this.iListIcons16.Images.SetKeyName(20, "");
            this.iListIcons16.Images.SetKeyName(21, "");
            this.iListIcons16.Images.SetKeyName(22, "");
            this.iListIcons16.Images.SetKeyName(23, "");
            this.iListIcons16.Images.SetKeyName(24, "");
            this.iListIcons16.Images.SetKeyName(25, "");
            this.iListIcons16.Images.SetKeyName(26, "");
            this.iListIcons16.Images.SetKeyName(27, "");
            this.iListIcons16.Images.SetKeyName(28, "");
            this.iListIcons16.Images.SetKeyName(29, "сундук.png");
            this.iListIcons16.Images.SetKeyName(30, "");
            this.iListIcons16.Images.SetKeyName(31, "");
            this.iListIcons16.Images.SetKeyName(32, "");
            this.iListIcons16.Images.SetKeyName(33, "");
            this.iListIcons16.Images.SetKeyName(34, "броня.png");
            this.iListIcons16.Images.SetKeyName(35, "мусор.png");
            this.iListIcons16.Images.SetKeyName(36, "огнестр_оружие.png");
            this.iListIcons16.Images.SetKeyName(37, "бумага.png");
            this.iListIcons16.Images.SetKeyName(38, "свиток.png");
            this.iListIcons16.Images.SetKeyName(39, "посох.png");
            this.iListIcons16.Images.SetKeyName(40, "рубаха.png");
            this.iListIcons16.Images.SetKeyName(41, "свеча.png");
            this.iListIcons16.Images.SetKeyName(42, "плащ.png");
            this.iListIcons16.Images.SetKeyName(43, "ключ.png");
            this.iListIcons16.Images.SetKeyName(44, "фонтан.png");
            this.iListIcons16.Images.SetKeyName(45, "gg_connecting.png");
            this.iListIcons16.Images.SetKeyName(46, "desc.png");
            this.iListIcons16.Images.SetKeyName(47, "unsaved_changes.png");
            this.iListIcons16.Images.SetKeyName(48, "button_empty.png");
            this.iListIcons16.Images.SetKeyName(49, "bandage.png");
            // 
            // cmsGridMenu
            // 
            this.cmsGridMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAddRow,
            this.tsmiRemoveRow,
            this.toolStripMenuItem1,
            this.tsmiGoTo});
            this.cmsGridMenu.Name = "cmsGridMenu";
            this.cmsGridMenu.Size = new System.Drawing.Size(140, 76);
            // 
            // tsmiAddRow
            // 
            this.tsmiAddRow.Image = ((System.Drawing.Image)(resources.GetObject("tsmiAddRow.Image")));
            this.tsmiAddRow.Name = "tsmiAddRow";
            this.tsmiAddRow.Size = new System.Drawing.Size(139, 22);
            this.tsmiAddRow.Text = "Добавить";
            this.tsmiAddRow.Click += new System.EventHandler(this.TsmiAddRowClick);
            // 
            // tsmiRemoveRow
            // 
            this.tsmiRemoveRow.Image = ((System.Drawing.Image)(resources.GetObject("tsmiRemoveRow.Image")));
            this.tsmiRemoveRow.Name = "tsmiRemoveRow";
            this.tsmiRemoveRow.Size = new System.Drawing.Size(139, 22);
            this.tsmiRemoveRow.Text = "Удалить";
            this.tsmiRemoveRow.Click += new System.EventHandler(this.TsmiRemoveRowClick);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(136, 6);
            // 
            // tsmiGoTo
            // 
            this.tsmiGoTo.Image = ((System.Drawing.Image)(resources.GetObject("tsmiGoTo.Image")));
            this.tsmiGoTo.Name = "tsmiGoTo";
            this.tsmiGoTo.Size = new System.Drawing.Size(139, 22);
            this.tsmiGoTo.Text = "Перейти к...";
            this.tsmiGoTo.Click += new System.EventHandler(this.TsmiGoToClick);
            // 
            // cmsRoomsDescription
            // 
            this.cmsRoomsDescription.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCopyDesc,
            this.tsmiCutDesc,
            this.toolStripMenuItem5,
            this.tsmiPasteDesc});
            this.cmsRoomsDescription.Name = "cmsRoomsDescription";
            this.cmsRoomsDescription.Size = new System.Drawing.Size(140, 76);
            // 
            // tsmiCopyDesc
            // 
            this.tsmiCopyDesc.Image = global::BZEditor.Properties.Resources.button_copy;
            this.tsmiCopyDesc.Name = "tsmiCopyDesc";
            this.tsmiCopyDesc.Size = new System.Drawing.Size(139, 22);
            this.tsmiCopyDesc.Text = "Копировать";
            this.tsmiCopyDesc.Click += new System.EventHandler(this.TsmiCopyDescClick);
            // 
            // tsmiCutDesc
            // 
            this.tsmiCutDesc.Image = global::BZEditor.Properties.Resources.button_cut;
            this.tsmiCutDesc.Name = "tsmiCutDesc";
            this.tsmiCutDesc.Size = new System.Drawing.Size(139, 22);
            this.tsmiCutDesc.Text = "Вырезать";
            this.tsmiCutDesc.Click += new System.EventHandler(this.TsmiCutDescClick);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(136, 6);
            // 
            // tsmiPasteDesc
            // 
            this.tsmiPasteDesc.Image = global::BZEditor.Properties.Resources.button_paste;
            this.tsmiPasteDesc.Name = "tsmiPasteDesc";
            this.tsmiPasteDesc.Size = new System.Drawing.Size(139, 22);
            this.tsmiPasteDesc.Text = "Вставить";
            this.tsmiPasteDesc.Click += new System.EventHandler(this.TsmiPasteDescClick);
            // 
            // syntaxDocument
            // 
            this.syntaxDocument.Lines = new string[] {
        ""};
            this.syntaxDocument.MaxUndoBufferSize = 1000;
            this.syntaxDocument.Modified = false;
            this.syntaxDocument.UndoStep = 0;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.Size = new System.Drawing.Size(781, 372);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // toolTip
            // 
            this.toolTip.ShowAlways = true;
            this.toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip.ToolTipTitle = "Подсказка";
            // 
            // cbIsertSpaces
            // 
            this.cbIsertSpaces.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbIsertSpaces.Checked = true;
            this.cbIsertSpaces.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIsertSpaces.Location = new System.Drawing.Point(3, 17);
            this.cbIsertSpaces.Name = "cbIsertSpaces";
            this.cbIsertSpaces.Size = new System.Drawing.Size(94, 18);
            this.cbIsertSpaces.TabIndex = 105;
            this.cbIsertSpaces.Text = "Выравнивать";
            this.toolTip.SetToolTip(this.cbIsertSpaces, "Выравнивание по ширине");
            this.cbIsertSpaces.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(3, 17);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(94, 18);
            this.checkBox2.TabIndex = 105;
            this.checkBox2.Text = "Выравнивать";
            this.toolTip.SetToolTip(this.checkBox2, "Выравнивание по ширине");
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox4.Location = new System.Drawing.Point(684, 74);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(83, 19);
            this.checkBox4.TabIndex = 98;
            this.checkBox4.Text = "Замещать";
            this.toolTip.SetToolTip(this.checkBox4, "Описание замещает общее");
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // nudOptimalCharsInGroup
            // 
            this.nudOptimalCharsInGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudOptimalCharsInGroup.Location = new System.Drawing.Point(222, 285);
            this.nudOptimalCharsInGroup.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudOptimalCharsInGroup.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudOptimalCharsInGroup.Name = "nudOptimalCharsInGroup";
            this.nudOptimalCharsInGroup.Size = new System.Drawing.Size(80, 20);
            this.nudOptimalCharsInGroup.TabIndex = 11;
            this.toolTip.SetToolTip(this.nudOptimalCharsInGroup, "Параметр задает оптимальное количество\r\nигроков в группе для групповых зон.\r\n1 = " +
        "Не групповая зона");
            this.nudOptimalCharsInGroup.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudOptimalCharsInGroup.ValueChanged += new System.EventHandler(this.NudOptimalCharsInGroupValueChanged);
            // 
            // btnAddAZones
            // 
            this.btnAddAZones.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddAZones.Image = ((System.Drawing.Image)(resources.GetObject("btnAddAZones.Image")));
            this.btnAddAZones.Location = new System.Drawing.Point(444, 18);
            this.btnAddAZones.Name = "btnAddAZones";
            this.btnAddAZones.Size = new System.Drawing.Size(28, 28);
            this.btnAddAZones.TabIndex = 11;
            this.toolTip.SetToolTip(this.btnAddAZones, "Добавить зону в список");
            this.btnAddAZones.Click += new System.EventHandler(this.BtnAddAZonesClick);
            // 
            // btnRemoveAZones
            // 
            this.btnRemoveAZones.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveAZones.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveAZones.Image")));
            this.btnRemoveAZones.Location = new System.Drawing.Point(444, 49);
            this.btnRemoveAZones.Name = "btnRemoveAZones";
            this.btnRemoveAZones.Size = new System.Drawing.Size(28, 28);
            this.btnRemoveAZones.TabIndex = 12;
            this.toolTip.SetToolTip(this.btnRemoveAZones, "Исключить зону из списка");
            this.btnRemoveAZones.Click += new System.EventHandler(this.BtnRemoveAZonesClick);
            // 
            // btnAddBZones
            // 
            this.btnAddBZones.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddBZones.Image = ((System.Drawing.Image)(resources.GetObject("btnAddBZones.Image")));
            this.btnAddBZones.Location = new System.Drawing.Point(444, 18);
            this.btnAddBZones.Name = "btnAddBZones";
            this.btnAddBZones.Size = new System.Drawing.Size(28, 28);
            this.btnAddBZones.TabIndex = 14;
            this.toolTip.SetToolTip(this.btnAddBZones, "Добавить зону в список");
            this.btnAddBZones.Click += new System.EventHandler(this.BtnAddBZonesClick);
            // 
            // btnRemoveBZones
            // 
            this.btnRemoveBZones.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveBZones.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveBZones.Image")));
            this.btnRemoveBZones.Location = new System.Drawing.Point(444, 51);
            this.btnRemoveBZones.Name = "btnRemoveBZones";
            this.btnRemoveBZones.Size = new System.Drawing.Size(28, 28);
            this.btnRemoveBZones.TabIndex = 15;
            this.toolTip.SetToolTip(this.btnRemoveBZones, "Исключить зону из списка");
            this.btnRemoveBZones.Click += new System.EventHandler(this.BtnRemoveBZonesClick);
            // 
            // btnValidate
            // 
            this.btnValidate.Image = global::BZEditor.Properties.Resources.validate;
            this.btnValidate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnValidate.Location = new System.Drawing.Point(3, 3);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Size = new System.Drawing.Size(85, 22);
            this.btnValidate.TabIndex = 2;
            this.btnValidate.Text = "Проверить";
            this.btnValidate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip.SetToolTip(this.btnValidate, "Проверка зоны на соответствие правилам");
            this.btnValidate.UseVisualStyleBackColor = true;
            this.btnValidate.Click += new System.EventHandler(this.BtnValidateClick);
            // 
            // tbExitSouth
            // 
            this.tbExitSouth.AllowError = true;
            this.tbExitSouth.DecimalPlace = 0;
            this.tbExitSouth.Location = new System.Drawing.Point(69, 73);
            this.tbExitSouth.Name = "tbExitSouth";
            this.tbExitSouth.Size = new System.Drawing.Size(54, 17);
            this.tbExitSouth.TabIndex = 10;
            this.toolTip.SetToolTip(this.tbExitSouth, "Виртуальный номер целевой комнаты");
            this.tbExitSouth.Validated += new System.EventHandler(this.ExitDirChanged);
            // 
            // tbExitNorth
            // 
            this.tbExitNorth.AllowError = true;
            this.tbExitNorth.DecimalPlace = 0;
            this.tbExitNorth.Location = new System.Drawing.Point(69, 20);
            this.tbExitNorth.Name = "tbExitNorth";
            this.tbExitNorth.Size = new System.Drawing.Size(54, 17);
            this.tbExitNorth.TabIndex = 9;
            this.toolTip.SetToolTip(this.tbExitNorth, "Виртуальный номер целевой комнаты");
            this.tbExitNorth.Validated += new System.EventHandler(this.ExitDirChanged);
            // 
            // tbExitDown
            // 
            this.tbExitDown.AllowError = true;
            this.tbExitDown.DecimalPlace = 0;
            this.tbExitDown.Location = new System.Drawing.Point(200, 73);
            this.tbExitDown.Name = "tbExitDown";
            this.tbExitDown.Size = new System.Drawing.Size(54, 17);
            this.tbExitDown.TabIndex = 13;
            this.toolTip.SetToolTip(this.tbExitDown, "Виртуальный номер целевой комнаты");
            this.tbExitDown.Validated += new System.EventHandler(this.ExitDirChanged);
            // 
            // tbExitUp
            // 
            this.tbExitUp.AllowError = true;
            this.tbExitUp.DecimalPlace = 0;
            this.tbExitUp.Location = new System.Drawing.Point(200, 20);
            this.tbExitUp.Name = "tbExitUp";
            this.tbExitUp.Size = new System.Drawing.Size(54, 17);
            this.tbExitUp.TabIndex = 12;
            this.toolTip.SetToolTip(this.tbExitUp, "Виртуальный номер целевой комнаты");
            this.tbExitUp.Validated += new System.EventHandler(this.ExitDirChanged);
            // 
            // tbExitEast
            // 
            this.tbExitEast.AllowError = true;
            this.tbExitEast.DecimalPlace = 0;
            this.tbExitEast.Location = new System.Drawing.Point(109, 46);
            this.tbExitEast.Name = "tbExitEast";
            this.tbExitEast.Size = new System.Drawing.Size(54, 17);
            this.tbExitEast.TabIndex = 11;
            this.toolTip.SetToolTip(this.tbExitEast, "Виртуальный номер целевой комнаты");
            this.tbExitEast.Validated += new System.EventHandler(this.ExitDirChanged);
            // 
            // tbExitWest
            // 
            this.tbExitWest.AllowError = true;
            this.tbExitWest.DecimalPlace = 0;
            this.tbExitWest.Location = new System.Drawing.Point(27, 46);
            this.tbExitWest.Name = "tbExitWest";
            this.tbExitWest.Size = new System.Drawing.Size(54, 17);
            this.tbExitWest.TabIndex = 10;
            this.toolTip.SetToolTip(this.tbExitWest, "Виртуальный номер целевой комнаты");
            this.tbExitWest.Validated += new System.EventHandler(this.ExitDirChanged);
            // 
            // label93
            // 
            this.label93.AutoSize = true;
            this.label93.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label93.ForeColor = System.Drawing.Color.DarkBlue;
            this.label93.Location = new System.Drawing.Point(214, 15);
            this.label93.Name = "label93";
            this.label93.Size = new System.Drawing.Size(13, 13);
            this.label93.TabIndex = 123;
            this.label93.Text = "?";
            this.toolTip.SetToolTip(this.label93, resources.GetString("label93.ToolTip"));
            // 
            // btnSelectDoorKey
            // 
            this.btnSelectDoorKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSelectDoorKey.Enabled = false;
            this.btnSelectDoorKey.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectDoorKey.Image")));
            this.btnSelectDoorKey.Location = new System.Drawing.Point(113, 96);
            this.btnSelectDoorKey.Name = "btnSelectDoorKey";
            this.btnSelectDoorKey.Size = new System.Drawing.Size(24, 24);
            this.btnSelectDoorKey.TabIndex = 120;
            this.btnSelectDoorKey.Text = "...";
            this.toolTip.SetToolTip(this.btnSelectDoorKey, "Кнопка выбора моба-продавца из списка");
            this.btnSelectDoorKey.UseVisualStyleBackColor = true;
            this.btnSelectDoorKey.Click += new System.EventHandler(this.BtnSelectDoorKeyClick);
            // 
            // tbDoorNameVin
            // 
            this.tbDoorNameVin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDoorNameVin.Enabled = false;
            this.tbDoorNameVin.Location = new System.Drawing.Point(329, 131);
            this.tbDoorNameVin.Name = "tbDoorNameVin";
            this.tbDoorNameVin.Size = new System.Drawing.Size(185, 20);
            this.tbDoorNameVin.TabIndex = 105;
            this.toolTip.SetToolTip(this.tbDoorNameVin, "Например:\r\nИмярек открыл скрипучую резную калитку");
            this.tbDoorNameVin.Validated += new System.EventHandler(this.TbDoorNameVinValidated);
            // 
            // btnRoomAddObj
            // 
            this.btnRoomAddObj.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRoomAddObj.Image = ((System.Drawing.Image)(resources.GetObject("btnRoomAddObj.Image")));
            this.btnRoomAddObj.Location = new System.Drawing.Point(488, 2);
            this.btnRoomAddObj.Name = "btnRoomAddObj";
            this.btnRoomAddObj.Size = new System.Drawing.Size(28, 28);
            this.btnRoomAddObj.TabIndex = 45;
            this.toolTip.SetToolTip(this.btnRoomAddObj, "Добавить помощника мобу.");
            this.btnRoomAddObj.Click += new System.EventHandler(this.BtnRoomAddObjClick);
            // 
            // btnRoomRemoveObj
            // 
            this.btnRoomRemoveObj.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRoomRemoveObj.Image = ((System.Drawing.Image)(resources.GetObject("btnRoomRemoveObj.Image")));
            this.btnRoomRemoveObj.Location = new System.Drawing.Point(488, 32);
            this.btnRoomRemoveObj.Name = "btnRoomRemoveObj";
            this.btnRoomRemoveObj.Size = new System.Drawing.Size(28, 28);
            this.btnRoomRemoveObj.TabIndex = 44;
            this.toolTip.SetToolTip(this.btnRoomRemoveObj, "Удалить помощника, выбранного в списке");
            this.btnRoomRemoveObj.Click += new System.EventHandler(this.BtnRoomRemoveObjClick);
            // 
            // elvRoomObjInObj
            // 
            this.elvRoomObjInObj.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.elvRoomObjInObj.ContextMenuStrip = this.cmsGridMenu;
            this.elvRoomObjInObj.FullRowSelect = true;
            this.elvRoomObjInObj.GridLines = true;
            this.elvRoomObjInObj.LabelWrap = false;
            this.elvRoomObjInObj.Location = new System.Drawing.Point(3, 14);
            this.elvRoomObjInObj.MultiSelect = false;
            this.elvRoomObjInObj.Name = "elvRoomObjInObj";
            this.elvRoomObjInObj.OwnerDraw = true;
            this.elvRoomObjInObj.Size = new System.Drawing.Size(480, 130);
            this.elvRoomObjInObj.TabIndex = 97;
            this.toolTip.SetToolTip(this.elvRoomObjInObj, "Даблклик на ячейке \"вероятность\" выводит\r\nэту ячейку в режим редактирования");
            this.elvRoomObjInObj.UseCompatibleStateImageBehavior = false;
            this.elvRoomObjInObj.View = System.Windows.Forms.View.Details;
            this.elvRoomObjInObj.ItemValueChanged += new ExtControls.ExtListView.ItemValueChangeEvent(this.ElvRoomObjInObjItemValueChanged);
            this.elvRoomObjInObj.SizeChanged += new System.EventHandler(this.LastColumnAutosize);
            this.elvRoomObjInObj.MouseUp += new System.Windows.Forms.MouseEventHandler(this.NavigatedControl_MouseUp);
            // 
            // btnRoomAddObjInObj
            // 
            this.btnRoomAddObjInObj.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRoomAddObjInObj.Image = ((System.Drawing.Image)(resources.GetObject("btnRoomAddObjInObj.Image")));
            this.btnRoomAddObjInObj.Location = new System.Drawing.Point(485, 14);
            this.btnRoomAddObjInObj.Name = "btnRoomAddObjInObj";
            this.btnRoomAddObjInObj.Size = new System.Drawing.Size(28, 28);
            this.btnRoomAddObjInObj.TabIndex = 96;
            this.toolTip.SetToolTip(this.btnRoomAddObjInObj, "Добавить умение мобу.");
            this.btnRoomAddObjInObj.Click += new System.EventHandler(this.BtnRoomAddObjInObjClick);
            // 
            // btnRoomRemoveObjFromObj
            // 
            this.btnRoomRemoveObjFromObj.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRoomRemoveObjFromObj.Image = ((System.Drawing.Image)(resources.GetObject("btnRoomRemoveObjFromObj.Image")));
            this.btnRoomRemoveObjFromObj.Location = new System.Drawing.Point(485, 44);
            this.btnRoomRemoveObjFromObj.Name = "btnRoomRemoveObjFromObj";
            this.btnRoomRemoveObjFromObj.Size = new System.Drawing.Size(28, 28);
            this.btnRoomRemoveObjFromObj.TabIndex = 95;
            this.toolTip.SetToolTip(this.btnRoomRemoveObjFromObj, "Удалить умение моба, выбранное в списке");
            this.btnRoomRemoveObjFromObj.Click += new System.EventHandler(this.BtnRoomRemoveObjFromObjClick);
            // 
            // btnRoomAddMob
            // 
            this.btnRoomAddMob.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRoomAddMob.Image = ((System.Drawing.Image)(resources.GetObject("btnRoomAddMob.Image")));
            this.btnRoomAddMob.Location = new System.Drawing.Point(488, 2);
            this.btnRoomAddMob.Name = "btnRoomAddMob";
            this.btnRoomAddMob.Size = new System.Drawing.Size(28, 28);
            this.btnRoomAddMob.TabIndex = 96;
            this.toolTip.SetToolTip(this.btnRoomAddMob, "Добавить моба в комнату.");
            this.btnRoomAddMob.Click += new System.EventHandler(this.BtnRoomAddMobClick);
            // 
            // btnRoomRemoveMob
            // 
            this.btnRoomRemoveMob.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRoomRemoveMob.Image = ((System.Drawing.Image)(resources.GetObject("btnRoomRemoveMob.Image")));
            this.btnRoomRemoveMob.Location = new System.Drawing.Point(488, 32);
            this.btnRoomRemoveMob.Name = "btnRoomRemoveMob";
            this.btnRoomRemoveMob.Size = new System.Drawing.Size(28, 28);
            this.btnRoomRemoveMob.TabIndex = 95;
            this.toolTip.SetToolTip(this.btnRoomRemoveMob, "Удалить моба, выбранного в списке");
            this.btnRoomRemoveMob.Click += new System.EventHandler(this.BtnRoomRemoveMobClick);
            // 
            // nudMaxInRoom
            // 
            this.nudMaxInRoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudMaxInRoom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudMaxInRoom.Enabled = false;
            this.nudMaxInRoom.Location = new System.Drawing.Point(454, 17);
            this.nudMaxInRoom.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudMaxInRoom.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMaxInRoom.Name = "nudMaxInRoom";
            this.nudMaxInRoom.Size = new System.Drawing.Size(61, 20);
            this.nudMaxInRoom.TabIndex = 84;
            this.toolTip.SetToolTip(this.nudMaxInRoom, "Максимальное количество мобов\r\nвыбранного типа в комнате.");
            this.nudMaxInRoom.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMaxInRoom.Validated += new System.EventHandler(this.NudMaxInRoomValidated);
            // 
            // label85
            // 
            this.label85.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label85.Location = new System.Drawing.Point(451, 0);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(71, 16);
            this.label85.TabIndex = 82;
            this.label85.Text = "max в комн.";
            this.label85.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip.SetToolTip(this.label85, "Максимум мобов такого типа в комнате");
            // 
            // btnRoomSpecFormatCommonDesc
            // 
            this.btnRoomSpecFormatCommonDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRoomSpecFormatCommonDesc.Image = ((System.Drawing.Image)(resources.GetObject("btnRoomSpecFormatCommonDesc.Image")));
            this.btnRoomSpecFormatCommonDesc.Location = new System.Drawing.Point(35, 3);
            this.btnRoomSpecFormatCommonDesc.Name = "btnRoomSpecFormatCommonDesc";
            this.btnRoomSpecFormatCommonDesc.Size = new System.Drawing.Size(28, 28);
            this.btnRoomSpecFormatCommonDesc.TabIndex = 44;
            this.toolTip.SetToolTip(this.btnRoomSpecFormatCommonDesc, "Выровнять по ширине\r\n(без сохранения абзацев).");
            this.btnRoomSpecFormatCommonDesc.Click += new System.EventHandler(this.BtnRoomFormatClick);
            // 
            // btnRoomSpellCheckCommonDesc
            // 
            this.btnRoomSpellCheckCommonDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRoomSpellCheckCommonDesc.Image = ((System.Drawing.Image)(resources.GetObject("btnRoomSpellCheckCommonDesc.Image")));
            this.btnRoomSpellCheckCommonDesc.Location = new System.Drawing.Point(3, 3);
            this.btnRoomSpellCheckCommonDesc.Name = "btnRoomSpellCheckCommonDesc";
            this.btnRoomSpellCheckCommonDesc.Size = new System.Drawing.Size(28, 28);
            this.btnRoomSpellCheckCommonDesc.TabIndex = 44;
            this.toolTip.SetToolTip(this.btnRoomSpellCheckCommonDesc, "Проверка ошибок.");
            this.btnRoomSpellCheckCommonDesc.Click += new System.EventHandler(this.BtnRoomSpellCheckCommonDescClick);
            // 
            // cbInsertSpaces
            // 
            this.cbInsertSpaces.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbInsertSpaces.Location = new System.Drawing.Point(2, 78);
            this.cbInsertSpaces.Name = "cbInsertSpaces";
            this.cbInsertSpaces.Size = new System.Drawing.Size(94, 18);
            this.cbInsertSpaces.TabIndex = 105;
            this.cbInsertSpaces.Text = "Выравнивать";
            this.toolTip.SetToolTip(this.cbInsertSpaces, "Выравнивание по ширине");
            this.cbInsertSpaces.UseVisualStyleBackColor = true;
            this.cbInsertSpaces.Visible = false;
            // 
            // btnRoomFormatCommonDesc
            // 
            this.btnRoomFormatCommonDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRoomFormatCommonDesc.Image = ((System.Drawing.Image)(resources.GetObject("btnRoomFormatCommonDesc.Image")));
            this.btnRoomFormatCommonDesc.Location = new System.Drawing.Point(66, 3);
            this.btnRoomFormatCommonDesc.Name = "btnRoomFormatCommonDesc";
            this.btnRoomFormatCommonDesc.Size = new System.Drawing.Size(28, 28);
            this.btnRoomFormatCommonDesc.TabIndex = 44;
            this.btnRoomFormatCommonDesc.Text = " ";
            this.toolTip.SetToolTip(this.btnRoomFormatCommonDesc, "Выровнять по ширине\r\n(с сохранением абзацев).");
            this.btnRoomFormatCommonDesc.Click += new System.EventHandler(this.BtnRoomFormatClick);
            // 
            // btnObjSetAutoCases
            // 
            this.btnObjSetAutoCases.Font = new System.Drawing.Font("Wingdings 3", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnObjSetAutoCases.Location = new System.Drawing.Point(36, 49);
            this.btnObjSetAutoCases.Name = "btnObjSetAutoCases";
            this.btnObjSetAutoCases.Size = new System.Drawing.Size(20, 20);
            this.btnObjSetAutoCases.TabIndex = 5;
            this.btnObjSetAutoCases.Text = "?";
            this.toolTip.SetToolTip(this.btnObjSetAutoCases, resources.GetString("btnObjSetAutoCases.ToolTip"));
            this.btnObjSetAutoCases.UseVisualStyleBackColor = true;
            this.btnObjSetAutoCases.Click += new System.EventHandler(this.BtnObjSetAutoCasesClick);
            // 
            // tboxObjAliases
            // 
            this.tboxObjAliases.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tboxObjAliases.Location = new System.Drawing.Point(57, 26);
            this.tboxObjAliases.Name = "tboxObjAliases";
            this.tboxObjAliases.Size = new System.Drawing.Size(294, 20);
            this.tboxObjAliases.TabIndex = 3;
            this.toolTip.SetToolTip(this.tboxObjAliases, "Перечень ключевых слов для объекта\r\n(в качетсве разделителя \"пробел\").");
            this.tboxObjAliases.TextChanged += new System.EventHandler(this.ObjValueValidated);
            // 
            // tboxObjTvor
            // 
            this.tboxObjTvor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tboxObjTvor.Location = new System.Drawing.Point(57, 141);
            this.tboxObjTvor.Name = "tboxObjTvor";
            this.tboxObjTvor.Size = new System.Drawing.Size(294, 20);
            this.tboxObjTvor.TabIndex = 9;
            this.toolTip.SetToolTip(this.tboxObjTvor, "Название объекта в творительном падеже\r\n(отвечает на вопрос \"Любуюсь чем?\")");
            this.tboxObjTvor.TextChanged += new System.EventHandler(this.ObjValueValidated);
            // 
            // tboxObjVin
            // 
            this.tboxObjVin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tboxObjVin.Location = new System.Drawing.Point(57, 118);
            this.tboxObjVin.Name = "tboxObjVin";
            this.tboxObjVin.Size = new System.Drawing.Size(294, 20);
            this.tboxObjVin.TabIndex = 8;
            this.toolTip.SetToolTip(this.tboxObjVin, "Название объекта в винительном падеже\r\n(отвечает на вопрос \"Вижу что?\")");
            this.tboxObjVin.TextChanged += new System.EventHandler(this.ObjValueValidated);
            // 
            // tboxObjDat
            // 
            this.tboxObjDat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tboxObjDat.Location = new System.Drawing.Point(57, 95);
            this.tboxObjDat.Name = "tboxObjDat";
            this.tboxObjDat.Size = new System.Drawing.Size(294, 20);
            this.tboxObjDat.TabIndex = 7;
            this.toolTip.SetToolTip(this.tboxObjDat, "Название объекта в дательном падеже\r\n(отвечает на вопрос \"Рад чему?\")");
            this.tboxObjDat.TextChanged += new System.EventHandler(this.ObjValueValidated);
            // 
            // tboxObjPredl
            // 
            this.tboxObjPredl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tboxObjPredl.Location = new System.Drawing.Point(57, 164);
            this.tboxObjPredl.Name = "tboxObjPredl";
            this.tboxObjPredl.Size = new System.Drawing.Size(294, 20);
            this.tboxObjPredl.TabIndex = 10;
            this.toolTip.SetToolTip(this.tboxObjPredl, "Название объекта в предложном падеже\r\n(отвечает на вопрос \"Говорю о чём?\")");
            this.tboxObjPredl.TextChanged += new System.EventHandler(this.ObjValueValidated);
            // 
            // tboxObjImen
            // 
            this.tboxObjImen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tboxObjImen.Location = new System.Drawing.Point(57, 49);
            this.tboxObjImen.Name = "tboxObjImen";
            this.tboxObjImen.Size = new System.Drawing.Size(294, 20);
            this.tboxObjImen.TabIndex = 4;
            this.toolTip.SetToolTip(this.tboxObjImen, "Название объекта в именительном падеже\r\n(отвечает на вопрос \"ЧТО?\")");
            this.tboxObjImen.TextChanged += new System.EventHandler(this.ObjValueValidated);
            this.tboxObjImen.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TboxObjImenKeyUp);
            // 
            // tboxObjRod
            // 
            this.tboxObjRod.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tboxObjRod.Location = new System.Drawing.Point(57, 72);
            this.tboxObjRod.Name = "tboxObjRod";
            this.tboxObjRod.Size = new System.Drawing.Size(294, 20);
            this.tboxObjRod.TabIndex = 6;
            this.toolTip.SetToolTip(this.tboxObjRod, "Название объекта в родительном падеже\r\n(отвечает на вопрос \"Нет чего?\")");
            this.tboxObjRod.TextChanged += new System.EventHandler(this.ObjValueValidated);
            // 
            // nudObjContainerKeyVNum
            // 
            this.nudObjContainerKeyVNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudObjContainerKeyVNum.Location = new System.Drawing.Point(76, 31);
            this.nudObjContainerKeyVNum.Maximum = new decimal(new int[] {
            -1486618625,
            232830643,
            0,
            0});
            this.nudObjContainerKeyVNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.nudObjContainerKeyVNum.Name = "nudObjContainerKeyVNum";
            this.nudObjContainerKeyVNum.Size = new System.Drawing.Size(50, 20);
            this.nudObjContainerKeyVNum.TabIndex = 2;
            this.toolTip.SetToolTip(this.nudObjContainerKeyVNum, "Виртуальный номер ключа (-1, если нет ключа)");
            this.nudObjContainerKeyVNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudObjContainerKeyVNum.ValueChanged += new System.EventHandler(this.ObjTypeSpecParamChanged);
            // 
            // nudObjContainerValue
            // 
            this.nudObjContainerValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudObjContainerValue.Location = new System.Drawing.Point(77, 6);
            this.nudObjContainerValue.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudObjContainerValue.Name = "nudObjContainerValue";
            this.nudObjContainerValue.Size = new System.Drawing.Size(50, 20);
            this.nudObjContainerValue.TabIndex = 1;
            this.toolTip.SetToolTip(this.nudObjContainerValue, "Максимальный вес внутри");
            this.nudObjContainerValue.ValueChanged += new System.EventHandler(this.ObjTypeSpecParamChanged);
            // 
            // btnSelectFontPorionProto
            // 
            this.btnSelectFontPorionProto.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectFontPorionProto.Image")));
            this.btnSelectFontPorionProto.Location = new System.Drawing.Point(193, 64);
            this.btnSelectFontPorionProto.Name = "btnSelectFontPorionProto";
            this.btnSelectFontPorionProto.Size = new System.Drawing.Size(24, 24);
            this.btnSelectFontPorionProto.TabIndex = 4;
            this.btnSelectFontPorionProto.Text = "...";
            this.toolTip.SetToolTip(this.btnSelectFontPorionProto, "Кнопка выбора зелья-прототипа\r\nиз списка загруженных зелий");
            this.btnSelectFontPorionProto.UseVisualStyleBackColor = true;
            this.btnSelectFontPorionProto.Visible = false;
            this.btnSelectFontPorionProto.Click += new System.EventHandler(this.BtnSelectPotionProtoClick);
            // 
            // btnSelectPotionProtoVNum
            // 
            this.btnSelectPotionProtoVNum.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectPotionProtoVNum.Image")));
            this.btnSelectPotionProtoVNum.Location = new System.Drawing.Point(193, 64);
            this.btnSelectPotionProtoVNum.Name = "btnSelectPotionProtoVNum";
            this.btnSelectPotionProtoVNum.Size = new System.Drawing.Size(24, 24);
            this.btnSelectPotionProtoVNum.TabIndex = 4;
            this.btnSelectPotionProtoVNum.Text = "...";
            this.toolTip.SetToolTip(this.btnSelectPotionProtoVNum, "Кнопка выбора зелья-прототипа\r\nиз списка загруженных зелий");
            this.btnSelectPotionProtoVNum.UseVisualStyleBackColor = true;
            this.btnSelectPotionProtoVNum.Visible = false;
            this.btnSelectPotionProtoVNum.Click += new System.EventHandler(this.BtnSelectPotionProtoClick);
            // 
            // btnAddObjTrigger
            // 
            this.btnAddObjTrigger.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddObjTrigger.Image = ((System.Drawing.Image)(resources.GetObject("btnAddObjTrigger.Image")));
            this.btnAddObjTrigger.Location = new System.Drawing.Point(379, 2);
            this.btnAddObjTrigger.Name = "btnAddObjTrigger";
            this.btnAddObjTrigger.Size = new System.Drawing.Size(28, 28);
            this.btnAddObjTrigger.TabIndex = 40;
            this.toolTip.SetToolTip(this.btnAddObjTrigger, "Добавить помощника мобу.");
            this.btnAddObjTrigger.Click += new System.EventHandler(this.BtnAddObjTriggerClick);
            // 
            // btnObjRemoveTrigger
            // 
            this.btnObjRemoveTrigger.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnObjRemoveTrigger.Image = ((System.Drawing.Image)(resources.GetObject("btnObjRemoveTrigger.Image")));
            this.btnObjRemoveTrigger.Location = new System.Drawing.Point(409, 2);
            this.btnObjRemoveTrigger.Name = "btnObjRemoveTrigger";
            this.btnObjRemoveTrigger.Size = new System.Drawing.Size(28, 28);
            this.btnObjRemoveTrigger.TabIndex = 39;
            this.toolTip.SetToolTip(this.btnObjRemoveTrigger, "Удалить помощника, выбранного в списке");
            this.btnObjRemoveTrigger.Click += new System.EventHandler(this.BtnObjRemoveTriggerClick);
            // 
            // btnObjReplaceAddDesc
            // 
            this.btnObjReplaceAddDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnObjReplaceAddDesc.Image = ((System.Drawing.Image)(resources.GetObject("btnObjReplaceAddDesc.Image")));
            this.btnObjReplaceAddDesc.Location = new System.Drawing.Point(409, 74);
            this.btnObjReplaceAddDesc.Name = "btnObjReplaceAddDesc";
            this.btnObjReplaceAddDesc.Size = new System.Drawing.Size(28, 28);
            this.btnObjReplaceAddDesc.TabIndex = 85;
            this.toolTip.SetToolTip(this.btnObjReplaceAddDesc, "Применить изменения описания предмета\r\nвыбранного в списке имеющихся.");
            this.btnObjReplaceAddDesc.Click += new System.EventHandler(this.BtnObjReplaceAddDescClick);
            // 
            // btnObjAddAddDesc
            // 
            this.btnObjAddAddDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnObjAddAddDesc.Image = ((System.Drawing.Image)(resources.GetObject("btnObjAddAddDesc.Image")));
            this.btnObjAddAddDesc.Location = new System.Drawing.Point(409, 45);
            this.btnObjAddAddDesc.Name = "btnObjAddAddDesc";
            this.btnObjAddAddDesc.Size = new System.Drawing.Size(28, 28);
            this.btnObjAddAddDesc.TabIndex = 36;
            this.toolTip.SetToolTip(this.btnObjAddAddDesc, "Добавить описание предмету.");
            this.btnObjAddAddDesc.Click += new System.EventHandler(this.BtnObjAddAddDescClick);
            // 
            // btnObjRemoveAddDesc
            // 
            this.btnObjRemoveAddDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnObjRemoveAddDesc.Image = ((System.Drawing.Image)(resources.GetObject("btnObjRemoveAddDesc.Image")));
            this.btnObjRemoveAddDesc.Location = new System.Drawing.Point(409, 103);
            this.btnObjRemoveAddDesc.Name = "btnObjRemoveAddDesc";
            this.btnObjRemoveAddDesc.Size = new System.Drawing.Size(28, 28);
            this.btnObjRemoveAddDesc.TabIndex = 35;
            this.toolTip.SetToolTip(this.btnObjRemoveAddDesc, "Удалить доп.описание предмета\r\nвыбранное в списке.");
            this.btnObjRemoveAddDesc.Click += new System.EventHandler(this.BtnObjRemoveAddDescClick);
            // 
            // btnMobSetAutoCases
            // 
            this.btnMobSetAutoCases.Font = new System.Drawing.Font("Wingdings 3", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnMobSetAutoCases.Location = new System.Drawing.Point(35, 70);
            this.btnMobSetAutoCases.Name = "btnMobSetAutoCases";
            this.btnMobSetAutoCases.Size = new System.Drawing.Size(20, 20);
            this.btnMobSetAutoCases.TabIndex = 7;
            this.btnMobSetAutoCases.Text = "?";
            this.toolTip.SetToolTip(this.btnMobSetAutoCases, resources.GetString("btnMobSetAutoCases.ToolTip"));
            this.btnMobSetAutoCases.UseVisualStyleBackColor = true;
            this.btnMobSetAutoCases.Click += new System.EventHandler(this.btnSetAutoCases_Click);
            // 
            // tboxMobNameTvor
            // 
            this.tboxMobNameTvor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tboxMobNameTvor.Location = new System.Drawing.Point(48, 158);
            this.tboxMobNameTvor.Name = "tboxMobNameTvor";
            this.tboxMobNameTvor.Size = new System.Drawing.Size(288, 20);
            this.tboxMobNameTvor.TabIndex = 11;
            this.toolTip.SetToolTip(this.tboxMobNameTvor, "Название моба в творительном падеже\r\n(отвечает на вопрос \"Любуюсь кем?\")");
            this.tboxMobNameTvor.TextChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // cboxMobSex
            // 
            this.cboxMobSex.DropDownWidth = 150;
            this.cboxMobSex.ItemHeight = 13;
            this.cboxMobSex.Location = new System.Drawing.Point(29, 25);
            this.cboxMobSex.Name = "cboxMobSex";
            this.cboxMobSex.Size = new System.Drawing.Size(50, 21);
            this.cboxMobSex.TabIndex = 2;
            this.toolTip.SetToolTip(this.cboxMobSex, "Пол моба.");
            this.cboxMobSex.SelectedValueChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // tboxMobAliases
            // 
            this.tboxMobAliases.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tboxMobAliases.Location = new System.Drawing.Point(48, 48);
            this.tboxMobAliases.Name = "tboxMobAliases";
            this.tboxMobAliases.Size = new System.Drawing.Size(288, 20);
            this.tboxMobAliases.TabIndex = 5;
            this.toolTip.SetToolTip(this.tboxMobAliases, "Перечень ключевых слов для моба\r\n(в качетсве разделителя \"пробел\").");
            this.tboxMobAliases.TextChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // tboxMobNameVin
            // 
            this.tboxMobNameVin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tboxMobNameVin.Location = new System.Drawing.Point(48, 136);
            this.tboxMobNameVin.Name = "tboxMobNameVin";
            this.tboxMobNameVin.Size = new System.Drawing.Size(288, 20);
            this.tboxMobNameVin.TabIndex = 10;
            this.toolTip.SetToolTip(this.tboxMobNameVin, "Название моба в винительном падеже\r\n(отвечает на вопрос \"Вижу кого?\")");
            this.tboxMobNameVin.TextChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // tboxMobNameDat
            // 
            this.tboxMobNameDat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tboxMobNameDat.Location = new System.Drawing.Point(48, 114);
            this.tboxMobNameDat.Name = "tboxMobNameDat";
            this.tboxMobNameDat.Size = new System.Drawing.Size(288, 20);
            this.tboxMobNameDat.TabIndex = 9;
            this.toolTip.SetToolTip(this.tboxMobNameDat, "Название моба в дательном падеже\r\n(отвечает на вопрос \"Рад кому?\")");
            this.tboxMobNameDat.TextChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // tboxMobNameRod
            // 
            this.tboxMobNameRod.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tboxMobNameRod.Location = new System.Drawing.Point(48, 92);
            this.tboxMobNameRod.Name = "tboxMobNameRod";
            this.tboxMobNameRod.Size = new System.Drawing.Size(288, 20);
            this.tboxMobNameRod.TabIndex = 8;
            this.toolTip.SetToolTip(this.tboxMobNameRod, "Название моба в родительном падеже\r\n(отвечает на вопрос \"Нет кого?\")");
            this.tboxMobNameRod.TextChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // tboxMobNameImen
            // 
            this.tboxMobNameImen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tboxMobNameImen.Location = new System.Drawing.Point(56, 70);
            this.tboxMobNameImen.Name = "tboxMobNameImen";
            this.tboxMobNameImen.Size = new System.Drawing.Size(280, 20);
            this.tboxMobNameImen.TabIndex = 6;
            this.toolTip.SetToolTip(this.tboxMobNameImen, "Название моба в именительном падеже\r\n(отвечает на вопрос \"КТО?\")");
            this.tboxMobNameImen.TextChanged += new System.EventHandler(this.MobValueChanged);
            this.tboxMobNameImen.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tboxMobNameImen_KeyUp);
            // 
            // tboxMobNamePred
            // 
            this.tboxMobNamePred.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tboxMobNamePred.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.tboxMobNamePred.Location = new System.Drawing.Point(48, 180);
            this.tboxMobNamePred.Name = "tboxMobNamePred";
            this.tboxMobNamePred.Size = new System.Drawing.Size(288, 20);
            this.tboxMobNamePred.TabIndex = 12;
            this.toolTip.SetToolTip(this.tboxMobNamePred, "Название моба в предложном падеже\r\n(отвечает на вопрос \"Говорю о ком?\")");
            this.tboxMobNamePred.TextChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // tboxMobDesc
            // 
            this.tboxMobDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tboxMobDesc.Location = new System.Drawing.Point(4, 223);
            this.tboxMobDesc.Name = "tboxMobDesc";
            this.tboxMobDesc.Size = new System.Drawing.Size(332, 20);
            this.tboxMobDesc.TabIndex = 13;
            this.toolTip.SetToolTip(this.tboxMobDesc, "Описание моба одной строкой.\r\nТо, что видит игрок, при входе\r\nв комнату с мобом.");
            this.tboxMobDesc.TextChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // nudMobHitroll
            // 
            this.nudMobHitroll.Location = new System.Drawing.Point(286, 62);
            this.nudMobHitroll.Name = "nudMobHitroll";
            this.nudMobHitroll.Size = new System.Drawing.Size(52, 20);
            this.nudMobHitroll.TabIndex = 12;
            this.toolTip.SetToolTip(this.nudMobHitroll, "Хитролл моба.\r\nОт 0[плохо] до 100[хорошо].\r\nЧем больше значение этого параметра,\r" +
        "\nтем больше вероятность успешного \r\nпрохождения физ.атаки этого моба.");
            this.nudMobHitroll.ValueChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // nudMobAC
            // 
            this.nudMobAC.Location = new System.Drawing.Point(230, 62);
            this.nudMobAC.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudMobAC.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            -2147483648});
            this.nudMobAC.Name = "nudMobAC";
            this.nudMobAC.Size = new System.Drawing.Size(52, 20);
            this.nudMobAC.TabIndex = 11;
            this.toolTip.SetToolTip(this.nudMobAC, "АС моба.\r\nЧем AC меньше, тем меньше вероятность\r\nуспешного прохождения по мобу фи" +
        "з.атаки");
            this.nudMobAC.ValueChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // nudMobMaxInWorld
            // 
            this.nudMobMaxInWorld.Location = new System.Drawing.Point(341, 62);
            this.nudMobMaxInWorld.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.nudMobMaxInWorld.Name = "nudMobMaxInWorld";
            this.nudMobMaxInWorld.Size = new System.Drawing.Size(52, 20);
            this.nudMobMaxInWorld.TabIndex = 13;
            this.toolTip.SetToolTip(this.nudMobMaxInWorld, "Максимальное количество \r\nмобов такого типа в мире.");
            this.nudMobMaxInWorld.Visible = false;
            this.nudMobMaxInWorld.ValueChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // dctrlMobHP
            // 
            this.dctrlMobHP.BackColor = System.Drawing.SystemColors.Control;
            this.dctrlMobHP.LabelText = "Количество хитов";
            this.dctrlMobHP.Location = new System.Drawing.Point(6, 84);
            this.dctrlMobHP.MinRandomValue = -10;
            this.dctrlMobHP.Name = "dctrlMobHP";
            this.dctrlMobHP.Param1 = 0;
            this.dctrlMobHP.Param1Max = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.dctrlMobHP.Param2 = 0;
            this.dctrlMobHP.Param2Max = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.dctrlMobHP.ParamConst = 0;
            this.dctrlMobHP.ParamConstMax = new decimal(new int[] {
            5000000,
            0,
            0,
            0});
            this.dctrlMobHP.SignFixed = true;
            this.dctrlMobHP.Size = new System.Drawing.Size(193, 42);
            this.dctrlMobHP.TabIndex = 14;
            this.toolTip.SetToolTip(this.dctrlMobHP, "Максимальное количество жизни у моба.");
            this.dctrlMobHP.Value = "0d0+0";
            this.dctrlMobHP.ValueChanged += new BZEditor.UcDiceControl.ValueChangeEvent(this.MobValueChanged);
            // 
            // dctrlMobAttack
            // 
            this.dctrlMobAttack.LabelText = "Сила удара";
            this.dctrlMobAttack.Location = new System.Drawing.Point(207, 84);
            this.dctrlMobAttack.MinRandomValue = 1;
            this.dctrlMobAttack.Name = "dctrlMobAttack";
            this.dctrlMobAttack.Param1 = 0;
            this.dctrlMobAttack.Param1Max = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.dctrlMobAttack.Param2 = 0;
            this.dctrlMobAttack.Param2Max = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.dctrlMobAttack.ParamConst = 0;
            this.dctrlMobAttack.ParamConstMax = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.dctrlMobAttack.SignFixed = true;
            this.dctrlMobAttack.Size = new System.Drawing.Size(193, 42);
            this.dctrlMobAttack.TabIndex = 15;
            this.toolTip.SetToolTip(this.dctrlMobAttack, "Сила удара моба.");
            this.dctrlMobAttack.Value = "0d0+0";
            this.dctrlMobAttack.ValueChanged += new BZEditor.UcDiceControl.ValueChangeEvent(this.MobValueChanged);
            // 
            // nudMobWeight
            // 
            this.nudMobWeight.Location = new System.Drawing.Point(174, 62);
            this.nudMobWeight.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudMobWeight.Name = "nudMobWeight";
            this.nudMobWeight.Size = new System.Drawing.Size(52, 20);
            this.nudMobWeight.TabIndex = 10;
            this.toolTip.SetToolTip(this.nudMobWeight, "Вес моба.");
            this.nudMobWeight.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudMobWeight.ValueChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // nudMobSize
            // 
            this.nudMobSize.Location = new System.Drawing.Point(62, 62);
            this.nudMobSize.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudMobSize.Name = "nudMobSize";
            this.nudMobSize.Size = new System.Drawing.Size(52, 20);
            this.nudMobSize.TabIndex = 8;
            this.toolTip.SetToolTip(this.nudMobSize, "Размер моба.");
            this.nudMobSize.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudMobSize.ValueChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // nudMobExpa
            // 
            this.nudMobExpa.Location = new System.Drawing.Point(7, 295);
            this.nudMobExpa.Maximum = new decimal(new int[] {
            -1981284353,
            -1966660860,
            0,
            0});
            this.nudMobExpa.Name = "nudMobExpa";
            this.nudMobExpa.Size = new System.Drawing.Size(106, 20);
            this.nudMobExpa.TabIndex = 23;
            this.toolTip.SetToolTip(this.nudMobExpa, "Количество очков опыта, которое\r\nполучит игрок за первое убийсво моба");
            this.nudMobExpa.ValueChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // btnSelectMobPath
            // 
            this.btnSelectMobPath.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSelectMobPath.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectMobPath.Image")));
            this.btnSelectMobPath.Location = new System.Drawing.Point(6, 253);
            this.btnSelectMobPath.Name = "btnSelectMobPath";
            this.btnSelectMobPath.Size = new System.Drawing.Size(71, 22);
            this.btnSelectMobPath.TabIndex = 22;
            this.btnSelectMobPath.Text = "Изменить";
            this.toolTip.SetToolTip(this.btnSelectMobPath, "Путь, по которому перемещается моб.\r\nДля изменения: \r\n1.Нажать кнопку\r\n2.Выделить" +
        " комнаты на карте\r\n3.Еще раз нажать кнопку\r\n(можно править вручную в строке)");
            this.btnSelectMobPath.Click += new System.EventHandler(this.btnSelectMobPath_Click);
            // 
            // tboxMobDestination
            // 
            this.tboxMobDestination.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tboxMobDestination.Location = new System.Drawing.Point(79, 254);
            this.tboxMobDestination.Name = "tboxMobDestination";
            this.tboxMobDestination.ReadOnly = true;
            this.tboxMobDestination.Size = new System.Drawing.Size(319, 20);
            this.tboxMobDestination.TabIndex = 17;
            this.toolTip.SetToolTip(this.tboxMobDestination, "Путь, по которому перемещается моб.\r\nДля изменения: \r\n1.Нажать кнопку\r\n2.Выделить" +
        " комнаты на карте\r\n3.Еще раз нажать кнопку\r\n(можно править вручную в строке)");
            this.tboxMobDestination.WordWrap = false;
            this.tboxMobDestination.TextChanged += new System.EventHandler(this.tboxMobDestination_TextChanged);
            // 
            // nudMobHeight
            // 
            this.nudMobHeight.Location = new System.Drawing.Point(118, 62);
            this.nudMobHeight.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudMobHeight.Name = "nudMobHeight";
            this.nudMobHeight.Size = new System.Drawing.Size(52, 20);
            this.nudMobHeight.TabIndex = 9;
            this.toolTip.SetToolTip(this.nudMobHeight, "Рост моба.");
            this.nudMobHeight.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudMobHeight.ValueChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // nudMobLevel
            // 
            this.nudMobLevel.Location = new System.Drawing.Point(6, 62);
            this.nudMobLevel.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudMobLevel.Name = "nudMobLevel";
            this.nudMobLevel.Size = new System.Drawing.Size(52, 20);
            this.nudMobLevel.TabIndex = 7;
            this.toolTip.SetToolTip(this.nudMobLevel, "Уровень моба.");
            this.nudMobLevel.ValueChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // cboxMobAlign
            // 
            this.cboxMobAlign.DropDownWidth = 52;
            this.cboxMobAlign.ItemHeight = 13;
            this.cboxMobAlign.Location = new System.Drawing.Point(6, 145);
            this.cboxMobAlign.Name = "cboxMobAlign";
            this.cboxMobAlign.Size = new System.Drawing.Size(190, 21);
            this.cboxMobAlign.TabIndex = 16;
            this.toolTip.SetToolTip(this.cboxMobAlign, "Наклонность моба.");
            this.cboxMobAlign.SelectedIndexChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // cboxMobAttackType
            // 
            this.cboxMobAttackType.DropDownWidth = 52;
            this.cboxMobAttackType.ItemHeight = 13;
            this.cboxMobAttackType.Location = new System.Drawing.Point(202, 145);
            this.cboxMobAttackType.Name = "cboxMobAttackType";
            this.cboxMobAttackType.Size = new System.Drawing.Size(198, 21);
            this.cboxMobAttackType.TabIndex = 17;
            this.toolTip.SetToolTip(this.cboxMobAttackType, "Тип атаки моба.");
            this.cboxMobAttackType.SelectedIndexChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // btnMobAddHelper
            // 
            this.btnMobAddHelper.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMobAddHelper.Image = ((System.Drawing.Image)(resources.GetObject("btnMobAddHelper.Image")));
            this.btnMobAddHelper.Location = new System.Drawing.Point(399, 1);
            this.btnMobAddHelper.Name = "btnMobAddHelper";
            this.btnMobAddHelper.Size = new System.Drawing.Size(28, 28);
            this.btnMobAddHelper.TabIndex = 29;
            this.toolTip.SetToolTip(this.btnMobAddHelper, "Добавить помощников мобу.");
            this.btnMobAddHelper.Click += new System.EventHandler(this.btnMobAddHelper_Click);
            // 
            // btnRemoveHelpersList
            // 
            this.btnRemoveHelpersList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveHelpersList.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveHelpersList.Image")));
            this.btnRemoveHelpersList.Location = new System.Drawing.Point(428, 1);
            this.btnRemoveHelpersList.Name = "btnRemoveHelpersList";
            this.btnRemoveHelpersList.Size = new System.Drawing.Size(28, 28);
            this.btnRemoveHelpersList.TabIndex = 28;
            this.toolTip.SetToolTip(this.btnRemoveHelpersList, "Удалить помощников, \r\nвыбранных в списке.");
            this.btnRemoveHelpersList.Click += new System.EventHandler(this.btnRemoveHelpersList_Click);
            // 
            // btnAddMobTrigger
            // 
            this.btnAddMobTrigger.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddMobTrigger.Image = ((System.Drawing.Image)(resources.GetObject("btnAddMobTrigger.Image")));
            this.btnAddMobTrigger.Location = new System.Drawing.Point(428, 3);
            this.btnAddMobTrigger.Name = "btnAddMobTrigger";
            this.btnAddMobTrigger.Size = new System.Drawing.Size(28, 28);
            this.btnAddMobTrigger.TabIndex = 34;
            this.toolTip.SetToolTip(this.btnAddMobTrigger, "Добавить триггеры мобу.");
            this.btnAddMobTrigger.Click += new System.EventHandler(this.btnAddMobTrigger_Click);
            // 
            // btnMobRemoveTrigger
            // 
            this.btnMobRemoveTrigger.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMobRemoveTrigger.Image = ((System.Drawing.Image)(resources.GetObject("btnMobRemoveTrigger.Image")));
            this.btnMobRemoveTrigger.Location = new System.Drawing.Point(428, 33);
            this.btnMobRemoveTrigger.Name = "btnMobRemoveTrigger";
            this.btnMobRemoveTrigger.Size = new System.Drawing.Size(28, 28);
            this.btnMobRemoveTrigger.TabIndex = 33;
            this.toolTip.SetToolTip(this.btnMobRemoveTrigger, "Удалить триггеры, \r\nвыбранные в списке.");
            this.btnMobRemoveTrigger.Click += new System.EventHandler(this.btnMobRemoveTrigger_Click);
            // 
            // nudSaveFightSkills
            // 
            this.nudSaveFightSkills.Location = new System.Drawing.Point(372, 18);
            this.nudSaveFightSkills.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nudSaveFightSkills.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.nudSaveFightSkills.Name = "nudSaveFightSkills";
            this.nudSaveFightSkills.Size = new System.Drawing.Size(45, 20);
            this.nudSaveFightSkills.TabIndex = 4;
            this.toolTip.SetToolTip(this.nudSaveFightSkills, "РЕАКЦИЯ");
            this.nudSaveFightSkills.ValueChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // nudSaveMagDam
            // 
            this.nudSaveMagDam.Location = new System.Drawing.Point(267, 18);
            this.nudSaveMagDam.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nudSaveMagDam.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.nudSaveMagDam.Name = "nudSaveMagDam";
            this.nudSaveMagDam.Size = new System.Drawing.Size(45, 20);
            this.nudSaveMagDam.TabIndex = 3;
            this.toolTip.SetToolTip(this.nudSaveMagDam, "СТОЙКОСТЬ");
            this.nudSaveMagDam.ValueChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // nudSaveParalyze
            // 
            this.nudSaveParalyze.Location = new System.Drawing.Point(39, 18);
            this.nudSaveParalyze.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nudSaveParalyze.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.nudSaveParalyze.Name = "nudSaveParalyze";
            this.nudSaveParalyze.Size = new System.Drawing.Size(45, 20);
            this.nudSaveParalyze.TabIndex = 1;
            this.toolTip.SetToolTip(this.nudSaveParalyze, "ВОЛЯ");
            this.nudSaveParalyze.ValueChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // nudSaveMagBreathe
            // 
            this.nudSaveMagBreathe.Location = new System.Drawing.Point(155, 18);
            this.nudSaveMagBreathe.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nudSaveMagBreathe.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.nudSaveMagBreathe.Name = "nudSaveMagBreathe";
            this.nudSaveMagBreathe.Size = new System.Drawing.Size(45, 20);
            this.nudSaveMagBreathe.TabIndex = 2;
            this.toolTip.SetToolTip(this.nudSaveMagBreathe, "ЖИВУЧЕСТЬ");
            this.nudSaveMagBreathe.ValueChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Enabled = false;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(176, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(28, 28);
            this.button1.TabIndex = 37;
            this.toolTip.SetToolTip(this.button1, "Добавить триггеры мобу.");
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Enabled = false;
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Location = new System.Drawing.Point(205, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(28, 28);
            this.button2.TabIndex = 36;
            this.toolTip.SetToolTip(this.button2, "Удалить триггеры, \r\nвыбранные в списке.");
            // 
            // bdtAddMobInVirtualRoom
            // 
            this.bdtAddMobInVirtualRoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bdtAddMobInVirtualRoom.Image = ((System.Drawing.Image)(resources.GetObject("bdtAddMobInVirtualRoom.Image")));
            this.bdtAddMobInVirtualRoom.Location = new System.Drawing.Point(459, 2);
            this.bdtAddMobInVirtualRoom.Name = "bdtAddMobInVirtualRoom";
            this.bdtAddMobInVirtualRoom.Size = new System.Drawing.Size(28, 28);
            this.bdtAddMobInVirtualRoom.TabIndex = 96;
            this.toolTip.SetToolTip(this.bdtAddMobInVirtualRoom, "Добавить моба в виртуальную комнату.");
            this.bdtAddMobInVirtualRoom.Click += new System.EventHandler(this.bdtAddMobInVirtualRoom_Click);
            // 
            // btnRemoveMobFromVitrualRoom
            // 
            this.btnRemoveMobFromVitrualRoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveMobFromVitrualRoom.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveMobFromVitrualRoom.Image")));
            this.btnRemoveMobFromVitrualRoom.Location = new System.Drawing.Point(459, 32);
            this.btnRemoveMobFromVitrualRoom.Name = "btnRemoveMobFromVitrualRoom";
            this.btnRemoveMobFromVitrualRoom.Size = new System.Drawing.Size(28, 28);
            this.btnRemoveMobFromVitrualRoom.TabIndex = 95;
            this.toolTip.SetToolTip(this.btnRemoveMobFromVitrualRoom, "Удалить из виртуальной комнаты моба, выбранного в списке");
            this.btnRemoveMobFromVitrualRoom.Click += new System.EventHandler(this.btnRemoveMobFromVitrualRoom_Click);
            // 
            // elvVitrualRoomMobObjects
            // 
            this.elvVitrualRoomMobObjects.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.elvVitrualRoomMobObjects.ContextMenuStrip = this.cmsGridMenu;
            this.elvVitrualRoomMobObjects.FullRowSelect = true;
            this.elvVitrualRoomMobObjects.GridLines = true;
            this.elvVitrualRoomMobObjects.LabelWrap = false;
            this.elvVitrualRoomMobObjects.Location = new System.Drawing.Point(3, 51);
            this.elvVitrualRoomMobObjects.MultiSelect = false;
            this.elvVitrualRoomMobObjects.Name = "elvVitrualRoomMobObjects";
            this.elvVitrualRoomMobObjects.OwnerDraw = true;
            this.elvVitrualRoomMobObjects.Size = new System.Drawing.Size(454, 110);
            this.elvVitrualRoomMobObjects.TabIndex = 98;
            this.toolTip.SetToolTip(this.elvVitrualRoomMobObjects, "Даблклик на ячейках \"Вероятность\" и \"Положение\"\r\nвыводит их в режим редактировани" +
        "я");
            this.elvVitrualRoomMobObjects.UseCompatibleStateImageBehavior = false;
            this.elvVitrualRoomMobObjects.View = System.Windows.Forms.View.Details;
            this.elvVitrualRoomMobObjects.ItemValueChanged += new ExtControls.ExtListView.ItemValueChangeEvent(this.elvVitrualRoomMobObjects_ItemValueChanged);
            this.elvVitrualRoomMobObjects.SizeChanged += new System.EventHandler(this.LastColumnAutosize);
            this.elvVitrualRoomMobObjects.MouseUp += new System.Windows.Forms.MouseEventHandler(this.NavigatedControl_MouseUp);
            // 
            // btnAddItemToMobInVirtualRoom
            // 
            this.btnAddItemToMobInVirtualRoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddItemToMobInVirtualRoom.Image = ((System.Drawing.Image)(resources.GetObject("btnAddItemToMobInVirtualRoom.Image")));
            this.btnAddItemToMobInVirtualRoom.Location = new System.Drawing.Point(460, 51);
            this.btnAddItemToMobInVirtualRoom.Name = "btnAddItemToMobInVirtualRoom";
            this.btnAddItemToMobInVirtualRoom.Size = new System.Drawing.Size(28, 28);
            this.btnAddItemToMobInVirtualRoom.TabIndex = 96;
            this.toolTip.SetToolTip(this.btnAddItemToMobInVirtualRoom, "Добавить мобу предмет.");
            this.btnAddItemToMobInVirtualRoom.Click += new System.EventHandler(this.btnAddItemToMobInVirtualRoomClick);
            // 
            // btnRemoveItemFromMobInVirtualRoom
            // 
            this.btnRemoveItemFromMobInVirtualRoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveItemFromMobInVirtualRoom.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveItemFromMobInVirtualRoom.Image")));
            this.btnRemoveItemFromMobInVirtualRoom.Location = new System.Drawing.Point(460, 81);
            this.btnRemoveItemFromMobInVirtualRoom.Name = "btnRemoveItemFromMobInVirtualRoom";
            this.btnRemoveItemFromMobInVirtualRoom.Size = new System.Drawing.Size(28, 28);
            this.btnRemoveItemFromMobInVirtualRoom.TabIndex = 95;
            this.toolTip.SetToolTip(this.btnRemoveItemFromMobInVirtualRoom, "Удалить предмет моба, выбранный в списке");
            this.btnRemoveItemFromMobInVirtualRoom.Click += new System.EventHandler(this.btnRemoveItemFromMobInVirtualRoomClick);
            // 
            // nudVirtualRoomMobMaxInRoom
            // 
            this.nudVirtualRoomMobMaxInRoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudVirtualRoomMobMaxInRoom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudVirtualRoomMobMaxInRoom.Enabled = false;
            this.nudVirtualRoomMobMaxInRoom.Location = new System.Drawing.Point(425, 15);
            this.nudVirtualRoomMobMaxInRoom.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudVirtualRoomMobMaxInRoom.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudVirtualRoomMobMaxInRoom.Name = "nudVirtualRoomMobMaxInRoom";
            this.nudVirtualRoomMobMaxInRoom.Size = new System.Drawing.Size(61, 20);
            this.nudVirtualRoomMobMaxInRoom.TabIndex = 84;
            this.toolTip.SetToolTip(this.nudVirtualRoomMobMaxInRoom, "Максимальное количество мобов\r\nвыбранного типа в комнате.");
            this.nudVirtualRoomMobMaxInRoom.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudVirtualRoomMobMaxInRoom.Validated += new System.EventHandler(this.nudVirtualRoomMobMaxInRoomValidated);
            // 
            // label40
            // 
            this.label40.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label40.Location = new System.Drawing.Point(422, -1);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(71, 16);
            this.label40.TabIndex = 82;
            this.label40.Text = "max в комн.";
            this.label40.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip.SetToolTip(this.label40, "Максимум мобов такого типа в комнате");
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.Location = new System.Drawing.Point(459, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(28, 28);
            this.button3.TabIndex = 100;
            this.toolTip.SetToolTip(this.button3, "Добавить мобу объект после смерти.");
            this.button3.Click += new System.EventHandler(this.BtnRoomAddObjToMobAfterDeathClick);
            // 
            // elvRoomMobObjectsLoadingAfterDeath
            // 
            this.elvRoomMobObjectsLoadingAfterDeath.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.elvRoomMobObjectsLoadingAfterDeath.ContextMenuStrip = this.cmsGridMenu;
            this.elvRoomMobObjectsLoadingAfterDeath.FullRowSelect = true;
            this.elvRoomMobObjectsLoadingAfterDeath.GridLines = true;
            this.elvRoomMobObjectsLoadingAfterDeath.LabelWrap = false;
            this.elvRoomMobObjectsLoadingAfterDeath.Location = new System.Drawing.Point(3, 3);
            this.elvRoomMobObjectsLoadingAfterDeath.MultiSelect = false;
            this.elvRoomMobObjectsLoadingAfterDeath.Name = "elvRoomMobObjectsLoadingAfterDeath";
            this.elvRoomMobObjectsLoadingAfterDeath.OwnerDraw = true;
            this.elvRoomMobObjectsLoadingAfterDeath.Size = new System.Drawing.Size(454, 77);
            this.elvRoomMobObjectsLoadingAfterDeath.TabIndex = 101;
            this.toolTip.SetToolTip(this.elvRoomMobObjectsLoadingAfterDeath, "Даблклик на ячейках \"Вероятность\" и \"Положение\"\r\nвыводит их в режим редактировани" +
        "я");
            this.elvRoomMobObjectsLoadingAfterDeath.UseCompatibleStateImageBehavior = false;
            this.elvRoomMobObjectsLoadingAfterDeath.View = System.Windows.Forms.View.Details;
            this.elvRoomMobObjectsLoadingAfterDeath.ItemValueChanged += new ExtControls.ExtListView.ItemValueChangeEvent(this.ElvRoomMobObjectsLoadingAfterDeathItemValueChanged);
            this.elvRoomMobObjectsLoadingAfterDeath.SizeChanged += new System.EventHandler(this.LastColumnAutosize);
            this.elvRoomMobObjectsLoadingAfterDeath.MouseUp += new System.Windows.Forms.MouseEventHandler(this.NavigatedControl_MouseUp);
            // 
            // btnRoomRomoveObjFromMobAfterDeath
            // 
            this.btnRoomRomoveObjFromMobAfterDeath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRoomRomoveObjFromMobAfterDeath.Image = ((System.Drawing.Image)(resources.GetObject("btnRoomRomoveObjFromMobAfterDeath.Image")));
            this.btnRoomRomoveObjFromMobAfterDeath.Location = new System.Drawing.Point(459, 33);
            this.btnRoomRomoveObjFromMobAfterDeath.Name = "btnRoomRomoveObjFromMobAfterDeath";
            this.btnRoomRomoveObjFromMobAfterDeath.Size = new System.Drawing.Size(28, 28);
            this.btnRoomRomoveObjFromMobAfterDeath.TabIndex = 102;
            this.toolTip.SetToolTip(this.btnRoomRomoveObjFromMobAfterDeath, "Удалить объект выбранный в списке");
            this.btnRoomRomoveObjFromMobAfterDeath.Click += new System.EventHandler(this.BtnRemoveRoomObjFromMobAfterDeathClick);
            // 
            // btnRoomRomoveObjFromMob
            // 
            this.btnRoomRomoveObjFromMob.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRoomRomoveObjFromMob.Image = ((System.Drawing.Image)(resources.GetObject("btnRoomRomoveObjFromMob.Image")));
            this.btnRoomRomoveObjFromMob.Location = new System.Drawing.Point(480, 33);
            this.btnRoomRomoveObjFromMob.Name = "btnRoomRomoveObjFromMob";
            this.btnRoomRomoveObjFromMob.Size = new System.Drawing.Size(28, 28);
            this.btnRoomRomoveObjFromMob.TabIndex = 95;
            this.toolTip.SetToolTip(this.btnRoomRomoveObjFromMob, "Удалить объект моба, выбранный в списке");
            this.btnRoomRomoveObjFromMob.Click += new System.EventHandler(this.BtnRoomRomoveObjFromMobClick);
            // 
            // btnRoomAddObjToMob
            // 
            this.btnRoomAddObjToMob.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRoomAddObjToMob.Image = ((System.Drawing.Image)(resources.GetObject("btnRoomAddObjToMob.Image")));
            this.btnRoomAddObjToMob.Location = new System.Drawing.Point(480, 3);
            this.btnRoomAddObjToMob.Name = "btnRoomAddObjToMob";
            this.btnRoomAddObjToMob.Size = new System.Drawing.Size(28, 28);
            this.btnRoomAddObjToMob.TabIndex = 96;
            this.toolTip.SetToolTip(this.btnRoomAddObjToMob, "Добавить мобу объект.");
            this.btnRoomAddObjToMob.Click += new System.EventHandler(this.BtnRoomAddObjToMobClick);
            // 
            // elvRoomMobObjects
            // 
            this.elvRoomMobObjects.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.elvRoomMobObjects.ContextMenuStrip = this.cmsGridMenu;
            this.elvRoomMobObjects.FullRowSelect = true;
            this.elvRoomMobObjects.GridLines = true;
            this.elvRoomMobObjects.LabelWrap = false;
            this.elvRoomMobObjects.Location = new System.Drawing.Point(3, 3);
            this.elvRoomMobObjects.MultiSelect = false;
            this.elvRoomMobObjects.Name = "elvRoomMobObjects";
            this.elvRoomMobObjects.OwnerDraw = true;
            this.elvRoomMobObjects.Size = new System.Drawing.Size(475, 117);
            this.elvRoomMobObjects.TabIndex = 98;
            this.toolTip.SetToolTip(this.elvRoomMobObjects, "Даблклик на ячейках \"Вероятность\" и \"Положение\"\r\nвыводит их в режим редактировани" +
        "я");
            this.elvRoomMobObjects.UseCompatibleStateImageBehavior = false;
            this.elvRoomMobObjects.View = System.Windows.Forms.View.Details;
            this.elvRoomMobObjects.ItemValueChanged += new ExtControls.ExtListView.ItemValueChangeEvent(this.ElvRoomMobObjectsItemValueChanged);
            this.elvRoomMobObjects.SizeChanged += new System.EventHandler(this.LastColumnAutosize);
            this.elvRoomMobObjects.MouseUp += new System.Windows.Forms.MouseEventHandler(this.NavigatedControl_MouseUp);
            // 
            // btnMobSpecFormatCommonDesc
            // 
            this.btnMobSpecFormatCommonDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMobSpecFormatCommonDesc.Image = ((System.Drawing.Image)(resources.GetObject("btnMobSpecFormatCommonDesc.Image")));
            this.btnMobSpecFormatCommonDesc.Location = new System.Drawing.Point(35, 3);
            this.btnMobSpecFormatCommonDesc.Name = "btnMobSpecFormatCommonDesc";
            this.btnMobSpecFormatCommonDesc.Size = new System.Drawing.Size(28, 28);
            this.btnMobSpecFormatCommonDesc.TabIndex = 44;
            this.toolTip.SetToolTip(this.btnMobSpecFormatCommonDesc, "Выровнять по ширине\r\n(без сохранения абзацев).");
            this.btnMobSpecFormatCommonDesc.Click += new System.EventHandler(this.btnMobSpecFormatCommonDesc_Click);
            // 
            // btnMobSpellCheckCommonDesc
            // 
            this.btnMobSpellCheckCommonDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMobSpellCheckCommonDesc.Image = ((System.Drawing.Image)(resources.GetObject("btnMobSpellCheckCommonDesc.Image")));
            this.btnMobSpellCheckCommonDesc.Location = new System.Drawing.Point(3, 3);
            this.btnMobSpellCheckCommonDesc.Name = "btnMobSpellCheckCommonDesc";
            this.btnMobSpellCheckCommonDesc.Size = new System.Drawing.Size(28, 28);
            this.btnMobSpellCheckCommonDesc.TabIndex = 44;
            this.toolTip.SetToolTip(this.btnMobSpellCheckCommonDesc, "Проверка ошибок.");
            this.btnMobSpellCheckCommonDesc.Click += new System.EventHandler(this.btnMobSpellCheckCommonDesc_Click);
            // 
            // btnMobFormatCommonDesc
            // 
            this.btnMobFormatCommonDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMobFormatCommonDesc.Image = ((System.Drawing.Image)(resources.GetObject("btnMobFormatCommonDesc.Image")));
            this.btnMobFormatCommonDesc.Location = new System.Drawing.Point(66, 3);
            this.btnMobFormatCommonDesc.Name = "btnMobFormatCommonDesc";
            this.btnMobFormatCommonDesc.Size = new System.Drawing.Size(28, 28);
            this.btnMobFormatCommonDesc.TabIndex = 44;
            this.btnMobFormatCommonDesc.Text = " ";
            this.toolTip.SetToolTip(this.btnMobFormatCommonDesc, "Выровнять по ширине\r\n(с сохранением абзацев).");
            this.btnMobFormatCommonDesc.Click += new System.EventHandler(this.btnMobFormatCommonDesc_Click);
            // 
            // nudObjMinRemorts
            // 
            this.nudObjMinRemorts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudObjMinRemorts.Location = new System.Drawing.Point(266, 20);
            this.nudObjMinRemorts.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudObjMinRemorts.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            -2147483648});
            this.nudObjMinRemorts.Name = "nudObjMinRemorts";
            this.nudObjMinRemorts.Size = new System.Drawing.Size(80, 20);
            this.nudObjMinRemorts.TabIndex = 3;
            this.toolTip.SetToolTip(this.nudObjMinRemorts, "если x<0 то предмет доступен до x-реморта,\r\nесли x=0, то ограничение расчитываетс" +
        "я автоматически\r\nесли x>0 то предмет доступен начиная с x-реморта.\r\n");
            this.nudObjMinRemorts.ValueChanged += new System.EventHandler(this.ObjValueValidated);
            // 
            // btnAddRoomIngredient
            // 
            this.btnAddRoomIngredient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddRoomIngredient.Enabled = false;
            this.btnAddRoomIngredient.Image = ((System.Drawing.Image)(resources.GetObject("btnAddRoomIngredient.Image")));
            this.btnAddRoomIngredient.Location = new System.Drawing.Point(489, 3);
            this.btnAddRoomIngredient.Name = "btnAddRoomIngredient";
            this.btnAddRoomIngredient.Size = new System.Drawing.Size(28, 28);
            this.btnAddRoomIngredient.TabIndex = 99;
            this.toolTip.SetToolTip(this.btnAddRoomIngredient, "Добавить моба в комнату.");
            this.btnAddRoomIngredient.Click += new System.EventHandler(this.BtnAddRoomIngredientClick);
            // 
            // btnRemoveRoomIngredient
            // 
            this.btnRemoveRoomIngredient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveRoomIngredient.Enabled = false;
            this.btnRemoveRoomIngredient.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveRoomIngredient.Image")));
            this.btnRemoveRoomIngredient.Location = new System.Drawing.Point(489, 33);
            this.btnRemoveRoomIngredient.Name = "btnRemoveRoomIngredient";
            this.btnRemoveRoomIngredient.Size = new System.Drawing.Size(28, 28);
            this.btnRemoveRoomIngredient.TabIndex = 98;
            this.toolTip.SetToolTip(this.btnRemoveRoomIngredient, "Удалить моба, выбранного в списке");
            this.btnRemoveRoomIngredient.Click += new System.EventHandler(this.BtnRemoveRoomIngredientClick);
            // 
            // elvRoomIngredients
            // 
            this.elvRoomIngredients.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.elvRoomIngredients.FullRowSelect = true;
            this.elvRoomIngredients.GridLines = true;
            this.elvRoomIngredients.LabelWrap = false;
            this.elvRoomIngredients.Location = new System.Drawing.Point(3, 3);
            this.elvRoomIngredients.Name = "elvRoomIngredients";
            this.elvRoomIngredients.OwnerDraw = true;
            this.elvRoomIngredients.Size = new System.Drawing.Size(484, 282);
            this.elvRoomIngredients.TabIndex = 100;
            this.toolTip.SetToolTip(this.elvRoomIngredients, "Даблклик на ячейках \"Вероятность\" и \"Положение\"\r\nвыводит их в режим редактировани" +
        "я");
            this.elvRoomIngredients.UseCompatibleStateImageBehavior = false;
            this.elvRoomIngredients.View = System.Windows.Forms.View.Details;
            this.elvRoomIngredients.ItemValueChanged += new ExtControls.ExtListView.ItemValueChangeEvent(this.ElvRoomIngredientsItemValueChanged);
            this.elvRoomIngredients.SizeChanged += new System.EventHandler(this.LastColumnAutosize);
            // 
            // elvMobIngredients
            // 
            this.elvMobIngredients.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.elvMobIngredients.FullRowSelect = true;
            this.elvMobIngredients.GridLines = true;
            this.elvMobIngredients.LabelWrap = false;
            this.elvMobIngredients.Location = new System.Drawing.Point(3, 3);
            this.elvMobIngredients.Name = "elvMobIngredients";
            this.elvMobIngredients.OwnerDraw = true;
            this.elvMobIngredients.Size = new System.Drawing.Size(423, 317);
            this.elvMobIngredients.TabIndex = 103;
            this.toolTip.SetToolTip(this.elvMobIngredients, "Даблклик на ячейках выводит их в режим редактирования");
            this.elvMobIngredients.UseCompatibleStateImageBehavior = false;
            this.elvMobIngredients.View = System.Windows.Forms.View.Details;
            this.elvMobIngredients.ItemValueChanged += new ExtControls.ExtListView.ItemValueChangeEvent(this.ElvMobIngredientsItemValueChanged);
            this.elvMobIngredients.SizeChanged += new System.EventHandler(this.LastColumnAutosize);
            // 
            // btnAddMobIngredient
            // 
            this.btnAddMobIngredient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddMobIngredient.Image = ((System.Drawing.Image)(resources.GetObject("btnAddMobIngredient.Image")));
            this.btnAddMobIngredient.Location = new System.Drawing.Point(428, 3);
            this.btnAddMobIngredient.Name = "btnAddMobIngredient";
            this.btnAddMobIngredient.Size = new System.Drawing.Size(28, 28);
            this.btnAddMobIngredient.TabIndex = 102;
            this.toolTip.SetToolTip(this.btnAddMobIngredient, "Добавить моба в комнату.");
            this.btnAddMobIngredient.Click += new System.EventHandler(this.BtnAddMobIngredientClick);
            // 
            // btnRemoveMobIngredient
            // 
            this.btnRemoveMobIngredient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveMobIngredient.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveMobIngredient.Image")));
            this.btnRemoveMobIngredient.Location = new System.Drawing.Point(428, 33);
            this.btnRemoveMobIngredient.Name = "btnRemoveMobIngredient";
            this.btnRemoveMobIngredient.Size = new System.Drawing.Size(28, 28);
            this.btnRemoveMobIngredient.TabIndex = 101;
            this.toolTip.SetToolTip(this.btnRemoveMobIngredient, "Удалить моба, выбранного в списке");
            this.btnRemoveMobIngredient.Click += new System.EventHandler(this.BtnRemoveMobIngredientClick);
            // 
            // cbRoomDescAllowHyp
            // 
            this.cbRoomDescAllowHyp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbRoomDescAllowHyp.Location = new System.Drawing.Point(3, 1);
            this.cbRoomDescAllowHyp.Name = "cbRoomDescAllowHyp";
            this.cbRoomDescAllowHyp.Size = new System.Drawing.Size(91, 18);
            this.cbRoomDescAllowHyp.TabIndex = 104;
            this.cbRoomDescAllowHyp.Text = "По слогам";
            this.cbRoomDescAllowHyp.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox3.Location = new System.Drawing.Point(3, 1);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(91, 18);
            this.checkBox3.TabIndex = 104;
            this.checkBox3.Text = "По слогам";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.CExtRichTextBox2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(779, 96);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Общее";
            // 
            // CExtRichTextBox2
            // 
            this.CExtRichTextBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CExtRichTextBox2.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CExtRichTextBox2.Location = new System.Drawing.Point(0, 0);
            this.CExtRichTextBox2.Name = "CExtRichTextBox2";
            this.CExtRichTextBox2.Size = new System.Drawing.Size(678, 96);
            this.CExtRichTextBox2.TabIndex = 0;
            this.CExtRichTextBox2.Text = "";
            this.CExtRichTextBox2.WordWrap = false;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage3.Controls.Add(this.checkBox4);
            this.tabPage3.Controls.Add(this.CExtRichTextBox3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(779, 96);
            this.tabPage3.TabIndex = 1;
            this.tabPage3.Text = "День";
            // 
            // CExtRichTextBox3
            // 
            this.CExtRichTextBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CExtRichTextBox3.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CExtRichTextBox3.Location = new System.Drawing.Point(0, 0);
            this.CExtRichTextBox3.Name = "CExtRichTextBox3";
            this.CExtRichTextBox3.Size = new System.Drawing.Size(678, 96);
            this.CExtRichTextBox3.TabIndex = 1;
            this.CExtRichTextBox3.Text = "";
            this.CExtRichTextBox3.WordWrap = false;
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage4.Controls.Add(this.checkBox6);
            this.tabPage4.Controls.Add(this.CExtRichTextBox4);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(779, 96);
            this.tabPage4.TabIndex = 2;
            this.tabPage4.Text = "Ночь";
            // 
            // checkBox6
            // 
            this.checkBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox6.Location = new System.Drawing.Point(684, 74);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(83, 19);
            this.checkBox6.TabIndex = 99;
            this.checkBox6.Text = "Замещать";
            this.checkBox6.UseVisualStyleBackColor = true;
            // 
            // CExtRichTextBox4
            // 
            this.CExtRichTextBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CExtRichTextBox4.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CExtRichTextBox4.Location = new System.Drawing.Point(0, 0);
            this.CExtRichTextBox4.Name = "CExtRichTextBox4";
            this.CExtRichTextBox4.Size = new System.Drawing.Size(678, 96);
            this.CExtRichTextBox4.TabIndex = 2;
            this.CExtRichTextBox4.Text = "";
            this.CExtRichTextBox4.WordWrap = false;
            // 
            // tabPage5
            // 
            this.tabPage5.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage5.Controls.Add(this.checkBox7);
            this.tabPage5.Controls.Add(this.CExtRichTextBox5);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(779, 96);
            this.tabPage5.TabIndex = 3;
            this.tabPage5.Text = "Зима[Д]";
            // 
            // checkBox7
            // 
            this.checkBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox7.Location = new System.Drawing.Point(684, 74);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(83, 19);
            this.checkBox7.TabIndex = 99;
            this.checkBox7.Text = "Замещать";
            this.checkBox7.UseVisualStyleBackColor = true;
            // 
            // CExtRichTextBox5
            // 
            this.CExtRichTextBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CExtRichTextBox5.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CExtRichTextBox5.Location = new System.Drawing.Point(0, 0);
            this.CExtRichTextBox5.Name = "CExtRichTextBox5";
            this.CExtRichTextBox5.Size = new System.Drawing.Size(678, 96);
            this.CExtRichTextBox5.TabIndex = 2;
            this.CExtRichTextBox5.Text = "";
            this.CExtRichTextBox5.WordWrap = false;
            // 
            // tabPage6
            // 
            this.tabPage6.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage6.Controls.Add(this.checkBox8);
            this.tabPage6.Controls.Add(this.CExtRichTextBox6);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(779, 96);
            this.tabPage6.TabIndex = 4;
            this.tabPage6.Text = "Зима[Н]";
            // 
            // checkBox8
            // 
            this.checkBox8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox8.Location = new System.Drawing.Point(684, 74);
            this.checkBox8.Name = "checkBox8";
            this.checkBox8.Size = new System.Drawing.Size(83, 19);
            this.checkBox8.TabIndex = 99;
            this.checkBox8.Text = "Замещать";
            this.checkBox8.UseVisualStyleBackColor = true;
            // 
            // CExtRichTextBox6
            // 
            this.CExtRichTextBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CExtRichTextBox6.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CExtRichTextBox6.Location = new System.Drawing.Point(0, 0);
            this.CExtRichTextBox6.Name = "CExtRichTextBox6";
            this.CExtRichTextBox6.Size = new System.Drawing.Size(678, 96);
            this.CExtRichTextBox6.TabIndex = 3;
            this.CExtRichTextBox6.Text = "";
            this.CExtRichTextBox6.WordWrap = false;
            // 
            // tabPage7
            // 
            this.tabPage7.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage7.Controls.Add(this.checkBox9);
            this.tabPage7.Controls.Add(this.CExtRichTextBox7);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(779, 96);
            this.tabPage7.TabIndex = 5;
            this.tabPage7.Text = "Весна[Д]";
            // 
            // checkBox9
            // 
            this.checkBox9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox9.Location = new System.Drawing.Point(684, 74);
            this.checkBox9.Name = "checkBox9";
            this.checkBox9.Size = new System.Drawing.Size(83, 19);
            this.checkBox9.TabIndex = 99;
            this.checkBox9.Text = "Замещать";
            this.checkBox9.UseVisualStyleBackColor = true;
            // 
            // CExtRichTextBox7
            // 
            this.CExtRichTextBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CExtRichTextBox7.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CExtRichTextBox7.Location = new System.Drawing.Point(0, 0);
            this.CExtRichTextBox7.Name = "CExtRichTextBox7";
            this.CExtRichTextBox7.Size = new System.Drawing.Size(678, 96);
            this.CExtRichTextBox7.TabIndex = 3;
            this.CExtRichTextBox7.Text = "";
            this.CExtRichTextBox7.WordWrap = false;
            // 
            // tabPage16
            // 
            this.tabPage16.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage16.Controls.Add(this.checkBox10);
            this.tabPage16.Controls.Add(this.CExtRichTextBox8);
            this.tabPage16.Location = new System.Drawing.Point(4, 22);
            this.tabPage16.Name = "tabPage16";
            this.tabPage16.Size = new System.Drawing.Size(779, 96);
            this.tabPage16.TabIndex = 6;
            this.tabPage16.Text = "Весна[Н]";
            // 
            // checkBox10
            // 
            this.checkBox10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox10.Location = new System.Drawing.Point(684, 74);
            this.checkBox10.Name = "checkBox10";
            this.checkBox10.Size = new System.Drawing.Size(83, 19);
            this.checkBox10.TabIndex = 99;
            this.checkBox10.Text = "Замещать";
            this.checkBox10.UseVisualStyleBackColor = true;
            // 
            // CExtRichTextBox8
            // 
            this.CExtRichTextBox8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CExtRichTextBox8.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CExtRichTextBox8.Location = new System.Drawing.Point(0, 0);
            this.CExtRichTextBox8.Name = "CExtRichTextBox8";
            this.CExtRichTextBox8.Size = new System.Drawing.Size(678, 96);
            this.CExtRichTextBox8.TabIndex = 3;
            this.CExtRichTextBox8.Text = "";
            this.CExtRichTextBox8.WordWrap = false;
            // 
            // tabPage17
            // 
            this.tabPage17.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage17.Controls.Add(this.checkBox11);
            this.tabPage17.Controls.Add(this.CExtRichTextBox9);
            this.tabPage17.Location = new System.Drawing.Point(4, 22);
            this.tabPage17.Name = "tabPage17";
            this.tabPage17.Size = new System.Drawing.Size(779, 96);
            this.tabPage17.TabIndex = 7;
            this.tabPage17.Text = "Лето[Д]";
            // 
            // checkBox11
            // 
            this.checkBox11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox11.Location = new System.Drawing.Point(684, 74);
            this.checkBox11.Name = "checkBox11";
            this.checkBox11.Size = new System.Drawing.Size(83, 19);
            this.checkBox11.TabIndex = 99;
            this.checkBox11.Text = "Замещать";
            this.checkBox11.UseVisualStyleBackColor = true;
            // 
            // CExtRichTextBox9
            // 
            this.CExtRichTextBox9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CExtRichTextBox9.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CExtRichTextBox9.Location = new System.Drawing.Point(0, 0);
            this.CExtRichTextBox9.Name = "CExtRichTextBox9";
            this.CExtRichTextBox9.Size = new System.Drawing.Size(678, 96);
            this.CExtRichTextBox9.TabIndex = 3;
            this.CExtRichTextBox9.Text = "";
            this.CExtRichTextBox9.WordWrap = false;
            // 
            // tabPage18
            // 
            this.tabPage18.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage18.Controls.Add(this.checkBox12);
            this.tabPage18.Controls.Add(this.CExtRichTextBox10);
            this.tabPage18.Location = new System.Drawing.Point(4, 22);
            this.tabPage18.Name = "tabPage18";
            this.tabPage18.Size = new System.Drawing.Size(779, 96);
            this.tabPage18.TabIndex = 8;
            this.tabPage18.Text = "Лето[Н]";
            // 
            // checkBox12
            // 
            this.checkBox12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox12.Location = new System.Drawing.Point(684, 74);
            this.checkBox12.Name = "checkBox12";
            this.checkBox12.Size = new System.Drawing.Size(83, 19);
            this.checkBox12.TabIndex = 99;
            this.checkBox12.Text = "Замещать";
            this.checkBox12.UseVisualStyleBackColor = true;
            // 
            // CExtRichTextBox10
            // 
            this.CExtRichTextBox10.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CExtRichTextBox10.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CExtRichTextBox10.Location = new System.Drawing.Point(0, 0);
            this.CExtRichTextBox10.Name = "CExtRichTextBox10";
            this.CExtRichTextBox10.Size = new System.Drawing.Size(678, 96);
            this.CExtRichTextBox10.TabIndex = 3;
            this.CExtRichTextBox10.Text = "";
            this.CExtRichTextBox10.WordWrap = false;
            // 
            // tabPage19
            // 
            this.tabPage19.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage19.Controls.Add(this.checkBox13);
            this.tabPage19.Controls.Add(this.CExtRichTextBox11);
            this.tabPage19.Location = new System.Drawing.Point(4, 22);
            this.tabPage19.Name = "tabPage19";
            this.tabPage19.Size = new System.Drawing.Size(779, 96);
            this.tabPage19.TabIndex = 9;
            this.tabPage19.Text = "Осень[Д]";
            // 
            // checkBox13
            // 
            this.checkBox13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox13.Location = new System.Drawing.Point(684, 74);
            this.checkBox13.Name = "checkBox13";
            this.checkBox13.Size = new System.Drawing.Size(83, 19);
            this.checkBox13.TabIndex = 99;
            this.checkBox13.Text = "Замещать";
            this.checkBox13.UseVisualStyleBackColor = true;
            // 
            // CExtRichTextBox11
            // 
            this.CExtRichTextBox11.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CExtRichTextBox11.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CExtRichTextBox11.Location = new System.Drawing.Point(0, 0);
            this.CExtRichTextBox11.Name = "CExtRichTextBox11";
            this.CExtRichTextBox11.Size = new System.Drawing.Size(678, 96);
            this.CExtRichTextBox11.TabIndex = 3;
            this.CExtRichTextBox11.Text = "";
            this.CExtRichTextBox11.WordWrap = false;
            // 
            // tabPage20
            // 
            this.tabPage20.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage20.Controls.Add(this.checkBox14);
            this.tabPage20.Controls.Add(this.CExtRichTextBox12);
            this.tabPage20.Location = new System.Drawing.Point(4, 22);
            this.tabPage20.Name = "tabPage20";
            this.tabPage20.Size = new System.Drawing.Size(779, 96);
            this.tabPage20.TabIndex = 10;
            this.tabPage20.Text = "Осень[Н]";
            // 
            // checkBox14
            // 
            this.checkBox14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox14.Location = new System.Drawing.Point(684, 74);
            this.checkBox14.Name = "checkBox14";
            this.checkBox14.Size = new System.Drawing.Size(83, 19);
            this.checkBox14.TabIndex = 99;
            this.checkBox14.Text = "Замещать";
            this.checkBox14.UseVisualStyleBackColor = true;
            // 
            // CExtRichTextBox12
            // 
            this.CExtRichTextBox12.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CExtRichTextBox12.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CExtRichTextBox12.Location = new System.Drawing.Point(0, 0);
            this.CExtRichTextBox12.Name = "CExtRichTextBox12";
            this.CExtRichTextBox12.Size = new System.Drawing.Size(678, 96);
            this.CExtRichTextBox12.TabIndex = 3;
            this.CExtRichTextBox12.Text = "";
            this.CExtRichTextBox12.WordWrap = false;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::BZEditor.Properties.Resources.button_edit;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(21, 20);
            this.toolStripButton1.Text = "Редактировать значение выделенного доп.аффекта";
            this.toolStripButton1.ToolTipText = "Редактировать значение \r\nвыделенного доп.аффекта";
            this.toolStripButton1.Click += new System.EventHandler(this.ToolStripButton1Click);
            // 
            // toolStripButton18
            // 
            this.toolStripButton18.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton18.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton18.Image")));
            this.toolStripButton18.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton18.Name = "toolStripButton18";
            this.toolStripButton18.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton18.Text = "toolStripButton18";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "toolStripButton3";
            // 
            // cmsCodeEditor
            // 
            this.cmsCodeEditor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCodeEditorCopy,
            this.tsmiCodeEditorCut,
            this.toolStripSeparator17,
            this.tsmiCodeEditorPaste});
            this.cmsCodeEditor.Name = "cmsRoomsDescription";
            this.cmsCodeEditor.Size = new System.Drawing.Size(140, 76);
            // 
            // tsmiCodeEditorCopy
            // 
            this.tsmiCodeEditorCopy.Image = global::BZEditor.Properties.Resources.button_copy;
            this.tsmiCodeEditorCopy.Name = "tsmiCodeEditorCopy";
            this.tsmiCodeEditorCopy.Size = new System.Drawing.Size(139, 22);
            this.tsmiCodeEditorCopy.Text = "Копировать";
            this.tsmiCodeEditorCopy.Click += new System.EventHandler(this.tsmiCodeEditorCopy_Click);
            // 
            // tsmiCodeEditorCut
            // 
            this.tsmiCodeEditorCut.Image = global::BZEditor.Properties.Resources.button_cut;
            this.tsmiCodeEditorCut.Name = "tsmiCodeEditorCut";
            this.tsmiCodeEditorCut.Size = new System.Drawing.Size(139, 22);
            this.tsmiCodeEditorCut.Text = "Вырезать";
            this.tsmiCodeEditorCut.Click += new System.EventHandler(this.tsmiCodeEditorCut_Click);
            // 
            // toolStripSeparator17
            // 
            this.toolStripSeparator17.Name = "toolStripSeparator17";
            this.toolStripSeparator17.Size = new System.Drawing.Size(136, 6);
            // 
            // tsmiCodeEditorPaste
            // 
            this.tsmiCodeEditorPaste.Image = global::BZEditor.Properties.Resources.button_paste;
            this.tsmiCodeEditorPaste.Name = "tsmiCodeEditorPaste";
            this.tsmiCodeEditorPaste.Size = new System.Drawing.Size(139, 22);
            this.tsmiCodeEditorPaste.Text = "Вставить";
            this.tsmiCodeEditorPaste.Click += new System.EventHandler(this.tsmiCodeEditorPaste_Click);
            // 
            // splitContainerBase
            // 
            this.splitContainerBase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerBase.Location = new System.Drawing.Point(0, 25);
            this.splitContainerBase.Name = "splitContainerBase";
            this.splitContainerBase.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerBase.Panel1
            // 
            this.splitContainerBase.Panel1.Controls.Add(this.splitContainerMap);
            // 
            // splitContainerBase.Panel2
            // 
            this.splitContainerBase.Panel2.Controls.Add(this.tcMain);
            this.splitContainerBase.Panel2MinSize = 100;
            this.splitContainerBase.Size = new System.Drawing.Size(829, 785);
            this.splitContainerBase.SplitterDistance = 280;
            this.splitContainerBase.TabIndex = 4;
            // 
            // splitContainerMap
            // 
            this.splitContainerMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMap.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMap.Name = "splitContainerMap";
            // 
            // splitContainerMap.Panel1
            // 
            this.splitContainerMap.Panel1.BackColor = System.Drawing.SystemColors.Window;
            this.splitContainerMap.Panel1.Controls.Add(this.tcListAndInfo);
            // 
            // splitContainerMap.Panel2
            // 
            this.splitContainerMap.Panel2.Controls.Add(this.wldMap);
            this.splitContainerMap.Panel2.Controls.Add(this.vertSBMap);
            this.splitContainerMap.Panel2.Controls.Add(this.pnlMapHorizSplitter);
            this.splitContainerMap.Size = new System.Drawing.Size(829, 280);
            this.splitContainerMap.SplitterDistance = 347;
            this.splitContainerMap.TabIndex = 3;
            // 
            // tcListAndInfo
            // 
            this.tcListAndInfo.Controls.Add(this.tpList);
            this.tcListAndInfo.Controls.Add(this.tpInfo);
            this.tcListAndInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcListAndInfo.ImageList = this.iListIcons16;
            this.tcListAndInfo.Location = new System.Drawing.Point(0, 0);
            this.tcListAndInfo.Name = "tcListAndInfo";
            this.tcListAndInfo.SelectedIndex = 0;
            this.tcListAndInfo.Size = new System.Drawing.Size(345, 278);
            this.tcListAndInfo.TabIndex = 105;
            // 
            // tpList
            // 
            this.tpList.Controls.Add(this.tboxMainListFilter);
            this.tpList.Controls.Add(this.label29);
            this.tpList.Controls.Add(this.lvMainList);
            this.tpList.Controls.Add(this.cboxMainListConditions);
            this.tpList.Controls.Add(this.label51);
            this.tpList.Controls.Add(this.lvZoneInfo);
            this.tpList.Location = new System.Drawing.Point(4, 23);
            this.tpList.Name = "tpList";
            this.tpList.Padding = new System.Windows.Forms.Padding(3);
            this.tpList.Size = new System.Drawing.Size(337, 251);
            this.tpList.TabIndex = 0;
            this.tpList.Text = "Список";
            this.tpList.UseVisualStyleBackColor = true;
            // 
            // tboxMainListFilter
            // 
            this.tboxMainListFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tboxMainListFilter.Location = new System.Drawing.Point(4, 16);
            this.tboxMainListFilter.Name = "tboxMainListFilter";
            this.tboxMainListFilter.Size = new System.Drawing.Size(157, 20);
            this.tboxMainListFilter.TabIndex = 103;
            this.tboxMainListFilter.TextChanged += new System.EventHandler(this.TboxMainListFilterTextChanged);
            // 
            // label29
            // 
            this.label29.Location = new System.Drawing.Point(1, 1);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(49, 16);
            this.label29.TabIndex = 102;
            this.label29.Text = "Фильтр";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lvMainList
            // 
            this.lvMainList.AllowDrop = true;
            this.lvMainList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvMainList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chMainListVNum,
            this.chMainListItemName});
            this.lvMainList.ContextMenuStrip = this.cmsMainTree;
            this.lvMainList.FullRowSelect = true;
            this.lvMainList.GridLines = true;
            this.lvMainList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvMainList.HideSelection = false;
            this.lvMainList.Location = new System.Drawing.Point(3, 39);
            this.lvMainList.Name = "lvMainList";
            this.lvMainList.ShowGroups = false;
            this.lvMainList.ShowItemToolTips = true;
            this.lvMainList.Size = new System.Drawing.Size(331, 209);
            this.lvMainList.SmallImageList = this.iListIcons16;
            this.lvMainList.TabIndex = 100;
            this.lvMainList.UseCompatibleStateImageBehavior = false;
            this.lvMainList.View = System.Windows.Forms.View.Details;
            this.lvMainList.SelectedIndexChanged += new System.EventHandler(this.LvMainListSelectedIndexChanged);
            this.lvMainList.SizeChanged += new System.EventHandler(this.LastColumnAutosize);
            this.lvMainList.DragDrop += new System.Windows.Forms.DragEventHandler(this.LvMainListDragDrop);
            this.lvMainList.DragEnter += new System.Windows.Forms.DragEventHandler(this.LvMainListDragEnter);
            this.lvMainList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LvMainListMouseDown);
            this.lvMainList.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LvMainListMouseMove);
            this.lvMainList.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LvMainListMouseUp);
            // 
            // chMainListVNum
            // 
            this.chMainListVNum.Text = "Номер";
            this.chMainListVNum.Width = 66;
            // 
            // chMainListItemName
            // 
            this.chMainListItemName.Text = "Название";
            this.chMainListItemName.Width = 245;
            // 
            // cboxMainListConditions
            // 
            this.cboxMainListConditions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboxMainListConditions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxMainListConditions.ItemHeight = 13;
            this.cboxMainListConditions.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.cboxMainListConditions.Location = new System.Drawing.Point(167, 15);
            this.cboxMainListConditions.Name = "cboxMainListConditions";
            this.cboxMainListConditions.Size = new System.Drawing.Size(167, 21);
            this.cboxMainListConditions.TabIndex = 22;
            this.cboxMainListConditions.SelectedIndexChanged += new System.EventHandler(this.CboxMainListConditionsSelectedIndexChanged);
            // 
            // label51
            // 
            this.label51.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label51.Location = new System.Drawing.Point(164, -1);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(51, 16);
            this.label51.TabIndex = 102;
            this.label51.Text = "Условие";
            this.label51.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lvZoneInfo
            // 
            this.lvZoneInfo.BackColor = System.Drawing.SystemColors.Window;
            this.lvZoneInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chParamName,
            this.chParamVal});
            this.lvZoneInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvZoneInfo.FullRowSelect = true;
            this.lvZoneInfo.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvZoneInfo.Location = new System.Drawing.Point(3, 3);
            this.lvZoneInfo.MultiSelect = false;
            this.lvZoneInfo.Name = "lvZoneInfo";
            this.lvZoneInfo.Size = new System.Drawing.Size(331, 245);
            this.lvZoneInfo.TabIndex = 104;
            this.lvZoneInfo.UseCompatibleStateImageBehavior = false;
            this.lvZoneInfo.View = System.Windows.Forms.View.Details;
            // 
            // chParamName
            // 
            this.chParamName.Text = "";
            // 
            // chParamVal
            // 
            this.chParamVal.Text = "";
            // 
            // tpInfo
            // 
            this.tpInfo.Controls.Add(this.lvDetails);
            this.tpInfo.Location = new System.Drawing.Point(4, 23);
            this.tpInfo.Name = "tpInfo";
            this.tpInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpInfo.Size = new System.Drawing.Size(337, 251);
            this.tpInfo.TabIndex = 1;
            this.tpInfo.Text = "Навигация/Сводная информация";
            this.tpInfo.UseVisualStyleBackColor = true;
            // 
            // lvDetails
            // 
            this.lvDetails.AllowDrop = true;
            this.lvDetails.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader29});
            this.lvDetails.ContextMenuStrip = this.cmsNavigation;
            this.lvDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvDetails.FullRowSelect = true;
            this.lvDetails.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvDetails.HideSelection = false;
            this.lvDetails.Location = new System.Drawing.Point(3, 3);
            this.lvDetails.MultiSelect = false;
            this.lvDetails.Name = "lvDetails";
            this.lvDetails.ShowItemToolTips = true;
            this.lvDetails.Size = new System.Drawing.Size(331, 245);
            this.lvDetails.TabIndex = 101;
            this.lvDetails.UseCompatibleStateImageBehavior = false;
            this.lvDetails.View = System.Windows.Forms.View.Details;
            this.lvDetails.SizeChanged += new System.EventHandler(this.LastColumnAutosize);
            this.lvDetails.DoubleClick += new System.EventHandler(this.LvDetailsDoubleClick);
            // 
            // columnHeader29
            // 
            this.columnHeader29.Text = "";
            this.columnHeader29.Width = 317;
            // 
            // wldMap
            // 
            this.wldMap.AllowDrop = true;
            this.wldMap.AutolinkingX = true;
            this.wldMap.AutolinkingY = true;
            this.wldMap.AutolinkingZ = true;
            this.wldMap.BackColor = System.Drawing.SystemColors.Window;
            this.wldMap.ClearSketchAfterGeneratingRooms = true;
            this.wldMap.Cursor = System.Windows.Forms.Cursors.Default;
            this.wldMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wldMap.DrawSketchMode = false;
            this.wldMap.FocusHighlightColor = System.Drawing.SystemColors.ActiveCaption;
            this.wldMap.GridColor = System.Drawing.SystemColors.Control;
            this.wldMap.Location = new System.Drawing.Point(0, 0);
            this.wldMap.MapScale = 3;
            this.wldMap.Name = "wldMap";
            this.wldMap.RoomDetailsVisible = true;
            this.wldMap.RoomHighlightColor = System.Drawing.Color.LightSalmon;
            this.wldMap.RoomHighlightingAlphaFactor = 150;
            this.wldMap.SelectionAlphaFactor = 50;
            this.wldMap.ShowMob = false;
            this.wldMap.ShowObj = false;
            this.wldMap.ShowSketchMode = true;
            this.wldMap.ShowTrg = false;
            this.wldMap.ShowVNums = false;
            this.wldMap.Size = new System.Drawing.Size(459, 260);
            this.wldMap.SketchCurrentColor = System.Drawing.Color.Transparent;
            this.wldMap.TabIndex = 0;
            this.wldMap.RoomCreated += new ExtControls.WldMap.RoomCreateEvent(this.WldMapRoomCreated);
            this.wldMap.RoomsSelectionChanged += new ExtControls.WldMap.RoomsSelectionChangeEvent(this.WldMapRoomsSelectionChanged);
            this.wldMap.RoomDroped += new ExtControls.WldMap.RoomDropEvent(this.WldMapRoomDroped);
            this.wldMap.MobDroped += new ExtControls.WldMap.MobDropEvent(this.WldMapMobDroped);
            this.wldMap.ObjDroped += new ExtControls.WldMap.ObjDropEvent(this.WldMapObjDroped);
            // 
            // vertSBMap
            // 
            this.vertSBMap.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.vertSBMap.Dock = System.Windows.Forms.DockStyle.Right;
            this.vertSBMap.LargeChange = 1;
            this.vertSBMap.Location = new System.Drawing.Point(459, 0);
            this.vertSBMap.Maximum = 0;
            this.vertSBMap.Minimum = -100;
            this.vertSBMap.Name = "vertSBMap";
            this.vertSBMap.Size = new System.Drawing.Size(17, 260);
            this.vertSBMap.TabIndex = 2;
            this.vertSBMap.ValueChanged += new System.EventHandler(this.VertSbMapValueChanged);
            // 
            // pnlMapHorizSplitter
            // 
            this.pnlMapHorizSplitter.Controls.Add(this.horizSBMap);
            this.pnlMapHorizSplitter.Controls.Add(this.btnToMapCenter);
            this.pnlMapHorizSplitter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlMapHorizSplitter.Location = new System.Drawing.Point(0, 260);
            this.pnlMapHorizSplitter.Name = "pnlMapHorizSplitter";
            this.pnlMapHorizSplitter.Size = new System.Drawing.Size(476, 18);
            this.pnlMapHorizSplitter.TabIndex = 1;
            // 
            // horizSBMap
            // 
            this.horizSBMap.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.horizSBMap.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.horizSBMap.LargeChange = 1;
            this.horizSBMap.Location = new System.Drawing.Point(0, 1);
            this.horizSBMap.Maximum = 0;
            this.horizSBMap.Minimum = -100;
            this.horizSBMap.Name = "horizSBMap";
            this.horizSBMap.Size = new System.Drawing.Size(458, 17);
            this.horizSBMap.TabIndex = 1;
            this.horizSBMap.ValueChanged += new System.EventHandler(this.HorizSbMapValueChanged);
            // 
            // btnToMapCenter
            // 
            this.btnToMapCenter.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnToMapCenter.Image = global::BZEditor.Properties.Resources.button_to0room;
            this.btnToMapCenter.Location = new System.Drawing.Point(458, 0);
            this.btnToMapCenter.Name = "btnToMapCenter";
            this.btnToMapCenter.Size = new System.Drawing.Size(18, 18);
            this.btnToMapCenter.TabIndex = 0;
            this.btnToMapCenter.UseVisualStyleBackColor = true;
            this.btnToMapCenter.Click += new System.EventHandler(this.ToMapCenterClick);
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tpZone);
            this.tcMain.Controls.Add(this.tpRooms);
            this.tcMain.Controls.Add(this.tpObjects);
            this.tcMain.Controls.Add(this.tpMobs);
            this.tcMain.Controls.Add(this.tpTriggers);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.ImageList = this.iListIcons16;
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(827, 499);
            this.tcMain.TabIndex = 0;
            this.tcMain.SelectedIndexChanged += new System.EventHandler(this.TcMainSelectedIndexChanged);
            // 
            // tpZone
            // 
            this.tpZone.BackColor = System.Drawing.Color.Transparent;
            this.tpZone.Controls.Add(this.splitContainerZon);
            this.tpZone.Location = new System.Drawing.Point(4, 23);
            this.tpZone.Name = "tpZone";
            this.tpZone.Padding = new System.Windows.Forms.Padding(3);
            this.tpZone.Size = new System.Drawing.Size(819, 472);
            this.tpZone.TabIndex = 3;
            this.tpZone.Text = "Зона";
            this.tpZone.UseVisualStyleBackColor = true;
            // 
            // splitContainerZon
            // 
            this.splitContainerZon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerZon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerZon.Location = new System.Drawing.Point(3, 3);
            this.splitContainerZon.Name = "splitContainerZon";
            // 
            // splitContainerZon.Panel1
            // 
            this.splitContainerZon.Panel1.AutoScroll = true;
            this.splitContainerZon.Panel1.Controls.Add(this.nudOptimalCharsInGroup);
            this.splitContainerZon.Panel1.Controls.Add(this.btnChangeZoneNumber);
            this.splitContainerZon.Panel1.Controls.Add(this.nudZoneLevel);
            this.splitContainerZon.Panel1.Controls.Add(this.nudZoneNumber);
            this.splitContainerZon.Panel1.Controls.Add(this.nudRepopTimer);
            this.splitContainerZon.Panel1.Controls.Add(this.label69);
            this.splitContainerZon.Panel1.Controls.Add(this.label63);
            this.splitContainerZon.Panel1.Controls.Add(this.label65);
            this.splitContainerZon.Panel1.Controls.Add(this.cboxZonType);
            this.splitContainerZon.Panel1.Controls.Add(this.tbZoneAuthor);
            this.splitContainerZon.Panel1.Controls.Add(this.tbZoneDescription);
            this.splitContainerZon.Panel1.Controls.Add(this.tbZoneLocation);
            this.splitContainerZon.Panel1.Controls.Add(this.tbZoneComment);
            this.splitContainerZon.Panel1.Controls.Add(this.label66);
            this.splitContainerZon.Panel1.Controls.Add(this.label73);
            this.splitContainerZon.Panel1.Controls.Add(this.label72);
            this.splitContainerZon.Panel1.Controls.Add(this.tbZoneName);
            this.splitContainerZon.Panel1.Controls.Add(this.label91);
            this.splitContainerZon.Panel1.Controls.Add(this.label30);
            this.splitContainerZon.Panel1.Controls.Add(label152);
            this.splitContainerZon.Panel1.Controls.Add(this.label96);
            this.splitContainerZon.Panel1.Controls.Add(this.label64);
            // 
            // splitContainerZon.Panel2
            // 
            this.splitContainerZon.Panel2.Controls.Add(this.tcZon);
            this.splitContainerZon.Size = new System.Drawing.Size(813, 466);
            this.splitContainerZon.SplitterDistance = 307;
            this.splitContainerZon.TabIndex = 0;
            // 
            // btnChangeZoneNumber
            // 
            this.btnChangeZoneNumber.Location = new System.Drawing.Point(69, 14);
            this.btnChangeZoneNumber.Name = "btnChangeZoneNumber";
            this.btnChangeZoneNumber.Size = new System.Drawing.Size(85, 24);
            this.btnChangeZoneNumber.TabIndex = 2;
            this.btnChangeZoneNumber.Text = "Изменить";
            this.btnChangeZoneNumber.Click += new System.EventHandler(this.BtnChangeZoneNumberClick);
            // 
            // nudZoneLevel
            // 
            this.nudZoneLevel.Location = new System.Drawing.Point(4, 285);
            this.nudZoneLevel.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.nudZoneLevel.Name = "nudZoneLevel";
            this.nudZoneLevel.Size = new System.Drawing.Size(43, 20);
            this.nudZoneLevel.TabIndex = 6;
            this.nudZoneLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudZoneLevel.ValueChanged += new System.EventHandler(this.NudZoneLevelValueChanged);
            // 
            // nudZoneNumber
            // 
            this.nudZoneNumber.Enabled = false;
            this.nudZoneNumber.Location = new System.Drawing.Point(4, 16);
            this.nudZoneNumber.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudZoneNumber.Name = "nudZoneNumber";
            this.nudZoneNumber.Size = new System.Drawing.Size(64, 20);
            this.nudZoneNumber.TabIndex = 1;
            // 
            // nudRepopTimer
            // 
            this.nudRepopTimer.Location = new System.Drawing.Point(160, 16);
            this.nudRepopTimer.Maximum = new decimal(new int[] {
            276447231,
            23283,
            0,
            0});
            this.nudRepopTimer.Name = "nudRepopTimer";
            this.nudRepopTimer.Size = new System.Drawing.Size(75, 20);
            this.nudRepopTimer.TabIndex = 3;
            this.nudRepopTimer.ValueChanged += new System.EventHandler(this.NudRepopTimerValueChanged);
            // 
            // label69
            // 
            this.label69.Location = new System.Drawing.Point(233, 14);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(60, 20);
            this.label69.TabIndex = 0;
            this.label69.Text = "(в мин.РЛ)";
            this.label69.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label63
            // 
            this.label63.Location = new System.Drawing.Point(157, -2);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(133, 20);
            this.label63.TabIndex = 0;
            this.label63.Text = "Время перезагрузки";
            this.label63.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label65
            // 
            this.label65.Location = new System.Drawing.Point(1, -2);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(80, 20);
            this.label65.TabIndex = 0;
            this.label65.Text = "Номер зоны";
            this.label65.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboxZonType
            // 
            this.cboxZonType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboxZonType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxZonType.DropDownWidth = 150;
            this.cboxZonType.Location = new System.Drawing.Point(53, 284);
            this.cboxZonType.Name = "cboxZonType";
            this.cboxZonType.Size = new System.Drawing.Size(163, 21);
            this.cboxZonType.TabIndex = 7;
            this.cboxZonType.SelectedIndexChanged += new System.EventHandler(this.CboxZonTypeSelectedIndexChanged);
            // 
            // tbZoneAuthor
            // 
            this.tbZoneAuthor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbZoneAuthor.Location = new System.Drawing.Point(4, 244);
            this.tbZoneAuthor.Name = "tbZoneAuthor";
            this.tbZoneAuthor.Size = new System.Drawing.Size(298, 20);
            this.tbZoneAuthor.TabIndex = 5;
            this.tbZoneAuthor.Text = "Автор";
            this.tbZoneAuthor.Validated += new System.EventHandler(this.TbZoneAuthorValidated);
            // 
            // tbZoneDescription
            // 
            this.tbZoneDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbZoneDescription.Location = new System.Drawing.Point(4, 199);
            this.tbZoneDescription.Name = "tbZoneDescription";
            this.tbZoneDescription.Size = new System.Drawing.Size(298, 20);
            this.tbZoneDescription.TabIndex = 5;
            this.tbZoneDescription.Text = "Описание";
            this.tbZoneDescription.Validated += new System.EventHandler(this.TbZoneDescriptionValidated);
            // 
            // tbZoneLocation
            // 
            this.tbZoneLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbZoneLocation.Location = new System.Drawing.Point(4, 154);
            this.tbZoneLocation.Name = "tbZoneLocation";
            this.tbZoneLocation.Size = new System.Drawing.Size(298, 20);
            this.tbZoneLocation.TabIndex = 5;
            this.tbZoneLocation.Text = "Местоположение";
            this.tbZoneLocation.Validated += new System.EventHandler(this.TbZoneLocationValidated);
            // 
            // tbZoneComment
            // 
            this.tbZoneComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbZoneComment.Location = new System.Drawing.Point(4, 105);
            this.tbZoneComment.Name = "tbZoneComment";
            this.tbZoneComment.Size = new System.Drawing.Size(298, 20);
            this.tbZoneComment.TabIndex = 5;
            this.tbZoneComment.Text = "Комментарий";
            this.tbZoneComment.Validated += new System.EventHandler(this.TbZoneCommentValidated);
            // 
            // label66
            // 
            this.label66.Location = new System.Drawing.Point(1, 223);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(93, 19);
            this.label66.TabIndex = 0;
            this.label66.Text = "Автор";
            this.label66.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label73
            // 
            this.label73.Location = new System.Drawing.Point(1, 178);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(93, 19);
            this.label73.TabIndex = 0;
            this.label73.Text = "Описание";
            this.label73.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label72
            // 
            this.label72.Location = new System.Drawing.Point(1, 133);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(93, 19);
            this.label72.TabIndex = 0;
            this.label72.Text = "Местоположение";
            this.label72.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbZoneName
            // 
            this.tbZoneName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbZoneName.Location = new System.Drawing.Point(4, 60);
            this.tbZoneName.Name = "tbZoneName";
            this.tbZoneName.Size = new System.Drawing.Size(298, 20);
            this.tbZoneName.TabIndex = 4;
            this.tbZoneName.Text = "Наименование зоны";
            this.tbZoneName.Validated += new System.EventHandler(this.TbZoneNameValidated);
            // 
            // label91
            // 
            this.label91.Location = new System.Drawing.Point(1, 84);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(93, 19);
            this.label91.TabIndex = 0;
            this.label91.Text = "Комментарий";
            this.label91.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label30
            // 
            this.label30.Location = new System.Drawing.Point(50, 270);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(136, 15);
            this.label30.TabIndex = 0;
            this.label30.Text = "Тип зоны";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label96
            // 
            this.label96.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label96.Location = new System.Drawing.Point(211, 270);
            this.label96.Name = "label96";
            this.label96.Size = new System.Drawing.Size(92, 15);
            this.label96.TabIndex = 0;
            this.label96.Text = "Опт.чаров в гр.";
            this.label96.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label64
            // 
            this.label64.Location = new System.Drawing.Point(1, 39);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(93, 19);
            this.label64.TabIndex = 0;
            this.label64.Text = "Название зоны";
            this.label64.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tcZon
            // 
            this.tcZon.Controls.Add(this.tpVitrualRoom);
            this.tcZon.Controls.Add(this.tpResetCondition);
            this.tcZon.Controls.Add(this.tpStatistics);
            this.tcZon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcZon.Location = new System.Drawing.Point(0, 0);
            this.tcZon.Name = "tcZon";
            this.tcZon.SelectedIndex = 0;
            this.tcZon.Size = new System.Drawing.Size(500, 464);
            this.tcZon.TabIndex = 3;
            // 
            // tpVitrualRoom
            // 
            this.tpVitrualRoom.Controls.Add(this.splitContainerVirtualRoomMobs);
            this.tpVitrualRoom.Location = new System.Drawing.Point(4, 22);
            this.tpVitrualRoom.Name = "tpVitrualRoom";
            this.tpVitrualRoom.Size = new System.Drawing.Size(492, 438);
            this.tpVitrualRoom.TabIndex = 3;
            this.tpVitrualRoom.Text = "Виртуальная комната";
            this.tpVitrualRoom.UseVisualStyleBackColor = true;
            // 
            // splitContainerVirtualRoomMobs
            // 
            this.splitContainerVirtualRoomMobs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerVirtualRoomMobs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerVirtualRoomMobs.Location = new System.Drawing.Point(0, 0);
            this.splitContainerVirtualRoomMobs.Name = "splitContainerVirtualRoomMobs";
            this.splitContainerVirtualRoomMobs.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerVirtualRoomMobs.Panel1
            // 
            this.splitContainerVirtualRoomMobs.Panel1.Controls.Add(this.bdtAddMobInVirtualRoom);
            this.splitContainerVirtualRoomMobs.Panel1.Controls.Add(this.btnRemoveMobFromVitrualRoom);
            this.splitContainerVirtualRoomMobs.Panel1.Controls.Add(this.lvMobsInVitrualRoom);
            // 
            // splitContainerVirtualRoomMobs.Panel2
            // 
            this.splitContainerVirtualRoomMobs.Panel2.Controls.Add(this.scontMobInVitrualRoomLoadedObjects);
            this.splitContainerVirtualRoomMobs.Size = new System.Drawing.Size(492, 438);
            this.splitContainerVirtualRoomMobs.SplitterDistance = 132;
            this.splitContainerVirtualRoomMobs.TabIndex = 22;
            // 
            // lvMobsInVitrualRoom
            // 
            this.lvMobsInVitrualRoom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvMobsInVitrualRoom.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvMobsInVitrualRoom.ContextMenuStrip = this.cmsGridMenu;
            this.lvMobsInVitrualRoom.FullRowSelect = true;
            this.lvMobsInVitrualRoom.GridLines = true;
            this.lvMobsInVitrualRoom.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvMobsInVitrualRoom.HideSelection = false;
            this.lvMobsInVitrualRoom.Location = new System.Drawing.Point(3, 3);
            this.lvMobsInVitrualRoom.MultiSelect = false;
            this.lvMobsInVitrualRoom.Name = "lvMobsInVitrualRoom";
            this.lvMobsInVitrualRoom.ShowItemToolTips = true;
            this.lvMobsInVitrualRoom.Size = new System.Drawing.Size(454, 124);
            this.lvMobsInVitrualRoom.TabIndex = 16;
            this.lvMobsInVitrualRoom.UseCompatibleStateImageBehavior = false;
            this.lvMobsInVitrualRoom.View = System.Windows.Forms.View.Details;
            this.lvMobsInVitrualRoom.SelectedIndexChanged += new System.EventHandler(this.lvMobsInVitrualRoom_SelectedIndexChanged);
            this.lvMobsInVitrualRoom.SizeChanged += new System.EventHandler(this.LastColumnAutosize);
            this.lvMobsInVitrualRoom.MouseUp += new System.Windows.Forms.MouseEventHandler(this.NavigatedControl_MouseUp);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 50;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Width = 380;
            // 
            // scontMobInVitrualRoomLoadedObjects
            // 
            this.scontMobInVitrualRoomLoadedObjects.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.scontMobInVitrualRoomLoadedObjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scontMobInVitrualRoomLoadedObjects.Enabled = false;
            this.scontMobInVitrualRoomLoadedObjects.Location = new System.Drawing.Point(0, 0);
            this.scontMobInVitrualRoomLoadedObjects.Name = "scontMobInVitrualRoomLoadedObjects";
            this.scontMobInVitrualRoomLoadedObjects.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scontMobInVitrualRoomLoadedObjects.Panel1
            // 
            this.scontMobInVitrualRoomLoadedObjects.Panel1.Controls.Add(this.cboxVitrualRoomMobFollowBy);
            this.scontMobInVitrualRoomLoadedObjects.Panel1.Controls.Add(this.elvVitrualRoomMobObjects);
            this.scontMobInVitrualRoomLoadedObjects.Panel1.Controls.Add(this.label43);
            this.scontMobInVitrualRoomLoadedObjects.Panel1.Controls.Add(this.btnAddItemToMobInVirtualRoom);
            this.scontMobInVitrualRoomLoadedObjects.Panel1.Controls.Add(this.label57);
            this.scontMobInVitrualRoomLoadedObjects.Panel1.Controls.Add(this.btnRemoveItemFromMobInVirtualRoom);
            this.scontMobInVitrualRoomLoadedObjects.Panel1.Controls.Add(this.label40);
            this.scontMobInVitrualRoomLoadedObjects.Panel1.Controls.Add(this.nudVirtualRoomMobMaxInRoom);
            // 
            // scontMobInVitrualRoomLoadedObjects.Panel2
            // 
            this.scontMobInVitrualRoomLoadedObjects.Panel2.Controls.Add(this.elvVitrualRoomMobObjectsAfterDeath);
            this.scontMobInVitrualRoomLoadedObjects.Panel2.Controls.Add(this.btnRemoveItemFromMobInVirtualRoomAfterDeath);
            this.scontMobInVitrualRoomLoadedObjects.Panel2.Controls.Add(this.btnAddItemToMobInVirtualRoomAfterDeath);
            this.scontMobInVitrualRoomLoadedObjects.Panel2.Controls.Add(this.label15);
            this.scontMobInVitrualRoomLoadedObjects.Size = new System.Drawing.Size(492, 302);
            this.scontMobInVitrualRoomLoadedObjects.SplitterDistance = 169;
            this.scontMobInVitrualRoomLoadedObjects.TabIndex = 99;
            // 
            // cboxVitrualRoomMobFollowBy
            // 
            this.cboxVitrualRoomMobFollowBy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboxVitrualRoomMobFollowBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxVitrualRoomMobFollowBy.ItemHeight = 13;
            this.cboxVitrualRoomMobFollowBy.Location = new System.Drawing.Point(2, 14);
            this.cboxVitrualRoomMobFollowBy.Name = "cboxVitrualRoomMobFollowBy";
            this.cboxVitrualRoomMobFollowBy.Size = new System.Drawing.Size(417, 21);
            this.cboxVitrualRoomMobFollowBy.TabIndex = 83;
            this.cboxVitrualRoomMobFollowBy.SelectedIndexChanged += new System.EventHandler(this.cboxVitrualRoomMobFollowBySelectedIndexChanged);
            // 
            // label43
            // 
            this.label43.Location = new System.Drawing.Point(3, -1);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(73, 16);
            this.label43.TabIndex = 82;
            this.label43.Text = "Следует за";
            this.label43.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label57
            // 
            this.label57.Location = new System.Drawing.Point(3, 35);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(107, 16);
            this.label57.TabIndex = 81;
            this.label57.Text = "Объекты моба";
            this.label57.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // elvVitrualRoomMobObjectsAfterDeath
            // 
            this.elvVitrualRoomMobObjectsAfterDeath.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.elvVitrualRoomMobObjectsAfterDeath.ContextMenuStrip = this.cmsGridMenu;
            this.elvVitrualRoomMobObjectsAfterDeath.FullRowSelect = true;
            this.elvVitrualRoomMobObjectsAfterDeath.GridLines = true;
            this.elvVitrualRoomMobObjectsAfterDeath.LabelWrap = false;
            this.elvVitrualRoomMobObjectsAfterDeath.Location = new System.Drawing.Point(3, 16);
            this.elvVitrualRoomMobObjectsAfterDeath.MultiSelect = false;
            this.elvVitrualRoomMobObjectsAfterDeath.Name = "elvVitrualRoomMobObjectsAfterDeath";
            this.elvVitrualRoomMobObjectsAfterDeath.OwnerDraw = true;
            this.elvVitrualRoomMobObjectsAfterDeath.Size = new System.Drawing.Size(454, 108);
            this.elvVitrualRoomMobObjectsAfterDeath.TabIndex = 98;
            this.elvVitrualRoomMobObjectsAfterDeath.UseCompatibleStateImageBehavior = false;
            this.elvVitrualRoomMobObjectsAfterDeath.View = System.Windows.Forms.View.Details;
            this.elvVitrualRoomMobObjectsAfterDeath.ItemValueChanged += new ExtControls.ExtListView.ItemValueChangeEvent(this.elvVitrualRoomMobObjectsAfterDeath_ItemValueChanged);
            this.elvVitrualRoomMobObjectsAfterDeath.SizeChanged += new System.EventHandler(this.LastColumnAutosize);
            this.elvVitrualRoomMobObjectsAfterDeath.MouseUp += new System.Windows.Forms.MouseEventHandler(this.NavigatedControl_MouseUp);
            // 
            // btnRemoveItemFromMobInVirtualRoomAfterDeath
            // 
            this.btnRemoveItemFromMobInVirtualRoomAfterDeath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveItemFromMobInVirtualRoomAfterDeath.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveItemFromMobInVirtualRoomAfterDeath.Image")));
            this.btnRemoveItemFromMobInVirtualRoomAfterDeath.Location = new System.Drawing.Point(460, 46);
            this.btnRemoveItemFromMobInVirtualRoomAfterDeath.Name = "btnRemoveItemFromMobInVirtualRoomAfterDeath";
            this.btnRemoveItemFromMobInVirtualRoomAfterDeath.Size = new System.Drawing.Size(28, 28);
            this.btnRemoveItemFromMobInVirtualRoomAfterDeath.TabIndex = 95;
            this.btnRemoveItemFromMobInVirtualRoomAfterDeath.Click += new System.EventHandler(this.btnRemoveItemFromMobInVirtualRoomAfterDeathClick);
            // 
            // btnAddItemToMobInVirtualRoomAfterDeath
            // 
            this.btnAddItemToMobInVirtualRoomAfterDeath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddItemToMobInVirtualRoomAfterDeath.Image = ((System.Drawing.Image)(resources.GetObject("btnAddItemToMobInVirtualRoomAfterDeath.Image")));
            this.btnAddItemToMobInVirtualRoomAfterDeath.Location = new System.Drawing.Point(460, 16);
            this.btnAddItemToMobInVirtualRoomAfterDeath.Name = "btnAddItemToMobInVirtualRoomAfterDeath";
            this.btnAddItemToMobInVirtualRoomAfterDeath.Size = new System.Drawing.Size(28, 28);
            this.btnAddItemToMobInVirtualRoomAfterDeath.TabIndex = 96;
            this.btnAddItemToMobInVirtualRoomAfterDeath.Click += new System.EventHandler(this.btnAddItemToMobInVirtualRoomAfterDeathClick);
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(3, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(244, 16);
            this.label15.TabIndex = 81;
            this.label15.Text = "Объекты моба, загружаемые посмертно";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tpResetCondition
            // 
            this.tpResetCondition.Controls.Add(this.gbResetRelatedZones);
            this.tpResetCondition.Controls.Add(this.cbZoneReopopType);
            this.tpResetCondition.Location = new System.Drawing.Point(4, 22);
            this.tpResetCondition.Name = "tpResetCondition";
            this.tpResetCondition.Size = new System.Drawing.Size(492, 438);
            this.tpResetCondition.TabIndex = 4;
            this.tpResetCondition.Text = "Условие перезагрузки";
            this.tpResetCondition.UseVisualStyleBackColor = true;
            // 
            // gbResetRelatedZones
            // 
            this.gbResetRelatedZones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbResetRelatedZones.Controls.Add(this.splitContainerRepop);
            this.gbResetRelatedZones.Location = new System.Drawing.Point(4, 35);
            this.gbResetRelatedZones.Name = "gbResetRelatedZones";
            this.gbResetRelatedZones.Size = new System.Drawing.Size(483, 400);
            this.gbResetRelatedZones.TabIndex = 10;
            this.gbResetRelatedZones.TabStop = false;
            this.gbResetRelatedZones.Text = "Зоны, перезагружаемые одновременно с текущей";
            // 
            // splitContainerRepop
            // 
            this.splitContainerRepop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerRepop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerRepop.Location = new System.Drawing.Point(3, 16);
            this.splitContainerRepop.Name = "splitContainerRepop";
            this.splitContainerRepop.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerRepop.Panel1
            // 
            this.splitContainerRepop.Panel1.Controls.Add(this.label82);
            this.splitContainerRepop.Panel1.Controls.Add(this.lvAZones);
            this.splitContainerRepop.Panel1.Controls.Add(this.btnAddAZones);
            this.splitContainerRepop.Panel1.Controls.Add(this.btnRemoveAZones);
            // 
            // splitContainerRepop.Panel2
            // 
            this.splitContainerRepop.Panel2.Controls.Add(this.btnAddBZones);
            this.splitContainerRepop.Panel2.Controls.Add(this.lvBZones);
            this.splitContainerRepop.Panel2.Controls.Add(this.label97);
            this.splitContainerRepop.Panel2.Controls.Add(this.btnRemoveBZones);
            this.splitContainerRepop.Size = new System.Drawing.Size(477, 381);
            this.splitContainerRepop.SplitterDistance = 182;
            this.splitContainerRepop.TabIndex = 14;
            // 
            // label82
            // 
            this.label82.Dock = System.Windows.Forms.DockStyle.Top;
            this.label82.Location = new System.Drawing.Point(0, 0);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(475, 15);
            this.label82.TabIndex = 0;
            this.label82.Text = "\"A\" перегружаются, если в них нет игроков";
            this.label82.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lvAZones
            // 
            this.lvAZones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvAZones.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader30,
            this.columnHeader31});
            this.lvAZones.FullRowSelect = true;
            this.lvAZones.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvAZones.HideSelection = false;
            this.lvAZones.Location = new System.Drawing.Point(3, 18);
            this.lvAZones.Name = "lvAZones";
            this.lvAZones.Size = new System.Drawing.Size(439, 159);
            this.lvAZones.TabIndex = 10;
            this.lvAZones.UseCompatibleStateImageBehavior = false;
            this.lvAZones.View = System.Windows.Forms.View.Details;
            this.lvAZones.SizeChanged += new System.EventHandler(this.LastColumnAutosize);
            // 
            // columnHeader30
            // 
            this.columnHeader30.Width = 50;
            // 
            // columnHeader31
            // 
            this.columnHeader31.Width = 199;
            // 
            // lvBZones
            // 
            this.lvBZones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvBZones.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader32,
            this.columnHeader33});
            this.lvBZones.FullRowSelect = true;
            this.lvBZones.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvBZones.HideSelection = false;
            this.lvBZones.Location = new System.Drawing.Point(3, 18);
            this.lvBZones.Name = "lvBZones";
            this.lvBZones.Size = new System.Drawing.Size(439, 173);
            this.lvBZones.TabIndex = 13;
            this.lvBZones.UseCompatibleStateImageBehavior = false;
            this.lvBZones.View = System.Windows.Forms.View.Details;
            this.lvBZones.SizeChanged += new System.EventHandler(this.LastColumnAutosize);
            // 
            // columnHeader32
            // 
            this.columnHeader32.Width = 50;
            // 
            // columnHeader33
            // 
            this.columnHeader33.Width = 199;
            // 
            // label97
            // 
            this.label97.Dock = System.Windows.Forms.DockStyle.Top;
            this.label97.Location = new System.Drawing.Point(0, 0);
            this.label97.Name = "label97";
            this.label97.Size = new System.Drawing.Size(475, 15);
            this.label97.TabIndex = 0;
            this.label97.Text = "\"B\" перегружаются в любом случае";
            this.label97.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbZoneReopopType
            // 
            this.cbZoneReopopType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbZoneReopopType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbZoneReopopType.DropDownWidth = 200;
            this.cbZoneReopopType.Location = new System.Drawing.Point(5, 8);
            this.cbZoneReopopType.Name = "cbZoneReopopType";
            this.cbZoneReopopType.Size = new System.Drawing.Size(481, 21);
            this.cbZoneReopopType.TabIndex = 9;
            this.cbZoneReopopType.SelectedIndexChanged += new System.EventHandler(this.CbZoneReopopTypeSelectedIndexChanged);
            // 
            // tpStatistics
            // 
            this.tpStatistics.Controls.Add(this.mlbValidationResult);
            this.tpStatistics.Controls.Add(this.btnValidate);
            this.tpStatistics.Controls.Add(this.cbShowInfo);
            this.tpStatistics.Controls.Add(this.cbShowWarnings);
            this.tpStatistics.Controls.Add(this.cbShowErrors);
            this.tpStatistics.Controls.Add(this.label126);
            this.tpStatistics.Controls.Add(this.label70);
            this.tpStatistics.Location = new System.Drawing.Point(4, 22);
            this.tpStatistics.Name = "tpStatistics";
            this.tpStatistics.Size = new System.Drawing.Size(492, 438);
            this.tpStatistics.TabIndex = 2;
            this.tpStatistics.Text = "Ошибки и предупреждения";
            this.tpStatistics.UseVisualStyleBackColor = true;
            // 
            // mlbValidationResult
            // 
            this.mlbValidationResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mlbValidationResult.AutoScroll = true;
            this.mlbValidationResult.AutoScrollMinSize = new System.Drawing.Size(442, 0);
            this.mlbValidationResult.BackColor = System.Drawing.Color.White;
            this.mlbValidationResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mlbValidationResult.Location = new System.Drawing.Point(0, 43);
            this.mlbValidationResult.Name = "mlbValidationResult";
            this.mlbValidationResult.SelectedIndex = -1;
            this.mlbValidationResult.SelectedItem = null;
            this.mlbValidationResult.Size = new System.Drawing.Size(492, 395);
            this.mlbValidationResult.TabIndex = 4;
            this.mlbValidationResult.DoubleClick += new System.EventHandler(this.MlbValidationResultDoubleClick);
            // 
            // cbShowInfo
            // 
            this.cbShowInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cbShowInfo.Checked = true;
            this.cbShowInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowInfo.Image = global::BZEditor.Properties.Resources.info;
            this.cbShowInfo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cbShowInfo.Location = new System.Drawing.Point(403, 4);
            this.cbShowInfo.Name = "cbShowInfo";
            this.cbShowInfo.Size = new System.Drawing.Size(39, 26);
            this.cbShowInfo.TabIndex = 5;
            this.cbShowInfo.UseVisualStyleBackColor = true;
            this.cbShowInfo.CheckedChanged += new System.EventHandler(this.ErrVisibilityChanged);
            // 
            // cbShowWarnings
            // 
            this.cbShowWarnings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cbShowWarnings.Checked = true;
            this.cbShowWarnings.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowWarnings.Image = global::BZEditor.Properties.Resources.alert16;
            this.cbShowWarnings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cbShowWarnings.Location = new System.Drawing.Point(361, 4);
            this.cbShowWarnings.Name = "cbShowWarnings";
            this.cbShowWarnings.Size = new System.Drawing.Size(39, 26);
            this.cbShowWarnings.TabIndex = 5;
            this.cbShowWarnings.UseVisualStyleBackColor = true;
            this.cbShowWarnings.CheckedChanged += new System.EventHandler(this.ErrVisibilityChanged);
            // 
            // cbShowErrors
            // 
            this.cbShowErrors.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cbShowErrors.Checked = true;
            this.cbShowErrors.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowErrors.Image = global::BZEditor.Properties.Resources.file_close1;
            this.cbShowErrors.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cbShowErrors.Location = new System.Drawing.Point(321, 4);
            this.cbShowErrors.Name = "cbShowErrors";
            this.cbShowErrors.Size = new System.Drawing.Size(39, 26);
            this.cbShowErrors.TabIndex = 5;
            this.cbShowErrors.UseVisualStyleBackColor = true;
            this.cbShowErrors.CheckedChanged += new System.EventHandler(this.ErrVisibilityChanged);
            // 
            // label126
            // 
            this.label126.Location = new System.Drawing.Point(94, 4);
            this.label126.Name = "label126";
            this.label126.Size = new System.Drawing.Size(234, 20);
            this.label126.TabIndex = 0;
            this.label126.Text = "Отображаемые несоответствия правилам:";
            this.label126.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label70
            // 
            this.label70.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label70.AutoSize = true;
            this.label70.ForeColor = System.Drawing.Color.DimGray;
            this.label70.Location = new System.Drawing.Point(3, 27);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(449, 13);
            this.label70.TabIndex = 6;
            this.label70.Text = "Используйте даблклик по сообщениям об ошибках для навигации к источнику ошибки";
            this.label70.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tpRooms
            // 
            this.tpRooms.BackColor = System.Drawing.Color.Transparent;
            this.tpRooms.Controls.Add(this.splitContainerRoomsDesc);
            this.tpRooms.ImageKey = "(none)";
            this.tpRooms.Location = new System.Drawing.Point(4, 23);
            this.tpRooms.Name = "tpRooms";
            this.tpRooms.Size = new System.Drawing.Size(819, 472);
            this.tpRooms.TabIndex = 4;
            this.tpRooms.Text = "Комнаты";
            this.tpRooms.UseVisualStyleBackColor = true;
            // 
            // splitContainerRoomsDesc
            // 
            this.splitContainerRoomsDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerRoomsDesc.Location = new System.Drawing.Point(0, 0);
            this.splitContainerRoomsDesc.Name = "splitContainerRoomsDesc";
            this.splitContainerRoomsDesc.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerRoomsDesc.Panel1
            // 
            this.splitContainerRoomsDesc.Panel1.Controls.Add(this.splitContainerRooms);
            // 
            // splitContainerRoomsDesc.Panel2
            // 
            this.splitContainerRoomsDesc.Panel2.Controls.Add(this.panel2);
            this.splitContainerRoomsDesc.Panel2.Controls.Add(this.tabControlRoomDescriptions);
            this.splitContainerRoomsDesc.Panel2.Controls.Add(this.pnlFormating);
            this.splitContainerRoomsDesc.Size = new System.Drawing.Size(819, 472);
            this.splitContainerRoomsDesc.SplitterDistance = 316;
            this.splitContainerRoomsDesc.TabIndex = 89;
            // 
            // splitContainerRooms
            // 
            this.splitContainerRooms.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerRooms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerRooms.Location = new System.Drawing.Point(0, 0);
            this.splitContainerRooms.Name = "splitContainerRooms";
            // 
            // splitContainerRooms.Panel1
            // 
            this.splitContainerRooms.Panel1.AutoScroll = true;
            this.splitContainerRooms.Panel1.Controls.Add(this.gboxExits);
            this.splitContainerRooms.Panel1.Controls.Add(this.cboxSectorType);
            this.splitContainerRooms.Panel1.Controls.Add(this.label81);
            this.splitContainerRooms.Panel1.Controls.Add(this.tbRoomName);
            this.splitContainerRooms.Panel1.Controls.Add(this.label80);
            this.splitContainerRooms.Panel1.Controls.Add(this.lRoomDesc);
            this.splitContainerRooms.Panel1.Padding = new System.Windows.Forms.Padding(3);
            // 
            // splitContainerRooms.Panel2
            // 
            this.splitContainerRooms.Panel2.Controls.Add(this.tcRoom);
            this.splitContainerRooms.Size = new System.Drawing.Size(819, 316);
            this.splitContainerRooms.SplitterDistance = 284;
            this.splitContainerRooms.TabIndex = 1;
            // 
            // gboxExits
            // 
            this.gboxExits.Controls.Add(this.tbExitSouth);
            this.gboxExits.Controls.Add(this.tbExitNorth);
            this.gboxExits.Controls.Add(this.tbExitDown);
            this.gboxExits.Controls.Add(this.tbExitUp);
            this.gboxExits.Controls.Add(this.tbExitEast);
            this.gboxExits.Controls.Add(this.tbExitWest);
            this.gboxExits.Controls.Add(this.bSelectExitDown);
            this.gboxExits.Controls.Add(this.bSelectExitSouth);
            this.gboxExits.Controls.Add(this.bSelectExitEast);
            this.gboxExits.Controls.Add(this.bSelectExitWest);
            this.gboxExits.Controls.Add(this.bSelectExitUp);
            this.gboxExits.Controls.Add(this.bSelectExitNorth);
            this.gboxExits.Dock = System.Windows.Forms.DockStyle.Top;
            this.gboxExits.Location = new System.Drawing.Point(3, 96);
            this.gboxExits.Name = "gboxExits";
            this.gboxExits.Size = new System.Drawing.Size(276, 102);
            this.gboxExits.TabIndex = 84;
            this.gboxExits.TabStop = false;
            this.gboxExits.Text = "Выходы";
            // 
            // bSelectExitDown
            // 
            this.bSelectExitDown.Location = new System.Drawing.Point(179, 71);
            this.bSelectExitDown.Name = "bSelectExitDown";
            this.bSelectExitDown.Size = new System.Drawing.Size(20, 20);
            this.bSelectExitDown.TabIndex = 8;
            this.bSelectExitDown.Text = "v";
            this.bSelectExitDown.UseVisualStyleBackColor = true;
            this.bSelectExitDown.Click += new System.EventHandler(this.BSelectExitDownClick);
            // 
            // bSelectExitSouth
            // 
            this.bSelectExitSouth.Location = new System.Drawing.Point(48, 71);
            this.bSelectExitSouth.Name = "bSelectExitSouth";
            this.bSelectExitSouth.Size = new System.Drawing.Size(20, 20);
            this.bSelectExitSouth.TabIndex = 4;
            this.bSelectExitSouth.Text = "Ю";
            this.bSelectExitSouth.UseVisualStyleBackColor = true;
            this.bSelectExitSouth.Click += new System.EventHandler(this.BSelectExitSouthClick);
            // 
            // bSelectExitEast
            // 
            this.bSelectExitEast.Location = new System.Drawing.Point(88, 45);
            this.bSelectExitEast.Name = "bSelectExitEast";
            this.bSelectExitEast.Size = new System.Drawing.Size(20, 20);
            this.bSelectExitEast.TabIndex = 6;
            this.bSelectExitEast.Text = "В";
            this.bSelectExitEast.UseVisualStyleBackColor = true;
            this.bSelectExitEast.Click += new System.EventHandler(this.BSelectExitEastClick);
            // 
            // bSelectExitWest
            // 
            this.bSelectExitWest.Location = new System.Drawing.Point(6, 45);
            this.bSelectExitWest.Name = "bSelectExitWest";
            this.bSelectExitWest.Size = new System.Drawing.Size(20, 20);
            this.bSelectExitWest.TabIndex = 5;
            this.bSelectExitWest.Text = "З";
            this.bSelectExitWest.UseVisualStyleBackColor = true;
            this.bSelectExitWest.Click += new System.EventHandler(this.BSelectExitWestClick);
            // 
            // bSelectExitUp
            // 
            this.bSelectExitUp.Location = new System.Drawing.Point(179, 19);
            this.bSelectExitUp.Name = "bSelectExitUp";
            this.bSelectExitUp.Size = new System.Drawing.Size(20, 20);
            this.bSelectExitUp.TabIndex = 7;
            this.bSelectExitUp.Text = "^";
            this.bSelectExitUp.UseVisualStyleBackColor = true;
            this.bSelectExitUp.Click += new System.EventHandler(this.BSelectExitUpClick);
            // 
            // bSelectExitNorth
            // 
            this.bSelectExitNorth.Location = new System.Drawing.Point(48, 19);
            this.bSelectExitNorth.Name = "bSelectExitNorth";
            this.bSelectExitNorth.Size = new System.Drawing.Size(20, 20);
            this.bSelectExitNorth.TabIndex = 3;
            this.bSelectExitNorth.Text = "С";
            this.bSelectExitNorth.UseVisualStyleBackColor = true;
            this.bSelectExitNorth.Click += new System.EventHandler(this.BSelectExitNorthClick);
            // 
            // cboxSectorType
            // 
            this.cboxSectorType.Dock = System.Windows.Forms.DockStyle.Top;
            this.cboxSectorType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxSectorType.ItemHeight = 13;
            this.cboxSectorType.Location = new System.Drawing.Point(3, 75);
            this.cboxSectorType.Name = "cboxSectorType";
            this.cboxSectorType.Size = new System.Drawing.Size(276, 21);
            this.cboxSectorType.TabIndex = 2;
            this.cboxSectorType.SelectedIndexChanged += new System.EventHandler(this.CboxSectorTypeSelectedIndexChanged);
            // 
            // label81
            // 
            this.label81.Dock = System.Windows.Forms.DockStyle.Top;
            this.label81.Location = new System.Drawing.Point(3, 59);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(276, 16);
            this.label81.TabIndex = 21;
            this.label81.Text = "Тип сектора";
            this.label81.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbRoomName
            // 
            this.tbRoomName.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbRoomName.Location = new System.Drawing.Point(3, 39);
            this.tbRoomName.Name = "tbRoomName";
            this.tbRoomName.Size = new System.Drawing.Size(276, 20);
            this.tbRoomName.TabIndex = 1;
            this.tbRoomName.Validated += new System.EventHandler(this.TbRoomNameValidated);
            // 
            // label80
            // 
            this.label80.Dock = System.Windows.Forms.DockStyle.Top;
            this.label80.Location = new System.Drawing.Point(3, 23);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(276, 16);
            this.label80.TabIndex = 80;
            this.label80.Text = "Название";
            this.label80.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lRoomDesc
            // 
            this.lRoomDesc.BackColor = System.Drawing.SystemColors.Info;
            this.lRoomDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lRoomDesc.Dock = System.Windows.Forms.DockStyle.Top;
            this.lRoomDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lRoomDesc.ForeColor = System.Drawing.Color.MediumBlue;
            this.lRoomDesc.Location = new System.Drawing.Point(3, 3);
            this.lRoomDesc.Name = "lRoomDesc";
            this.lRoomDesc.Size = new System.Drawing.Size(276, 20);
            this.lRoomDesc.TabIndex = 85;
            this.lRoomDesc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tcRoom
            // 
            this.tcRoom.Controls.Add(this.tpRoomDoors);
            this.tcRoom.Controls.Add(this.tpRoomFlags);
            this.tcRoom.Controls.Add(this.tpRoomObjs);
            this.tcRoom.Controls.Add(this.tpRoomMobs);
            this.tcRoom.Controls.Add(this.tpRoomTrgs);
            this.tcRoom.Controls.Add(this.tpRoomDelObjs);
            this.tcRoom.Controls.Add(this.tpRoomAddDescrs);
            this.tcRoom.Controls.Add(this.tpRoomIngrediens);
            this.tcRoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcRoom.Location = new System.Drawing.Point(0, 0);
            this.tcRoom.Multiline = true;
            this.tcRoom.Name = "tcRoom";
            this.tcRoom.SelectedIndex = 0;
            this.tcRoom.Size = new System.Drawing.Size(529, 314);
            this.tcRoom.TabIndex = 1;
            this.tcRoom.SelectedIndexChanged += new System.EventHandler(this.TcRoomSelectedIndexChanged);
            // 
            // tpRoomDoors
            // 
            this.tpRoomDoors.AutoScroll = true;
            this.tpRoomDoors.Controls.Add(this.pDoors);
            this.tpRoomDoors.Location = new System.Drawing.Point(4, 22);
            this.tpRoomDoors.Name = "tpRoomDoors";
            this.tpRoomDoors.Size = new System.Drawing.Size(521, 288);
            this.tpRoomDoors.TabIndex = 7;
            this.tpRoomDoors.Text = "Двери";
            this.tpRoomDoors.UseVisualStyleBackColor = true;
            // 
            // pDoors
            // 
            this.pDoors.AutoScroll = true;
            this.pDoors.Controls.Add(this.gbDoorType);
            this.pDoors.Controls.Add(this.btnConfigExitDown);
            this.pDoors.Controls.Add(this.btnConfigExitSouth);
            this.pDoors.Controls.Add(this.btnConfigExitEast);
            this.pDoors.Controls.Add(this.btnConfigExitWest);
            this.pDoors.Controls.Add(this.btnConfigExitUp);
            this.pDoors.Controls.Add(this.btnConfigExitNorth);
            this.pDoors.Controls.Add(this.tbDoorDesc);
            this.pDoors.Controls.Add(this.tbDoorNameVin);
            this.pDoors.Controls.Add(this.tbDoorAlias);
            this.pDoors.Controls.Add(this.label99);
            this.pDoors.Controls.Add(this.label86);
            this.pDoors.Controls.Add(this.label89);
            this.pDoors.Controls.Add(this.label90);
            this.pDoors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pDoors.Location = new System.Drawing.Point(0, 0);
            this.pDoors.Name = "pDoors";
            this.pDoors.Size = new System.Drawing.Size(521, 288);
            this.pDoors.TabIndex = 0;
            // 
            // gbDoorType
            // 
            this.gbDoorType.Controls.Add(this.nudLockLevel);
            this.gbDoorType.Controls.Add(label172);
            this.gbDoorType.Controls.Add(this.label93);
            this.gbDoorType.Controls.Add(this.cbDoorLocked);
            this.gbDoorType.Controls.Add(this.cbDoorPeekproof);
            this.gbDoorType.Controls.Add(this.cbDoorClosed);
            this.gbDoorType.Controls.Add(this.cbExitDoor);
            this.gbDoorType.Controls.Add(this.cbExitVisible);
            this.gbDoorType.Controls.Add(this.cbExitHidden);
            this.gbDoorType.Controls.Add(this.btnSelectDoorKey);
            this.gbDoorType.Controls.Add(this.tbRoomDoorKeyName);
            this.gbDoorType.Controls.Add(this.nudDoorKeyVNum);
            this.gbDoorType.Controls.Add(this.label92);
            this.gbDoorType.Location = new System.Drawing.Point(5, 70);
            this.gbDoorType.Name = "gbDoorType";
            this.gbDoorType.Size = new System.Drawing.Size(318, 124);
            this.gbDoorType.TabIndex = 121;
            this.gbDoorType.TabStop = false;
            this.gbDoorType.Text = "Параметры выхода";
            // 
            // nudLockLevel
            // 
            this.nudLockLevel.Location = new System.Drawing.Point(145, 68);
            this.nudLockLevel.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudLockLevel.Name = "nudLockLevel";
            this.nudLockLevel.Size = new System.Drawing.Size(47, 20);
            this.nudLockLevel.TabIndex = 124;
            this.nudLockLevel.ValueChanged += new System.EventHandler(this.LockLevelValueChanged);
            // 
            // cbDoorLocked
            // 
            this.cbDoorLocked.AutoSize = true;
            this.cbDoorLocked.Location = new System.Drawing.Point(71, 49);
            this.cbDoorLocked.Name = "cbDoorLocked";
            this.cbDoorLocked.Size = new System.Drawing.Size(68, 17);
            this.cbDoorLocked.TabIndex = 122;
            this.cbDoorLocked.Text = "Заперта";
            this.cbDoorLocked.UseVisualStyleBackColor = true;
            this.cbDoorLocked.CheckedChanged += new System.EventHandler(this.DoorLockedCheckedChanged);
            // 
            // cbDoorPeekproof
            // 
            this.cbDoorPeekproof.AutoSize = true;
            this.cbDoorPeekproof.Location = new System.Drawing.Point(145, 49);
            this.cbDoorPeekproof.Name = "cbDoorPeekproof";
            this.cbDoorPeekproof.Size = new System.Drawing.Size(92, 17);
            this.cbDoorPeekproof.TabIndex = 122;
            this.cbDoorPeekproof.Text = "Не взломать";
            this.cbDoorPeekproof.UseVisualStyleBackColor = true;
            this.cbDoorPeekproof.CheckedChanged += new System.EventHandler(this.DoorPeekproofCheckedChanged);
            // 
            // cbDoorClosed
            // 
            this.cbDoorClosed.AutoSize = true;
            this.cbDoorClosed.Location = new System.Drawing.Point(71, 32);
            this.cbDoorClosed.Name = "cbDoorClosed";
            this.cbDoorClosed.Size = new System.Drawing.Size(70, 17);
            this.cbDoorClosed.TabIndex = 122;
            this.cbDoorClosed.Text = "Закрыта";
            this.cbDoorClosed.UseVisualStyleBackColor = true;
            this.cbDoorClosed.CheckedChanged += new System.EventHandler(this.DoorClosedCheckedChanged);
            // 
            // cbExitDoor
            // 
            this.cbExitDoor.AutoSize = true;
            this.cbExitDoor.Location = new System.Drawing.Point(6, 32);
            this.cbExitDoor.Name = "cbExitDoor";
            this.cbExitDoor.Size = new System.Drawing.Size(59, 17);
            this.cbExitDoor.TabIndex = 122;
            this.cbExitDoor.Text = "Дверь";
            this.cbExitDoor.UseVisualStyleBackColor = true;
            this.cbExitDoor.CheckedChanged += new System.EventHandler(this.ExitDoorCheckedChanged);
            // 
            // cbExitVisible
            // 
            this.cbExitVisible.AutoSize = true;
            this.cbExitVisible.Location = new System.Drawing.Point(112, 14);
            this.cbExitVisible.Name = "cbExitVisible";
            this.cbExitVisible.Size = new System.Drawing.Size(107, 17);
            this.cbExitVisible.TabIndex = 122;
            this.cbExitVisible.Tag = "4";
            this.cbExitVisible.Text = "Видимый выход";
            this.cbExitVisible.UseVisualStyleBackColor = true;
            this.cbExitVisible.CheckedChanged += new System.EventHandler(this.ExitVisibleCheckedChanged);
            // 
            // cbExitHidden
            // 
            this.cbExitHidden.AutoSize = true;
            this.cbExitHidden.Location = new System.Drawing.Point(6, 14);
            this.cbExitHidden.Name = "cbExitHidden";
            this.cbExitHidden.Size = new System.Drawing.Size(106, 17);
            this.cbExitHidden.TabIndex = 122;
            this.cbExitHidden.Tag = "4";
            this.cbExitHidden.Text = "Скрытый выход";
            this.cbExitHidden.UseVisualStyleBackColor = true;
            this.cbExitHidden.CheckedChanged += new System.EventHandler(this.ExitHiddenCheckedChanged);
            // 
            // tbRoomDoorKeyName
            // 
            this.tbRoomDoorKeyName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRoomDoorKeyName.Location = new System.Drawing.Point(140, 98);
            this.tbRoomDoorKeyName.Name = "tbRoomDoorKeyName";
            this.tbRoomDoorKeyName.ReadOnly = true;
            this.tbRoomDoorKeyName.Size = new System.Drawing.Size(172, 20);
            this.tbRoomDoorKeyName.TabIndex = 119;
            // 
            // nudDoorKeyVNum
            // 
            this.nudDoorKeyVNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudDoorKeyVNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudDoorKeyVNum.Enabled = false;
            this.nudDoorKeyVNum.Location = new System.Drawing.Point(41, 98);
            this.nudDoorKeyVNum.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.nudDoorKeyVNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.nudDoorKeyVNum.Name = "nudDoorKeyVNum";
            this.nudDoorKeyVNum.Size = new System.Drawing.Size(69, 20);
            this.nudDoorKeyVNum.TabIndex = 118;
            this.nudDoorKeyVNum.ValueChanged += new System.EventHandler(this.NudDoorKeyVNumValueChanged);
            // 
            // label92
            // 
            this.label92.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label92.Location = new System.Drawing.Point(4, 99);
            this.label92.Name = "label92";
            this.label92.Size = new System.Drawing.Size(42, 16);
            this.label92.TabIndex = 113;
            this.label92.Text = "Ключ";
            this.label92.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnConfigExitDown
            // 
            this.btnConfigExitDown.Location = new System.Drawing.Point(213, 48);
            this.btnConfigExitDown.Name = "btnConfigExitDown";
            this.btnConfigExitDown.Size = new System.Drawing.Size(60, 21);
            this.btnConfigExitDown.TabIndex = 109;
            this.btnConfigExitDown.Text = "Низ";
            this.btnConfigExitDown.UseVisualStyleBackColor = true;
            this.btnConfigExitDown.Visible = false;
            this.btnConfigExitDown.Click += new System.EventHandler(this.DoorDirectionSelect);
            // 
            // btnConfigExitSouth
            // 
            this.btnConfigExitSouth.Location = new System.Drawing.Point(138, 48);
            this.btnConfigExitSouth.Name = "btnConfigExitSouth";
            this.btnConfigExitSouth.Size = new System.Drawing.Size(60, 21);
            this.btnConfigExitSouth.TabIndex = 110;
            this.btnConfigExitSouth.Text = "Юг";
            this.btnConfigExitSouth.UseVisualStyleBackColor = true;
            this.btnConfigExitSouth.Visible = false;
            this.btnConfigExitSouth.Click += new System.EventHandler(this.DoorDirectionSelect);
            // 
            // btnConfigExitEast
            // 
            this.btnConfigExitEast.Location = new System.Drawing.Point(175, 26);
            this.btnConfigExitEast.Name = "btnConfigExitEast";
            this.btnConfigExitEast.Size = new System.Drawing.Size(60, 21);
            this.btnConfigExitEast.TabIndex = 111;
            this.btnConfigExitEast.Text = "Восток";
            this.btnConfigExitEast.UseVisualStyleBackColor = true;
            this.btnConfigExitEast.Visible = false;
            this.btnConfigExitEast.Click += new System.EventHandler(this.DoorDirectionSelect);
            // 
            // btnConfigExitWest
            // 
            this.btnConfigExitWest.Location = new System.Drawing.Point(100, 26);
            this.btnConfigExitWest.Name = "btnConfigExitWest";
            this.btnConfigExitWest.Size = new System.Drawing.Size(60, 21);
            this.btnConfigExitWest.TabIndex = 106;
            this.btnConfigExitWest.Text = "Запад";
            this.btnConfigExitWest.UseVisualStyleBackColor = true;
            this.btnConfigExitWest.Visible = false;
            this.btnConfigExitWest.Click += new System.EventHandler(this.DoorDirectionSelect);
            // 
            // btnConfigExitUp
            // 
            this.btnConfigExitUp.Location = new System.Drawing.Point(213, 4);
            this.btnConfigExitUp.Name = "btnConfigExitUp";
            this.btnConfigExitUp.Size = new System.Drawing.Size(60, 21);
            this.btnConfigExitUp.TabIndex = 107;
            this.btnConfigExitUp.Text = "Верх";
            this.btnConfigExitUp.UseVisualStyleBackColor = true;
            this.btnConfigExitUp.Visible = false;
            this.btnConfigExitUp.Click += new System.EventHandler(this.DoorDirectionSelect);
            // 
            // btnConfigExitNorth
            // 
            this.btnConfigExitNorth.Location = new System.Drawing.Point(138, 4);
            this.btnConfigExitNorth.Name = "btnConfigExitNorth";
            this.btnConfigExitNorth.Size = new System.Drawing.Size(60, 21);
            this.btnConfigExitNorth.TabIndex = 108;
            this.btnConfigExitNorth.Text = "Север";
            this.btnConfigExitNorth.UseVisualStyleBackColor = true;
            this.btnConfigExitNorth.Visible = false;
            this.btnConfigExitNorth.Click += new System.EventHandler(this.DoorDirectionSelect);
            // 
            // tbDoorDesc
            // 
            this.tbDoorDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDoorDesc.Enabled = false;
            this.tbDoorDesc.Location = new System.Drawing.Point(329, 89);
            this.tbDoorDesc.Name = "tbDoorDesc";
            this.tbDoorDesc.Size = new System.Drawing.Size(185, 20);
            this.tbDoorDesc.TabIndex = 104;
            this.tbDoorDesc.Validated += new System.EventHandler(this.TbDoorDescValidated);
            // 
            // tbDoorAlias
            // 
            this.tbDoorAlias.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDoorAlias.Enabled = false;
            this.tbDoorAlias.Location = new System.Drawing.Point(329, 171);
            this.tbDoorAlias.Name = "tbDoorAlias";
            this.tbDoorAlias.Size = new System.Drawing.Size(185, 20);
            this.tbDoorAlias.TabIndex = 105;
            this.tbDoorAlias.Validated += new System.EventHandler(this.TbDoorAliasValidated);
            // 
            // label99
            // 
            this.label99.Location = new System.Drawing.Point(326, 112);
            this.label99.Name = "label99";
            this.label99.Size = new System.Drawing.Size(173, 16);
            this.label99.TabIndex = 102;
            this.label99.Text = "Название выхода в вин. падеже";
            this.label99.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label86
            // 
            this.label86.Location = new System.Drawing.Point(326, 152);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(167, 16);
            this.label86.TabIndex = 102;
            this.label86.Text = "Альясы выхода ";
            this.label86.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label89
            // 
            this.label89.Location = new System.Drawing.Point(326, 70);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(107, 16);
            this.label89.TabIndex = 101;
            this.label89.Text = "Описание выхода:";
            this.label89.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label90
            // 
            this.label90.Location = new System.Drawing.Point(2, 26);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(146, 16);
            this.label90.TabIndex = 103;
            this.label90.Text = "Направление:";
            this.label90.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tpRoomFlags
            // 
            this.tpRoomFlags.Controls.Add(this.tplRoomFlags);
            this.tpRoomFlags.Controls.Add(this.cbShowRoomsWithFlags);
            this.tpRoomFlags.Location = new System.Drawing.Point(4, 22);
            this.tpRoomFlags.Name = "tpRoomFlags";
            this.tpRoomFlags.Size = new System.Drawing.Size(521, 288);
            this.tpRoomFlags.TabIndex = 5;
            this.tpRoomFlags.Text = "Флаги";
            this.tpRoomFlags.UseVisualStyleBackColor = true;
            // 
            // tplRoomFlags
            // 
            this.tplRoomFlags.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tplRoomFlags.Grouped = true;
            this.tplRoomFlags.LeftListCaption = "Флаги комнаты";
            this.tplRoomFlags.Location = new System.Drawing.Point(3, 17);
            this.tplRoomFlags.Name = "tplRoomFlags";
            this.tplRoomFlags.RightListCaption = "Доступные флаги";
            this.tplRoomFlags.Size = new System.Drawing.Size(515, 268);
            this.tplRoomFlags.SplitterDistance = 242;
            this.tplRoomFlags.TabIndex = 98;
            this.tplRoomFlags.Removed += new BZEditor.UcTwoPanelsList.RemoveEvent(this.TplRoomFlagsRemoved);
            this.tplRoomFlags.Added += new BZEditor.UcTwoPanelsList.AddEvent(this.TplRoomFlagsAdded);
            this.tplRoomFlags.ValueChanged += new BZEditor.UcTwoPanelsList.ValueChangeEvent(this.TplRoomFlagsValueChanged);
            this.tplRoomFlags.SelectionChanged += new BZEditor.UcTwoPanelsList.SelectionChangeEvent(this.TplRoomFlagsSelectionChanged);
            // 
            // cbShowRoomsWithFlags
            // 
            this.cbShowRoomsWithFlags.AutoSize = true;
            this.cbShowRoomsWithFlags.Location = new System.Drawing.Point(3, 0);
            this.cbShowRoomsWithFlags.Name = "cbShowRoomsWithFlags";
            this.cbShowRoomsWithFlags.Size = new System.Drawing.Size(297, 17);
            this.cbShowRoomsWithFlags.TabIndex = 97;
            this.cbShowRoomsWithFlags.Text = "Отображать на карте комнаты с выбранным флагом";
            this.cbShowRoomsWithFlags.UseVisualStyleBackColor = true;
            this.cbShowRoomsWithFlags.CheckedChanged += new System.EventHandler(this.CbShowRoomsWithFlagsCheckedChanged);
            // 
            // tpRoomObjs
            // 
            this.tpRoomObjs.Controls.Add(this.splitContainerRoomObjects);
            this.tpRoomObjs.Location = new System.Drawing.Point(4, 22);
            this.tpRoomObjs.Name = "tpRoomObjs";
            this.tpRoomObjs.Size = new System.Drawing.Size(521, 288);
            this.tpRoomObjs.TabIndex = 1;
            this.tpRoomObjs.Text = "Объекты";
            this.tpRoomObjs.UseVisualStyleBackColor = true;
            // 
            // splitContainerRoomObjects
            // 
            this.splitContainerRoomObjects.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerRoomObjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerRoomObjects.Location = new System.Drawing.Point(0, 0);
            this.splitContainerRoomObjects.Name = "splitContainerRoomObjects";
            this.splitContainerRoomObjects.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerRoomObjects.Panel1
            // 
            this.splitContainerRoomObjects.Panel1.Controls.Add(this.elvObjectsInRoom);
            this.splitContainerRoomObjects.Panel1.Controls.Add(this.btnRoomAddObj);
            this.splitContainerRoomObjects.Panel1.Controls.Add(this.btnRoomRemoveObj);
            // 
            // splitContainerRoomObjects.Panel2
            // 
            this.splitContainerRoomObjects.Panel2.Controls.Add(this.gbObjInObj);
            this.splitContainerRoomObjects.Size = new System.Drawing.Size(521, 288);
            this.splitContainerRoomObjects.SplitterDistance = 133;
            this.splitContainerRoomObjects.TabIndex = 20;
            // 
            // elvObjectsInRoom
            // 
            this.elvObjectsInRoom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.elvObjectsInRoom.ContextMenuStrip = this.cmsGridMenu;
            this.elvObjectsInRoom.FullRowSelect = true;
            this.elvObjectsInRoom.GridLines = true;
            this.elvObjectsInRoom.LabelWrap = false;
            this.elvObjectsInRoom.Location = new System.Drawing.Point(3, 3);
            this.elvObjectsInRoom.MultiSelect = false;
            this.elvObjectsInRoom.Name = "elvObjectsInRoom";
            this.elvObjectsInRoom.OwnerDraw = true;
            this.elvObjectsInRoom.Size = new System.Drawing.Size(483, 125);
            this.elvObjectsInRoom.TabIndex = 70;
            this.elvObjectsInRoom.UseCompatibleStateImageBehavior = false;
            this.elvObjectsInRoom.View = System.Windows.Forms.View.Details;
            this.elvObjectsInRoom.ItemValueChanged += new ExtControls.ExtListView.ItemValueChangeEvent(this.ElvObjectsInRoomItemValueChanged);
            this.elvObjectsInRoom.SelectedIndexChanged += new System.EventHandler(this.ElvObjectsInRoomSelectedIndexChanged);
            this.elvObjectsInRoom.SizeChanged += new System.EventHandler(this.LastColumnAutosize);
            this.elvObjectsInRoom.MouseUp += new System.Windows.Forms.MouseEventHandler(this.NavigatedControl_MouseUp);
            // 
            // gbObjInObj
            // 
            this.gbObjInObj.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbObjInObj.Controls.Add(this.elvRoomObjInObj);
            this.gbObjInObj.Controls.Add(this.btnRoomAddObjInObj);
            this.gbObjInObj.Controls.Add(this.btnRoomRemoveObjFromObj);
            this.gbObjInObj.Enabled = false;
            this.gbObjInObj.Location = new System.Drawing.Point(3, -1);
            this.gbObjInObj.Name = "gbObjInObj";
            this.gbObjInObj.Size = new System.Drawing.Size(514, 147);
            this.gbObjInObj.TabIndex = 97;
            this.gbObjInObj.TabStop = false;
            this.gbObjInObj.Text = "Объекты загружаемые в объект";
            // 
            // tpRoomMobs
            // 
            this.tpRoomMobs.Controls.Add(this.splitContainerRoomMobs);
            this.tpRoomMobs.Location = new System.Drawing.Point(4, 22);
            this.tpRoomMobs.Name = "tpRoomMobs";
            this.tpRoomMobs.Size = new System.Drawing.Size(521, 288);
            this.tpRoomMobs.TabIndex = 2;
            this.tpRoomMobs.Text = "Мобы";
            this.tpRoomMobs.UseVisualStyleBackColor = true;
            // 
            // splitContainerRoomMobs
            // 
            this.splitContainerRoomMobs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerRoomMobs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerRoomMobs.Location = new System.Drawing.Point(0, 0);
            this.splitContainerRoomMobs.Name = "splitContainerRoomMobs";
            this.splitContainerRoomMobs.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerRoomMobs.Panel1
            // 
            this.splitContainerRoomMobs.Panel1.Controls.Add(this.btnRoomAddMob);
            this.splitContainerRoomMobs.Panel1.Controls.Add(this.btnRoomRemoveMob);
            this.splitContainerRoomMobs.Panel1.Controls.Add(this.lvMobsInRoom);
            // 
            // splitContainerRoomMobs.Panel2
            // 
            this.splitContainerRoomMobs.Panel2.Controls.Add(this.label84);
            this.splitContainerRoomMobs.Panel2.Controls.Add(this.tabControl1);
            this.splitContainerRoomMobs.Panel2.Controls.Add(this.cboxMobFollowBy);
            this.splitContainerRoomMobs.Panel2.Controls.Add(this.nudMaxInRoom);
            this.splitContainerRoomMobs.Panel2.Controls.Add(this.label85);
            this.splitContainerRoomMobs.Panel2.Enabled = false;
            this.splitContainerRoomMobs.Size = new System.Drawing.Size(521, 288);
            this.splitContainerRoomMobs.SplitterDistance = 90;
            this.splitContainerRoomMobs.TabIndex = 21;
            // 
            // lvMobsInRoom
            // 
            this.lvMobsInRoom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvMobsInRoom.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader20,
            this.columnHeader21});
            this.lvMobsInRoom.ContextMenuStrip = this.cmsGridMenu;
            this.lvMobsInRoom.FullRowSelect = true;
            this.lvMobsInRoom.GridLines = true;
            this.lvMobsInRoom.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvMobsInRoom.HideSelection = false;
            this.lvMobsInRoom.Location = new System.Drawing.Point(3, 3);
            this.lvMobsInRoom.MultiSelect = false;
            this.lvMobsInRoom.Name = "lvMobsInRoom";
            this.lvMobsInRoom.ShowItemToolTips = true;
            this.lvMobsInRoom.Size = new System.Drawing.Size(483, 82);
            this.lvMobsInRoom.TabIndex = 16;
            this.lvMobsInRoom.UseCompatibleStateImageBehavior = false;
            this.lvMobsInRoom.View = System.Windows.Forms.View.Details;
            this.lvMobsInRoom.SelectedIndexChanged += new System.EventHandler(this.LvMobsInRoomSelectedIndexChanged);
            this.lvMobsInRoom.SizeChanged += new System.EventHandler(this.LastColumnAutosize);
            this.lvMobsInRoom.MouseUp += new System.Windows.Forms.MouseEventHandler(this.NavigatedControl_MouseUp);
            // 
            // columnHeader20
            // 
            this.columnHeader20.Width = 50;
            // 
            // columnHeader21
            // 
            this.columnHeader21.Width = 408;
            // 
            // label84
            // 
            this.label84.Location = new System.Drawing.Point(1, 0);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(73, 16);
            this.label84.TabIndex = 82;
            this.label84.Text = "Следует за";
            this.label84.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tpMobObjectsLoaded);
            this.tabControl1.Controls.Add(this.tpMobObjectsLoadedAfterDeath);
            this.tabControl1.Location = new System.Drawing.Point(0, 43);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(519, 149);
            this.tabControl1.TabIndex = 99;
            // 
            // tpMobObjectsLoaded
            // 
            this.tpMobObjectsLoaded.Controls.Add(this.elvRoomMobObjects);
            this.tpMobObjectsLoaded.Controls.Add(this.btnRoomAddObjToMob);
            this.tpMobObjectsLoaded.Controls.Add(this.btnRoomRomoveObjFromMob);
            this.tpMobObjectsLoaded.Location = new System.Drawing.Point(4, 22);
            this.tpMobObjectsLoaded.Name = "tpMobObjectsLoaded";
            this.tpMobObjectsLoaded.Padding = new System.Windows.Forms.Padding(3);
            this.tpMobObjectsLoaded.Size = new System.Drawing.Size(511, 123);
            this.tpMobObjectsLoaded.TabIndex = 0;
            this.tpMobObjectsLoaded.Text = "Объекты моба";
            this.tpMobObjectsLoaded.UseVisualStyleBackColor = true;
            // 
            // tpMobObjectsLoadedAfterDeath
            // 
            this.tpMobObjectsLoadedAfterDeath.Controls.Add(this.btnRoomRomoveObjFromMobAfterDeath);
            this.tpMobObjectsLoadedAfterDeath.Controls.Add(this.elvRoomMobObjectsLoadingAfterDeath);
            this.tpMobObjectsLoadedAfterDeath.Controls.Add(this.button3);
            this.tpMobObjectsLoadedAfterDeath.Location = new System.Drawing.Point(4, 22);
            this.tpMobObjectsLoadedAfterDeath.Name = "tpMobObjectsLoadedAfterDeath";
            this.tpMobObjectsLoadedAfterDeath.Padding = new System.Windows.Forms.Padding(3);
            this.tpMobObjectsLoadedAfterDeath.Size = new System.Drawing.Size(511, 123);
            this.tpMobObjectsLoadedAfterDeath.TabIndex = 1;
            this.tpMobObjectsLoadedAfterDeath.Text = "Объекты моба, загружаемые посмертно";
            this.tpMobObjectsLoadedAfterDeath.UseVisualStyleBackColor = true;
            // 
            // cboxMobFollowBy
            // 
            this.cboxMobFollowBy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboxMobFollowBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxMobFollowBy.ItemHeight = 13;
            this.cboxMobFollowBy.Location = new System.Drawing.Point(4, 16);
            this.cboxMobFollowBy.Name = "cboxMobFollowBy";
            this.cboxMobFollowBy.Size = new System.Drawing.Size(444, 21);
            this.cboxMobFollowBy.TabIndex = 83;
            this.cboxMobFollowBy.SelectedValueChanged += new System.EventHandler(this.CboxMobFollowBySelectedValueChanged);
            // 
            // tpRoomTrgs
            // 
            this.tpRoomTrgs.Controls.Add(this.btnAddRoomTrigger);
            this.tpRoomTrgs.Controls.Add(this.btnRemoveRoomTrigger);
            this.tpRoomTrgs.Controls.Add(this.lvRoomTriggers);
            this.tpRoomTrgs.Location = new System.Drawing.Point(4, 22);
            this.tpRoomTrgs.Name = "tpRoomTrgs";
            this.tpRoomTrgs.Padding = new System.Windows.Forms.Padding(3);
            this.tpRoomTrgs.Size = new System.Drawing.Size(521, 288);
            this.tpRoomTrgs.TabIndex = 0;
            this.tpRoomTrgs.Text = "Триггеры";
            this.tpRoomTrgs.UseVisualStyleBackColor = true;
            // 
            // btnAddRoomTrigger
            // 
            this.btnAddRoomTrigger.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddRoomTrigger.Image = ((System.Drawing.Image)(resources.GetObject("btnAddRoomTrigger.Image")));
            this.btnAddRoomTrigger.Location = new System.Drawing.Point(489, 3);
            this.btnAddRoomTrigger.Name = "btnAddRoomTrigger";
            this.btnAddRoomTrigger.Size = new System.Drawing.Size(28, 28);
            this.btnAddRoomTrigger.TabIndex = 43;
            this.btnAddRoomTrigger.Click += new System.EventHandler(this.BtnAddRoomTriggerClick);
            // 
            // btnRemoveRoomTrigger
            // 
            this.btnRemoveRoomTrigger.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveRoomTrigger.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveRoomTrigger.Image")));
            this.btnRemoveRoomTrigger.Location = new System.Drawing.Point(489, 33);
            this.btnRemoveRoomTrigger.Name = "btnRemoveRoomTrigger";
            this.btnRemoveRoomTrigger.Size = new System.Drawing.Size(28, 28);
            this.btnRemoveRoomTrigger.TabIndex = 42;
            this.btnRemoveRoomTrigger.Click += new System.EventHandler(this.BtnRemoveRoomTriggerClick);
            // 
            // lvRoomTriggers
            // 
            this.lvRoomTriggers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvRoomTriggers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader7});
            this.lvRoomTriggers.ContextMenuStrip = this.cmsGridMenu;
            this.lvRoomTriggers.FullRowSelect = true;
            this.lvRoomTriggers.GridLines = true;
            this.lvRoomTriggers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvRoomTriggers.HideSelection = false;
            this.lvRoomTriggers.Location = new System.Drawing.Point(3, 4);
            this.lvRoomTriggers.Name = "lvRoomTriggers";
            this.lvRoomTriggers.Size = new System.Drawing.Size(484, 280);
            this.lvRoomTriggers.TabIndex = 41;
            this.lvRoomTriggers.UseCompatibleStateImageBehavior = false;
            this.lvRoomTriggers.View = System.Windows.Forms.View.Details;
            this.lvRoomTriggers.SizeChanged += new System.EventHandler(this.LastColumnAutosize);
            this.lvRoomTriggers.MouseEnter += new System.EventHandler(this.FocusToControl);
            this.lvRoomTriggers.MouseUp += new System.Windows.Forms.MouseEventHandler(this.NavigatedControl_MouseUp);
            // 
            // columnHeader6
            // 
            this.columnHeader6.Width = 50;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Width = 350;
            // 
            // tpRoomDelObjs
            // 
            this.tpRoomDelObjs.Controls.Add(this.btnAddRoomObjectToRemove);
            this.tpRoomDelObjs.Controls.Add(this.btnRemoveRoomObjectToRemove);
            this.tpRoomDelObjs.Controls.Add(this.lvObjectsToRemove);
            this.tpRoomDelObjs.Location = new System.Drawing.Point(4, 22);
            this.tpRoomDelObjs.Name = "tpRoomDelObjs";
            this.tpRoomDelObjs.Size = new System.Drawing.Size(521, 288);
            this.tpRoomDelObjs.TabIndex = 3;
            this.tpRoomDelObjs.Text = "Удал.объекты";
            this.tpRoomDelObjs.UseVisualStyleBackColor = true;
            // 
            // btnAddRoomObjectToRemove
            // 
            this.btnAddRoomObjectToRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddRoomObjectToRemove.Image = ((System.Drawing.Image)(resources.GetObject("btnAddRoomObjectToRemove.Image")));
            this.btnAddRoomObjectToRemove.Location = new System.Drawing.Point(489, 3);
            this.btnAddRoomObjectToRemove.Name = "btnAddRoomObjectToRemove";
            this.btnAddRoomObjectToRemove.Size = new System.Drawing.Size(28, 28);
            this.btnAddRoomObjectToRemove.TabIndex = 47;
            this.btnAddRoomObjectToRemove.Click += new System.EventHandler(this.BtnAddRoomObjectToRemoveClick);
            // 
            // btnRemoveRoomObjectToRemove
            // 
            this.btnRemoveRoomObjectToRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveRoomObjectToRemove.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveRoomObjectToRemove.Image")));
            this.btnRemoveRoomObjectToRemove.Location = new System.Drawing.Point(489, 33);
            this.btnRemoveRoomObjectToRemove.Name = "btnRemoveRoomObjectToRemove";
            this.btnRemoveRoomObjectToRemove.Size = new System.Drawing.Size(28, 28);
            this.btnRemoveRoomObjectToRemove.TabIndex = 46;
            this.btnRemoveRoomObjectToRemove.Click += new System.EventHandler(this.BtnRemoveRoomObjectToRemoveClick);
            // 
            // lvObjectsToRemove
            // 
            this.lvObjectsToRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvObjectsToRemove.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader14,
            this.columnHeader15});
            this.lvObjectsToRemove.ContextMenuStrip = this.cmsGridMenu;
            this.lvObjectsToRemove.FullRowSelect = true;
            this.lvObjectsToRemove.GridLines = true;
            this.lvObjectsToRemove.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvObjectsToRemove.HideSelection = false;
            this.lvObjectsToRemove.Location = new System.Drawing.Point(3, 4);
            this.lvObjectsToRemove.Name = "lvObjectsToRemove";
            this.lvObjectsToRemove.ShowItemToolTips = true;
            this.lvObjectsToRemove.Size = new System.Drawing.Size(484, 280);
            this.lvObjectsToRemove.TabIndex = 16;
            this.lvObjectsToRemove.UseCompatibleStateImageBehavior = false;
            this.lvObjectsToRemove.View = System.Windows.Forms.View.Details;
            this.lvObjectsToRemove.SizeChanged += new System.EventHandler(this.LastColumnAutosize);
            this.lvObjectsToRemove.MouseEnter += new System.EventHandler(this.FocusToControl);
            this.lvObjectsToRemove.MouseUp += new System.Windows.Forms.MouseEventHandler(this.NavigatedControl_MouseUp);
            // 
            // columnHeader14
            // 
            this.columnHeader14.Width = 50;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Width = 410;
            // 
            // tpRoomAddDescrs
            // 
            this.tpRoomAddDescrs.AutoScroll = true;
            this.tpRoomAddDescrs.Controls.Add(this.rtbRoomAddDescText);
            this.tpRoomAddDescrs.Controls.Add(this.cbRoomAddDescWordwrap);
            this.tpRoomAddDescrs.Controls.Add(label87);
            this.tpRoomAddDescrs.Controls.Add(label88);
            this.tpRoomAddDescrs.Controls.Add(this.btnAddRoomAddDesc);
            this.tpRoomAddDescrs.Controls.Add(this.btnRemoveRoomAddDesc);
            this.tpRoomAddDescrs.Controls.Add(this.lvRoomAddDescriptions);
            this.tpRoomAddDescrs.Controls.Add(this.tbRoomAddDescAliases);
            this.tpRoomAddDescrs.Location = new System.Drawing.Point(4, 22);
            this.tpRoomAddDescrs.Name = "tpRoomAddDescrs";
            this.tpRoomAddDescrs.Size = new System.Drawing.Size(521, 288);
            this.tpRoomAddDescrs.TabIndex = 6;
            this.tpRoomAddDescrs.Text = "Доп.описания";
            this.tpRoomAddDescrs.UseVisualStyleBackColor = true;
            // 
            // rtbRoomAddDescText
            // 
            this.rtbRoomAddDescText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbRoomAddDescText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbRoomAddDescText.Location = new System.Drawing.Point(3, 53);
            this.rtbRoomAddDescText.Name = "rtbRoomAddDescText";
            this.rtbRoomAddDescText.Size = new System.Drawing.Size(485, 58);
            this.rtbRoomAddDescText.TabIndex = 93;
            this.rtbRoomAddDescText.Text = "";
            this.rtbRoomAddDescText.WordWrap = false;
            this.rtbRoomAddDescText.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CExtRichTextBoxKeyUp);
            this.rtbRoomAddDescText.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CExtRichTextBoxMouseUp);
            // 
            // cbRoomAddDescWordwrap
            // 
            this.cbRoomAddDescWordwrap.AutoSize = true;
            this.cbRoomAddDescWordwrap.Location = new System.Drawing.Point(65, 36);
            this.cbRoomAddDescWordwrap.Name = "cbRoomAddDescWordwrap";
            this.cbRoomAddDescWordwrap.Size = new System.Drawing.Size(311, 17);
            this.cbRoomAddDescWordwrap.TabIndex = 92;
            this.cbRoomAddDescWordwrap.Text = "Переносить текст описания по словам на новую строку";
            this.cbRoomAddDescWordwrap.UseVisualStyleBackColor = true;
            this.cbRoomAddDescWordwrap.CheckedChanged += new System.EventHandler(this.CbRoomAddDescWordwrapCheckedChanged);
            // 
            // btnAddRoomAddDesc
            // 
            this.btnAddRoomAddDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddRoomAddDesc.Image = ((System.Drawing.Image)(resources.GetObject("btnAddRoomAddDesc.Image")));
            this.btnAddRoomAddDesc.Location = new System.Drawing.Point(490, 53);
            this.btnAddRoomAddDesc.Name = "btnAddRoomAddDesc";
            this.btnAddRoomAddDesc.Size = new System.Drawing.Size(28, 28);
            this.btnAddRoomAddDesc.TabIndex = 88;
            this.btnAddRoomAddDesc.Click += new System.EventHandler(this.BtnAddRoomAddDescClick);
            // 
            // btnRemoveRoomAddDesc
            // 
            this.btnRemoveRoomAddDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveRoomAddDesc.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveRoomAddDesc.Image")));
            this.btnRemoveRoomAddDesc.Location = new System.Drawing.Point(490, 83);
            this.btnRemoveRoomAddDesc.Name = "btnRemoveRoomAddDesc";
            this.btnRemoveRoomAddDesc.Size = new System.Drawing.Size(28, 28);
            this.btnRemoveRoomAddDesc.TabIndex = 87;
            this.btnRemoveRoomAddDesc.Click += new System.EventHandler(this.BtnRemoveRoomAddDescClick);
            // 
            // lvRoomAddDescriptions
            // 
            this.lvRoomAddDescriptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvRoomAddDescriptions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader8,
            this.columnHeader12});
            this.lvRoomAddDescriptions.FullRowSelect = true;
            this.lvRoomAddDescriptions.GridLines = true;
            this.lvRoomAddDescriptions.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvRoomAddDescriptions.HideSelection = false;
            this.lvRoomAddDescriptions.Location = new System.Drawing.Point(3, 114);
            this.lvRoomAddDescriptions.MultiSelect = false;
            this.lvRoomAddDescriptions.Name = "lvRoomAddDescriptions";
            this.lvRoomAddDescriptions.Size = new System.Drawing.Size(515, 172);
            this.lvRoomAddDescriptions.TabIndex = 86;
            this.lvRoomAddDescriptions.UseCompatibleStateImageBehavior = false;
            this.lvRoomAddDescriptions.View = System.Windows.Forms.View.Details;
            this.lvRoomAddDescriptions.SizeChanged += new System.EventHandler(this.LastColumnAutosize);
            this.lvRoomAddDescriptions.DoubleClick += new System.EventHandler(this.LvRoomAddDescriptionsDoubleClick);
            this.lvRoomAddDescriptions.MouseEnter += new System.EventHandler(this.FocusToControl);
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Альясы";
            this.columnHeader8.Width = 150;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Описание";
            this.columnHeader12.Width = 341;
            // 
            // tbRoomAddDescAliases
            // 
            this.tbRoomAddDescAliases.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRoomAddDescAliases.Location = new System.Drawing.Point(3, 15);
            this.tbRoomAddDescAliases.Name = "tbRoomAddDescAliases";
            this.tbRoomAddDescAliases.Size = new System.Drawing.Size(515, 20);
            this.tbRoomAddDescAliases.TabIndex = 91;
            // 
            // tpRoomIngrediens
            // 
            this.tpRoomIngrediens.Controls.Add(this.elvRoomIngredients);
            this.tpRoomIngrediens.Controls.Add(this.btnAddRoomIngredient);
            this.tpRoomIngrediens.Controls.Add(this.btnRemoveRoomIngredient);
            this.tpRoomIngrediens.Location = new System.Drawing.Point(4, 22);
            this.tpRoomIngrediens.Name = "tpRoomIngrediens";
            this.tpRoomIngrediens.Size = new System.Drawing.Size(521, 288);
            this.tpRoomIngrediens.TabIndex = 8;
            this.tpRoomIngrediens.Text = "Ингредиенты";
            this.tpRoomIngrediens.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.rtbRoomDesc);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel2.Location = new System.Drawing.Point(0, 21);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(721, 131);
            this.panel2.TabIndex = 89;
            // 
            // rtbRoomDesc
            // 
            this.rtbRoomDesc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbRoomDesc.ContextMenuStrip = this.cmsRoomsDescription;
            this.rtbRoomDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbRoomDesc.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.errorProvider.SetIconAlignment(this.rtbRoomDesc, System.Windows.Forms.ErrorIconAlignment.TopRight);
            this.errorProvider.SetIconPadding(this.rtbRoomDesc, -16);
            this.rtbRoomDesc.Location = new System.Drawing.Point(0, 0);
            this.rtbRoomDesc.Name = "rtbRoomDesc";
            this.rtbRoomDesc.Size = new System.Drawing.Size(719, 129);
            this.rtbRoomDesc.TabIndex = 0;
            this.rtbRoomDesc.Text = "";
            this.rtbRoomDesc.WordWrap = false;
            this.rtbRoomDesc.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CExtRichTextBoxKeyUp);
            this.rtbRoomDesc.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CExtRichTextBoxMouseUp);
            this.rtbRoomDesc.Validated += new System.EventHandler(this.RoomDescValidated);
            // 
            // tabControlRoomDescriptions
            // 
            this.tabControlRoomDescriptions.Controls.Add(this.tpRoomDesc);
            this.tabControlRoomDescriptions.Controls.Add(this.tpRoomDescDay);
            this.tabControlRoomDescriptions.Controls.Add(this.tpRoomDescNight);
            this.tabControlRoomDescriptions.Controls.Add(this.tpRoomDescWinterDay);
            this.tabControlRoomDescriptions.Controls.Add(this.tpRoomDescWinterNight);
            this.tabControlRoomDescriptions.Controls.Add(this.tpRoomDescSpringDay);
            this.tabControlRoomDescriptions.Controls.Add(this.tpRoomDescSpringNight);
            this.tabControlRoomDescriptions.Controls.Add(this.tpRoomDescSummerDay);
            this.tabControlRoomDescriptions.Controls.Add(this.tpRoomDescSummerNight);
            this.tabControlRoomDescriptions.Controls.Add(this.tpRoomDescAutumnDay);
            this.tabControlRoomDescriptions.Controls.Add(this.tpRoomDescAutumnNight);
            this.tabControlRoomDescriptions.Dock = System.Windows.Forms.DockStyle.Top;
            this.errorProvider.SetIconAlignment(this.tabControlRoomDescriptions, System.Windows.Forms.ErrorIconAlignment.TopRight);
            this.tabControlRoomDescriptions.ImageList = this.iListIcons16;
            this.tabControlRoomDescriptions.Location = new System.Drawing.Point(0, 0);
            this.tabControlRoomDescriptions.Name = "tabControlRoomDescriptions";
            this.tabControlRoomDescriptions.SelectedIndex = 0;
            this.tabControlRoomDescriptions.Size = new System.Drawing.Size(721, 21);
            this.tabControlRoomDescriptions.TabIndex = 0;
            this.tabControlRoomDescriptions.SelectedIndexChanged += new System.EventHandler(this.TabControlRoomDescriptionsSelectedIndexChanged);
            // 
            // tpRoomDesc
            // 
            this.tpRoomDesc.BackColor = System.Drawing.Color.Transparent;
            this.tpRoomDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tpRoomDesc.Location = new System.Drawing.Point(4, 23);
            this.tpRoomDesc.Name = "tpRoomDesc";
            this.tpRoomDesc.Size = new System.Drawing.Size(713, 0);
            this.tpRoomDesc.TabIndex = 0;
            this.tpRoomDesc.Text = "Общее";
            this.tpRoomDesc.UseVisualStyleBackColor = true;
            // 
            // tpRoomDescDay
            // 
            this.tpRoomDescDay.BackColor = System.Drawing.Color.Transparent;
            this.tpRoomDescDay.Location = new System.Drawing.Point(4, 23);
            this.tpRoomDescDay.Name = "tpRoomDescDay";
            this.tpRoomDescDay.Size = new System.Drawing.Size(713, 0);
            this.tpRoomDescDay.TabIndex = 1;
            this.tpRoomDescDay.Text = "День";
            this.tpRoomDescDay.UseVisualStyleBackColor = true;
            // 
            // tpRoomDescNight
            // 
            this.tpRoomDescNight.BackColor = System.Drawing.Color.Transparent;
            this.tpRoomDescNight.Location = new System.Drawing.Point(4, 23);
            this.tpRoomDescNight.Name = "tpRoomDescNight";
            this.tpRoomDescNight.Size = new System.Drawing.Size(713, 0);
            this.tpRoomDescNight.TabIndex = 2;
            this.tpRoomDescNight.Text = "Ночь";
            this.tpRoomDescNight.UseVisualStyleBackColor = true;
            // 
            // tpRoomDescWinterDay
            // 
            this.tpRoomDescWinterDay.BackColor = System.Drawing.Color.Transparent;
            this.tpRoomDescWinterDay.Location = new System.Drawing.Point(4, 23);
            this.tpRoomDescWinterDay.Name = "tpRoomDescWinterDay";
            this.tpRoomDescWinterDay.Size = new System.Drawing.Size(713, 0);
            this.tpRoomDescWinterDay.TabIndex = 3;
            this.tpRoomDescWinterDay.Text = "Зима[Д]";
            this.tpRoomDescWinterDay.UseVisualStyleBackColor = true;
            // 
            // tpRoomDescWinterNight
            // 
            this.tpRoomDescWinterNight.BackColor = System.Drawing.Color.Transparent;
            this.tpRoomDescWinterNight.Location = new System.Drawing.Point(4, 23);
            this.tpRoomDescWinterNight.Name = "tpRoomDescWinterNight";
            this.tpRoomDescWinterNight.Size = new System.Drawing.Size(713, 0);
            this.tpRoomDescWinterNight.TabIndex = 4;
            this.tpRoomDescWinterNight.Text = "Зима[Н]";
            this.tpRoomDescWinterNight.UseVisualStyleBackColor = true;
            // 
            // tpRoomDescSpringDay
            // 
            this.tpRoomDescSpringDay.BackColor = System.Drawing.Color.Transparent;
            this.tpRoomDescSpringDay.Location = new System.Drawing.Point(4, 23);
            this.tpRoomDescSpringDay.Name = "tpRoomDescSpringDay";
            this.tpRoomDescSpringDay.Size = new System.Drawing.Size(713, 0);
            this.tpRoomDescSpringDay.TabIndex = 5;
            this.tpRoomDescSpringDay.Text = "Весна[Д]";
            this.tpRoomDescSpringDay.UseVisualStyleBackColor = true;
            // 
            // tpRoomDescSpringNight
            // 
            this.tpRoomDescSpringNight.BackColor = System.Drawing.Color.Transparent;
            this.tpRoomDescSpringNight.Location = new System.Drawing.Point(4, 23);
            this.tpRoomDescSpringNight.Name = "tpRoomDescSpringNight";
            this.tpRoomDescSpringNight.Size = new System.Drawing.Size(713, 0);
            this.tpRoomDescSpringNight.TabIndex = 6;
            this.tpRoomDescSpringNight.Text = "Весна[Н]";
            this.tpRoomDescSpringNight.UseVisualStyleBackColor = true;
            // 
            // tpRoomDescSummerDay
            // 
            this.tpRoomDescSummerDay.BackColor = System.Drawing.Color.Transparent;
            this.tpRoomDescSummerDay.Location = new System.Drawing.Point(4, 23);
            this.tpRoomDescSummerDay.Name = "tpRoomDescSummerDay";
            this.tpRoomDescSummerDay.Size = new System.Drawing.Size(713, 0);
            this.tpRoomDescSummerDay.TabIndex = 7;
            this.tpRoomDescSummerDay.Text = "Лето[Д]";
            this.tpRoomDescSummerDay.UseVisualStyleBackColor = true;
            // 
            // tpRoomDescSummerNight
            // 
            this.tpRoomDescSummerNight.BackColor = System.Drawing.Color.Transparent;
            this.tpRoomDescSummerNight.Location = new System.Drawing.Point(4, 23);
            this.tpRoomDescSummerNight.Name = "tpRoomDescSummerNight";
            this.tpRoomDescSummerNight.Size = new System.Drawing.Size(713, 0);
            this.tpRoomDescSummerNight.TabIndex = 8;
            this.tpRoomDescSummerNight.Text = "Лето[Н]";
            this.tpRoomDescSummerNight.UseVisualStyleBackColor = true;
            // 
            // tpRoomDescAutumnDay
            // 
            this.tpRoomDescAutumnDay.BackColor = System.Drawing.Color.Transparent;
            this.tpRoomDescAutumnDay.Location = new System.Drawing.Point(4, 23);
            this.tpRoomDescAutumnDay.Name = "tpRoomDescAutumnDay";
            this.tpRoomDescAutumnDay.Size = new System.Drawing.Size(713, 0);
            this.tpRoomDescAutumnDay.TabIndex = 9;
            this.tpRoomDescAutumnDay.Text = "Осень[Д]";
            this.tpRoomDescAutumnDay.UseVisualStyleBackColor = true;
            // 
            // tpRoomDescAutumnNight
            // 
            this.tpRoomDescAutumnNight.BackColor = System.Drawing.Color.Transparent;
            this.tpRoomDescAutumnNight.Location = new System.Drawing.Point(4, 23);
            this.tpRoomDescAutumnNight.Name = "tpRoomDescAutumnNight";
            this.tpRoomDescAutumnNight.Size = new System.Drawing.Size(713, 0);
            this.tpRoomDescAutumnNight.TabIndex = 10;
            this.tpRoomDescAutumnNight.Text = "Осень[Н]";
            this.tpRoomDescAutumnNight.UseVisualStyleBackColor = true;
            // 
            // pnlFormating
            // 
            this.pnlFormating.BackColor = System.Drawing.SystemColors.Control;
            this.pnlFormating.Controls.Add(this.cbDescReplace);
            this.pnlFormating.Controls.Add(this.btnRoomSpecFormatCommonDesc);
            this.pnlFormating.Controls.Add(this.btnRoomSpellCheckCommonDesc);
            this.pnlFormating.Controls.Add(this.cbInsertSpaces);
            this.pnlFormating.Controls.Add(this.btnRoomFormatCommonDesc);
            this.pnlFormating.Controls.Add(this.cbAllowHyp);
            this.pnlFormating.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlFormating.Location = new System.Drawing.Point(721, 0);
            this.pnlFormating.Name = "pnlFormating";
            this.pnlFormating.Size = new System.Drawing.Size(98, 152);
            this.pnlFormating.TabIndex = 88;
            // 
            // cbDescReplace
            // 
            this.cbDescReplace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbDescReplace.Location = new System.Drawing.Point(3, 37);
            this.cbDescReplace.Name = "cbDescReplace";
            this.cbDescReplace.Size = new System.Drawing.Size(83, 19);
            this.cbDescReplace.TabIndex = 99;
            this.cbDescReplace.Text = "Замещать";
            this.cbDescReplace.UseVisualStyleBackColor = true;
            this.cbDescReplace.CheckedChanged += new System.EventHandler(this.CbDescReplaceCheckedChanged);
            // 
            // cbAllowHyp
            // 
            this.cbAllowHyp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbAllowHyp.Location = new System.Drawing.Point(2, 62);
            this.cbAllowHyp.Name = "cbAllowHyp";
            this.cbAllowHyp.Size = new System.Drawing.Size(91, 18);
            this.cbAllowHyp.TabIndex = 104;
            this.cbAllowHyp.Text = "По слогам";
            this.cbAllowHyp.UseVisualStyleBackColor = true;
            this.cbAllowHyp.Visible = false;
            // 
            // tpObjects
            // 
            this.tpObjects.BackColor = System.Drawing.Color.Transparent;
            this.tpObjects.Controls.Add(this.splitContainerObj);
            this.tpObjects.Location = new System.Drawing.Point(4, 23);
            this.tpObjects.Name = "tpObjects";
            this.tpObjects.Padding = new System.Windows.Forms.Padding(3);
            this.tpObjects.Size = new System.Drawing.Size(819, 472);
            this.tpObjects.TabIndex = 1;
            this.tpObjects.Text = "Объекты";
            this.tpObjects.UseVisualStyleBackColor = true;
            // 
            // splitContainerObj
            // 
            this.splitContainerObj.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerObj.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerObj.Location = new System.Drawing.Point(3, 3);
            this.splitContainerObj.Name = "splitContainerObj";
            // 
            // splitContainerObj.Panel1
            // 
            this.splitContainerObj.Panel1.AutoScroll = true;
            this.splitContainerObj.Panel1.Controls.Add(this.btnObjSetAutoCases);
            this.splitContainerObj.Panel1.Controls.Add(this.tboxObjAliases);
            this.splitContainerObj.Panel1.Controls.Add(this.tboxObjTvor);
            this.splitContainerObj.Panel1.Controls.Add(this.tboxObjVin);
            this.splitContainerObj.Panel1.Controls.Add(this.tboxObjDat);
            this.splitContainerObj.Panel1.Controls.Add(this.tboxObjPredl);
            this.splitContainerObj.Panel1.Controls.Add(this.tboxObjImen);
            this.splitContainerObj.Panel1.Controls.Add(this.tboxObjRod);
            this.splitContainerObj.Panel1.Controls.Add(this.cboxObjectGender);
            this.splitContainerObj.Panel1.Controls.Add(label45);
            this.splitContainerObj.Panel1.Controls.Add(label42);
            this.splitContainerObj.Panel1.Controls.Add(this.tboxObjDesc);
            this.splitContainerObj.Panel1.Controls.Add(label41);
            this.splitContainerObj.Panel1.Controls.Add(this.tboxObjActionDesc);
            this.splitContainerObj.Panel1.Controls.Add(this.label35);
            this.splitContainerObj.Panel1.Controls.Add(label33);
            this.splitContainerObj.Panel1.Controls.Add(label39);
            this.splitContainerObj.Panel1.Controls.Add(label38);
            this.splitContainerObj.Panel1.Controls.Add(label28);
            this.splitContainerObj.Panel1.Controls.Add(label37);
            this.splitContainerObj.Panel1.Controls.Add(label36);
            this.splitContainerObj.Panel1.Margin = new System.Windows.Forms.Padding(3);
            // 
            // splitContainerObj.Panel2
            // 
            this.splitContainerObj.Panel2.Controls.Add(this.tcObject);
            this.splitContainerObj.Size = new System.Drawing.Size(813, 466);
            this.splitContainerObj.SplitterDistance = 359;
            this.splitContainerObj.TabIndex = 1001;
            // 
            // cboxObjectGender
            // 
            this.cboxObjectGender.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboxObjectGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxObjectGender.ItemHeight = 13;
            this.cboxObjectGender.Location = new System.Drawing.Point(57, 3);
            this.cboxObjectGender.Name = "cboxObjectGender";
            this.cboxObjectGender.Size = new System.Drawing.Size(294, 21);
            this.cboxObjectGender.TabIndex = 1;
            this.cboxObjectGender.SelectedIndexChanged += new System.EventHandler(this.ObjValueValidated);
            // 
            // tboxObjDesc
            // 
            this.tboxObjDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tboxObjDesc.Location = new System.Drawing.Point(5, 205);
            this.tboxObjDesc.Name = "tboxObjDesc";
            this.tboxObjDesc.Size = new System.Drawing.Size(346, 20);
            this.tboxObjDesc.TabIndex = 11;
            this.tboxObjDesc.TextChanged += new System.EventHandler(this.ObjValueValidated);
            // 
            // tboxObjActionDesc
            // 
            this.tboxObjActionDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tboxObjActionDesc.Location = new System.Drawing.Point(5, 246);
            this.tboxObjActionDesc.Name = "tboxObjActionDesc";
            this.tboxObjActionDesc.Size = new System.Drawing.Size(346, 20);
            this.tboxObjActionDesc.TabIndex = 12;
            this.tboxObjActionDesc.Visible = false;
            this.tboxObjActionDesc.TextChanged += new System.EventHandler(this.ObjValueValidated);
            // 
            // label35
            // 
            this.label35.Location = new System.Drawing.Point(2, 228);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(150, 17);
            this.label35.TabIndex = 80;
            this.label35.Text = "Опиcание при действии";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label35.Visible = false;
            // 
            // tcObject
            // 
            this.tcObject.Controls.Add(this.tpObjParams);
            this.tcObject.Controls.Add(this.tpObjEffects);
            this.tcObject.Controls.Add(this.tpObjAffects);
            this.tcObject.Controls.Add(this.tpObjWearTo);
            this.tcObject.Controls.Add(this.tpObjCantTouch);
            this.tcObject.Controls.Add(this.tpObjCantUse);
            this.tcObject.Controls.Add(this.tpObjTriggers);
            this.tcObject.Controls.Add(this.tpObjAddDescs);
            this.tcObject.Controls.Add(this.tpObjAddAffects);
            this.tcObject.Controls.Add(this.tpObjSkillBonus);
            this.tcObject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcObject.ItemSize = new System.Drawing.Size(54, 18);
            this.tcObject.Location = new System.Drawing.Point(0, 0);
            this.tcObject.Margin = new System.Windows.Forms.Padding(0);
            this.tcObject.Multiline = true;
            this.tcObject.Name = "tcObject";
            this.tcObject.SelectedIndex = 0;
            this.tcObject.Size = new System.Drawing.Size(448, 464);
            this.tcObject.TabIndex = 10;
            this.tcObject.SelectedIndexChanged += new System.EventHandler(this.TabControlObjectSelectedIndexChanged);
            // 
            // tpObjParams
            // 
            this.tpObjParams.AutoScroll = true;
            this.tpObjParams.Controls.Add(this.cboxObjSkill);
            this.tpObjParams.Controls.Add(label49);
            this.tpObjParams.Controls.Add(this.nudObjMaxInWorld);
            this.tpObjParams.Controls.Add(this.nudObjRentPriceInv);
            this.tpObjParams.Controls.Add(this.cboxObjMatherial);
            this.tpObjParams.Controls.Add(this.cboxObjTimerUOM);
            this.tpObjParams.Controls.Add(this.nudObjTimer);
            this.tpObjParams.Controls.Add(label46);
            this.tpObjParams.Controls.Add(this.nudObjWeight);
            this.tpObjParams.Controls.Add(label47);
            this.tpObjParams.Controls.Add(this.nudObjMinRemorts);
            this.tpObjParams.Controls.Add(label74);
            this.tpObjParams.Controls.Add(this.nudObjPrice);
            this.tpObjParams.Controls.Add(label48);
            this.tpObjParams.Controls.Add(this.nudObjRentPriceEquip);
            this.tpObjParams.Controls.Add(label50);
            this.tpObjParams.Controls.Add(label52);
            this.tpObjParams.Controls.Add(label53);
            this.tpObjParams.Controls.Add(this.cboxObjMaxStructHits);
            this.tpObjParams.Controls.Add(this.nudObjCurStructHits);
            this.tpObjParams.Controls.Add(label55);
            this.tpObjParams.Controls.Add(label56);
            this.tpObjParams.Controls.Add(label54);
            this.tpObjParams.Controls.Add(this.gbObjType);
            this.tpObjParams.Location = new System.Drawing.Point(4, 40);
            this.tpObjParams.Margin = new System.Windows.Forms.Padding(0);
            this.tpObjParams.Name = "tpObjParams";
            this.tpObjParams.Padding = new System.Windows.Forms.Padding(3);
            this.tpObjParams.Size = new System.Drawing.Size(440, 420);
            this.tpObjParams.TabIndex = 9;
            this.tpObjParams.Text = "Параметры";
            this.tpObjParams.UseVisualStyleBackColor = true;
            // 
            // cboxObjSkill
            // 
            this.cboxObjSkill.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxObjSkill.ItemHeight = 13;
            this.cboxObjSkill.Location = new System.Drawing.Point(8, 60);
            this.cboxObjSkill.Name = "cboxObjSkill";
            this.cboxObjSkill.Size = new System.Drawing.Size(252, 21);
            this.cboxObjSkill.TabIndex = 6;
            this.cboxObjSkill.SelectedIndexChanged += new System.EventHandler(this.ObjValueValidated);
            // 
            // nudObjMaxInWorld
            // 
            this.nudObjMaxInWorld.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudObjMaxInWorld.Location = new System.Drawing.Point(352, 20);
            this.nudObjMaxInWorld.Maximum = new decimal(new int[] {
            -1981284353,
            -1966660860,
            0,
            0});
            this.nudObjMaxInWorld.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.nudObjMaxInWorld.Name = "nudObjMaxInWorld";
            this.nudObjMaxInWorld.Size = new System.Drawing.Size(80, 20);
            this.nudObjMaxInWorld.TabIndex = 5;
            this.nudObjMaxInWorld.ValueChanged += new System.EventHandler(this.ObjValueValidated);
            // 
            // nudObjRentPriceInv
            // 
            this.nudObjRentPriceInv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudObjRentPriceInv.Location = new System.Drawing.Point(94, 20);
            this.nudObjRentPriceInv.Maximum = new decimal(new int[] {
            -1981284353,
            -1966660860,
            0,
            0});
            this.nudObjRentPriceInv.Name = "nudObjRentPriceInv";
            this.nudObjRentPriceInv.Size = new System.Drawing.Size(80, 20);
            this.nudObjRentPriceInv.TabIndex = 2;
            this.nudObjRentPriceInv.ValueChanged += new System.EventHandler(this.ObjValueValidated);
            // 
            // cboxObjMatherial
            // 
            this.cboxObjMatherial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxObjMatherial.ItemHeight = 13;
            this.cboxObjMatherial.Location = new System.Drawing.Point(8, 100);
            this.cboxObjMatherial.Name = "cboxObjMatherial";
            this.cboxObjMatherial.Size = new System.Drawing.Size(252, 21);
            this.cboxObjMatherial.TabIndex = 7;
            this.cboxObjMatherial.SelectedIndexChanged += new System.EventHandler(this.ObjValueValidated);
            // 
            // cboxObjTimerUOM
            // 
            this.cboxObjTimerUOM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxObjTimerUOM.ItemHeight = 13;
            this.cboxObjTimerUOM.Items.AddRange(new object[] {
            "Минут",
            "Часов",
            "Суток"});
            this.cboxObjTimerUOM.Location = new System.Drawing.Point(352, 100);
            this.cboxObjTimerUOM.Name = "cboxObjTimerUOM";
            this.cboxObjTimerUOM.Size = new System.Drawing.Size(82, 21);
            this.cboxObjTimerUOM.TabIndex = 9;
            this.cboxObjTimerUOM.SelectedIndexChanged += new System.EventHandler(this.CboxObjTimerUomSelectedIndexChanged);
            // 
            // nudObjTimer
            // 
            this.nudObjTimer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudObjTimer.Location = new System.Drawing.Point(266, 101);
            this.nudObjTimer.Maximum = new decimal(new int[] {
            -1981284353,
            -1966660860,
            0,
            0});
            this.nudObjTimer.Name = "nudObjTimer";
            this.nudObjTimer.Size = new System.Drawing.Size(80, 20);
            this.nudObjTimer.TabIndex = 8;
            this.nudObjTimer.ValueChanged += new System.EventHandler(this.NudObjTimerValueChanged);
            // 
            // nudObjWeight
            // 
            this.nudObjWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudObjWeight.Location = new System.Drawing.Point(266, 61);
            this.nudObjWeight.Maximum = new decimal(new int[] {
            -1981284353,
            -1966660860,
            0,
            0});
            this.nudObjWeight.Name = "nudObjWeight";
            this.nudObjWeight.Size = new System.Drawing.Size(80, 20);
            this.nudObjWeight.TabIndex = 4;
            this.nudObjWeight.ValueChanged += new System.EventHandler(this.ObjValueValidated);
            // 
            // nudObjPrice
            // 
            this.nudObjPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudObjPrice.Location = new System.Drawing.Point(180, 20);
            this.nudObjPrice.Maximum = new decimal(new int[] {
            -1981284353,
            -1966660860,
            0,
            0});
            this.nudObjPrice.Name = "nudObjPrice";
            this.nudObjPrice.Size = new System.Drawing.Size(80, 20);
            this.nudObjPrice.TabIndex = 3;
            this.nudObjPrice.ValueChanged += new System.EventHandler(this.ObjValueValidated);
            // 
            // nudObjRentPriceEquip
            // 
            this.nudObjRentPriceEquip.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudObjRentPriceEquip.Location = new System.Drawing.Point(8, 20);
            this.nudObjRentPriceEquip.Maximum = new decimal(new int[] {
            -1981284353,
            -1966660860,
            0,
            0});
            this.nudObjRentPriceEquip.Name = "nudObjRentPriceEquip";
            this.nudObjRentPriceEquip.Size = new System.Drawing.Size(80, 20);
            this.nudObjRentPriceEquip.TabIndex = 1;
            this.nudObjRentPriceEquip.ValueChanged += new System.EventHandler(this.ObjValueValidated);
            // 
            // cboxObjMaxStructHits
            // 
            this.cboxObjMaxStructHits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxObjMaxStructHits.ItemHeight = 13;
            this.cboxObjMaxStructHits.Location = new System.Drawing.Point(8, 140);
            this.cboxObjMaxStructHits.Name = "cboxObjMaxStructHits";
            this.cboxObjMaxStructHits.Size = new System.Drawing.Size(338, 21);
            this.cboxObjMaxStructHits.TabIndex = 10;
            this.cboxObjMaxStructHits.SelectedIndexChanged += new System.EventHandler(this.ObjValueValidated);
            // 
            // nudObjCurStructHits
            // 
            this.nudObjCurStructHits.Location = new System.Drawing.Point(352, 141);
            this.nudObjCurStructHits.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudObjCurStructHits.Name = "nudObjCurStructHits";
            this.nudObjCurStructHits.Size = new System.Drawing.Size(82, 20);
            this.nudObjCurStructHits.TabIndex = 11;
            this.nudObjCurStructHits.ValueChanged += new System.EventHandler(this.ObjValueValidated);
            // 
            // gbObjType
            // 
            this.gbObjType.Controls.Add(this.pObjMagIngr);
            this.gbObjType.Controls.Add(this.cboxObjType);
            this.gbObjType.Controls.Add(this.pObjMagWand);
            this.gbObjType.Controls.Add(this.pObjWeapon);
            this.gbObjType.Controls.Add(this.pObjPotion);
            this.gbObjType.Controls.Add(this.pObjMoney);
            this.gbObjType.Controls.Add(this.pObjMagStaff);
            this.gbObjType.Controls.Add(this.pObjMagicScroll);
            this.gbObjType.Controls.Add(this.pObjMagBook);
            this.gbObjType.Controls.Add(this.pObjLiquidContainer);
            this.gbObjType.Controls.Add(this.pObjLighter);
            this.gbObjType.Controls.Add(this.pObjFood);
            this.gbObjType.Controls.Add(this.pObjFontan);
            this.gbObjType.Controls.Add(this.pObjContainer);
            this.gbObjType.Controls.Add(this.pObjBandage);
            this.gbObjType.Controls.Add(this.pObjArmor);
            this.gbObjType.Location = new System.Drawing.Point(6, 167);
            this.gbObjType.Name = "gbObjType";
            this.gbObjType.Size = new System.Drawing.Size(428, 230);
            this.gbObjType.TabIndex = 99;
            this.gbObjType.TabStop = false;
            this.gbObjType.Text = "Тип объекта";
            // 
            // pObjMagIngr
            // 
            this.pObjMagIngr.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pObjMagIngr.AutoScroll = true;
            this.pObjMagIngr.Controls.Add(this.lvObjMagIngrFlags);
            this.pObjMagIngr.Controls.Add(this.nudObjMagIngrUseRemain);
            this.pObjMagIngr.Controls.Add(this.nudObjMagIngrPrototype);
            this.pObjMagIngr.Controls.Add(this.nudObjMagIngrMinLev);
            this.pObjMagIngr.Controls.Add(this.nudObjMagIngrLag);
            this.pObjMagIngr.Controls.Add(label128);
            this.pObjMagIngr.Controls.Add(label129);
            this.pObjMagIngr.Controls.Add(label130);
            this.pObjMagIngr.Controls.Add(label131);
            this.pObjMagIngr.Location = new System.Drawing.Point(6, 43);
            this.pObjMagIngr.Name = "pObjMagIngr";
            this.pObjMagIngr.Size = new System.Drawing.Size(416, 187);
            this.pObjMagIngr.TabIndex = 30;
            // 
            // lvObjMagIngrFlags
            // 
            this.lvObjMagIngrFlags.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvObjMagIngrFlags.CheckBoxes = true;
            this.lvObjMagIngrFlags.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4});
            this.lvObjMagIngrFlags.FullRowSelect = true;
            this.lvObjMagIngrFlags.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvObjMagIngrFlags.HideSelection = false;
            this.lvObjMagIngrFlags.Location = new System.Drawing.Point(0, 51);
            this.lvObjMagIngrFlags.Name = "lvObjMagIngrFlags";
            this.lvObjMagIngrFlags.ShowItemToolTips = true;
            this.lvObjMagIngrFlags.Size = new System.Drawing.Size(416, 136);
            this.lvObjMagIngrFlags.TabIndex = 5;
            this.lvObjMagIngrFlags.UseCompatibleStateImageBehavior = false;
            this.lvObjMagIngrFlags.View = System.Windows.Forms.View.Details;
            this.lvObjMagIngrFlags.Validated += new System.EventHandler(this.ObjTypeSpecParamChanged);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Width = 228;
            // 
            // nudObjMagIngrUseRemain
            // 
            this.nudObjMagIngrUseRemain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudObjMagIngrUseRemain.Location = new System.Drawing.Point(164, 28);
            this.nudObjMagIngrUseRemain.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.nudObjMagIngrUseRemain.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.nudObjMagIngrUseRemain.Name = "nudObjMagIngrUseRemain";
            this.nudObjMagIngrUseRemain.Size = new System.Drawing.Size(80, 20);
            this.nudObjMagIngrUseRemain.TabIndex = 4;
            this.nudObjMagIngrUseRemain.ValueChanged += new System.EventHandler(this.ObjTypeSpecParamChanged);
            // 
            // nudObjMagIngrPrototype
            // 
            this.nudObjMagIngrPrototype.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudObjMagIngrPrototype.Location = new System.Drawing.Point(164, 2);
            this.nudObjMagIngrPrototype.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.nudObjMagIngrPrototype.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.nudObjMagIngrPrototype.Name = "nudObjMagIngrPrototype";
            this.nudObjMagIngrPrototype.Size = new System.Drawing.Size(80, 20);
            this.nudObjMagIngrPrototype.TabIndex = 2;
            this.nudObjMagIngrPrototype.ValueChanged += new System.EventHandler(this.ObjTypeSpecParamChanged);
            // 
            // nudObjMagIngrMinLev
            // 
            this.nudObjMagIngrMinLev.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudObjMagIngrMinLev.Location = new System.Drawing.Point(0, 26);
            this.nudObjMagIngrMinLev.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudObjMagIngrMinLev.Name = "nudObjMagIngrMinLev";
            this.nudObjMagIngrMinLev.Size = new System.Drawing.Size(80, 20);
            this.nudObjMagIngrMinLev.TabIndex = 3;
            this.nudObjMagIngrMinLev.ValueChanged += new System.EventHandler(this.ObjTypeSpecParamChanged);
            // 
            // nudObjMagIngrLag
            // 
            this.nudObjMagIngrLag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudObjMagIngrLag.Location = new System.Drawing.Point(0, 0);
            this.nudObjMagIngrLag.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudObjMagIngrLag.Name = "nudObjMagIngrLag";
            this.nudObjMagIngrLag.Size = new System.Drawing.Size(80, 20);
            this.nudObjMagIngrLag.TabIndex = 1;
            this.nudObjMagIngrLag.ValueChanged += new System.EventHandler(this.ObjTypeSpecParamChanged);
            // 
            // cboxObjType
            // 
            this.cboxObjType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxObjType.ItemHeight = 13;
            this.cboxObjType.Location = new System.Drawing.Point(6, 16);
            this.cboxObjType.Name = "cboxObjType";
            this.cboxObjType.Size = new System.Drawing.Size(416, 21);
            this.cboxObjType.TabIndex = 33;
            this.cboxObjType.SelectedIndexChanged += new System.EventHandler(this.CBoxObjTypeSelectedIndexChanged);
            // 
            // pObjMagWand
            // 
            this.pObjMagWand.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pObjMagWand.AutoScroll = true;
            this.pObjMagWand.Controls.Add(this.cboxObjMagWandSpell);
            this.pObjMagWand.Controls.Add(this.nudObjMagWandZarCntCurr);
            this.pObjMagWand.Controls.Add(this.nudObjMagWandZarCnt);
            this.pObjMagWand.Controls.Add(this.nudObjMagWandMinLev);
            this.pObjMagWand.Controls.Add(label166);
            this.pObjMagWand.Controls.Add(label167);
            this.pObjMagWand.Controls.Add(this.label168);
            this.pObjMagWand.Controls.Add(label169);
            this.pObjMagWand.Location = new System.Drawing.Point(6, 43);
            this.pObjMagWand.Name = "pObjMagWand";
            this.pObjMagWand.Size = new System.Drawing.Size(416, 187);
            this.pObjMagWand.TabIndex = 22;
            // 
            // cboxObjMagWandSpell
            // 
            this.cboxObjMagWandSpell.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboxObjMagWandSpell.ItemHeight = 13;
            this.cboxObjMagWandSpell.Location = new System.Drawing.Point(0, 93);
            this.cboxObjMagWandSpell.Name = "cboxObjMagWandSpell";
            this.cboxObjMagWandSpell.Size = new System.Drawing.Size(416, 21);
            this.cboxObjMagWandSpell.TabIndex = 4;
            this.cboxObjMagWandSpell.SelectedIndexChanged += new System.EventHandler(this.ObjTypeSpecParamChanged);
            // 
            // nudObjMagWandZarCntCurr
            // 
            this.nudObjMagWandZarCntCurr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudObjMagWandZarCntCurr.Location = new System.Drawing.Point(0, 52);
            this.nudObjMagWandZarCntCurr.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudObjMagWandZarCntCurr.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudObjMagWandZarCntCurr.Name = "nudObjMagWandZarCntCurr";
            this.nudObjMagWandZarCntCurr.Size = new System.Drawing.Size(66, 20);
            this.nudObjMagWandZarCntCurr.TabIndex = 3;
            this.nudObjMagWandZarCntCurr.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudObjMagWandZarCntCurr.ValueChanged += new System.EventHandler(this.ObjTypeSpecParamChanged);
            // 
            // nudObjMagWandZarCnt
            // 
            this.nudObjMagWandZarCnt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudObjMagWandZarCnt.Location = new System.Drawing.Point(0, 26);
            this.nudObjMagWandZarCnt.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudObjMagWandZarCnt.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudObjMagWandZarCnt.Name = "nudObjMagWandZarCnt";
            this.nudObjMagWandZarCnt.Size = new System.Drawing.Size(66, 20);
            this.nudObjMagWandZarCnt.TabIndex = 2;
            this.nudObjMagWandZarCnt.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudObjMagWandZarCnt.ValueChanged += new System.EventHandler(this.ObjTypeSpecParamChanged);
            // 
            // nudObjMagWandMinLev
            // 
            this.nudObjMagWandMinLev.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudObjMagWandMinLev.Location = new System.Drawing.Point(0, 0);
            this.nudObjMagWandMinLev.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudObjMagWandMinLev.Name = "nudObjMagWandMinLev";
            this.nudObjMagWandMinLev.Size = new System.Drawing.Size(66, 20);
            this.nudObjMagWandMinLev.TabIndex = 1;
            this.nudObjMagWandMinLev.ValueChanged += new System.EventHandler(this.ObjTypeSpecParamChanged);
            // 
            // label168
            // 
            this.label168.Location = new System.Drawing.Point(-3, 76);
            this.label168.Name = "label168";
            this.label168.Size = new System.Drawing.Size(120, 14);
            this.label168.TabIndex = 75;
            this.label168.Text = "Заклинание";
            this.label168.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pObjWeapon
            // 
            this.pObjWeapon.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pObjWeapon.AutoScroll = true;
            this.pObjWeapon.Controls.Add(this.lObjAverageDam);
            this.pObjWeapon.Controls.Add(this.cboxObjWeaponSrikeType);
            this.pObjWeapon.Controls.Add(this.nudObjWeaponD2);
            this.pObjWeapon.Controls.Add(this.nudObjWeaponD1);
            this.pObjWeapon.Controls.Add(label143);
            this.pObjWeapon.Controls.Add(this.label144);
            this.pObjWeapon.Controls.Add(label145);
            this.pObjWeapon.Location = new System.Drawing.Point(6, 43);
            this.pObjWeapon.Name = "pObjWeapon";
            this.pObjWeapon.Size = new System.Drawing.Size(416, 187);
            this.pObjWeapon.TabIndex = 24;
            // 
            // lObjAverageDam
            // 
            this.lObjAverageDam.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lObjAverageDam.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lObjAverageDam.Location = new System.Drawing.Point(198, 6);
            this.lObjAverageDam.Name = "lObjAverageDam";
            this.lObjAverageDam.Size = new System.Drawing.Size(59, 14);
            this.lObjAverageDam.TabIndex = 81;
            this.lObjAverageDam.Text = "Ср: ";
            this.lObjAverageDam.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboxObjWeaponSrikeType
            // 
            this.cboxObjWeaponSrikeType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboxObjWeaponSrikeType.ItemHeight = 13;
            this.cboxObjWeaponSrikeType.Location = new System.Drawing.Point(0, 42);
            this.cboxObjWeaponSrikeType.Name = "cboxObjWeaponSrikeType";
            this.cboxObjWeaponSrikeType.Size = new System.Drawing.Size(416, 21);
            this.cboxObjWeaponSrikeType.TabIndex = 3;
            this.cboxObjWeaponSrikeType.SelectedIndexChanged += new System.EventHandler(this.ObjTypeSpecParamChanged);
            // 
            // nudObjWeaponD2
            // 
            this.nudObjWeaponD2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudObjWeaponD2.Location = new System.Drawing.Point(148, 3);
            this.nudObjWeaponD2.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudObjWeaponD2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudObjWeaponD2.Name = "nudObjWeaponD2";
            this.nudObjWeaponD2.Size = new System.Drawing.Size(50, 20);
            this.nudObjWeaponD2.TabIndex = 2;
            this.nudObjWeaponD2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudObjWeaponD2.ValueChanged += new System.EventHandler(this.ObjTypeSpecParamChanged);
            // 
            // nudObjWeaponD1
            // 
            this.nudObjWeaponD1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudObjWeaponD1.Location = new System.Drawing.Point(80, 3);
            this.nudObjWeaponD1.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudObjWeaponD1.Name = "nudObjWeaponD1";
            this.nudObjWeaponD1.Size = new System.Drawing.Size(50, 20);
            this.nudObjWeaponD1.TabIndex = 1;
            this.nudObjWeaponD1.ValueChanged += new System.EventHandler(this.ObjTypeSpecParamChanged);
            // 
            // label144
            // 
            this.label144.Location = new System.Drawing.Point(-2, 26);
            this.label144.Name = "label144";
            this.label144.Size = new System.Drawing.Size(120, 14);
            this.label144.TabIndex = 75;
            this.label144.Text = "Тип удара";
            this.label144.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pObjPotion
            // 
            this.pObjPotion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pObjPotion.AutoScroll = true;
            this.pObjPotion.Controls.Add(this.cboxObjPotionSpell2);
            this.pObjPotion.Controls.Add(this.cboxObjPotionSpell3);
            this.pObjPotion.Controls.Add(this.cboxObjPotionSpell1);
            this.pObjPotion.Controls.Add(this.nudObjPotionMinLev);
            this.pObjPotion.Controls.Add(this.label148);
            this.pObjPotion.Controls.Add(this.label149);
            this.pObjPotion.Controls.Add(label150);
            this.pObjPotion.Controls.Add(this.label151);
            this.pObjPotion.Location = new System.Drawing.Point(6, 43);
            this.pObjPotion.Name = "pObjPotion";
            this.pObjPotion.Size = new System.Drawing.Size(416, 187);
            this.pObjPotion.TabIndex = 26;
            // 
            // cboxObjPotionSpell2
            // 
            this.cboxObjPotionSpell2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboxObjPotionSpell2.ItemHeight = 13;
            this.cboxObjPotionSpell2.Location = new System.Drawing.Point(0, 84);
            this.cboxObjPotionSpell2.Name = "cboxObjPotionSpell2";
            this.cboxObjPotionSpell2.Size = new System.Drawing.Size(416, 21);
            this.cboxObjPotionSpell2.TabIndex = 3;
            this.cboxObjPotionSpell2.SelectedIndexChanged += new System.EventHandler(this.ObjTypeSpecParamChanged);
            // 
            // cboxObjPotionSpell3
            // 
            this.cboxObjPotionSpell3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboxObjPotionSpell3.ItemHeight = 13;
            this.cboxObjPotionSpell3.Location = new System.Drawing.Point(0, 125);
            this.cboxObjPotionSpell3.Name = "cboxObjPotionSpell3";
            this.cboxObjPotionSpell3.Size = new System.Drawing.Size(416, 21);
            this.cboxObjPotionSpell3.TabIndex = 4;
            this.cboxObjPotionSpell3.SelectedIndexChanged += new System.EventHandler(this.ObjTypeSpecParamChanged);
            // 
            // cboxObjPotionSpell1
            // 
            this.cboxObjPotionSpell1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboxObjPotionSpell1.ItemHeight = 13;
            this.cboxObjPotionSpell1.Location = new System.Drawing.Point(0, 43);
            this.cboxObjPotionSpell1.Name = "cboxObjPotionSpell1";
            this.cboxObjPotionSpell1.Size = new System.Drawing.Size(416, 21);
            this.cboxObjPotionSpell1.TabIndex = 2;
            this.cboxObjPotionSpell1.SelectedIndexChanged += new System.EventHandler(this.ObjTypeSpecParamChanged);
            // 
            // nudObjPotionMinLev
            // 
            this.nudObjPotionMinLev.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudObjPotionMinLev.Location = new System.Drawing.Point(0, 0);
            this.nudObjPotionMinLev.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudObjPotionMinLev.Name = "nudObjPotionMinLev";
            this.nudObjPotionMinLev.Size = new System.Drawing.Size(66, 20);
            this.nudObjPotionMinLev.TabIndex = 1;
            this.nudObjPotionMinLev.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudObjPotionMinLev.ValueChanged += new System.EventHandler(this.ObjTypeSpecParamChanged);
            // 
            // label148
            // 
            this.label148.Location = new System.Drawing.Point(-3, 67);
            this.label148.Name = "label148";
            this.label148.Size = new System.Drawing.Size(120, 14);
            this.label148.TabIndex = 75;
            this.label148.Text = "Заклинание 2";
            this.label148.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label149
            // 
            this.label149.Location = new System.Drawing.Point(-3, 108);
            this.label149.Name = "label149";
            this.label149.Size = new System.Drawing.Size(120, 14);
            this.label149.TabIndex = 75;
            this.label149.Text = "Заклинание 3";
            this.label149.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label151
            // 
            this.label151.Location = new System.Drawing.Point(-3, 26);
            this.label151.Name = "label151";
            this.label151.Size = new System.Drawing.Size(120, 14);
            this.label151.TabIndex = 75;
            this.label151.Text = "Заклинание 1";
            this.label151.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pObjMoney
            // 
            this.pObjMoney.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pObjMoney.AutoScroll = true;
            this.pObjMoney.Controls.Add(this.cboxMoneyCurrency);
            this.pObjMoney.Controls.Add(label68);
            this.pObjMoney.Controls.Add(this.nudObjMoneyValue);
            this.pObjMoney.Controls.Add(label136);
            this.pObjMoney.Location = new System.Drawing.Point(6, 43);
            this.pObjMoney.Name = "pObjMoney";
            this.pObjMoney.Size = new System.Drawing.Size(416, 187);
            this.pObjMoney.TabIndex = 28;
            // 
            // cboxMoneyCurrency
            // 
            this.cboxMoneyCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxMoneyCurrency.ItemHeight = 13;
            this.cboxMoneyCurrency.Location = new System.Drawing.Point(0, 26);
            this.cboxMoneyCurrency.Name = "cboxMoneyCurrency";
            this.cboxMoneyCurrency.Size = new System.Drawing.Size(105, 21);
            this.cboxMoneyCurrency.TabIndex = 69;
            this.cboxMoneyCurrency.SelectedIndexChanged += new System.EventHandler(this.ObjTypeSpecParamChanged);
            // 
            // nudObjMoneyValue
            // 
            this.nudObjMoneyValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudObjMoneyValue.Location = new System.Drawing.Point(0, 0);
            this.nudObjMoneyValue.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudObjMoneyValue.Name = "nudObjMoneyValue";
            this.nudObjMoneyValue.Size = new System.Drawing.Size(105, 20);
            this.nudObjMoneyValue.TabIndex = 1;
            this.nudObjMoneyValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudObjMoneyValue.ValueChanged += new System.EventHandler(this.ObjTypeSpecParamChanged);
            // 
            // pObjMagStaff
            // 
            this.pObjMagStaff.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pObjMagStaff.AutoScroll = true;
            this.pObjMagStaff.Controls.Add(this.cboxObjMagStaffSpell);
            this.pObjMagStaff.Controls.Add(this.nudObjMagStaffZarCntCur);
            this.pObjMagStaff.Controls.Add(this.nudObjMagStaffZarCnt);
            this.pObjMagStaff.Controls.Add(this.nudObjMagStaffMinLev);
            this.pObjMagStaff.Controls.Add(label157);
            this.pObjMagStaff.Controls.Add(label158);
            this.pObjMagStaff.Controls.Add(this.label159);
            this.pObjMagStaff.Controls.Add(label160);
            this.pObjMagStaff.Location = new System.Drawing.Point(6, 43);
            this.pObjMagStaff.Name = "pObjMagStaff";
            this.pObjMagStaff.Size = new System.Drawing.Size(416, 187);
            this.pObjMagStaff.TabIndex = 23;
            // 
            // cboxObjMagStaffSpell
            // 
            this.cboxObjMagStaffSpell.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboxObjMagStaffSpell.ItemHeight = 13;
            this.cboxObjMagStaffSpell.Location = new System.Drawing.Point(0, 93);
            this.cboxObjMagStaffSpell.Name = "cboxObjMagStaffSpell";
            this.cboxObjMagStaffSpell.Size = new System.Drawing.Size(416, 21);
            this.cboxObjMagStaffSpell.TabIndex = 4;
            this.cboxObjMagStaffSpell.SelectedIndexChanged += new System.EventHandler(this.ObjTypeSpecParamChanged);
            // 
            // nudObjMagStaffZarCntCur
            // 
            this.nudObjMagStaffZarCntCur.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudObjMagStaffZarCntCur.Location = new System.Drawing.Point(0, 52);
            this.nudObjMagStaffZarCntCur.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudObjMagStaffZarCntCur.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudObjMagStaffZarCntCur.Name = "nudObjMagStaffZarCntCur";
            this.nudObjMagStaffZarCntCur.Size = new System.Drawing.Size(66, 20);
            this.nudObjMagStaffZarCntCur.TabIndex = 3;
            this.nudObjMagStaffZarCntCur.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudObjMagStaffZarCntCur.ValueChanged += new System.EventHandler(this.ObjTypeSpecParamChanged);
            // 
            // nudObjMagStaffZarCnt
            // 
            this.nudObjMagStaffZarCnt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudObjMagStaffZarCnt.Location = new System.Drawing.Point(0, 26);
            this.nudObjMagStaffZarCnt.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudObjMagStaffZarCnt.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudObjMagStaffZarCnt.Name = "nudObjMagStaffZarCnt";
            this.nudObjMagStaffZarCnt.Size = new System.Drawing.Size(66, 20);
            this.nudObjMagStaffZarCnt.TabIndex = 2;
            this.nudObjMagStaffZarCnt.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudObjMagStaffZarCnt.ValueChanged += new System.EventHandler(this.ObjTypeSpecParamChanged);
            // 
            // nudObjMagStaffMinLev
            // 
            this.nudObjMagStaffMinLev.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudObjMagStaffMinLev.Location = new System.Drawing.Point(0, 0);
            this.nudObjMagStaffMinLev.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudObjMagStaffMinLev.Name = "nudObjMagStaffMinLev";
            this.nudObjMagStaffMinLev.Size = new System.Drawing.Size(66, 20);
            this.nudObjMagStaffMinLev.TabIndex = 1;
            this.nudObjMagStaffMinLev.ValueChanged += new System.EventHandler(this.ObjTypeSpecParamChanged);
            // 
            // label159
            // 
            this.label159.Location = new System.Drawing.Point(-3, 76);
            this.label159.Name = "label159";
            this.label159.Size = new System.Drawing.Size(120, 14);
            this.label159.TabIndex = 75;
            this.label159.Text = "Заклинание";
            this.label159.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pObjMagicScroll
            // 
            this.pObjMagicScroll.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pObjMagicScroll.AutoScroll = true;
            this.pObjMagicScroll.Controls.Add(this.cboxObjMagScrollSpell2);
            this.pObjMagicScroll.Controls.Add(this.cboxObjMagScrollSpell3);
            this.pObjMagicScroll.Controls.Add(this.cboxObjMagScrollSpell1);
            this.pObjMagicScroll.Controls.Add(this.nudObjMagScrollMinLev);
            this.pObjMagicScroll.Controls.Add(this.label141);
            this.pObjMagicScroll.Controls.Add(this.label153);
            this.pObjMagicScroll.Controls.Add(label154);
            this.pObjMagicScroll.Controls.Add(this.label155);
            this.pObjMagicScroll.Location = new System.Drawing.Point(6, 43);
            this.pObjMagicScroll.Name = "pObjMagicScroll";
            this.pObjMagicScroll.Size = new System.Drawing.Size(416, 187);
            this.pObjMagicScroll.TabIndex = 21;
            // 
            // cboxObjMagScrollSpell2
            // 
            this.cboxObjMagScrollSpell2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboxObjMagScrollSpell2.ItemHeight = 13;
            this.cboxObjMagScrollSpell2.Location = new System.Drawing.Point(0, 90);
            this.cboxObjMagScrollSpell2.Name = "cboxObjMagScrollSpell2";
            this.cboxObjMagScrollSpell2.Size = new System.Drawing.Size(416, 21);
            this.cboxObjMagScrollSpell2.TabIndex = 3;
            this.cboxObjMagScrollSpell2.SelectedIndexChanged += new System.EventHandler(this.ObjTypeSpecParamChanged);
            // 
            // cboxObjMagScrollSpell3
            // 
            this.cboxObjMagScrollSpell3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboxObjMagScrollSpell3.ItemHeight = 13;
            this.cboxObjMagScrollSpell3.Location = new System.Drawing.Point(0, 131);
            this.cboxObjMagScrollSpell3.Name = "cboxObjMagScrollSpell3";
            this.cboxObjMagScrollSpell3.Size = new System.Drawing.Size(416, 21);
            this.cboxObjMagScrollSpell3.TabIndex = 4;
            this.cboxObjMagScrollSpell3.SelectedIndexChanged += new System.EventHandler(this.ObjTypeSpecParamChanged);
            // 
            // cboxObjMagScrollSpell1
            // 
            this.cboxObjMagScrollSpell1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboxObjMagScrollSpell1.ItemHeight = 13;
            this.cboxObjMagScrollSpell1.Location = new System.Drawing.Point(0, 49);
            this.cboxObjMagScrollSpell1.Name = "cboxObjMagScrollSpell1";
            this.cboxObjMagScrollSpell1.Size = new System.Drawing.Size(416, 21);
            this.cboxObjMagScrollSpell1.TabIndex = 2;
            this.cboxObjMagScrollSpell1.SelectedIndexChanged += new System.EventHandler(this.ObjTypeSpecParamChanged);
            // 
            // nudObjMagScrollMinLev
            // 
            this.nudObjMagScrollMinLev.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudObjMagScrollMinLev.Location = new System.Drawing.Point(201, 7);
            this.nudObjMagScrollMinLev.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudObjMagScrollMinLev.Name = "nudObjMagScrollMinLev";
            this.nudObjMagScrollMinLev.Size = new System.Drawing.Size(66, 20);
            this.nudObjMagScrollMinLev.TabIndex = 1;
            this.nudObjMagScrollMinLev.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudObjMagScrollMinLev.ValueChanged += new System.EventHandler(this.ObjTypeSpecParamChanged);
            // 
            // label141
            // 
            this.label141.Location = new System.Drawing.Point(-3, 74);
            this.label141.Name = "label141";
            this.label141.Size = new System.Drawing.Size(120, 14);
            this.label141.TabIndex = 75;
            this.label141.Text = "Заклинание 2";
            this.label141.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label153
            // 
            this.label153.Location = new System.Drawing.Point(-2, 114);
            this.label153.Name = "label153";
            this.label153.Size = new System.Drawing.Size(120, 14);
            this.label153.TabIndex = 75;
            this.label153.Text = "Заклинание 3";
            this.label153.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label155
            // 
            this.label155.Location = new System.Drawing.Point(-2, 30);
            this.label155.Name = "label155";
            this.label155.Size = new System.Drawing.Size(120, 14);
            this.label155.TabIndex = 75;
            this.label155.Text = "Заклинание 1";
            this.label155.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pObjMagBook
            // 
            this.pObjMagBook.AutoScroll = true;
            this.pObjMagBook.Controls.Add(this.cboxObjMagBookSpell);
            this.pObjMagBook.Controls.Add(this.label156);
            this.pObjMagBook.Location = new System.Drawing.Point(6, 43);
            this.pObjMagBook.Name = "pObjMagBook";
            this.pObjMagBook.Size = new System.Drawing.Size(392, 164);
            this.pObjMagBook.TabIndex = 23;
            // 
            // cboxObjMagBookSpell
            // 
            this.cboxObjMagBookSpell.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboxObjMagBookSpell.ItemHeight = 13;
            this.cboxObjMagBookSpell.Location = new System.Drawing.Point(0, 24);
            this.cboxObjMagBookSpell.Name = "cboxObjMagBookSpell";
            this.cboxObjMagBookSpell.Size = new System.Drawing.Size(392, 21);
            this.cboxObjMagBookSpell.TabIndex = 1;
            this.cboxObjMagBookSpell.SelectedIndexChanged += new System.EventHandler(this.ObjTypeSpecParamChanged);
            // 
            // label156
            // 
            this.label156.Location = new System.Drawing.Point(-2, 4);
            this.label156.Name = "label156";
            this.label156.Size = new System.Drawing.Size(120, 19);
            this.label156.TabIndex = 75;
            this.label156.Text = "Заклинание";
            this.label156.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pObjLiquidContainer
            // 
            this.pObjLiquidContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pObjLiquidContainer.AutoScroll = true;
            this.pObjLiquidContainer.Controls.Add(this.btnSelectPotionProtoVNum);
            this.pObjLiquidContainer.Controls.Add(this.cboxObjLiquidContainerDrinkType);
            this.pObjLiquidContainer.Controls.Add(this.nudPotionProtoVNum);
            this.pObjLiquidContainer.Controls.Add(this.nudObjLiquidContainerCurVal);
            this.pObjLiquidContainer.Controls.Add(this.nudObjLiquidContainerMaxVal);
            this.pObjLiquidContainer.Controls.Add(this.nudObjLiquidContainerPoison);
            this.pObjLiquidContainer.Controls.Add(label1111);
            this.pObjLiquidContainer.Controls.Add(label163);
            this.pObjLiquidContainer.Controls.Add(label164);
            this.pObjLiquidContainer.Controls.Add(label165);
            this.pObjLiquidContainer.Location = new System.Drawing.Point(6, 43);
            this.pObjLiquidContainer.Name = "pObjLiquidContainer";
            this.pObjLiquidContainer.Size = new System.Drawing.Size(416, 187);
            this.pObjLiquidContainer.TabIndex = 22;
            // 
            // cboxObjLiquidContainerDrinkType
            // 
            this.cboxObjLiquidContainerDrinkType.ItemHeight = 13;
            this.cboxObjLiquidContainerDrinkType.Location = new System.Drawing.Point(0, 66);
            this.cboxObjLiquidContainerDrinkType.Name = "cboxObjLiquidContainerDrinkType";
            this.cboxObjLiquidContainerDrinkType.Size = new System.Drawing.Size(188, 21);
            this.cboxObjLiquidContainerDrinkType.TabIndex = 3;
            this.cboxObjLiquidContainerDrinkType.SelectedIndexChanged += new System.EventHandler(this.ObjTypeSpecParamChanged);
            // 
            // nudPotionProtoVNum
            // 
            this.nudPotionProtoVNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudPotionProtoVNum.Location = new System.Drawing.Point(219, 66);
            this.nudPotionProtoVNum.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.nudPotionProtoVNum.Name = "nudPotionProtoVNum";
            this.nudPotionProtoVNum.Size = new System.Drawing.Size(50, 20);
            this.nudPotionProtoVNum.TabIndex = 5;
            this.nudPotionProtoVNum.Visible = false;
            this.nudPotionProtoVNum.ValueChanged += new System.EventHandler(this.ObjTypeSpecParamChanged);
            // 
            // nudObjLiquidContainerCurVal
            // 
            this.nudObjLiquidContainerCurVal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudObjLiquidContainerCurVal.Location = new System.Drawing.Point(219, 26);
            this.nudObjLiquidContainerCurVal.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.nudObjLiquidContainerCurVal.Name = "nudObjLiquidContainerCurVal";
            this.nudObjLiquidContainerCurVal.Size = new System.Drawing.Size(50, 20);
            this.nudObjLiquidContainerCurVal.TabIndex = 2;
            this.nudObjLiquidContainerCurVal.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudObjLiquidContainerCurVal.ValueChanged += new System.EventHandler(this.ObjTypeSpecParamChanged);
            // 
            // nudObjLiquidContainerMaxVal
            // 
            this.nudObjLiquidContainerMaxVal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudObjLiquidContainerMaxVal.Location = new System.Drawing.Point(219, 0);
            this.nudObjLiquidContainerMaxVal.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.nudObjLiquidContainerMaxVal.Name = "nudObjLiquidContainerMaxVal";
            this.nudObjLiquidContainerMaxVal.Size = new System.Drawing.Size(50, 20);
            this.nudObjLiquidContainerMaxVal.TabIndex = 1;
            this.nudObjLiquidContainerMaxVal.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudObjLiquidContainerMaxVal.ValueChanged += new System.EventHandler(this.ObjTypeSpecParamChanged);
            // 
            // nudObjLiquidContainerPoison
            // 
            this.nudObjLiquidContainerPoison.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudObjLiquidContainerPoison.Location = new System.Drawing.Point(219, 91);
            this.nudObjLiquidContainerPoison.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudObjLiquidContainerPoison.Name = "nudObjLiquidContainerPoison";
            this.nudObjLiquidContainerPoison.Size = new System.Drawing.Size(50, 20);
            this.nudObjLiquidContainerPoison.TabIndex = 6;
            this.nudObjLiquidContainerPoison.ValueChanged += new System.EventHandler(this.ObjTypeSpecParamChanged);
            // 
            // pObjLighter
            // 
            this.pObjLighter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pObjLighter.AutoScroll = true;
            this.pObjLighter.Controls.Add(this.nudObjLighterValue);
            this.pObjLighter.Controls.Add(label161);
            this.pObjLighter.Controls.Add(label162);
            this.pObjLighter.Location = new System.Drawing.Point(6, 43);
            this.pObjLighter.Name = "pObjLighter";
            this.pObjLighter.Size = new System.Drawing.Size(416, 187);
            this.pObjLighter.TabIndex = 20;
            // 
            // nudObjLighterValue
            // 
            this.nudObjLighterValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudObjLighterValue.Location = new System.Drawing.Point(0, 6);
            this.nudObjLighterValue.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudObjLighterValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.nudObjLighterValue.Name = "nudObjLighterValue";
            this.nudObjLighterValue.Size = new System.Drawing.Size(66, 20);
            this.nudObjLighterValue.TabIndex = 1;
            this.nudObjLighterValue.ValueChanged += new System.EventHandler(this.ObjTypeSpecParamChanged);
            // 
            // pObjFood
            // 
            this.pObjFood.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pObjFood.AutoScroll = true;
            this.pObjFood.Controls.Add(this.nudObjFoodPoison);
            this.pObjFood.Controls.Add(this.nudObjFoodVal);
            this.pObjFood.Controls.Add(label137);
            this.pObjFood.Controls.Add(label138);
            this.pObjFood.Location = new System.Drawing.Point(6, 43);
            this.pObjFood.Name = "pObjFood";
            this.pObjFood.Size = new System.Drawing.Size(416, 187);
            this.pObjFood.TabIndex = 27;
            // 
            // nudObjFoodPoison
            // 
            this.nudObjFoodPoison.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudObjFoodPoison.Location = new System.Drawing.Point(0, 26);
            this.nudObjFoodPoison.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.nudObjFoodPoison.Name = "nudObjFoodPoison";
            this.nudObjFoodPoison.Size = new System.Drawing.Size(50, 20);
            this.nudObjFoodPoison.TabIndex = 2;
            this.nudObjFoodPoison.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudObjFoodPoison.ValueChanged += new System.EventHandler(this.ObjTypeSpecParamChanged);
            // 
            // nudObjFoodVal
            // 
            this.nudObjFoodVal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudObjFoodVal.Location = new System.Drawing.Point(0, 0);
            this.nudObjFoodVal.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.nudObjFoodVal.Name = "nudObjFoodVal";
            this.nudObjFoodVal.Size = new System.Drawing.Size(50, 20);
            this.nudObjFoodVal.TabIndex = 1;
            this.nudObjFoodVal.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudObjFoodVal.ValueChanged += new System.EventHandler(this.ObjTypeSpecParamChanged);
            // 
            // pObjFontan
            // 
            this.pObjFontan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pObjFontan.AutoScroll = true;
            this.pObjFontan.Controls.Add(this.btnSelectFontPorionProto);
            this.pObjFontan.Controls.Add(this.nudFontPorionProtoVNum);
            this.pObjFontan.Controls.Add(this.cboxObjFontanDrinkType);
            this.pObjFontan.Controls.Add(this.nudObjFontanCurVal);
            this.pObjFontan.Controls.Add(this.nudObjFontanMaxVal);
            this.pObjFontan.Controls.Add(this.nudObjFontanPoison);
            this.pObjFontan.Controls.Add(label132);
            this.pObjFontan.Controls.Add(label133);
            this.pObjFontan.Controls.Add(label134);
            this.pObjFontan.Controls.Add(label135);
            this.pObjFontan.Location = new System.Drawing.Point(6, 43);
            this.pObjFontan.Name = "pObjFontan";
            this.pObjFontan.Size = new System.Drawing.Size(416, 187);
            this.pObjFontan.TabIndex = 29;
            // 
            // nudFontPorionProtoVNum
            // 
            this.nudFontPorionProtoVNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudFontPorionProtoVNum.Location = new System.Drawing.Point(219, 66);
            this.nudFontPorionProtoVNum.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.nudFontPorionProtoVNum.Name = "nudFontPorionProtoVNum";
            this.nudFontPorionProtoVNum.Size = new System.Drawing.Size(50, 20);
            this.nudFontPorionProtoVNum.TabIndex = 5;
            this.nudFontPorionProtoVNum.Visible = false;
            this.nudFontPorionProtoVNum.ValueChanged += new System.EventHandler(this.ObjTypeSpecParamChanged);
            // 
            // cboxObjFontanDrinkType
            // 
            this.cboxObjFontanDrinkType.ItemHeight = 13;
            this.cboxObjFontanDrinkType.Location = new System.Drawing.Point(0, 65);
            this.cboxObjFontanDrinkType.Name = "cboxObjFontanDrinkType";
            this.cboxObjFontanDrinkType.Size = new System.Drawing.Size(188, 21);
            this.cboxObjFontanDrinkType.TabIndex = 3;
            this.cboxObjFontanDrinkType.SelectedIndexChanged += new System.EventHandler(this.ObjTypeSpecParamChanged);
            // 
            // nudObjFontanCurVal
            // 
            this.nudObjFontanCurVal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudObjFontanCurVal.Location = new System.Drawing.Point(219, 26);
            this.nudObjFontanCurVal.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.nudObjFontanCurVal.Name = "nudObjFontanCurVal";
            this.nudObjFontanCurVal.Size = new System.Drawing.Size(50, 20);
            this.nudObjFontanCurVal.TabIndex = 2;
            this.nudObjFontanCurVal.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudObjFontanCurVal.ValueChanged += new System.EventHandler(this.ObjTypeSpecParamChanged);
            // 
            // nudObjFontanMaxVal
            // 
            this.nudObjFontanMaxVal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudObjFontanMaxVal.Location = new System.Drawing.Point(219, 0);
            this.nudObjFontanMaxVal.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.nudObjFontanMaxVal.Name = "nudObjFontanMaxVal";
            this.nudObjFontanMaxVal.Size = new System.Drawing.Size(50, 20);
            this.nudObjFontanMaxVal.TabIndex = 1;
            this.nudObjFontanMaxVal.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudObjFontanMaxVal.ValueChanged += new System.EventHandler(this.ObjTypeSpecParamChanged);
            // 
            // nudObjFontanPoison
            // 
            this.nudObjFontanPoison.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudObjFontanPoison.Location = new System.Drawing.Point(219, 91);
            this.nudObjFontanPoison.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudObjFontanPoison.Name = "nudObjFontanPoison";
            this.nudObjFontanPoison.Size = new System.Drawing.Size(50, 20);
            this.nudObjFontanPoison.TabIndex = 6;
            this.nudObjFontanPoison.ValueChanged += new System.EventHandler(this.ObjTypeSpecParamChanged);
            // 
            // pObjContainer
            // 
            this.pObjContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pObjContainer.AutoScroll = true;
            this.pObjContainer.Controls.Add(this.nudObjLockVal);
            this.pObjContainer.Controls.Add(label171);
            this.pObjContainer.Controls.Add(this.lvObjContainerFlags);
            this.pObjContainer.Controls.Add(this.nudObjContainerKeyVNum);
            this.pObjContainer.Controls.Add(this.nudObjContainerValue);
            this.pObjContainer.Controls.Add(label139);
            this.pObjContainer.Controls.Add(label140);
            this.pObjContainer.Location = new System.Drawing.Point(6, 43);
            this.pObjContainer.Name = "pObjContainer";
            this.pObjContainer.Size = new System.Drawing.Size(416, 183);
            this.pObjContainer.TabIndex = 25;
            // 
            // nudObjLockVal
            // 
            this.nudObjLockVal.Location = new System.Drawing.Point(234, 6);
            this.nudObjLockVal.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudObjLockVal.Name = "nudObjLockVal";
            this.nudObjLockVal.Size = new System.Drawing.Size(54, 20);
            this.nudObjLockVal.TabIndex = 68;
            this.nudObjLockVal.ValueChanged += new System.EventHandler(this.ObjTypeSpecParamChanged);
            // 
            // lvObjContainerFlags
            // 
            this.lvObjContainerFlags.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvObjContainerFlags.CheckBoxes = true;
            this.lvObjContainerFlags.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5});
            this.lvObjContainerFlags.FullRowSelect = true;
            this.lvObjContainerFlags.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvObjContainerFlags.HideSelection = false;
            this.lvObjContainerFlags.Location = new System.Drawing.Point(6, 57);
            this.lvObjContainerFlags.Name = "lvObjContainerFlags";
            this.lvObjContainerFlags.ShowItemToolTips = true;
            this.lvObjContainerFlags.Size = new System.Drawing.Size(405, 123);
            this.lvObjContainerFlags.TabIndex = 3;
            this.lvObjContainerFlags.UseCompatibleStateImageBehavior = false;
            this.lvObjContainerFlags.View = System.Windows.Forms.View.Details;
            this.lvObjContainerFlags.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.ObjTypeSpecParamChanged);
            // 
            // columnHeader5
            // 
            this.columnHeader5.Width = 228;
            // 
            // pObjBandage
            // 
            this.pObjBandage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pObjBandage.AutoScroll = true;
            this.pObjBandage.Controls.Add(this.label173);
            this.pObjBandage.Controls.Add(this.nudObjBandageValue);
            this.pObjBandage.Location = new System.Drawing.Point(6, 43);
            this.pObjBandage.Name = "pObjBandage";
            this.pObjBandage.Size = new System.Drawing.Size(413, 183);
            this.pObjBandage.TabIndex = 32;
            // 
            // label173
            // 
            this.label173.AutoSize = true;
            this.label173.Location = new System.Drawing.Point(-4, 6);
            this.label173.Name = "label173";
            this.label173.Size = new System.Drawing.Size(74, 13);
            this.label173.TabIndex = 1;
            this.label173.Text = "HP в секунду";
            // 
            // nudObjBandageValue
            // 
            this.nudObjBandageValue.Location = new System.Drawing.Point(74, 2);
            this.nudObjBandageValue.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudObjBandageValue.Name = "nudObjBandageValue";
            this.nudObjBandageValue.Size = new System.Drawing.Size(57, 20);
            this.nudObjBandageValue.TabIndex = 0;
            this.nudObjBandageValue.ValueChanged += new System.EventHandler(this.ObjTypeSpecParamChanged);
            // 
            // pObjArmor
            // 
            this.pObjArmor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pObjArmor.AutoScroll = true;
            this.pObjArmor.Controls.Add(this.nudObjArmorArm);
            this.pObjArmor.Controls.Add(this.nudObjArmorAC);
            this.pObjArmor.Controls.Add(label142);
            this.pObjArmor.Controls.Add(this.label146);
            this.pObjArmor.Controls.Add(label147);
            this.pObjArmor.Location = new System.Drawing.Point(6, 43);
            this.pObjArmor.Name = "pObjArmor";
            this.pObjArmor.Size = new System.Drawing.Size(416, 187);
            this.pObjArmor.TabIndex = 25;
            // 
            // nudObjArmorArm
            // 
            this.nudObjArmorArm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudObjArmorArm.Location = new System.Drawing.Point(36, 25);
            this.nudObjArmorArm.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudObjArmorArm.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.nudObjArmorArm.Name = "nudObjArmorArm";
            this.nudObjArmorArm.Size = new System.Drawing.Size(50, 20);
            this.nudObjArmorArm.TabIndex = 2;
            this.nudObjArmorArm.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudObjArmorArm.ValueChanged += new System.EventHandler(this.ObjTypeSpecParamChanged);
            // 
            // nudObjArmorAC
            // 
            this.nudObjArmorAC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudObjArmorAC.Location = new System.Drawing.Point(36, -1);
            this.nudObjArmorAC.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudObjArmorAC.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.nudObjArmorAC.Name = "nudObjArmorAC";
            this.nudObjArmorAC.Size = new System.Drawing.Size(50, 20);
            this.nudObjArmorAC.TabIndex = 1;
            this.nudObjArmorAC.Value = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.nudObjArmorAC.ValueChanged += new System.EventHandler(this.ObjTypeSpecParamChanged);
            // 
            // label146
            // 
            this.label146.Location = new System.Drawing.Point(-3, 49);
            this.label146.Name = "label146";
            this.label146.Size = new System.Drawing.Size(249, 41);
            this.label146.TabIndex = 75;
            this.label146.Text = "Значения больше 0 улучшают AC и броню\r\nЗначения меньше 0 ухудшают AC и броню";
            // 
            // tpObjEffects
            // 
            this.tpObjEffects.Controls.Add(this.tplObjEffects);
            this.tpObjEffects.Location = new System.Drawing.Point(4, 40);
            this.tpObjEffects.Name = "tpObjEffects";
            this.tpObjEffects.Size = new System.Drawing.Size(440, 420);
            this.tpObjEffects.TabIndex = 3;
            this.tpObjEffects.Text = "Эффекты";
            this.tpObjEffects.UseVisualStyleBackColor = true;
            // 
            // tplObjEffects
            // 
            this.tplObjEffects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tplObjEffects.Grouped = false;
            this.tplObjEffects.LeftListCaption = "Эффекты объекта";
            this.tplObjEffects.Location = new System.Drawing.Point(0, 0);
            this.tplObjEffects.Name = "tplObjEffects";
            this.tplObjEffects.RightListCaption = "Доступные эффекты";
            this.tplObjEffects.Size = new System.Drawing.Size(440, 420);
            this.tplObjEffects.SplitterDistance = 207;
            this.tplObjEffects.TabIndex = 0;
            this.tplObjEffects.ValueChanged += new BZEditor.UcTwoPanelsList.ValueChangeEvent(this.TplObjEffectsValueChanged);
            // 
            // tpObjAffects
            // 
            this.tpObjAffects.Controls.Add(this.tplObjAffects);
            this.tpObjAffects.Location = new System.Drawing.Point(4, 40);
            this.tpObjAffects.Name = "tpObjAffects";
            this.tpObjAffects.Size = new System.Drawing.Size(440, 420);
            this.tpObjAffects.TabIndex = 4;
            this.tpObjAffects.Text = "Аффекты";
            this.tpObjAffects.UseVisualStyleBackColor = true;
            // 
            // tplObjAffects
            // 
            this.tplObjAffects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tplObjAffects.Grouped = false;
            this.tplObjAffects.LeftListCaption = "Аффекты объекта";
            this.tplObjAffects.Location = new System.Drawing.Point(0, 0);
            this.tplObjAffects.Name = "tplObjAffects";
            this.tplObjAffects.RightListCaption = "Доступные аффекты";
            this.tplObjAffects.Size = new System.Drawing.Size(440, 420);
            this.tplObjAffects.SplitterDistance = 207;
            this.tplObjAffects.TabIndex = 1;
            this.tplObjAffects.ValueChanged += new BZEditor.UcTwoPanelsList.ValueChangeEvent(this.TplObjAffectsValueChanged);
            // 
            // tpObjWearTo
            // 
            this.tpObjWearTo.Controls.Add(this.tplObjWearTo);
            this.tpObjWearTo.Location = new System.Drawing.Point(4, 40);
            this.tpObjWearTo.Name = "tpObjWearTo";
            this.tpObjWearTo.Size = new System.Drawing.Size(440, 420);
            this.tpObjWearTo.TabIndex = 0;
            this.tpObjWearTo.Text = "Где одеть";
            this.tpObjWearTo.UseVisualStyleBackColor = true;
            // 
            // tplObjWearTo
            // 
            this.tplObjWearTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tplObjWearTo.Grouped = false;
            this.tplObjWearTo.LeftListCaption = "Можно одеть";
            this.tplObjWearTo.Location = new System.Drawing.Point(0, 0);
            this.tplObjWearTo.Name = "tplObjWearTo";
            this.tplObjWearTo.RightListCaption = "Доступные варианты";
            this.tplObjWearTo.Size = new System.Drawing.Size(440, 420);
            this.tplObjWearTo.SplitterDistance = 207;
            this.tplObjWearTo.TabIndex = 0;
            this.tplObjWearTo.ValueChanged += new BZEditor.UcTwoPanelsList.ValueChangeEvent(this.TplObjWearToValueChanged);
            // 
            // tpObjCantTouch
            // 
            this.tpObjCantTouch.Controls.Add(this.tplObjCantTouch);
            this.tpObjCantTouch.Location = new System.Drawing.Point(4, 40);
            this.tpObjCantTouch.Name = "tpObjCantTouch";
            this.tpObjCantTouch.Size = new System.Drawing.Size(440, 420);
            this.tpObjCantTouch.TabIndex = 6;
            this.tpObjCantTouch.Text = "Не могут взять";
            this.tpObjCantTouch.UseVisualStyleBackColor = true;
            // 
            // tplObjCantTouch
            // 
            this.tplObjCantTouch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tplObjCantTouch.Grouped = true;
            this.tplObjCantTouch.LeftListCaption = "Не могут взять";
            this.tplObjCantTouch.Location = new System.Drawing.Point(0, 0);
            this.tplObjCantTouch.Name = "tplObjCantTouch";
            this.tplObjCantTouch.RightListCaption = "Доступные варианты";
            this.tplObjCantTouch.Size = new System.Drawing.Size(440, 420);
            this.tplObjCantTouch.SplitterDistance = 207;
            this.tplObjCantTouch.TabIndex = 1;
            this.tplObjCantTouch.ValueChanged += new BZEditor.UcTwoPanelsList.ValueChangeEvent(this.TplObjCantTouchValueChanged);
            // 
            // tpObjCantUse
            // 
            this.tpObjCantUse.Controls.Add(this.tplObjCantUse);
            this.tpObjCantUse.Location = new System.Drawing.Point(4, 40);
            this.tpObjCantUse.Name = "tpObjCantUse";
            this.tpObjCantUse.Size = new System.Drawing.Size(440, 420);
            this.tpObjCantUse.TabIndex = 7;
            this.tpObjCantUse.Text = "Не могут исп.";
            this.tpObjCantUse.UseVisualStyleBackColor = true;
            // 
            // tplObjCantUse
            // 
            this.tplObjCantUse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tplObjCantUse.Grouped = true;
            this.tplObjCantUse.LeftListCaption = "Не могут использовать";
            this.tplObjCantUse.Location = new System.Drawing.Point(0, 0);
            this.tplObjCantUse.Name = "tplObjCantUse";
            this.tplObjCantUse.RightListCaption = "Доступные варианты";
            this.tplObjCantUse.Size = new System.Drawing.Size(440, 420);
            this.tplObjCantUse.SplitterDistance = 207;
            this.tplObjCantUse.TabIndex = 2;
            this.tplObjCantUse.ValueChanged += new BZEditor.UcTwoPanelsList.ValueChangeEvent(this.TplObjCantUseValueChanged);
            // 
            // tpObjTriggers
            // 
            this.tpObjTriggers.Controls.Add(this.btnAddObjTrigger);
            this.tpObjTriggers.Controls.Add(this.btnObjRemoveTrigger);
            this.tpObjTriggers.Controls.Add(this.lvObjTriggers);
            this.tpObjTriggers.Location = new System.Drawing.Point(4, 40);
            this.tpObjTriggers.Name = "tpObjTriggers";
            this.tpObjTriggers.Size = new System.Drawing.Size(440, 420);
            this.tpObjTriggers.TabIndex = 8;
            this.tpObjTriggers.Text = "Триггеры";
            this.tpObjTriggers.UseVisualStyleBackColor = true;
            // 
            // lvObjTriggers
            // 
            this.lvObjTriggers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvObjTriggers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader18,
            this.columnHeader19});
            this.lvObjTriggers.ContextMenuStrip = this.cmsGridMenu;
            this.lvObjTriggers.FullRowSelect = true;
            this.lvObjTriggers.GridLines = true;
            this.lvObjTriggers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvObjTriggers.HideSelection = false;
            this.lvObjTriggers.Location = new System.Drawing.Point(3, 31);
            this.lvObjTriggers.Name = "lvObjTriggers";
            this.lvObjTriggers.Size = new System.Drawing.Size(434, 387);
            this.lvObjTriggers.TabIndex = 36;
            this.lvObjTriggers.UseCompatibleStateImageBehavior = false;
            this.lvObjTriggers.View = System.Windows.Forms.View.Details;
            this.lvObjTriggers.SizeChanged += new System.EventHandler(this.LastColumnAutosize);
            this.lvObjTriggers.KeyUp += new System.Windows.Forms.KeyEventHandler(this.LvObjTriggersKeyUp);
            this.lvObjTriggers.MouseUp += new System.Windows.Forms.MouseEventHandler(this.NavigatedControl_MouseUp);
            // 
            // columnHeader18
            // 
            this.columnHeader18.Width = 50;
            // 
            // columnHeader19
            // 
            this.columnHeader19.Width = 350;
            // 
            // tpObjAddDescs
            // 
            this.tpObjAddDescs.Controls.Add(this.btnObjReplaceAddDesc);
            this.tpObjAddDescs.Controls.Add(this.rtbObjAddDesc);
            this.tpObjAddDescs.Controls.Add(this.cbMustWordwrapAddDesc);
            this.tpObjAddDescs.Controls.Add(label123);
            this.tpObjAddDescs.Controls.Add(label125);
            this.tpObjAddDescs.Controls.Add(this.btnObjAddAddDesc);
            this.tpObjAddDescs.Controls.Add(this.btnObjRemoveAddDesc);
            this.tpObjAddDescs.Controls.Add(this.lvObjAddDesc);
            this.tpObjAddDescs.Controls.Add(this.tboxAddDescAliases);
            this.tpObjAddDescs.Location = new System.Drawing.Point(4, 40);
            this.tpObjAddDescs.Name = "tpObjAddDescs";
            this.tpObjAddDescs.Size = new System.Drawing.Size(440, 420);
            this.tpObjAddDescs.TabIndex = 2;
            this.tpObjAddDescs.Text = "Доп.описание";
            this.tpObjAddDescs.UseVisualStyleBackColor = true;
            // 
            // rtbObjAddDesc
            // 
            this.rtbObjAddDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbObjAddDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbObjAddDesc.Location = new System.Drawing.Point(3, 57);
            this.rtbObjAddDesc.Name = "rtbObjAddDesc";
            this.rtbObjAddDesc.Size = new System.Drawing.Size(403, 73);
            this.rtbObjAddDesc.TabIndex = 84;
            this.rtbObjAddDesc.Text = "";
            this.rtbObjAddDesc.WordWrap = false;
            // 
            // cbMustWordwrapAddDesc
            // 
            this.cbMustWordwrapAddDesc.AutoSize = true;
            this.cbMustWordwrapAddDesc.Location = new System.Drawing.Point(62, 40);
            this.cbMustWordwrapAddDesc.Name = "cbMustWordwrapAddDesc";
            this.cbMustWordwrapAddDesc.Size = new System.Drawing.Size(311, 17);
            this.cbMustWordwrapAddDesc.TabIndex = 83;
            this.cbMustWordwrapAddDesc.Text = "Переносить текст описания по словам на новую строку";
            this.cbMustWordwrapAddDesc.UseVisualStyleBackColor = true;
            this.cbMustWordwrapAddDesc.Visible = false;
            // 
            // lvObjAddDesc
            // 
            this.lvObjAddDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvObjAddDesc.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader24,
            this.columnHeader25});
            this.lvObjAddDesc.FullRowSelect = true;
            this.lvObjAddDesc.GridLines = true;
            this.lvObjAddDesc.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvObjAddDesc.HideSelection = false;
            this.lvObjAddDesc.Location = new System.Drawing.Point(0, 137);
            this.lvObjAddDesc.Name = "lvObjAddDesc";
            this.lvObjAddDesc.Size = new System.Drawing.Size(437, 280);
            this.lvObjAddDesc.TabIndex = 34;
            this.lvObjAddDesc.UseCompatibleStateImageBehavior = false;
            this.lvObjAddDesc.View = System.Windows.Forms.View.Details;
            this.lvObjAddDesc.SelectedIndexChanged += new System.EventHandler(this.LvObjAddDescSelectedIndexChanged);
            this.lvObjAddDesc.SizeChanged += new System.EventHandler(this.LastColumnAutosize);
            this.lvObjAddDesc.KeyUp += new System.Windows.Forms.KeyEventHandler(this.LvObjAddDescKeyUp);
            // 
            // columnHeader24
            // 
            this.columnHeader24.Text = "Альясы";
            this.columnHeader24.Width = 150;
            // 
            // columnHeader25
            // 
            this.columnHeader25.Text = "Описание";
            this.columnHeader25.Width = 253;
            // 
            // tboxAddDescAliases
            // 
            this.tboxAddDescAliases.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tboxAddDescAliases.Location = new System.Drawing.Point(3, 19);
            this.tboxAddDescAliases.Name = "tboxAddDescAliases";
            this.tboxAddDescAliases.Size = new System.Drawing.Size(434, 20);
            this.tboxAddDescAliases.TabIndex = 82;
            // 
            // tpObjAddAffects
            // 
            this.tpObjAddAffects.Controls.Add(this.splitContainerAddAff);
            this.tpObjAddAffects.Location = new System.Drawing.Point(4, 40);
            this.tpObjAddAffects.Name = "tpObjAddAffects";
            this.tpObjAddAffects.Size = new System.Drawing.Size(440, 420);
            this.tpObjAddAffects.TabIndex = 5;
            this.tpObjAddAffects.Text = "Доп.аффекты";
            this.tpObjAddAffects.UseVisualStyleBackColor = true;
            // 
            // splitContainerAddAff
            // 
            this.splitContainerAddAff.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerAddAff.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerAddAff.Location = new System.Drawing.Point(0, 0);
            this.splitContainerAddAff.Name = "splitContainerAddAff";
            // 
            // splitContainerAddAff.Panel1
            // 
            this.splitContainerAddAff.Panel1.Controls.Add(this.lvObjBonuses);
            // 
            // splitContainerAddAff.Panel2
            // 
            this.splitContainerAddAff.Panel2.Controls.Add(this.lvAvailAddAffects);
            this.splitContainerAddAff.Panel2.Controls.Add(this.toolStripObjAddBonuses);
            this.splitContainerAddAff.Size = new System.Drawing.Size(440, 420);
            this.splitContainerAddAff.SplitterDistance = 205;
            this.splitContainerAddAff.TabIndex = 51;
            // 
            // lvObjBonuses
            // 
            this.lvObjBonuses.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chObjAddAffectPercent,
            this.chObjAddAffectName});
            this.lvObjBonuses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvObjBonuses.FullRowSelect = true;
            this.lvObjBonuses.GridLines = true;
            this.lvObjBonuses.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvObjBonuses.HideSelection = false;
            this.lvObjBonuses.LabelEdit = true;
            this.lvObjBonuses.Location = new System.Drawing.Point(0, 0);
            this.lvObjBonuses.Name = "lvObjBonuses";
            this.lvObjBonuses.Size = new System.Drawing.Size(203, 418);
            this.lvObjBonuses.TabIndex = 42;
            this.lvObjBonuses.UseCompatibleStateImageBehavior = false;
            this.lvObjBonuses.View = System.Windows.Forms.View.Details;
            this.lvObjBonuses.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.LvObjBonusesAfterLabelEdit);
            this.lvObjBonuses.SizeChanged += new System.EventHandler(this.LastColumnAutosize);
            this.lvObjBonuses.KeyUp += new System.Windows.Forms.KeyEventHandler(this.LvObjAdditionalAffectKeyUp);
            this.lvObjBonuses.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LvObjBonusesMouseDoubleClick);
            // 
            // chObjAddAffectPercent
            // 
            this.chObjAddAffectPercent.Text = "%";
            this.chObjAddAffectPercent.Width = 50;
            // 
            // chObjAddAffectName
            // 
            this.chObjAddAffectName.Text = "Доп.аффект";
            this.chObjAddAffectName.Width = 132;
            // 
            // lvAvailAddAffects
            // 
            this.lvAvailAddAffects.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chObjAddAffectAvail});
            this.lvAvailAddAffects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvAvailAddAffects.FullRowSelect = true;
            this.lvAvailAddAffects.HideSelection = false;
            this.lvAvailAddAffects.Location = new System.Drawing.Point(24, 0);
            this.lvAvailAddAffects.Name = "lvAvailAddAffects";
            this.lvAvailAddAffects.Size = new System.Drawing.Size(205, 418);
            this.lvAvailAddAffects.TabIndex = 50;
            this.lvAvailAddAffects.UseCompatibleStateImageBehavior = false;
            this.lvAvailAddAffects.View = System.Windows.Forms.View.Details;
            this.lvAvailAddAffects.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvAvailAddAffects_ColumnClick);
            this.lvAvailAddAffects.SizeChanged += new System.EventHandler(this.LastColumnAutosize);
            this.lvAvailAddAffects.DoubleClick += new System.EventHandler(this.LvAvailAddAffectsDoubleClick);
            // 
            // chObjAddAffectAvail
            // 
            this.chObjAddAffectAvail.Text = "Доступные доп.аффекты";
            this.chObjAddAffectAvail.Width = 175;
            // 
            // toolStripObjAddBonuses
            // 
            this.toolStripObjAddBonuses.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStripObjAddBonuses.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripObjAddBonuses.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbObjAdditAffectAdd,
            this.tsbObjAdditAffectRemove,
            this.toolStripSeparator15,
            this.tsbEditAddAffectValue});
            this.toolStripObjAddBonuses.Location = new System.Drawing.Point(0, 0);
            this.toolStripObjAddBonuses.Name = "toolStripObjAddBonuses";
            this.toolStripObjAddBonuses.Size = new System.Drawing.Size(24, 418);
            this.toolStripObjAddBonuses.TabIndex = 52;
            this.toolStripObjAddBonuses.Text = "toolStrip1";
            // 
            // tsbObjAdditAffectAdd
            // 
            this.tsbObjAdditAffectAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbObjAdditAffectAdd.Image = ((System.Drawing.Image)(resources.GetObject("tsbObjAdditAffectAdd.Image")));
            this.tsbObjAdditAffectAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbObjAdditAffectAdd.Name = "tsbObjAdditAffectAdd";
            this.tsbObjAdditAffectAdd.Size = new System.Drawing.Size(21, 20);
            this.tsbObjAdditAffectAdd.Text = "Добавить дополнительный аффект";
            this.tsbObjAdditAffectAdd.Click += new System.EventHandler(this.TsbObjAdditAffectAddClick);
            // 
            // tsbObjAdditAffectRemove
            // 
            this.tsbObjAdditAffectRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbObjAdditAffectRemove.Image = ((System.Drawing.Image)(resources.GetObject("tsbObjAdditAffectRemove.Image")));
            this.tsbObjAdditAffectRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbObjAdditAffectRemove.Name = "tsbObjAdditAffectRemove";
            this.tsbObjAdditAffectRemove.Size = new System.Drawing.Size(21, 20);
            this.tsbObjAdditAffectRemove.Text = "Убрать дополнительный аффект";
            this.tsbObjAdditAffectRemove.Click += new System.EventHandler(this.TsbObjAdditAffectRemoveClick);
            // 
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Size = new System.Drawing.Size(21, 6);
            // 
            // tsbEditAddAffectValue
            // 
            this.tsbEditAddAffectValue.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbEditAddAffectValue.Image = global::BZEditor.Properties.Resources.button_edit;
            this.tsbEditAddAffectValue.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEditAddAffectValue.Name = "tsbEditAddAffectValue";
            this.tsbEditAddAffectValue.Size = new System.Drawing.Size(21, 20);
            this.tsbEditAddAffectValue.Text = "Изменить числовое значение доб.аффекта";
            this.tsbEditAddAffectValue.Click += new System.EventHandler(this.tsbEditAddAffectValue_Click);
            // 
            // tpObjSkillBonus
            // 
            this.tpObjSkillBonus.Controls.Add(this.splitContainerSkillBonus);
            this.tpObjSkillBonus.Location = new System.Drawing.Point(4, 40);
            this.tpObjSkillBonus.Name = "tpObjSkillBonus";
            this.tpObjSkillBonus.Size = new System.Drawing.Size(440, 420);
            this.tpObjSkillBonus.TabIndex = 10;
            this.tpObjSkillBonus.Text = "Увеличение % умения";
            this.tpObjSkillBonus.UseVisualStyleBackColor = true;
            // 
            // splitContainerSkillBonus
            // 
            this.splitContainerSkillBonus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerSkillBonus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerSkillBonus.Location = new System.Drawing.Point(0, 0);
            this.splitContainerSkillBonus.Name = "splitContainerSkillBonus";
            // 
            // splitContainerSkillBonus.Panel1
            // 
            this.splitContainerSkillBonus.Panel1.Controls.Add(this.lvSkillBonuses);
            // 
            // splitContainerSkillBonus.Panel2
            // 
            this.splitContainerSkillBonus.Panel2.Controls.Add(this.lvAvailSkillBonuses);
            this.splitContainerSkillBonus.Panel2.Controls.Add(this.toolStripObjSkillBonuses);
            this.splitContainerSkillBonus.Size = new System.Drawing.Size(440, 420);
            this.splitContainerSkillBonus.SplitterDistance = 205;
            this.splitContainerSkillBonus.TabIndex = 52;
            // 
            // lvSkillBonuses
            // 
            this.lvSkillBonuses.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chObjAddSkillPercent,
            this.chObjAddSkill});
            this.lvSkillBonuses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvSkillBonuses.FullRowSelect = true;
            this.lvSkillBonuses.GridLines = true;
            this.lvSkillBonuses.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvSkillBonuses.HideSelection = false;
            this.lvSkillBonuses.LabelEdit = true;
            this.lvSkillBonuses.Location = new System.Drawing.Point(0, 0);
            this.lvSkillBonuses.Name = "lvSkillBonuses";
            this.lvSkillBonuses.Size = new System.Drawing.Size(203, 418);
            this.lvSkillBonuses.TabIndex = 42;
            this.lvSkillBonuses.UseCompatibleStateImageBehavior = false;
            this.lvSkillBonuses.View = System.Windows.Forms.View.Details;
            this.lvSkillBonuses.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.LvSkillBonusesAfterLabelEdit);
            this.lvSkillBonuses.SizeChanged += new System.EventHandler(this.LastColumnAutosize);
            this.lvSkillBonuses.KeyUp += new System.Windows.Forms.KeyEventHandler(this.LvSkillBonusesKeyUp);
            this.lvSkillBonuses.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LvSkillBonusesMouseDoubleClick);
            // 
            // chObjAddSkillPercent
            // 
            this.chObjAddSkillPercent.Text = "%";
            this.chObjAddSkillPercent.Width = 50;
            // 
            // chObjAddSkill
            // 
            this.chObjAddSkill.Text = "Умение";
            this.chObjAddSkill.Width = 132;
            // 
            // lvAvailSkillBonuses
            // 
            this.lvAvailSkillBonuses.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chObjAddSkillAvail});
            this.lvAvailSkillBonuses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvAvailSkillBonuses.FullRowSelect = true;
            this.lvAvailSkillBonuses.HideSelection = false;
            this.lvAvailSkillBonuses.Location = new System.Drawing.Point(24, 0);
            this.lvAvailSkillBonuses.Name = "lvAvailSkillBonuses";
            this.lvAvailSkillBonuses.Size = new System.Drawing.Size(205, 418);
            this.lvAvailSkillBonuses.TabIndex = 50;
            this.lvAvailSkillBonuses.UseCompatibleStateImageBehavior = false;
            this.lvAvailSkillBonuses.View = System.Windows.Forms.View.Details;
            this.lvAvailSkillBonuses.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvAvailSkillBonuses_ColumnClick);
            this.lvAvailSkillBonuses.SizeChanged += new System.EventHandler(this.LastColumnAutosize);
            this.lvAvailSkillBonuses.DoubleClick += new System.EventHandler(this.LvAvailSkillBonusesDoubleClick);
            // 
            // chObjAddSkillAvail
            // 
            this.chObjAddSkillAvail.Text = "Доступные умения";
            this.chObjAddSkillAvail.Width = 175;
            // 
            // toolStripObjSkillBonuses
            // 
            this.toolStripObjSkillBonuses.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStripObjSkillBonuses.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripObjSkillBonuses.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAddSkillBonus,
            this.tsbRemoveSkillBonus,
            this.toolStripSeparator16,
            this.tsbEditSkillBonus});
            this.toolStripObjSkillBonuses.Location = new System.Drawing.Point(0, 0);
            this.toolStripObjSkillBonuses.Name = "toolStripObjSkillBonuses";
            this.toolStripObjSkillBonuses.Size = new System.Drawing.Size(24, 418);
            this.toolStripObjSkillBonuses.TabIndex = 52;
            // 
            // tsbAddSkillBonus
            // 
            this.tsbAddSkillBonus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddSkillBonus.Image = ((System.Drawing.Image)(resources.GetObject("tsbAddSkillBonus.Image")));
            this.tsbAddSkillBonus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddSkillBonus.Name = "tsbAddSkillBonus";
            this.tsbAddSkillBonus.Size = new System.Drawing.Size(21, 20);
            this.tsbAddSkillBonus.Text = "Добавить умение";
            this.tsbAddSkillBonus.Click += new System.EventHandler(this.TsbAddSkillBonusClick);
            // 
            // tsbRemoveSkillBonus
            // 
            this.tsbRemoveSkillBonus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRemoveSkillBonus.Image = ((System.Drawing.Image)(resources.GetObject("tsbRemoveSkillBonus.Image")));
            this.tsbRemoveSkillBonus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRemoveSkillBonus.Name = "tsbRemoveSkillBonus";
            this.tsbRemoveSkillBonus.Size = new System.Drawing.Size(21, 20);
            this.tsbRemoveSkillBonus.Text = "Убрать умение";
            this.tsbRemoveSkillBonus.Click += new System.EventHandler(this.TsbRemoveSkillBonusClick);
            // 
            // toolStripSeparator16
            // 
            this.toolStripSeparator16.Name = "toolStripSeparator16";
            this.toolStripSeparator16.Size = new System.Drawing.Size(21, 6);
            // 
            // tsbEditSkillBonus
            // 
            this.tsbEditSkillBonus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbEditSkillBonus.Image = global::BZEditor.Properties.Resources.button_edit;
            this.tsbEditSkillBonus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEditSkillBonus.Name = "tsbEditSkillBonus";
            this.tsbEditSkillBonus.Size = new System.Drawing.Size(21, 20);
            this.tsbEditSkillBonus.Text = "Изменить числовое значение бонуса";
            this.tsbEditSkillBonus.Click += new System.EventHandler(this.tsbEditSkillBonus_Click);
            // 
            // tpMobs
            // 
            this.tpMobs.BackColor = System.Drawing.Color.Transparent;
            this.tpMobs.Controls.Add(this.splitContainer1);
            this.tpMobs.Location = new System.Drawing.Point(4, 23);
            this.tpMobs.Name = "tpMobs";
            this.tpMobs.Padding = new System.Windows.Forms.Padding(3);
            this.tpMobs.Size = new System.Drawing.Size(819, 472);
            this.tpMobs.TabIndex = 0;
            this.tpMobs.Text = "Мобы";
            this.tpMobs.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainerMob);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnlAddMobDesc);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Controls.Add(this.label67);
            this.splitContainer1.Size = new System.Drawing.Size(813, 466);
            this.splitContainer1.SplitterDistance = 369;
            this.splitContainer1.TabIndex = 10;
            // 
            // splitContainerMob
            // 
            this.splitContainerMob.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerMob.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMob.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerMob.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMob.Name = "splitContainerMob";
            // 
            // splitContainerMob.Panel1
            // 
            this.splitContainerMob.Panel1.AutoScroll = true;
            this.splitContainerMob.Panel1.Controls.Add(this.btnMobSetAutoCases);
            this.splitContainerMob.Panel1.Controls.Add(this.tboxMobNameTvor);
            this.splitContainerMob.Panel1.Controls.Add(this.cboxMobRace);
            this.splitContainerMob.Panel1.Controls.Add(this.cboxMobClass);
            this.splitContainerMob.Panel1.Controls.Add(this.cboxMobSex);
            this.splitContainerMob.Panel1.Controls.Add(label127);
            this.splitContainerMob.Panel1.Controls.Add(this.tboxMobAliases);
            this.splitContainerMob.Panel1.Controls.Add(label170);
            this.splitContainerMob.Panel1.Controls.Add(label27);
            this.splitContainerMob.Panel1.Controls.Add(label1);
            this.splitContainerMob.Panel1.Controls.Add(this.tboxMobNameVin);
            this.splitContainerMob.Panel1.Controls.Add(this.tboxMobNameDat);
            this.splitContainerMob.Panel1.Controls.Add(this.tboxMobNameRod);
            this.splitContainerMob.Panel1.Controls.Add(this.tboxMobNameImen);
            this.splitContainerMob.Panel1.Controls.Add(this.tboxMobNamePred);
            this.splitContainerMob.Panel1.Controls.Add(this.tboxMobDesc);
            this.splitContainerMob.Panel1.Controls.Add(label31);
            this.splitContainerMob.Panel1.Controls.Add(label24);
            this.splitContainerMob.Panel1.Controls.Add(label25);
            this.splitContainerMob.Panel1.Controls.Add(label26);
            this.splitContainerMob.Panel1.Controls.Add(label32);
            this.splitContainerMob.Panel1.Controls.Add(label59);
            this.splitContainerMob.Panel1.Controls.Add(label60);
            this.splitContainerMob.Panel1.Controls.Add(this.cborMobRemoveOnReload);
            this.splitContainerMob.Panel1.Padding = new System.Windows.Forms.Padding(3);
            // 
            // splitContainerMob.Panel2
            // 
            this.splitContainerMob.Panel2.Controls.Add(this.tcMobs);
            this.splitContainerMob.Size = new System.Drawing.Size(813, 369);
            this.splitContainerMob.SplitterDistance = 340;
            this.splitContainerMob.TabIndex = 9;
            // 
            // cboxMobRace
            // 
            this.cboxMobRace.DropDownWidth = 150;
            this.cboxMobRace.ItemHeight = 13;
            this.cboxMobRace.Location = new System.Drawing.Point(218, 25);
            this.cboxMobRace.Name = "cboxMobRace";
            this.cboxMobRace.Size = new System.Drawing.Size(118, 21);
            this.cboxMobRace.TabIndex = 4;
            this.cboxMobRace.SelectedValueChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // cboxMobClass
            // 
            this.cboxMobClass.DropDownWidth = 150;
            this.cboxMobClass.ItemHeight = 13;
            this.cboxMobClass.Location = new System.Drawing.Point(113, 25);
            this.cboxMobClass.Name = "cboxMobClass";
            this.cboxMobClass.Size = new System.Drawing.Size(82, 21);
            this.cboxMobClass.TabIndex = 3;
            this.cboxMobClass.SelectedValueChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // cborMobRemoveOnReload
            // 
            this.cborMobRemoveOnReload.AutoSize = true;
            this.cborMobRemoveOnReload.ForeColor = System.Drawing.Color.Maroon;
            this.errorProvider.SetIconPadding(this.cborMobRemoveOnReload, 1);
            this.cborMobRemoveOnReload.Location = new System.Drawing.Point(3, 2);
            this.cborMobRemoveOnReload.Name = "cborMobRemoveOnReload";
            this.cborMobRemoveOnReload.Size = new System.Drawing.Size(192, 17);
            this.cborMobRemoveOnReload.TabIndex = 0;
            this.cborMobRemoveOnReload.Text = "Удалять моба при перезагрузке";
            this.cborMobRemoveOnReload.UseVisualStyleBackColor = true;
            this.cborMobRemoveOnReload.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cborMobRemoveOnReload_MouseClick);
            // 
            // tcMobs
            // 
            this.tcMobs.Controls.Add(this.tpMobParameters);
            this.tcMobs.Controls.Add(this.tpMobSkills);
            this.tcMobs.Controls.Add(this.tpMobSpells);
            this.tcMobs.Controls.Add(this.tpMobFeatures);
            this.tcMobs.Controls.Add(this.tpMobAffects);
            this.tcMobs.Controls.Add(this.tpMobFlags);
            this.tcMobs.Controls.Add(this.tpMobSpecFlags);
            this.tcMobs.Controls.Add(this.tpMobHelpers);
            this.tcMobs.Controls.Add(this.tpMobTriggers);
            this.tcMobs.Controls.Add(this.tpMobResists);
            this.tcMobs.Controls.Add(this.tpMobRoles);
            this.tcMobs.Controls.Add(this.tpMobIngredients);
            this.tcMobs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMobs.ItemSize = new System.Drawing.Size(54, 18);
            this.tcMobs.Location = new System.Drawing.Point(0, 0);
            this.tcMobs.Multiline = true;
            this.tcMobs.Name = "tcMobs";
            this.tcMobs.SelectedIndex = 0;
            this.tcMobs.Size = new System.Drawing.Size(467, 367);
            this.tcMobs.TabIndex = 8;
            this.tcMobs.SelectedIndexChanged += new System.EventHandler(this.tcMobs_SelectedIndexChanged);
            // 
            // tpMobParameters
            // 
            this.tpMobParameters.AutoScroll = true;
            this.tpMobParameters.Controls.Add(label11);
            this.tpMobParameters.Controls.Add(label4);
            this.tpMobParameters.Controls.Add(this.nudMobHitroll);
            this.tpMobParameters.Controls.Add(label3);
            this.tpMobParameters.Controls.Add(this.nudMobAC);
            this.tpMobParameters.Controls.Add(this.nudMobMaxInWorld);
            this.tpMobParameters.Controls.Add(this.cboxMobDefPosition);
            this.tpMobParameters.Controls.Add(this.cboxMobStartPosition);
            this.tpMobParameters.Controls.Add(this.dctrlMobHP);
            this.tpMobParameters.Controls.Add(this.dctrlMobAttack);
            this.tpMobParameters.Controls.Add(this.nudMobWeight);
            this.tpMobParameters.Controls.Add(this.nudMobMaxFactor);
            this.tpMobParameters.Controls.Add(this.nudMobLikeWork);
            this.tpMobParameters.Controls.Add(this.nudMobSize);
            this.tpMobParameters.Controls.Add(this.nudMobExpa);
            this.tpMobParameters.Controls.Add(this.nudMobCha);
            this.tpMobParameters.Controls.Add(this.btnSelectMobPath);
            this.tpMobParameters.Controls.Add(this.nudMobDex);
            this.tpMobParameters.Controls.Add(this.tboxMobDestination);
            this.tpMobParameters.Controls.Add(label23);
            this.tpMobParameters.Controls.Add(this.nudMobInt);
            this.tpMobParameters.Controls.Add(this.nudMobHeight);
            this.tpMobParameters.Controls.Add(this.nudMobExtraAttack);
            this.tpMobParameters.Controls.Add(label2);
            this.tpMobParameters.Controls.Add(this.nudMobLevel);
            this.tpMobParameters.Controls.Add(this.nudMobCon);
            this.tpMobParameters.Controls.Add(label19);
            this.tpMobParameters.Controls.Add(this.nudMobWis);
            this.tpMobParameters.Controls.Add(this.cboxMobAlign);
            this.tpMobParameters.Controls.Add(this.label34);
            this.tpMobParameters.Controls.Add(this.nudMobStr);
            this.tpMobParameters.Controls.Add(label14);
            this.tpMobParameters.Controls.Add(label21);
            this.tpMobParameters.Controls.Add(this.cboxMobAttackType);
            this.tpMobParameters.Controls.Add(label20);
            this.tpMobParameters.Controls.Add(label22);
            this.tpMobParameters.Controls.Add(this.dctrlMobMoney);
            this.tpMobParameters.Controls.Add(label10);
            this.tpMobParameters.Controls.Add(label6);
            this.tpMobParameters.Controls.Add(label9);
            this.tpMobParameters.Controls.Add(label18);
            this.tpMobParameters.Controls.Add(label17);
            this.tpMobParameters.Controls.Add(label8);
            this.tpMobParameters.Controls.Add(label7);
            this.tpMobParameters.Controls.Add(label16);
            this.tpMobParameters.Controls.Add(label12);
            this.tpMobParameters.Controls.Add(label13);
            this.tpMobParameters.Controls.Add(label5);
            this.tpMobParameters.Location = new System.Drawing.Point(4, 40);
            this.tpMobParameters.Name = "tpMobParameters";
            this.tpMobParameters.Padding = new System.Windows.Forms.Padding(3);
            this.tpMobParameters.Size = new System.Drawing.Size(459, 323);
            this.tpMobParameters.TabIndex = 9;
            this.tpMobParameters.Text = "Параметры";
            this.tpMobParameters.UseVisualStyleBackColor = true;
            // 
            // cboxMobDefPosition
            // 
            this.cboxMobDefPosition.DropDownWidth = 52;
            this.cboxMobDefPosition.ItemHeight = 13;
            this.cboxMobDefPosition.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50"});
            this.cboxMobDefPosition.Location = new System.Drawing.Point(201, 210);
            this.cboxMobDefPosition.Name = "cboxMobDefPosition";
            this.cboxMobDefPosition.Size = new System.Drawing.Size(199, 21);
            this.cboxMobDefPosition.TabIndex = 21;
            this.cboxMobDefPosition.SelectedIndexChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // cboxMobStartPosition
            // 
            this.cboxMobStartPosition.DropDownWidth = 52;
            this.cboxMobStartPosition.ItemHeight = 13;
            this.cboxMobStartPosition.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50"});
            this.cboxMobStartPosition.Location = new System.Drawing.Point(5, 210);
            this.cboxMobStartPosition.Name = "cboxMobStartPosition";
            this.cboxMobStartPosition.Size = new System.Drawing.Size(191, 21);
            this.cboxMobStartPosition.TabIndex = 20;
            this.cboxMobStartPosition.SelectedIndexChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // nudMobMaxFactor
            // 
            this.nudMobMaxFactor.Location = new System.Drawing.Point(118, 295);
            this.nudMobMaxFactor.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudMobMaxFactor.Name = "nudMobMaxFactor";
            this.nudMobMaxFactor.Size = new System.Drawing.Size(82, 20);
            this.nudMobMaxFactor.TabIndex = 24;
            this.nudMobMaxFactor.ValueChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // nudMobLikeWork
            // 
            this.nudMobLikeWork.Location = new System.Drawing.Point(349, 169);
            this.nudMobLikeWork.Name = "nudMobLikeWork";
            this.nudMobLikeWork.Size = new System.Drawing.Size(51, 20);
            this.nudMobLikeWork.TabIndex = 19;
            this.nudMobLikeWork.ValueChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // nudMobCha
            // 
            this.nudMobCha.Location = new System.Drawing.Point(286, 20);
            this.nudMobCha.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudMobCha.Name = "nudMobCha";
            this.nudMobCha.Size = new System.Drawing.Size(52, 20);
            this.nudMobCha.TabIndex = 6;
            this.nudMobCha.ValueChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // nudMobDex
            // 
            this.nudMobDex.Location = new System.Drawing.Point(174, 20);
            this.nudMobDex.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudMobDex.Name = "nudMobDex";
            this.nudMobDex.Size = new System.Drawing.Size(52, 20);
            this.nudMobDex.TabIndex = 4;
            this.nudMobDex.ValueChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // nudMobInt
            // 
            this.nudMobInt.Location = new System.Drawing.Point(62, 20);
            this.nudMobInt.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudMobInt.Name = "nudMobInt";
            this.nudMobInt.Size = new System.Drawing.Size(52, 20);
            this.nudMobInt.TabIndex = 2;
            this.nudMobInt.ValueChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // nudMobExtraAttack
            // 
            this.nudMobExtraAttack.Location = new System.Drawing.Point(118, 171);
            this.nudMobExtraAttack.Name = "nudMobExtraAttack";
            this.nudMobExtraAttack.Size = new System.Drawing.Size(47, 20);
            this.nudMobExtraAttack.TabIndex = 18;
            this.nudMobExtraAttack.ValueChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // nudMobCon
            // 
            this.nudMobCon.Location = new System.Drawing.Point(230, 20);
            this.nudMobCon.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudMobCon.Name = "nudMobCon";
            this.nudMobCon.Size = new System.Drawing.Size(52, 20);
            this.nudMobCon.TabIndex = 5;
            this.nudMobCon.ValueChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // nudMobWis
            // 
            this.nudMobWis.Location = new System.Drawing.Point(118, 20);
            this.nudMobWis.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudMobWis.Name = "nudMobWis";
            this.nudMobWis.Size = new System.Drawing.Size(52, 20);
            this.nudMobWis.TabIndex = 3;
            this.nudMobWis.ValueChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // label34
            // 
            this.label34.Location = new System.Drawing.Point(2, 237);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(256, 14);
            this.label34.TabIndex = 15;
            this.label34.Text = "Список комнат по которым передвигается моб";
            this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudMobStr
            // 
            this.nudMobStr.Location = new System.Drawing.Point(6, 20);
            this.nudMobStr.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudMobStr.Name = "nudMobStr";
            this.nudMobStr.Size = new System.Drawing.Size(52, 20);
            this.nudMobStr.TabIndex = 1;
            this.nudMobStr.ValueChanged += new System.EventHandler(this.MobValueChanged);
            // 
            // dctrlMobMoney
            // 
            this.dctrlMobMoney.LabelText = "Деньги";
            this.dctrlMobMoney.Location = new System.Drawing.Point(206, 279);
            this.dctrlMobMoney.MinRandomValue = 0;
            this.dctrlMobMoney.Name = "dctrlMobMoney";
            this.dctrlMobMoney.Param1 = 0;
            this.dctrlMobMoney.Param1Max = new decimal(new int[] {
            276447231,
            23283,
            0,
            0});
            this.dctrlMobMoney.Param2 = 0;
            this.dctrlMobMoney.Param2Max = new decimal(new int[] {
            1661992959,
            1808227885,
            5,
            0});
            this.dctrlMobMoney.ParamConst = 0;
            this.dctrlMobMoney.ParamConstMax = new decimal(new int[] {
            1874919423,
            2328306,
            0,
            0});
            this.dctrlMobMoney.SignFixed = true;
            this.dctrlMobMoney.Size = new System.Drawing.Size(194, 42);
            this.dctrlMobMoney.TabIndex = 25;
            this.dctrlMobMoney.Value = "0d0+0";
            this.dctrlMobMoney.ValueChanged += new BZEditor.UcDiceControl.ValueChangeEvent(this.MobValueChanged);
            // 
            // tpMobSkills
            // 
            this.tpMobSkills.Controls.Add(this.splitContainerMobSkills);
            this.tpMobSkills.Location = new System.Drawing.Point(4, 40);
            this.tpMobSkills.Name = "tpMobSkills";
            this.tpMobSkills.Size = new System.Drawing.Size(459, 323);
            this.tpMobSkills.TabIndex = 5;
            this.tpMobSkills.Text = "Умения";
            this.tpMobSkills.UseVisualStyleBackColor = true;
            // 
            // splitContainerMobSkills
            // 
            this.splitContainerMobSkills.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerMobSkills.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMobSkills.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMobSkills.Name = "splitContainerMobSkills";
            // 
            // splitContainerMobSkills.Panel1
            // 
            this.splitContainerMobSkills.Panel1.Controls.Add(this.lvMobSkills);
            // 
            // splitContainerMobSkills.Panel2
            // 
            this.splitContainerMobSkills.Panel2.Controls.Add(this.lvAvailMobSkills);
            this.splitContainerMobSkills.Panel2.Controls.Add(this.toolStripMobSkills);
            this.splitContainerMobSkills.Size = new System.Drawing.Size(459, 323);
            this.splitContainerMobSkills.SplitterDistance = 213;
            this.splitContainerMobSkills.TabIndex = 52;
            // 
            // lvMobSkills
            // 
            this.lvMobSkills.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chMobSkillPercent,
            this.chMobSkillName});
            this.lvMobSkills.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvMobSkills.FullRowSelect = true;
            this.lvMobSkills.GridLines = true;
            this.lvMobSkills.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvMobSkills.HideSelection = false;
            this.lvMobSkills.LabelEdit = true;
            this.lvMobSkills.Location = new System.Drawing.Point(0, 0);
            this.lvMobSkills.Name = "lvMobSkills";
            this.lvMobSkills.Size = new System.Drawing.Size(211, 321);
            this.lvMobSkills.TabIndex = 20;
            this.lvMobSkills.UseCompatibleStateImageBehavior = false;
            this.lvMobSkills.View = System.Windows.Forms.View.Details;
            this.lvMobSkills.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.lvMobSkills_AfterLabelEdit);
            this.lvMobSkills.SizeChanged += new System.EventHandler(this.LastColumnAutosize);
            this.lvMobSkills.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lvMobSkills_KeyUp);
            this.lvMobSkills.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvMobSkills_MouseDoubleClick);
            this.lvMobSkills.MouseEnter += new System.EventHandler(this.FocusToControl);
            // 
            // chMobSkillPercent
            // 
            this.chMobSkillPercent.Text = "%";
            this.chMobSkillPercent.Width = 50;
            // 
            // chMobSkillName
            // 
            this.chMobSkillName.Text = "Умение";
            this.chMobSkillName.Width = 124;
            // 
            // lvAvailMobSkills
            // 
            this.lvAvailMobSkills.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chMobSkillAvail});
            this.lvAvailMobSkills.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvAvailMobSkills.FullRowSelect = true;
            this.lvAvailMobSkills.HideSelection = false;
            this.lvAvailMobSkills.Location = new System.Drawing.Point(24, 0);
            this.lvAvailMobSkills.Name = "lvAvailMobSkills";
            this.lvAvailMobSkills.Size = new System.Drawing.Size(216, 321);
            this.lvAvailMobSkills.TabIndex = 50;
            this.lvAvailMobSkills.UseCompatibleStateImageBehavior = false;
            this.lvAvailMobSkills.View = System.Windows.Forms.View.Details;
            this.lvAvailMobSkills.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvAvailMobSkills_ColumnClick);
            this.lvAvailMobSkills.SizeChanged += new System.EventHandler(this.LastColumnAutosize);
            this.lvAvailMobSkills.DoubleClick += new System.EventHandler(this.lvAvailMobSkills_DoubleClick);
            // 
            // chMobSkillAvail
            // 
            this.chMobSkillAvail.Text = "Доступные умения";
            this.chMobSkillAvail.Width = 175;
            // 
            // toolStripMobSkills
            // 
            this.toolStripMobSkills.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStripMobSkills.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripMobSkills.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbMobAddSkill,
            this.tsbMobRemoveSkill,
            this.toolStripSeparator18,
            this.tsbMobEditSkill});
            this.toolStripMobSkills.Location = new System.Drawing.Point(0, 0);
            this.toolStripMobSkills.Name = "toolStripMobSkills";
            this.toolStripMobSkills.Size = new System.Drawing.Size(24, 321);
            this.toolStripMobSkills.TabIndex = 52;
            this.toolStripMobSkills.Text = "toolStrip1";
            // 
            // tsbMobAddSkill
            // 
            this.tsbMobAddSkill.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMobAddSkill.Image = ((System.Drawing.Image)(resources.GetObject("tsbMobAddSkill.Image")));
            this.tsbMobAddSkill.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMobAddSkill.Name = "tsbMobAddSkill";
            this.tsbMobAddSkill.Size = new System.Drawing.Size(21, 20);
            this.tsbMobAddSkill.Text = "Добавить выбранное";
            this.tsbMobAddSkill.Click += new System.EventHandler(this.tsbMobAddSkill_Click);
            // 
            // tsbMobRemoveSkill
            // 
            this.tsbMobRemoveSkill.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMobRemoveSkill.Image = ((System.Drawing.Image)(resources.GetObject("tsbMobRemoveSkill.Image")));
            this.tsbMobRemoveSkill.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMobRemoveSkill.Name = "tsbMobRemoveSkill";
            this.tsbMobRemoveSkill.Size = new System.Drawing.Size(21, 20);
            this.tsbMobRemoveSkill.Text = "Удалить выбранное";
            this.tsbMobRemoveSkill.Click += new System.EventHandler(this.tsbMobRemoveSkill_Click);
            // 
            // toolStripSeparator18
            // 
            this.toolStripSeparator18.Name = "toolStripSeparator18";
            this.toolStripSeparator18.Size = new System.Drawing.Size(21, 6);
            // 
            // tsbMobEditSkill
            // 
            this.tsbMobEditSkill.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMobEditSkill.Image = global::BZEditor.Properties.Resources.button_edit;
            this.tsbMobEditSkill.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMobEditSkill.Name = "tsbMobEditSkill";
            this.tsbMobEditSkill.Size = new System.Drawing.Size(21, 20);
            this.tsbMobEditSkill.Text = "Изменить значение";
            this.tsbMobEditSkill.Click += new System.EventHandler(this.tsbMobEditSkill_Click);
            // 
            // tpMobSpells
            // 
            this.tpMobSpells.Controls.Add(this.splitContainerMobSpells);
            this.tpMobSpells.Location = new System.Drawing.Point(4, 40);
            this.tpMobSpells.Name = "tpMobSpells";
            this.tpMobSpells.Size = new System.Drawing.Size(459, 323);
            this.tpMobSpells.TabIndex = 4;
            this.tpMobSpells.Text = "Заклинания";
            this.tpMobSpells.UseVisualStyleBackColor = true;
            // 
            // splitContainerMobSpells
            // 
            this.splitContainerMobSpells.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerMobSpells.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMobSpells.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMobSpells.Name = "splitContainerMobSpells";
            // 
            // splitContainerMobSpells.Panel1
            // 
            this.splitContainerMobSpells.Panel1.Controls.Add(this.lvMobSpells);
            // 
            // splitContainerMobSpells.Panel2
            // 
            this.splitContainerMobSpells.Panel2.Controls.Add(this.lvMobAvailSpells);
            this.splitContainerMobSpells.Panel2.Controls.Add(this.toolStripMobSpells);
            this.splitContainerMobSpells.Size = new System.Drawing.Size(459, 323);
            this.splitContainerMobSpells.SplitterDistance = 213;
            this.splitContainerMobSpells.TabIndex = 53;
            // 
            // lvMobSpells
            // 
            this.lvMobSpells.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chMobSpellCnt,
            this.chMobSpellName});
            this.lvMobSpells.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvMobSpells.FullRowSelect = true;
            this.lvMobSpells.GridLines = true;
            this.lvMobSpells.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvMobSpells.HideSelection = false;
            this.lvMobSpells.LabelEdit = true;
            this.lvMobSpells.Location = new System.Drawing.Point(0, 0);
            this.lvMobSpells.Name = "lvMobSpells";
            this.lvMobSpells.Size = new System.Drawing.Size(211, 321);
            this.lvMobSpells.TabIndex = 1;
            this.lvMobSpells.UseCompatibleStateImageBehavior = false;
            this.lvMobSpells.View = System.Windows.Forms.View.Details;
            this.lvMobSpells.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.lvMobSpells_AfterLabelEdit);
            this.lvMobSpells.SizeChanged += new System.EventHandler(this.LastColumnAutosize);
            this.lvMobSpells.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lvMobSpells_KeyUp);
            this.lvMobSpells.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvMobSpells_MouseDoubleClick);
            this.lvMobSpells.MouseEnter += new System.EventHandler(this.FocusToControl);
            // 
            // chMobSpellCnt
            // 
            this.chMobSpellCnt.Text = "Кол-во";
            this.chMobSpellCnt.Width = 50;
            // 
            // chMobSpellName
            // 
            this.chMobSpellName.Text = "Заклинание";
            this.chMobSpellName.Width = 124;
            // 
            // lvMobAvailSpells
            // 
            this.lvMobAvailSpells.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chAvailMobSpellName});
            this.lvMobAvailSpells.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvMobAvailSpells.FullRowSelect = true;
            this.lvMobAvailSpells.HideSelection = false;
            this.lvMobAvailSpells.Location = new System.Drawing.Point(24, 0);
            this.lvMobAvailSpells.Name = "lvMobAvailSpells";
            this.lvMobAvailSpells.Size = new System.Drawing.Size(216, 321);
            this.lvMobAvailSpells.TabIndex = 50;
            this.lvMobAvailSpells.UseCompatibleStateImageBehavior = false;
            this.lvMobAvailSpells.View = System.Windows.Forms.View.Details;
            this.lvMobAvailSpells.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvMobAvailSpells_ColumnClick);
            this.lvMobAvailSpells.SizeChanged += new System.EventHandler(this.LastColumnAutosize);
            this.lvMobAvailSpells.DoubleClick += new System.EventHandler(this.lvMobAvailSpells_DoubleClick);
            // 
            // chAvailMobSpellName
            // 
            this.chAvailMobSpellName.Text = "Заклинание";
            this.chAvailMobSpellName.Width = 175;
            // 
            // toolStripMobSpells
            // 
            this.toolStripMobSpells.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStripMobSpells.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripMobSpells.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbMobAddSpell,
            this.tsbMobRemoveSpell,
            this.toolStripSeparator19,
            this.tsbMobEditSpell});
            this.toolStripMobSpells.Location = new System.Drawing.Point(0, 0);
            this.toolStripMobSpells.Name = "toolStripMobSpells";
            this.toolStripMobSpells.Size = new System.Drawing.Size(24, 321);
            this.toolStripMobSpells.TabIndex = 52;
            this.toolStripMobSpells.Text = "toolStrip1";
            // 
            // tsbMobAddSpell
            // 
            this.tsbMobAddSpell.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMobAddSpell.Image = ((System.Drawing.Image)(resources.GetObject("tsbMobAddSpell.Image")));
            this.tsbMobAddSpell.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMobAddSpell.Name = "tsbMobAddSpell";
            this.tsbMobAddSpell.Size = new System.Drawing.Size(21, 20);
            this.tsbMobAddSpell.Text = "Добавить выбранное";
            this.tsbMobAddSpell.Click += new System.EventHandler(this.tsbMobAddSpell_Click);
            // 
            // tsbMobRemoveSpell
            // 
            this.tsbMobRemoveSpell.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMobRemoveSpell.Image = ((System.Drawing.Image)(resources.GetObject("tsbMobRemoveSpell.Image")));
            this.tsbMobRemoveSpell.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMobRemoveSpell.Name = "tsbMobRemoveSpell";
            this.tsbMobRemoveSpell.Size = new System.Drawing.Size(21, 20);
            this.tsbMobRemoveSpell.Text = "Удалить выбранное";
            this.tsbMobRemoveSpell.Click += new System.EventHandler(this.tsbMobRemoveSpell_Click);
            // 
            // toolStripSeparator19
            // 
            this.toolStripSeparator19.Name = "toolStripSeparator19";
            this.toolStripSeparator19.Size = new System.Drawing.Size(21, 6);
            // 
            // tsbMobEditSpell
            // 
            this.tsbMobEditSpell.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMobEditSpell.Image = global::BZEditor.Properties.Resources.button_edit;
            this.tsbMobEditSpell.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMobEditSpell.Name = "tsbMobEditSpell";
            this.tsbMobEditSpell.Size = new System.Drawing.Size(21, 20);
            this.tsbMobEditSpell.Text = "Изменить значение";
            this.tsbMobEditSpell.Click += new System.EventHandler(this.tsbMobEditSpell_Click);
            // 
            // tpMobFeatures
            // 
            this.tpMobFeatures.Controls.Add(this.tplMobFeats);
            this.tpMobFeatures.Location = new System.Drawing.Point(4, 40);
            this.tpMobFeatures.Name = "tpMobFeatures";
            this.tpMobFeatures.Size = new System.Drawing.Size(459, 323);
            this.tpMobFeatures.TabIndex = 10;
            this.tpMobFeatures.Text = "Фиты";
            this.tpMobFeatures.UseVisualStyleBackColor = true;
            // 
            // tplMobFeats
            // 
            this.tplMobFeats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tplMobFeats.Grouped = false;
            this.tplMobFeats.LeftListCaption = "Фиты моба";
            this.tplMobFeats.Location = new System.Drawing.Point(0, 0);
            this.tplMobFeats.Name = "tplMobFeats";
            this.tplMobFeats.RightListCaption = "Доступные фиты";
            this.tplMobFeats.Size = new System.Drawing.Size(459, 323);
            this.tplMobFeats.SplitterDistance = 216;
            this.tplMobFeats.TabIndex = 0;
            this.tplMobFeats.ValueChanged += new BZEditor.UcTwoPanelsList.ValueChangeEvent(this.tplMobFeats_ValueChanged);
            // 
            // tpMobAffects
            // 
            this.tpMobAffects.Controls.Add(this.tplMobAffects);
            this.tpMobAffects.Location = new System.Drawing.Point(4, 40);
            this.tpMobAffects.Name = "tpMobAffects";
            this.tpMobAffects.Size = new System.Drawing.Size(459, 323);
            this.tpMobAffects.TabIndex = 3;
            this.tpMobAffects.Text = "Аффекты";
            this.tpMobAffects.UseVisualStyleBackColor = true;
            // 
            // tplMobAffects
            // 
            this.tplMobAffects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tplMobAffects.Grouped = false;
            this.tplMobAffects.LeftListCaption = "Аффекты моба";
            this.tplMobAffects.Location = new System.Drawing.Point(0, 0);
            this.tplMobAffects.Name = "tplMobAffects";
            this.tplMobAffects.RightListCaption = "Доступные аффекты";
            this.tplMobAffects.Size = new System.Drawing.Size(459, 323);
            this.tplMobAffects.SplitterDistance = 216;
            this.tplMobAffects.TabIndex = 0;
            this.tplMobAffects.ValueChanged += new BZEditor.UcTwoPanelsList.ValueChangeEvent(this.tplMobAffects_ValueChanged);
            // 
            // tpMobFlags
            // 
            this.tpMobFlags.Controls.Add(this.tplMobFlags);
            this.tpMobFlags.Location = new System.Drawing.Point(4, 40);
            this.tpMobFlags.Name = "tpMobFlags";
            this.tpMobFlags.Size = new System.Drawing.Size(459, 323);
            this.tpMobFlags.TabIndex = 0;
            this.tpMobFlags.Text = "Флаги";
            this.tpMobFlags.UseVisualStyleBackColor = true;
            // 
            // tplMobFlags
            // 
            this.tplMobFlags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tplMobFlags.Grouped = false;
            this.tplMobFlags.LeftListCaption = "Флаги моба";
            this.tplMobFlags.Location = new System.Drawing.Point(0, 0);
            this.tplMobFlags.Name = "tplMobFlags";
            this.tplMobFlags.RightListCaption = "Доступные флаги";
            this.tplMobFlags.Size = new System.Drawing.Size(459, 323);
            this.tplMobFlags.SplitterDistance = 216;
            this.tplMobFlags.TabIndex = 0;
            this.tplMobFlags.ValueChanged += new BZEditor.UcTwoPanelsList.ValueChangeEvent(this.tplMobFlags_ValueChanged);
            // 
            // tpMobSpecFlags
            // 
            this.tpMobSpecFlags.Controls.Add(this.tplMobSpecFlags);
            this.tpMobSpecFlags.Location = new System.Drawing.Point(4, 40);
            this.tpMobSpecFlags.Name = "tpMobSpecFlags";
            this.tpMobSpecFlags.Size = new System.Drawing.Size(459, 323);
            this.tpMobSpecFlags.TabIndex = 7;
            this.tpMobSpecFlags.Text = "Спец.флаги";
            this.tpMobSpecFlags.UseVisualStyleBackColor = true;
            // 
            // tplMobSpecFlags
            // 
            this.tplMobSpecFlags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tplMobSpecFlags.Grouped = false;
            this.tplMobSpecFlags.LeftListCaption = "Спецфлаги моба";
            this.tplMobSpecFlags.Location = new System.Drawing.Point(0, 0);
            this.tplMobSpecFlags.Name = "tplMobSpecFlags";
            this.tplMobSpecFlags.RightListCaption = "Доступные спецфлаги";
            this.tplMobSpecFlags.Size = new System.Drawing.Size(459, 323);
            this.tplMobSpecFlags.SplitterDistance = 216;
            this.tplMobSpecFlags.TabIndex = 0;
            this.tplMobSpecFlags.ValueChanged += new BZEditor.UcTwoPanelsList.ValueChangeEvent(this.tplMobSpecFlags_ValueChanged);
            // 
            // tpMobHelpers
            // 
            this.tpMobHelpers.Controls.Add(this.btnMobAddHelper);
            this.tpMobHelpers.Controls.Add(this.btnRemoveHelpersList);
            this.tpMobHelpers.Controls.Add(this.lvMobHelpers);
            this.tpMobHelpers.Location = new System.Drawing.Point(4, 40);
            this.tpMobHelpers.Name = "tpMobHelpers";
            this.tpMobHelpers.Size = new System.Drawing.Size(459, 323);
            this.tpMobHelpers.TabIndex = 2;
            this.tpMobHelpers.Text = "Помощники";
            this.tpMobHelpers.UseVisualStyleBackColor = true;
            // 
            // lvMobHelpers
            // 
            this.lvMobHelpers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvMobHelpers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chMobHelperVNum,
            this.chMobHelperName});
            this.lvMobHelpers.ContextMenuStrip = this.cmsGridMenu;
            this.lvMobHelpers.FullRowSelect = true;
            this.lvMobHelpers.GridLines = true;
            this.lvMobHelpers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvMobHelpers.HideSelection = false;
            this.lvMobHelpers.Location = new System.Drawing.Point(4, 30);
            this.lvMobHelpers.Name = "lvMobHelpers";
            this.lvMobHelpers.Size = new System.Drawing.Size(452, 290);
            this.lvMobHelpers.TabIndex = 12;
            this.lvMobHelpers.UseCompatibleStateImageBehavior = false;
            this.lvMobHelpers.View = System.Windows.Forms.View.Details;
            this.lvMobHelpers.SizeChanged += new System.EventHandler(this.LastColumnAutosize);
            this.lvMobHelpers.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lvMobHelpers_KeyUp);
            this.lvMobHelpers.MouseEnter += new System.EventHandler(this.FocusToControl);
            this.lvMobHelpers.MouseUp += new System.Windows.Forms.MouseEventHandler(this.NavigatedControl_MouseUp);
            // 
            // chMobHelperVNum
            // 
            this.chMobHelperVNum.Width = 50;
            // 
            // chMobHelperName
            // 
            this.chMobHelperName.Width = 366;
            // 
            // tpMobTriggers
            // 
            this.tpMobTriggers.Controls.Add(this.btnAddMobTrigger);
            this.tpMobTriggers.Controls.Add(this.btnMobRemoveTrigger);
            this.tpMobTriggers.Controls.Add(this.lvMobTriggers);
            this.tpMobTriggers.Location = new System.Drawing.Point(4, 40);
            this.tpMobTriggers.Name = "tpMobTriggers";
            this.tpMobTriggers.Size = new System.Drawing.Size(459, 323);
            this.tpMobTriggers.TabIndex = 6;
            this.tpMobTriggers.Text = "Триггеры";
            this.tpMobTriggers.UseVisualStyleBackColor = true;
            // 
            // lvMobTriggers
            // 
            this.lvMobTriggers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvMobTriggers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader16,
            this.columnHeader17});
            this.lvMobTriggers.ContextMenuStrip = this.cmsGridMenu;
            this.lvMobTriggers.FullRowSelect = true;
            this.lvMobTriggers.GridLines = true;
            this.lvMobTriggers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvMobTriggers.HideSelection = false;
            this.lvMobTriggers.Location = new System.Drawing.Point(4, 3);
            this.lvMobTriggers.Name = "lvMobTriggers";
            this.lvMobTriggers.Size = new System.Drawing.Size(422, 317);
            this.lvMobTriggers.TabIndex = 30;
            this.lvMobTriggers.UseCompatibleStateImageBehavior = false;
            this.lvMobTriggers.View = System.Windows.Forms.View.Details;
            this.lvMobTriggers.SizeChanged += new System.EventHandler(this.LastColumnAutosize);
            this.lvMobTriggers.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lvMobTriggers_KeyUp);
            this.lvMobTriggers.MouseEnter += new System.EventHandler(this.FocusToControl);
            this.lvMobTriggers.MouseUp += new System.Windows.Forms.MouseEventHandler(this.NavigatedControl_MouseUp);
            // 
            // columnHeader16
            // 
            this.columnHeader16.Width = 50;
            // 
            // columnHeader17
            // 
            this.columnHeader17.Width = 366;
            // 
            // tpMobResists
            // 
            this.tpMobResists.AutoScroll = true;
            this.tpMobResists.Controls.Add(gbOthers);
            this.tpMobResists.Controls.Add(gbResists);
            this.tpMobResists.Controls.Add(this.gbSaves);
            this.tpMobResists.Location = new System.Drawing.Point(4, 40);
            this.tpMobResists.Name = "tpMobResists";
            this.tpMobResists.Padding = new System.Windows.Forms.Padding(2);
            this.tpMobResists.Size = new System.Drawing.Size(459, 323);
            this.tpMobResists.TabIndex = 11;
            this.tpMobResists.Text = "Сейвы/Резисты/Прочее";
            this.tpMobResists.UseVisualStyleBackColor = true;
            // 
            // gbSaves
            // 
            this.gbSaves.Controls.Add(this.nudSaveFightSkills);
            this.gbSaves.Controls.Add(this.nudSaveMagDam);
            this.gbSaves.Controls.Add(this.nudSaveParalyze);
            this.gbSaves.Controls.Add(label107);
            this.gbSaves.Controls.Add(label104);
            this.gbSaves.Controls.Add(this.nudSaveMagBreathe);
            this.gbSaves.Controls.Add(label105);
            this.gbSaves.Controls.Add(label106);
            this.gbSaves.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbSaves.Location = new System.Drawing.Point(2, 2);
            this.gbSaves.Name = "gbSaves";
            this.gbSaves.Size = new System.Drawing.Size(455, 44);
            this.gbSaves.TabIndex = 36;
            this.gbSaves.TabStop = false;
            this.gbSaves.Text = "Сейвы";
            // 
            // tpMobRoles
            // 
            this.tpMobRoles.Controls.Add(this.tplMobRoles);
            this.tpMobRoles.Location = new System.Drawing.Point(4, 40);
            this.tpMobRoles.Name = "tpMobRoles";
            this.tpMobRoles.Size = new System.Drawing.Size(459, 323);
            this.tpMobRoles.TabIndex = 12;
            this.tpMobRoles.Text = "Роли";
            this.tpMobRoles.UseVisualStyleBackColor = true;
            // 
            // tplMobRoles
            // 
            this.tplMobRoles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tplMobRoles.Grouped = false;
            this.tplMobRoles.LeftListCaption = "Роли моба";
            this.tplMobRoles.Location = new System.Drawing.Point(0, 0);
            this.tplMobRoles.Name = "tplMobRoles";
            this.tplMobRoles.RightListCaption = "Доступные роли";
            this.tplMobRoles.Size = new System.Drawing.Size(459, 323);
            this.tplMobRoles.SplitterDistance = 216;
            this.tplMobRoles.TabIndex = 1;
            this.tplMobRoles.ValueChanged += new BZEditor.UcTwoPanelsList.ValueChangeEvent(this.tplMobRolesValueChanged);
            // 
            // tpMobIngredients
            // 
            this.tpMobIngredients.Controls.Add(this.elvMobIngredients);
            this.tpMobIngredients.Controls.Add(this.btnAddMobIngredient);
            this.tpMobIngredients.Controls.Add(this.btnRemoveMobIngredient);
            this.tpMobIngredients.Location = new System.Drawing.Point(4, 40);
            this.tpMobIngredients.Name = "tpMobIngredients";
            this.tpMobIngredients.Size = new System.Drawing.Size(459, 323);
            this.tpMobIngredients.TabIndex = 13;
            this.tpMobIngredients.Text = "Ингредиенты";
            this.tpMobIngredients.UseVisualStyleBackColor = true;
            // 
            // pnlAddMobDesc
            // 
            this.pnlAddMobDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlAddMobDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAddMobDesc.Controls.Add(this.ertbMobDescription);
            this.pnlAddMobDesc.Location = new System.Drawing.Point(3, 20);
            this.pnlAddMobDesc.Name = "pnlAddMobDesc";
            this.pnlAddMobDesc.Size = new System.Drawing.Size(709, 70);
            this.pnlAddMobDesc.TabIndex = 2;
            // 
            // ertbMobDescription
            // 
            this.ertbMobDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ertbMobDescription.ContextMenuStrip = this.cmsRoomsDescription;
            this.ertbMobDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ertbMobDescription.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.errorProvider.SetIconAlignment(this.ertbMobDescription, System.Windows.Forms.ErrorIconAlignment.TopRight);
            this.errorProvider.SetIconPadding(this.ertbMobDescription, -16);
            this.ertbMobDescription.Location = new System.Drawing.Point(0, 0);
            this.ertbMobDescription.Name = "ertbMobDescription";
            this.ertbMobDescription.Size = new System.Drawing.Size(707, 68);
            this.ertbMobDescription.TabIndex = 1;
            this.ertbMobDescription.Text = "";
            this.ertbMobDescription.WordWrap = false;
            this.ertbMobDescription.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CExtRichTextBoxKeyUp);
            this.ertbMobDescription.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CExtRichTextBoxMouseUp);
            this.ertbMobDescription.Validated += new System.EventHandler(this.cmsMobDescription_Validated);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.btnMobSpecFormatCommonDesc);
            this.panel1.Controls.Add(this.btnMobSpellCheckCommonDesc);
            this.panel1.Controls.Add(this.btnMobFormatCommonDesc);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(715, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(98, 93);
            this.panel1.TabIndex = 89;
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Location = new System.Drawing.Point(3, 4);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(278, 13);
            this.label67.TabIndex = 2;
            this.label67.Text = "Подробное описание моба (по команде \"осмотреть\")";
            // 
            // tpTriggers
            // 
            this.tpTriggers.BackColor = System.Drawing.Color.Transparent;
            this.tpTriggers.Controls.Add(this.splitContainerTrg);
            this.tpTriggers.Location = new System.Drawing.Point(4, 23);
            this.tpTriggers.Name = "tpTriggers";
            this.tpTriggers.Padding = new System.Windows.Forms.Padding(3);
            this.tpTriggers.Size = new System.Drawing.Size(819, 472);
            this.tpTriggers.TabIndex = 2;
            this.tpTriggers.Text = "Триггеры";
            this.tpTriggers.UseVisualStyleBackColor = true;
            // 
            // splitContainerTrg
            // 
            this.splitContainerTrg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerTrg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerTrg.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerTrg.Location = new System.Drawing.Point(3, 3);
            this.splitContainerTrg.Name = "splitContainerTrg";
            // 
            // splitContainerTrg.Panel1
            // 
            this.splitContainerTrg.Panel1.Controls.Add(this.tcTriggers);
            // 
            // splitContainerTrg.Panel2
            // 
            this.splitContainerTrg.Panel2.Controls.Add(this.codeEditor);
            this.splitContainerTrg.Panel2.Controls.Add(this.toolStripPanelTrgTop);
            this.splitContainerTrg.Panel2.Controls.Add(this.toolStripPanelTrgRight);
            this.splitContainerTrg.Panel2.Controls.Add(this.toolStripPanelTrgBottom);
            this.splitContainerTrg.Panel2.Controls.Add(this.toolStripPanelTrgLeft);
            this.splitContainerTrg.Size = new System.Drawing.Size(813, 466);
            this.splitContainerTrg.SplitterDistance = 246;
            this.splitContainerTrg.TabIndex = 2;
            // 
            // tcTriggers
            // 
            this.tcTriggers.Controls.Add(this.tpTrgParams);
            this.tcTriggers.Controls.Add(this.tpTrgGlobalVars);
            this.tcTriggers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcTriggers.Location = new System.Drawing.Point(0, 0);
            this.tcTriggers.Name = "tcTriggers";
            this.tcTriggers.SelectedIndex = 0;
            this.tcTriggers.Size = new System.Drawing.Size(244, 464);
            this.tcTriggers.TabIndex = 18;
            // 
            // tpTrgParams
            // 
            this.tpTrgParams.Controls.Add(this.cboxTrgClass);
            this.tpTrgParams.Controls.Add(this.gbObjectsToCreate);
            this.tpTrgParams.Controls.Add(this.tbTrgArgs);
            this.tpTrgParams.Controls.Add(this.nudTrgNumArg);
            this.tpTrgParams.Controls.Add(this.tbTrgName);
            this.tpTrgParams.Controls.Add(this.label61);
            this.tpTrgParams.Controls.Add(this.label58);
            this.tpTrgParams.Controls.Add(this.label62);
            this.tpTrgParams.Controls.Add(this.label44);
            this.tpTrgParams.Location = new System.Drawing.Point(4, 22);
            this.tpTrgParams.Name = "tpTrgParams";
            this.tpTrgParams.Padding = new System.Windows.Forms.Padding(3);
            this.tpTrgParams.Size = new System.Drawing.Size(236, 438);
            this.tpTrgParams.TabIndex = 0;
            this.tpTrgParams.Text = "Параметры";
            this.tpTrgParams.UseVisualStyleBackColor = true;
            // 
            // cboxTrgClass
            // 
            this.cboxTrgClass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboxTrgClass.ItemHeight = 13;
            this.cboxTrgClass.Location = new System.Drawing.Point(93, 5);
            this.cboxTrgClass.Name = "cboxTrgClass";
            this.cboxTrgClass.Size = new System.Drawing.Size(140, 21);
            this.cboxTrgClass.TabIndex = 17;
            this.cboxTrgClass.SelectedIndexChanged += new System.EventHandler(this.CboxTrgClassSelectedIndexChanged);
            // 
            // gbObjectsToCreate
            // 
            this.gbObjectsToCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbObjectsToCreate.Controls.Add(this.lvTrgActivationConditions);
            this.gbObjectsToCreate.Location = new System.Drawing.Point(1, 131);
            this.gbObjectsToCreate.Name = "gbObjectsToCreate";
            this.gbObjectsToCreate.Size = new System.Drawing.Size(235, 308);
            this.gbObjectsToCreate.TabIndex = 9;
            this.gbObjectsToCreate.TabStop = false;
            this.gbObjectsToCreate.Text = "Условия срабатывания триггера";
            // 
            // lvTrgActivationConditions
            // 
            this.lvTrgActivationConditions.CheckBoxes = true;
            this.lvTrgActivationConditions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chTrgActCond});
            this.lvTrgActivationConditions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvTrgActivationConditions.FullRowSelect = true;
            this.lvTrgActivationConditions.GridLines = true;
            this.lvTrgActivationConditions.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvTrgActivationConditions.Location = new System.Drawing.Point(3, 16);
            this.lvTrgActivationConditions.Name = "lvTrgActivationConditions";
            this.lvTrgActivationConditions.ShowItemToolTips = true;
            this.lvTrgActivationConditions.Size = new System.Drawing.Size(229, 289);
            this.lvTrgActivationConditions.TabIndex = 103;
            this.lvTrgActivationConditions.UseCompatibleStateImageBehavior = false;
            this.lvTrgActivationConditions.View = System.Windows.Forms.View.Details;
            this.lvTrgActivationConditions.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.LvTrgActivationConditionsItemChecked);
            this.lvTrgActivationConditions.SizeChanged += new System.EventHandler(this.LastColumnAutosize);
            this.lvTrgActivationConditions.Leave += new System.EventHandler(this.LvTrgActivationConditionsLeave);
            this.lvTrgActivationConditions.MouseEnter += new System.EventHandler(this.FocusToControl);
            // 
            // chTrgActCond
            // 
            this.chTrgActCond.Width = 224;
            // 
            // tbTrgArgs
            // 
            this.tbTrgArgs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTrgArgs.Location = new System.Drawing.Point(5, 76);
            this.tbTrgArgs.Name = "tbTrgArgs";
            this.tbTrgArgs.Size = new System.Drawing.Size(228, 20);
            this.tbTrgArgs.TabIndex = 14;
            this.tbTrgArgs.Validated += new System.EventHandler(this.TbTrgArgsValidated);
            // 
            // nudTrgNumArg
            // 
            this.nudTrgNumArg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudTrgNumArg.Location = new System.Drawing.Point(114, 105);
            this.nudTrgNumArg.Maximum = new decimal(new int[] {
            -1486618625,
            232830643,
            0,
            0});
            this.nudTrgNumArg.Minimum = new decimal(new int[] {
            -1486618625,
            232830643,
            0,
            -2147483648});
            this.nudTrgNumArg.Name = "nudTrgNumArg";
            this.nudTrgNumArg.Size = new System.Drawing.Size(119, 20);
            this.nudTrgNumArg.TabIndex = 13;
            this.nudTrgNumArg.Validated += new System.EventHandler(this.NudTrgNumArgValidated);
            // 
            // tbTrgName
            // 
            this.tbTrgName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTrgName.Location = new System.Drawing.Point(5, 40);
            this.tbTrgName.Name = "tbTrgName";
            this.tbTrgName.Size = new System.Drawing.Size(228, 20);
            this.tbTrgName.TabIndex = 15;
            this.tbTrgName.Validated += new System.EventHandler(this.TbTrgNameValidated);
            // 
            // label61
            // 
            this.label61.Location = new System.Drawing.Point(2, 105);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(112, 16);
            this.label61.TabIndex = 11;
            this.label61.Text = "Числовой аргумент";
            this.label61.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label58
            // 
            this.label58.Location = new System.Drawing.Point(2, 6);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(92, 16);
            this.label58.TabIndex = 16;
            this.label58.Text = "Класс триггера";
            this.label58.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label62
            // 
            this.label62.Location = new System.Drawing.Point(2, 60);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(63, 16);
            this.label62.TabIndex = 12;
            this.label62.Text = "Аргумент";
            this.label62.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label44
            // 
            this.label44.Location = new System.Drawing.Point(2, 22);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(63, 16);
            this.label44.TabIndex = 10;
            this.label44.Text = "Название";
            this.label44.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tpTrgGlobalVars
            // 
            this.tpTrgGlobalVars.Controls.Add(this.button1);
            this.tpTrgGlobalVars.Controls.Add(this.button2);
            this.tpTrgGlobalVars.Controls.Add(this.listView1);
            this.tpTrgGlobalVars.Location = new System.Drawing.Point(4, 22);
            this.tpTrgGlobalVars.Name = "tpTrgGlobalVars";
            this.tpTrgGlobalVars.Padding = new System.Windows.Forms.Padding(3);
            this.tpTrgGlobalVars.Size = new System.Drawing.Size(236, 438);
            this.tpTrgGlobalVars.TabIndex = 1;
            this.tpTrgGlobalVars.Text = "Глобальные переменные";
            this.tpTrgGlobalVars.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader22,
            this.columnHeader23});
            this.listView1.ContextMenuStrip = this.cmsGridMenu;
            this.listView1.Enabled = false;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(3, 33);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(230, 402);
            this.listView1.TabIndex = 35;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader22
            // 
            this.columnHeader22.Width = 50;
            // 
            // columnHeader23
            // 
            this.columnHeader23.Width = 176;
            // 
            // codeEditor
            // 
            this.codeEditor.ActiveView = Fireball.Windows.Forms.CodeEditor.ActiveView.BottomRight;
            this.codeEditor.AutoListPosition = null;
            this.codeEditor.AutoListSelectedText = "";
            this.codeEditor.AutoListVisible = false;
            this.codeEditor.ContextMenuStrip = this.cmsCodeEditor;
            this.codeEditor.CopyAsRTF = false;
            this.codeEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codeEditor.Document = this.syntaxDocument;
            this.codeEditor.InfoTipPosition = null;
            lineMarginRender1.Bounds = new System.Drawing.Rectangle(19, 0, 19, 16);
            this.codeEditor.LineMarginRender = lineMarginRender1;
            this.codeEditor.Location = new System.Drawing.Point(0, 0);
            this.codeEditor.LockCursorUpdate = false;
            this.codeEditor.Name = "codeEditor";
            this.codeEditor.Saved = false;
            this.codeEditor.ShowScopeIndicator = false;
            this.codeEditor.Size = new System.Drawing.Size(561, 464);
            this.codeEditor.SmoothScroll = false;
            this.codeEditor.SplitviewH = -4;
            this.codeEditor.SplitviewV = -4;
            this.codeEditor.TabGuideColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(243)))), ((int)(((byte)(234)))));
            this.codeEditor.TabIndex = 9;
            this.codeEditor.Text = " ";
            this.codeEditor.WhitespaceColor = System.Drawing.SystemColors.ControlDark;
            this.codeEditor.TextChanged += new System.EventHandler(this.CodeEditorTextChanged);
            // 
            // toolStripPanelTrgTop
            // 
            this.toolStripPanelTrgTop.Controls.Add(this.toolStripTrgEditor);
            this.toolStripPanelTrgTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.toolStripPanelTrgTop.Location = new System.Drawing.Point(0, 0);
            this.toolStripPanelTrgTop.Name = "toolStripPanelTrgTop";
            this.toolStripPanelTrgTop.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.toolStripPanelTrgTop.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.toolStripPanelTrgTop.Size = new System.Drawing.Size(561, 0);
            // 
            // toolStripTrgEditor
            // 
            this.toolStripTrgEditor.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripTrgEditor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbTrgClear,
            this.toolStripSeparator3,
            this.tsbTrgUndo,
            this.tsbTrgRedo,
            this.toolStripSeparator4,
            this.tsbTrgToggleBookmark,
            this.tsbTrgGoToPrevBookmark,
            this.tsbTrgGoToNextBookmark,
            this.toolStripSeparator10,
            this.tsbTrgGoToLine,
            this.tsbTrgSearch,
            this.tsbTrgSearchNext,
            this.tsbTrgReplace,
            this.toolStripSeparator5,
            this.tsbTrgIndent,
            this.tsbTrgOutdent,
            this.tsbTrgCopy,
            this.tsbTrgCut,
            this.tsbTrgPaste,
            this.toolStripSeparator2,
            this.tsbInsertSpellNumber});
            this.toolStripTrgEditor.Location = new System.Drawing.Point(3, 0);
            this.toolStripTrgEditor.Name = "toolStripTrgEditor";
            this.toolStripTrgEditor.Size = new System.Drawing.Size(410, 25);
            this.toolStripTrgEditor.TabIndex = 0;
            this.toolStripTrgEditor.Visible = false;
            // 
            // tsbTrgClear
            // 
            this.tsbTrgClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbTrgClear.Image = ((System.Drawing.Image)(resources.GetObject("tsbTrgClear.Image")));
            this.tsbTrgClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTrgClear.Name = "tsbTrgClear";
            this.tsbTrgClear.Size = new System.Drawing.Size(23, 22);
            this.tsbTrgClear.ToolTipText = "Очистить поле редактирования";
            this.tsbTrgClear.Click += new System.EventHandler(this.TsbTrgClearClick);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbTrgUndo
            // 
            this.tsbTrgUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbTrgUndo.Image = ((System.Drawing.Image)(resources.GetObject("tsbTrgUndo.Image")));
            this.tsbTrgUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTrgUndo.Name = "tsbTrgUndo";
            this.tsbTrgUndo.Size = new System.Drawing.Size(23, 22);
            this.tsbTrgUndo.Text = "Отменить изменения  (Ctrl+Z)";
            this.tsbTrgUndo.Click += new System.EventHandler(this.TsbTrgUndoClick);
            // 
            // tsbTrgRedo
            // 
            this.tsbTrgRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbTrgRedo.Image = ((System.Drawing.Image)(resources.GetObject("tsbTrgRedo.Image")));
            this.tsbTrgRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTrgRedo.Name = "tsbTrgRedo";
            this.tsbTrgRedo.Size = new System.Drawing.Size(23, 22);
            this.tsbTrgRedo.Text = "Вернуть отмененное изменение (Ctrl+Y)";
            this.tsbTrgRedo.Click += new System.EventHandler(this.TsbTrgRedoClick);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbTrgToggleBookmark
            // 
            this.tsbTrgToggleBookmark.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbTrgToggleBookmark.Image = ((System.Drawing.Image)(resources.GetObject("tsbTrgToggleBookmark.Image")));
            this.tsbTrgToggleBookmark.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTrgToggleBookmark.Name = "tsbTrgToggleBookmark";
            this.tsbTrgToggleBookmark.Size = new System.Drawing.Size(23, 22);
            this.tsbTrgToggleBookmark.Text = "Установить/снять метку (Ctrl+F2)";
            this.tsbTrgToggleBookmark.Click += new System.EventHandler(this.TsbTrgToggleBookmarkClick);
            // 
            // tsbTrgGoToPrevBookmark
            // 
            this.tsbTrgGoToPrevBookmark.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbTrgGoToPrevBookmark.Image = ((System.Drawing.Image)(resources.GetObject("tsbTrgGoToPrevBookmark.Image")));
            this.tsbTrgGoToPrevBookmark.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTrgGoToPrevBookmark.Name = "tsbTrgGoToPrevBookmark";
            this.tsbTrgGoToPrevBookmark.Size = new System.Drawing.Size(23, 22);
            this.tsbTrgGoToPrevBookmark.Text = "К предыдущей метке (Shift+F2)";
            this.tsbTrgGoToPrevBookmark.Click += new System.EventHandler(this.TsbTrgGoToPrevBookmarkClick);
            // 
            // tsbTrgGoToNextBookmark
            // 
            this.tsbTrgGoToNextBookmark.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbTrgGoToNextBookmark.Image = ((System.Drawing.Image)(resources.GetObject("tsbTrgGoToNextBookmark.Image")));
            this.tsbTrgGoToNextBookmark.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTrgGoToNextBookmark.Name = "tsbTrgGoToNextBookmark";
            this.tsbTrgGoToNextBookmark.Size = new System.Drawing.Size(23, 22);
            this.tsbTrgGoToNextBookmark.Text = "К следующей метке (F2)";
            this.tsbTrgGoToNextBookmark.Click += new System.EventHandler(this.TsbTrgGoToNextBookmarkClick);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbTrgGoToLine
            // 
            this.tsbTrgGoToLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbTrgGoToLine.Image = ((System.Drawing.Image)(resources.GetObject("tsbTrgGoToLine.Image")));
            this.tsbTrgGoToLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTrgGoToLine.Name = "tsbTrgGoToLine";
            this.tsbTrgGoToLine.Size = new System.Drawing.Size(23, 22);
            this.tsbTrgGoToLine.Text = "Перейти к строке №...(Ctrl+G)";
            this.tsbTrgGoToLine.Click += new System.EventHandler(this.TsbTrgGoToLineClick);
            // 
            // tsbTrgSearch
            // 
            this.tsbTrgSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbTrgSearch.Image = ((System.Drawing.Image)(resources.GetObject("tsbTrgSearch.Image")));
            this.tsbTrgSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTrgSearch.Name = "tsbTrgSearch";
            this.tsbTrgSearch.Size = new System.Drawing.Size(23, 22);
            this.tsbTrgSearch.Text = "Найти (Ctrl+F)";
            this.tsbTrgSearch.Click += new System.EventHandler(this.TsbTrgSearchClick);
            // 
            // tsbTrgSearchNext
            // 
            this.tsbTrgSearchNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbTrgSearchNext.Image = ((System.Drawing.Image)(resources.GetObject("tsbTrgSearchNext.Image")));
            this.tsbTrgSearchNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTrgSearchNext.Name = "tsbTrgSearchNext";
            this.tsbTrgSearchNext.Size = new System.Drawing.Size(23, 22);
            this.tsbTrgSearchNext.Text = "Найти следующее вхождение (F3)";
            this.tsbTrgSearchNext.Click += new System.EventHandler(this.TsbTrgSearchNextClick);
            // 
            // tsbTrgReplace
            // 
            this.tsbTrgReplace.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbTrgReplace.Image = ((System.Drawing.Image)(resources.GetObject("tsbTrgReplace.Image")));
            this.tsbTrgReplace.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTrgReplace.Name = "tsbTrgReplace";
            this.tsbTrgReplace.Size = new System.Drawing.Size(23, 22);
            this.tsbTrgReplace.Text = "Заменить (Ctrl+H)";
            this.tsbTrgReplace.Click += new System.EventHandler(this.TsbTrgReplaceClick);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbTrgIndent
            // 
            this.tsbTrgIndent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbTrgIndent.Image = ((System.Drawing.Image)(resources.GetObject("tsbTrgIndent.Image")));
            this.tsbTrgIndent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTrgIndent.Name = "tsbTrgIndent";
            this.tsbTrgIndent.Size = new System.Drawing.Size(23, 22);
            this.tsbTrgIndent.Text = "Сдвиг выделенного текста вправо (TAB)";
            this.tsbTrgIndent.Click += new System.EventHandler(this.TsbTrgIndentClick);
            // 
            // tsbTrgOutdent
            // 
            this.tsbTrgOutdent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbTrgOutdent.Image = ((System.Drawing.Image)(resources.GetObject("tsbTrgOutdent.Image")));
            this.tsbTrgOutdent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTrgOutdent.Name = "tsbTrgOutdent";
            this.tsbTrgOutdent.Size = new System.Drawing.Size(23, 22);
            this.tsbTrgOutdent.Text = "Сдвиг выделенного текста влево (Shift+TAB)";
            this.tsbTrgOutdent.Click += new System.EventHandler(this.TsbTrgOutdentClick);
            // 
            // tsbTrgCopy
            // 
            this.tsbTrgCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbTrgCopy.Image = ((System.Drawing.Image)(resources.GetObject("tsbTrgCopy.Image")));
            this.tsbTrgCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTrgCopy.Name = "tsbTrgCopy";
            this.tsbTrgCopy.Size = new System.Drawing.Size(23, 22);
            this.tsbTrgCopy.Text = "Копировать (Ctrl+C)";
            this.tsbTrgCopy.Click += new System.EventHandler(this.TsbTrgCopyClick);
            // 
            // tsbTrgCut
            // 
            this.tsbTrgCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbTrgCut.Image = ((System.Drawing.Image)(resources.GetObject("tsbTrgCut.Image")));
            this.tsbTrgCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTrgCut.Name = "tsbTrgCut";
            this.tsbTrgCut.Size = new System.Drawing.Size(23, 22);
            this.tsbTrgCut.Text = "Вырезать (Ctrl+X)";
            this.tsbTrgCut.Click += new System.EventHandler(this.TsbTrgCutClick);
            // 
            // tsbTrgPaste
            // 
            this.tsbTrgPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbTrgPaste.Image = ((System.Drawing.Image)(resources.GetObject("tsbTrgPaste.Image")));
            this.tsbTrgPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTrgPaste.Name = "tsbTrgPaste";
            this.tsbTrgPaste.Size = new System.Drawing.Size(23, 22);
            this.tsbTrgPaste.Text = "Вставить (Ctrl+V)";
            this.tsbTrgPaste.Click += new System.EventHandler(this.TsbTrgPasteClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbInsertSpellNumber
            // 
            this.tsbInsertSpellNumber.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbInsertSpellNumber.Image = global::BZEditor.Properties.Resources.button_insert_spell_num;
            this.tsbInsertSpellNumber.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbInsertSpellNumber.Name = "tsbInsertSpellNumber";
            this.tsbInsertSpellNumber.Size = new System.Drawing.Size(23, 22);
            this.tsbInsertSpellNumber.Text = "Вставить номер заклинания";
            this.tsbInsertSpellNumber.Click += new System.EventHandler(this.TsbInsertSpellNumberClick);
            // 
            // toolStripPanelTrgRight
            // 
            this.toolStripPanelTrgRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolStripPanelTrgRight.Location = new System.Drawing.Point(561, 0);
            this.toolStripPanelTrgRight.Name = "toolStripPanelTrgRight";
            this.toolStripPanelTrgRight.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.toolStripPanelTrgRight.RowMargin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.toolStripPanelTrgRight.Size = new System.Drawing.Size(0, 464);
            // 
            // toolStripPanelTrgBottom
            // 
            this.toolStripPanelTrgBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStripPanelTrgBottom.Location = new System.Drawing.Point(0, 464);
            this.toolStripPanelTrgBottom.Name = "toolStripPanelTrgBottom";
            this.toolStripPanelTrgBottom.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.toolStripPanelTrgBottom.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.toolStripPanelTrgBottom.Size = new System.Drawing.Size(561, 0);
            // 
            // toolStripPanelTrgLeft
            // 
            this.toolStripPanelTrgLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStripPanelTrgLeft.Location = new System.Drawing.Point(0, 0);
            this.toolStripPanelTrgLeft.Name = "toolStripPanelTrgLeft";
            this.toolStripPanelTrgLeft.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.toolStripPanelTrgLeft.RowMargin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.toolStripPanelTrgLeft.Size = new System.Drawing.Size(0, 464);
            // 
            // toolStripMain
            // 
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbReloadWithoutSave,
            this.toolStripSeparator14,
            this.tsbSaveZone,
            this.tsbSaveCurTabData,
            this.toolStripSeparator11,
            this.tsbAddItems,
            this.tsbRemoveItems,
            this.tsbAddTemplate,
            this.tsbAutolinkingZ,
            this.tsbAutolinkingY,
            this.tsbAutolinkingX,
            this.tsbCreateClones,
            this.toolStripSeparator13,
            this.tsbCopy,
            this.tsbPaste,
            this.tsbPasteAsTemplate,
            this.toolStripSeparator8,
            this.tsbSetOppositeExit,
            this.tsbHistoryBack,
            this.tsbHistoryForward,
            this.toolStripSeparator7,
            this.tsbMapZoomOut,
            this.tsbMapZoomIn,
            this.toolStripSplitButton1,
            this.tsbMapZDec,
            this.tsbMapToZeroRom,
            this.tsbMapZInc,
            this.toolStripSeparator12,
            this.tsbShowRoomTriggers,
            this.tsbShowRoomMobs,
            this.tsbShowRoomObjects,
            this.tsbShowRoomNumbers,
            this.tsbShowRoomDetails,
            this.toolStripButton2,
            this.tsbSketchColor,
            this.tsbBrush,
            this.tsbCreateRoomsBySketch,
            this.tsbClearSketch,
            this.toolStripButton4,
            this.tsbGenerateMap});
            this.toolStripMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(829, 25);
            this.toolStripMain.TabIndex = 5;
            this.toolStripMain.Text = "toolStrip";
            // 
            // tsbReloadWithoutSave
            // 
            this.tsbReloadWithoutSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbReloadWithoutSave.Image = global::BZEditor.Properties.Resources.button_reloadzone;
            this.tsbReloadWithoutSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbReloadWithoutSave.Name = "tsbReloadWithoutSave";
            this.tsbReloadWithoutSave.Size = new System.Drawing.Size(23, 22);
            this.tsbReloadWithoutSave.Text = "Перечитать зону из файла без сохранения";
            this.tsbReloadWithoutSave.Click += new System.EventHandler(this.TsbReloadWithoutSaveClick);
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbSaveZone
            // 
            this.tsbSaveZone.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSaveZone.Image = ((System.Drawing.Image)(resources.GetObject("tsbSaveZone.Image")));
            this.tsbSaveZone.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveZone.Name = "tsbSaveZone";
            this.tsbSaveZone.Size = new System.Drawing.Size(23, 22);
            this.tsbSaveZone.Text = "Сохранить зону";
            this.tsbSaveZone.ToolTipText = "Сохранить зону (Ctrl+Shift+S)";
            this.tsbSaveZone.Click += new System.EventHandler(this.TsbSaveZoneClick);
            // 
            // tsbSaveCurTabData
            // 
            this.tsbSaveCurTabData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSaveCurTabData.Image = global::BZEditor.Properties.Resources.button_save_cur_tab;
            this.tsbSaveCurTabData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveCurTabData.Name = "tsbSaveCurTabData";
            this.tsbSaveCurTabData.Size = new System.Drawing.Size(23, 22);
            this.tsbSaveCurTabData.Text = "Сохранить данные текущей закладки";
            this.tsbSaveCurTabData.ToolTipText = "Сохранить данные текущей закладки (Ctrl+S)";
            this.tsbSaveCurTabData.Click += new System.EventHandler(this.TsbSaveCurTabDataClick);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbAddItems
            // 
            this.tsbAddItems.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddItems.Image = ((System.Drawing.Image)(resources.GetObject("tsbAddItems.Image")));
            this.tsbAddItems.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddItems.Name = "tsbAddItems";
            this.tsbAddItems.Size = new System.Drawing.Size(23, 22);
            this.tsbAddItems.Text = "Добавить";
            this.tsbAddItems.ToolTipText = "Создать (Ctrl+N)";
            this.tsbAddItems.Click += new System.EventHandler(this.TsbAddItemsClick);
            // 
            // tsbRemoveItems
            // 
            this.tsbRemoveItems.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRemoveItems.Image = ((System.Drawing.Image)(resources.GetObject("tsbRemoveItems.Image")));
            this.tsbRemoveItems.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRemoveItems.Name = "tsbRemoveItems";
            this.tsbRemoveItems.Size = new System.Drawing.Size(23, 22);
            this.tsbRemoveItems.Text = "Удалить";
            this.tsbRemoveItems.Click += new System.EventHandler(this.TsbRemoveItemsClick);
            // 
            // tsbAddTemplate
            // 
            this.tsbAddTemplate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddTemplate.Image = ((System.Drawing.Image)(resources.GetObject("tsbAddTemplate.Image")));
            this.tsbAddTemplate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddTemplate.Name = "tsbAddTemplate";
            this.tsbAddTemplate.Size = new System.Drawing.Size(23, 22);
            this.tsbAddTemplate.Text = "Добавить в набор шаблонов";
            this.tsbAddTemplate.ToolTipText = "Добавить в набор шаблонов (Ctrl+T)";
            this.tsbAddTemplate.Click += new System.EventHandler(this.TsbAddTemplateClick);
            // 
            // tsbAutolinkingZ
            // 
            this.tsbAutolinkingZ.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbAutolinkingZ.Checked = true;
            this.tsbAutolinkingZ.CheckOnClick = true;
            this.tsbAutolinkingZ.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbAutolinkingZ.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAutolinkingZ.Image = global::BZEditor.Properties.Resources.button_autolink_z;
            this.tsbAutolinkingZ.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAutolinkingZ.Name = "tsbAutolinkingZ";
            this.tsbAutolinkingZ.Size = new System.Drawing.Size(23, 22);
            this.tsbAutolinkingZ.Text = "Автолинковка по Z";
            this.tsbAutolinkingZ.Click += new System.EventHandler(this.TsbAutolinkingZClick);
            // 
            // tsbAutolinkingY
            // 
            this.tsbAutolinkingY.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbAutolinkingY.Checked = true;
            this.tsbAutolinkingY.CheckOnClick = true;
            this.tsbAutolinkingY.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbAutolinkingY.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAutolinkingY.Image = global::BZEditor.Properties.Resources.button_autolink_y;
            this.tsbAutolinkingY.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAutolinkingY.Name = "tsbAutolinkingY";
            this.tsbAutolinkingY.Size = new System.Drawing.Size(23, 22);
            this.tsbAutolinkingY.Text = "Автолинковка по Y";
            this.tsbAutolinkingY.Click += new System.EventHandler(this.TsbAutolinkingYClick);
            // 
            // tsbAutolinkingX
            // 
            this.tsbAutolinkingX.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbAutolinkingX.Checked = true;
            this.tsbAutolinkingX.CheckOnClick = true;
            this.tsbAutolinkingX.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbAutolinkingX.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAutolinkingX.Image = global::BZEditor.Properties.Resources.button_autolink_x;
            this.tsbAutolinkingX.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tsbAutolinkingX.Name = "tsbAutolinkingX";
            this.tsbAutolinkingX.Size = new System.Drawing.Size(23, 22);
            this.tsbAutolinkingX.Text = "Автолинковка по X";
            this.tsbAutolinkingX.Click += new System.EventHandler(this.TsbAutolinkingXClick);
            // 
            // tsbCreateClones
            // 
            this.tsbCreateClones.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCreateClones.Image = global::BZEditor.Properties.Resources.button_clone;
            this.tsbCreateClones.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCreateClones.Name = "tsbCreateClones";
            this.tsbCreateClones.Size = new System.Drawing.Size(23, 22);
            this.tsbCreateClones.Text = "Клонировать";
            this.tsbCreateClones.ToolTipText = "Клонировать (Ctrl+C)";
            this.tsbCreateClones.Click += new System.EventHandler(this.TsbCreateClonesClick);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbCopy
            // 
            this.tsbCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCopy.Image = ((System.Drawing.Image)(resources.GetObject("tsbCopy.Image")));
            this.tsbCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCopy.Name = "tsbCopy";
            this.tsbCopy.Size = new System.Drawing.Size(23, 22);
            this.tsbCopy.Text = "Копировать";
            this.tsbCopy.Click += new System.EventHandler(this.TsbCopyClick);
            // 
            // tsbPaste
            // 
            this.tsbPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPaste.Image = ((System.Drawing.Image)(resources.GetObject("tsbPaste.Image")));
            this.tsbPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPaste.Name = "tsbPaste";
            this.tsbPaste.Size = new System.Drawing.Size(23, 22);
            this.tsbPaste.Text = "Вставить скопированное";
            this.tsbPaste.Click += new System.EventHandler(this.TsbPasteClick);
            // 
            // tsbPasteAsTemplate
            // 
            this.tsbPasteAsTemplate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPasteAsTemplate.Image = ((System.Drawing.Image)(resources.GetObject("tsbPasteAsTemplate.Image")));
            this.tsbPasteAsTemplate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPasteAsTemplate.Name = "tsbPasteAsTemplate";
            this.tsbPasteAsTemplate.Size = new System.Drawing.Size(23, 22);
            this.tsbPasteAsTemplate.Text = "Применить к выбранному в списке как шаблон";
            this.tsbPasteAsTemplate.Click += new System.EventHandler(this.TsbPasteAsTemplateClick);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbSetOppositeExit
            // 
            this.tsbSetOppositeExit.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbSetOppositeExit.Checked = true;
            this.tsbSetOppositeExit.CheckOnClick = true;
            this.tsbSetOppositeExit.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbSetOppositeExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSetOppositeExit.Image = ((System.Drawing.Image)(resources.GetObject("tsbSetOppositeExit.Image")));
            this.tsbSetOppositeExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSetOppositeExit.Name = "tsbSetOppositeExit";
            this.tsbSetOppositeExit.Size = new System.Drawing.Size(23, 22);
            this.tsbSetOppositeExit.Text = "Предлагать установку встречного выхода ";
            this.tsbSetOppositeExit.ToolTipText = "Предлагать установку встречного выхода\r\nпри установке выходов вручную";
            // 
            // tsbHistoryBack
            // 
            this.tsbHistoryBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbHistoryBack.Image = ((System.Drawing.Image)(resources.GetObject("tsbHistoryBack.Image")));
            this.tsbHistoryBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHistoryBack.Name = "tsbHistoryBack";
            this.tsbHistoryBack.Size = new System.Drawing.Size(23, 22);
            this.tsbHistoryBack.Text = "Назад";
            this.tsbHistoryBack.Visible = false;
            this.tsbHistoryBack.Click += new System.EventHandler(this.TsbHistoryBackClick);
            // 
            // tsbHistoryForward
            // 
            this.tsbHistoryForward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbHistoryForward.Image = ((System.Drawing.Image)(resources.GetObject("tsbHistoryForward.Image")));
            this.tsbHistoryForward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHistoryForward.Name = "tsbHistoryForward";
            this.tsbHistoryForward.Size = new System.Drawing.Size(23, 22);
            this.tsbHistoryForward.Text = "Вперед";
            this.tsbHistoryForward.Visible = false;
            this.tsbHistoryForward.Click += new System.EventHandler(this.TsbHistoryForwardClick);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbMapZoomOut
            // 
            this.tsbMapZoomOut.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbMapZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMapZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("tsbMapZoomOut.Image")));
            this.tsbMapZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMapZoomOut.Name = "tsbMapZoomOut";
            this.tsbMapZoomOut.Size = new System.Drawing.Size(23, 22);
            this.tsbMapZoomOut.Text = "Отдалить карту";
            this.tsbMapZoomOut.Click += new System.EventHandler(this.TsbMapZoomOutClick);
            // 
            // tsbMapZoomIn
            // 
            this.tsbMapZoomIn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbMapZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMapZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("tsbMapZoomIn.Image")));
            this.tsbMapZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMapZoomIn.Name = "tsbMapZoomIn";
            this.tsbMapZoomIn.Size = new System.Drawing.Size(23, 22);
            this.tsbMapZoomIn.Text = "Приблизить карту";
            this.tsbMapZoomIn.Click += new System.EventHandler(this.TsbMapZoomInClick);
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbMapZDec
            // 
            this.tsbMapZDec.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbMapZDec.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMapZDec.Image = ((System.Drawing.Image)(resources.GetObject("tsbMapZDec.Image")));
            this.tsbMapZDec.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMapZDec.Name = "tsbMapZDec";
            this.tsbMapZDec.Size = new System.Drawing.Size(23, 22);
            this.tsbMapZDec.Text = "На уровень ниже по оcи Z";
            this.tsbMapZDec.ToolTipText = "На уровень ниже по оcи Z\r\n(Shift+Колесо мыши вперед)";
            this.tsbMapZDec.Click += new System.EventHandler(this.TsbMapZDecClick);
            // 
            // tsbMapToZeroRom
            // 
            this.tsbMapToZeroRom.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbMapToZeroRom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMapToZeroRom.Image = ((System.Drawing.Image)(resources.GetObject("tsbMapToZeroRom.Image")));
            this.tsbMapToZeroRom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMapToZeroRom.Name = "tsbMapToZeroRom";
            this.tsbMapToZeroRom.Size = new System.Drawing.Size(23, 22);
            this.tsbMapToZeroRom.Text = "К центру зоны";
            this.tsbMapToZeroRom.Click += new System.EventHandler(this.ToMapCenterClick);
            // 
            // tsbMapZInc
            // 
            this.tsbMapZInc.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbMapZInc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMapZInc.Image = ((System.Drawing.Image)(resources.GetObject("tsbMapZInc.Image")));
            this.tsbMapZInc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMapZInc.Name = "tsbMapZInc";
            this.tsbMapZInc.Size = new System.Drawing.Size(23, 22);
            this.tsbMapZInc.Text = "На уровень выше по оcи Z";
            this.tsbMapZInc.ToolTipText = "На уровень выше по оcи Z\r\n(Shift+Колесо мыши назад)";
            this.tsbMapZInc.Click += new System.EventHandler(this.TsbMapZIncClick);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbShowRoomTriggers
            // 
            this.tsbShowRoomTriggers.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbShowRoomTriggers.CheckOnClick = true;
            this.tsbShowRoomTriggers.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbShowRoomTriggers.Image = global::BZEditor.Properties.Resources.button_showtriggers;
            this.tsbShowRoomTriggers.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbShowRoomTriggers.Name = "tsbShowRoomTriggers";
            this.tsbShowRoomTriggers.Size = new System.Drawing.Size(23, 22);
            this.tsbShowRoomTriggers.Text = "Отображать метки триггеров";
            this.tsbShowRoomTriggers.CheckedChanged += new System.EventHandler(this.TsbShowRoomTriggersCheckedChanged);
            // 
            // tsbShowRoomMobs
            // 
            this.tsbShowRoomMobs.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbShowRoomMobs.CheckOnClick = true;
            this.tsbShowRoomMobs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbShowRoomMobs.Image = global::BZEditor.Properties.Resources.button_showmobs;
            this.tsbShowRoomMobs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbShowRoomMobs.Name = "tsbShowRoomMobs";
            this.tsbShowRoomMobs.Size = new System.Drawing.Size(23, 22);
            this.tsbShowRoomMobs.Text = "Отображать метки мобов";
            this.tsbShowRoomMobs.CheckedChanged += new System.EventHandler(this.TsbShowRoomMobsCheckedChanged);
            // 
            // tsbShowRoomObjects
            // 
            this.tsbShowRoomObjects.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbShowRoomObjects.CheckOnClick = true;
            this.tsbShowRoomObjects.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbShowRoomObjects.Image = global::BZEditor.Properties.Resources.button_showobjects;
            this.tsbShowRoomObjects.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbShowRoomObjects.Name = "tsbShowRoomObjects";
            this.tsbShowRoomObjects.Size = new System.Drawing.Size(23, 22);
            this.tsbShowRoomObjects.Text = "Отображать метки объектов";
            this.tsbShowRoomObjects.CheckedChanged += new System.EventHandler(this.TsbShowRoomObjectsCheckedChanged);
            // 
            // tsbShowRoomNumbers
            // 
            this.tsbShowRoomNumbers.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbShowRoomNumbers.CheckOnClick = true;
            this.tsbShowRoomNumbers.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbShowRoomNumbers.Image = ((System.Drawing.Image)(resources.GetObject("tsbShowRoomNumbers.Image")));
            this.tsbShowRoomNumbers.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbShowRoomNumbers.Name = "tsbShowRoomNumbers";
            this.tsbShowRoomNumbers.Size = new System.Drawing.Size(23, 22);
            this.tsbShowRoomNumbers.Text = "Отображать номера комнат на карте";
            this.tsbShowRoomNumbers.CheckedChanged += new System.EventHandler(this.TsbShowRoomNumbersCheckedChanged);
            // 
            // tsbShowRoomDetails
            // 
            this.tsbShowRoomDetails.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbShowRoomDetails.Checked = true;
            this.tsbShowRoomDetails.CheckOnClick = true;
            this.tsbShowRoomDetails.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbShowRoomDetails.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbShowRoomDetails.Image = global::BZEditor.Properties.Resources.button_details;
            this.tsbShowRoomDetails.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbShowRoomDetails.Name = "tsbShowRoomDetails";
            this.tsbShowRoomDetails.Size = new System.Drawing.Size(23, 22);
            this.tsbShowRoomDetails.Text = "Детальная информация о комнате под курсором";
            this.tsbShowRoomDetails.ToolTipText = "Детальная информация о комнате под курсором";
            this.tsbShowRoomDetails.CheckedChanged += new System.EventHandler(this.TsbShowRoomDetailsCheckedChanged);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbSketchColor
            // 
            this.tsbSketchColor.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbSketchColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSketchColor.Image = global::BZEditor.Properties.Resources.Выбор_цвета;
            this.tsbSketchColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSketchColor.Name = "tsbSketchColor";
            this.tsbSketchColor.Size = new System.Drawing.Size(23, 22);
            this.tsbSketchColor.Text = "Текущий цвет кисти эскиза";
            this.tsbSketchColor.Visible = false;
            this.tsbSketchColor.Click += new System.EventHandler(this.TsbSketchColorClick);
            // 
            // tsbBrush
            // 
            this.tsbBrush.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbBrush.CheckOnClick = true;
            this.tsbBrush.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbBrush.Image = global::BZEditor.Properties.Resources.brush2;
            this.tsbBrush.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBrush.Name = "tsbBrush";
            this.tsbBrush.Size = new System.Drawing.Size(23, 22);
            this.tsbBrush.Text = "Набросок зоны";
            this.tsbBrush.CheckedChanged += new System.EventHandler(this.TsbBrushCheckedChanged);
            // 
            // tsbCreateRoomsBySketch
            // 
            this.tsbCreateRoomsBySketch.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbCreateRoomsBySketch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCreateRoomsBySketch.Image = global::BZEditor.Properties.Resources.wand;
            this.tsbCreateRoomsBySketch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCreateRoomsBySketch.Name = "tsbCreateRoomsBySketch";
            this.tsbCreateRoomsBySketch.Size = new System.Drawing.Size(23, 22);
            this.tsbCreateRoomsBySketch.Text = "Сгенерировать комнаты по эскизу";
            this.tsbCreateRoomsBySketch.Click += new System.EventHandler(this.TsbCreateRoomsBySketchClick);
            // 
            // tsbClearSketch
            // 
            this.tsbClearSketch.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbClearSketch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbClearSketch.Image = global::BZEditor.Properties.Resources.ластик;
            this.tsbClearSketch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClearSketch.Name = "tsbClearSketch";
            this.tsbClearSketch.Size = new System.Drawing.Size(23, 22);
            this.tsbClearSketch.Text = "Удалить набросок зоны";
            this.tsbClearSketch.Click += new System.EventHandler(this.TsbClearSketchClick);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbGenerateMap
            // 
            this.tsbGenerateMap.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbGenerateMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbGenerateMap.Image = global::BZEditor.Properties.Resources.zonengenerate;
            this.tsbGenerateMap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbGenerateMap.Name = "tsbGenerateMap";
            this.tsbGenerateMap.Size = new System.Drawing.Size(23, 22);
            this.tsbGenerateMap.Text = "Разместить комнаты на карте по выходам";
            this.tsbGenerateMap.Click += new System.EventHandler(this.TsbGenerateMapClick);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // WldForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 810);
            this.Controls.Add(this.splitContainerBase);
            this.Controls.Add(this.toolStripMain);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WldForm";
            this.TabText = "WldForm";
            this.Text = "WldForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WldForm_FormClosing);
            gbOthers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudPResist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMResist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAResist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudArmour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdsorb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudInitiative)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSuccess)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCastSuccess)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRegeneration)).EndInit();
            gbResists.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudResistDark)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudResEarth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudResAir)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudResWater)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudResFire)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVitality)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMind)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudImmun)).EndInit();
            this.cmsMainTree.ResumeLayout(false);
            this.cmsNavigation.ResumeLayout(false);
            this.cmsGridMenu.ResumeLayout(false);
            this.cmsRoomsDescription.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudOptimalCharsInGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxInRoom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjContainerKeyVNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjContainerValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMobHitroll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMobAC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMobMaxInWorld)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMobWeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMobSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMobExpa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMobHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMobLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSaveFightSkills)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSaveMagDam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSaveParalyze)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSaveMagBreathe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVirtualRoomMobMaxInRoom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjMinRemorts)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.tabPage7.ResumeLayout(false);
            this.tabPage16.ResumeLayout(false);
            this.tabPage17.ResumeLayout(false);
            this.tabPage18.ResumeLayout(false);
            this.tabPage19.ResumeLayout(false);
            this.tabPage20.ResumeLayout(false);
            this.cmsCodeEditor.ResumeLayout(false);
            this.splitContainerBase.Panel1.ResumeLayout(false);
            this.splitContainerBase.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerBase)).EndInit();
            this.splitContainerBase.ResumeLayout(false);
            this.splitContainerMap.Panel1.ResumeLayout(false);
            this.splitContainerMap.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMap)).EndInit();
            this.splitContainerMap.ResumeLayout(false);
            this.tcListAndInfo.ResumeLayout(false);
            this.tpList.ResumeLayout(false);
            this.tpList.PerformLayout();
            this.tpInfo.ResumeLayout(false);
            this.pnlMapHorizSplitter.ResumeLayout(false);
            this.tcMain.ResumeLayout(false);
            this.tpZone.ResumeLayout(false);
            this.splitContainerZon.Panel1.ResumeLayout(false);
            this.splitContainerZon.Panel1.PerformLayout();
            this.splitContainerZon.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerZon)).EndInit();
            this.splitContainerZon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudZoneLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudZoneNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRepopTimer)).EndInit();
            this.tcZon.ResumeLayout(false);
            this.tpVitrualRoom.ResumeLayout(false);
            this.splitContainerVirtualRoomMobs.Panel1.ResumeLayout(false);
            this.splitContainerVirtualRoomMobs.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerVirtualRoomMobs)).EndInit();
            this.splitContainerVirtualRoomMobs.ResumeLayout(false);
            this.scontMobInVitrualRoomLoadedObjects.Panel1.ResumeLayout(false);
            this.scontMobInVitrualRoomLoadedObjects.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scontMobInVitrualRoomLoadedObjects)).EndInit();
            this.scontMobInVitrualRoomLoadedObjects.ResumeLayout(false);
            this.tpResetCondition.ResumeLayout(false);
            this.gbResetRelatedZones.ResumeLayout(false);
            this.splitContainerRepop.Panel1.ResumeLayout(false);
            this.splitContainerRepop.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRepop)).EndInit();
            this.splitContainerRepop.ResumeLayout(false);
            this.tpStatistics.ResumeLayout(false);
            this.tpStatistics.PerformLayout();
            this.tpRooms.ResumeLayout(false);
            this.splitContainerRoomsDesc.Panel1.ResumeLayout(false);
            this.splitContainerRoomsDesc.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRoomsDesc)).EndInit();
            this.splitContainerRoomsDesc.ResumeLayout(false);
            this.splitContainerRooms.Panel1.ResumeLayout(false);
            this.splitContainerRooms.Panel1.PerformLayout();
            this.splitContainerRooms.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRooms)).EndInit();
            this.splitContainerRooms.ResumeLayout(false);
            this.gboxExits.ResumeLayout(false);
            this.tcRoom.ResumeLayout(false);
            this.tpRoomDoors.ResumeLayout(false);
            this.pDoors.ResumeLayout(false);
            this.pDoors.PerformLayout();
            this.gbDoorType.ResumeLayout(false);
            this.gbDoorType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLockLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDoorKeyVNum)).EndInit();
            this.tpRoomFlags.ResumeLayout(false);
            this.tpRoomFlags.PerformLayout();
            this.tpRoomObjs.ResumeLayout(false);
            this.splitContainerRoomObjects.Panel1.ResumeLayout(false);
            this.splitContainerRoomObjects.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRoomObjects)).EndInit();
            this.splitContainerRoomObjects.ResumeLayout(false);
            this.gbObjInObj.ResumeLayout(false);
            this.tpRoomMobs.ResumeLayout(false);
            this.splitContainerRoomMobs.Panel1.ResumeLayout(false);
            this.splitContainerRoomMobs.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRoomMobs)).EndInit();
            this.splitContainerRoomMobs.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpMobObjectsLoaded.ResumeLayout(false);
            this.tpMobObjectsLoadedAfterDeath.ResumeLayout(false);
            this.tpRoomTrgs.ResumeLayout(false);
            this.tpRoomDelObjs.ResumeLayout(false);
            this.tpRoomAddDescrs.ResumeLayout(false);
            this.tpRoomAddDescrs.PerformLayout();
            this.tpRoomIngrediens.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tabControlRoomDescriptions.ResumeLayout(false);
            this.pnlFormating.ResumeLayout(false);
            this.tpObjects.ResumeLayout(false);
            this.splitContainerObj.Panel1.ResumeLayout(false);
            this.splitContainerObj.Panel1.PerformLayout();
            this.splitContainerObj.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerObj)).EndInit();
            this.splitContainerObj.ResumeLayout(false);
            this.tcObject.ResumeLayout(false);
            this.tpObjParams.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudObjMaxInWorld)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjRentPriceInv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjTimer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjWeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjRentPriceEquip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjCurStructHits)).EndInit();
            this.gbObjType.ResumeLayout(false);
            this.pObjMagIngr.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudObjMagIngrUseRemain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjMagIngrPrototype)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjMagIngrMinLev)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjMagIngrLag)).EndInit();
            this.pObjMagWand.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudObjMagWandZarCntCurr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjMagWandZarCnt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjMagWandMinLev)).EndInit();
            this.pObjWeapon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudObjWeaponD2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjWeaponD1)).EndInit();
            this.pObjPotion.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudObjPotionMinLev)).EndInit();
            this.pObjMoney.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudObjMoneyValue)).EndInit();
            this.pObjMagStaff.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudObjMagStaffZarCntCur)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjMagStaffZarCnt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjMagStaffMinLev)).EndInit();
            this.pObjMagicScroll.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudObjMagScrollMinLev)).EndInit();
            this.pObjMagBook.ResumeLayout(false);
            this.pObjLiquidContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudPotionProtoVNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjLiquidContainerCurVal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjLiquidContainerMaxVal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjLiquidContainerPoison)).EndInit();
            this.pObjLighter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudObjLighterValue)).EndInit();
            this.pObjFood.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudObjFoodPoison)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjFoodVal)).EndInit();
            this.pObjFontan.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudFontPorionProtoVNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjFontanCurVal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjFontanMaxVal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjFontanPoison)).EndInit();
            this.pObjContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudObjLockVal)).EndInit();
            this.pObjBandage.ResumeLayout(false);
            this.pObjBandage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjBandageValue)).EndInit();
            this.pObjArmor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudObjArmorArm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjArmorAC)).EndInit();
            this.tpObjEffects.ResumeLayout(false);
            this.tpObjAffects.ResumeLayout(false);
            this.tpObjWearTo.ResumeLayout(false);
            this.tpObjCantTouch.ResumeLayout(false);
            this.tpObjCantUse.ResumeLayout(false);
            this.tpObjTriggers.ResumeLayout(false);
            this.tpObjAddDescs.ResumeLayout(false);
            this.tpObjAddDescs.PerformLayout();
            this.tpObjAddAffects.ResumeLayout(false);
            this.splitContainerAddAff.Panel1.ResumeLayout(false);
            this.splitContainerAddAff.Panel2.ResumeLayout(false);
            this.splitContainerAddAff.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerAddAff)).EndInit();
            this.splitContainerAddAff.ResumeLayout(false);
            this.toolStripObjAddBonuses.ResumeLayout(false);
            this.toolStripObjAddBonuses.PerformLayout();
            this.tpObjSkillBonus.ResumeLayout(false);
            this.splitContainerSkillBonus.Panel1.ResumeLayout(false);
            this.splitContainerSkillBonus.Panel2.ResumeLayout(false);
            this.splitContainerSkillBonus.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerSkillBonus)).EndInit();
            this.splitContainerSkillBonus.ResumeLayout(false);
            this.toolStripObjSkillBonuses.ResumeLayout(false);
            this.toolStripObjSkillBonuses.PerformLayout();
            this.tpMobs.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainerMob.Panel1.ResumeLayout(false);
            this.splitContainerMob.Panel1.PerformLayout();
            this.splitContainerMob.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMob)).EndInit();
            this.splitContainerMob.ResumeLayout(false);
            this.tcMobs.ResumeLayout(false);
            this.tpMobParameters.ResumeLayout(false);
            this.tpMobParameters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMobMaxFactor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMobLikeWork)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMobCha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMobDex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMobInt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMobExtraAttack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMobCon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMobWis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMobStr)).EndInit();
            this.tpMobSkills.ResumeLayout(false);
            this.splitContainerMobSkills.Panel1.ResumeLayout(false);
            this.splitContainerMobSkills.Panel2.ResumeLayout(false);
            this.splitContainerMobSkills.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMobSkills)).EndInit();
            this.splitContainerMobSkills.ResumeLayout(false);
            this.toolStripMobSkills.ResumeLayout(false);
            this.toolStripMobSkills.PerformLayout();
            this.tpMobSpells.ResumeLayout(false);
            this.splitContainerMobSpells.Panel1.ResumeLayout(false);
            this.splitContainerMobSpells.Panel2.ResumeLayout(false);
            this.splitContainerMobSpells.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMobSpells)).EndInit();
            this.splitContainerMobSpells.ResumeLayout(false);
            this.toolStripMobSpells.ResumeLayout(false);
            this.toolStripMobSpells.PerformLayout();
            this.tpMobFeatures.ResumeLayout(false);
            this.tpMobAffects.ResumeLayout(false);
            this.tpMobFlags.ResumeLayout(false);
            this.tpMobSpecFlags.ResumeLayout(false);
            this.tpMobHelpers.ResumeLayout(false);
            this.tpMobTriggers.ResumeLayout(false);
            this.tpMobResists.ResumeLayout(false);
            this.gbSaves.ResumeLayout(false);
            this.tpMobRoles.ResumeLayout(false);
            this.tpMobIngredients.ResumeLayout(false);
            this.pnlAddMobDesc.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tpTriggers.ResumeLayout(false);
            this.splitContainerTrg.Panel1.ResumeLayout(false);
            this.splitContainerTrg.Panel2.ResumeLayout(false);
            this.splitContainerTrg.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerTrg)).EndInit();
            this.splitContainerTrg.ResumeLayout(false);
            this.tcTriggers.ResumeLayout(false);
            this.tpTrgParams.ResumeLayout(false);
            this.tpTrgParams.PerformLayout();
            this.gbObjectsToCreate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudTrgNumArg)).EndInit();
            this.tpTrgGlobalVars.ResumeLayout(false);
            this.toolStripPanelTrgTop.ResumeLayout(false);
            this.toolStripPanelTrgTop.PerformLayout();
            this.toolStripTrgEditor.ResumeLayout(false);
            this.toolStripTrgEditor.PerformLayout();
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public ExtControls.WldMap wldMap;
        public System.Windows.Forms.SplitContainer splitContainerMap;
        public System.Windows.Forms.SplitContainer splitContainerBase;
        public System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        public System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        public System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        public System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        public System.Windows.Forms.ToolStripContentPanel ContentPanel;
        public System.Windows.Forms.ToolStrip toolStripMain;
        public System.Windows.Forms.ToolStripButton tsbAutolinkingX;
        public System.Windows.Forms.ToolTip toolTip;
        public ExtControls.SICFListView lvMainList;
        public System.Windows.Forms.ColumnHeader chMainListVNum;
        public System.Windows.Forms.ColumnHeader chMainListItemName;
        public System.Windows.Forms.TextBox tboxMainListFilter;
        public System.Windows.Forms.Label label29;
        public System.Windows.Forms.Label label51;
        public System.Windows.Forms.ComboBox cboxMainListConditions;
        public System.Windows.Forms.TabControl tcMain;
        public System.Windows.Forms.TabPage tpZone;
        public System.Windows.Forms.SplitContainer splitContainerZon;
        public System.Windows.Forms.Button btnChangeZoneNumber;
        public System.Windows.Forms.NumericUpDown nudZoneNumber;
        public System.Windows.Forms.NumericUpDown nudRepopTimer;
        public System.Windows.Forms.Label label69;
        public System.Windows.Forms.Label label63;
        public System.Windows.Forms.Label label64;
        public System.Windows.Forms.Label label65;
        public System.Windows.Forms.ComboBox cbZoneReopopType;
        public System.Windows.Forms.TextBox tbZoneName;
        public System.Windows.Forms.TabControl tcZon;
        public System.Windows.Forms.TabPage tpStatistics;
        public System.Windows.Forms.TabPage tpRooms;
        public System.Windows.Forms.SplitContainer splitContainerRooms;
        public System.Windows.Forms.GroupBox gboxExits;
        public System.Windows.Forms.Button bSelectExitDown;
        public System.Windows.Forms.Button bSelectExitSouth;
        public System.Windows.Forms.Button bSelectExitEast;
        public System.Windows.Forms.Button bSelectExitWest;
        public System.Windows.Forms.Button bSelectExitUp;
        public System.Windows.Forms.Button bSelectExitNorth;
        public System.Windows.Forms.ComboBox cboxSectorType;
        public System.Windows.Forms.Label label81;
        public System.Windows.Forms.TextBox tbRoomName;
        public System.Windows.Forms.Label label80;
        public System.Windows.Forms.TabControl tabControlRoomDescriptions;
        public System.Windows.Forms.TabPage tpRoomDesc;
        public CExtRichTextBox rtbRoomDesc;
        public System.Windows.Forms.TabPage tpRoomDescDay;
        public System.Windows.Forms.TabPage tpRoomDescNight;
        public System.Windows.Forms.TabPage tpRoomDescWinterDay;
        public System.Windows.Forms.TabPage tpRoomDescWinterNight;
        public System.Windows.Forms.TabPage tpRoomDescSpringDay;
        public System.Windows.Forms.TabPage tpRoomDescSpringNight;
        public System.Windows.Forms.TabPage tpRoomDescSummerDay;
        public System.Windows.Forms.TabPage tpRoomDescSummerNight;
        public System.Windows.Forms.TabPage tpRoomDescAutumnDay;
        public System.Windows.Forms.TabPage tpRoomDescAutumnNight;
        public System.Windows.Forms.TabPage tpObjects;
        public System.Windows.Forms.SplitContainer splitContainerObj;
        public System.Windows.Forms.TabControl tcObject;
        public System.Windows.Forms.TabPage tpObjParams;
        public System.Windows.Forms.ComboBox cboxObjSkill;
        public System.Windows.Forms.NumericUpDown nudObjMaxInWorld;
        public System.Windows.Forms.ComboBox cboxObjMatherial;
        public System.Windows.Forms.NumericUpDown nudObjRentPriceInv;
        public System.Windows.Forms.ComboBox cboxObjTimerUOM;
        public System.Windows.Forms.TextBox tboxObjTvor;
        public System.Windows.Forms.NumericUpDown nudObjTimer;
        public System.Windows.Forms.TextBox tboxObjAliases;
        public System.Windows.Forms.TextBox tboxObjVin;
        public System.Windows.Forms.ComboBox cboxObjectGender;
        public System.Windows.Forms.TextBox tboxObjDat;
        public System.Windows.Forms.TextBox tboxObjRod;
        public System.Windows.Forms.NumericUpDown nudObjWeight;
        public System.Windows.Forms.TextBox tboxObjImen;
        public System.Windows.Forms.TextBox tboxObjPredl;
        public System.Windows.Forms.NumericUpDown nudObjPrice;
        public System.Windows.Forms.TextBox tboxObjDesc;
        public System.Windows.Forms.NumericUpDown nudObjRentPriceEquip;
        public System.Windows.Forms.ComboBox cboxObjMaxStructHits;
        public System.Windows.Forms.NumericUpDown nudObjCurStructHits;
        public System.Windows.Forms.TabPage tpObjEffects;
        public System.Windows.Forms.TabPage tpObjWearTo;
        public System.Windows.Forms.TabPage tpObjAffects;
        public System.Windows.Forms.TabPage tpObjCantTouch;
        public System.Windows.Forms.TabPage tpObjCantUse;
        public System.Windows.Forms.TabPage tpObjTriggers;
        public System.Windows.Forms.TabPage tpObjAddDescs;
        public System.Windows.Forms.TabPage tpObjAddAffects;
        public System.Windows.Forms.TabPage tpMobs;
        public System.Windows.Forms.SplitContainer splitContainerMob;
        public System.Windows.Forms.CheckBox cborMobRemoveOnReload;
        public System.Windows.Forms.TextBox tboxMobNameTvor;
        public System.Windows.Forms.ComboBox cboxMobSex;
        public System.Windows.Forms.TextBox tboxMobAliases;
        public System.Windows.Forms.TextBox tboxMobNameVin;
        public System.Windows.Forms.TextBox tboxMobNameDat;
        public System.Windows.Forms.TextBox tboxMobNameRod;
        public System.Windows.Forms.TextBox tboxMobNameImen;
        public System.Windows.Forms.TextBox tboxMobNamePred;
        public System.Windows.Forms.TextBox tboxMobDesc;
        public System.Windows.Forms.TabControl tcMobs;
        public System.Windows.Forms.TabPage tpMobParameters;
        public System.Windows.Forms.NumericUpDown nudMobMaxInWorld;
        public System.Windows.Forms.ComboBox cboxMobDefPosition;
        public System.Windows.Forms.NumericUpDown nudMobExpa;
        public System.Windows.Forms.ComboBox cboxMobStartPosition;
        public System.Windows.Forms.Button btnSelectMobPath;
        public System.Windows.Forms.TextBox tboxMobDestination;
        public UcDiceControl dctrlMobHP;
        public System.Windows.Forms.ComboBox cboxMobAlign;
        public System.Windows.Forms.Label label34;
        public UcDiceControl dctrlMobAttack;
        public System.Windows.Forms.ComboBox cboxMobAttackType;
        public UcDiceControl dctrlMobMoney;
        public System.Windows.Forms.TabPage tpMobHelpers;
        public System.Windows.Forms.TabPage tpMobAffects;
        public System.Windows.Forms.TabPage tpMobSpecFlags;
        public System.Windows.Forms.TabPage tpMobFlags;
        public System.Windows.Forms.TabPage tpMobSkills;
        public System.Windows.Forms.TabPage tpMobTriggers;
        public System.Windows.Forms.TabPage tpMobFeatures;
        public System.Windows.Forms.TabPage tpTriggers;
        public System.Windows.Forms.SplitContainer splitContainerTrg;
        public System.Windows.Forms.TextBox tbTrgName;
        public System.Windows.Forms.TextBox tbTrgArgs;
        public System.Windows.Forms.Label label44;
        public System.Windows.Forms.NumericUpDown nudTrgNumArg;
        public System.Windows.Forms.ComboBox cboxTrgClass;
        public System.Windows.Forms.Label label58;
        public System.Windows.Forms.Label label61;
        public System.Windows.Forms.Label label62;
        public System.Windows.Forms.GroupBox gbObjectsToCreate;
        public System.Windows.Forms.ListView lvTrgActivationConditions;
        public System.Windows.Forms.ColumnHeader chTrgActCond;
        public System.Windows.Forms.ToolStripPanel toolStripPanelTrgTop;
        public System.Windows.Forms.ToolStripPanel toolStripPanelTrgRight;
        public System.Windows.Forms.ToolStripPanel toolStripPanelTrgBottom;
        public System.Windows.Forms.ToolStripPanel toolStripPanelTrgLeft;
        public System.Windows.Forms.ToolStrip toolStripTrgEditor;
        public System.Windows.Forms.ToolStripButton tsbTrgClear;
        public System.Windows.Forms.ToolStripButton tsbTrgUndo;
        public System.Windows.Forms.ToolStripButton tsbTrgRedo;
        public System.Windows.Forms.TabPage tpMobResists;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        public System.Windows.Forms.NumericUpDown nudMobDex;
        public System.Windows.Forms.NumericUpDown nudMobInt;
        public System.Windows.Forms.NumericUpDown nudMobStr;
        public System.Windows.Forms.NumericUpDown nudMobSize;
        public System.Windows.Forms.NumericUpDown nudMobCha;
        public System.Windows.Forms.NumericUpDown nudMobLevel;
        public System.Windows.Forms.NumericUpDown nudMobHitroll;
        public System.Windows.Forms.NumericUpDown nudMobAC;
        public System.Windows.Forms.NumericUpDown nudMobMaxFactor;
        public System.Windows.Forms.TextBox tboxObjActionDesc;
        public System.Windows.Forms.NumericUpDown nudMobCon;
        public System.Windows.Forms.Button btnMobSetAutoCases;
        public System.Windows.Forms.NumericUpDown nudMobWis;
        public System.Windows.Forms.NumericUpDown nudMobHeight;
        public System.Windows.Forms.NumericUpDown nudMobWeight;
        public System.Windows.Forms.NumericUpDown nudMobLikeWork;
        public System.Windows.Forms.NumericUpDown nudMobExtraAttack;
        public System.Windows.Forms.Button btnObjSetAutoCases;
        public System.Windows.Forms.ListView lvMobHelpers;
        public System.Windows.Forms.ColumnHeader chMobHelperVNum;
        public System.Windows.Forms.ColumnHeader chMobHelperName;
        public System.Windows.Forms.ListView lvMobSkills;
        public System.Windows.Forms.ColumnHeader chMobSkillPercent;
        public System.Windows.Forms.ColumnHeader chMobSkillName;
        public System.Windows.Forms.TabPage tpMobSpells;
        public System.Windows.Forms.ColumnHeader chMobSpellCnt;
        public System.Windows.Forms.ColumnHeader chMobSpellName;
        public System.Windows.Forms.Button btnMobAddHelper;
        public System.Windows.Forms.Button btnRemoveHelpersList;
        public System.Windows.Forms.ListView lvMobSpells;
        public System.Windows.Forms.Label label30;
        public System.Windows.Forms.ComboBox cboxZonType;
        public System.Windows.Forms.Button btnAddMobTrigger;
        public System.Windows.Forms.Button btnMobRemoveTrigger;
        public System.Windows.Forms.ListView lvMobTriggers;
        public System.Windows.Forms.ColumnHeader columnHeader16;
        public System.Windows.Forms.ColumnHeader columnHeader17;
        public System.Windows.Forms.NumericUpDown nudSaveFightSkills;
        public System.Windows.Forms.NumericUpDown nudSaveMagDam;
        public System.Windows.Forms.NumericUpDown nudSaveMagBreathe;
        public System.Windows.Forms.NumericUpDown nudSaveParalyze;
        public System.Windows.Forms.NumericUpDown nudVitality;
        public System.Windows.Forms.NumericUpDown nudResEarth;
        public System.Windows.Forms.NumericUpDown nudResAir;
        public System.Windows.Forms.NumericUpDown nudResWater;
        public System.Windows.Forms.NumericUpDown nudResFire;
        public System.Windows.Forms.NumericUpDown nudMem;
        public System.Windows.Forms.NumericUpDown nudMResist;
        public System.Windows.Forms.NumericUpDown nudMind;
        public System.Windows.Forms.NumericUpDown nudRegeneration;
        public System.Windows.Forms.NumericUpDown nudArmour;
        public System.Windows.Forms.NumericUpDown nudAdsorb;
        public System.Windows.Forms.NumericUpDown nudImmun;
        public System.Windows.Forms.NumericUpDown nudCastSuccess;
        public System.Windows.Forms.NumericUpDown nudInitiative;
        public System.Windows.Forms.NumericUpDown nudSuccess;
        public System.Windows.Forms.Button btnAddObjTrigger;
        public System.Windows.Forms.Button btnObjRemoveTrigger;
        public System.Windows.Forms.ListView lvObjTriggers;
        public System.Windows.Forms.ColumnHeader columnHeader18;
        public System.Windows.Forms.ColumnHeader columnHeader19;
        public System.Windows.Forms.Button btnObjAddAddDesc;
        public System.Windows.Forms.Button btnObjRemoveAddDesc;
        public System.Windows.Forms.ListView lvObjAddDesc;
        public System.Windows.Forms.ColumnHeader columnHeader24;
        public System.Windows.Forms.ColumnHeader columnHeader25;
        public System.Windows.Forms.ColumnHeader chObjAddAffectPercent;
        public System.Windows.Forms.ColumnHeader chObjAddAffectName;
        public System.Windows.Forms.TextBox tboxAddDescAliases;
        public CExtRichTextBox rtbObjAddDesc;
        public UcTwoPanelsList tplMobFeats;
        public UcTwoPanelsList tplMobAffects;
        public UcTwoPanelsList tplMobFlags;
        public UcTwoPanelsList tplMobSpecFlags;
        public UcTwoPanelsList tplObjEffects;
        public UcTwoPanelsList tplObjAffects;
        public UcTwoPanelsList tplObjWearTo;
        public UcTwoPanelsList tplObjCantTouch;
        public UcTwoPanelsList tplObjCantUse;
        public System.Windows.Forms.NumericUpDown nudZoneLevel;
        public System.Windows.Forms.GroupBox gbObjType;
        public System.Windows.Forms.Panel pObjMagIngr;
        public System.Windows.Forms.ListView lvObjMagIngrFlags;
        public System.Windows.Forms.ColumnHeader columnHeader4;
        public System.Windows.Forms.NumericUpDown nudObjMagIngrUseRemain;
        public System.Windows.Forms.NumericUpDown nudObjMagIngrPrototype;
        public System.Windows.Forms.NumericUpDown nudObjMagIngrMinLev;
        public System.Windows.Forms.NumericUpDown nudObjMagIngrLag;
        public System.Windows.Forms.Panel pObjFontan;
        public System.Windows.Forms.ComboBox cboxObjFontanDrinkType;
        public System.Windows.Forms.NumericUpDown nudObjFontanCurVal;
        public System.Windows.Forms.NumericUpDown nudObjFontanMaxVal;
        public System.Windows.Forms.NumericUpDown nudObjFontanPoison;
        public System.Windows.Forms.Panel pObjMoney;
        public System.Windows.Forms.NumericUpDown nudObjMoneyValue;
        public System.Windows.Forms.Panel pObjFood;
        public System.Windows.Forms.NumericUpDown nudObjFoodPoison;
        public System.Windows.Forms.NumericUpDown nudObjFoodVal;
        public System.Windows.Forms.Panel pObjContainer;
        public System.Windows.Forms.ListView lvObjContainerFlags;
        public System.Windows.Forms.ColumnHeader columnHeader5;
        public System.Windows.Forms.NumericUpDown nudObjContainerKeyVNum;
        public System.Windows.Forms.NumericUpDown nudObjContainerValue;
        public System.Windows.Forms.Panel pObjArmor;
        public System.Windows.Forms.NumericUpDown nudObjArmorArm;
        public System.Windows.Forms.NumericUpDown nudObjArmorAC;
        public System.Windows.Forms.Label label146;
        public System.Windows.Forms.Panel pObjPotion;
        public System.Windows.Forms.ComboBox cboxObjPotionSpell2;
        public System.Windows.Forms.ComboBox cboxObjPotionSpell3;
        public System.Windows.Forms.ComboBox cboxObjPotionSpell1;
        public System.Windows.Forms.NumericUpDown nudObjPotionMinLev;
        public System.Windows.Forms.Label label148;
        public System.Windows.Forms.Label label149;
        public System.Windows.Forms.Label label151;
        public System.Windows.Forms.Panel pObjWeapon;
        public System.Windows.Forms.ComboBox cboxObjWeaponSrikeType;
        public System.Windows.Forms.NumericUpDown nudObjWeaponD2;
        public System.Windows.Forms.NumericUpDown nudObjWeaponD1;
        public System.Windows.Forms.Label label144;
        public System.Windows.Forms.Panel pObjMagicScroll;
        public System.Windows.Forms.ComboBox cboxObjMagScrollSpell2;
        public System.Windows.Forms.ComboBox cboxObjMagScrollSpell3;
        public System.Windows.Forms.ComboBox cboxObjMagScrollSpell1;
        public System.Windows.Forms.NumericUpDown nudObjMagScrollMinLev;
        public System.Windows.Forms.Label label141;
        public System.Windows.Forms.Label label153;
        public System.Windows.Forms.Label label155;
        public System.Windows.Forms.Panel pObjMagBook;
        public System.Windows.Forms.ComboBox cboxObjMagBookSpell;
        public System.Windows.Forms.Label label156;
        public System.Windows.Forms.Panel pObjMagStaff;
        public System.Windows.Forms.ComboBox cboxObjMagStaffSpell;
        public System.Windows.Forms.NumericUpDown nudObjMagStaffZarCntCur;
        public System.Windows.Forms.NumericUpDown nudObjMagStaffZarCnt;
        public System.Windows.Forms.NumericUpDown nudObjMagStaffMinLev;
        public System.Windows.Forms.Label label159;
        public System.Windows.Forms.Panel pObjLiquidContainer;
        public System.Windows.Forms.ComboBox cboxObjLiquidContainerDrinkType;
        public System.Windows.Forms.NumericUpDown nudObjLiquidContainerCurVal;
        public System.Windows.Forms.NumericUpDown nudObjLiquidContainerMaxVal;
        public System.Windows.Forms.NumericUpDown nudObjLiquidContainerPoison;
        public System.Windows.Forms.Panel pObjMagWand;
        public System.Windows.Forms.ComboBox cboxObjMagWandSpell;
        public System.Windows.Forms.NumericUpDown nudObjMagWandZarCntCurr;
        public System.Windows.Forms.NumericUpDown nudObjMagWandZarCnt;
        public System.Windows.Forms.NumericUpDown nudObjMagWandMinLev;
        public System.Windows.Forms.Label label168;
        public System.Windows.Forms.Label label35;
        public System.Windows.Forms.Panel pObjLighter;
        public System.Windows.Forms.NumericUpDown nudObjLighterValue;
        public System.Windows.Forms.ErrorProvider errorProvider;
        public System.Windows.Forms.ToolStripButton tsbMapZoomIn;
        public System.Windows.Forms.ToolStripButton tsbMapZoomOut;
        public System.Windows.Forms.ComboBox cboxMobClass;
        public System.Windows.Forms.Button btnObjReplaceAddDesc;
        public System.Windows.Forms.Label lObjAverageDam;
        public System.Windows.Forms.TabControl tcRoom;
        public System.Windows.Forms.TabPage tpRoomFlags;
        public System.Windows.Forms.CheckBox cbShowRoomsWithFlags;
        public System.Windows.Forms.TabPage tpRoomDoors;
        public System.Windows.Forms.Panel pDoors;
        public System.Windows.Forms.NumericUpDown nudDoorKeyVNum;
        public System.Windows.Forms.Label label92;
        public System.Windows.Forms.Button btnConfigExitDown;
        public System.Windows.Forms.Button btnConfigExitSouth;
        public System.Windows.Forms.Button btnConfigExitEast;
        public System.Windows.Forms.Button btnConfigExitWest;
        public System.Windows.Forms.Button btnConfigExitUp;
        public System.Windows.Forms.Button btnConfigExitNorth;
        public System.Windows.Forms.TextBox tbDoorDesc;
        public System.Windows.Forms.TextBox tbDoorAlias;
        public System.Windows.Forms.Label label90;
        public System.Windows.Forms.Label label86;
        public System.Windows.Forms.Label label89;
        public System.Windows.Forms.TabPage tpRoomTrgs;
        public System.Windows.Forms.Button btnAddRoomTrigger;
        public System.Windows.Forms.Button btnRemoveRoomTrigger;
        public System.Windows.Forms.ListView lvRoomTriggers;
        public System.Windows.Forms.ColumnHeader columnHeader6;
        public System.Windows.Forms.ColumnHeader columnHeader7;
        public System.Windows.Forms.TabPage tpRoomObjs;
        public System.Windows.Forms.SplitContainer splitContainerRoomObjects;
        public System.Windows.Forms.TabPage tpRoomMobs;
        public System.Windows.Forms.SplitContainer splitContainerRoomMobs;
        public System.Windows.Forms.ListView lvMobsInRoom;
        public System.Windows.Forms.NumericUpDown nudMaxInRoom;
        public System.Windows.Forms.ComboBox cboxMobFollowBy;
        public System.Windows.Forms.Label label85;
        public System.Windows.Forms.Label label84;
        public System.Windows.Forms.TabPage tpRoomDelObjs;
        public System.Windows.Forms.ListView lvObjectsToRemove;
        public System.Windows.Forms.TabPage tpRoomAddDescrs;
        public System.Windows.Forms.Button btnRoomAddObj;
        public System.Windows.Forms.Button btnRoomRemoveObj;
        public System.Windows.Forms.Button btnAddRoomObjectToRemove;
        public CExtRichTextBox rtbRoomAddDescText;
        public System.Windows.Forms.CheckBox cbRoomAddDescWordwrap;
        public System.Windows.Forms.Button btnAddRoomAddDesc;
        public System.Windows.Forms.Button btnRemoveRoomAddDesc;
        public System.Windows.Forms.ListView lvRoomAddDescriptions;
        public System.Windows.Forms.ColumnHeader columnHeader8;
        public System.Windows.Forms.ColumnHeader columnHeader12;
        public System.Windows.Forms.TextBox tbRoomAddDescAliases;
        public System.Windows.Forms.Button btnRoomAddMob;
        public System.Windows.Forms.Button btnRoomRemoveMob;
        public System.Windows.Forms.Button btnRoomAddObjInObj;
        public System.Windows.Forms.Button btnRoomRemoveObjFromObj;
        public UcTwoPanelsList tplRoomFlags;
        public System.Windows.Forms.Button btnRoomFormatCommonDesc;
        public System.Windows.Forms.Button btnRoomSpellCheckCommonDesc;
        public System.Windows.Forms.Button btnRoomSpecFormatCommonDesc;
        public System.Windows.Forms.CheckBox cbIsertSpaces;
        public System.Windows.Forms.CheckBox cbRoomDescAllowHyp;
        public System.Windows.Forms.CheckBox checkBox2;
        public System.Windows.Forms.CheckBox checkBox3;
        public System.Windows.Forms.CheckBox cbInsertSpaces;
        public System.Windows.Forms.CheckBox cbAllowHyp;
        public System.Windows.Forms.Panel pnlFormating;
        public System.Windows.Forms.TabPage tabPage1;
        public CExtRichTextBox CExtRichTextBox2;
        public System.Windows.Forms.TabPage tabPage3;
        public System.Windows.Forms.CheckBox checkBox4;
        public CExtRichTextBox CExtRichTextBox3;
        public System.Windows.Forms.TabPage tabPage4;
        public System.Windows.Forms.CheckBox checkBox6;
        public CExtRichTextBox CExtRichTextBox4;
        public System.Windows.Forms.TabPage tabPage5;
        public System.Windows.Forms.CheckBox checkBox7;
        public CExtRichTextBox CExtRichTextBox5;
        public System.Windows.Forms.TabPage tabPage6;
        public System.Windows.Forms.CheckBox checkBox8;
        public CExtRichTextBox CExtRichTextBox6;
        public System.Windows.Forms.TabPage tabPage7;
        public System.Windows.Forms.CheckBox checkBox9;
        public CExtRichTextBox CExtRichTextBox7;
        public System.Windows.Forms.TabPage tabPage16;
        public System.Windows.Forms.CheckBox checkBox10;
        public CExtRichTextBox CExtRichTextBox8;
        public System.Windows.Forms.TabPage tabPage17;
        public System.Windows.Forms.CheckBox checkBox11;
        public CExtRichTextBox CExtRichTextBox9;
        public System.Windows.Forms.TabPage tabPage18;
        public System.Windows.Forms.CheckBox checkBox12;
        public CExtRichTextBox CExtRichTextBox10;
        public System.Windows.Forms.TabPage tabPage19;
        public System.Windows.Forms.CheckBox checkBox13;
        public CExtRichTextBox CExtRichTextBox11;
        public System.Windows.Forms.TabPage tabPage20;
        public System.Windows.Forms.CheckBox checkBox14;
        public CExtRichTextBox CExtRichTextBox12;
        public System.Windows.Forms.ColumnHeader columnHeader14;
        public System.Windows.Forms.ColumnHeader columnHeader15;
        public Fireball.Windows.Forms.CodeEditorControl codeEditor;
        public Fireball.Syntax.SyntaxDocument syntaxDocument;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        public System.Windows.Forms.ToolStripButton tsbTrgGoToLine;
        public System.Windows.Forms.ToolStripButton tsbTrgSearch;
        public System.Windows.Forms.ToolStripButton tsbTrgSearchNext;
        public System.Windows.Forms.ToolStripButton tsbTrgReplace;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        public System.Windows.Forms.ToolStripButton tsbTrgCopy;
        public System.Windows.Forms.ToolStripButton tsbTrgCut;
        public System.Windows.Forms.ToolStripButton tsbTrgPaste;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        public System.Windows.Forms.ToolStripButton tsbTrgToggleBookmark;
        public System.Windows.Forms.ToolStripButton tsbTrgGoToPrevBookmark;
        public System.Windows.Forms.ToolStripButton tsbTrgGoToNextBookmark;
        public System.Windows.Forms.ToolStripButton toolStripButton18;
        public System.Windows.Forms.ToolStripButton tsbTrgIndent;
        public System.Windows.Forms.ToolStripButton tsbTrgOutdent;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        public System.Windows.Forms.ToolStripButton tsbRemoveItems;
        public System.Windows.Forms.ToolStripButton tsbAddItems;
        public ExtControls.ExtListView elvObjectsInRoom;
        public System.Windows.Forms.GroupBox gbObjInObj;
        public ExtControls.ExtListView elvRoomObjInObj;
        public System.Windows.Forms.ColumnHeader columnHeader20;
        public System.Windows.Forms.ColumnHeader columnHeader21;
        public Fireball.Windows.Forms.NumericBox tbExitSouth;
        public Fireball.Windows.Forms.NumericBox tbExitNorth;
        public Fireball.Windows.Forms.NumericBox tbExitEast;
        public Fireball.Windows.Forms.NumericBox tbExitWest;
        public Fireball.Windows.Forms.NumericBox tbExitDown;
        public Fireball.Windows.Forms.NumericBox tbExitUp;
        public System.Windows.Forms.TextBox tbRoomDoorKeyName;
        public System.Windows.Forms.Button btnSelectDoorKey;
        public System.Windows.Forms.ToolStripButton tsbAddTemplate;
        public System.Windows.Forms.ToolStripButton tsbCopy;
        public System.Windows.Forms.ToolStripButton tsbPaste;
        public System.Windows.Forms.ToolStripButton tsbPasteAsTemplate;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        public System.Windows.Forms.ToolStripButton tsbSaveZone;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        public System.Windows.Forms.ToolStripButton tsbSetOppositeExit;
        public System.Windows.Forms.ContextMenuStrip cmsGridMenu;
        public System.Windows.Forms.ToolStripMenuItem tsmiAddRow;
        public System.Windows.Forms.ToolStripMenuItem tsmiRemoveRow;
        public System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        public System.Windows.Forms.ToolStripMenuItem tsmiGoTo;
        public System.Windows.Forms.ToolStripButton tsbHistoryBack;
        public System.Windows.Forms.ToolStripButton tsbHistoryForward;
        public System.Windows.Forms.TabControl tcTriggers;
        public System.Windows.Forms.TabPage tpTrgParams;
        public System.Windows.Forms.TabPage tpTrgGlobalVars;
        public System.Windows.Forms.Button button1;
        public System.Windows.Forms.Button button2;
        public System.Windows.Forms.ListView listView1;
        public System.Windows.Forms.ColumnHeader columnHeader22;
        public System.Windows.Forms.ColumnHeader columnHeader23;
        public System.Windows.Forms.ToolStripSeparator toolStripSplitButton1;
        public System.Windows.Forms.ToolStripButton tsbMapZDec;
        public System.Windows.Forms.ToolStripButton tsbMapZInc;
        public System.Windows.Forms.ListView lvAvailAddAffects;
        public System.Windows.Forms.ColumnHeader chObjAddAffectAvail;
        public System.Windows.Forms.SplitContainer splitContainerAddAff;
        public System.Windows.Forms.ToolStripButton tsbObjAdditAffectAdd;
        public System.Windows.Forms.ToolStripButton tsbObjAdditAffectRemove;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        public System.Windows.Forms.ToolStripButton tsbShowRoomMobs;
        public System.Windows.Forms.ToolStripButton tsbShowRoomObjects;
        public System.Windows.Forms.ToolStripButton tsbShowRoomTriggers;
        public System.Windows.Forms.ToolStripButton tsbShowRoomNumbers;
        public System.Windows.Forms.Label lRoomDesc;
        public System.Windows.Forms.ListView lvZoneInfo;
        public System.Windows.Forms.ColumnHeader chParamName;
        public System.Windows.Forms.ColumnHeader chParamVal;
        public System.Windows.Forms.ToolStripButton tsbMapToZeroRom;
        public System.Windows.Forms.NumericUpDown nudPotionProtoVNum;
        public System.Windows.Forms.Button btnSelectPotionProtoVNum;
        public System.Windows.Forms.Button btnSelectFontPorionProto;
        public System.Windows.Forms.NumericUpDown nudFontPorionProtoVNum;
        public System.Windows.Forms.GroupBox gbSaves;
        public System.Windows.Forms.ContextMenuStrip cmsMainTree;
        public System.Windows.Forms.ToolStripMenuItem tsmiAddTemplate;
        public System.Windows.Forms.ToolStripMenuItem tsmiPasteAsTemplate;
        public System.Windows.Forms.ToolStripMenuItem tsmiCopy;
        public System.Windows.Forms.ToolStripMenuItem tsmiPaste;
        public System.Windows.Forms.ToolStripMenuItem tsmiCreateClones;
        public System.Windows.Forms.ToolStripButton tsbCreateClones;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        public System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        public System.Windows.Forms.ToolStripMenuItem tsmiAddItems;
        public System.Windows.Forms.ToolStripMenuItem tsmiRemoveItems;
        public System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        public System.Windows.Forms.ToolStrip toolStripObjAddBonuses;
        public System.Windows.Forms.ToolStripButton toolStripButton1;
        public System.Windows.Forms.Button btnRemoveRoomObjectToRemove;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
        private System.Windows.Forms.ToolStripButton tsbReloadWithoutSave;
        private System.Windows.Forms.TabPage tpList;
        private System.Windows.Forms.TabPage tpInfo;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem tsmiInfo;
        public ExtControls.SICFListView lvDetails;
        public System.Windows.Forms.ColumnHeader columnHeader29;
        public System.Windows.Forms.TabControl tcListAndInfo;
        private System.Windows.Forms.ContextMenuStrip cmsNavigation;
        private System.Windows.Forms.ToolStripMenuItem tsmiNavigateTo;
        private System.Windows.Forms.GroupBox gbResetRelatedZones;
        public System.Windows.Forms.Button btnAddAZones;
        public System.Windows.Forms.Button btnRemoveAZones;
        public System.Windows.Forms.ListView lvAZones;
        public System.Windows.Forms.ColumnHeader columnHeader30;
        public System.Windows.Forms.ColumnHeader columnHeader31;
        public System.Windows.Forms.Button btnAddBZones;
        public System.Windows.Forms.Button btnRemoveBZones;
        public System.Windows.Forms.ListView lvBZones;
        public System.Windows.Forms.ColumnHeader columnHeader32;
        public System.Windows.Forms.ColumnHeader columnHeader33;
        public System.Windows.Forms.Label label97;
        public System.Windows.Forms.Label label82;
        public System.Windows.Forms.TextBox tbDoorNameVin;
        public System.Windows.Forms.Label label99;
        private System.Windows.Forms.ToolStripSeparator toolStripButton2;
        private System.Windows.Forms.ToolStripButton tsbBrush;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton tsbSketchColor;
        private System.Windows.Forms.ImageList iListIcons16;
        private System.Windows.Forms.ToolStripButton tsbClearSketch;
        private System.Windows.Forms.ToolStripButton tsbCreateRoomsBySketch;
        private System.Windows.Forms.ToolStripSeparator toolStripButton4;
        private System.Windows.Forms.ToolStripButton tsbGenerateMap;
        private System.Windows.Forms.Button btnValidate;
        public System.Windows.Forms.Label label126;
        private MessageListBox mlbValidationResult;
        private CheckBox cbShowErrors;
        private CheckBox cbShowWarnings;
        private CheckBox cbShowInfo;
        private ToolStripMenuItem tsmiShowRoomOnMap;
        public ComboBox cboxMobRace;
        private GroupBox gbDoorType;
        private CheckBox cbExitHidden;
        private CheckBox cbDoorPeekproof;
        private CheckBox cbExitDoor;
        private TabPage tpObjSkillBonus;
        public SplitContainer splitContainerSkillBonus;
        public ListView lvSkillBonuses;
        public ColumnHeader chObjAddSkillPercent;
        public ColumnHeader chObjAddSkill;
        public ListView lvAvailSkillBonuses;
        public ColumnHeader chObjAddSkillAvail;
        public ToolStrip toolStripObjSkillBonuses;
        public ToolStripButton tsbAddSkillBonus;
        public ToolStripButton tsbRemoveSkillBonus;
        private ToolStripSeparator toolStripSeparator15;
        private ToolStripButton tsbEditAddAffectValue;
        private ToolStripSeparator toolStripSeparator16;
        private ToolStripButton tsbEditSkillBonus;
        public Label label70;
        private ToolStripButton tsbSaveCurTabData;
        private SplitContainer splitContainerRoomsDesc;
        public CheckBox cbDescReplace;
        private ToolStripButton tsbShowRoomDetails;
        private ToolStripButton tsbAutolinkingZ;
        private ToolStripButton tsbAutolinkingY;
        private ContextMenuStrip cmsRoomsDescription;
        private ToolStripMenuItem tsmiCopyDesc;
        private ToolStripMenuItem tsmiCutDesc;
        private ToolStripSeparator toolStripMenuItem5;
        private ToolStripMenuItem tsmiPasteDesc;
        public Label label91;
        public TextBox tbZoneComment;
        private SplitContainer splitContainerRepop;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton tsbInsertSpellNumber;
        public NumericUpDown nudPResist;
        public NumericUpDown nudAResist;
        public NumericUpDown nudOptimalCharsInGroup;
        public Label label96;
        private CheckBox cbDoorClosed;
        private CheckBox cbDoorLocked;
        private CheckBox cbExitVisible;
        private Label label93;
        private NumericUpDown nudObjLockVal;
        private NumericUpDown nudLockLevel;
        private Label label173;
        private NumericUpDown nudObjBandageValue;
        public Panel pObjBandage;
        private ContextMenuStrip cmsCodeEditor;
        private ToolStripMenuItem tsmiCodeEditorCopy;
        private ToolStripMenuItem tsmiCodeEditorCut;
        private ToolStripSeparator toolStripSeparator17;
        private ToolStripMenuItem tsmiCodeEditorPaste;
        private Panel pnlMapHorizSplitter;
        private Button btnToMapCenter;
        private VerticalScrollBar vertSBMap;
        private HorizontalScrollBar horizSBMap;
        public SplitContainer splitContainerMobSkills;
        public ListView lvAvailMobSkills;
        public ColumnHeader chMobSkillAvail;
        public ToolStrip toolStripMobSkills;
        public ToolStripButton tsbMobAddSkill;
        public ToolStripButton tsbMobRemoveSkill;
        private ToolStripSeparator toolStripSeparator18;
        private ToolStripButton tsbMobEditSkill;
        public SplitContainer splitContainerMobSpells;
        public ListView lvMobAvailSpells;
        public ColumnHeader chAvailMobSpellName;
        public ToolStrip toolStripMobSpells;
        public ToolStripButton tsbMobAddSpell;
        public ToolStripButton tsbMobRemoveSpell;
        private ToolStripSeparator toolStripSeparator19;
        private ToolStripButton tsbMobEditSpell;
        protected internal ListView lvObjBonuses;
        private TabPage tpVitrualRoom;
        public SplitContainer splitContainerVirtualRoomMobs;
        public Button bdtAddMobInVirtualRoom;
        public Button btnRemoveMobFromVitrualRoom;
        public ListView lvMobsInVitrualRoom;
        public ColumnHeader columnHeader1;
        public ColumnHeader columnHeader2;
        public ExtListView elvVitrualRoomMobObjects;
        public Button btnAddItemToMobInVirtualRoom;
        public Button btnRemoveItemFromMobInVirtualRoom;
        public NumericUpDown nudVirtualRoomMobMaxInRoom;
        public ComboBox cboxVitrualRoomMobFollowBy;
        public Label label40;
        public Label label43;
        public Label label57;
        private TabPage tpMobRoles;
        public UcTwoPanelsList tplMobRoles;
        public SplitContainer scontMobInVitrualRoomLoadedObjects;
        public ExtListView elvVitrualRoomMobObjectsAfterDeath;
        public Button btnRemoveItemFromMobInVirtualRoomAfterDeath;
        public Button btnAddItemToMobInVirtualRoomAfterDeath;
        public Label label15;
        private TabControl tabControl1;
        private TabPage tpMobObjectsLoaded;
        public ExtListView elvRoomMobObjects;
        public Button btnRoomAddObjToMob;
        public Button btnRoomRomoveObjFromMob;
        private TabPage tpMobObjectsLoadedAfterDeath;
        public Button btnRoomRomoveObjFromMobAfterDeath;
        public ExtListView elvRoomMobObjectsLoadingAfterDeath;
        public Button button3;
        private SplitContainer splitContainer1;
        public Panel panel1;
        public Button btnMobSpecFormatCommonDesc;
        public Button btnMobSpellCheckCommonDesc;
        public Button btnMobFormatCommonDesc;
        private Label label67;
        public CExtRichTextBox ertbMobDescription;
        private Panel pnlAddMobDesc;
        private Panel panel2;
        public ComboBox cboxMoneyCurrency;
        public CheckBox cbMustWordwrapAddDesc;
        public ComboBox cboxObjType;
        public NumericUpDown nudResistDark;
        public TextBox tbZoneLocation;
        public Label label72;
        public TextBox tbZoneDescription;
        public Label label73;
        public TextBox tbZoneAuthor;
        public Label label66;
        private TabPage tpResetCondition;
        public NumericUpDown nudObjMinRemorts;
        private TabPage tpRoomIngrediens;
        public Button btnAddRoomIngredient;
        public Button btnRemoveRoomIngredient;
        public ExtListView elvRoomIngredients;
        private TabPage tpMobIngredients;
        public ExtListView elvMobIngredients;
        public Button btnAddMobIngredient;
        public Button btnRemoveMobIngredient;
    }
}