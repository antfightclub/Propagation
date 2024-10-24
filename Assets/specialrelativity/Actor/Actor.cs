using UnityEngine;
using System;

namespace SpecialRelativity.Entity
{
    public class Actor
    {
        
        public Vector4D actualPosition;
        public Vector4D playerPosition;
        public float maxDist;
        public double actualDiameter;

        public Actor(Vector4D actualPosition, Vector4D playerPosition, double actualDiameter, float maxDist = 300.0f)
        {
            this.actualPosition = actualPosition;
            this.playerPosition = playerPosition;
            this.actualDiameter = actualDiameter;
            this.maxDist = maxDist;
        }

        public float scalingFactor = 1.0f;


        private static float[] GetLocalPositionOnSphere(Vector3D actorPosition, Vector3D playerPosition, float maxDist = 300.0f)
        {
            Vector3D translated = actorPosition - playerPosition;
            double[] sphCoords = SphericalCoordinates.Double.ConvertRectToSpherical(translated);
            return new[] { maxDist, (float)sphCoords[1], (float)sphCoords[2] };
        }

        private static double GetDistBetweenActorAndPlayer(Vector3D actorPosition, Vector3D playerPosition)
        {
            Vector3D v = actorPosition  - playerPosition;
            return Math.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z);
        }


        private static double CalculateAngularDiameter(Vector3D v, double actualDiameter)
        {
            double distance = Math.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z);
            return 2 * Math.Atan2(actualDiameter, 2 * distance);

        }
        private static double CalculateScalingFactor(float maxDist, double angle, double actualDiameter)
        {
            double scaledAngularDiameter = (double)maxDist * Math.Tan(angle);
            double scalingFactor = scaledAngularDiameter / actualDiameter;
            if (scalingFactor > 1.0f)
            {
                scalingFactor = 1.0f;
            }
            return scalingFactor;
        }

        public static void UpdateLogic(Vector4D actorPos, Vector4D playerPos, double actualDiameter, float maxDist, out Vector3 drawnPos, out float diam, out double meterdist)
        {
            Vector3D aPos = new Vector3D(actorPos.x, actorPos.y, actorPos.z);
            Vector3D pPos = new Vector3D(playerPos.x, playerPos.y, playerPos.z);
            float[] localSphCoords = GetLocalPositionOnSphere(aPos, pPos);
            drawnPos = SphericalCoordinates.Single.ConvertSphericalToRect(maxDist, localSphCoords[1], localSphCoords[2]);
            double lsdist = GetDistBetweenActorAndPlayer(aPos, pPos);
            meterdist = Conversions.LightsecondsToMeters(lsdist);
            double meterDiameter = Conversions.LightsecondsToMeters(actualDiameter);
            double angle = 2 * Math.Atan2(meterDiameter, 2 * meterdist);
            double scaleFactor = CalculateScalingFactor(maxDist, angle, meterDiameter);
            double doublediameter = scaleFactor * actualDiameter;
            diam = (float)doublediameter;
        }


    }

}
