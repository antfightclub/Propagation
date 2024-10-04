using Unity.VisualScripting;
using UnityEngine;

namespace SpecialRelativity
{
    public class PhaseSpace
    {
        public Vector4 X; public Vector4 U;

        void Init(Vector4 X, Vector4 U)
        {
            this.X = new Vector4(X.w, X.x, X.y, X.z);
            this.U = new Vector4(U.w, U.x, U.y, U.z);
        }

        public PhaseSpace(Vector4 X, Vector4 U)
        {
            this.X = new Vector4(X.w, X.x, X.y, X.z);
            this.U = new Vector4(U.w, U.x, U.y, U.z);
        }
        public Vector4 GetX() { return this.X; }
        public Vector4 GetU() { return this.U; }

        public PhaseSpace Copy()
        {
            return new PhaseSpace(this.X, this.U);
        }

        public Vector4 GetResist(float b)
        {
            return new Vector4(
                0.0f, 
                -this.U.x*b,
                -this.U.y*b,
                -this.U.z*b);
        }

        // I think I need to rethink some of the code from sogebu et al
        // Not sure... but I gotta fix the CS0120 error...
        public Vector4 Transform(Vector4 acceleration, float ds)
        {
            this.X.w = this.X.w + this.U.w * ds;
            this.X.x = this.X.x + this.U.x * ds;
            this.X.y = this.X.y + this.U.y * ds;
            this.X.z = this.X.z + this.U.z * ds;
            acceleration.w = 0.0f;
            Vector4 negu = -GetU();
            Matrix44 L = Matrix44.Lorentz(negu);
            Vector4 accel = L.get_transform(acceleration);
            float r = accel.x * accel.x + accel.y * accel.y + accel.z * accel.z;
            if (r>10.0)
            { this.U  += new Vector4(
                accel.x * (ds*10.0f/r),
                accel.y * (ds*10.0f/r),
                accel.z * (ds*10.0f/r)); 
            }
            else
            {
                this.U += new Vector4(
                    accel.x * ds,
                    accel.y * ds,
                    accel.z * ds);
            }
            this.U.w = Mathf.Sqrt(1.0f + this.U.x * this.U.x + this.U.y * this.U.y + this.U.z + this.U.z);
            return this.U;
        }


    }

}