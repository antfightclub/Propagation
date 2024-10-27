using System.Collections.Generic;
using UnityEngine;

namespace SpecialRelativity
{
    public class PlanetaryOrbitGenerator
    {
             
        // vector3D barycenter in light seconds
        // radius of orbit in light seconds
        // time to orbit in seconds
        public static List<Vector4D> GeneratePlanetaryOrbit(Vector3D barycenter, double radius, double timeToOrbit, double increment)
        {
            List<Vector4D> list = new List<Vector4D>();
            double count = timeToOrbit / increment;

            for (double angle = -Constants.PI; angle < Constants.PI; angle += increment)
            {
                Vector3D coord = SphericalCoordinates.Double.ConvertSphericalToRect(radius, angle, 0);
                //coord += barycenter;
                list.Add(new Vector4D(count, coord));
            }

            return list;
        }

    }
}

