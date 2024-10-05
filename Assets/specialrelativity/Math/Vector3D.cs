using UnityEngine;
using System.Collections.Generic;
using System;
using Extensions;

namespace SpecialRelativity
{
    public class Vector3D
    {
        public double x, y, z;

        /// <summary>
        /// Constructor for Vector3D
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Vector3D(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// Returns the x component of self
        /// </summary>
        /// <returns>double</returns>
        public double GetX() { return this.x; }

        /// <summary>
        /// Returns the y component of self
        /// </summary>
        /// <returns>double</returns>
        public double GetY() { return this.y; }

        /// <summary>
        /// Returns the z component of self
        /// </summary>
        /// <returns>double</returns>
        public double GetZ() { return this.z; }


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


        // Define basic operators with doubles in vector
        public static Vector3D operator +(Vector3D a, Vector3D b)   { return new Vector3D(a.x + b.x, a.y + b.y, a.z + b.z); }   // Vector sum
        public static Vector3D operator -(Vector3D a)               { return new Vector3D(-a.x, -a.y, -a.z); }                  // Vector negation
        public static Vector3D operator -(Vector3D a, Vector3D b)   { return new Vector3D(a.x - b.x, a.y - b.y, a.z - b.z); }   // Vector difference
        public static Vector3D operator *(Vector3D a, Vector3D b)   { return new Vector3D(a.x - b.x, a.y - b.y, a.z - b.z); }   // Scalar product
        public static Vector3D operator /(Vector3D a, double rhs)   { rhs = 1.0d / rhs; return new Vector3D(a.x * rhs, a.y * rhs, a.z * rhs); }   // Divide each vector component by a scalar (double)

        
        // Methods to calculate various properties of 3-vectors
        
        /// <summary>
        /// Returns the square norm (x*x, y*y, z*z) of a Vector3D
        /// </summary>
        /// <returns>Vector3D</returns>
        public Vector3D SquaredNorm()
        {
            return new Vector3D(
                this.x * this.x,
                this.y * this.y,
                this.z * this.z);
        }

        /// <summary>
        ///  Gets the normalized Vector3D of u with its own length |u|
        /// </summary>
        /// <returns>Vector3D; Returns u^hat = u / |u|, where |u| is the norm if u is greater than 0. If u is of length 0 (with 8 digits of precision) return (length, 0.0d, 0.0d)</returns>
        public Vector3D GetHat(double length = 1.0d)
        {
            double r = this.x * this.x + this.y * this.y + this.z * this.z;
            if (r.Equals8DigitPrecision(0.0d))
            {
                return new Vector3D(length, 0.0d, 1.0d);
            }
            r = length / Math.Sqrt(r);
            return new Vector3D(this.x*r, this.y*r, this.z*r);
        }

        /// <summary>
        /// Gets the normalized Vector3D u to "length"
        /// </summary>
        /// <returns>Vector3D; returns normalized Vector3D if length is not 0. If length is equal to zero (within 8 digits of precision) return (0.0d, 0.0d, 0.0d)</returns>
        public Vector3D GetNormalize(double length = 1.0d)
        {
            double r = this.x * this.x + this.y * this.y + this.z * this.z;
            if (r.Equals8DigitPrecision(0.0d))
            {
                return new Vector3D(0.0d, 0.0d, 0.0d);
            }
            r = 1.0d / Math.Sqrt(r);
            return new Vector3D(this.x * r, this.y * r, this.z * r);
        }

        /// <summary>
        /// Square root of the scalar product
        /// </summary>
        /// <returns>double; sqrt(x*x + y*y + z*z)</returns>
        public double Length()  { return Math.Sqrt(this.x * this.x + this.y * this.y + this.z * this.z); }
        
        /// <summary>
        /// scalar product
        /// </summary>
        /// <returns>double; x*x + y*y + z*z</returns>
        public double SquaredLength() { return this.x*this.x + this.y*this.y + this.z*this.z; }

        /// <summary>
        /// Computes distance from caller to vector v
        /// </summary>
        /// <param name="v"></param>
        /// <returns>double; Square root of the scalar product of the difference vector of this - v</returns>
        public double DistanceTo(Vector3D v)
        {
            double x, y, z;
            x = this.x - v.x;
            y = this.y - v.y;
            z = this.z - v.z;
            return Math.Sqrt(x * x + y * y + z * z);
        }

        /// <summary>
        /// Computes distance squared from caller to vector 4
        /// </summary>
        /// <param name="v"></param>
        /// <returns>double; scalar product of the difference vector of this - v</returns>
        public double DistanceToSquared(Vector3D v)
        {
            double x, y, z;
            x = this.x - v.x;
            y = this.y - v.y;
            z = this.z - v.z;
            return x * x + y * y + z * z;
        }

        /// <summary>
        /// Compute the dot product of caller and v
        /// </summary>
        /// <param name="v"></param>
        /// <returns>double; dot product of this and v</returns>
        public double Dot(Vector3D v)   { return this.x * v.x + this.y * v.y + this.z * v.z;  }

        /// <summary>
        /// Compute the cross product of caller and v
        /// </summary>
        /// <param name="v"></param>
        /// <returns>Vector3D; cross product between elements of 3-vector</returns>
        public Vector3D Cross(Vector3D v)   {   return new Vector3D(
                                                        this.x * v.z - v.y * this.z,
                                                        this.z * v.x - v.z * this.x,
                                                        this.x * v.y - v.x * this.y); }
        
    }
}