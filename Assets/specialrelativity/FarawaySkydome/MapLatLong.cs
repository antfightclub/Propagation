using UnityEngine;
using System;


namespace SpecialRelativity
{
    public class MapLatLong
    {
        public Vector4D BigXi;
        public double BigTheta;
        public double BigPhi;

        public Vector4D SmallXi;
        public double SmallTheta;
        public double SmallPhi;

        public float S;
        public float T;

        public Vector2 TextureCoordinate;
        
        // small theta
        // TODO: DOES NOT CHECK FOR QUADRANT. WONDERING IF IT IS A PROBLEM.
        public static double GetZenithAngle(Vector4D xi) // xi referring to the small greek letter
        {
            return Math.Atan2(xi.x, xi.z);
        }

        // small phi
        // TODO: DOES NOT CHECK FOR QUADRANT. WONDERING IF IT IS A PROBLEM.
        public static double GetAzimuthAngle(Vector4D xi) // xi referring to the small greek letter
        {
            var xzLen = new Vector2((float)xi.x, (float)xi.z).magnitude;
            return Math.Atan2(-xi.y, xzLen);
        }

        public Vector4D SmallXiFromAnglesAndDist(double r, double theta, double phi)
        {
            return new Vector4D(
                -r,
                r * Math.Sin(theta) * Math.Cos(phi),
                r * Math.Sin(theta) * Math.Sin(phi),
                r * Math.Cos(theta));
        }
        
        public Vector4D BigXiFromDirectionAndDist(double R, double THETA, double PHI)
        {
            return new Vector4D(
            -R,
                R * Math.Sin(THETA) * Math.Cos(PHI),
                R * Math.Sin(THETA) * Math.Sin(PHI),
                R * Math.Cos(THETA));
        }

        public static float CoordSFromPhi(double phi)
        {
            float S = (float)phi / 2 * (float)Constants.PI;
            return S;
        }

        public static float CoordTFromTheta(double theta)
        {
            float T = 1 - ((float)theta / (float)Constants.PI);
            return T;
        }

        public static Vector2 GetST(Vector4D PlayerPos, Vector4D ObjectPos)
        {
            double theta = GetZenithAngle(ObjectPos);
            double phi = GetAzimuthAngle(ObjectPos);
            //Vector4D SmallXi = SmallXiFromAnglesAndDist(r, theta, phi);
            float S = CoordSFromPhi(phi);
            float T = CoordTFromTheta(theta);
            return new Vector2(S, T);
        }
        

        

    }

}
