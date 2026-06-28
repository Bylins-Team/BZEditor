using System;
using System.Windows.Forms;
using DataUtils;
using Object = DataUtils.Obj;

namespace BZEditor
{
    public partial class CreateClonesForm : BaseDialog
    {
        private int maxAvail;
        private readonly EntityType prototype;
        private readonly TemplatesDataManager templatesDm;
        private readonly ZoneDataManager zoneDm;
        public int FirstCreatedNum = -1;

        public CreateClonesForm()
        {
        }

        public CreateClonesForm(ref ZoneDataManager zoneDm, ref TemplatesDataManager templatesDm, EntityType prototype)
        {
            InitializeComponent();
            this.prototype = prototype;
            this.zoneDm = zoneDm;
            this.templatesDm = templatesDm;
            cboxFullCopy.Checked = prototype != EntityType.Room;
            switch (prototype)
            {
                case EntityType.Room:
                    tbNewName.Text = $"Клон комнаты {templatesDm.RoomClip.Name}";
                    break;
                case EntityType.Mob:
                    tbNewName.Text = $"Клон моба {templatesDm.MobClip.Cases.Imen}";
                    break;
                case EntityType.Object:
                    tbNewName.Text = $"Клон объекта {templatesDm.ObjClip.Cases.Imen}";
                    break;
            }
            RefreshData();
        }

        private void RefreshData()
        {
            switch (prototype)
            {
                case EntityType.Room:
                    maxAvail = 97 - zoneDm.Rooms.Count;
                    break;
                case EntityType.Mob:
                    maxAvail = 99 - zoneDm.Mobs.Count;
                    break;
                case EntityType.Object:
                    maxAvail = 99 - zoneDm.Objects.Count;
                    break;
            }
            if (maxAvail > 0)
            {
                label1.Text = $"Количество добавляемых клонов от 1 до {maxAvail}";
                nbCount.Maximum = maxAvail; nbCount.Minimum = 1;
                nbCount.Enabled = true;
                btnCreate.Enabled = true;
            }
            else
            {
                label1.Text = "Достигнуто максимальное колич.клонов";
                nbCount.Enabled = false;
                btnCreate.Enabled = false;
            }
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

        private void CbChangeNameCheckedChanged(object sender, EventArgs e)
        {
            tbNewName.ReadOnly = !cbChangeName.Checked;
        }

        private void BtnCancelClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void BtnCreateClick(object sender, EventArgs e)
        {
            for (int i = 1; i <= nbCount.Value; i++)
            {
                switch (prototype)
                {
                    case EntityType.Room:
                        Room newRoom = zoneDm.Rooms.AddRoom(zoneDm.Zone.Number);
                        templatesDm.ApplyClipAsTemplate(ref newRoom);
                        if (cbChangeName.Checked)
                            newRoom.Name = tbNewName.Text;
                        if (FirstCreatedNum == -1)
                            FirstCreatedNum = newRoom.VNum;
                        break;
                    case EntityType.Mob:
                        Mob newMob = zoneDm.Mobs.AddMob(zoneDm.Zone.Number);
                        templatesDm.ApplyClipAsTemplate(ref newMob, cboxFullCopy.Checked);
                        if (cbChangeName.Checked)
                        {
                            newMob.Cases.Imen = tbNewName.Text;
                            newMob.Cases.Dat = string.Empty;
                            newMob.Cases.Pred = string.Empty;
                            newMob.Cases.Rod = string.Empty;
                            newMob.Cases.Tvor = string.Empty;
                            newMob.Cases.Vin = string.Empty;
                        }
                        if (FirstCreatedNum == -1)
                            FirstCreatedNum = newMob.VNum;
                        break;
                    case EntityType.Object:
                        Obj newObject = zoneDm.Objects.AddObject(zoneDm.Zone.Number);
                        templatesDm.ApplyClipAsTemplate(ref newObject, cboxFullCopy.Checked);
                        if (cbChangeName.Checked)
                        {
                            newObject.Cases.Imen = tbNewName.Text;
                            newObject.Cases.Dat = string.Empty;
                            newObject.Cases.Pred = string.Empty;
                            newObject.Cases.Rod = string.Empty;
                            newObject.Cases.Tvor = string.Empty;
                            newObject.Cases.Vin = string.Empty;
                        }
                        if (FirstCreatedNum == -1)
                            FirstCreatedNum = newObject.VNum;
                        break;
                }
            }
            zoneDm.Zone.LastRoomNumber = Convert.ToInt32(zoneDm.Rooms.GetLastVNum().ToString().Replace(zoneDm.Zone.Number.ToString(), "")); 
            RefreshData();
            DialogResult = DialogResult.OK;
        }

        private void NbCountTextChanged(object sender, EventArgs e)
        {
            btnCreate.Enabled = (nbCount.Value > 0 && nbCount.Value <= maxAvail && nbCount.Text != "");
        }
    }
}