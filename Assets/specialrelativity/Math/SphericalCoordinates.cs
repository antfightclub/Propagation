using UnityEngine;
using System;

namespace SpecialRelativity
{
    public class SphericalCoordinates
    {
        public class SphericalCoordinatesSingle
        {
            public struct coords_single
            {
                float rho;
                float theta;
                float phi;
            }

            public static float GetTheta(Vector3 v)
            {
                return Mathf.Atan2(v.x, v.z) / (float)Constants.PI;
            }

            public static float GetPhi(Vector3 v)
            {
                var xzLen = new Vector2((float)v.x, (float)v.z).magnitude;
                return Mathf.Atan2(-v.y, xzLen);
            }

        }

        public class SphericalCoordinatesDouble
        {
            public struct coords_double
            {
                double rho;
                double theta;
                double phi;
            }

            public static double GetTheta(Vector3D v)
            {
                return Math.Atan2(v.x, v.z) / Constants.PI;
            }

            public static double GetPhi(Vector3D v)
            {
                var xzLen = new Vector2((float)v.x, (float)v.z).magnitude;
                return Math.Atan2(-v.y, xzLen);
            }


        }


    }
}