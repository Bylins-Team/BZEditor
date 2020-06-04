using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace DataUtils
{
    public class GlobalSketch : BaseDataArrayList
    {
        #region Delegates & Events

        public new delegate void ChangeEvent(GlobalSketch sender);
        public new event ChangeEvent Changed;

        public delegate void SaveEvent(GlobalSketch sender);
        public event SaveEvent Saved;

        #endregion

        public string Name { get; set; }

        private string fileName;
        public string FileName
        {
            get => fileName;
            set
            {            
                foreach (char c in Path.GetInvalidFileNameChars())
                    value = value.Replace(c, '_');
                fileName = value;
            }
        }

        public override void FireChangeEvent(object sender)
        {
            if (Changed != null && StaticData.CanFireChangeEvent)
                Changed(this);
        }

        public void AddSketchRoom(int x, int y, int z, int zoneNum)
        {
            Add(new GlobalSketchRoom(x, y, z, zoneNum));
            FireChangeEvent(this);
        }

        public GlobalSketchRoom GetCketchRoom(int num)
        {
            if (num <= Count) return null;
            return (GlobalSketchRoom)(this[num]);
        }

        public GlobalSketchRoom GetSketchRoom(int x, int y, int z)
        {
            foreach (GlobalSketchRoom sr in this)
            {
                if (sr.X == x && sr.Y == y && sr.Z == z)
                    return sr;
            }
            return null;
        }

        public int GetRoomsCount(int zoneNum)
        {
            int cntr = 0;
            foreach (GlobalSketchRoom sr in this)
            {
                if (sr.ZoneNum == zoneNum)
                    cntr++;
            }
            return cntr;
        }

        public void RemoveSketchRoom(int x, int y, int z)
        {
            GlobalSketchRoom srtoremove = null;
            foreach (GlobalSketchRoom sr in this)
            {
                if (sr.X == x && sr.Y == y && sr.Z == z)
                    srtoremove = sr;
            }
            if (srtoremove != null)
            {
                Remove(srtoremove);
                FireChangeEvent(this);
            }
        }

        public readonly List<int> ZonesNumbers = new List<int>();
        public readonly Dictionary<int, string> ZonesNames = new Dictionary<int, string>();
        public readonly Dictionary<int, Color> ZonesColors = new Dictionary<int, Color>();

        public void AddZone(int zoneNum, string name, Color color)
        {
            ZonesNumbers.Add(zoneNum);
            ZonesNames.Add(zoneNum, name);
            ZonesColors.Add(zoneNum, color);
            FireChangeEvent(this);
        }

        public void RemoveZone(int zoneNum)
        {
            ZonesNumbers.Remove(zoneNum);
            ZonesNames.Remove(zoneNum);
            ZonesColors.Remove(zoneNum);
            List<GlobalSketchRoom> toDelete = new List<GlobalSketchRoom>(Count);
            foreach (GlobalSketchRoom room in this)
                if (room.ZoneNum == zoneNum)
                    toDelete.Add(room);
            foreach (GlobalSketchRoom delRoom in toDelete)
                Remove(delRoom);
            if (toDelete.Count > 0)
                FireChangeEvent(this);
        }

        public Color GenerateRandomColor()
        {
            Random rand = new Random();
            Color color = Color.FromArgb(rand.Next(100, 255), rand.Next(100, 255), rand.Next(100, 255));
            while (!ColorExist(color))
            {
                color = Color.FromArgb(rand.Next(100, 255), rand.Next(100, 255), rand.Next(100, 255));
            }
            return color;
        }

        public bool ColorExist(Color color)
        {
            foreach (KeyValuePair<int, Color> zoneColor in ZonesColors)
            {
                if (zoneColor.Value == color)
                    return false;
            }
            return true;
        }

        public Point3D GetPointOfCenterRoomPlacedOnMap()
        {
            GlobalSketchRoom trgRoom = null;
            foreach (GlobalSketchRoom room in this)
                if (trgRoom == null || (Math.Abs(room.X) + Math.Abs(room.Y) + Math.Abs(room.Z) < Math.Abs(trgRoom.X) + Math.Abs(trgRoom.Y) + Math.Abs(trgRoom.Z)))
                    trgRoom = room;
            return (trgRoom != null) ? trgRoom.Location : new Point3D(0, 0, 0);
        }

        public bool LoadData()
        {
            if (string.IsNullOrEmpty(Name)) return false;
            GlobalSketchFileManager fm = new GlobalSketchFileManager();
            return fm.Load(this);
        }

        public bool SaveData()
        {
            if (string.IsNullOrEmpty(Name)) return false;
            GlobalSketchFileManager fm = new GlobalSketchFileManager();
            Saved?.Invoke(this);
            return fm.Save(this);            
        }

        public void UpdateZone(int num, int newNum, string newName, Color newSketchColor)
        {
            foreach (GlobalSketchRoom room in this)
                if (room.ZoneNum == num)
                {
                    room.ZoneNum = newNum;
                }
            ZonesNumbers.Remove(num);
            ZonesNames.Remove(num);
            ZonesColors.Remove(num);

            ZonesNumbers.Add(newNum);
            ZonesNames.Add(newNum, newName);
            ZonesColors.Add(newNum, newSketchColor);
        }
    }
}