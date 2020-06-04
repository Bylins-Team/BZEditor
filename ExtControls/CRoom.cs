using System;
using System.Collections.Generic;
using System.Text;

namespace ExtControls
{
    class CRoom : Point3D
    {
        public string VNum = "";
        public CRoom(string vnum, int X, int Y, int Z)
            : base(X, Y, Z)
        {
            VNum = vnum;
        }
    }
}
