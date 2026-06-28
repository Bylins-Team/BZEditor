#region

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using DataUtils;

#endregion

namespace ExtControls
{
    internal class RoomDrawer
    {
        public static Bitmap GetRoom(SmoothingMode smoothMode, int gSize, int mapScale, int exitScale,
                                     Color roomBgColorTop, Color roomBgColorBottom, ExitColors exitColors, bool drawO,
                                     bool drawM, bool drawT, Font f, string vnum)
        {
            Size roomSize = new Size((gSize*mapScale) - 2, (gSize*mapScale) - 2);
            int exitSize = (int) Math.Ceiling((double) mapScale/3);
            long res = Math.DivRem(roomSize.Width, exitScale, out var rem);
            float exitSizeX = (rem > 0) ? res + 1 : res;
            res = Math.DivRem(roomSize.Height, exitScale, out rem);
            float exitSizeY = (rem > 0) ? res + 1 : res;
            Color colorBright = Color.White;
            Color colorDark = Color.Black;
            Bitmap resbmp = new Bitmap(roomSize.Width + 1, roomSize.Height + 1);
            using (Graphics gr = Graphics.FromImage(resbmp))
            {
                gr.SmoothingMode = smoothMode;
                gr.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                using (
                    LinearGradientBrush br =
                        new LinearGradientBrush(new Rectangle(0, 0, roomSize.Width, roomSize.Height), roomBgColorTop,
                                                roomBgColorBottom, 90F, false))
                    //Brush br = new SolidBrush(RoomBGColorBottom);
                    gr.FillRectangle(br, 0, 0, roomSize.Width, roomSize.Height);
                gr.DrawLine(new Pen(colorBright), 0, 0, roomSize.Width, 0);
                gr.DrawLine(new Pen(colorBright), 0, 0, 0, roomSize.Height);
                gr.DrawLine(new Pen(colorDark), roomSize.Width, 0, roomSize.Width, roomSize.Height);
                gr.DrawLine(new Pen(colorDark), 0, roomSize.Height, roomSize.Width, roomSize.Height);

                //Выходы
                if (exitColors.ColorExitN != Color.Transparent)
                {
                    gr.FillRectangle(new SolidBrush(exitColors.ColorExitN), GetCenterX(roomSize) - exitSizeX/2, 0,
                                     exitSizeX, exitSize);
                    if (mapScale > 1)
                    {
                        gr.DrawLine(new Pen(colorBright), GetCenterX(roomSize) - exitSizeX/2, exitSize,
                                    GetCenterX(roomSize) + exitSizeX/2, exitSize);
                        gr.DrawLine(new Pen(colorBright), GetCenterX(roomSize) + exitSizeX/2, 0,
                                    GetCenterX(roomSize) + exitSizeX/2, exitSize);
                        gr.DrawLine(new Pen(colorDark), GetCenterX(roomSize) - exitSizeX/2, 0,
                                    GetCenterX(roomSize) - exitSizeX/2, exitSize);
                    }
                }

                if (exitColors.ColorExitE != Color.Transparent)
                {
                    gr.FillRectangle(new SolidBrush(exitColors.ColorExitE), roomSize.Width - exitSize,
                                     GetCenterY(roomSize) - exitSizeY/2, exitSize + 1, exitSizeY);
                    if (mapScale > 1)
                    {
                        gr.DrawLine(new Pen(colorDark), roomSize.Width - exitSize, GetCenterY(roomSize) - exitSizeY/2,
                                    roomSize.Width - exitSize, GetCenterY(roomSize) + exitSizeY/2);
                        gr.DrawLine(new Pen(colorBright), roomSize.Width, GetCenterY(roomSize) + exitSizeY/2,
                                    roomSize.Width - exitSize, GetCenterY(roomSize) + exitSizeY/2);
                        gr.DrawLine(new Pen(colorDark), roomSize.Width, GetCenterY(roomSize) - exitSizeY/2,
                                    roomSize.Width - exitSize, GetCenterY(roomSize) - exitSizeY/2);
                    }
                }

                if (exitColors.ColorExitS != Color.Transparent)
                {
                    gr.FillRectangle(new SolidBrush(exitColors.ColorExitS), GetCenterX(roomSize) - exitSizeX/2,
                                     roomSize.Height - exitSize, exitSizeX, exitSize + 1);
                    if (mapScale > 1)
                    {
                        gr.DrawLine(new Pen(colorDark), GetCenterX(roomSize) - exitSizeX/2, roomSize.Height - exitSize,
                                    GetCenterX(roomSize) + exitSizeX/2, roomSize.Height - exitSize);
                        gr.DrawLine(new Pen(colorBright), GetCenterX(roomSize) + exitSizeX/2, roomSize.Height,
                                    GetCenterX(roomSize) + exitSizeX/2, roomSize.Height - exitSize);
                        gr.DrawLine(new Pen(colorDark), GetCenterX(roomSize) - exitSizeX/2, roomSize.Height,
                                    GetCenterX(roomSize) - exitSizeX/2, roomSize.Height - exitSize);
                    }
                }

                if (exitColors.ColorExitW != Color.Transparent)
                {
                    gr.FillRectangle(new SolidBrush(exitColors.ColorExitW), 0, GetCenterY(roomSize) - exitSizeY/2,
                                     exitSize, exitSizeY);
                    if (mapScale > 1)
                    {
                        gr.DrawLine(new Pen(colorBright), exitSize, GetCenterY(roomSize) - exitSizeY/2, exitSize,
                                    GetCenterY(roomSize) + exitSizeY/2);
                        gr.DrawLine(new Pen(colorBright), 0, GetCenterY(roomSize) + exitSizeY/2, exitSize,
                                    GetCenterY(roomSize) + exitSizeY/2);
                        gr.DrawLine(new Pen(colorDark), 0, GetCenterY(roomSize) - exitSizeY/2, exitSize,
                                    GetCenterY(roomSize) - exitSizeY/2);
                    }
                }

                if (exitColors.ColorExitU != Color.Transparent)
                {
                    PointF point1 = new PointF(GetCenterX(roomSize) - exitSizeX/2, exitSizeX + exitSize*2);
                    PointF point2 = new PointF(GetCenterX(roomSize), exitSize*2);
                    PointF point3 = new PointF(GetCenterX(roomSize) + exitSizeX/2, exitSizeX + exitSize*2);
                    gr.FillPolygon(new SolidBrush(exitColors.ColorExitU), new[] {point1, point2, point3});
                    if (mapScale > 1)
                    {
                        gr.DrawLine(new Pen(colorDark), point1, point2);
                        gr.DrawLine(new Pen(colorBright), point2, point3);
                        gr.DrawLine(new Pen(colorBright), point3, point1);
                    }
                }

                if (exitColors.ColorExitD != Color.Transparent)
                {
                    PointF point1 =
                        new PointF(GetCenterX(roomSize) - exitSizeX/2, roomSize.Height + 1 - (exitSizeX + exitSize));
                    PointF point2 = new PointF(GetCenterX(roomSize), roomSize.Height + 1 - exitSize*2);
                    PointF point3 =
                        new PointF(GetCenterX(roomSize) + exitSizeX/2, roomSize.Height + 1 - (exitSizeX + exitSize));
                    gr.FillPolygon(new SolidBrush(exitColors.ColorExitD), new[] {point1, point2, point3});
                    if (mapScale > 1)
                    {
                        gr.DrawLine(new Pen(colorDark), point1, point2);
                        gr.DrawLine(new Pen(colorDark), point3, point1);
                        gr.DrawLine(new Pen(colorBright), point2, point3);
                    }
                }

                if (drawO)
                {
                    gr.FillRectangle(new SolidBrush(Color.FromArgb(219, 233, 0)), 2,
                                     roomSize.Height - roomSize.Height/4 - 2, roomSize.Width/4, roomSize.Height/4);
                    gr.DrawRectangle(new Pen(Color.DimGray), 2, roomSize.Height - roomSize.Height/4 - 2,
                                     roomSize.Width/4, roomSize.Height/4);
                }
                if (drawM)
                {
                    gr.FillRectangle(new SolidBrush(Color.FromArgb(235, 0, 0)), roomSize.Width - roomSize.Width/4 - 2, 2,
                                     roomSize.Width/4, roomSize.Height/4);
                    gr.DrawRectangle(new Pen(Color.DimGray), roomSize.Width - roomSize.Width/4 - 2, 2, roomSize.Width/4,
                                     roomSize.Height/4);
                }
                if (drawT)
                {
                    gr.FillRectangle(new SolidBrush(Color.FromArgb(125, 30, 205)), roomSize.Width - roomSize.Width/4 - 2,
                                     roomSize.Height - roomSize.Height/4 - 2, roomSize.Width/4, roomSize.Height/4);
                    gr.DrawRectangle(new Pen(Color.DimGray), roomSize.Width - roomSize.Width/4 - 2,
                                     roomSize.Height - roomSize.Height/4 - 2, roomSize.Width/4, roomSize.Height/4);
                }
                if (mapScale > 2 && vnum != "")
                {
                    string num = vnum.Substring(vnum.Length - 2).TrimStart('0');
                    using (Font fnt = new Font(f.FontFamily, 1.5f*mapScale))
                    using (Pen p = new Pen(Color.DarkBlue))
                    using (
                        Brush b =
                            new SolidBrush(Color.FromArgb(100, Color.DarkBlue.R, Color.DarkBlue.G, Color.DarkBlue.B)))
                    {
                        SizeF sf = gr.MeasureString(num, fnt);
                        Rectangle r = new Rectangle(2, 2, Convert.ToInt32(sf.Width), Convert.ToInt32(sf.Height));
                        gr.FillRectangle(b, r);
                        gr.DrawRectangle(p, r);
                        gr.DrawString(num, fnt, new SolidBrush(Color.White), new PointF(3f, 3f));
                    }
                }
            }
            return resbmp;
        }

        private static float GetCenterX(Size roomSize)
        {
            return roomSize.Width/2;
        }

        private static float GetCenterY(Size roomSize)
        {
            return roomSize.Height/2;
        }
    }
}