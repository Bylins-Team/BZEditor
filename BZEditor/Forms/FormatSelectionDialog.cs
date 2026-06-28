using System;
using System.Drawing;
using System.Windows.Forms;
using DataUtils;

namespace BZEditor
{
    /// <summary>
    /// Dialog for selecting the world data format
    /// </summary>
    public class FormatSelectionDialog : Form
    {
        private ComboBox cboFormat;
        private Label lblDescription;
        private Label lblFormatLabel;
        private Button btnOK;
        private Button btnCancel;

        /// <summary>
        /// Gets the selected format name
        /// </summary>
        public string SelectedFormat { get; private set; }

        public FormatSelectionDialog()
        {
            InitializeComponent();
            LoadFormats();
        }

        private void InitializeComponent()
        {
            Text = "Формат данных";
            Size = new Size(400, 200);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;

            lblFormatLabel = new Label
            {
                Text = "Формат данных мира:",
                Location = new Point(12, 20),
                Size = new Size(150, 20)
            };

            cboFormat = new ComboBox
            {
                Location = new Point(12, 45),
                Size = new Size(360, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cboFormat.SelectedIndexChanged += CboFormat_SelectedIndexChanged;

            lblDescription = new Label
            {
                Text = "",
                Location = new Point(12, 80),
                Size = new Size(360, 40)
            };

            btnOK = new Button
            {
                Text = "OK",
                DialogResult = DialogResult.OK,
                Location = new Point(216, 130),
                Size = new Size(75, 25)
            };
            btnOK.Click += BtnOK_Click;

            btnCancel = new Button
            {
                Text = "Отмена",
                DialogResult = DialogResult.Cancel,
                Location = new Point(297, 130),
                Size = new Size(75, 25)
            };

            Controls.Add(lblFormatLabel);
            Controls.Add(cboFormat);
            Controls.Add(lblDescription);
            Controls.Add(btnOK);
            Controls.Add(btnCancel);

            AcceptButton = btnOK;
            CancelButton = btnCancel;
        }

        private void LoadFormats()
        {
            foreach (DataUtils.IFormatProvider provider in FormatProviderFactory.GetAllProviders())
            {
                cboFormat.Items.Add(provider.FormatName + " - " + provider.FormatDescription);
            }

            // Select current format
            string currentFormat = StaticData.WorldDataFormat;
            for (int i = 0; i < cboFormat.Items.Count; i++)
            {
                if (cboFormat.Items[i].ToString().StartsWith(currentFormat))
                {
                    cboFormat.SelectedIndex = i;
                    break;
                }
            }

            if (cboFormat.SelectedIndex < 0 && cboFormat.Items.Count > 0)
            {
                cboFormat.SelectedIndex = 0;
            }
        }

        private void CboFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFormat.SelectedIndex >= 0)
            {
                string selected = cboFormat.SelectedItem.ToString();
                string formatName = selected.Split('-')[0].Trim();
                DataUtils.IFormatProvider provider = FormatProviderFactory.GetProvider(formatName);
                if (provider != null)
                {
                    lblDescription.Text = provider.FormatDescription;
                }
            }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (cboFormat.SelectedIndex >= 0)
            {
                string selected = cboFormat.SelectedItem.ToString();
                SelectedFormat = selected.Split('-')[0].Trim();
                StaticData.WorldDataFormat = SelectedFormat;
            }
        }
    }
}
