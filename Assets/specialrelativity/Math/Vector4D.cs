using UnityEngine;
using Extensions;
using System;

namespace SpecialRelativity
{
    public class Vector4D
    {
        public double t, x, y, z;

        /// <summary>
        /// Constructor for Vector4D
        /// </summary>
        /// <param name="t"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Vector4D(double t, double x, double y, double z)
        {
            this.t = t;
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// Returns the t component of self
        /// </summary>
        /// <returns></returns>
        public double GetT() { return this.t; }

        /// <summary>
        /// Returns the x component of self
        /// </summary>
        /// <returns></returns>
        public double GetX() { return this.x; }

        /// <summary>
        /// Returns the y component of self
        /// </summary>
        /// <returns></returns>
        public double GetY() { return this.y; }

        /// <summary>
        /// Returns the z component of self
        /// </summary>
        /// <returns></returns>
        public double GetZ() { return this.z; }


        /// <summary>
        /// Set the t component of self
        /// </summary>
        /// <param name="t"></param>
        public void SetT(double t) { this.t = t; }

        /// <summary>
        /// Set the x component of self
        /// </summary>
        /// <param name="x"></param>
        public void SetX(double x) { this.x = x; }

        /// <summary>
        /// Set the y component of self
        /// </summary>
        /// <param name="y"></param>
        public void SetY(double y) { this.y = y; }

        /// <summary>
        /// Set the z component of self
        /// </summary>
        /// <param name="z"></param>
        public void SetZ(double z) { this.z = z; }



        /// <summary>
        /// Get the spatial (x, y, z) components of Vector4D as Vector3D
        /// </summary>
        /// <returns></returns>
        public Vector3D Get3D() { return new Vector3D(this.x, this.y, this.z); }
        /// <summary>
        /// Set the spatial (x, y, z) component of a Vector4D with a Vector3D
        /// </summary>
        /// <param name="v"></param>
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
        /// Get the vector length of the spatial component (x,y,z) of a Vector4D
        /// as Sqrt(xx + yy + zz) (compared to coordinate system origin!)
        /// </summary>
        /// <returns></returns>
        public double Length()
        {
            return Math.Sqrt(this.x * this.x + this.y * this.y + this.z * this.z);
        }

        /// <summary>
        /// Get the vector length squared of the spatial component (x, y, z) of a Vector4D 
        /// as (xx + yy + zz)
        /// </summary>
        /// <returns></returns>
        public double LengthSquared()
        {
            return this.x * this.x + this.y * this.y + this.z * this.z;
        }

        /// <summary>
        /// Get a number (distance) in 3D between spatial components of self and v
        /// </summary>
        /// <param name="v"></param>
        /// <returns>double</returns>
        public double DistanceTo(Vector4D v)
        {
            double x, y, z;
            x = this.x - v.x;
            y = this.y - v.y;
            z = this.z - v.z;
            return Math.Sqrt(x * x + y * y + z * z);
        }

        /// <summary>
        /// Get a number (distance squared) in 3D between spatial components of self and v
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public double DistanceToSquared(Vector4D v)
        {
            double x, y, z;
            x = this.x - v.x;
            y = this.y - v.y;
            z = this.z - v.z;
            return x * x + y * y + z*z;

        }

        /// <summary>
        /// Get the dot product of caller and v
        /// </summary>
        /// <param name="v"></param>
        /// <returns>double</returns>
        public double Dot(Vector4D v)
        {
            return this.x * v.x + this.y * v.y + this.z * v.z;
        }

        /// <summary>
        /// Get gamma (lorentz factor) of the spatial components. If vector is 0 length it returns 1.0d
        /// </summary>
        /// <returns></returns>
        public double GetGamma()
        {
            return Math.Sqrt(1.0d + this.x * this.x + this.y * this.y + this.z * this.z);
        }

        /// <summary>
        /// Get the lorentzian inner product between self and v
        /// </summary>
        /// <returns>double</returns>
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

        /// <summary>
        /// Lorentzian squared norm between self and v
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public double SquaredNormTo(Vector4D v)
        {
            double t, x, y, z;
            t = this.t - v.t;
            x = this.x - v.x;
            y = this.y - v.y;
            z = this.z - v.z;
            return x * x + y * y + z * z - t * t;
        }

        // TODO: Should implement "length" instead of "1.0d" for flexibility. OBS: more of these methods neeed this in Vector3D.cs and Vector4D.cs!
        /// <summary>
        /// Normalizes the spatial component of self to length. If length of self Vector4D is 0, 
        /// return instead Vector3D(length, 0, 0)
        /// </summary>
        /// <returns>Vector3D</returns>
        public Vector3D GetHat(double length = 1.0d)
        {
            double r = this.x * this.x + this.y * this.y + this.z * this.z;
            if (r.Equals8DigitPrecision(0.0d))
            {
                return new Vector3D(length, 0.0d, 0.0d);
            }
            r = length / Math.Sqrt(r);
            return new Vector3D(this.x * r, this.y * r, this.z * r);
        }

        /// <summary>
        /// Gets the normalized spatial component of self vector to length
        /// </summary>
        /// <returns>Vector4D</returns>
        public Vector4D GetNormalize(double length = 1.0d)
        {
            double r = this.x * this.x + this.y * this.y + this.z * this.z;
            if (r.Equals8DigitPrecision(0.0d))
            {
                return new Vector4D(this.t, 0.0d, 0.0d, 0.0d);
            }
            r = length / Math.Sqrt(r);
            return new Vector4D(this.t, this.x*r, this.y*r, this.z*r);
        }

        /// <summary>
        /// Returns a Vector4D (this + s * N)
        /// </summary>
        /// <param name="N"></param>
        /// <param name="s"></param>
        /// <returns>Vector4D</returns>
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
        /// s must be between 0 and 1. equation is (1-s)*this + s * othr
        /// </summary>
        /// <param name="v"></param>
        /// <param name="s"></param>
        /// <returns>Vector4D</returns>
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