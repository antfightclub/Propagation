using SpecialRelativity.Entity;
using System.Collections.Generic;
using UnityEngine;

namespace SpecialRelativity
{
    public class StarController : MonoBehaviour
    {
        private static readonly HashSet<StarController> _instances = new HashSet<StarController>();
        public static HashSet<StarController> Instances => new HashSet<StarController>(_instances);

        [SerializeField] private Transform trans;
        public Transform Trans => trans;

        private Star star;
        public Star Star => star;

        public double radius;
        public double posX, posY, posZ;

        private PlayerController player;

        private ActorController actorController;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if (!trans) trans = GetComponent<Transform>();
            player = FindFirstObjectByType(typeof(PlayerController)) as PlayerController;
            star = new Star(new Vector4D(1.0d, posX, posY, posZ), radius);
            actorController = GetComponent<ActorController>();
            actorController.SetPosition(star.Position);
            star.GetRadiusAsMeters(out double rad);
            actorController.SetDiameter(rad * 2);
            _instances.Add(this);
        }

        private void Awake()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Vector4D pos = new Vector4D(1.0d, posX, posY, posZ);
            Star.Position = pos;
            actorController.Pos = pos;
        }



        private void OnDestroy()
        {
            _instances.Remove(this);
        }
    }
}

