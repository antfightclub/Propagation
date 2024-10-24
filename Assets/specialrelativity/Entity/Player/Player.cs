using Unity.VisualScripting;
using UnityEngine;

namespace SpecialRelativity
{
    public class Player
    {
        World World;
        PhaseSpace P;
        Quat Quaternion;
        Vector4D Position;
        double Time;
        WorldLine Worldline;

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

        public Vector4D GetPosition()
        {
            return this.Position;
        }

        public void SetPosition(Vector4D position)
        {
            this.Position = position;
        }

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