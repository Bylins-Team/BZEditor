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
        /// max_in_world of this very reset command as read from the world (-1 = not loaded, e.g. a
        /// spawn added in the UI). The engine checks it per command (total_online &lt; arg2), so a zone
        /// may give one mob different limits in different rooms; the save keeps it, clamped to
        /// Mob.MaxInWorld, instead of overwriting every command with the zone-wide maximum.
        /// </summary>
        public int CommandMaxInWorld { get; set; } = -1;

        /// <summary>
        /// The reset command's if_flag as read (-1 = not loaded). Bit 1 (CHECK_SUCCESS) is what
        /// ConditionFlag edits; bit 2 (FLAG_PERSIST: "don't change the success state") has no UI
        /// and is carried through here so a save does not turn 3 into 1 or 2 into 0.
        /// </summary>
        public int IfFlagRaw { get; set; } = -1;

        /// <summary>if_flag of the FOLLOW command that made this mob follow (-1 = not loaded).</summary>
        public int FollowIfFlagRaw { get; set; } = -1;

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