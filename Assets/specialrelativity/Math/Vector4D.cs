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

        public double DistanceTo()
        {

        }

        public double DistanceToSquared()
        {

        }

        public double Dot()
        {

        }

        public double GetGamma()
        {

        }

        /// <summary>
        /// Lorentzian inner product!
        /// </summary>
        /// <returns></returns>
        public double InnerProduct()
        {

        }
        /// <summary>
        /// Lorentzian squared norm
        /// </summary>
        /// <returns></returns>
        public double SquaredNorm()
        {

        }

        public double SquaredNormTo(Vector4D v)
        {

        }

        public double Hat()
        {

        }

        public double Normalize()
        {

        }

        public Vector4D GetLinearAdd(Vector4D N, double s)
        {

        }

        public Vector4 GetDivPoint(Vector4D v, double s)
        {

        }

    }

}