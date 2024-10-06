using System.Collections.Generic;
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
        public List<Vector4D> line;
        public List<Vector4D> state;
        public Dictionary<int, int> ix_map;
        public int last;

        public void Init()
        {
            
        }

    }
}