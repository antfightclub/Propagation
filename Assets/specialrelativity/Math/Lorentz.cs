using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

namespace SpecialRelativity
{
    public class Lorentz
    {
        // natural units; time and length have the same dimension;
        // 1 second = 299792458 meters
        // if c is given as natural units, velocity should be given as fraction of 1
        // if c is given as lightspeed in meters per second, velocity should be given as meters per second
        public int c_natural = 1; 
        public int c_lightspeed = 299792458; // lightspeed is defined as meters per second!

        // Returns the ordinary inner product of two 3-vectors
        Vector3 InnerProduct(Vector3 a, Vector3 b)
        {
            return new Vector3(
                a.x * b.x,
                a.y * b.y,
                a.z * b.z);
        }

        // Returns the Lorentzian inner product of two 4-vectors
        // Note that I'm using wxyz
        Vector3 LorentzianInnerProduct(Vector4 a, Vector4 b)
        {
            return new Vector4(
              -(a.w * b.w),
                a.x * b.x,
                a.y * b.y,
                a.z * b.z);
        }

        float OrdinarySquaredNorm(Vector3 a)
        {
            float norm = 
                  a.x * a.x
                + a.y * a.y
                + a.z * a.z;
            return norm;
        }

        // Returns the Lorentzian Norm, which tells us whether a 4-vector in D-dimensional spacetime
        // is spacelike, lightlike, or timelike compared to the rest frame (origin)
        float LorentzianNorm(Vector4 a)
        {
            float res = (-(a.w * a.w) 
                + a.x * a.x 
                + a.y * a.y 
                + a.z * a.z);
            return res;
        }

        public enum Likeness
        {
            None = -1,      // Didn't work!
            SpaceLike = 0,  // less than zero:    separated in space
            LightLike = 1,  // equals to zero:    light cone
            TimeLike  = 2   // greater than zero: separated in time
        }

        /// <summary>
        /// Be mindful - I've added the last return statement to appease the 
        /// but it might come back to bite me in the ass. If the first three if
        /// statements do not return before the last return statement I don't know
        /// what went wrong! :<
        /// </summary>
        /// <param name="a"></param>
        /// <returns>Likeness enum</returns>
        public Likeness DetermineLikeness(Vector4 a)
        {
            float lorentzianNorm = LorentzianNorm(a);
            if(lorentzianNorm >  0) { return Likeness.SpaceLike; }
            if(lorentzianNorm == 0) { return Likeness.LightLike; }
            if(lorentzianNorm <  0) { return Likeness.TimeLike;  }
            return Likeness.None;
        }

        // next implement boost matrix and rotation matrix
        public Matrix44 LorentzBoostMatrix(Vector3 vel)
        {
            float beta_x = vel.x / c_lightspeed;
            float beta_y = vel.y / c_lightspeed;
            float beta_z = vel.z / c_lightspeed;
            float v_2 = (beta_x * beta_x + beta_y * beta_y + beta_z * beta_z);
            float gamma = 1 / (Mathf.Sqrt(1 - v_2));
            return new Matrix44(
                new Vector4(gamma,         -beta_x* gamma,                  -beta_y*gamma,                  -beta_z*gamma                    ),
                new Vector4(-beta_x*gamma, (gamma-1)*((vel.x*vel.x)/v_2)+1, (gamma-1)*((vel.y*vel.x)/v_2)  , (gamma-1)*((vel.z*vel.x)/v_2)   ),
                new Vector4(-beta_y*gamma, (gamma-1)*((vel.x*vel.y)/v_2)  , (gamma-1)*((vel.y*vel.y)/v_2)+1, (gamma-1)*((vel.z*vel.y)/v_2)   ),
                new Vector4(-beta_z*gamma, (gamma-1)*((vel.x*vel.z)/v_2)  , (gamma-1)*((vel.y*vel.z)/v_2)  , (gamma-1)*((vel.z*vel.z)/v_2)+1 ));

        }

        public Matrix44 RotMatrixR(Vector3 col1, Vector3 col2, Vector3 col3)
        {
            return new Matrix44(
                new Vector4(1, 0, 0, 0),
                new Vector4(0, col1.x, col1.y, col1.z),
                new Vector4(0, col2.x, col2.y, col2.z),
                new Vector4(0, col3.x, col3.y, col3.z));
        }
        
        // then proper time and lorentz covariant velocity

        // Returns the displacement D vector between current and previous position
        public Vector4 DisplacementDVector(Vector4 previous, Vector4 current)
        {
            return new Vector4(
                current.w - previous.w,
                current.x - previous.x,
                current.y - previous.y,
                current.z - previous.z);
        }
        // Returns the proper time increased for a particle by an amount delta from the displacementDvector
        public float ProperTimeDelta(Vector4 DisplacementDVector)
        {
            float norm = LorentzianNorm(DisplacementDVector);
            return Mathf.Sqrt(-norm);
        }

        // Unsure whether this actually works since on paper it involves a derivative (eq. 41, sogebu et al)
        public Vector4 DVelocityVector(Vector4 DisplacementDVector, float properTimeDelta)
        {
            return new Vector4(
                DisplacementDVector.w / properTimeDelta,
                DisplacementDVector.x / properTimeDelta,
                DisplacementDVector.y / properTimeDelta,
                DisplacementDVector.z / properTimeDelta);
        }

        public Vector4 AdvanceProperTimeStep(Vector4 current, Vector4 DisplacementDVector, float ProperTimeDelta)
        {
            return new Vector4(
                current.w + DisplacementDVector.w * ProperTimeDelta,
                current.x + DisplacementDVector.x * ProperTimeDelta,
                current.y + DisplacementDVector.y * ProperTimeDelta,
                current.z + DisplacementDVector.y * ProperTimeDelta);
        }

        // not sure if this works since on paper it involves a derivative (eq. 53, sogebu et al)
        public Vector4 DAccelerationVector(Vector4 previous, Vector4 current, float properTimeDelta)
        {
            Vector4 displacementDVector = DisplacementDVector(previous, current);
            return new Vector4(
                displacementDVector.w / properTimeDelta,
                displacementDVector.x / properTimeDelta,
                displacementDVector.y / properTimeDelta,
                displacementDVector.z / properTimeDelta);
        }
        
        public float LorentzGammaFactor(Vector4 vel)
        {
            float beta_x = vel.x / c_lightspeed;
            float beta_y = vel.y / c_lightspeed;
            float beta_z = vel.z / c_lightspeed;
            float v_2 = (beta_x * beta_x + beta_y * beta_y + beta_z * beta_z);
            float gamma = 1 / (Mathf.Sqrt(1 - v_2));
            return gamma;
        }
        


        // then rest frame

        /*public Vector4 LorentzTransformToRestFrame(Vector4 vel)
        {
            float beta_x = vel.x / c_lightspeed;
            float beta_y = vel.y / c_lightspeed;
            float beta_z = vel.z / c_lightspeed;
            float v_norm = (beta_x * beta_x + beta_y * beta_y + beta_z * beta_z);
            float gamma = LorentzGammaFactor(vel);
            return new Matrix44(
                new Vector4(gamma),
                new Vector4(),
                new Vector4(),
                new Vector4());
        }*/



        // then drawing the world

    }

}