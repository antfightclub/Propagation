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
                float r = GetR(rho, phi);
                float x = r * Mathf.Cos(theta);
                float y = r * Mathf.Sin(theta);
                float z = rho * Mathf.Cos(phi);
                return new Vector3(x, y, z);
            }


        }

        public class Double
        {
            public struct coords_double
            {
                double rho;
                double theta;
                double phi;
            }

            public static double GetTheta(Vector3D v)
            {
                return Math.Atan2(v.z, v.y) / Constants.PI; //originally atan2(v.x, v.z)
            }

            public static double GetPhi(Vector3D v)
            {
                var xzLen = new Vector2((float)v.x, (float)v.z).magnitude;
                return Math.Atan2(-v.y, xzLen);
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
                double r = GetR(rho, phi);
                double x = r*Math.Cos(theta);
                double y = r*Math.Sin(theta);
                double z = rho * Math.Cos(phi);
                return new Vector3D(x, y, z);
            }

        }


    }
}