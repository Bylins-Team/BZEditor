using System.Drawing;

namespace DataUtils
{
    public class ExitColors : BaseDataObject
    {
        private Color fColorExitD = Color.Transparent;
        private Color fColorExitE = Color.Transparent;
        private Color fColorExitN = Color.Transparent;
        private Color fColorExitS = Color.Transparent;
        private Color fColorExitU = Color.Transparent;
        private Color fColorExitW = Color.Transparent;

        public ExitColors()
        {
        }

        public ExitColors(Color colorExitN, Color colorExitE, Color colorExitS, Color colorExitW, Color colorExitU,
                           Color colorExitD)
        {
            fColorExitN = colorExitN;
            fColorExitE = colorExitE;
            fColorExitS = colorExitS;
            fColorExitW = colorExitW;
            fColorExitU = colorExitU;
            fColorExitD = colorExitD;
        }

        /// <summary>
        /// Цвет выхода на север
        /// </summary>
        public Color ColorExitN
        {
            get { return fColorExitN; }
            set
            {
                fColorExitN = value;
                //FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Цвет выхода на восток
        /// </summary>
        public Color ColorExitE
        {
            get { return fColorExitE; }
            set
            {
                fColorExitE = value;
                //FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Цвет выхода на юг
        /// </summary>
        public Color ColorExitS
        {
            get { return fColorExitS; }
            set
            {
                fColorExitS = value;
                //FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Цвет выхода на запад
        /// </summary>
        public Color ColorExitW
        {
            get { return fColorExitW; }
            set
            {
                fColorExitW = value;
                //FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Цвет выхода вверх
        /// </summary>
        public Color ColorExitU
        {
            get { return fColorExitU; }
            set
            {
                fColorExitU = value;
                //FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Цвет выхода вниз
        /// </summary>
        public Color ColorExitD
        {
            get { return fColorExitD; }
            set
            {
                fColorExitD = value;
                //FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Сброс выходов в исходное состояние (отобрадение выхода на карте зависит от цвета выхода)
        /// Color.Transparent не отображается
        /// </summary>
        public void Reset()
        {
            fColorExitN = Color.Transparent;
            fColorExitE = Color.Transparent;
            fColorExitS = Color.Transparent;
            fColorExitW = Color.Transparent;
            fColorExitU = Color.Transparent;
            fColorExitD = Color.Transparent;
            //FireChangeEvent(this);
        }

        public ExitColors Clone()
        {
            var res = new ExitColors(ColorExitN, ColorExitE, ColorExitS, ColorExitW, ColorExitU, ColorExitD);
            /*res.ColorExitD = ColorExitD;
            res.ColorExitE = ColorExitE;
            res.ColorExitN = ColorExitN;
            res.ColorExitS = ColorExitS;
            res.ColorExitU = ColorExitU;
            res.ColorExitW = ColorExitW;*/
            return res;
        }
    }
}