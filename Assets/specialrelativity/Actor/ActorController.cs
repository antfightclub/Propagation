using System.Collections.Generic;
using UnityEngine;

namespace SpecialRelativity.Entity
{
    public class ActorController : MonoBehaviour
    {
        private static readonly HashSet<ActorController> _instances = new HashSet<ActorController>();

        public static HashSet<ActorController> Instances => new HashSet<ActorController>(_instances);

        [SerializeField] private Transform trans;

        public Transform Trans => trans;

        private Vector4D pos;

        public Vector4D Position => pos;

        [SerializeField] private Mesh mesh;
        public Mesh Mesh => mesh;

        [SerializeField] private Material material;
        public Material Material => material;

        private Actor actor;
        public Actor Actor => actor;

        [SerializeField] private double actualDiameter;
        public double ActualDiameter => actualDiameter;

        [SerializeField] private float maxDist = 300.0f;
        public float MaxDist => maxDist;

        


        private void Awake()
        {
            if(!trans) trans = GetComponent<Transform>();
            pos = new Vector4D(0, 0, 0, 0);
            _instances.Add(this);
        }

        private void Update()
        {
            // Populate Update() method according to Actor_MonoBehaviour.
        }



        private void OnDestroy()
        {
            _instances.Remove(this);
        }

        private void DrawMesh(Material material, Vector3 pos, float scale = 1.0f)
        {
            Matrix4x4 trans = Matrix4x4.TRS(pos, Quaternion.identity, new Vector3(scale, scale, scale));
            Graphics.DrawMesh(mesh, trans, material, 0);
        }

    }

}
