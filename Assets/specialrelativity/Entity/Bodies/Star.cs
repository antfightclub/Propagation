using UnityEngine;


namespace SpecialRelativity.Entity
{
    public class Star
    {
        private Vector4D position;
        private double radius;

        public Vector4D Position => position;
        public double Radius => radius;

        public Star(Vector4D position, double radius)
        {
            this.position = position;
            this.radius = radius;
        }

        public void GetRadiusAsMeters(out double radius)
        {
            radius = this.radius * Constants.c;
        }
    }

}
