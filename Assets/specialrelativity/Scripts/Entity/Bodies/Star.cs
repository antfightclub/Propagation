using UnityEngine;


namespace SpecialRelativity.Entity
{
    public class Star
    {
        private Vector4D _position;
        private double _radius;

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
        public double Radius
        {
            get
            {
                return _radius;
            }
            set
            {
                _radius = value;
            }
        }

        public Star(Vector4D position, double radius)
        {
            this.Position = position;
            this.Radius = radius;
        }

        public void GetRadiusAsMeters(out double radius)
        {
            radius = this.Radius * Constants.c;
        }
    }

}
