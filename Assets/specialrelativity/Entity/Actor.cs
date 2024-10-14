using System;
using UnityEngine;

namespace SpecialRelativity
{
    public class Actor : MonoBehaviour
    {
        [SerializeField]
        public float maxDist = 3000.0f;

        Transform trans;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            this.trans = GetComponent<Transform>();
        }

        // Update is called once per frame
        void Update()
        {

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


        }

        Vector3 GetSphericalCoordinatesLocal(Vector3D actorPosition, Vector3D playerPosition)
        {
            Vector3D playerToOrigin = -playerPosition;
            Vector3D actorPositionTranslated = actorPosition - playerPosition;
            double theta = Math.Atan2(actorPositionTranslated.x, actorPositionTranslated.z) / Constants.PI;
            var xzLen = new Vector2((float)actorPositionTranslated.x, (float)actorPositionTranslated.z).magnitude;
            double phi = Math.Atan2(-actorPositionTranslated.y, xzLen);
            // Redo to use new spherical coordinate classes
            throw new NotImplementedException();
        }
        
        

    }

}
