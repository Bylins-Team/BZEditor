using System;

namespace DataUtils
{
    public class MobsCollection : BaseDataArrayList
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
        public Mob this[int vNum, int tmp] => GetMob(vNum);

        private Mob GetMob(int vNum)
        {
            foreach (Mob mob in this)
            {
                if (mob.VNum == vNum)
                    return mob;
            }
            return null;
        }

        public Mob GetNext(int prevNum)
        {
            int nextVNum = prevNum;
            Mob res = null;
            foreach (Mob mob in this)
            {
                int diff = mob.VNum - prevNum;
                if (diff > 0 && diff <= mob.VNum - nextVNum)
                {
                    nextVNum = mob.VNum;
                    res = mob;
                }
            }
            return res;
        }        

        public Mob GetFirst()
        {
            if (Count == 0) return null;
            Sort(new BaseDataObjectComparer());
            return (Mob)this[0];
        }

        /// <summary>
        /// Создает заданное количество новых мобов
        /// </summary>
        /// <param name="count">Требуемое количество мобов</param>
        /// <param name="zoneNum"></param>
        /// <param name="templatesDm"></param>
        /// <param name="guid"></param>
        /// <returns>Идентификатор первого из созданных мобов</returns>
        public int AddMobs(int count, int zoneNum, TemplatesDataManager templatesDm, Guid guid)
        {
            int firstId = -1;
            for (int i = 0; i < count; i++)
            {
                int vnum = GetFirstFreeVNum(zoneNum);
                if (vnum < 0) break;
                if (firstId == -1)
                    firstId = vnum;
                var mob = new Mob(vnum) { Cases = { Imen = ("Новый моб " + vnum) } };
                if (guid != Guid.Empty)
                    templatesDm.ApplyTemplate(ref mob, guid);
                Add(mob);
            }
            Sort(new BaseDataObjectComparer());
            return firstId;
        }

        public Mob AddMob(int zoneNum)
        {
            int vnum = GetFirstFreeVNum(zoneNum);
            if (vnum < 0) return null;

            var mob = new Mob(vnum);
            Add(mob);
            Sort(new BaseDataObjectComparer());
            return mob;
        }
    }
}