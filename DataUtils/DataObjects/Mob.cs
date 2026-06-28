using System;

namespace DataUtils
{
    public class Mob : BaseDataObject
    {
        /// <summary>
        /// Падежи
        /// </summary>
        public Cases Cases = new Cases();

        /// <summary>
        /// Путь моба
        /// </summary>
        public readonly BaseDataArrayList Destination = new BaseDataArrayList();

        private int absorbe;
        private int ac;
        private string affects = "";
        private string alias = "";
        private int align;
        private int aResist;
        private int armour;
        private int bareHandAttack;
        private int castSuccess;
        private int mobClass = 100;
        private string damages = "0d0+0";
        private string desc = "";
        private string detailDescr = "";
        public BaseDataArrayList Feats = new BaseDataArrayList();
        private int exp;
        private int extraAttack;
        private string flags = "";
        private int hitroll;
        private string hits = "0d0+0";
        private int hPreg;
        private int immunitet;
        private int initiative;
        private int level;
        private int likeWork;
        private int maxFactork;
        private int maxInWorld = -1;
        private int mind;
        private string money = "0d0+0";
        private int mResist;
        private int plusMem;
        private int posDefault = 8; //моб стоит
        private int posLoad = 8; //моб стоит
        private int speed = -1; // -1 = default movement cadence
        private int pResist;
        private int race = 100;
        private int resistDark;
        private int resistFromAir;
        private int resistFromEarthr;
        private int resistFromFire;
        private int resistFromWater;
        private int saveFightSkills;
        private int saveMagBreathes;
        private int saveMagDamages;
        private int saveParalyzeCast;
        private int sex = 1;
        private string specialBitvector = "";
        private int luck;
        private int vitality;

        /// <summary>
        /// Глобальный идентификатор (внутр.)
        /// </summary>
        public Guid Guid = Guid.NewGuid();

        public readonly BaseDataArrayList Helpers = new BaseDataArrayList();

        /// <summary>
        /// Список предметов, загружаемых после смерти моба
        /// </summary>
        public readonly LoadedObjAfterDeathCollection LoadedObjectAfterDeath = new LoadedObjAfterDeathCollection();

        public MobSkillsCollection Skills = new MobSkillsCollection();

        /// <summary>
        /// Коллекчия заклинаний моба
        /// </summary>
        public MobSpellsCollection Spells = new MobSpellsCollection();

        /// <summary>
        /// Параметры моба
        /// </summary>
        public MobStats Stats = new MobStats();

        public BaseDataArrayList TriggersList = new BaseDataArrayList();

        /// <summary>
        /// Роли моба
        /// </summary>
        public BaseDataArrayList Roles = new BaseDataArrayList();

        /// <summary>
        /// Ингредиенты
        /// </summary>
        public IngredientsCollection Ingredients = new IngredientsCollection();

        public Mob(int vNum)
        {
            VNum = vNum;
            Reactivate();
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
        /// Альясы
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
        /// Детальное описание моба
        /// </summary>
        public string DetailDescr
        {
            get => detailDescr;
            set
            {
                if (detailDescr == value) return;
                detailDescr = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Флаги моба
        /// </summary>
        public string Flags
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
        /// Аффекты моба
        /// </summary>
        public string Affects
        {
            get => affects;
            set
            {
                if (affects == value) return;
                affects = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Наклонность
        /// </summary>
        public int Align
        {
            get => align;
            set
            {
                if (align == value) return;
                align = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Уровень
        /// </summary>
        public int Level
        {
            get => level;
            set
            {
                if (level == value) return;
                level = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Хитролл
        /// </summary>
        public int Hitroll
        {
            get => hitroll;
            set
            {
                if (hitroll == value) return;
                hitroll = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// АС
        /// </summary>
        public int Ac
        {
            get => ac;
            set
            {
                if (ac == value) return;
                ac = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Хиты
        /// </summary>
        public string Hits
        {
            get => hits;
            set
            {
                if (hits == value) return;
                hits = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Повреждения
        /// </summary>
        public string Damage
        {
            get => damages;
            set
            {
                if (damages == value) return;
                damages = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Деньги
        /// </summary>
        public string Money
        {
            get => money;
            set
            {
                if (money == value) return;
                money = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Опыт
        /// </summary>
        public int Exp
        {
            get => exp;
            set
            {
                if (exp == value) return;
                exp = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Позиция при загрузке
        /// </summary>
        public int PosLoad
        {
            get => posLoad;
            set
            {
                if (posLoad == value) return;
                posLoad = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Позиция по умолчанию
        /// </summary>
        public int PosDefault
        {
            get => posDefault;
            set
            {
                if (posDefault == value) return;
                posDefault = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Movement speed (4th position-line field); -1 = default cadence
        /// </summary>
        public int Speed
        {
            get => speed;
            set
            {
                if (speed == value) return;
                speed = value;
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
        /// Воля
        /// </summary>
        public int SaveParalyzeCast
        {
            get => saveParalyzeCast;
            set
            {
                if (saveParalyzeCast == value) return;
                saveParalyzeCast = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Здоровье
        /// </summary>
        public int SaveMagBreathes
        {
            get => saveMagBreathes;
            set
            {
                if (saveMagBreathes == value) return;
                saveMagBreathes = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Стойкость
        /// </summary>
        public int SaveMagDamages
        {
            get => saveMagDamages;
            set
            {
                if (saveMagDamages == value) return;
                saveMagDamages = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Реакция
        /// </summary>
        public int SaveFightSkills
        {
            get => saveFightSkills;
            set
            {
                if (saveFightSkills == value) return;
                saveFightSkills = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Защита от огня
        /// </summary>
        public int ResistFromFire
        {
            get => resistFromFire;
            set
            {
                if (resistFromFire == value) return;
                resistFromFire = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Защита от воздуха
        /// </summary>
        public int ResistFromAir
        {
            get => resistFromAir;
            set
            {
                if (resistFromAir == value) return;
                resistFromAir = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Защита от воды
        /// </summary>
        public int ResistFromWater
        {
            get => resistFromWater;
            set
            {
                if (resistFromWater == value) return;
                resistFromWater = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Защита от земли
        /// </summary>
        public int ResistFromEarth
        {
            get => resistFromEarthr;
            set
            {
                if (resistFromEarthr == value) return;
                resistFromEarthr = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Живучесть
        /// </summary>
        public int Vitality
        {
            get => vitality;
            set
            {
                if (vitality == value) return;
                vitality = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Разум
        /// </summary>
        public int Mind
        {
            get => mind;
            set
            {
                if (mind == value) return;
                mind = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Иммунитет
        /// </summary>
        public int Immunitet
        {
            get => immunitet;
            set
            {
                if (immunitet == value) return;
                immunitet = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Иммунитет
        /// </summary>
        public int ResistDark
        {
            get => resistDark;
            set
            {
                if (resistDark == value) return;
                resistDark = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Ругенерация хитов
        /// </summary>
        public int HPreg
        {
            get => hPreg;
            set
            {
                if (hPreg == value) return;
                hPreg = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Броня
        /// </summary>
        public int Armour
        {
            get => armour;
            set
            {
                if (armour == value) return;
                armour = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Запоминание
        /// </summary>
        public int PlusMem
        {
            get => plusMem;
            set
            {
                if (plusMem == value) return;
                plusMem = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Успех колдовства
        /// </summary>
        public int CastSuccess
        {
            get => castSuccess;
            set
            {
                if (castSuccess == value) return;
                castSuccess = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Удача
        /// </summary>
        public int Luck
        {
            get => luck;
            set
            {
                if (luck == value) return;
                luck = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Инициатива
        /// </summary>
        public int Initiative
        {
            get => initiative;
            set
            {
                if (initiative == value) return;
                initiative = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Поглощение
        /// </summary>
        public int Absorbe
        {
            get => absorbe;
            set
            {
                if (absorbe == value) return;
                absorbe = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Иммунитет к магическим аффектам
        /// </summary>
        public int AResist
        {
            get => aResist;
            set
            {
                if (aResist == value) return;
                aResist = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Иммунитет к магическим повреждениям
        /// </summary>
        public int MResist
        {
            get => mResist;
            set
            {
                if (mResist == value) return;
                mResist = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Иммунитет к физическим повреждениям
        /// </summary>
        public int PResist
        {
            get => pResist;
            set
            {
                if (pResist == value) return;
                pResist = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Тип атаки (кажется)
        /// </summary>
        public int BareHandAttack
        {
            get => bareHandAttack;
            set
            {
                if (bareHandAttack == value) return;
                bareHandAttack = value;
                FireChangeEvent(this);
            }
        }

        public int LikeWork
        {
            get => likeWork;
            set
            {
                if (likeWork == value) return;
                likeWork = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Скорость замакса
        /// </summary>
        public int MaxFactor
        {
            get => maxFactork;
            set
            {
                if (maxFactork == value) return;
                maxFactork = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Количество доп.атак
        /// </summary>
        public int ExtraAttack
        {
            get => extraAttack;
            set
            {
                if (extraAttack == value) return;
                extraAttack = value;
                FireChangeEvent(this);
            }
        }

        private int mobRemort;
        // Mob remort level (enhanced E-spec). Preserved through YAML save.
        public int MobRemort
        {
            get => mobRemort;
            set
            {
                if (mobRemort == value) return;
                mobRemort = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Класс моба
        /// </summary>
        public int Class
        {
            get => mobClass;
            set
            {
                if (mobClass == value) return;
                mobClass = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Тип моба
        /// </summary>
        public int Race
        {
            get => race;
            set
            {
                if (race == value) return;
                race = value;
                FireChangeEvent(this);
            }
        }

        public string SpecialBitvector
        {
            get => specialBitvector;
            set
            {
                if (specialBitvector == value) return;
                specialBitvector = value;
                FireChangeEvent(this);
            }
        }

        public void Reactivate()
        {
            Cases.Changed += FireChangeEvent;
            Stats.Changed += FireChangeEvent;
            Destination.Changed += FireChangeEvent;
            Spells.Changed += FireChangeEvent;
            LoadedObjectAfterDeath.Changed += FireChangeEvent;
            Skills.Changed += FireChangeEvent;
            Helpers.Changed += FireChangeEvent;
            Feats.Changed += FireChangeEvent;
            TriggersList.Changed += FireChangeEvent;
            Ingredients.Changed += FireChangeEvent;
        }

        public override string ToString()
        {
            return "[" + VNum + "] " + Cases.Imen;
        }
    }
}