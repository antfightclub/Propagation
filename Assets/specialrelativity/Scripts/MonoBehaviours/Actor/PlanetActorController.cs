using SpecialRelativity;
using SpecialRelativity.Entity;
using System.Collections.Generic;
using UnityEngine;

namespace SpecialRelativity
{
    public class PlanetActorController : MonoBehaviour
    {
        private static readonly HashSet<PlanetActorController> _instances = new HashSet<PlanetActorController>();
        public static HashSet<PlanetActorController> Instances => new HashSet<PlanetActorController>(_instances);
        
        [SerializeField] private Transform trans;
        public Transform Trans => trans;

        [SerializeField] private Mesh mesh;
        public Mesh Mesh => mesh;

        [SerializeField] private Material material;
        public Material Material => material;

        private RenderParams rp; 


        [SerializeField] private float maxDist = 300.0f;
        public float MaxDist
        {
            get { return maxDist; }
            set { maxDist = value; }
        }

        private Planet planet;
        public Planet Planet
        {
            get { return planet; }
            set { planet = value; }
        }

        public double radius;
        public double Radius
        {
            get { return radius; }
            set { radius = value; }
        }

        public double posX, posY, posZ;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if (!trans) trans = GetComponent<Transform>();
            planet = new Planet(new Vector4D(1.0d, posX, posY, posZ), radius);
            rp = new RenderParams(material);
            _instances.Add(this);
        }

        // Update is called once per frame
        void LateUpdate()
        {
            planet.Position = new Vector4D(1.0d, posX, posY, posZ);
            Actor.UpdateLogic(planet.Position, PlayerController.Instance.Player.Position, 2 * radius, MaxDist, out Vector3 drawnPos, out float diam, out double meterdist, out float[] sphCoords, out float[] sphCoordsObj);
            Debug.unityLogger.Log("The actual diameter of the object is " + Conversions.LightsecondsToMeters(2*radius) + " meters, and the distance between the player and the object's center is " + meterdist + " meters, and scaled diameter given as: " + diam + " when placed at: " + MaxDist + " meters from player");
            double H_wawa =  (meterdist - Conversions.LightsecondsToMeters(radius)) / Conversions.LightsecondsToMeters(radius);
            Debug.unityLogger.Log("With the actual radius in meters being " + Conversions.LightsecondsToMeters(radius) + " meters, the height above the surface of the object in terms of the object's radius is " + H_wawa);
            
           
            DrawSphere(material, drawnPos, diam, sphCoordsObj, (float)H_wawa);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(Vector3.zero, maxDist);
        }

        private void DrawSphere(Material material, Vector3 pos, float diam, float[] sph, float H)
        {
            // We should give:
            /*
             * P = H/R + 1 ; with R being radius (scale/2) of the sphere and H being height above the surface of the sphere
             * Phi1 and Lambda0 as spherical coordinates for the *center of the projection*
             * Phi and Lambda are spherical coordinates for the point (they will vary as we calculate all points)
             * all angles must be carefully given, signs and all, because otherwise I need to impl. arctangent2 stuff
             * in the shader graph.
             * */

            float R = diam / 2;
            float h = Mathf.Sqrt(H * H - R * R) * (R / H);
            float sinAngle = h / H;
            float cosAngle = Mathf.Sqrt(1.0f  - sinAngle * sinAngle);
            float tanAngle = sinAngle / cosAngle;
            float quadScale = tanAngle * 300f * 2f;
            rp.material.SetFloat("_R", R);
            rp.material.SetFloat("_Phi1", -sph[0]);
            rp.material.SetFloat("_Lambda0", -sph[1]);
            rp.material.SetFloat("_MaxDist", maxDist);
            rp.material.SetFloat("_H", H);
            float P = H / R + 1;
            rp.material.SetFloat("_P", P);
            float mapLim = R * Mathf.Sqrt((P-1)/(P+1));
            rp.material.SetFloat("_MapLim", mapLim);
            float shaderScaleFactor = (P / (P / 2)) * mapLim; // internally in shader the x and y position coords are scaled by this to keep the sphere inside the UV's
            //float h = Mathf.Sqrt(maxDist*maxDist - R*R) * (R/maxDist); // I hope this heuristic scales the quad accordingly. It looks better but I'm unsure!
            float z = Mathf.Sqrt(maxDist * maxDist - R * R) * (h / R);
            float Q = R * (h/z);
            Debug.unityLogger.Log("R = " + R + ", h = " + h + ", z = " + z + ", Q = " + Q + ", quadScale = " + quadScale);
            Quaternion rot = Quaternion.LookRotation(pos) * Quaternion.Euler(0, 0, 90);
            Matrix4x4 trans = Matrix4x4.TRS(pos, rot, new Vector3(diam, diam, diam));
            //Graphics.DrawMesh(mesh, trans, material, 0);
            Graphics.RenderMesh(rp, mesh, 0, trans);
        }

    }

}
