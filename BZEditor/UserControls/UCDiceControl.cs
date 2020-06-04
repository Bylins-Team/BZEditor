using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BZEditor
{
    /// <summary>
    /// Summary description for DiceControl.
    /// </summary>
    public class UcDiceControl : UserControl
    {
        #region Delegates

        public delegate void ValueChangeEvent(object sender, EventArgs e);

        #endregion

        private Button bAction;
        private GroupBox groupBox;
        private Label Label3;
        private NumericUpDown nudParam1;
        private NumericUpDown nudParam2;
        private ErrorProvider errorProvider;
        private IContainer components;
        private NumericUpDown nudParamConst;

        public UcDiceControl()
        {
            MinRandomValue = 0;
            InitializeComponent();
        }

        public string Value
        {
            get => $"{nudParam1.Value}d{nudParam2.Value}{bAction.Text}{nudParamConst.Value}";
            set
            {

                if (!new Regex("^(?<nudParam1>\\d+)\\w(?<nudParam2>\\d+)(?<sign>.)(?<nudParamConst>\\d+)").Match(value).Success) return;

                nudParam1.Value = Convert.ToInt32(new Regex("^(?<nudParam1>\\d+)\\w(?<nudParam2>\\d+)(?<sign>.)(?<nudParamConst>\\d+)").Match(value).Groups["nudParam1"].ToString().Trim());
                nudParam2.Value = Convert.ToInt32(new Regex("^(?<nudParam1>\\d+)\\w(?<nudParam2>\\d+)(?<sign>.)(?<nudParamConst>\\d+)").Match(value).Groups["nudParam2"].ToString().Trim());
                nudParamConst.Value = Convert.ToInt32(new Regex("^(?<nudParam1>\\d+)\\w(?<nudParam2>\\d+)(?<sign>.)(?<nudParamConst>\\d+)").Match(value).Groups["nudParamConst"].ToString().Trim());
                bAction.Text = value.IndexOf("+", StringComparison.Ordinal) > 0 ? "+" : "-";
            }
        }

        public bool SignFixed {
            get => !bAction.Enabled;
            set => bAction.Enabled = !value;
        }

        public int MinRandomValue { get; set; }

        public string LabelText
        {
            get => groupBox.Text;
            set => groupBox.Text = value;
        }

        public int Param1
        {
            get => (int)nudParam1.Value;
            set => nudParam1.Value = value;
        }

        public int Param2
        {
            get => (int)nudParam2.Value;
            set => nudParam2.Value = value;
        }

        public int ParamConst
        {
            get => (int)nudParamConst.Value;
            set => nudParamConst.Value = value;
        }

        public decimal Param1Max
        {
            get => nudParam1.Maximum;
            set => nudParam1.Maximum = value;
        }

        public decimal Param2Max
        {
            get => nudParam2.Maximum;
            set => nudParam2.Maximum = value;
        }

        public decimal ParamConstMax
        {
            get => nudParamConst.Maximum;
            set => nudParamConst.Maximum = value;
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.nudParam1 = new System.Windows.Forms.NumericUpDown();
            this.nudParam2 = new System.Windows.Forms.NumericUpDown();
            this.nudParamConst = new System.Windows.Forms.NumericUpDown();
            this.Label3 = new System.Windows.Forms.Label();
            this.bAction = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudParam1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudParam2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudParamConst)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox.Controls.Add(this.nudParam1);
            this.groupBox.Controls.Add(this.nudParam2);
            this.groupBox.Controls.Add(this.nudParamConst);
            this.groupBox.Controls.Add(this.Label3);
            this.groupBox.Controls.Add(this.bAction);
            this.groupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.errorProvider.SetIconAlignment(this.groupBox, System.Windows.Forms.ErrorIconAlignment.TopRight);
            this.errorProvider.SetIconPadding(this.groupBox, -16);
            this.groupBox.Location = new System.Drawing.Point(0, 0);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(192, 42);
            this.groupBox.TabIndex = 12;
            this.groupBox.TabStop = false;
            // 
            // nudParam1
            // 
            this.nudParam1.Location = new System.Drawing.Point(4, 16);
            this.nudParam1.Maximum = new decimal(new int[] {
            276447231,
            23283,
            0,
            0});
            this.nudParam1.Name = "nudParam1";
            this.nudParam1.Size = new System.Drawing.Size(50, 20);
            this.nudParam1.TabIndex = 10;
            this.nudParam1.ValueChanged += new System.EventHandler(this.nudParam_ValueChanged);
            // 
            // nudParam2
            // 
            this.nudParam2.Location = new System.Drawing.Point(66, 16);
            this.nudParam2.Maximum = new decimal(new int[] {
            1661992959,
            1808227885,
            5,
            0});
            this.nudParam2.Name = "nudParam2";
            this.nudParam2.Size = new System.Drawing.Size(50, 20);
            this.nudParam2.TabIndex = 10;
            this.nudParam2.ValueChanged += new System.EventHandler(this.nudParam_ValueChanged);
            // 
            // nudParamConst
            // 
            this.nudParamConst.Location = new System.Drawing.Point(139, 16);
            this.nudParamConst.Maximum = new decimal(new int[] {
            1874919423,
            2328306,
            0,
            0});
            this.nudParamConst.Name = "nudParamConst";
            this.nudParamConst.Size = new System.Drawing.Size(50, 20);
            this.nudParamConst.TabIndex = 10;
            this.nudParamConst.ValueChanged += new System.EventHandler(this.nudParam_ValueChanged);
            // 
            // Label3
            // 
            this.Label3.Location = new System.Drawing.Point(53, 18);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(16, 16);
            this.Label3.TabIndex = 8;
            this.Label3.Text = "d";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bAction
            // 
            this.bAction.BackColor = System.Drawing.SystemColors.Control;
            this.bAction.FlatAppearance.BorderSize = 0;
            this.bAction.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.bAction.Location = new System.Drawing.Point(118, 16);
            this.bAction.Name = "bAction";
            this.bAction.Size = new System.Drawing.Size(19, 20);
            this.bAction.TabIndex = 13;
            this.bAction.Text = "+";
            this.bAction.UseVisualStyleBackColor = false;
            this.bAction.Click += new System.EventHandler(this.bAction_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // UcDiceControl
            // 
            this.Controls.Add(this.groupBox);
            this.Name = "UcDiceControl";
            this.Size = new System.Drawing.Size(192, 42);
            this.groupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudParam1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudParam2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudParamConst)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public event ValueChangeEvent ValueChanged;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }

        private void bAction_Click(object sender, EventArgs e)
        {
            bAction.Text = bAction.Text == "+" ? "-" : "+";
            if (Check())
                ValueChanged?.Invoke(this, new EventArgs());
        }

        private void nudParam_ValueChanged(object sender, EventArgs e)
        {
            if (Check())
                ValueChanged?.Invoke(this, new EventArgs());
        }

        bool Check()
        {
            decimal constVal = nudParamConst.Value;
            decimal minVal2 = nudParam2.Value > 0 ? 1 : 0;
            if (bAction.Text == "-") constVal = constVal * -1;
            if (nudParam1.Value * minVal2 + constVal < MinRandomValue)
            {
                errorProvider.SetError(groupBox, $" Минимальное значение выкинутое на костях\nне должно быть меньше {MinRandomValue}");
                return false;
            }
            errorProvider.SetError(groupBox, "");
            return true;
        }
    }
}