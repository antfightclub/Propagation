using System;
using UnityEngine;

/*
 * OKAY; so this is a bit screwy!
 * Gonna need to define a way to change between "near" and "far" actors. 
 * Stars are usually gonna be mostly really far away. Planets are mostly gonna be really far away, but they're gonna need some special arcsine treatment when you get closer.
 * If I want to land on planets I'll have to be able to switch context entirely.
 * If in deep space and nearing a spaceship sized object, I'll have to be able to transition between near and far. Probably there's gonna be a range in which objects are detectable
 * depending on how bright it is and how far away it is. It's gonna be a dot until it gets close enough to be resolved. Then it's gotta grow in size, until it's within 3km
 * at which point it is allowed to pass closer. 
 * Another point is that all of the actor contexts probably need to be outside of monobehaviors; I only need an MB to actually draw objects. Potentially I could have a
 * single empty with a monobehavior that does Graphics.DrawMesh(); calls when passed a list of all objects to be drawn and where. 
 * We have got to pass this list **AFTER** we have performed the foliation of the past-light cone.
 * Gonna need to design a bit I think, but I have some basic stuff up and running!
 * 
 * But yea it a bit messy owo
 */

namespace SpecialRelativity
{
    public class Actor : MonoBehaviour
    {
        public Mesh mesh;
        public Material material;
        public Vector3 testPos;

        public float maxDist = 300.0f;

        Vector3D actorPosition = new Vector3D(0.0d, 0.0d, Conversions.MetersToLightseconds(384_400_000.0d)); // actor position in the global world space of doubles
        Vector3D playerPosition = new Vector3D(0.0d, 0.0d, Conversions.MetersToLightseconds(5000.0d)); 
        public float actualDiameter = 3_474_000.0f;


        //public float actualDiameter = 10.0f;
        public float scalingFactor = 1.0f;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            // Here is what should be used when stuff is really really far away
            float[] localSphCoords = GetLocalPositionOnSphere(actorPosition, playerPosition);
            Vector3 drawnPos = SphericalCoordinates.Single.ConvertSphericalToRect(maxDist, localSphCoords[1], localSphCoords[2]);
            double lsdist = GetDistBetweenActorAndPlayer(actorPosition, playerPosition);
            double meterdist = Conversions.LightsecondsToMeters(lsdist);
            float angle = 2 * Mathf.Atan2(actualDiameter, 2*(float)meterdist);
            scalingFactor = CalculateScalingFactor(maxDist, angle, actualDiameter);
            DrawMesh(material, drawnPos, actualDiameter * scalingFactor);

            // Below is what should be used when stuff is "near" (between 0 meters and a few thousand ish meters)
            /*float x = testPos.x; float y = testPos.y; float z = testPos.z;
            if (testPos.magnitude < maxDist)
            {
                Vector3 drawnPos = new Vector3(x, y, z);
                DrawMesh(material, drawnPos, actualDiameter);
            }*
            else if (testPos.magnitude > maxDist)
            {
                float[] sphCoords = SphericalCoordinates.Single.ConvertRectToSpherical(new Vector3(x, y, z));
                Vector3 drawnPos = SphericalCoordinates.Single.ConvertSphericalToRect(maxDist, sphCoords[1], sphCoords[2]);
                
                float angle = CalculateAngularDiameter(testPos, actualDiameter);
                scalingFactor = CalculateScalingFactor(maxDist, angle, actualDiameter);
                DrawMesh(material, drawnPos, actualDiameter * scalingFactor);
            }*/
            
            //LockDist();
            
        }

        // Assumed that the distance is NONLOCAL, as in not anywhere near local space
        private float[] GetLocalPositionOnSphere(Vector3D actorPosition, Vector3D playerPosition)
        {
            Vector3D translated = actorPosition - playerPosition;
            double[] sphCoords = SphericalCoordinates.Double.ConvertRectToSpherical(translated);
            return new[] { maxDist, (float)sphCoords[1], (float)sphCoords[2] };

        }

        private double GetDistBetweenActorAndPlayer(Vector3D actorPosition, Vector3D playerPosition)
        {
            Vector3D v = actorPosition - playerPosition;
            return Math.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z);
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

        /*void LockDist()
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
            double rho = SphericalCoordinates.Double.GetMagnitude(actorPositionTranslated);
            float[] res = new float[] {(float)rho, (float)theta, (float)phi};
            return res;
        }*/

        
        
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
