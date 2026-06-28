using System.Diagnostics;
using System.IO;
using System.Text;

namespace DataUtils
{
    /// <summary>
    /// Format provider for CircleMUD text file format.
    /// This is an adapter that wraps existing FileManagers without modifying them.
    /// </summary>
    public class CircleMudFormatProvider : BaseFormatProvider
    {
        public override string FormatName
        {
            get { return "circlemud"; }
        }

        public override string FormatDescription
        {
            get { return "CircleMUD Text Files (Legacy)"; }
        }

        public override Encoding DefaultEncoding
        {
            get { return Encoding.GetEncoding("koi8-r"); }
        }

        #region Load Operations

        public override bool LoadZone(Zone zone, MobsCollection mobs, RoomsCollection rooms, string zoneNumber, Encoding encoding)
        {
            ZoneFileManager zfm = new ZoneFileManager();
            zfm.ExceptionThrowed += FireExceptionEvent;
            return zfm.Load(zone, mobs, rooms, zoneNumber, encoding);
        }

        public override bool LoadRooms(RoomsCollection rooms, string zoneNumber, Encoding encoding)
        {
            WorldFileManager wfm = new WorldFileManager();
            wfm.ExceptionThrowed += FireExceptionEvent;
            return wfm.Load(rooms, zoneNumber, encoding);
        }

        public override bool LoadMobs(MobsCollection mobs, string zoneNumber, Encoding encoding)
        {
            MobsFileManager mfm = new MobsFileManager();
            mfm.ExceptionThrowed += FireExceptionEvent;
            return mfm.Load(mobs, zoneNumber, encoding);
        }

        public override bool LoadObjects(ObjsCollection objects, string zoneNumber, Encoding encoding)
        {
            ObjectsFileManager ofm = new ObjectsFileManager();
            ofm.ExceptionThrowed += FireExceptionEvent;
            return ofm.Load(objects, zoneNumber, encoding);
        }

        public override bool LoadTriggers(TriggersCollection triggers, string zoneNumber, Encoding encoding)
        {
            TriggersFileManager tfm = new TriggersFileManager();
            tfm.ExceptionThrowed += FireExceptionEvent;
            return tfm.Load(triggers, zoneNumber, encoding);
        }

        public override bool LoadSketches(SketchRoomsCollection sketches, string zoneNumber, Encoding encoding)
        {
            SketchFileManager sktfm = new SketchFileManager();
            sktfm.ExceptionThrowed += FireExceptionEvent;
            return sktfm.Load(sketches, zoneNumber, encoding);
        }

        #endregion

        #region Save Operations

        public override void SaveZone(Zone zone, ObjsCollection objects, MobsCollection mobs, RoomsCollection rooms)
        {
            ZoneFileManager zfm = new ZoneFileManager();
            zfm.Save(zone, objects, mobs, rooms);
        }

        public override void SaveRooms(RoomsCollection rooms, string zoneNumber)
        {
            WorldFileManager wfm = new WorldFileManager();
            wfm.Save(rooms, zoneNumber);
        }

        public override void SaveMobs(MobsCollection mobs, string zoneNumber)
        {
            MobsFileManager mfm = new MobsFileManager();
            mfm.Save(mobs, zoneNumber);
        }

        public override void SaveObjects(ObjsCollection objects, string zoneNumber)
        {
            ObjectsFileManager ofm = new ObjectsFileManager();
            ofm.Save(objects, zoneNumber);
        }

        public override void SaveTriggers(TriggersCollection triggers, string zoneNumber)
        {
            TriggersFileManager tfm = new TriggersFileManager();
            tfm.Save(triggers, zoneNumber);
        }

        public override void SaveSketches(SketchRoomsCollection sketches, string zoneNumber)
        {
            SketchFileManager sktfm = new SketchFileManager();
            sktfm.Save(sketches, zoneNumber);
        }

        #endregion

        #region Format Detection

        public override bool CanLoadZone(string zoneNumber)
        {
            // Check if any of the CircleMUD zone files exist
            string worldPath = StaticData.WorldFolderPath;

            // Zone file is the primary indicator
            string zonFilePath = Path.Combine(Path.Combine(worldPath, "ZON"), zoneNumber + ".zon");
            if (File.Exists(zonFilePath))
                return true;

            // Also check for .wld file (world/rooms)
            string wldFilePath = Path.Combine(Path.Combine(worldPath, "WLD"), zoneNumber + ".wld");
            if (File.Exists(wldFilePath))
                return true;

            // Check for .mob file
            string mobFilePath = Path.Combine(Path.Combine(worldPath, "MOB"), zoneNumber + ".mob");
            if (File.Exists(mobFilePath))
                return true;

            // Check for .obj file
            string objFilePath = Path.Combine(Path.Combine(worldPath, "OBJ"), zoneNumber + ".obj");
            if (File.Exists(objFilePath))
                return true;

            return false;
        }

        #endregion
    }
}
