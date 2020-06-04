using System;

namespace DataUtils
{
    public class ObjsCollection : BaseDataArrayList
    {
        #region Delegates

        public new delegate void ChangeEvent(object changedClass, object sender);

        #endregion

        public new event ChangeEvent Changed;

        public override void FireChangeEvent(object sender)
        {
            if (Changed != null && StaticData.CanFireChangeEvent)
                Changed(this, sender);
        }

        /// <summary>
        /// Возвращает ссылку
        /// </summary>
        public Obj this[int vNum, int tmp] => GetObject(vNum);

        public Obj GetObject(int vNum)
        {
            foreach (Obj obj in this)
            {
                if (obj.VNum == vNum)
                    return obj;
            }
            return null;
        }

        /// <summary>
        /// Создает заданное количество новых предметов
        /// </summary>
        /// <param name="count">Требуемое количество предметов</param>
        /// <param name="zoneNum"></param>
        /// <param name="templatesDm"></param>
        /// <param name="guid"></param>
        /// <returns>Количество созданных предметов</returns>
        public int AddObjects(int count, int zoneNum, TemplatesDataManager templatesDm, Guid guid)
        {
            int firstId = -1;
            for (int i = 0; i < count; i++)
            {
                int vnum = GetFirstFreeVNum(zoneNum);
                if (vnum < 0) break;
                if (firstId == -1)
                    firstId = vnum;
                var obj = new Obj(vnum) { Cases = { Imen = "Новый объект " + vnum } };
                if (guid != Guid.Empty)
                    templatesDm.ApplyTemplate(ref obj, guid);
                Add(obj);
            }
            Sort(new BaseDataObjectComparer());
            return firstId;
        }

        public Obj AddObject(int zoneNum)
        {
            int vnum = GetFirstFreeVNum(zoneNum);
            if (vnum < 0) return null;

            var obj = new Obj(vnum);
            Add(obj);
            Sort(new BaseDataObjectComparer());
            return obj;
        }
    }
}