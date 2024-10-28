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
            _instances.Add(this);
        }

        // Update is called once per frame
        void LateUpdate()
        {
            planet.Position = new Vector4D(1.0d, posX, posY, posZ);
            Actor.UpdateLogic(planet.Position, PlayerController.Instance.Player.Position, 2 * radius, MaxDist, out Vector3 drawnPos, out float diam, out double meterdist, out float[] sphCoords);
            Debug.unityLogger.Log("The actual diameter of the object is " + Conversions.LightsecondsToMeters(2*radius) + " meters, and scaled diameter given as: " + diam + " when placed at: " + MaxDist + " meters from player");
            DrawSphere(material, drawnPos, diam);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(Vector3.zero, maxDist);
        }

        private void DrawSphere(Material material, Vector3 pos, float scale = 1.0f)
        {
            RenderParams rp = new RenderParams(material);
            rp.material.SetFloat("ApparentSize", scale);
            rp.material.SetVector("SphereCenter", pos);
            rp.material.SetVector("RayOrigin", Camera.main.transform.position);
            rp.material.SetFloat("SphereRadius", scale);
            Quaternion rot = Quaternion.LookRotation(pos) * Quaternion.Euler(0, 0, 90);
            Matrix4x4 trans = Matrix4x4.TRS(pos, rot, new Vector3(scale, scale, scale));
            //Graphics.DrawMesh(mesh, trans, material, 0);
            Graphics.RenderMesh(rp, mesh, 0, trans);
        }

    }

}
