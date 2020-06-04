using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace ExtControls
{
    internal sealed class NativeMethods
    {
        public const int EP_EDITTEXT = 1;
        public const int ETS_DISABLED = 4;
        public const int ETS_NORMAL = 1;
        public const int ETS_READONLY = 6;
        public const int S_OK = 0x0;
        public const int WM_NCCALCSIZE = 0x83;
        public const int WM_NCPAINT = 0x85;
        public const int WM_THEMECHANGED = 0x031A;
        public const int WS_EX_CLIENTEDGE = 0x200;
        public const int WVR_HREDRAW = 0x100;
        public const int WVR_REDRAW = (WVR_HREDRAW | WVR_VREDRAW);
        public const int WVR_VREDRAW = 0x200;

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("gdi32.dll")]
        public static extern int ExcludeClipRect(IntPtr hdc, int nLeftRect, int nTopRect,
                                                 int nRightRect, int nBottomRect);

        [DllImport("UxTheme.dll", CharSet=CharSet.Auto)]
        public static extern bool IsAppThemed();

        [DllImport("UxTheme.dll", CharSet=CharSet.Auto)]
        public static extern bool IsThemeActive();

        [DllImport("comctl32.dll", CharSet=CharSet.Auto)]
        public static extern int DllGetVersion(ref DLLVersionInfo version);

        [DllImport("uxtheme.dll", ExactSpelling=true, CharSet=CharSet.Unicode)]
        public static extern IntPtr OpenThemeData(IntPtr hWnd, String classList);

        [DllImport("uxtheme.dll", ExactSpelling=true)]
        public static extern Int32 CloseThemeData(IntPtr hTheme);

        [DllImport("uxtheme", ExactSpelling=true)]
        public static extern Int32 DrawThemeBackground(IntPtr hTheme, IntPtr hdc, int iPartId,
                                                       int iStateId, ref RECT pRect, IntPtr pClipRect);

        [DllImport("uxtheme", ExactSpelling=true)]
        public static extern int IsThemeBackgroundPartiallyTransparent(IntPtr hTheme, int iPartId, int iStateId);

        [DllImport("uxtheme", ExactSpelling=true)]
        public static extern Int32 GetThemeBackgroundContentRect(IntPtr hTheme, IntPtr hdc
                                                                 , int iPartId, int iStateId, ref RECT pBoundingRect,
                                                                 out RECT pContentRect);

        [DllImport("uxtheme", ExactSpelling=true)]
        public static extern Int32 DrawThemeParentBackground(IntPtr hWnd, IntPtr hdc, ref RECT pRect);

        [DllImport("uxtheme", ExactSpelling=true)]
        public static extern Int32 DrawThemeBackground(IntPtr hTheme, IntPtr hdc, int iPartId,
                                                       int iStateId, ref RECT pRect, ref RECT pClipRect);

        #region Nested type: DLLVersionInfo

        [StructLayout(LayoutKind.Sequential)]
        public struct DLLVersionInfo
        {
            public int cbSize;
            public int dwBuildNumber;
            public int dwMajorVersion;
            public int dwMinorVersion;
            public int dwPlatformID;
        }

        #endregion

        #region Nested type: NCCALCSIZE_PARAMS

        [StructLayout(LayoutKind.Sequential)]
        public struct NCCALCSIZE_PARAMS
        {
            public IntPtr lppos;
            public RECT rgrc0, rgrc1, rgrc2;
        }

        #endregion

        #region Nested type: RECT

        [Serializable, StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Bottom;
            public int Left;
            public int Right;
            public int Top;

            public RECT(int left_, int top_, int right_, int bottom_)
            {
                Left = left_;
                Top = top_;
                Right = right_;
                Bottom = bottom_;
            }

            public int Height
            {
                get { return Bottom - Top + 1; }
            }

            public int Width
            {
                get { return Right - Left + 1; }
            }

            public Size Size
            {
                get { return new Size(Width, Height); }
            }

            public Point Location
            {
                get { return new Point(Left, Top); }
            }

            // Handy method for converting to a System.Drawing.Rectangle
            public Rectangle ToRectangle()
            {
                return Rectangle.FromLTRB(Left, Top, Right, Bottom);
            }

            public static RECT FromRectangle(Rectangle rectangle)
            {
                return new RECT(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);
            }

            public void Inflate(int width, int height)
            {
                Left -= width;
                Top -= height;
                Right += width;
                Bottom += height;
            }

            public override int GetHashCode()
            {
                return Left ^ ((Top << 13) | (Top >> 0x13))
                       ^ ((Width << 0x1a) | (Width >> 6))
                       ^ ((Height << 7) | (Height >> 0x19));
            }

            #region Operator overloads

            public static implicit operator Rectangle(RECT rect)
            {
                return Rectangle.FromLTRB(rect.Left, rect.Top, rect.Right, rect.Bottom);
            }

            public static implicit operator RECT(Rectangle rect)
            {
                return new RECT(rect.Left, rect.Top, rect.Right, rect.Bottom);
            }

            #endregion
        }

        #endregion
    }
}