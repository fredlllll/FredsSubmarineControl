using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FredsSubmarineControlServer
{
    internal class VectorConstants
    {
        public static readonly Vector3 forward = Vector3.UnitZ;
        public static readonly Vector3 up = Vector3.UnitY;
        public static readonly Vector3 right = Vector3.UnitX;
        public static readonly Vector3 back = -Vector3.UnitZ;
        public static readonly Vector3 down = -Vector3.UnitY;
        public static readonly Vector3 left = -Vector3.UnitX;
    }
}
