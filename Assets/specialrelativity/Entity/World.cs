using UnityEngine;

namespace SpecialRelativity
{
    public class World
    {

        // !!! Note
        // Idea:
        // Have a staggered frame of reference (change spatial index coords to change star system, for example) 
        // 
        public struct StaggeredReference
        {
            public Vector4D pos()
            {

            }

            public double t;
            public double x;
            public double y;
            public double z;

            public int spatialindex; 
            
            // OR have another Vector4D - this spatialindex acts sort of as the "nether", and the local coordinates (radius of the oort cloud) are sort of slid over the more general larger spanning coordinate space
            // This probably needs to be an established early design desicion

        }
        

    }

}