using UnityEngine;
using System;

namespace SpecialRelativity.Entity
{
    public class Actor
    {
        
        public Vector3D actualPosition;
        public Vector3D playerPosition;
        public float maxDist;
        public double actualDiameter;

        public Actor(Vector3D actualPosition, Vector3D playerPosition, double actualDiameter, float maxDist = 300.0f)
        {
            this.actualPosition = actualPosition;
            this.playerPosition = playerPosition;
            this.actualDiameter = actualDiameter;
            this.maxDist = maxDist;
        }

        public float scalingFactor = 1.0f;


        private float[] GetLocalPositionOnSphere(Vector3D actorPosition, Vector3D playerPosition, float maxDist = 300.0f)
        {
            Vector3D translated = actorPosition - playerPosition;
            double[] sphCoords = SphericalCoordinates.Double.ConvertRectToSpherical(translated);
            return new[] { maxDist, (float)sphCoords[1], (float)sphCoords[2] };
        }

        private double GetDistBetweenActorAndPlayer(Vector3D actorPosition, Vector3D playerPosition)
        {
            Vector3D v = actorPosition  - playerPosition;
            return Math.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z);
        }


        private double CalculateAngularDiameter(Vector3D v, double actualDiameter)
        {
            double distance = Math.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z);
            return 2 * Math.Atan2(actualDiameter, 2 * distance);

        }
        private double CalculateScalingFactor(float maxDist, double angle, double actualDiameter)
        {
            double scaledAngularDiameter = (double)maxDist * Math.Tan(angle);
            double scalingFactor = scaledAngularDiameter / actualDiameter;
            if (scalingFactor > 1.0f)
            {
                scalingFactor = 1.0f;
            }
            return scalingFactor;
        }


    }

}
