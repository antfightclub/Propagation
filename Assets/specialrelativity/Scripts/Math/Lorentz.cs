using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using System;

namespace SpecialRelativity
{
    public class Lorentz
    {
        // natural units; time and length have the same dimension;
        // 1 second = 299792458 meters
        // if c is given as natural units, velocity should be given as fraction of 1
        // if c is given as lightspeed in meters per second, velocity should be given as meters per second
        public double c_natural = 1.0d; 
        public double c_lightspeed = 299792458.0d; // lightspeed is defined as meters per second!

        // Returns the ordinary inner product of two 3-vectors
        Vector3D InnerProduct(Vector3D a, Vector3D b)
        {
            return new Vector3D(
                a.x * b.x,
                a.y * b.y,
                a.z * b.z);
        }

        // Returns the Lorentzian inner product of two 4-vectors
        // Note that I'm using txyz
        Vector4D LorentzianInnerProduct(Vector4D a, Vector4D b)
        {
            return new Vector4D(
              -(a.t * b.t),
                a.x * b.x,
                a.y * b.y,
                a.z * b.z);
        }

        double OrdinarySquaredNorm(Vector3D a)
        {
            double norm = 
                  a.x * a.x
                + a.y * a.y
                + a.z * a.z;
            return norm;
        }

        // Returns the Lorentzian Norm, which tells us whether a 4-vector in D-dimensional spacetime
        // is spacelike, lightlike, or timelike compared to the rest frame (origin)
        double LorentzianNorm(Vector4D a)
        {
            double res = (-(a.t * a.t) 
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
        public Likeness DetermineLikeness(Vector4D a)
        {
            double lorentzianNorm = LorentzianNorm(a);
            if(lorentzianNorm >  0) { return Likeness.SpaceLike; }
            if(lorentzianNorm == 0) { return Likeness.LightLike; }
            if(lorentzianNorm <  0) { return Likeness.TimeLike;  }
            return Likeness.None;
        }

        // next implement boost matrix and rotation matrix
        public Matrix44 LorentzBoostMatrix(Vector3D vel)
        {
            double beta_x = vel.x / c_lightspeed;
            double beta_y = vel.y / c_lightspeed;
            double beta_z = vel.z / c_lightspeed;
            double v_2 = (beta_x * beta_x + beta_y * beta_y + beta_z * beta_z);
            double gamma = 1 / (Math.Sqrt(1.0d - v_2));
            return new Matrix44(
                new Vector4D(gamma,         -beta_x* gamma,                  -beta_y*gamma,                  -beta_z*gamma                    ),
                new Vector4D(-beta_x*gamma, (gamma-1)*((vel.x*vel.x)/v_2)+1, (gamma-1)*((vel.y*vel.x)/v_2)  , (gamma-1)*((vel.z*vel.x)/v_2)   ),
                new Vector4D(-beta_y*gamma, (gamma-1)*((vel.x*vel.y)/v_2)  , (gamma-1)*((vel.y*vel.y)/v_2)+1, (gamma-1)*((vel.z*vel.y)/v_2)   ),
                new Vector4D(-beta_z*gamma, (gamma-1)*((vel.x*vel.z)/v_2)  , (gamma-1)*((vel.y*vel.z)/v_2)  , (gamma-1)*((vel.z*vel.z)/v_2)+1 ));

        }

        public Matrix44 RotMatrixR(Vector3D col1, Vector3D col2, Vector3D col3)
        {
            return new Matrix44(
                new Vector4D(1, 0, 0, 0),
                new Vector4D(0, col1.x, col1.y, col1.z),
                new Vector4D(0, col2.x, col2.y, col2.z),
                new Vector4D(0, col3.x, col3.y, col3.z));
        }
        
        // then proper time and lorentz covariant velocity

        // Returns the displacement D vector between current and previous position
        public Vector4D DisplacementDVector(Vector4D previous, Vector4D current)
        {
            return new Vector4D(
                current.t - previous.t,
                current.x - previous.x,
                current.y - previous.y,
                current.z - previous.z);
        }
        // Returns the proper time increased for a particle by an amount delta from the displacementDvector
        public double ProperTimeDelta(Vector4D DisplacementDVector)
        {
            double norm = LorentzianNorm(DisplacementDVector);
            return Math.Sqrt(-norm);
        }

        // Unsure whether this actually works since on paper it involves a derivative (eq. 41, sogebu et al)
        public Vector4D DVelocityVector(Vector4D DisplacementDVector, double properTimeDelta)
        {
            return new Vector4D(
                DisplacementDVector.t / properTimeDelta,
                DisplacementDVector.x / properTimeDelta,
                DisplacementDVector.y / properTimeDelta,
                DisplacementDVector.z / properTimeDelta);
        }

        public Vector4D AdvanceProperTimeStep(Vector4D current, Vector4D DisplacementDVector, double ProperTimeDelta)
        {
            return new Vector4D(
                current.t + DisplacementDVector.t * ProperTimeDelta,
                current.x + DisplacementDVector.x * ProperTimeDelta,
                current.y + DisplacementDVector.y * ProperTimeDelta,
                current.z + DisplacementDVector.y * ProperTimeDelta);
        }

        // not sure if this works since on paper it involves a derivative (eq. 53, sogebu et al)
        public Vector4D DAccelerationVector(Vector4D previous, Vector4D current, double properTimeDelta)
        {
            Vector4D displacementDVector = DisplacementDVector(previous, current);
            return new Vector4D(
                displacementDVector.t / properTimeDelta,
                displacementDVector.x / properTimeDelta,
                displacementDVector.y / properTimeDelta,
                displacementDVector.z / properTimeDelta);
        }
        
        public double LorentzGammaFactor(Vector4D vel)
        {
            double beta_x = vel.x / c_lightspeed;
            double beta_y = vel.y / c_lightspeed;
            double beta_z = vel.z / c_lightspeed;
            double v_2 = (beta_x * beta_x + beta_y * beta_y + beta_z * beta_z);
            double gamma = 1 / (Math.Sqrt(1 - v_2));
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