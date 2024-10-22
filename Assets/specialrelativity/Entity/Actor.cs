using System;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

namespace SpecialRelativity
{
    public class Actor : MonoBehaviour
    {
        [SerializeField]
        public float maxDist = 300.0f;
        //[SerializeField]
        Transform trans;
        Vector3D actorPosition; // actor position in the global world space of doubles
        Vector3D playerPosition = new Vector3D(0, 0, 0); // For testing it is assumed player is in origin

        [SerializeField]
        Vector3 testPos;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            this.trans = GetComponent<Transform>();
        }

        // Update is called once per frame
        void Update()
        {
            float x = testPos.x; float y = testPos.y; float z = testPos.z;
            trans.position = new Vector3(x, y, z);
            LockDist();
        }

        /// <summary>
        /// Calculate angular diameter as apparent for the player. diameter and distance from player expressed in meters
        /// </summary>
        /// <param name="actualDiameter"></param>
        /// <param name="distFromPlayer"></param>
        /// <returns>an angle in degrees</returns>
        public double CalculateAngularDiameter(double actualDiameter, double distFromPlayer)
        {
            return 2 * Math.Atan((actualDiameter) / (2 * distFromPlayer));
        }

        /// <summary>
        /// Calculate the projected angular diameter when object is locked at distance sphere. Angle is given in degrees, obtained by CalcuateAngularDiameter
        /// </summary>
        /// <param name="angle"></param>
        /// <returns>angular diameter in meters</returns>
        public double ProjectedAngularDiameter(double angle)
        {
            return 2 * this.maxDist * Math.Tan(angle);
        }

        void LockDist()
        {
            Vector3 pos = trans.position;
            float rho = SphericalCoordinates.Single.GetMagnitude(pos);
            if (rho > maxDist)
            {
                // sphCoords[0] = rho; sphCoords[1] = theta; sphCoords[2] = phi;
                float[] sphCoords = SphericalCoordinates.Single.ConvertRectToSpherical(pos);
                float r = maxDist;
                float x = r * Mathf.Cos(sphCoords[1]);
                float y = r * Mathf.Sin(sphCoords[1]);
                float z = rho * Mathf.Cos(sphCoords[2]);
                trans.position = new Vector3(x, y, z);
            }

        }

        float[] GetSphericalCoordinatesLocal(Vector3D actorPosition, Vector3D playerPosition)
        {
            Vector3D playerToOrigin = -playerPosition;
            Vector3D actorPositionTranslated = actorPosition - playerPosition;
            double theta = SphericalCoordinates.Double.GetTheta(actorPositionTranslated);
            double phi = SphericalCoordinates.Double.GetPhi(actorPositionTranslated);
            double rho = SphericalCoordinates.Double.GetSquareMagnitude(actorPositionTranslated);
            float[] res = new float[] {(float)rho, (float)theta, (float)phi};
            return res;
        }
        
        

    }

}
