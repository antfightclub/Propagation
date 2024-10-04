using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using System;

namespace SpecialRelativity
{
    public class Vector3D
    {
        double x, y, z;

        Vector3D(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// Get the x component of a Vector3D
        /// </summary>
        /// <returns>double</returns>
        public double GetX() { return this.x; }
        /// <summary>
        /// Get the y component of a Vector3D
        /// </summary>
        /// <returns>double</returns>
        public double GetY() { return this.y; }
        /// <summary>
        /// Get the z component of a Vector3D
        /// </summary>
        /// <returns>double</returns>
        public double GetZ() { return this.z; }

        /// <summary>
        /// Set the x component of a Vector3D
        /// </summary>
        /// <param name="x"></param>
        public void SetX(double x) { this.x = x; }
        /// <summary>
        /// Set the y component of a Vector3D
        /// </summary>
        /// <param name="y"></param>
        public void SetY(double y) { this.y = y; }
        /// <summary>
        /// Set the z component of a Vector3D
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
        ///  Returns u^hat = u / |a|, where |a| is the square   known as a vector with a hat
        /// </summary>
        /// <returns>Vector3D</returns>
        public Vector3D GetHat()
        {
            double r = this.x * this.x + this.y * this.y + this.z * this.z;
            if (r == 0)
            {

            }
            r = 1.0d / Math.Sqrt(r);
            return new Vector3D(this.x*r, this.y*r, this.z*r);
        }

        /// <summary>
        /// Squashes a vector 
        /// </summary>
        /// <returns></returns>
        public Vector3D Hat()
        {

        }

    }
}