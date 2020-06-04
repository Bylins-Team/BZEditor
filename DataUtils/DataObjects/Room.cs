using System.Drawing;

namespace DataUtils
{
    public class Room : BaseDataObject
    {
        /// <summary>
        /// Описание комнаты
        /// </summary>
        public RoomDescription Description = new RoomDescription();

        public ExitColors ExitColors = new ExitColors();
        public Exit ExitDown = new Exit();
        public Exit ExitEast = new Exit();
        public Exit ExitNorth = new Exit();
        public Exit ExitSouth = new Exit();
        public Exit ExitUp = new Exit();
        public Exit ExitWest = new Exit();
        public ExtraDescCollection ExtraDescriptions = new ExtraDescCollection();
        private string flags = "";
        private string name = "";
        private bool placedOnMap;
        private int sectorType;

        private int x;
        private int y;
        private int z;
        private int zoneNum = -1;
        public Bitmap Img;
        public OperatedMobsCollection LoadingMobsCollection = new OperatedMobsCollection();
        public OperatedObjCollection LoadingObjectsCollection = new OperatedObjCollection();
        public OperatedObjCollection RemoovingObjects = new OperatedObjCollection();
        public BaseDataArrayList TriggersList = new BaseDataArrayList();

        /// <summary>
        /// Ингредиенты
        /// </summary>
        public IngredientsCollection Ingredients = new IngredientsCollection();

        public Room(int vNum)
        {
            VNum = vNum;
            Reactivate();
        }

        /// <summary>
        /// Координата X
        /// </summary>
        public int X
        {
            get => x;
            set
            {
                if (value == x) return;
                x = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Координата Y
        /// </summary>
        public int Y
        {
            get => y;
            set
            {
                if (value == y) return;
                y = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Координата Z
        /// </summary>
        public int Z
        {
            get => z;
            set
            {
                if (value == z) return;
                z = value;
                FireChangeEvent(this);
            }
        }

        public Point3D Location
        {
            get => new Point3D(x, y, z);
            set
            {
                if (x == value.X && y == value.Y && z == value.Z) return;
                x = value.X;
                y = value.Y;
                z = value.Z;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Признак того, что комната размещена на карте
        /// </summary>
        public bool PlacedOnMap
        {
            get => placedOnMap;
            set
            {
                if (placedOnMap == value) return;
                placedOnMap = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Название комнаты
        /// </summary>
        public string Name
        {
            get => name;
            set
            {
                if (name == value) return;
                name = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Номер зоны в которой находится комната
        /// </summary>
        public int ZoneNum
        {
            get => zoneNum;
            set
            {
                if (zoneNum == value) return;
                zoneNum = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Флаги комнаты
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
        /// Тип сектора
        /// </summary>
        public int SectorType
        {
            get => sectorType;
            set
            {
                if (sectorType == value) return;
                sectorType = value;
                FireChangeEvent(this);
            }
        }

        public void Reactivate()
        {
            Description.Changed += FireChangeEvent;
            ExitNorth.Changed += FireChangeEvent;
            ExitEast.Changed += FireChangeEvent;
            ExitSouth.Changed += FireChangeEvent;
            ExitWest.Changed += FireChangeEvent;
            ExitUp.Changed += FireChangeEvent;
            ExitDown.Changed += FireChangeEvent;
            ExtraDescriptions.Changed += FireChangeEvent;
            TriggersList.Changed += FireChangeEvent;
            LoadingMobsCollection.Changed += FireChangeEvent;
            LoadingObjectsCollection.Changed += FireChangeEvent;
            RemoovingObjects.Changed += FireChangeEvent;
            ExitColors.Changed += FireChangeEvent;
            Ingredients.Changed += FireChangeEvent;
        }

        public void AddExtraDescription(string aliases, string description)
        {
            ExtraDescriptions.Add(new ExtraDesc(aliases, description));
        }

        public override string ToString()
        {
            return string.Format("[{0}] {1}", VNum, name);
        }
    }
}