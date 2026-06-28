using System.Drawing;

namespace DataUtils
{
    public class SketchRoom : Point3D
    {
        public Color RoomColor = Color.Transparent;

        public SketchRoom()
        {
        }

        public SketchRoom(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public SketchRoom(int x, int y, int z, Color roomColor)
        {
            X = x;
            Y = y;
            Z = z;
            RoomColor = roomColor;
        }

        public new SketchRoom Copy()
        {
            var res = new SketchRoom { X = X, Y = Y, Z = Z };
            return res;
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}:{2}:{3}", X, Y, Z, RoomColor);
        }

        public override bool Equals(object obj)
        {
            if (obj is SketchRoom)
            {
                return
                    (X == ((SketchRoom)(obj)).X && Y == ((SketchRoom)(obj)).Y && Z == ((SketchRoom)(obj)).Z &&
                     RoomColor.ToArgb() == ((SketchRoom)(obj)).RoomColor.ToArgb());
            }
            return false;
        }

        public bool Equals(SketchRoom other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && other.RoomColor.Equals(RoomColor);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                {
                    return (base.GetHashCode() * 397) ^ RoomColor.GetHashCode();
                }
            }
        }
    }
}