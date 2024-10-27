using UnityEngine;
using System;

namespace SpecialRelativity
{
    public class PhaseSpace
    {
        public Vector4D _spaceTimeCoords; public Vector4D _spaceTimeVelocity;

        public PhaseSpace(Vector4D X, Vector4D U)
        {
            this._spaceTimeCoords = new Vector4D(X.t, X.x, X.y, X.z);
            this._spaceTimeVelocity = new Vector4D(U.t, U.x, U.y, U.z);
        }
        public Vector4D X
        { 
            get { return this._spaceTimeCoords; } 
            set { this._spaceTimeCoords = value;}
        }
        public Vector4D U
        { 
            get { return this._spaceTimeVelocity; }
            set { this._spaceTimeVelocity = value;}
        }

        public PhaseSpace Copy()
        {
            return new PhaseSpace(this._spaceTimeCoords, this._spaceTimeVelocity);
        }

        public Vector4D GetResist(double b)
        {
            return new Vector4D(
                0.0d,
                -this._spaceTimeVelocity.x * b,
                -this._spaceTimeVelocity.y * b,
                -this._spaceTimeVelocity.z * b);
        }

        // I think I need to rethink some of the code from sogebu et al
        // Not sure... but I gotta fix the CS0120 error...
        public Vector4D Transform(Vector4D acceleration, double ds)
        {
            this._spaceTimeCoords.t += this._spaceTimeVelocity.t * ds;
            this._spaceTimeCoords.x += this._spaceTimeVelocity.x * ds;
            this._spaceTimeCoords.y += this._spaceTimeVelocity.y * ds;
            this._spaceTimeCoords.z += this._spaceTimeVelocity.z * ds;
            acceleration.t = 0.0d;
            Vector4D neg = new Vector4D(-_spaceTimeVelocity.t, -_spaceTimeVelocity.x, -_spaceTimeVelocity.y, -_spaceTimeVelocity.z);
            Matrix44 L = Matrix44.Zero;
            L = L.Lorentz(neg);
            Vector4D accel = L.GetTransform(acceleration);
            double r = accel.x * accel.x + accel.y * accel.y + accel.z * accel.z;
            if (r > 10.0)
            {
                this._spaceTimeVelocity += new Vector4D(0.0d,
                    accel.x * (ds * 10.0d / r),
                    accel.y * (ds * 10.0d / r),
                    accel.z * (ds * 10.0d / r));
            }
            else
            {
                this._spaceTimeVelocity += new Vector4D(0.0d,
                        accel.x * ds,
                        accel.y * ds,
                        accel.z * ds);
            }
            this._spaceTimeVelocity.t = Math.Sqrt(1.0f + this._spaceTimeVelocity.x * this._spaceTimeVelocity.x + this._spaceTimeVelocity.y * this._spaceTimeVelocity.y + this._spaceTimeVelocity.z + this._spaceTimeVelocity.z);
            return this._spaceTimeVelocity;
        }
    }
}