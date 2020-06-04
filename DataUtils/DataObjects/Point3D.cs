namespace DataUtils
{
    public class Point3D
    {
        public int X;
        public int Y;
        public int Z;

        public Point3D()
        {
        }

        public Point3D(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Point3D Copy()
        {
            var res = new Point3D {X = X, Y = Y, Z = Z};
            return res;
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}:{2}", X, Y, Z);
        }

        public override bool Equals(object obj)
        {
            if (obj is Point3D)
                return Equals((Point3D)obj);
            return false;
        }

        public bool Equals(Point3D other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.X == X && other.Y == Y && other.Z == Z;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = X;
                result = (result*397) ^ Y;
                result = (result*397) ^ Z;
                return result;
            }
        }
    }
}