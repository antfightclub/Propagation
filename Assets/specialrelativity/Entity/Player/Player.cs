using Unity.VisualScripting;
using UnityEngine;

namespace SpecialRelativity
{
    public class Player
    {
        private World _world;
        private PhaseSpace _p;
        private Quat _quaternion;
        private Vector4D _position;
        private double _time;
        private WorldLine _worldline;

        public World World
        {
            get
            {
                return _world;
            }
            set
            {
                _world = value;
            }
        }
        public PhaseSpace P
        {
            get
            {
                return _p;
            }
            set
            {
                _p = value;
            }
        }
        public Quat Quaternion
        {
            get
            {
                return _quaternion;
            }
            set
            {
                _quaternion = value;
            }
        }
        public Vector4D Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
            }
        }
        public double Time
        {
            get { return _time; }
            set { _time = value; }
        }
        public WorldLine Worldline
        {
            get { return _worldline;}
            set { _worldline = value;}
        }

        public Player(World world, Vector4D position)
        {
            this.World = world;
            this.P = new PhaseSpace(position, new Vector4D(1.0d, 0.0d, 0.0d, 0.0d));
            this.Quaternion = new Quat();
            this.Time = 0.0d;
            PhaseSpace start = this.P.Copy();
            start.X.t = 0.0d;
            //this.Worldline = WorldLine(start);
            this.Worldline.Add(this.P, this.Quaternion);
        }

        public Player(Vector4D position)
        {
            this.Position = position;
        }

        /*public Vector4D GetPosition()
        {
            return this.Position;
        }*/
        

        /*public void SetPosition(Vector4D position)
        {
            this.Position = position;
        }*/


        // commented out for now 
        /*void Init(World world, Vector4D pos)
        {
            this.World = world;
            this.P = new PhaseSpace(pos, new Vector4D(1.0d, 0.0d, 0.0d, 0.0d));
            this.Quaternion = new Quat();
            this.Time = 0.0d;
            PhaseSpace start = this.P.Copy();
            start.X.t = 0.0d;
            this.Worldline = WorldLine(start);
           this.Worldline.Add(this.P);
        }*/


    }
}