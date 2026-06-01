using System;
using System.Diagnostics;
using System.Text;

namespace DataUtils
{
    /// <summary>
    /// Interface for zone data format providers.
    /// Allows loading and saving zone data in different formats (CircleMUD text, YAML, SQLite, etc.)
    /// </summary>
    public interface IFormatProvider
    {
        /// <summary>
        /// Short name of the format (e.g., "circlemud", "yaml", "sqlite")
        /// </summary>
        string FormatName { get; }

        /// <summary>
        /// Human-readable description of the format
        /// </summary>
        string FormatDescription { get; }

        /// <summary>
        /// Default encoding for this format
        /// </summary>
        Encoding DefaultEncoding { get; }

        /// <summary>
        /// Event fired when an exception occurs during loading or saving
        /// </summary>
        event BaseFileManager.ExceptionEvent ExceptionThrowed;

        #region Load Operations

        /// <summary>
        /// Load zone metadata and zone commands
        /// </summary>
        bool LoadZone(Zone zone, MobsCollection mobs, RoomsCollection rooms, string zoneNumber, Encoding encoding);

        /// <summary>
        /// Load rooms (world data)
        /// </summary>
        bool LoadRooms(RoomsCollection rooms, string zoneNumber, Encoding encoding);

        /// <summary>
        /// Load mobs (mobiles/NPCs)
        /// </summary>
        bool LoadMobs(MobsCollection mobs, string zoneNumber, Encoding encoding);

        /// <summary>
        /// Load objects
        /// </summary>
        bool LoadObjects(ObjsCollection objects, string zoneNumber, Encoding encoding);

        /// <summary>
        /// Load triggers (DG Scripts)
        /// </summary>
        bool LoadTriggers(TriggersCollection triggers, string zoneNumber, Encoding encoding);

        /// <summary>
        /// Load sketch rooms (map editor data)
        /// </summary>
        bool LoadSketches(SketchRoomsCollection sketches, string zoneNumber, Encoding encoding);

        #endregion

        #region Save Operations

        /// <summary>
        /// Save zone metadata and zone commands
        /// </summary>
        void SaveZone(Zone zone, ObjsCollection objects, MobsCollection mobs, RoomsCollection rooms);

        /// <summary>
        /// Save rooms (world data)
        /// </summary>
        void SaveRooms(RoomsCollection rooms, string zoneNumber);

        /// <summary>
        /// Save mobs (mobiles/NPCs)
        /// </summary>
        void SaveMobs(MobsCollection mobs, string zoneNumber);

        /// <summary>
        /// Save objects
        /// </summary>
        void SaveObjects(ObjsCollection objects, string zoneNumber);

        /// <summary>
        /// Save triggers (DG Scripts)
        /// </summary>
        void SaveTriggers(TriggersCollection triggers, string zoneNumber);

        /// <summary>
        /// Save sketch rooms (map editor data)
        /// </summary>
        void SaveSketches(SketchRoomsCollection sketches, string zoneNumber);

        #endregion

        #region Format Detection

        /// <summary>
        /// Check if this provider can load the specified zone
        /// </summary>
        bool CanLoadZone(string zoneNumber);

        #endregion
    }
}
