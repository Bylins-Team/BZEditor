using System;
using System.Windows.Forms;
using DataUtils;
using Object = DataUtils.Obj;

namespace BZEditor
{
    public partial class WldForm
    {
        private void AddTemplate()
        {
            switch (tcMain.SelectedTab.Name)
            {
                case "tpRooms":
                    if (lvMainList.SelectedItems.Count > 0)
                    {
                        //CObject Object = ZoneDM.ObjectsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
                        //CtreateTemplateForm ctf = new CtreateTemplateForm("На основе объекта [" + Object.VNum.ToString() + "] " + Object.Cases.Imen);
                        //DialogResult dres = ctf.ShowDialog();
                        //if (dres == DialogResult.OK)
                        //    TemplatesDM.AddTemplate(Object, ctf.TemplateName);
                        //ctf.Dispose();
                    }
                    break;
                case "tpObjects":
                    if (lvMainList.SelectedItems.Count > 0)
                    {
                        Obj obj = ZoneDM.ObjectsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
                        CtreateTemplateForm ctf =
                            new CtreateTemplateForm(string.Format("На основе объекта [{0}] {1}", obj.VNum, obj.Cases.Imen));
                        DialogResult dres = ctf.ShowDialog();
                        if (dres == DialogResult.OK)
                            _templatesDM.AddTemplate(obj, ctf.TemplateName);
                        ctf.Dispose();
                    }
                    break;
                case "tpMobs":
                    if (lvMainList.SelectedItems.Count > 0)
                    {
                        Mob mob = ZoneDM.MobsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
                        CtreateTemplateForm ctf =
                            new CtreateTemplateForm(string.Format("На основе моба [{0}] {1}", mob.VNum, mob.Cases.Imen));
                        DialogResult dres = ctf.ShowDialog();
                        if (dres == DialogResult.OK)
                            _templatesDM.AddTemplate(mob, ctf.TemplateName);
                        ctf.Dispose();
                    }
                    break;
            }
        }

        private void CreateClones()
        {
            DoCopy();
            CreateClonesForm ccf = null;
            bool isRoom = false;
            switch (tcMain.SelectedTab.Name)
            {
                case "tpRooms":
                    if (SelectedRooms.Count > 0)
                        ccf = new CreateClonesForm(ref ZoneDM, ref _templatesDM, EntityType.Room);
                    isRoom = true;
                    break;
                case "tpObjects":
                    if (lvMainList.SelectedItems.Count > 0)
                        ccf = new CreateClonesForm(ref ZoneDM, ref _templatesDM, EntityType.Object);
                    break;
                case "tpMobs":
                    if (lvMainList.SelectedItems.Count > 0)
                        ccf = new CreateClonesForm(ref ZoneDM, ref _templatesDM, EntityType.Mob);
                    break;
            }
            if (ccf == null) return;
            if (ccf.ShowDialog() == DialogResult.OK)
            {
                RefreshMainList(false);
                SelectMainListItem(ccf.FirstCreatedNum, isRoom);
            }
        }

        private void DoCopy()
        {
            switch (tcMain.SelectedTab.Name)
            {
                case "tpRooms":
                    if (SelectedRooms.Count > 0)
                    {
                        Room room = (Room)(SelectedRooms[0]);
                        /*if (lvMainList.SelectedItems.Count != 0) 
                            Room = ZoneDM.RoomsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
                        else
                            Room = ZoneDM.RoomsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];*/
                        _templatesDM.AddToClipboard(room);
                    }
                    break;
                case "tpObjects":
                    if (lvMainList.SelectedItems.Count > 0)
                    {
                        Obj obj = ZoneDM.ObjectsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
                        _templatesDM.AddToClipboard(obj);
                    }
                    break;
                case "tpMobs":
                    if (lvMainList.SelectedItems.Count > 0)
                    {
                        Mob mob = ZoneDM.MobsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
                        _templatesDM.AddToClipboard(mob);
                    }
                    break;
                case "tpShops":
                    if (lvMainList.SelectedItems.Count > 0)
                    {
                        Shop shp = ZoneDM.ShopsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
                        _templatesDM.AddToClipboard(shp);
                    }
                    break;
               case "tpTriggers":
                    if (lvMainList.SelectedItems.Count > 0)
                    {
                        Trigger trg = ZoneDM.TriggersCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
                        _templatesDM.AddToClipboard(trg);
                    }
                    break;
            }
        }

        private void DoPaste()
        {
            CreateClonesForm ccf = new CreateClonesForm();
            switch (tcMain.SelectedTab.Name)
            {
                case "tpRooms":
                    ccf = new CreateClonesForm(ref ZoneDM, ref _templatesDM, EntityType.Room);
                    break;
                case "tpObjects":
                    ccf = new CreateClonesForm(ref ZoneDM, ref _templatesDM, EntityType.Object);
                    break;
                case "tpMobs":
                    ccf = new CreateClonesForm(ref ZoneDM, ref _templatesDM, EntityType.Mob);
                    break;
                case "tpShops":
                    Shop newShop = ZoneDM.ShopsCollection[ZoneDM.ShopsCollection.AddShop(ZoneDM.Zone.Number), 0];
                    _templatesDM.ApplyClipAsTemplate(ref newShop);
                    RefreshMainList();
                    return;
                case "tpTriggers":
                    Trigger newTrigger = ZoneDM.TriggersCollection[ZoneDM.TriggersCollection.AddTrigger(ZoneDM.Zone.Number), 0];
                    _templatesDM.ApplyClipAsTemplate(ref newTrigger);
                    RefreshMainList();
                    return;
            }
            if (ccf.ShowDialog() == DialogResult.OK)
                RefreshMainList();
        }

        private void PastAsTemplate()
        {
            switch (tcMain.SelectedTab.Name)
            {
                case "tpRooms":
                    break;
                case "tpObjects":
                    if (lvMainList.SelectedItems.Count > 0)
                    {
                        Obj obj = ZoneDM.ObjectsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
                        if (obj != null)
                            _templatesDM.ApplyClipAsTemplate(ref obj, false);
                        RefreshObjectData();
                    }
                    break;
                case "tpMobs":
                    if (lvMainList.SelectedItems.Count > 0)
                    {
                        if (lvMainList.SelectedItems.Count > 0)
                        {
                            Mob mob = ZoneDM.MobsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
                            if (mob != null)
                                _templatesDM.ApplyClipAsTemplate(ref mob, false);
                            RefreshMobData();
                        }
                    }
                    break;
            }
        }

        public void ApplyTemplate(Guid guid, TemplatesDataManager.TemplatesType type)
        {
            switch (type)
            {
                case TemplatesDataManager.TemplatesType.Object:
                    if (lvMainList.SelectedItems.Count > 0 && tcMain.SelectedTab.Name == "tpObjects")
                    {
                        Obj obj = ZoneDM.ObjectsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
                        if (obj != null)
                            _templatesDM.ApplyTemplate(ref obj, guid);
                        RefreshObjectData();
                    }
                    break;
                case TemplatesDataManager.TemplatesType.Mob:
                    Mob mob = ZoneDM.MobsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
                    if (lvMainList.SelectedItems.Count > 0 && tcMain.SelectedTab.Name == "tpMobs")
                    {
                        if (mob != null)
                            _templatesDM.ApplyTemplate(ref mob, guid);
                        RefreshMobData();
                    }
                    break;
            }
        }

        private void TsmiInfoClick(object sender, EventArgs e)
        {
            switch (tcMain.SelectedTab.Name)
            {
                case "tpRooms":
                    if (SelectedRooms.Count == 1)
                    {
                    }
                    if (SelectedRooms.Count > 1)
                    {
                    }
                    break;
                case "tpObjects":
                    if (lvMainList.SelectedItems.Count > 0)
                    {
                        Obj obj = ZoneDM.ObjectsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
                        if (obj != null)
                            RefreshDetailsAndLocations(obj);
                        tcListAndInfo.SelectedIndex = 1;
                    }
                    break;
                case "tpMobs":
                    Mob mob = ZoneDM.MobsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
                    if (lvMainList.SelectedItems.Count > 0)
                    {
                        if (mob != null)
                            RefreshDetailsAndLocations(mob);
                        tcListAndInfo.SelectedIndex = 1;
                    }
                    break;
            }
        }
    }
}