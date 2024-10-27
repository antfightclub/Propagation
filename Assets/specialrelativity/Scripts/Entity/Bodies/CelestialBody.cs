using UnityEngine;
namespace SpecialRelativity
{
    public class CelestialBody
    {
        private Vector4D _position;
        private double _radius;
        // MORE THINGS!!! because it will actually have a worldline...
        // I may need to make a "cyclical worldline" data structure as well
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
        public CelestialBody(Vector4D position, double radius)
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
