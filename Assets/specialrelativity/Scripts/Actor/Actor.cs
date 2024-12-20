using UnityEngine;
using System;

namespace SpecialRelativity.Entity
{
    public class Actor
    {  
        private Vector4D _actualPosition;
        private Vector4D _playerPosition;
        private float _maxDist;
        private double _actualDiameter;
        private float _scalingFactor = 1.0f;

        public Vector4D ActualPosition
        {
            get { return _actualPosition; }
            set { _actualPosition = value; }
        }
        public Vector4D PlayerPosition
        {
            get { return _playerPosition; }
            set { _playerPosition = value; }
        }
        public float MaxDist
        {
            get { return _maxDist; }
            set { _maxDist = value; }
        }
        public double ActualDiameter
        {
            get
            {
                return _actualDiameter;
            }
            set
            {
                _actualDiameter = value;
            }
        }
        public float ScalingFactor
        {
            get
            {
                return _scalingFactor;
            }
            set
            {
                _scalingFactor = value;
            }
        }

        public Actor(Vector4D actualPosition, Vector4D playerPosition, double actualDiameter, float maxDist = 300.0f)
        {
            ActualPosition = actualPosition;
            PlayerPosition = playerPosition;
            ActualDiameter = actualDiameter;
            MaxDist = maxDist;
        }


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

        public static void UpdateLogic(Vector4D actorPos, Vector4D playerPos, double actualDiameter, float maxDist, out Vector3 drawnPos, out float diam, out double meterdist, out float[] sphCoords, out float[] sphCoordsObj)
        {
            Vector3D aPos = actorPos.Component3D;
            Vector3D pPos = playerPos.Component3D;
            sphCoords = GetLocalPositionOnSphere(aPos, pPos);
            drawnPos = SphericalCoordinates.Single.ConvertSphericalToRect(maxDist, sphCoords[1], sphCoords[2]);
            double lsdist = GetDistBetweenActorAndPlayer(aPos, pPos);
            meterdist = Conversions.LightsecondsToMeters(lsdist);
            double meterDiameter = Conversions.LightsecondsToMeters(actualDiameter);
            //double angle = 2 * Math.Atan2(meterDiameter, 2 * meterdist);
            //double angle = meterDiameter / meterdist;
            //double angle = 2 * Math.Asin(meterDiameter / (2 * meterdist));
            double surfaceDist = meterdist - (meterDiameter / 2);
            double angle = 2 * Math.Acos(Math.Sqrt(surfaceDist*surfaceDist + meterDiameter*surfaceDist)/(meterDiameter/2 + surfaceDist));
            double scaleFactor = CalculateScalingFactor(maxDist, angle, meterDiameter);
            double doublediameter = scaleFactor * meterDiameter;
            diam = (float)doublediameter;
            sphCoordsObj = new float[2];
            Vector3D translatedInv = playerPos.Component3D - actorPos.Component3D;
            double[] _ = SphericalCoordinates.Double.ConvertRectToSpherical(translatedInv);
            sphCoordsObj[0] = (float)_[1];
            sphCoordsObj[1] = (float)_[2];

            /*Debug.unityLogger.Log(
                "meter diameter: " + meterDiameter + 
                " meters, float scaled diameter: " + diam + 
                " meters, double scaled diameter: " + doublediameter + 
                " meters, scalefactor: " + scaleFactor + 
                ", angle: " + angle +
                "radians, lsdist: " + lsdist +
                "light seconds, meterdist: " + meterdist);*/
        }


    }

}
