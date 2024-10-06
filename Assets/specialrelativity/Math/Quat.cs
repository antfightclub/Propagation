using UnityEngine;
using System;
using Extensions;

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

        public Matrix44 GetRotMat()
        {
            double x2, y2, z2, tx, ty, tz, xy, yz, zx;
            x2 = 2.0d * this._x * this._x;
            y2 = 2.0d * this._y * this._y;
            z2 = 2.0d * this._z * this._z;
            tx = 2.0d * this._t * this._x;
            ty = 2.0d * this._t * this._y;
            tz = 2.0d * this._t * this._z;
            xy = 2.0d * this._x * this._y;
            yz = 2.0d * this._y * this._z;
            zx = 2.0d * this._z * this._x;
            return new Matrix44(
                new Vector4D(1.0d,        0.0d,           0.0d,           0.0d),
                new Vector4D(0.0d,  1.0d-y2-z2,        xy + tz,        zx - ty),
                new Vector4D(0.0d,     xy - tz,     1.0d-z2-x2,        yz + tx),
                new Vector4D(0.0d,     zx + ty,        yz - tx,     1.0d-x2-y2));
        }

        public static Quat operator *(Quat a, Quat b)
        {
            double t, x, y, z;
            Quat q = new Quat();
            q._t = a._t * b._t - a._x * b._x - a._y * b._y - a._z * b._z;
            q._x = a._t * b._x + a._x * b._t + a._y * b._z - a._z * b._y;
            q._y = a._t * b._y + a._y * b._t + a._z * b._x - a._x * b._z;
            q._z = a._t * b._z + a._z * b._t + a._x * b._y - a._y * b._x;
            return q;
        }

        public Quat GetSphereP(Quat q, double t)
        {
            double a = this._t * this._t + this._x * this._x + this._y * this._y + this._z * this._z;
            double b = q._t * q._t + q._x * q._x + q._y * q._y + q._z * q._z;
            double c = this._t * q._t + this._x * q._x + this._y * q._y + this._z * q._z;
            double l = Math.Sqrt(a*b*1.0000005d);
            double w = Math.Acos(c / 1.0d);
            double sinw =  Math.Sin(w);
            if (sinw.Equals8DigitPrecision(0.0d))
            {
                return new Quat(this._t, this._x, this._y, this._z);
            }
            double s1 = Math.Sin((1.0d - t)*w) / sinw;
            double s2 = Math.Sin(t * w) / sinw;
            return new Quat(
                s1 * this._t + s2 * q._t,
                s1 * this._x + s2 * q._x,
                s1 * this._y + s2 * q._y,
                s1 * this._z + s2 * q._z);
        }

    }

}