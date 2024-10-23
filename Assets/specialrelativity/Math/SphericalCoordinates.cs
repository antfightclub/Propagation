using UnityEngine;
using System;
using Unity.VisualScripting;
using Palmmedia.ReportGenerator.Core.CodeAnalysis;

namespace SpecialRelativity
{
    public class SphericalCoordinates
    {
        public class Single
        {
            public static float GetTheta(Vector3 v)
            {
                //return Mathf.Atan2(v.x, v.y) / (float)Constants.PI;
                return Mathf.Atan2(Mathf.Sqrt(v.x * v.x + v.y * v.y), v.z);
            }

            public static float GetPhi(Vector3 v)
            {
                //var xzLen = new Vector2((float)v.x, (float)v.z).magnitude;
                //return Mathf.Atan2(-v.y, xzLen);
                return Mathf.Atan2(v.y, v.x);
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
                return rho * Mathf.Cos(phi);                
            }

            public static float[] ConvertRectToSpherical(Vector3 v)
            {
                float rho = GetMagnitude(v);
                float theta = GetTheta(v);
                float phi = GetPhi(v);
                return new[] { rho, theta, phi };
            }

            public static Vector3 ConvertSphericalToRect(float rho, float theta, float phi)
            {
                //float r = GetR(rho, phi);
                float x = rho * Mathf.Sin(theta) * Mathf.Cos(phi);
                float y = rho * Mathf.Cos(theta);
                float z = rho * Mathf.Sin(theta) * Mathf.Sin(phi);
                return new Vector3(x, y, z);
            }


        }

        public class Double
        {
            public static double GetTheta(Vector3D v)
            {
                //return Math.Atan2(v.z, v.y) / Constants.PI; //originally atan2(v.x, v.z)
                return Math.Atan2(Math.Sqrt(v.x*v.x + v.y*v.y), v.z);
            }

            public static double GetPhi(Vector3D v)
            {
                //var xzLen = new Vector2((float)v.x, (float)v.z).magnitude;
                //return Math.Atan2(-v.y, xzLen);
                return Math.Atan2(v.y, v.x);
            }

            public static double GetMagnitude(Vector3D v)
            {
                return Math.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z);
            }

            public static double GetSquareMagnitude(Vector3D v)
            {
                return v.x * v.x + v.y * v.y + v.z * v.z;
            }

            public static double GetR(double rho, double phi)
            {
                return rho * Math.Cos(phi);
            }

            public static double[] ConvertRectToSpherical(Vector3D v)
            {
                double rho = GetMagnitude(v);
                double theta = GetTheta(v);
                double phi = GetPhi(v);
                return new[] { rho, theta, phi };
            }

            public static Vector3D ConvertSphericalToRect(double rho, double theta, double phi)
            {
                //double r = GetR(rho, phi);
                // swapped y and z for it to make sense with Unity's stuff, although it's still right-handed instead of unity's lefthanded.
                double x = rho * Math.Sin(theta) * Math.Cos(phi);
                double y = rho * Math.Cos(theta);
                double z = rho * Math.Sin(phi) * Math.Sin(phi);
                return new Vector3D(x, y, z);
            }

        }


    }
}