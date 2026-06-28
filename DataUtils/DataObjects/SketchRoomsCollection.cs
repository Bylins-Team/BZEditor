using System.Drawing;

namespace DataUtils
{
    public class SketchRoomsCollection : BaseDataArrayList
    {
        #region Delegates

        public new delegate void ChangeEvent(object changedClass, object sender);

        #endregion

        public new event ChangeEvent Changed;

        public override void FireChangeEvent(object sender)
        {
            if (Changed != null && StaticData.CanFireChangeEvent)
                Changed(this, sender);
        }

        private Color lastSketchColor = Color.LightSteelBlue;

        public Color LastSketchColor
        {
            get => lastSketchColor;
            set => lastSketchColor = value;
        }

        public void AddSketchRoom(int x, int y, int z, Color roomColor)
        {
            Add(new SketchRoom(x, y, z, roomColor));
        }

        public SketchRoom GetCketchRoom(int num)
        {
            if (num <= Count) return null;
            return (SketchRoom) (this[num]);
        }

        public SketchRoom GetSketchRoom(int x, int y, int z)
        {
            foreach (SketchRoom sr in this)
            {
                if (sr.X == x && sr.Y == y && sr.Z == z)
                    return sr;
            }
            return null;
        }

        public void RemoveSketchRoom(int x, int y, int z)
        {
            SketchRoom srtoremove = null;
            foreach (SketchRoom sr in this)
            {
                if (sr.X == x && sr.Y == y && sr.Z == z)
                    srtoremove = sr;
            }
            if (srtoremove != null)
                Remove(srtoremove);
        }
    }
}