using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using DataUtils;
using Fireball.Windows.Forms;

namespace BZEditor
{
    public partial class CreateSketchZoneForm : BaseDialog
    {
        private readonly GlobalSketch sketch;

        public CreateSketchZoneForm()
        {
            InitializeComponent();
        }

        public CreateSketchZoneForm(GlobalSketch sketch):this()
        {
            this.sketch = sketch;
            btnSketchColor.BackColor = this.sketch.GenerateRandomColor();
        }

        public int ZoneNum
        {
            get { return Convert.ToInt32(nbCount.Value); }
            set { nbCount.Text = value.ToString(); }
        }

        public string ZoneName
        {
            get { return tbZoneName.Text; }
            set { tbZoneName.Text = value; }
        }

        public Color ZoneSketchColor
        {
            get { return btnSketchColor.BackColor; }
            set { btnSketchColor.BackColor = value; }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void nbCount_TextChanged(object sender, EventArgs e)
        {
            btnCreate.Enabled = (nbCount.Text != "");
        }

        private void flcAlert_ClickLink(object sender, ClickLinkEventArgs e)
        {
            Process.Start(
                "mailto:belobog@mud.ru?subject=Жажду билдить, дайте номер&body=Ну дайте же мне скорее номер зоны!!!НУ ПАЖАЛАСТА!!!Я такой ленивый что даже не написал ни строчки сам");
        }

        private void btnGenerateRndColor_Click(object sender, EventArgs e)
        {
            btnSketchColor.BackColor = sketch.GenerateRandomColor();
            Text = (btnSketchColor.BackColor.R + btnSketchColor.BackColor.G + btnSketchColor.BackColor.B).ToString();
        }

        private void btnSketchColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog
            {
                Color = btnSketchColor.BackColor
            };
            if (cd.ShowDialog(this) == DialogResult.OK)
                //if (!_sketch.ColorExist(cd.Color))
                btnSketchColor.BackColor = cd.Color;
            cd.Dispose();
        }
    }
}