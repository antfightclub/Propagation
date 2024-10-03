using UnityEngine;

namespace SpecialRelativity
{
    public class Lorentz
    {
        Vector3 InnerProduct(Vector3 a, Vector3 b)
        {
            return new Vector3(
                a.x * b.x,
                a.y * b.y,
                a.z * b.z);
        }

        Vector3 LorentzianInnerProduct(Vector4 a, Vector4 b)
        {
            return new Vector4(
              -(a.w * b.w),
                a.x * b.x,
                a.y * b.y,
                a.z * b.z);
        }

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
        // then proper time and lorentz covariant velocity
        // then rest frame
        // then drawing the world
    
    }

}