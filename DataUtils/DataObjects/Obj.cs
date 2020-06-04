using System;

namespace DataUtils
{
    public class Obj : BaseDataObject
    {
        public BonusesCollection BonusesCollection = new BonusesCollection();
        public readonly BonusesCollection SkillBonusesCollection = new BonusesCollection();

        /// <summary>
        /// Падежи
        /// </summary>
        public Cases Cases = new Cases();

        public ExtraDescCollection ExtraDescriptions = new ExtraDescCollection();
        private string actionDesc = "";
        private string alias = "";
        private string cantTouch = "";
        private string cantUse = "";
        private int currDurab = 75;
        private string desc = "";
        private string effects = "";
        private string flags = "";
        private string magicFlags = "";
        private int material;
        private int maxDurab = 75;
        private int maxInWorld;
        private int minimumRemorts;
        private string param1 = "0";
        private string param2 = "0";
        private string param3 = "0";
        private string param4 = "0";
        private int price = 1;
        private int rentInv = 1;
        private int rentWear = 1;
        private int sex;
        private int spell = -1;
        private int spellLevel;
        private int timer = 1440;
        private int trenSkill;
        private int type = 12;
        private string wearFlags = "";
        private int weight = 1;

        public Guid Guid = Guid.NewGuid();
        public BaseDataArrayList TriggersList = new BaseDataArrayList();

        public Obj(int vNum)
        {
            VNum = vNum;
            Reactivate();
        }

        /// <summary>
        /// Альяс
        /// </summary>
        public string Alias
        {
            get => alias;
            set
            {
                if (alias == value) return;
                alias = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Описание
        /// </summary>
        public string Desc
        {
            get => desc;
            set
            {
                if (desc == value) return;
                desc = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Описание при действии
        /// </summary>
        public string ActionDesc
        {
            get => actionDesc;
            set
            {
                if (actionDesc == value) return;
                actionDesc = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Флаги для магического ингредиента
        /// </summary>
        public string MagicFlags
        {
            get => magicFlags;
            set
            {
                if (magicFlags == value) return;
                magicFlags = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Тренируемый скилл
        /// </summary>
        public int TrenSkill
        {
            get => trenSkill;
            set
            {
                if (trenSkill == value) return;
                trenSkill = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Максимальная прочность
        /// </summary>
        public int MaxDurab
        {
            get => maxDurab;
            set
            {
                if (maxDurab == value) return;
                maxDurab = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Текущая прочность
        /// </summary>
        public int CurrDurab
        {
            get => currDurab;
            set
            {
                if (currDurab == value) return;
                currDurab = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Материал
        /// </summary>
        public int Material
        {
            get => material;
            set
            {
                if (material == value) return;
                material = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Пол
        /// </summary>
        public int Sex
        {
            get => sex;
            set
            {
                if (sex == value) return;
                sex = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Максимум в мире
        /// </summary>
        public int MaxInWorld
        {
            get => maxInWorld;
            set
            {
                if (maxInWorld == value) return;
                maxInWorld = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Мнимальное количество ремортов
        /// </summary>
        public int MinimumRemorts
        {
            get => minimumRemorts;
            set
            {
                if (minimumRemorts == value) return;
                minimumRemorts = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Таймер
        /// </summary>
        public int Timer
        {
            get => timer;
            set
            {
                if (timer == value) return;
                timer = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Заклинание
        /// </summary>
        public int Spell
        {
            get => spell;
            set
            {
                if (spell == value) return;
                spell = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Уровень заклинания
        /// </summary>
        public int SpellLevel
        {
            get => spellLevel;
            set
            {
                if (spellLevel == value) return;
                spellLevel = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Эффекты
        /// </summary>
        public string ExctraEffects
        {
            get => effects;
            set
            {
                if (effects == value) return;
                effects = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        ///Флаги запрета
        /// </summary>
        public string CantTouch
        {
            get => cantTouch;
            set
            {
                if (cantTouch == value) return;
                cantTouch = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Флаги неудобства
        /// </summary>
        public string CantUse
        {
            get => cantUse;
            set
            {
                if (cantUse == value) return;
                cantUse = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Тип предмета
        /// Тип 12 по умолчанию при создании по просьбе Свентовита
        /// </summary>
        public int Type
        {
            get => type;
            set
            {
                if (type == value) return;
                type = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Экстрафлаги
        /// </summary>
        public string Affects
        {
            get => flags;
            set
            {
                if (flags == value) return;
                flags = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Флаги, куда можно одеть
        /// </summary>
        public string WearFlags
        {
            get => wearFlags;
            set
            {
                if (wearFlags == value) return;
                wearFlags = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Параметр 1
        /// </summary>
        public string Param1
        {
            get => param1;
            set
            {
                if (param1 == value) return;
                param1 = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Параметр 2
        /// </summary>
        public string Param2
        {
            get => param2;
            set
            {
                if (param2 == value) return;
                param2 = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Параметр 3
        /// </summary>
        public string Param3
        {
            get => param3;
            set
            {
                if (param3 == value) return;
                param3 = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Параметр 4
        /// </summary>
        public string Param4
        {
            get => param4;
            set
            {
                if (param4 == value) return;
                param4 = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Вес предмета
        /// </summary>
        public int Weight
        {
            get => weight;
            set
            {
                if (weight == value) return;
                weight = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Цена
        /// </summary>
        public int Price
        {
            get => price;
            set
            {
                if (price == value) return;
                price = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Цена ренты в инвентаре
        /// </summary>
        public int RentInv
        {
            get => rentInv;
            set
            {
                if (rentInv == value) return;
                rentInv = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Цена ренты когда предмет экипирован
        /// </summary>
        public int RentWear
        {
            get => rentWear;
            set
            {
                if (rentWear == value) return;
                rentWear = value;
                FireChangeEvent(this);
            }
        }

        public void Reactivate()
        {
            Cases.Changed += FireChangeEvent;
            ExtraDescriptions.Changed += FireChangeEvent;
            BonusesCollection.Changed += FireChangeEvent;
            TriggersList.Changed += FireChangeEvent;
        }

        public void AddExtraDescription(string aliases, string description)
        {
            ExtraDescriptions.Add(new ExtraDesc(aliases, description));
        }

        public void AddBonus(int bonus, int value)
        {
            BonusesCollection.Add(new Bonus(bonus, value));
        }

        public void AddSkillBonus(int bonus, int value)
        {
            SkillBonusesCollection.Add(new Bonus(bonus, value));
        }

        public void AddTrigger(int triggerVNum)
        {
            TriggersList.Add(triggerVNum);
        }

        public override string ToString()
        {
            return "[" + VNum + "] " + Cases.Imen;
        }
    }
}