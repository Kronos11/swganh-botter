using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWGANH.Core
{
    public class Vec3
    {
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }

        public Vec3(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
}
