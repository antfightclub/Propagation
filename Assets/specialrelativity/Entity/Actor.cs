using System;
using UnityEngine;


namespace SpecialRelativity
{
    public class Actor : MonoBehaviour
    {
        public Mesh mesh;
        public Material material;

        [SerializeField]
        public float maxDist = 300.0f;
        //[SerializeField]
        Transform trans;
        Vector3D actorPosition; // actor position in the global world space of doubles
        Vector3D playerPosition = new Vector3D(0, 0, 0); // For testing it is assumed player is in origin

        [SerializeField]
        Vector3 testPos;
        public float actualDiameter = 10.0f;
        public float scalingFactor = 1.0f;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            this.trans = GetComponent<Transform>();
        }

        // Update is called once per frame
        void Update()
        {
            float x = testPos.x; float y = testPos.y; float z = testPos.z;
            //trans.position = new Vector3(x, y, z);
            //float[] sphCoords = SphericalCoordinates.Single.ConvertRectToSpherical(testPos);
            //Vector3 drawnPos = SphericalCoordinates.Single.ConvertSphericalToRect(maxDist, sphCoords[1], sphCoords[2]);
            //float angle = CalculateAngularDiameter(testPos, actualDiameter);
            //scalingFactor = CalculateScalingFactor(maxDist, angle, actualDiameter);
            if (testPos.magnitude < maxDist)
            {
                Vector3 drawnPos = new Vector3(x, y, z);
                DrawMesh(material, drawnPos, actualDiameter);
            }
            else if (testPos.magnitude > maxDist)
            {
                float[] sphCoords = SphericalCoordinates.Single.ConvertRectToSpherical(new Vector3(x, y, z));
                Vector3 drawnPos = SphericalCoordinates.Single.ConvertSphericalToRect(maxDist, sphCoords[1], sphCoords[2]);
                
                float angle = CalculateAngularDiameter(testPos, actualDiameter);
                scalingFactor = CalculateScalingFactor(maxDist, angle, actualDiameter);
                DrawMesh(material, drawnPos, actualDiameter * scalingFactor);
            }
            
            //LockDist();
            
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
                float radius = maxDist;
                Vector3 newPos = SphericalCoordinates.Single.ConvertSphericalToRect(radius, sphCoords[1], sphCoords[2]);
                trans.position = newPos;
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
        
        private float CalculateAngularDiameter(Vector3 v, float actualDiameter)
        {
            float distance = Mathf.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z);
            return 2 * Mathf.Atan2(actualDiameter, 2 * distance);
        }

        private float CalculateScalingFactor(float radius, float angle, float actualDiameter)
        {
            float scaledDiameter = radius * Mathf.Tan(angle);
            float scalingFactor = scaledDiameter / actualDiameter;
            if (scalingFactor > 1.0f)
            {
                scalingFactor = 1.0f;
            }
            return scalingFactor;
        }

        private void DrawMesh(Material material, Vector3 pos, float scale = 1.0f)
        {
            Matrix4x4 trans = Matrix4x4.TRS(pos, Quaternion.identity, new Vector3(scale, scale, scale));
            Graphics.DrawMesh(mesh, trans, material, 0);
        }

    }

}
