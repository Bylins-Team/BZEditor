namespace ExtControls
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Text;
    using System.Windows.Forms;
    using DataUtils;
    using System.ComponentModel;

    public class WldMap : UserControl
    {
        #region Delegates

        public delegate void RoomCreateEvent();

        public delegate void PathChangeEvent();

        public delegate void RoomDropEvent();

        public delegate void MobDropEvent(int mobVnum, Room trgRoom);

        public delegate void ObjDropEvent(int objVnum, Room trgRoom);

        public delegate void RoomSelectEvent(int vNum);

        public delegate void RoomsSelectionChangeEvent(SelectedRoomsCollection rooms);

        public delegate void CenterRoomXValueChangeEvent(int newX);

        public delegate void CenterRoomYValueChangeEvent(int newY);
        
        #endregion

        #region Events

        public event RoomCreateEvent RoomCreated;

        public event RoomSelectEvent RoomSelected;

        public event PathChangeEvent PathChanged;

        public event RoomsSelectionChangeEvent RoomsSelectionChanged;

        public event RoomDropEvent RoomDroped;

        public event MobDropEvent MobDroped;

        public event ObjDropEvent ObjDroped;

        public event CenterRoomXValueChangeEvent CenterRoomXValueChanged;

        public event CenterRoomYValueChangeEvent CenterRoomYValueChanged;


        #endregion

        #region Internal

        private const int ConstGSize = 6;
        private readonly PictureBox thumbnail = new PictureBox();
        private Bitmap bmp;
        private bool altPushed;
        private bool controlPushed;
        private bool shiftPushed;
        private Room curRoom;
        private bool dragDataValid;
        private bool leftMouseBtnPushed;
        private Point mouseClickedPoint = new Point(-1, -1);
        private Point mouseCurrentPoint = new Point(-1, -1);
        private bool mustStartDragging;
        private bool rightBtnScrolling;
        private int roomSideSize = 16;
        private bool selectionStarted;

        private Point startPoint = new Point(0, 0);
        private int startPointVnum = -1;
        private ZoneDataManager zoneDm;

        #endregion

        #region Доступные пользователю параметры

        private const int RoomSelectionAlphaFactor = 65;
        private int selectionAlphaFactor = 50;
        private const SmoothingMode SmoothMode = SmoothingMode.AntiAlias;
        private readonly Color doorExitColor = Color.Blue;
        private readonly Color roomBgColorBottom = Color.Gainsboro;
        private readonly Color roomBgColorTop = Color.DarkGray;
        private readonly Color selectionRegionBorderColor = Color.DarkGray;
        private readonly Color simpleExitColor = Color.LightGray;
        private readonly bool solidGridLines;
        private readonly Color teleportExitColor = Color.Pink;
        private readonly Color zoneExitColor = Color.Red;
        private bool clearSketchAfterGeneratingRooms = true;
        private SelectedRoomsCollection highlightedRooms = new SelectedRoomsCollection();
        private SelectedRoomsCollection selectedRooms = new SelectedRoomsCollection();
        private bool autolinkingX = true;
        private bool autolinkingY = true;
        private bool autolinkingZ = true;
        private Point3D centerRoom = new Point3D(0, 0, 0);
        private bool drawSketchMode;
        private int exitScale = 3;
        private bool externalPathSelection;
        private bool externalRoomRoomSelection;
        private Color gridColor = SystemColors.ActiveBorder; //+
        private int mapScale = 4;
        private bool mustRecalcExitColors = true;
        private Color roomHighlightColor = Color.LightSalmon;
        private Color focusHighlightColor = SystemColors.ActiveCaption;
        private int roomHighlightingAlphaFactor = 150;
        private RoomsCollection roomsCollection = new RoomsCollection();
        public ListItemDescCollection ColorDescriptors = new ListItemDescCollection();
        protected int LastDragX;
        protected int LastDragY;

        //Рамка выбора

        //Комнаты
        private Color roomSelectionRegionBgColor = Color.Blue;

        private Color selectionRegionBgColor = Color.DarkSlateBlue;
        private bool showMob;
        private bool showObj;
        private bool showTrg;
        private bool showVNums;
        private bool roomDetailsVisible = true;

        //Сетка
        private bool showSketchMode = true;
        private SketchRoomsCollection sketchRoomsCollection;
        private ToolStripMenuItem tsmiMoveRoomDown;
        private ToolStripMenuItem tsmiMoveRoomUp;
        private IContainer components;
        private ContextMenuStrip cmsMapMenu;
        public int ZoneNumber;

        #endregion

        #region Get/Set

        /// <summary>
        /// Прозрачность области выделения
        /// </summary>
        public int SelectionAlphaFactor
        {
            get => selectionAlphaFactor;
            set => selectionAlphaFactor = value;
        }

        public bool ClearSketchAfterGeneratingRooms
        {
            get => clearSketchAfterGeneratingRooms;
            set => clearSketchAfterGeneratingRooms = value;
        }

        public Color SketchCurrentColor
        {
            get => sketchRoomsCollection != null ? sketchRoomsCollection.LastSketchColor : Color.Transparent;
            set
            {
                if (sketchRoomsCollection != null)
                    sketchRoomsCollection.LastSketchColor = value;
            }
        }

        public SketchRoomsCollection SketchRoomsCollection => sketchRoomsCollection;

        /// <summary>
        /// Флаг переключения в режим рисования эскиза карты
        /// </summary>
        public bool DrawSketchMode
        {
            get => drawSketchMode;
            set
            {
                drawSketchMode = value;
                Cursor = drawSketchMode ? Cursors.Cross : Cursors.Default;
            }
        }

        /// <summary>
        /// Флаг переключения в режим отображения эскиза карты
        /// </summary>
        public bool ShowSketchMode
        {
            get => showSketchMode;
            set
            {
                showSketchMode = value;
                RedrawBitmap();
            }
        }

        [Bindable(false), Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SelectedRoomsCollection SelectedRooms
        {
            get => selectedRooms;
            set
            {
                selectedRooms.Clear();
                if (value == null) return;
                selectedRooms = value;
                RedrawBitmap();
                RoomsSelectionChanged?.Invoke(selectedRooms);
            }
        }

        [Bindable(false), Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SelectedRoomsCollection HighlightedRooms
        {
            get => highlightedRooms;
            set
            {
                highlightedRooms.Clear();
                if (value == null) return;
                highlightedRooms = value;
                RedrawBitmap();
            }
        }

        //
        [Bindable(false), Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SelectedPath
        {
            get
            {
                string res = "";
                foreach (int vNum in selectedRooms)
                    res = (res == "") ? vNum.ToString() : res + "/" + vNum;
                return res;
            }
            set
            {
                selectedRooms.Clear();
                if (value.Length <= 0) return;
                string[] parts = value.TrimStart(new[] {' ', '/'}).TrimEnd(new[] {' ', '/'}).Split('/');
                Room room;
                foreach (string num in parts)
                {
                    room = roomsCollection[Convert.ToInt32(num), 0];
                    if (room != null)
                        selectedRooms.SmartAddRoom(room.VNum);
                }
                RedrawBitmap();
            }
        }

        [Bindable(false), Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ExternalPathSelection
        {
            get => externalPathSelection;
            set
            {
                externalPathSelection = value;
                if (value == false)
                    selectedRooms.Clear();
                RedrawBitmap();
            }
        }

        [Bindable(false), Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ExternalRoomRoomSelection
        {
            get => externalRoomRoomSelection;
            set
            {
                externalRoomRoomSelection = value;
                RedrawBitmap();
            }
        }

        [Bindable(false), Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Point3D CenterRoomCoord
        {
            get => centerRoom;
            set
            {
                centerRoom = value.Copy();
                mustRecalcExitColors = true;
                RedrawBitmap();
            }
        }

        [Bindable(false), Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CenterRoomZ
        {
            get => centerRoom.Z;
            set
            {
                if (Math.Abs(value) > 10) return;
                centerRoom.Z = value;
                mustRecalcExitColors = true;
                RedrawBitmap();
            }
        }

        [Bindable(false), Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CenterRoomX
        {
            get => centerRoom.X;
            set
            {
                if (centerRoom.X != value)
                {
                    centerRoom.X = value;
                    mustRecalcExitColors = true;
                    CenterRoomXValueChanged?.Invoke(centerRoom.X);
                }
                RedrawBitmap();
            }
        }

        [Bindable(false), Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CenterRoomY
        {
            get => centerRoom.Y;
            set
            {
                if (centerRoom.Y != value)
                {
                    centerRoom.Y = value;
                    mustRecalcExitColors = true;
                    CenterRoomYValueChanged?.Invoke(centerRoom.Y);
                }
                RedrawBitmap();
            }
        }

        [Bindable(false), Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ExitScale
        {
            get => exitScale;
            set
            {
                if (value < 2) return;
                exitScale = value;
                RecreateAllRoomsBitmaps();
                RedrawBitmap();
            }
        }        
        
        [Bindable(false), Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ZoneDataManager ZoneDm
        {
            get => zoneDm;
            set
            {
                if (value == zoneDm) return;
                zoneDm = value;
                RedrawBitmap();
            }
        }

        public int MapScale
        {
            get => mapScale;
            set
            {
                if (value < 1) return;
                mapScale = value;
                roomSideSize = value*ConstGSize;
                thumbnail.Size = new Size(roomSideSize, roomSideSize);
                RecreateAllRoomsBitmaps();
                RedrawBitmap();
            }
        }

        public Color GridColor
        {
            get => gridColor;
            set
            {
                gridColor = value;
                RedrawBitmap();
            }
        }

        public bool AutolinkingX
        {
            get => autolinkingX;
            set => autolinkingX = value;
        }

        public bool AutolinkingY
        {
            get => autolinkingY;
            set => autolinkingY = value;
        }

        public bool AutolinkingZ
        {
            get => autolinkingZ;
            set => autolinkingZ = value;
        }

        public bool ShowObj
        {
            get => showObj;
            set
            {
                showObj = value;
                RecreateAllRoomsBitmaps();
                RedrawBitmap();
            }
        }

        public bool ShowMob
        {
            get => showMob;
            set
            {
                showMob = value;
                RecreateAllRoomsBitmaps();
                RedrawBitmap();
            }
        }

        public bool ShowTrg
        {
            get => showTrg;
            set
            {
                showTrg = value;
                RecreateAllRoomsBitmaps();
                RedrawBitmap();
            }
        }

        public bool ShowVNums
        {
            get => showVNums;
            set
            {
                showVNums = value;
                RecreateAllRoomsBitmaps();
                RedrawBitmap();
            }
        }

        public bool RoomDetailsVisible
        {
            get => roomDetailsVisible;
            set => roomDetailsVisible = value;
        }

        public Color RoomHighlightColor
        {
            get => roomHighlightColor;
            set => roomHighlightColor = value;
        }

        public Color FocusHighlightColor
        {
            get => focusHighlightColor;
            set => focusHighlightColor = value;
        }

        public int RoomHighlightingAlphaFactor
        {
            get => roomHighlightingAlphaFactor;
            set => roomHighlightingAlphaFactor = value;
        }

        #endregion

        #region ctor/dtor

        public WldMap()
        {
            InitializeComponent();
            solidGridLines = false;
            curRoom = new Room(-1) {X = -99999999, Y = -99999999, Z = -99999999};
            SetStyle(
                ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                ControlStyles.DoubleBuffer, true);
            bmp = new Bitmap(Width, Height);
            RedrawBitmap();
            InitializeThumbnail();
            Controls.Add(thumbnail);
            thumbnail.BringToFront();
        }

        private void InitializeThumbnail()
        {
            thumbnail.Width = roomSideSize;
            thumbnail.Height = roomSideSize;
            thumbnail.Visible = false;
            thumbnail.Location = new Point(0, 0);
            thumbnail.Image =
                RoomDrawer.GetRoom(SmoothMode, ConstGSize, mapScale, exitScale, roomBgColorTop, roomBgColorBottom,
                                    new ExitColors(), false, false, false, Font, "");
        }

        public void SetRoomsCollection(ref RoomsCollection rooms,
                                       ref SketchRoomsCollection sketchRooms, int inZoneNumber)
        {
            ZoneNumber = inZoneNumber;
            roomsCollection = rooms;
            sketchRoomsCollection = sketchRooms;
            /*if (mSketchRoomsCollection.Count > 0) //Получение последнего используемого цвета в клетке
            {
            }
            else //Установка цвета по умолчанию
            {
            }*/
            if (roomsCollection.Count > 0)
            {
                centerRoom.X = ((Room) (roomsCollection[0])).X;
                centerRoom.Y = ((Room) (roomsCollection[0])).Y;
                centerRoom.Z = ((Room) (roomsCollection[0])).Z;
            }
            RecreateAllRoomsBitmaps();
            RedrawBitmap();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (thumbnail != null)
                    thumbnail.Dispose();
                if (bmp != null)
                    bmp.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion

        #region Drawing

        //Заглушка для устранения мерцания
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            if (DesignMode)
            {
                Graphics gr = Graphics.FromImage(bmp);
                gr.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                gr.Clear(BackColor);
                gr.DrawRectangle(new Pen(focusHighlightColor, (float)0.5), 0, 0, Width-1, Height-1);
                gr.Dispose();
                pevent.Graphics.DrawImageUnscaled(bmp, 0, 0);
                return;
            }
            pevent.Graphics.DrawImageUnscaled(bmp, 0, 0);
            pevent.Graphics.SmoothingMode = SmoothMode;
            //Тут рисуется сетка
            //DrawGrid(pevent.Graphics);
            //Отрисовка статуса
            DrawInfo(pevent.Graphics);

            //Отрисовка выделения
            using (var p = new Pen(selectionRegionBorderColor))
            {
                if (selectionStarted)
                {
                    //Pen p = new Pen(new HatchBrush(HatchStyle.Percent50, mSelectionRegionBorderColor, Color.Transparent));
                    if (mouseClickedPoint.X - mouseCurrentPoint.X == 0 ||
                        mouseClickedPoint.Y - mouseCurrentPoint.Y == 0)
                        pevent.Graphics.DrawLine(p, mouseClickedPoint, mouseCurrentPoint);
                    else
                    {
                        var r = new Rectangle(Math.Min(mouseClickedPoint.X, mouseCurrentPoint.X),
                                              Math.Min(mouseClickedPoint.Y, mouseCurrentPoint.Y),
                                              Math.Abs(mouseClickedPoint.X - mouseCurrentPoint.X),
                                              Math.Abs(mouseClickedPoint.Y - mouseCurrentPoint.Y));
                        using (
                            Brush b =
                                new SolidBrush(
                                    Color.FromArgb(selectionAlphaFactor, selectionRegionBgColor.R,
                                                   selectionRegionBgColor.G, selectionRegionBgColor.B)))
                            pevent.Graphics.FillRectangle(b, r);
                        pevent.Graphics.DrawRectangle(p, r);
                    }
                }
            }
            //Отрисовка признака что карта в фокусе
            if (Focused)
                pevent.Graphics.DrawRectangle(new Pen(focusHighlightColor, (float)0.5), 0, 0, Width - 1, Height - 1);

        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (Height <= 0 || Width <= 0) return;
            bmp = new Bitmap(Width, Height);
            RedrawBitmap();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            Invalidate();
            base.OnLostFocus(e);
        }

        /// <summary>
        /// Перерисовывает кэш карты
        /// </summary>
        public void RedrawBitmap()
        {
            //if (!canRedrawBMP) return;
            Graphics gr = Graphics.FromImage(bmp);
            gr.SmoothingMode = SmoothMode;
            gr.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            gr.Clear(BackColor);
            //Тут собсна рисовать карту :)
            DrawSketch(gr);
            DrawMap(gr);
            //Тут рисуется сетка
            DrawGrid(gr);


            //Тут возможно какая то отрисовка поверх

            //DrawInfo(gr);
            gr.Dispose();
            Invalidate();
            //this.Invalidate();
        }

        private void DrawInfo(Graphics gr)
        {
            using (Brush b = new SolidBrush(Color.FromArgb(150, Color.DarkBlue.R, Color.DarkBlue.G, Color.DarkBlue.B)))
            {
                Brush b1 = new SolidBrush(Color.FromArgb(100, 0, 255, 0));
                string modetxt = string.Empty;
                if (externalPathSelection)
                {
                    modetxt = "Режим: Выбор пути моба";
                    b1 = new SolidBrush(Color.FromArgb(100, 0, 0, 255));
                }
                else if (externalRoomRoomSelection)
                {
                    modetxt = "Режим: Выбор комнаты для выхода";
                    b1 = new SolidBrush(Color.FromArgb(100, 255, 0, 0));
                }
                string text = string.IsNullOrEmpty(modetxt) ? "" : modetxt + "\n";
                text += (curRoom.VNum != -1) ? $"Координаты: [{curRoom.X}:{curRoom.Y}:{curRoom.Z}]" : "";

                if (curRoom != null)
                    text += (curRoom.VNum != -1) ? $"\nКомната: [{curRoom.VNum}] {curRoom.Name}" : "";

                if (curRoom != null && roomDetailsVisible)//ToDo:отключаемость детальной инфы
                {
                    if (curRoom.LoadingMobsCollection.Count > 0)
                    {
                        text += "\nМобы:";
                        foreach (OperatedMob lm in curRoom.LoadingMobsCollection)
                        {
                            Mob mob = zoneDm?.Mobs[lm.VNum, 0];
                            text += $"\n  • {((mob != null) ? mob.ToString() : lm.VNum + " -моб из другой зоны-")}";
                        }
                    }
                    if (curRoom.LoadingObjectsCollection.Count > 0)
                    {
                        text += "\nОбъекты:";
                        foreach (OperatedObj lo in curRoom.LoadingObjectsCollection)
                        {
                            Obj obj = zoneDm?.Objects[lo.VNum, 0];
                            text += $"\n  • {((obj != null) ? obj.ToString() : lo.VNum + " -предмет из другой зоны-")} <{lo.Probability}%>";
                        }
                    }
                    if (curRoom.TriggersList.Count > 0)
                    {
                        text += "\nТриггеры:";
                        foreach (int trVnum in curRoom.TriggersList)
                        {
                            Trigger trg = zoneDm?.Triggers[trVnum, 0];
                            text += $"\n  • {((trg != null) ? trg.ToString() : trVnum + " -триггер из другой зоны-")}";
                        }
                    }
                }

                if (string.IsNullOrEmpty(text)) return;

                SizeF s = gr.MeasureString(text, Font);                
                Rectangle r = new Rectangle(3, Height - Convert.ToInt32(s.Height)-3, Convert.ToInt32(s.Width), Convert.ToInt32(s.Height));
                gr.FillRectangle(b, r);
                using (var p = new Pen(Color.DarkBlue))
                    gr.DrawRectangle(p, r);
                var r1 =
                    new Rectangle(4, Height - Convert.ToInt32(s.Height) - 3, Convert.ToInt32(gr.MeasureString(modetxt, Font).Width), 14);
                gr.FillRectangle(b1, r1);
                b1.Dispose();
                gr.DrawString(text, Font, new SolidBrush(Color.WhiteSmoke), 5, Height - Convert.ToInt32(s.Height)-3);
            }
        }

        private void DrawSketch(Graphics gr)
        {
            if (sketchRoomsCollection == null || !showSketchMode) return;
            int dx = GetVisibleColumns()/2;
            int dy = GetVisibleRows()/2;
            var b = new Bitmap(1, 1);
            const int delta = 4; //Желательно четное
            Point p = GetStartRoomPoint();
            foreach (SketchRoom room in sketchRoomsCollection)
            {
                if (room.X < centerRoom.X - dx || room.X > centerRoom.X + dx || room.Y < centerRoom.Y - dy ||
                    room.Y > centerRoom.Y + dy || room.Z != centerRoom.Z ||
                    room.RoomColor.ToArgb() == Color.Transparent.ToArgb()) continue;                
                using (Brush sb = new SolidBrush(room.RoomColor))
                {
                    gr.FillRectangle(sb, p.X - delta/2 + roomSideSize*(room.X - centerRoom.X),
                                     p.Y - delta/2 + roomSideSize*(room.Y - centerRoom.Y),
                                     roomSideSize + delta - 1, roomSideSize + delta - 1);
                }
            }

            b.Dispose();
        }

        private void DrawMap(Graphics gr)
        {
            if (roomsCollection == null) return;
            int dx = GetVisibleColumns()/2;
            int dy = GetVisibleRows()/2;
            const int delta = 4; //Желательно четное
            foreach (Room room in roomsCollection)
            {
                if (mustRecalcExitColors)
                    RecreateRoomBitmap(room);
                if (room.X >= centerRoom.X - dx && room.X <= centerRoom.X + dx &&
                    room.Y >= centerRoom.Y - dy && room.Y <= centerRoom.Y + dy &&
                    room.Z == centerRoom.Z && room.PlacedOnMap)
                {
                    //CRoom Room = new CRoom(dr["vnum"].ToString(), Convert.ToInt32(dr["x"]), Convert.ToInt32(dr["y"]), Convert.ToInt32(dr["z"]));
                    /* if (mSelectionType == 1)
                     {
                         if (iSelectedRooms.RoomExist(Room))//Отрисовка выбранных комнат
                             b = CRoomDrawer.GetRoom(this.mSmoothMode, iConstGSize, mMapScale, mExitScale, mSelectedRoomBGColorTop, mSelectedRoomBGColorBottom, Room.ExitColors);
                         else//Отрисовка невыбранных комнат
                             b = CRoomDrawer.GetRoom(this.mSmoothMode, iConstGSize, mMapScale, mExitScale, mRoomBGColorTop, mRoomBGColorBottom, Room.ExitColors);
                         Point p = GetStartRoomPoint();
                         gr.DrawImageUnscaled(b, p.X + iRoomSideSize * (Room.X - mCenterRoom.X), p.Y + iRoomSideSize * (Room.Y - mCenterRoom.Y));
                     }
                     else
                     {*/
                    Point p = GetStartRoomPoint();
                    if (room.Img == null)
                        RecreateRoomBitmap(room);
                    if (room.Img != null)
                        gr.DrawImageUnscaled(room.Img, p.X + roomSideSize*(room.X - centerRoom.X),
                                             p.Y + roomSideSize*(room.Y - centerRoom.Y));
                    if (highlightedRooms.RoomExist(room))
                    {
                        using (
                            Brush sb =
                                new SolidBrush(Color.FromArgb(roomHighlightingAlphaFactor, roomHighlightColor.R,
                                                              roomHighlightColor.G, roomHighlightColor.B)))
                        {
                            gr.FillRectangle(sb, p.X - delta/2 + roomSideSize*(room.X - centerRoom.X),
                                             p.Y - delta/2 + roomSideSize*(room.Y - centerRoom.Y),
                                             roomSideSize + delta - 1, roomSideSize + delta - 1);
                        }
                    }
                    if (selectedRooms.RoomExist(room))
                    {
                        using (
                            Brush sb =
                                new SolidBrush(
                                    Color.FromArgb(RoomSelectionAlphaFactor, roomSelectionRegionBgColor.R,
                                                   roomSelectionRegionBgColor.G, roomSelectionRegionBgColor.B)))
                        {
                            gr.FillRectangle(sb, p.X - delta/2 + roomSideSize*(room.X - centerRoom.X),
                                             p.Y - delta/2 + roomSideSize*(room.Y - centerRoom.Y),
                                             roomSideSize + delta - 1, roomSideSize + delta - 1);
                        }
                        int number = selectedRooms.RoomIndex(room.VNum);
                        if (number != -1 && externalPathSelection)
                        {
                            var fnt = new Font(Font.Name, MapScale*2);
                            gr.DrawString((number + 1).ToString(), fnt, Brushes.Red,
                                          new Point(p.X + roomSideSize*(room.X - centerRoom.X) + 2,
                                                    p.Y + roomSideSize*(room.Y - centerRoom.Y) + 2));
                        }
                    }
                    // }
                }

                mustRecalcExitColors = false;
            }
        }

        public void RecreateAllRoomsBitmaps()
        {
            foreach (Room room in roomsCollection)
                RecreateRoomBitmap(room);
        }

        public void RecreateRoomBitmap(Room inRoom)
        {
            if (inRoom == null) return;
            CalcExitColors(inRoom);
            bool drawO = (showObj && inRoom.LoadingObjectsCollection.Count > 0);
            bool drawM = (showMob && inRoom.LoadingMobsCollection.Count > 0);
            bool drawT = (showTrg && inRoom.TriggersList.Count > 0);
            string number = inRoom.VNum.ToString();
            if (!showVNums)
                number = "";
            inRoom.Img =
                RoomDrawer.GetRoom(SmoothMode, ConstGSize, mapScale, exitScale, roomBgColorBottom,
                                    GetRoomBgColor(inRoom), inRoom.ExitColors, drawO, drawM, drawT, Font, number);
        }

        private Color GetRoomBgColor(Room inRoom)
        {
            ListItemDesc reslid = null;
            foreach (ListItemDesc lid in ColorDescriptors)
            {
                if (inRoom.Flags.Contains(lid.Val))
                {
                    if (reslid == null)
                        reslid = lid;
                    else if (reslid.Order > lid.Order)
                        reslid = lid;
                }
            }
            if (reslid != null) return reslid.ItemBGColor;
            return roomBgColorTop;
        }

        private void CalcExitColors(Room inRoom)
        {
            inRoom.ExitColors.Reset();
            //тут явно нужна логика по обработке выходов с дверями
            Room room;
            if (inRoom.ExitNorth.RoomVNum != -1)
            {
                room = roomsCollection[inRoom.ExitNorth.RoomVNum, 0];
                if (inRoom.ExitNorth.ExitFlag != 0)
                    inRoom.ExitColors.ColorExitN = doorExitColor;
                else if (room != null)
                {
                    inRoom.ExitColors.ColorExitN = inRoom.Y != room.Y + 1 ? teleportExitColor : simpleExitColor;
                }
                else
                    inRoom.ExitColors.ColorExitN = zoneExitColor; //Выход в несуществующую в этой зоне клетку
            }
            if (inRoom.ExitEast.RoomVNum != -1)
            {
                room = roomsCollection[inRoom.ExitEast.RoomVNum, 0];
                if (inRoom.ExitEast.ExitFlag != 0)
                    inRoom.ExitColors.ColorExitE = doorExitColor;
                else if (room != null)
                {
                    inRoom.ExitColors.ColorExitE = inRoom.X != room.X - 1 ? teleportExitColor : simpleExitColor;
                }
                else
                    inRoom.ExitColors.ColorExitE = zoneExitColor;
            }
            if (inRoom.ExitSouth.RoomVNum != -1)
            {
                room = roomsCollection[inRoom.ExitSouth.RoomVNum, 0];
                if (inRoom.ExitSouth.ExitFlag != 0)
                    inRoom.ExitColors.ColorExitS = doorExitColor;
                else if (room != null)
                {
                    inRoom.ExitColors.ColorExitS = inRoom.Y != room.Y - 1 ? teleportExitColor : simpleExitColor;
                }
                else
                    inRoom.ExitColors.ColorExitS = zoneExitColor;
            }
            if (inRoom.ExitWest.RoomVNum != -1)
            {
                room = roomsCollection[inRoom.ExitWest.RoomVNum, 0];
                if (inRoom.ExitWest.ExitFlag != 0)
                    inRoom.ExitColors.ColorExitW = doorExitColor;
                else if (room != null)
                {
                    inRoom.ExitColors.ColorExitW = inRoom.X != room.X + 1 ? teleportExitColor : simpleExitColor;
                }
                else
                    inRoom.ExitColors.ColorExitW = zoneExitColor;
            }
            if (inRoom.ExitUp.RoomVNum != -1)
            {
                room = roomsCollection[inRoom.ExitUp.RoomVNum, 0];
                if (inRoom.ExitUp.ExitFlag != 0)
                    inRoom.ExitColors.ColorExitU = doorExitColor;
                else if (room != null)
                {
                    inRoom.ExitColors.ColorExitU = inRoom.Z != room.Z - 1 ? teleportExitColor : simpleExitColor;
                }
                else
                    inRoom.ExitColors.ColorExitU = zoneExitColor;
            }
            if (inRoom.ExitDown.RoomVNum != -1)
            {
                room = roomsCollection[inRoom.ExitDown.RoomVNum, 0];
                if (inRoom.ExitDown.ExitFlag != 0)
                    inRoom.ExitColors.ColorExitD = doorExitColor;
                else if (room != null)
                {
                    inRoom.ExitColors.ColorExitD = inRoom.Z != room.Z + 1 ? teleportExitColor : simpleExitColor;
                }
                else
                    inRoom.ExitColors.ColorExitD = zoneExitColor;
            }
        }

        private void DrawGrid(Graphics gr)
        {
            Point startPoint = GetStartPoint();
            //iVisibleColumns = (int) (Math.Ceiling((double) (Width/(iRoomSideSize))));
            //iVisibleRows = (int) (Math.Ceiling((double) (Height/(iRoomSideSize))));
            Pen p = solidGridLines ? new Pen(gridColor) : new Pen(new HatchBrush(HatchStyle.Percent50, gridColor, Color.Transparent));
            for (int f = 0; f < Width/(roomSideSize); f++)
            {
                gr.DrawLine(p, startPoint.X - (roomSideSize)*f, 0, startPoint.X - (roomSideSize)*f, Height);
                gr.DrawLine(p, startPoint.X + (roomSideSize)*f, 0, startPoint.X + (roomSideSize)*f, Height);
            }
            for (int f = 0; f < Height/(roomSideSize); f++)
            {
                gr.DrawLine(p, Width, startPoint.Y - (roomSideSize)*f, 0, startPoint.Y - (roomSideSize)*f);
                gr.DrawLine(p, Width, startPoint.Y + (roomSideSize)*f, 0, startPoint.Y + (roomSideSize)*f);
            }
        }

        #endregion

        #region Mouse and Keyboard Events

        /// <summary>
        /// Обработка хотекеев
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (Focused)
            {
                bool mustRedraw = false;
                switch (keyData)
                {
                    #region Shift mod

                    case Keys.Shift | Keys.Left:
                        centerRoom.X++;
                        mustRedraw = true;
                        break;
                    case Keys.Shift | Keys.Right:
                        centerRoom.X--;
                        mustRedraw = true;
                        break;
                    case Keys.Shift | Keys.Up:
                        centerRoom.Y++;
                        mustRedraw = true;
                        break;
                    case Keys.Shift | Keys.Down:
                        centerRoom.Y--;
                        mustRedraw = true;
                        break;
                    case Keys.Shift | Keys.PageUp:
                        centerRoom.Z++;
                        mustRedraw = true;
                        break;
                    case Keys.Shift | Keys.PageDown:
                        centerRoom.Z--;
                        mustRedraw = true;
                        break;
                    case Keys.Shift | Keys.Home:
                        ToZeroPoint();
                        break;

                    #endregion

                    default:
                        return base.ProcessCmdKey(ref msg, keyData);
                }
                if (mustRedraw)
                {
                    RedrawBitmap();
                    return true;
                }
            }
            Focus();
            return false;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            shiftPushed = e.Shift;
            controlPushed = e.Control;
            altPushed = e.Alt;
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            shiftPushed = e.Shift;
            controlPushed = e.Control;
            altPushed = e.Alt;
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                CreateNewRoom(GetRoomCoordinatesByPoint(e.Location));
                RedrawBitmap();
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Focus();
            Room room;
            if (e.Button == MouseButtons.Left)
            {
                mouseClickedPoint = e.Location;
                Point3D roomCoordinates = GetRoomCoordinatesByPoint(mouseClickedPoint);
                leftMouseBtnPushed = true;
                room = roomsCollection[roomCoordinates.X, roomCoordinates.Y, roomCoordinates.Z];
                if (shiftPushed || room == null)
                    selectionStarted = true;
                else //начинаем драгдроп клетки
                {
                    startPointVnum = room.VNum;
                    mustStartDragging = true;
                }
            }
            else if (e.Button == MouseButtons.Right && shiftPushed)
            {
                Cursor = Cursors.SizeAll;
                rightBtnScrolling = true;
                startPoint = e.Location;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            mustStartDragging = false;
            //ToDo если выделено более чем 1 комната, то данные на форму не грузить но все изменения применять ко всем выбранным комнатам
            switch (e.Button)
            {
                case MouseButtons.Left:
                    {
                        leftMouseBtnPushed = false;
                        Point3D point1 = GetRoomCoordinatesByPoint(mouseClickedPoint);
                        Point3D point2 = GetRoomCoordinatesByPoint(e.Location);
                        if (selectionStarted) //Отрабатываем мультиселект
                        {
                            selectionStarted = false;
                            //Если добавляем без контрола и это не выбор пути, то чистим массив выбранных точек
                            if (!controlPushed && !externalPathSelection) selectedRooms.Clear();
                            //int cnt = (Math.Max(Point1.X, Point2.X) - Math.Min(Point1.X, Point2.X)) * (Math.Max(Point1.Y, Point2.Y) - Math.Min(Point1.Y, Point2.Y));
                            for (int x = Math.Min(point1.X, point2.X); x <= Math.Max(point1.X, point2.X); x++)
                            {
                                for (int y = Math.Min(point1.Y, point2.Y); y <= Math.Max(point1.Y, point2.Y); y++)
                                {
                                    if (drawSketchMode)
                                    {
                                        SketchRoom sr = sketchRoomsCollection.GetSketchRoom(x, y, centerRoom.Z);
                                        if (sr == null)
                                        {
                                            sketchRoomsCollection.AddSketchRoom(x, y, centerRoom.Z,
                                                                                 sketchRoomsCollection.LastSketchColor);
                                        }
                                        else //if (cnt == 0)
                                            sketchRoomsCollection.Remove(sr);
                                    }
                                    else
                                    {
                                        Room room = roomsCollection[x, y, centerRoom.Z];
                                        if (room != null)
                                        {
                                            selectedRooms.SmartAddRemoveRoom(room.VNum);
                                            if (PathChanged != null && externalPathSelection)
                                                PathChanged();
                                        }
                                    }
                                }
                            }
                            RedrawBitmap();
                        }
                            //else if (Point1.Equals(Point2)) //Отрабатываем синглселект
                        else if (Math.Abs(point1.X - point2.X) < 6 && Math.Abs(point1.Y - point2.Y) < 6 &&
                                 point1.Z == point2.Z)
                            //Отрабатываем синглселект
                        {
                            if (drawSketchMode)
                            {
                                SketchRoom sr = sketchRoomsCollection.GetSketchRoom(point1.X, point1.Y, point1.Z);
                                if (sr == null)
                                {
                                    sketchRoomsCollection.AddSketchRoom(point1.X, point1.Y, point1.Z,
                                                                         sketchRoomsCollection.LastSketchColor);
                                }
                                else
                                    sketchRoomsCollection.Remove(sr);
                                RedrawBitmap();
                                return;
                            }
                            Room room = roomsCollection[point1.X, point1.Y, point1.Z];
                            if (externalRoomRoomSelection && room != null) //Выдача номера кликнутой клетки наружу
                            {
                                externalRoomRoomSelection = false;
                                RoomSelected?.Invoke(room.VNum);
                            }
                            else
                            {
                                //Если добавляем без контрола и это не выбор пути, то чистим массив выбранных точек
                                if (!controlPushed && !externalPathSelection) selectedRooms.Clear();
                                if (room != null)
                                {
                                    selectedRooms.SmartAddRemoveRoom(room.VNum);
                                    if (PathChanged != null && externalPathSelection)
                                        PathChanged();
                                }
                            }
                            RedrawBitmap();
                        }
                        //else //Отрабатываем драгдроп
                        //{
                        //}
                        if (RoomsSelectionChanged != null && !externalPathSelection && !ExternalRoomRoomSelection)
                            //Инициируем событие "Изменился список выбранных комнат"
                            RoomsSelectionChanged(selectedRooms);
                    }
                    break;
                case MouseButtons.Right:
                    if (rightBtnScrolling)
                    {
                        Cursor = Cursors.Default;
                        rightBtnScrolling = false;
                    }
                    else 
                    {
                        tsmiMoveRoomUp.Enabled = SelectedRooms.Count > 0;
                        tsmiMoveRoomDown.Enabled = SelectedRooms.Count > 0;
                        //if (mRoomsCollection.Exists(GetRoomCoordinatesByPoint(e.Location)))
                        cmsMapMenu.Show(this, e.Location);
                    }
                    break;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            mouseCurrentPoint = e.Location;
            //Драгдроп начинается тут из-за того, что надо иметь возможность просто кликнуть клетку для выделения
            //if (iMustStartDragging && !iMouseClickedPoint.Equals(e.Location) && iLeftMouseBtnPushed)
            if (mustStartDragging &&
                Math.Abs(mouseClickedPoint.X - e.Location.X) + Math.Abs(mouseClickedPoint.Y - e.Location.Y) > 5 &&
                leftMouseBtnPushed)
            {
                Room room = roomsCollection[startPointVnum, 0];
                if (room != null)
                {
                    thumbnail.Width = roomSideSize;
                    thumbnail.Height = roomSideSize;
                    thumbnail.Image = room.Img;
                    DoDragDrop(new DataObject(DataFormats.CommaSeparatedValue, startPointVnum + "," + "room"),
                               DragDropEffects.Move);
                }
                mustStartDragging = false;
            }
            if (rightBtnScrolling)
            {
                //bool mustRedraw = false;
                int deltaX = (startPoint.X - e.X);
                int deltaY = (startPoint.Y - e.Y);
                if (Math.Abs(deltaX) > (roomSideSize))
                {
                    startPoint.X = e.X;
                    //_centerRoom.X += deltaX/(_roomSideSize);
                    //mustRedraw = true;
                    CenterRoomX += deltaX / (roomSideSize);
                }
                if (Math.Abs(deltaY) > (roomSideSize))
                {
                    startPoint.Y = e.Y;
                    //_centerRoom.Y += deltaY/(_roomSideSize);
                    //mustRedraw = true;
                    CenterRoomY += deltaY / (roomSideSize);
                }
                /*if (mustRedraw)
                    RedrawBitmap();*/
                return;
            }
            Point3D p3D = GetRoomCoordinatesByPoint(e.Location);
            if (curRoom.X != p3D.X || curRoom.Y != p3D.Y || curRoom.Z != p3D.Z)
            {
                Room room = roomsCollection[p3D.X, p3D.Y, p3D.Z];
                if (room != null)
                {
                    //if (room.VNum == 46100) Debug.WriteLine(room);
                    if (curRoom.VNum != room.VNum)
                        curRoom = room;
                }
                else
                    curRoom = new Room(-1) { X = -99999999, Y = -99999999, Z = -99999999 };
                Invalidate();
                return;
            }
            if (selectionStarted)
                Invalidate();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            int step = e.Delta/120;
            if (controlPushed) //Изменение масштаба
            {
                MapScale += step;
                return;
                //RecreateAllRoomsBitmaps();
            }
            if (shiftPushed && centerRoom.Z - step >= StaticData.MinZ && centerRoom.Z - step <= StaticData.MaxZ) //Изменение уровня по Z
                //_centerRoom.Z -= step;
                CenterRoomZ -= step;
            else if (altPushed) //Горизонтальное перемещение
                //_centerRoom.X -= step;
                CenterRoomX -= step;
            else //Вертикальное перемещение
                //_centerRoom.Y -= step;
                CenterRoomY -= step;
            //RedrawBitmap();
        }

        #endregion

        #region Sketch

        public void EraseSketch()
        {
            sketchRoomsCollection.Clear();
            RedrawBitmap();
        }

        public void CreateRoomsBySketch()
        {
            var toRemove = new SketchRoomsCollection();
            foreach (SketchRoom room in sketchRoomsCollection)
            {
                if (!CreateNewRoom(room))
                    break;
                toRemove.Add(room);
            }
            if (clearSketchAfterGeneratingRooms)
            {
                foreach (SketchRoom room in toRemove)
                    sketchRoomsCollection.Remove(room);
            }

            RedrawBitmap();
        }

        #endregion

        #region Util

        public void AddRoomToSelection(int roomVNum)
        {
            selectedRooms.Add(roomVNum);
            if (RoomsSelectionChanged != null && !externalPathSelection && !ExternalRoomRoomSelection)
                //Инициируем событие "Изменился список выбранных комнат"
                RoomsSelectionChanged(selectedRooms);
        }

        /// <summary>
        /// Расставляет комнаты на карте
        /// </summary>
        /// <param name="centerRoomVNum"></param>
        /// <returns>Количество комнат оставшихся нерасставленными</returns>
        public void GenerateMap(int centerRoomVNum)
        {
            if (roomsCollection.Count == 0) return;
            SingleMapGenerator generator = new SingleMapGenerator();
            generator.GenerateMap(roomsCollection, centerRoomVNum);

            foreach (Room room in roomsCollection)
            {
                CalcExitColors(room);
                RecreateRoomBitmap(room);
            }
            ToZeroPoint();
            RoomDroped?.Invoke();
        }

        private bool CreateNewRoom(Point3D inLocation)
        {
            if (roomsCollection[inLocation.X, inLocation.Y, inLocation.Z] != null)
                return true;
            Room newRoom = roomsCollection.AddDefRoom(ZoneNumber, -1);
            //Тип можно указывать если сделать меню с выбором типа зоны для автодобавления (горы, лес и т.п.)
            if (newRoom == null)
            {
                MessageBox.Show("Максимальное число комнат в зоне достигнуто!", "Сообщение", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                //TODO: Тут можно предложить начать новую зону как продолжение данной зоны
                return false;
            }
            newRoom.Location = inLocation;
            newRoom.PlacedOnMap = true;
            DoAutolinking(newRoom);
            RoomCreated?.Invoke();
            return true;
        }

        public void ToZeroPoint()
        {
            centerRoom = roomsCollection.GetPointOfCenterRoomPlacedOnMap();
            mustRecalcExitColors = true;
            RedrawBitmap();
        }

        private Point GetStartPoint()
        {
            return new Point((Width - (roomSideSize))/2, (Height - (roomSideSize))/2);
        }

        private Point GetStartRoomPoint()
        {
            return new Point(1 + (Width - (roomSideSize))/2, 1 + (Height - (roomSideSize))/2);
        }

        /*private Point GetStartRoomPoint(int X, int Y)
        {
            return
                new Point(1 + (Width - (iRoomSideSize))/2 + X*iRoomSideSize,
                          1 + (Height - (iRoomSideSize))/2 + Y*iRoomSideSize);
        }*/

        private int GetVisibleColumns()
        {
            return Width/(roomSideSize) + 2;
        }

        private int GetVisibleRows()
        {
            return Height/(roomSideSize) + 2;
        }

        private Point3D GetRoomCoordinatesByPoint(Point p)
        {
            var resPoint = new Point3D(0, 0, CenterRoomZ)
                               {
                                   X = (Convert.ToInt32(((float) (p.X - Width/2))/(roomSideSize)) + centerRoom.X),
                                   Y = (Convert.ToInt32(((float) (p.Y - Height/2))/(roomSideSize)) + centerRoom.Y)
                               };
            return resPoint;
        }

        /// <summary>
        /// Автолинковка
        /// </summary>
        /// <param name="room"></param>
        /// <returns>да, если произведена</returns>
        private bool DoAutolinking(Room room)
        {
            Point3D trgPoint = room.Location;
            Room tmpRoom;
            bool flag = false;
            //Следующее условие введено для того, чтоб не чистить выходы если если в новой локации у комнаты нет соседей
            if (autolinkingX && (roomsCollection[trgPoint.X + 1, trgPoint.Y, trgPoint.Z] != null ||
                                  roomsCollection[trgPoint.X - 1, trgPoint.Y, trgPoint.Z] != null))
            {
                tmpRoom = roomsCollection[trgPoint.X + 1, trgPoint.Y, trgPoint.Z];
                room.ExitEast.RoomVNum = -1;
                if (tmpRoom != null)
                {
                    if (tmpRoom.PlacedOnMap)
                    {
                        tmpRoom.ExitWest.RoomVNum = room.VNum;
                        room.ExitEast.RoomVNum = tmpRoom.VNum;
                    }
                }

                tmpRoom = roomsCollection[trgPoint.X - 1, trgPoint.Y, trgPoint.Z];
                room.ExitWest.RoomVNum = -1;
                if (tmpRoom != null)
                {
                    if (tmpRoom.PlacedOnMap)
                    {
                        tmpRoom.ExitEast.RoomVNum = room.VNum;
                        room.ExitWest.RoomVNum = tmpRoom.VNum;
                    }
                }
                tmpRoom = roomsCollection[trgPoint.X + 1, trgPoint.Y, trgPoint.Z];
                if (tmpRoom != null)
                    RecreateRoomBitmap(tmpRoom);
                tmpRoom = roomsCollection[trgPoint.X - 1, trgPoint.Y, trgPoint.Z];
                if (tmpRoom != null)
                    RecreateRoomBitmap(tmpRoom);
                flag = true;
            }
            if (autolinkingY && (roomsCollection[trgPoint.X, trgPoint.Y - 1, trgPoint.Z] != null ||
                 roomsCollection[trgPoint.X, trgPoint.Y + 1, trgPoint.Z] != null))
            {
                tmpRoom = roomsCollection[trgPoint.X, trgPoint.Y - 1, trgPoint.Z];
                room.ExitNorth.RoomVNum = -1;
                if (tmpRoom != null)
                {
                    if (tmpRoom.PlacedOnMap)
                    {
                        tmpRoom.ExitSouth.RoomVNum = room.VNum;
                        room.ExitNorth.RoomVNum = tmpRoom.VNum;
                    }
                }

                tmpRoom = roomsCollection[trgPoint.X, trgPoint.Y + 1, trgPoint.Z];
                room.ExitSouth.RoomVNum = -1;
                if (tmpRoom != null)
                {
                    if (tmpRoom.PlacedOnMap)
                    {
                        tmpRoom.ExitNorth.RoomVNum = room.VNum;
                        room.ExitSouth.RoomVNum = tmpRoom.VNum;
                    }
                }
                tmpRoom = roomsCollection[trgPoint.X, trgPoint.Y - 1, trgPoint.Z];
                if (tmpRoom != null)
                    RecreateRoomBitmap(tmpRoom);
                tmpRoom = roomsCollection[trgPoint.X, trgPoint.Y + 1, trgPoint.Z];
                if (tmpRoom != null)
                    RecreateRoomBitmap(tmpRoom);
                flag = true;
            }
            if (autolinkingZ && (roomsCollection[trgPoint.X, trgPoint.Y, trgPoint.Z + 1] != null ||
                 roomsCollection[trgPoint.X, trgPoint.Y, trgPoint.Z - 1] != null))
            {
                tmpRoom = roomsCollection[trgPoint.X, trgPoint.Y, trgPoint.Z + 1];
                room.ExitUp.RoomVNum = -1;
                if (tmpRoom != null)
                {
                    if (tmpRoom.PlacedOnMap)
                    {
                        tmpRoom.ExitDown.RoomVNum = room.VNum;
                        room.ExitUp.RoomVNum = tmpRoom.VNum;
                    }
                }

                tmpRoom = roomsCollection[trgPoint.X, trgPoint.Y, trgPoint.Z - 1];
                room.ExitDown.RoomVNum = -1;
                if (tmpRoom != null)
                {
                    if (tmpRoom.PlacedOnMap)
                    {
                        tmpRoom.ExitUp.RoomVNum = room.VNum;
                        room.ExitDown.RoomVNum = tmpRoom.VNum;
                    }
                }
                tmpRoom = roomsCollection[trgPoint.X, trgPoint.Y, trgPoint.Z + 1];
                if (tmpRoom != null)
                    RecreateRoomBitmap(tmpRoom);
                tmpRoom = roomsCollection[trgPoint.X, trgPoint.Y, trgPoint.Z - 1];
                if (tmpRoom != null)
                    RecreateRoomBitmap(tmpRoom);
                flag = true;
            }
            if (!flag) return false;
            RecreateRoomBitmap(room);
            return true;

        }

        #endregion

        #region DragDrop

        private int dropZLevel;

        protected override void OnDragEnter(DragEventArgs drgevent)
        {
            dragDataValid = false;
            foreach (string s in drgevent.Data.GetFormats())
                if (s == DataFormats.CommaSeparatedValue) dragDataValid = true;
            if (dragDataValid)
            {
                //string[] data = drgevent.Data.GetData(DataFormats.CommaSeparatedValue).ToString().Split(',');
                thumbnail.Visible = true;
                drgevent.Effect = DragDropEffects.Move;
            }
            else
                drgevent.Effect = DragDropEffects.None;
        }

        protected override void OnDragOver(DragEventArgs drgevent)
        {
            if (!dragDataValid) return;
            string[] data = drgevent.Data.GetData(DataFormats.CommaSeparatedValue).ToString().Split(',');
            Point3D prevPoint = GetRoomCoordinatesByPoint(PointToClient(new Point(LastDragX, LastDragY)));
            Point3D newPoint = GetRoomCoordinatesByPoint(PointToClient(new Point(drgevent.X, drgevent.Y)));
            if (LastDragX != drgevent.X || LastDragY != drgevent.Y)
            {
                dropZLevel = newPoint.Z;
                LastDragX = drgevent.X;
                LastDragY = drgevent.Y;
                Room tr = roomsCollection[newPoint.X, newPoint.Y, newPoint.Z];
                if (tr != null) //(клетка занята)                   
                {                    
                    if (data[1] == "room" && tr.PlacedOnMap && tr.VNum.ToString() != data[0])
                    {
                        thumbnail.Width = roomSideSize;
                        thumbnail.Height = roomSideSize;
                        Point p = PointToClient(new Point(drgevent.X, drgevent.Y));
                        int tmp = Height/2 - (CenterRoomY - newPoint.Y)*roomSideSize;
                        //Debug.WriteLine(tr +" ********** " +tmp + " " + p.Y);
                        if (tmp > p.Y) //А вот тут надо правильно переводить координаты комнаты в реальные
                        {
                            //Добавляем комнату уровнем выше
                            Room trup = roomsCollection[newPoint.X, newPoint.Y, newPoint.Z + 1];
                            if (trup != null)
                                drgevent.Effect = DragDropEffects.None;
                            else
                            {
                                drgevent.Effect = DragDropEffects.Move;
                                thumbnail.Image = RoomDrawer.GetRoom(SmoothMode, ConstGSize, mapScale, exitScale,
                                                                      roomBgColorTop,
                                                                      roomBgColorBottom,
                                                                      GenerateExitColors(new Point3D(newPoint.X,
                                                                                                     newPoint.Y,
                                                                                                     newPoint.Z + 1)),
                                                                      false, false, false, Font, "");
                                dropZLevel = newPoint.Z + 1;
                            }
                        }
                        else
                        {
                            //Добавляем комнату уровнем ниже
                            Room trdown = roomsCollection[newPoint.X, newPoint.Y, newPoint.Z - 1];
                            if (trdown != null)
                                drgevent.Effect = DragDropEffects.None;
                            else
                            {
                                drgevent.Effect = DragDropEffects.Move;
                                thumbnail.Image = RoomDrawer.GetRoom(SmoothMode, ConstGSize, mapScale, exitScale,
                                                                      roomBgColorTop,
                                                                      roomBgColorBottom,
                                                                      GenerateExitColors(new Point3D(newPoint.X,
                                                                                                     newPoint.Y,
                                                                                                     newPoint.Z - 1)),
                                                                      false, false, false, Font, "");
                                dropZLevel = newPoint.Z - 1;
                            }
                        }
                    }
                    else if (data[1] == "mob" && tr.PlacedOnMap)
                    {
                        drgevent.Effect = DragDropEffects.Move;
                        thumbnail.Width = Properties.Resources.thumb_mob.Width;
                        thumbnail.Height = Properties.Resources.thumb_mob.Height;
                        thumbnail.Image = Properties.Resources.thumb_mob;
                    }
                    else if (data[1] == "obj" && tr.PlacedOnMap)
                    {
                        thumbnail.Width = Properties.Resources.thumb_mob.Width;
                        thumbnail.Height = Properties.Resources.thumb_mob.Height;
                        drgevent.Effect = DragDropEffects.Move;
                        thumbnail.Image = Properties.Resources.thumb_obj;
                    }
                    else
                        drgevent.Effect = DragDropEffects.None;
                }
                else if (newPoint.X != prevPoint.X || newPoint.Y != prevPoint.Y)
                {
                    drgevent.Effect = DragDropEffects.Move;
                    switch (data[1])
                    {
                        case "room":
                            thumbnail.Width = roomSideSize;
                            thumbnail.Height = roomSideSize;
                            if (autolinkingX || autolinkingY || autolinkingZ)
                            {
                                thumbnail.Image =
                                    RoomDrawer.GetRoom(SmoothMode, ConstGSize, mapScale, exitScale, roomBgColorTop,
                                                        roomBgColorBottom, GenerateExitColors(newPoint), false, false, false,
                                                        Font, "");
                            }
                            break;
                        case "mob":
                            thumbnail.Width = Properties.Resources.thumb_mob.Width;
                            thumbnail.Height = Properties.Resources.thumb_mob.Height;
                            thumbnail.Image = Properties.Resources.thumb_mob;
                            break;
                        case "obj":
                            thumbnail.Width = Properties.Resources.thumb_mob.Width;
                            thumbnail.Height = Properties.Resources.thumb_mob.Height;
                            thumbnail.Image = Properties.Resources.thumb_obj;
                            break;
                    }
                }
            }
            SetThumbnailLocation(PointToClient(new Point(drgevent.X, drgevent.Y)));
            //Thumbnail.Invalidate();
        }

        private ExitColors GenerateExitColors(Point3D newPoint)
        {
            //Тут возможна обработка на то, чтоб не учитывалась стартовая позиция клетки
            var exitColors = new ExitColors();
            Room nr = roomsCollection[newPoint.X, newPoint.Y - 1, newPoint.Z];
            if (nr != null)
                if (nr.PlacedOnMap)
                    exitColors.ColorExitN = simpleExitColor;
            nr = roomsCollection[newPoint.X + 1, newPoint.Y, newPoint.Z];
            if (nr != null)
                if (nr.PlacedOnMap)
                    exitColors.ColorExitE = simpleExitColor;
            nr = roomsCollection[newPoint.X, newPoint.Y + 1, newPoint.Z];
            if (nr != null)
                if (nr.PlacedOnMap)
                    exitColors.ColorExitS = simpleExitColor;
            nr = roomsCollection[newPoint.X - 1, newPoint.Y, newPoint.Z];
            if (nr != null)
                if (nr.PlacedOnMap)
                    exitColors.ColorExitW = simpleExitColor;
            nr = roomsCollection[newPoint.X, newPoint.Y, newPoint.Z + 1];
            if (nr != null)
                if (nr.PlacedOnMap)
                    exitColors.ColorExitU = simpleExitColor;
            nr = roomsCollection[newPoint.X, newPoint.Y, newPoint.Z - 1];
            if (nr != null)
                if (nr.PlacedOnMap)
                    exitColors.ColorExitD = simpleExitColor;
            return exitColors;
        }

        protected override void OnDragDrop(DragEventArgs e)
        {
            string[] data = e.Data.GetData(DataFormats.CommaSeparatedValue).ToString().Split(',');
            thumbnail.Visible = false;
            Point3D startPoint = GetRoomCoordinatesByPoint(mouseClickedPoint);
            Point3D newPoint = GetRoomCoordinatesByPoint(PointToClient(new Point(e.X, e.Y)));
            if (e.Effect != DragDropEffects.Move) return;
            switch (data[1])
            {
                case "room":
                    {
                        Room room = roomsCollection[Convert.ToInt32(data[0]), 0];
                        if (room != null)
                        {
                            if (!room.PlacedOnMap)
                                room.PlacedOnMap = true;
                            else if (startPoint.Equals(newPoint))
                                return; //Исключение перетаскивания клетки на то место с которого ее взяли
                            newPoint.Z = dropZLevel;
                            var lastRoomPos = new Point3D(room.X, room.Y, room.Z);
                            room.Location = newPoint;
                            if (autolinkingX || autolinkingY || autolinkingZ)
                            {
                                if (DoAutolinking(room)) //Если автолинковка произведена, тогда чистим старые выходы
                                {
                                    Room tmpRoom;
                                    
                                    if (autolinkingX)
                                    {
                                        tmpRoom = roomsCollection[lastRoomPos.X + 1, lastRoomPos.Y, lastRoomPos.Z];
                                        if (tmpRoom != null)
                                        {
                                            //Возможно надо проверять на наличие этой комнаты на карте
                                            tmpRoom.ExitWest.RoomVNum = -1;
                                        }
                                        tmpRoom = roomsCollection[lastRoomPos.X - 1, lastRoomPos.Y, lastRoomPos.Z];
                                        if (tmpRoom != null)
                                        {
                                            //Возможно надо проверять на наличие этой комнаты на карте
                                            tmpRoom.ExitEast.RoomVNum = -1;
                                        }
                                        tmpRoom = roomsCollection[lastRoomPos.X + 1, lastRoomPos.Y, lastRoomPos.Z];
                                        if (tmpRoom != null)
                                            RecreateRoomBitmap(tmpRoom);
                                        tmpRoom = roomsCollection[lastRoomPos.X - 1, lastRoomPos.Y, lastRoomPos.Z];
                                        if (tmpRoom != null)
                                            RecreateRoomBitmap(tmpRoom);
                                    }

                                    if (autolinkingY)
                                    {
                                        tmpRoom = roomsCollection[lastRoomPos.X, lastRoomPos.Y - 1, lastRoomPos.Z];
                                        if (tmpRoom != null)
                                        {
                                            //Возможно надо проверять на наличие этой комнаты на карте
                                            tmpRoom.ExitSouth.RoomVNum = -1;
                                        }
                                        tmpRoom = roomsCollection[lastRoomPos.X, lastRoomPos.Y + 1, lastRoomPos.Z];
                                        if (tmpRoom != null)
                                        {
                                            //Возможно надо проверять на наличие этой комнаты на карте
                                            tmpRoom.ExitNorth.RoomVNum = -1;
                                        }
                                        tmpRoom = roomsCollection[lastRoomPos.X, lastRoomPos.Y - 1, lastRoomPos.Z];
                                        if (tmpRoom != null)
                                            RecreateRoomBitmap(tmpRoom);
                                        tmpRoom = roomsCollection[lastRoomPos.X, lastRoomPos.Y + 1, lastRoomPos.Z];
                                        if (tmpRoom != null)
                                            RecreateRoomBitmap(tmpRoom);
                                    }

                                    if (autolinkingZ)
                                    {
                                        tmpRoom = roomsCollection[lastRoomPos.X, lastRoomPos.Y, lastRoomPos.Z + 1];
                                        if (tmpRoom != null)
                                        {
                                            //Возможно надо проверять на наличие этой комнаты на карте
                                            tmpRoom.ExitDown.RoomVNum = -1;
                                        }
                                        tmpRoom = roomsCollection[lastRoomPos.X, lastRoomPos.Y, lastRoomPos.Z - 1];
                                        if (tmpRoom != null)
                                        {
                                            //Возможно надо проверять на наличие этой комнаты на карте
                                            tmpRoom.ExitUp.RoomVNum = -1;
                                        }
                                        tmpRoom = roomsCollection[lastRoomPos.X, lastRoomPos.Y, lastRoomPos.Z + 1];
                                        if (tmpRoom != null)
                                            RecreateRoomBitmap(tmpRoom);
                                        tmpRoom = roomsCollection[lastRoomPos.X, lastRoomPos.Y, lastRoomPos.Z - 1];
                                        if (tmpRoom != null)
                                            RecreateRoomBitmap(tmpRoom);
                                    }
                                }
                            }

                            RoomDroped?.Invoke();
                        }
                    }
                    break;
                case "mob":
                    MobDroped?.Invoke(Convert.ToInt32(data[0]), roomsCollection[newPoint.X, newPoint.Y, newPoint.Z]);
                    break;
                case "obj":
                    ObjDroped?.Invoke(Convert.ToInt32(data[0]), roomsCollection[newPoint.X, newPoint.Y, newPoint.Z]);
                    break;
            }
            /*else
                {
                    //Тут надо реализовать добавление клетки в список 
                    //и дальнейшую обработку при драгдропе из списка непривязанных клеток
                }*/
            RedrawBitmap();
        }

        protected override void OnDragLeave(EventArgs e)
        {
            thumbnail.Visible = false;
        }

        /// <summary>
        /// Перерисовка перетаскиваемой иконки
        /// </summary>
        /// <param name="p"></param>
        protected void SetThumbnailLocation(Point p)
        {
            /*if (Thumbnail.Image == null)
            {
                Thumbnail.Visible = false;
            }
            else
            {*/
            p.X -= thumbnail.Width/2;
            p.Y -= thumbnail.Height/2;
            thumbnail.Location = p;
            /*if (!Thumbnail.Visible)
                    Thumbnail.Visible = true;*/
            //}
        }

        #endregion

        private void TsmiMoveRoomUpClick(object sender, EventArgs e)
        {
            string unmoved = string.Empty;
            foreach (int selectedRoom in selectedRooms)
            {
                Room srcRoom = roomsCollection[selectedRoom, 0];
                if (roomsCollection[srcRoom.X, srcRoom.Y, srcRoom.Z + 1] == null)
                    srcRoom.Z += 1;
                else
                    unmoved += srcRoom + "\n";
            }
            RedrawBitmap();
            if (!string.IsNullOrEmpty(unmoved))
                MessageBox.Show(this, "По причине занятости клеток уровнем выше, не были перенесены комнаты:\n" + unmoved);
        }

        private void TsmiMoveRoomDownClick(object sender, EventArgs e)
        {
            string unmoved = string.Empty;
            foreach (int selectedRoom in selectedRooms)
            {
                Room srcRoom = roomsCollection[selectedRoom, 0];
                if (roomsCollection[srcRoom.X, srcRoom.Y, srcRoom.Z - 1] == null)
                    srcRoom.Z -= 1;
                else
                    unmoved += srcRoom + "\n";
            }
            RedrawBitmap();
            if (!string.IsNullOrEmpty(unmoved))
                MessageBox.Show(this, "По причине занятости клеток уровнем ниже, не были перенесены комнаты:\n" + unmoved);
        }

        // ReSharper disable RedundantThisQualifier
        // ReSharper disable RedundantNameQualifier
        // ReSharper disable RedundantDelegateCreation
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cmsMapMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiMoveRoomUp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMoveRoomDown = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsMapMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmsMapMenu
            // 
            this.cmsMapMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMoveRoomUp,
            this.tsmiMoveRoomDown});
            this.cmsMapMenu.Name = "cmsMapMenu";
            this.cmsMapMenu.Size = new System.Drawing.Size(352, 70);
            // 
            // tsmiMoveRoomUp
            // 
            this.tsmiMoveRoomUp.Image = global::ExtControls.Properties.Resources.button_zinc;
            this.tsmiMoveRoomUp.Name = "tsmiMoveRoomUp";
            this.tsmiMoveRoomUp.Size = new System.Drawing.Size(351, 22);
            this.tsmiMoveRoomUp.Text = "Переместить выбранные комнаты уровнем выше";
            this.tsmiMoveRoomUp.Click += new System.EventHandler(this.TsmiMoveRoomUpClick);
            // 
            // tsmiMoveRoomDown
            // 
            this.tsmiMoveRoomDown.Image = global::ExtControls.Properties.Resources.button_zdec;
            this.tsmiMoveRoomDown.Name = "tsmiMoveRoomDown";
            this.tsmiMoveRoomDown.Size = new System.Drawing.Size(351, 22);
            this.tsmiMoveRoomDown.Text = "Переместить выбранные комнаты уровнем ниже";
            this.tsmiMoveRoomDown.Click += new System.EventHandler(this.TsmiMoveRoomDownClick);
            // 
            // WldMap
            // 
            this.AllowDrop = true;
            this.Name = "WldMap";
            this.cmsMapMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        // ReSharper restore RedundantThisQualifier
        // ReSharper restore RedundantNameQualifier
        // ReSharper restore RedundantDelegateCreation
    }
}