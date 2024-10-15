using UnityEngine;
using System;
using Unity.VisualScripting;
using Palmmedia.ReportGenerator.Core.CodeAnalysis;

namespace SpecialRelativity
{
    public class SphericalCoordinates
    {
        public class SphericalCoordsSingle
        {
            float rho;
            float theta;
            float phi;

            public static float GetTheta(Vector3 v)
            {
                return Mathf.Atan2(v.x, v.z) / (float)Constants.PI;
            }

            public static float GetPhi(Vector3 v)
            {
                var xzLen = new Vector2((float)v.x, (float)v.z).magnitude;
                return Mathf.Atan2(-v.y, xzLen);
            }

            public static float GetMagnitude(Vector3 v)
            {
                return Mathf.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z);
            }

            public static float GetSquareMagnitude(Vector3 v)
            {
                return v.x * v.x + v.y * v.y + v.z * v.z;
            }

            public static float GetR(float rho, float phi)
            {
                return rho * Mathf.Sin(phi);                
            }

            public static float ConvertRectToSpherical(Vector3 v)
            {
                throw new NotImplementedException();
            }

            public static float ConvertSphericalToRect(float rho, float theta, float phi)
            {
                throw new NotImplementedException();
            }


        }

        public class SphericalCoordsDouble
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