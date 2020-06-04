namespace DataUtils
{
    public class GlobalSketchRoom : Point3D
    {
        public int ZoneNum = -1;
        public int WldRoomVNum = -1;

        public GlobalSketchRoom()
        {
        }

        public GlobalSketchRoom(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public GlobalSketchRoom(int x, int y, int z, int zoneNum)
        {
            X = x;
            Y = y;
            Z = z;
            ZoneNum = zoneNum;
        }

        public Point3D Location => new Point3D(X,Y,Z);

        public new GlobalSketchRoom Copy()
        {
            var res = new GlobalSketchRoom { X = X, Y = Y, Z = Z, ZoneNum=ZoneNum };
            return res;
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}:{2}:{3}", X, Y, Z, ZoneNum);
        }

        public override bool Equals(object obj)
        {
            if (obj is GlobalSketchRoom)
            {
                return
                    (X == ((GlobalSketchRoom)(obj)).X && Y == ((GlobalSketchRoom)(obj)).Y && Z == ((GlobalSketchRoom)(obj)).Z &&
                     ZoneNum == ((GlobalSketchRoom)(obj)).ZoneNum);
            }
            return false;
        }

        public bool Equals(SketchRoom other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && other.RoomColor.Equals(ZoneNum);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                {
                    return (base.GetHashCode() * 397) ^ ZoneNum.GetHashCode();
                }
            }
        }
    }
}