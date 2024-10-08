using NUnit.Framework;
using UnityEngine;

namespace SpecialRelativity
{
    public class World
    {

        // !!! Note
        // Idea:
        // Have a staggered frame of reference (change spatial index coords to change star system, for example) 
        // One could even do smth like how minecraft nether corresponds to overworld
        // e.g. transform between double-based coordinate systems in a cosmic distance ladder type deal

        public class CoordinateSystems
        {
            public double local_subdiv_size = 9_999.0d;

            public double c = 299_792_458.0d; // meters per second

            public Vector3 ToLocalSpace_Unity(Vector3D v)
            {
                double l_x = (v.x * c) % local_subdiv_size;
                double l_y = (v.y * c) % local_subdiv_size;
                double l_z = (v.z * c) % local_subdiv_size;
                return new Vector3((float)l_x, (float)l_y, (float)l_z);
            }
            
        }
        

    }

}