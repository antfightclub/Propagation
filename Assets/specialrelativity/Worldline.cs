using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace SpecialRelativity
{
    public class Cache
    {
        public int ix;
        public double s;
        void Init()
        {
            this.ix = 0;
            this.s = 0.0;
        }
    }
    public class Worldline
    {
        public int n;
        public List<Vector4> line;
        public List<Vector4> state;
        public Dictionary<int, int> ix_map;
        public int last;

        public void Init()
        {
            
        }

    }
}