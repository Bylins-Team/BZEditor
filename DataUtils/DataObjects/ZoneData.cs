using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace DataUtils
{
    /// <summary>
    /// Состояния зоны
    /// </summary>
    public enum ZoneState
    {
        Loaded = 0,
        Opened = 2,
        Changed = 3,
        NotFound = 4,
        Available = 5,
    }

    [Serializable]
    public class ZoneData : BaseDataObject, ISerializable
    {
        private string name = "";
        private string fileName = "";
        private ZoneState state = ZoneState.NotFound;
        public bool Preloading;

        public ZoneData()
        {
        }

        public ZoneData(string fileName, string name)
            : this(fileName, name, ZoneState.NotFound)
        {
        }

        public ZoneData(string fileName, string name, ZoneState state)
        {
            this.fileName = fileName;
            this.name = name;
            this.state = state;
        }

        protected ZoneData(SerializationInfo info, StreamingContext context)
        {
            fileName = info.GetValue("number", typeof(string)).ToString();
            name = info.GetValue("name", typeof (string)).ToString();
            state = (ZoneState) (info.GetValue("CZonesData", typeof (int)));
        }

        /// <summary>
        /// Имя файла
        /// </summary>
        public string FileName
        {
            get => fileName;
            set
            {
                if (fileName == value) return;
                fileName = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Название зоны
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
        /// Состояние зоны
        /// </summary>
        public ZoneState State
        {
            get => state;
            set
            {
                if (state == value) return;
                state = value;
                FireChangeEvent(this);
            }
        }

        #region ISerializable Members

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("number", fileName);
            info.AddValue("name", name);
            info.AddValue("state", (int) state, typeof (int));
            info.AddValue("preload", Preloading, typeof(bool));
        }

        #endregion

        public static ZoneData NewCZoneData()
        {
            return new ZoneData();
        }
    }
}