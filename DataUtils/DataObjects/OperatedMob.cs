using System;

namespace DataUtils
{
    public class OperatedMob : BaseDataObject
    {
        private bool conditionFlag;
        private int followsBy = -1;
        public Guid Guid = Guid.NewGuid();
        private bool leader;
        private int maxInRoom = 1;
        //private int maxInWorld = 1;
        public readonly MobObjsCollection Items = new MobObjsCollection();
        public readonly MobObjsAfterDeathCollection ItemsAfterDeath = new MobObjsAfterDeathCollection();

        public OperatedMob(int vNum)
        {
            VNum = vNum;
            Items.Changed += FireChangeEvent;
        }

        /// <summary>
        /// Флаг условия загрузки
        /// </summary>
        public bool ConditionFlag
        {
            get => conditionFlag;
            set
            {
                if (conditionFlag == value) return;
                conditionFlag = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Максимум в комнате
        /// </summary>
        public int MaxInRoom
        {
            get => maxInRoom;
            set
            {
                if (maxInRoom == value) return;
                maxInRoom = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Признак лидерства моба
        /// </summary>
        public bool Leader
        {
            get => leader;
            set
            {
                if (leader == value) return;
                leader = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Идентификатор лидера за которым следует этот моб
        /// </summary>
        public int FollowsBy
        {
            get => followsBy;
            set
            {
                if (followsBy == value) return;
                followsBy = value;
                FireChangeEvent(this);
            }
        }

        public void AddObject(int inObjectVNum, bool inConditionFlag, int inProbability)
        {
            var obj = new MobObj(inObjectVNum)
            {
                ConditionFlag = inConditionFlag,
                ObjPos = (-1),
                Probability = inProbability
            };
            //Object.MaxInWorld = inMaxInWorld;
            obj.Changed += FireChangeEvent;
            Items.Add(obj);
        }


        public void AddObject(int inObjectVNum, bool inConditionFlag, int inObjPos, int inProbability)
        {
            var obj = new MobObj(inObjectVNum)
            {
                ConditionFlag = inConditionFlag,
                ObjPos = inObjPos,
                Probability = inProbability
            };
            //Object.MaxInWorld = inMaxInWorld;
            obj.Changed += FireChangeEvent;
            Items.Add(obj);
        }

        public void AddObjectAfterDeath(int inObjectVNum, int inProbability, int loadType, int specParam)
        {
            var obj = new MobObjAfterDeath(inObjectVNum)
            {
                Probability = inProbability,
                LoadType = loadType,
                SpecParam = specParam
            };
            //Object.MaxInWorld = inMaxInWorld;
            obj.Changed += FireChangeEvent;
            ItemsAfterDeath.Add(obj);
        }
    }
}