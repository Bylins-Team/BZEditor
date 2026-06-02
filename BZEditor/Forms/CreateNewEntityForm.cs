using System;
using System.Data;
using System.Windows.Forms;
using DataUtils;

namespace BZEditor
{
    public enum EntityType
    {
        Room,
        Object,
        Mob,
        Shop,
        Trigger
    }

    public partial class CreateNewEntityForm : BaseDialog
    {
        private string name = string.Empty;
        private readonly EntityType type;
        public int FirstCreatedNum = -1;
        private int maxAvail;
        private readonly TemplatesDataManager templatesDm;
        private readonly ZoneDataManager zoneDm;
        private readonly DataTable baseDt;

        public CreateNewEntityForm(ref ZoneDataManager zoneDm, DataTable baseDt, EntityType type)
        {
            InitializeComponent();
            this.baseDt = baseDt;
            this.type = type;
            this.zoneDm = zoneDm;
            SetName();
        }

        public CreateNewEntityForm(ref ZoneDataManager zoneDm, ref TemplatesDataManager templatesDm, EntityType type)
        {
            InitializeComponent();
            this.type = type;
            this.zoneDm = zoneDm;
            this.templatesDm = templatesDm;
            SetName();
        }

        private void SetName()
        {
            switch (type)
            {
                case EntityType.Mob:
                    name = "мобов";
                    break;
                case EntityType.Object:
                    name = "объектов";
                    break;
                case EntityType.Room:
                    name = "комнат";
                    cbUseTemplate.Text = "Выбрать тип сектора";
                    break;
            }
            Text = "Добавление новых " + name;
        }

        /// <summary>
        /// Обработка хотекеев
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {

                case Keys.Enter:
                    BtnCreateClick(null, null);
                    return true;
            }
            return false;
        }

        public void RefreshData()
        {
            cboxTemplate.DropDownStyle = ComboBoxStyle.DropDownList;
            cboxTemplate.Items.Clear();
            switch (type)
            {
                case EntityType.Mob:
                    foreach (Mob m in templatesDm.ConstMobsTemplates)
                        cboxTemplate.Items.Add(new TaggedComboBoxItem {Text = m.Cases.Imen, Tag = m.Guid});
                    foreach (Mob m in templatesDm.UserMobsTemplates)
                        cboxTemplate.Items.Add(new TaggedComboBoxItem {Text = m.Cases.Imen, Tag = m.Guid});
                    maxAvail = 100 - zoneDm.Mobs.Count;
                    break;
                case EntityType.Object:
                    foreach (Obj o in templatesDm.ConstObjectsTemplates)
                        cboxTemplate.Items.Add(new TaggedComboBoxItem { Text = o.Cases.Imen, Tag = o.Guid });
                    foreach (Obj o in templatesDm.UserObjectsTemplates)
                        cboxTemplate.Items.Add(new TaggedComboBoxItem { Text = o.Cases.Imen, Tag = o.Guid });
                    maxAvail = 100 - zoneDm.Objects.Count; 
                    break;
                case EntityType.Room:
                    foreach (DataRow dr in baseDt.Rows)
                        cboxTemplate.Items.Add(new TaggedComboBoxItem{Text = dr["desc"].ToString(),Tag = dr["val"].ToString()});
                    maxAvail = 99 - zoneDm.Rooms.Count; 
                    break;
            }
            if (cboxTemplate.Items.Count > 0)
            {
                cbUseTemplate.Enabled = true;
                cboxTemplate.SelectedIndex = 0;
            }
            else
                cbUseTemplate.Enabled = false;

            if (maxAvail > 0)
            {
                label1.Text = "Количество создаваемых " + name + " от 1 до " + maxAvail;
                nbCount.Maximum = maxAvail; nbCount.Minimum = 1;
                nbCount.Enabled = true;
            }
            else
            {
                label1.Text = "Достигнуто максимальное колич." + name;
                nbCount.Enabled = false;
            }

        }

        private void CbUseTemplateCheckedChanged(object sender, EventArgs e)
        {
            cboxTemplate.Enabled = cbUseTemplate.Checked;
        }

        private void BtnCancelClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void BtnCreateClick(object sender, EventArgs e)
        {
            switch (type)
            {
                case EntityType.Mob:
                    if (cbUseTemplate.Checked)
                    {
                        FirstCreatedNum = zoneDm.Mobs.AddMobs(Convert.ToInt32(nbCount.Value), zoneDm.Zone.Number,
                                                          templatesDm,
                                                          (Guid)
                                                          (((TaggedComboBoxItem) (cboxTemplate.SelectedItem)).Tag));
                    }
                    else
                    {
                        FirstCreatedNum = zoneDm.Mobs.AddMobs(Convert.ToInt32(nbCount.Value), zoneDm.Zone.Number,
                                                          templatesDm,
                                                          Guid.Empty);
                    }
                    maxAvail = 100 - zoneDm.Mobs.Count;
                    break;
                case EntityType.Object:
                    if (cbUseTemplate.Checked)
                    {
                        FirstCreatedNum = zoneDm.Objects.AddObjects(Convert.ToInt32(nbCount.Value), zoneDm.Zone.Number,
                                                                templatesDm,
                                                                (Guid)(((TaggedComboBoxItem)(cboxTemplate.SelectedItem)).Tag));
                    }
                    else
                    {
                        FirstCreatedNum = zoneDm.Objects.AddObjects(Convert.ToInt32(nbCount.Value), zoneDm.Zone.Number,
                                                                templatesDm, Guid.Empty);
                    }
                    maxAvail = 100 - zoneDm.Objects.Count;
                    break;
                case EntityType.Room:
                    if (cbUseTemplate.Checked)
                    {
                        FirstCreatedNum = zoneDm.Rooms.AddRooms(Convert.ToInt32(nbCount.Value),
                                                            zoneDm.Zone.Number,
                                                            Convert.ToInt32(
                                                                ((TaggedComboBoxItem)(cboxTemplate.SelectedItem)).Tag));
                    }
                    else
                    {
                        FirstCreatedNum = zoneDm.Rooms.AddRooms(Convert.ToInt32(nbCount.Value),
                                                            zoneDm.Zone.Number, -1);
                    }
                    maxAvail = 99 - zoneDm.Rooms.Count;
                    zoneDm.Zone.LastRoomNumber = Convert.ToInt32(zoneDm.Rooms.GetLastVNum().ToString().Replace(zoneDm.Zone.Number.ToString(), "")); 
                    break;
            }
            if (maxAvail > 0)
            {
                label1.Text = "Количество создаваемых " + name + " от 1 до " + maxAvail;
                nbCount.Maximum = maxAvail;
                nbCount.Enabled = true;
            }
            else
            {
                label1.Text = "Достигнуто максимальное колич." + name;
                nbCount.Enabled = false;
            }
            DialogResult = DialogResult.OK;
        }

        private void NbCountTextChanged(object sender, EventArgs e)
        {
            btnCreate.Enabled = (nbCount.Value > 0 && nbCount.Value <= maxAvail && nbCount.Text != "");
        }
    }
}