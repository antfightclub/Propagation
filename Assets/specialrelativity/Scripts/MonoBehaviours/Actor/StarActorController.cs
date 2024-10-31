using SpecialRelativity.Entity;
using System.Collections.Generic;
using UnityEngine;

namespace SpecialRelativity
{
    public class StarActorController : MonoBehaviour
    {
        private static readonly HashSet<StarActorController> _instances = new HashSet<StarActorController>();
        public static HashSet<StarActorController> Instances => new HashSet<StarActorController>(_instances);

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

        private Star star;
        public Star Star
        {
            get { return star; }
            set { star = value; }
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
            star = new Star(new Vector4D(1.0d, posX, posY, posZ), radius);
            _instances.Add(this);
        }

        private void Awake()
        {

        }

        // Update is called once per frame
        void LateUpdate()
        {
            star.Position = new Vector4D(1.0d, posX, posY, posZ);
            Actor.UpdateLogic(Star.Position, PlayerController.Instance.Player.Position, Conversions.LightsecondsToMeters(2 * radius), MaxDist, out Vector3 drawnPos, out float diam, out double meterdist, out float[] sphcoords, out float[] sphCoordsObj);
            DrawQuad(material, drawnPos, 35f);
            //MoveTowards();
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(Vector3.zero, maxDist);
        }

        /*private void MoveTowards()
        {
            double accel = 0.02;
            Vector3D directionOfTravel = PlayerController.Instance.Player.Position.Component3D - star.Position.Component3D;
            directionOfTravel.Normalize();
            posX *= directionOfTravel.x * 1+accel * Time.deltaTime;
            posY *= directionOfTravel.y * 1+accel * Time.deltaTime;
            posZ *= directionOfTravel.z * 1+accel * Time.deltaTime;
        }*/

        private void OnDestroy()
        {
            _instances.Remove(this);
        }

        private void DrawQuad(Material material, Vector3 pos, float scale = 1.0f)
        {
            Quaternion rot = Quaternion.LookRotation(pos) * Quaternion.Euler(0, 0, 90);
            Matrix4x4 trans = Matrix4x4.TRS(pos, rot, new Vector3(scale, scale, scale));
            Graphics.DrawMesh(mesh, trans, material, 0);
        }

    }
}

