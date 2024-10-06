using UnityEngine;
using System;

namespace SpecialRelativity
{
    public class Quat
    {
        public double _t, _x, _y, _z;
        
        /// <summary>
        /// If no arguments given, set t to 1, x,y,z to 0
        /// </summary>
        public Quat()
        {
            this._t = 1.0d;
            this._x = 0.0d;
            this._y = 0.0d;
            this._z = 0.0d;
        }
        
        public Quat(double t, double x, double y, double z)
        {
            Quat v = new Quat();
            v._t = t;
            v._x = x;
            v._y = y;
            v._z = z;
        }

        public Quat FromAx(double theta, Vector3D ax)
        {
            double c, s, x, y, z;
            c = Math.Cos(theta*0.5);
            s = Math.Sin(theta*0.5);
            x = ax.x;
            y = ax.y;
            z = ax.z;
            Quat v = new Quat();
            v._t = c;
            v._x = x * s;
            v._y = y * s;
            v._z = z * s;
            return v;
        }

        public Quat FromRotMat(Matrix44 R)
        {
            double m, n, e;
            int i;
            m = R.m00 + R.m11 + R.m22 + 1.0d;
            n = R.m00 - R.m11 - R.m22 + 1.0d;
            i = 0;
            if (m<n)
            {
                m = n;
                i = 1;
            }
            n = R.m11 - R.m22 - R.m00 + 1.0d;
            if(m<n)
            {
                m = n;
                i = 2;
            }
            n = R.m22 - R.m00 - R.m11 + 1.0d;
            if (m<n)
            {
                m = n;
                i = 3;
            }

            e = Math.Sqrt(m) * 0.5d;
            m = 0.25d / e;
            Quat v = new Quat();
            if (i==0)
            {
                v._t = e;
                v._x = (R.m21 - R.m12) * m;
                v._y = (R.m02 - R.m20) * m;
                v._z = (R.m10 - R.m01) * m;
            }
            if (i==1)
            {
                v._t = (R.m21 - R.m12) * m;
                v._x = e;
                v._y = (R.m01 + R.m10) * m;
                v._z = (R.m02 + R.m20) * m;  
            }
            if (i==2)
            {
                v._t = (R.m02 - R.m20) * m;
                v._x = (R.m01 + R.m10) * m;
                v._y = e;
                v._z = (R.m12 + R.m21) * m;
            }
            else
            {
                v._t = (R.m10 - R.m01) * m;
                v._x = (R.m02 + R.m20) * m;
                v._y = (R.m12 + R.m21) * m;
                v._z = e;
            }
            return v;
        }


    }

}