using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace BZEditor
{
    /// <summary>
    /// Class handling persistent store of application settings in an XML file.
    /// There are several other techniques to do this, but they all seems to have serious drawbacks.
    /// The built in Settings from the Properties window is very nice but we can't choose where to put the file (or what to call it)...
    /// </summary>
#if INCLUDE_DESIGN_SUPPORT
  public class Settings: Component {
#else
    public class Configuration
    {
#endif
        //private static readonly string appPath = Application.StartupPath;
        private System.Configuration.Configuration configuration = null;
        private bool doReplaceDecimalChar = false;
        private readonly string file = "BZEditorConfig.xml";

        /// <summary>
        /// Constructor
        /// </summary>
        public Configuration()
        {
            CheckDecimalChar();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="file">The name of the configuration file (with or without path)</param>
        public Configuration(string file)
        {
            this.file = file;
            CheckDecimalChar();
        }

        /// <summary>
        /// Open file with settings
        /// </summary>
        public void Open()
        {
            // Try to open the configuration file
            ExeConfigurationFileMap exeConfigurationFileMap = new ExeConfigurationFileMap
            {
                ExeConfigFilename = file
            };
            configuration =
                ConfigurationManager.OpenMappedExeConfiguration(exeConfigurationFileMap, ConfigurationUserLevel.None);
        }

        /// <summary>
        /// Save settings to file
        /// </summary>
        public void Save()
        {
            if (configuration != null)
            {
                try
                {
                    configuration.Save();
                }
                catch (Exception)
                {
                    Open();
                    configuration.Save();
                }
            }
        }

        #region ********************************* Write *************************************

        /// <summary>
        /// Write string
        /// </summary>
        /// <param name="name">Name of setting</param>
        /// <param name="value">Value to store</param>
        public void Write(string name, string value)
        {
            WriteString(name, value);
        }

        /// <summary>
        /// Write int
        /// </summary>
        /// <param name="name">Name of setting</param>
        /// <param name="value">Value to store</param>
        public void Write(string name, int value)
        {
            WriteString(name, value.ToString());
        }

        /// <summary>
        /// Write bool 
        /// </summary>
        /// <param name="name">Name of setting</param>
        /// <param name="value">Value to store</param>
        public void Write(string name, bool value)
        {
            WriteString(name, value.ToString());
        }

        /// <summary>
        /// Write double
        /// </summary>
        /// <param name="name">Name of setting</param>
        /// <param name="value">Value to store</param>
        public void Write(string name, double value)
        {
            WriteString(name, DoubleToString(value));
        }

        /// <summary>
        /// Write size
        /// </summary>
        /// <param name="name">Name of setting</param>
        /// <param name="value">Value to store</param>
        public void Write(string name, Size value)
        {
            WriteString(name, value.ToString());
        }

        /// <summary>
        /// Write point
        /// </summary>
        /// <param name="name">Name of setting</param>
        /// <param name="value">Value to store</param>
        public void Write(string name, Point value)
        {
            WriteString(name, value.ToString());
        }

        /// <summary>
        /// Write FormWindowState
        /// </summary>
        /// <param name="name">Name of setting</param>
        /// <param name="value">Value to store</param>
        public void Write(string name, FormWindowState value)
        {
            Write(name, (int) value);
        }

        /// <summary>
        /// Write DockState
        /// </summary>
        /// <param name="name">Name of setting</param>
        /// <param name="value">Value to store</param>
        public void Write(string name, DockState value)
        {
            Write(name, (int) value);
        }

        /// <summary>
        /// Write Color
        /// </summary>
        /// <param name="name">Name of setting</param>
        /// <param name="value">Value to store</param>
        public void Write(string name, Color value)
        {
            Write(name, value.ToArgb());
        }

        #endregion

        #region ********************************* Read **************************************

        /// <summary>
        /// Read string
        /// </summary>
        /// <param name="name">Name of setting</param>
        /// <param name="defaultValue">Default value to return if missing</param>
        /// <returns>Value</returns>
        public string Read(string name, string defaultValue)
        {
            return ReadString(name, defaultValue);
        }

        /// <summary>
        /// Read int
        /// </summary>
        /// <param name="name">Name of setting</param>
        /// <param name="defaultValue">Default value to return if missing</param>
        /// <returns>Value</returns>
        public int Read(string name, int defaultValue)
        {
            return int.Parse(ReadString(name, defaultValue.ToString()));
        }

        /// <summary>
        /// Read bool
        /// </summary>
        /// <param name="name">Name of setting</param>
        /// <param name="defaultValue">Default value to return if missing</param>
        /// <returns>Value</returns>
        public bool Read(string name, bool defaultValue)
        {
            return bool.Parse(ReadString(name, defaultValue.ToString()));
        }

        /// <summary>
        /// Read double
        /// </summary>
        /// <param name="name">Name of setting</param>
        /// <param name="defaultValue">Default value to return if missing</param>
        /// <returns>Value</returns>
        public double Read(string name, double defaultValue)
        {
            return StringToDouble(ReadString(name, DoubleToString(defaultValue)));
        }

        /// <summary>
        /// Read Size
        /// </summary>
        /// <param name="name">Name of setting</param>
        /// <param name="defaultValue">Default value to return if missing</param>
        /// <returns>Value</returns>
        public Size Read(string name, Size defaultValue)
        {
            return StringToSize(ReadString(name, defaultValue.ToString()));
        }

        /// <summary>
        /// Read Point
        /// </summary>
        /// <param name="name">Name of setting</param>
        /// <param name="defaultValue">Default value to return if missing</param>
        /// <returns>Value</returns>
        public Point Read(string name, Point defaultValue)
        {
            return StringToPoint(ReadString(name, defaultValue.ToString()));
        }

        /// <summary>
        /// Read DockState
        /// </summary>
        /// <param name="name">Name of setting</param>
        /// <param name="defaultValue">Default value to return if missing</param>
        /// <returns>Value</returns>
        internal DockState Read(string name, DockState defaultValue)
        {
            return StringToDockState(ReadString(name, ((int)defaultValue).ToString()));
        }

        /// <summary>
        /// Read FormWindowState
        /// </summary>
        /// <param name="name">Name of setting</param>
        /// <param name="defaultValue">Default value to return if missing</param>
        /// <returns>Value</returns>
        public FormWindowState Read(string name, FormWindowState defaultValue)
        {
            return StringToFormWindowState(ReadString(name, ((int) defaultValue).ToString()));
        }

        /// <summary>
        /// Read Color
        /// </summary>
        /// <param name="name">Name of setting</param>
        /// <param name="defaultValue">Default value to return if missing</param>
        /// <returns>Value</returns>
        public Color Read(string name, Color defaultValue)
        {
            return StringToColor(ReadString(name, defaultValue.ToArgb().ToString()));
        }

        #endregion

        #region ********************************* Misc **************************************

        // Write setting as string
        private void WriteString(string name, string value)
        {
            if (configuration != null)
            {
                KeyValueConfigurationElement key = configuration.AppSettings.Settings[name];
                if (key != null) key.Value = value;
                else configuration.AppSettings.Settings.Add(name, value);
            }
        }

        // Read setting as string
        private string ReadString(string name, string defaultValue)
        {
            if (configuration != null)
            {
                KeyValueConfigurationElement key = configuration.AppSettings.Settings[name];
                if (key != null) return key.Value;
            }
            return defaultValue;
        }

        // Get the decimal character used in current environment
        // Note! Always uses "." as double separator 
        private void CheckDecimalChar()
        {
            string dummy = "" + 0.1M;
            char localDoubleChar = dummy[1];
            if (localDoubleChar == ',') doReplaceDecimalChar = true;
        }

        // Convert double to ascii representation
        private string DoubleToString(double data)
        {
            String dataConverted = data.ToString();
            if (doReplaceDecimalChar) dataConverted = dataConverted.Replace(',', '.');
            return dataConverted;
        }

        // Convert ascii representation to double
        private double StringToDouble(string data)
        {
            if (doReplaceDecimalChar) data = data.Replace('.', ',');
            return double.Parse(data);
        }

        // Convert ascii representation to Size
        private Size StringToSize(string data)
        {
            Size res = new Size(0, 0);
            Regex r = new Regex("{Width=(?<Width>\\d+), Height=(?<Height>\\d+)}");
            Match m = r.Match(data);
            if (m.Success)
            {
                GroupCollection gc = m.Groups;
                res.Width = Convert.ToInt32(gc["Width"].Value);
                res.Height = Convert.ToInt32(gc["Height"].Value);
            }
            return res;
        }

        /// <summary>
        /// Convert ascii representation to Point
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private Point StringToPoint(string data)
        {
            Point res = new Point(0, 0);
            Regex r = new Regex("{X=(?<X>\\d+), Y=(?<Y>\\d+)}");
            Match m = r.Match(data);
            if (m.Success)
            {
                GroupCollection gc = m.Groups;
                res.X = Convert.ToInt32(gc["X"].Value);
                res.Y = Convert.ToInt32(gc["Y"].Value);
            }
            return res;
        }

        /// <summary>
        /// Convert ascii representation to FormWindowState
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private FormWindowState StringToFormWindowState(string data)
        {
            return (FormWindowState)(int.Parse(data));
        }

        /// <summary>
        /// Convert ascii representation to DockState
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private DockState StringToDockState(string data)
        {
            return (DockState)(int.Parse(data));
        }

        /// <summary>
        /// Convert ascii representation to Color
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private Color StringToColor(string data)
        {
            return Color.FromArgb(int.Parse(data));
        }

        /// <summary>
        /// Проверяет наличие указанного пути, и если путь не верен, создает указанный путь
        /// </summary>
        /// <param name="path"></param>
        public static void EnsureDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public uint ColorToUInt(Color color)
        {
            uint colorValue = /*(uint)color. << 24 | */ (uint) color.B << 16 | (uint) color.G << 8 | color.R;
            return colorValue;
        }

        #endregion
    }
}