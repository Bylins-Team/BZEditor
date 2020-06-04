using System.ComponentModel;

namespace ExtControls
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Text;
    using System.Windows.Forms;
    using DataUtils;

    public class WldSketch : UserControl
    {
        public delegate void SketchRoomAddEvent(int zoneNum);
        public event SketchRoomAddEvent SketchRoomAdded;
        public delegate void SketchRoomReplaceEvent(int zoneFromNum, int zoneToNum);
        public event SketchRoomReplaceEvent SketchRoomReplaced;
        public delegate void SketchRoomRemoveEvent(int zoneFromNum);
        public event SketchRoomRemoveEvent SketchRoomRemoved;
        public delegate void SketchZoneSelectEvent(int zoneNum);
        public event SketchZoneSelectEvent SketchZoneSelected;

        private const int ConstGSize = 6;
        /// <summary>
        /// Максимальное ограничение по оси Z
        /// </summary>
        public static int MaxZ = 3;

        /// <summary>
        /// Минимальное ограничение по оси Z
        /// </summary>
        public static int MinZ = -3;

        private const SmoothingMode SmoothMode = SmoothingMode.AntiAlias;
        private Bitmap bmp;
        private bool altPushed;
        private bool controlPushed;
        private bool shiftPushed;
        private bool leftMouseBtnPushed;
        private bool solidGridLines;
        private Color focusHighlightColor = SystemColors.ActiveCaption;
        private Color gridColor = SystemColors.ActiveBorder; //+
        private int roomSideSize = 16;
        private Point3D centerRoom = new Point3D(0, 0, 0);
        private Point mouseClickedPoint = new Point(-1, -1);
        private bool rightBtnScrolling;
        private Point startPoint = new Point(0, 0);
        private int mapScale = 4;
        private int curZoneNum = -1;
        private GlobalSketchRoom curRoom;
        private Point3D currentCoordinates;
        private GlobalSketch sketch = new GlobalSketch();

        public WldSketch()
        {
            bmp = new Bitmap(Width, Height);
        }

        public WldSketch(GlobalSketch sketch)
        {
            bmp = new Bitmap(Width, Height);
            this.sketch = sketch;
        }

        public GlobalSketch Sketch
        {
            get => sketch;
            set
            {
                sketch = value;
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
                roomSideSize = value * ConstGSize;
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
                RedrawBitmap();
            }
        }

        [Bindable(false), Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CenterRoomX
        {
            get => centerRoom.X;
            set
            {
                centerRoom.X = value;
                RedrawBitmap();
            }
        }

        [Bindable(false), Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CenterRoomY
        {
            get => centerRoom.Y;
            set
            {
                centerRoom.Y = value;
                RedrawBitmap();
            }
        }
        [Bindable(false), Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CurZoneNum
        {
            get => curZoneNum;
            set => curZoneNum = value;
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

        public Color FocusHighlightColor
        {
            get => focusHighlightColor;
            set => focusHighlightColor = value;
        }

        public bool SolidGridLines
        {
            get => solidGridLines;
            set => solidGridLines = value;
        }

        private int GetVisibleColumns()
        {
            return Width / (roomSideSize) + 2;
        }

        private int GetVisibleRows()
        {
            return Height / (roomSideSize) + 2;
        }

        private Point GetStartPoint()
        {
            return new Point((Width - (roomSideSize)) / 2, (Height - (roomSideSize)) / 2);
        }

        private Point GetStartRoomPoint()
        {
            return new Point(1 + (Width - (roomSideSize)) / 2, 1 + (Height - (roomSideSize)) / 2);
        }

        public void ToZeroPoint()
        {
            centerRoom = sketch.GetPointOfCenterRoomPlacedOnMap();
            RedrawBitmap();
        }

        private Point3D GetRoomCoordinatesByPoint(Point p)
        {
            var resPoint = new Point3D(0, 0, CenterRoomZ)
            {
                X = (Convert.ToInt32(((float)(p.X - Width / 2)) / (roomSideSize)) + centerRoom.X),
                Y = (Convert.ToInt32(((float)(p.Y - Height / 2)) / (roomSideSize)) + centerRoom.Y)
            };
            return resPoint;
        }

        public void AddNewZone(int number, string name, Color color)
        {
            sketch.AddZone(number, name, color);
        }

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

        private bool mouseOver;
        protected override void OnMouseEnter(EventArgs e)
        {
            mouseOver = true;
            base.OnMouseEnter(e);
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            mouseOver = false;
            base.OnMouseLeave(e);
            Invalidate();
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
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Focus();
            //CRoom room;
            if (e.Button == MouseButtons.Left)
            {
                mouseClickedPoint = e.Location;
                leftMouseBtnPushed = true;
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
            switch (e.Button)
            {
                case MouseButtons.Left:
                    leftMouseBtnPushed = false;
                    Point3D point1 = GetRoomCoordinatesByPoint(mouseClickedPoint);
                    Point3D point2 = GetRoomCoordinatesByPoint(e.Location);
                    if (point1.X == point2.X && point1.Y == point2.Y && point1.Z == point2.Z)
                    {
                        GlobalSketchRoom sr = sketch.GetSketchRoom(point1.X, point1.Y, point1.Z);
                        if (sr == null && CurZoneNum > -1 && !controlPushed)
                        {
                            if (sketch.GetRoomsCount(CurZoneNum) < StaticData.MaxRoomsPerZone)
                                sketch.AddSketchRoom(point1.X, point1.Y, point1.Z, CurZoneNum);
                            SketchRoomAdded?.Invoke(CurZoneNum);
                        }
                        else if (shiftPushed && sr != null)
                        {
                            sketch.Remove(sr);
                            sketch.AddSketchRoom(point1.X, point1.Y, point1.Z, CurZoneNum);
                            SketchRoomReplaced?.Invoke(sr.ZoneNum, CurZoneNum);
                        }
                        else if (controlPushed && sr != null)
                        {
                            sketch.Remove(sr);
                            SketchRoomRemoved?.Invoke(sr.ZoneNum);
                        }
                        else if (altPushed && sr != null)
                        {
                            SketchZoneSelected?.Invoke(sr.ZoneNum);
                        }
                        RedrawBitmap();
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
                        /*_tsmiMoveRoomUp.Enabled = SelectedRooms.Count > 0;
                        _tsmiMoveRoomDown.Enabled = SelectedRooms.Count > 0;
                        //if (mRoomsCollection.Exists(GetRoomCoordinatesByPoint(e.Location)))
                        _cmsMapMenu.Show(this, e.Location);*/
                    }
                    break;
            }
        }

        private Point3D lastAddedRoomCoordinates = new Point3D(-99999999, -99999999, -99999999);

        protected override void OnMouseMove(MouseEventArgs e)
        {
            //Драгдроп начинается тут из-за того, что надо иметь возможность просто кликнуть клетку для выделения
            //if (iMustStartDragging && !iMouseClickedPoint.Equals(e.Location) && iLeftMouseBtnPushed)
            bool mustRedraw = false;
            if (rightBtnScrolling)
            {
                int deltaX = (startPoint.X - e.X);
                int deltaY = (startPoint.Y - e.Y);
                if (Math.Abs(deltaX) > (roomSideSize))
                {
                    startPoint.X = e.X;
                    centerRoom.X += deltaX / (roomSideSize);
                    mustRedraw = true;
                }
                if (Math.Abs(deltaY) > (roomSideSize))
                {
                    startPoint.Y = e.Y;
                    centerRoom.Y += deltaY / (roomSideSize);
                    mustRedraw = true;
                }
                
                if (mustRedraw)
                    RedrawBitmap();
                return;
            }
            Point3D coord = GetRoomCoordinatesByPoint(e.Location);
            if (!Equals(coord, currentCoordinates))
            {
                if (leftMouseBtnPushed)
                {
                    if (lastAddedRoomCoordinates.X != coord.X || lastAddedRoomCoordinates.Y != coord.Y || lastAddedRoomCoordinates.Z != coord.Z)
                    {
                        GlobalSketchRoom sr = sketch.GetSketchRoom(coord.X, coord.Y, coord.Z);
                        if (sr == null && CurZoneNum > -1 && !controlPushed)
                        {
                            if (sketch.GetRoomsCount(CurZoneNum) < StaticData.MaxRoomsPerZone)
                            {
                                sketch.AddSketchRoom(coord.X, coord.Y, coord.Z, CurZoneNum);
                                SketchRoomAdded?.Invoke(CurZoneNum);
                                mustRedraw = true;
                            }
                        }
                        else if (shiftPushed && sr != null)
                        {
                            sketch.Remove(sr);
                            sketch.AddSketchRoom(coord.X, coord.Y, coord.Z, CurZoneNum);
                            SketchRoomReplaced?.Invoke(sr.ZoneNum, CurZoneNum);
                            mustRedraw = true;
                        }
                        else if (controlPushed && sr != null)
                        {
                            sketch.Remove(sr);
                            SketchRoomRemoved?.Invoke(sr.ZoneNum);
                            mustRedraw = true;
                        }
                        if (mustRedraw)
                        {
                            lastAddedRoomCoordinates = coord;
                            RedrawBitmap(false);
                        }
                    }
                }

                currentCoordinates = coord;
                if (curRoom == null || curRoom.X != coord.X || curRoom.Y != coord.Y || curRoom.Z != coord.Z)
                {
                    curRoom = sketch.GetSketchRoom(currentCoordinates.X, currentCoordinates.Y, currentCoordinates.Z) ??
                               new GlobalSketchRoom { X = -99999999, Y = -99999999, Z = -99999999, ZoneNum = -1 };
                    Invalidate();
                }
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            int step = e.Delta / 120;
            if (controlPushed) //Изменение масштаба
            {
                MapScale += step;
                return;
                //RecreateAllRoomsBitmaps();
            }
            if (shiftPushed && centerRoom.Z - step >= MinZ && centerRoom.Z - step <= MaxZ) //Изменение уровня по Z
            {
                centerRoom.Z -= step;
                currentCoordinates.Z -= step;
            }
            else if (altPushed) //Горизонтальное перемещение
            {
                centerRoom.X -= step;
                currentCoordinates.X -= step;

            }
            else //Вертикальное перемещение
            {
                centerRoom.Y -= step;
                currentCoordinates.Y -= step;
            }
            RedrawBitmap();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            RedrawBitmap();
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            RedrawBitmap();
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
                gr.DrawRectangle(new Pen(focusHighlightColor, (float)0.5), 0, 0, Width - 1, Height - 1);
                gr.Dispose();
                pevent.Graphics.DrawImageUnscaled(bmp, 0, 0);
                return;
            }
            pevent.Graphics.DrawImageUnscaled(bmp, 0, 0);
            pevent.Graphics.SmoothingMode = SmoothMode;

            if (mouseOver)
                DrawInfo(pevent.Graphics);
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

        public void RedrawBitmap()
        {
            RedrawBitmap(true);
        }

        public void RedrawBitmap(bool invalidate)
        {
            Graphics gr = Graphics.FromImage(bmp);
            gr.SmoothingMode = SmoothMode;
            gr.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            gr.Clear(BackColor);

            //Тут собсна рисовать эскиз :)
            DrawSketch(gr);

            //Тут рисуется сетка
            DrawGrid(gr);

            //Тут возможно какая то отрисовка поверх сетки

            //Отрисовка признака что карта в фокусе
            if (Focused)
                gr.DrawRectangle(new Pen(focusHighlightColor, (float)0.5), 0, 0, Width - 1, Height - 1);

            gr.Dispose();
            if (invalidate)
                Invalidate();
        }

        private void DrawInfo(Graphics gr)
        {
            using (Brush b = new SolidBrush(Color.FromArgb(150, Color.DarkBlue.R, Color.DarkBlue.G, Color.DarkBlue.B)))
            {
                string text = (currentCoordinates != null)
                                  ? $"Координаты: [{currentCoordinates.X}:{currentCoordinates.Y}:{currentCoordinates.Z}]"
                    : "";
                if (curRoom != null && curRoom.ZoneNum != -1)
                {
                    text += $"\nЗона: [{curRoom.ZoneNum}] {sketch.ZonesNames[curRoom.ZoneNum]}";
                    text += $"\nКомтнат в зоне {sketch.Count} из 98";
                }

                if (string.IsNullOrEmpty(text)) return;

                SizeF s = gr.MeasureString(text, Font);
                Rectangle r = new Rectangle(3, Height - Convert.ToInt32(s.Height) - 3, Convert.ToInt32(s.Width),
                                            Convert.ToInt32(s.Height));
                gr.FillRectangle(b, r);
                using (var p = new Pen(Color.DarkBlue))
                    gr.DrawRectangle(p, r);
                gr.DrawString(text, Font, new SolidBrush(Color.WhiteSmoke), 5, Height - Convert.ToInt32(s.Height) - 3);
            }
        }

        private void DrawSketch(Graphics gr)
        {
            if (sketch.Count == 0) return;
            int dx = GetVisibleColumns() / 2;
            int dy = GetVisibleRows() / 2;
            var b = new Bitmap(1, 1);
            const int delta = 2; //Желательно четное
            Point p = GetStartRoomPoint();
            foreach (GlobalSketchRoom room in sketch)
            {
                if (room.X < centerRoom.X - dx || room.X > centerRoom.X + dx || room.Y < centerRoom.Y - dy ||
                    room.Y > centerRoom.Y + dy || room.Z != centerRoom.Z) continue;
                using (Brush sb = new SolidBrush(sketch.ZonesColors[room.ZoneNum]))
                {
                    gr.FillRectangle(sb, p.X - delta / 2 + roomSideSize * (room.X - centerRoom.X),
                                     p.Y - delta / 2 + roomSideSize * (room.Y - centerRoom.Y),
                                     roomSideSize + delta - 1, roomSideSize + delta - 1);
                }
            }

            b.Dispose();
        }

        private void DrawGrid(Graphics gr)
        {
            Point stPoint = GetStartPoint();
            //iVisibleColumns = (int) (Math.Ceiling((double) (Width/(iRoomSideSize))));
            //iVisibleRows = (int) (Math.Ceiling((double) (Height/(iRoomSideSize))));
            Pen p = SolidGridLines ? new Pen(gridColor) : new Pen(new HatchBrush(HatchStyle.Percent50, gridColor, Color.Transparent));
            for (int f = 0; f < Width / (roomSideSize); f++)
            {
                gr.DrawLine(p, stPoint.X - (roomSideSize) * f, 0, stPoint.X - (roomSideSize) * f, Height);
                gr.DrawLine(p, stPoint.X + (roomSideSize) * f, 0, stPoint.X + (roomSideSize) * f, Height);
            }
            for (int f = 0; f < Height / (roomSideSize); f++)
            {
                gr.DrawLine(p, Width, stPoint.Y - (roomSideSize) * f, 0, stPoint.Y - (roomSideSize) * f);
                gr.DrawLine(p, Width, stPoint.Y + (roomSideSize) * f, 0, stPoint.Y + (roomSideSize) * f);
            }
        }

        #endregion
    }
}
