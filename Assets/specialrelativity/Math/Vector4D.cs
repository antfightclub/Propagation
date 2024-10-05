using UnityEngine;
using Extensions;
using System;

namespace SpecialRelativity
{
    public class Vector4D
    {
        double t, x, y, z;

        /// <summary>
        /// Constructor for Vector4D
        /// </summary>
        /// <param name="t"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        Vector4D(double t, double x, double y, double z)
        {
            this.t = t;
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public double GetW() { return this.t; }
        public double GetX() { return this.x; }
        public double GetY() { return this.y; }
        public double GetZ() { return this.z; }

        public void SetT(double t) { this.t = t; }
        public void SetX(double x) { this.x = x; }
        public void SetY(double y) { this.y = y; }
        public void SetZ(double z) { this.z = z; }

        public Vector3D Get3D() { return new Vector3D(this.x, this.y, this.z); }
        public void Set3D(Vector3D v) { this.x = v.GetX(); this.y = v.GetY(); this.z = v.GetZ();  }

        // Define basic operators with doubles in vector

        public static Vector4D operator +(Vector4D a, Vector4D b) 
        { 
            return new Vector4D(
            a.t+b.t, 
            a.x+b.x, 
            a.y+b.y, 
            a.z+b.z); 
        }

        public static Vector4D operator -(Vector4D a)
        {
            return new Vector4D(
                -a.t,
                -a.x,
                -a.y,
                -a.z);
        }

        public static Vector4D operator -(Vector4D a, Vector4D b)
        {
            return new Vector4D(
                a.t - b.t,
                a.x - b.x,
                a.y - b.y,
                a.z - b.z);
        }

        public static Vector4D operator *(Vector4D a, double rhs)
        {
            return new Vector4D(
                a.t * rhs,
                a.x * rhs,
                a.y * rhs,
                a.z * rhs);
        }

        public static Vector4D operator /(Vector4D a, double rhs)
        {
            rhs = 1.0d / rhs;
            return new Vector4D(
                a.t * rhs,
                a.x * rhs,
                a.y * rhs,
                a.z * rhs);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public double Length()
        {
            return Math.Sqrt(this.x * this.x + this.y * this.y + this.z * this.z);
        }

        public double LengthSquared()
        {
            return this.x * this.x + this.y * this.y + this.z * this.z;
        }

        
        public double DistanceTo(Vector4D v)
        {
            double x, y, z;
            x = this.x - v.x;
            y = this.y - v.y;
            z = this.z - v.z;
            return Math.Sqrt(x * x + y * y + z * z);
        }

        public double DistanceToSquared(Vector4D v)
        {
            double x, y, z;
            x = this.x - v.x;
            y = this.y - v.y;
            z = this.z - v.z;
            return x * x + y * y + z*z;

        }

        public double Dot(Vector4D v)
        {
            return this.x * v.x + this.y * v.y + this.z * v.z;
        }

        public double GetGamma()
        {
            return Math.Sqrt(1.0d + this.x * this.x + this.y * this.y + this.z * this.z);
        }

        /// <summary>
        /// Lorentzian inner product!
        /// </summary>
        /// <returns></returns>
        public double InnerProduct(Vector4D v)
        {
            return Math.Sqrt(this.x * v.x + this.y * v.y + this.z * v.z - this.t * v.t);
        }
        /// <summary>
        /// Lorentzian squared norm
        /// </summary>
        /// <returns></returns>
        public double SquaredNorm()
        {
            return this.x * this.x + this.y * this.y + this.z * this.z - this.t * this.t;
        }

        public double SquaredNormTo(Vector4D v)
        {
            double t, x, y, z;
            t = this.t - v.t;
            x = this.x - v.x;
            y = this.y - v.y;
            z = this.z - v.z;
            return x * x + y * y + z * z - t * t;
        }

        public Vector3D Hat()
        {
            double r = this.x * this.x + this.y * this.y + this.z * this.z;
            if (r.Equals8DigitPrecision(0.0d))
            {
                return new Vector3D(1.0d, 0.0d, 0.0d);
            }
            r = 1.0d / Math.Sqrt(r);
            return new Vector3D(this.x * r, this.y * r, this.z * r);
        }

        public Vector4D Normalize()
        {
            double r = this.x * this.x + this.y * this.y + this.z * this.z;
            if (r.Equals8DigitPrecision(0.0d))
            {
                return new Vector4D(this.t, 0.0d, 0.0d, 0.0d);
            }
            r = 1.0d / Math.Sqrt(r);
            return new Vector4D(this.t, this.x*r, this.y*r, this.z*r);
        }

        public Vector4D GetLinearAdd(Vector4D N, double s)
        {
            double tt, xx, yy, zz;
            tt = this.t + N.t * s;
            xx = this.x + N.x * s;
            yy = this.y + N.y * s;
            zz = this.z + N.z * s;
            return new Vector4D(tt, xx, yy, zz);
        }

        /// <summary>
        /// s must be between 0 and 1
        /// </summary>
        /// <param name="v"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public Vector4D GetDivPoint(Vector4D v, double s)
        {
            if(s>0 && s<1)
            {
                throw new ArgumentOutOfRangeException("s must lie in the interval [0 and 1]");
            }
            double t, tt, xx, yy, zz;
            t  = 1.0d - s;
            tt = this.t * t + v.t * s;
            xx = this.x * t + v.x * s;
            yy = this.y * t * v.y * s;
            zz = this.z * t * v.z * s;
            return new Vector4D(tt, xx, yy, zz);
        }

    }

}