using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

namespace SpecialRelativity.Entity
{
    public class ActorController : MonoBehaviour
    {
        private Logger logger;

        private static readonly HashSet<ActorController> _instances = new HashSet<ActorController>();

        public static HashSet<ActorController> Instances => new HashSet<ActorController>(_instances);

        [SerializeField] private Transform trans;

        public Transform Trans => trans;

        private Vector4D pos = new Vector4D();

        //public Vector4D Position => pos;
        public Vector4D Pos
        {
            get { return pos; }
            set { pos = value; }
        }

        
        [SerializeField] private Mesh mesh;
        public Mesh Mesh => mesh;

        [SerializeField] private Material material;
        public Material Material => material;

        private Actor actor;
        public Actor Actor => actor;

        [SerializeField] private double actualDiameter = 10;
        public double ActualDiameter => actualDiameter;

        [SerializeField] private double actualDistance;
        public double ActualDistance => actualDistance;

        [SerializeField] private float maxDist = 300.0f;
        public float MaxDist => maxDist;

        private PlayerController playerController;
        private Player player;

        public float scaledDiameter;

        private Vector4D _playerPos;
        public Vector4D PlayerPos
        {
            get { return _playerPos; }
            set { _playerPos = value; }
        }


        private void Awake()
        {
            logger = new Logger(Debug.unityLogger.logHandler);
            if(!trans) trans = GetComponent<Transform>();
            //pos = new Vector4D(0, 0, 0, 0);
            //shit
            //playerController = PlayerController.Instance;
            //player = playerController.Player;

            //Console.WriteLine("player was null");

            PlayerPos = new Vector4D();
            
            actor = new Actor(Pos, PlayerPos, actualDiameter); 
            //Vector4D v = playerPos - pos;
            //actualDistance = Conversions.LightsecondsToMeters(Math.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z));

            //actor.actualPosition = pos;
            _instances.Add(this);
        }

        private void Update()
        {
            // Populate Update() method according to Actor_MonoBehaviour.

            //this.actor.playerPosition = player.Player.GetPosition();
            //this.actor.actualPosition = this.pos;
            //this.actor.actualDiameter = this.actualDiameter;
            
            Actor.UpdateLogic(Pos, PlayerController.Instance.Player.Position, actualDiameter, maxDist, out Vector3 drawnPos, out float diam, out double meterdist, out float[] sphCoords);
            logger.Log("drawnpos = " + drawnPos + " and  actual distance =" + meterdist + " and scaled diameter =" + diam);
            
            DrawMesh(material, drawnPos, diam);
            
            scaledDiameter = diam;
            actualDistance = meterdist;
            
            

        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(Vector3.zero, maxDist);
        }



        private void OnDestroy()
        {
            _instances.Remove(this);
        }

        private void DrawMesh(Material material, Vector3 pos, float scale = 1.0f)
        {
            if (scale <= 0.00001f) // egregious example of terrible code but im prototyping man
            {
                scale = 35f;
            }
            Quaternion rot = Quaternion.LookRotation(pos) * Quaternion.Euler(0, 0, 90);
            Matrix4x4 trans = Matrix4x4.TRS(pos, rot, new Vector3(scale, scale, scale));
            Graphics.DrawMesh(mesh, trans, material, 0);
        }

        public void SetPosition(Vector4D v)
        {
            this.pos = v;
        }
        public void SetDiameter(double diameter)
        {
            this.actualDiameter = diameter;
        }

        private void ActorUpdateLogic()
        {
            if(player != null)
            {

            }
        }

    }

}
