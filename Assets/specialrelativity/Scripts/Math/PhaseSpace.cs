using UnityEngine;
using System;

namespace SpecialRelativity
{
    public class PhaseSpace
    {
        public Vector4D X; public Vector4D U;

        // This is not really needed; the new PhaseSpace(Vector4D X, Vector4D U) constructor would be used to initialize anyways.
        // Artifact of transliterating Python syntax...
        void Init(Vector4D X, Vector4D U)
        {
            this.X = new Vector4D(X.t, X.x, X.y, X.z);
            this.U = new Vector4D(U.t, U.x, U.y, U.z);
        }

        public PhaseSpace(Vector4D X, Vector4D U)
        {
            this.X = new Vector4D(X.t, X.x, X.y, X.z);
            this.U = new Vector4D(U.t, U.x, U.y, U.z);
        }
        public Vector4D x 
        { 
            get { return this.X; } 
            set { this.X = value;}
        }
        public Vector4D u 
        { 
            get { return this.U; }
            set { this.U = value;}
        }

        public PhaseSpace Copy()
        {
            return new PhaseSpace(this.X, this.U);
        }

        public Vector4D GetResist(double b)
        {
            return new Vector4D(
                0.0d,
                -this.U.x * b,
                -this.U.y * b,
                -this.U.z * b);
        }

        // I think I need to rethink some of the code from sogebu et al
        // Not sure... but I gotta fix the CS0120 error...
        public Vector4D Transform(Vector4D acceleration, double ds)
        {
            this.X.t += this.U.t * ds;
            this.X.x += this.U.x * ds;
            this.X.y += this.U.y * ds;
            this.X.z += this.U.z * ds;
            acceleration.t = 0.0d;
            Vector4D neg = new Vector4D(-U.t, -U.x, -U.y, -U.z);
            Matrix44 L = Matrix44.Zero;
            L = L.Lorentz(neg);
            Vector4D accel = L.GetTransform(acceleration);
            double r = accel.x * accel.x + accel.y * accel.y + accel.z * accel.z;
            if (r > 10.0)
            {
                this.U += new Vector4D(0.0d,
                accel.x * (ds * 10.0d / r),
                accel.y * (ds * 10.0d / r),
                accel.z * (ds * 10.0d / r));
            }
            else
            {
                this.U += new Vector4D(0.0d,
                    accel.x * ds,
                    accel.y * ds,
                    accel.z * ds);
            }
            this.U.t = Math.Sqrt(1.0f + this.U.x * this.U.x + this.U.y * this.U.y + this.U.z + this.U.z);
            return this.U;
        }
    }
}