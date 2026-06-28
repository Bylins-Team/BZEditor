using System;
using System.Diagnostics;
using System.Text;

namespace DataUtils
{
    /// <summary>
    /// Base class for format providers with common event handling logic
    /// </summary>
    public abstract class BaseFormatProvider : IFormatProvider
    {
        public abstract string FormatName { get; }
        public abstract string FormatDescription { get; }
        public abstract Encoding DefaultEncoding { get; }

        public event BaseFileManager.ExceptionEvent ExceptionThrowed;

        /// <summary>
        /// Fire the ExceptionThrowed event
        /// </summary>
        protected virtual void FireExceptionEvent(string message, Exception exception, EventLogEntryType type)
        {
            if (ExceptionThrowed != null)
                ExceptionThrowed(message, exception, type);
        }

        #region Abstract Load Operations

        public abstract bool LoadZone(Zone zone, MobsCollection mobs, RoomsCollection rooms, string zoneNumber, Encoding encoding);
        public abstract bool LoadRooms(RoomsCollection rooms, string zoneNumber, Encoding encoding);
        public abstract bool LoadMobs(MobsCollection mobs, string zoneNumber, Encoding encoding);
        public abstract bool LoadObjects(ObjsCollection objects, string zoneNumber, Encoding encoding);
        public abstract bool LoadTriggers(TriggersCollection triggers, string zoneNumber, Encoding encoding);
        public abstract bool LoadSketches(SketchRoomsCollection sketches, string zoneNumber, Encoding encoding);

        #endregion

        #region Abstract Save Operations

        public abstract void SaveZone(Zone zone, ObjsCollection objects, MobsCollection mobs, RoomsCollection rooms);
        public abstract void SaveRooms(RoomsCollection rooms, string zoneNumber);
        public abstract void SaveMobs(MobsCollection mobs, string zoneNumber);
        public abstract void SaveObjects(ObjsCollection objects, string zoneNumber);
        public abstract void SaveTriggers(TriggersCollection triggers, string zoneNumber);
        public abstract void SaveSketches(SketchRoomsCollection sketches, string zoneNumber);

        #endregion

        #region Abstract Format Detection

        public abstract bool CanLoadZone(string zoneNumber);

        #endregion
    }
}
