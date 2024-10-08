using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SpecialRelativity
{
    /// Find out how to do this .. python or cython impl of cache to csharp
    /*public class Cache
    {
        public int ix;
        public double s;
        public Cache()
        {
            this.ix = 0;
            this.s = 0.0d;
        }

        public Cache(int ix)
        {
            this.ix = ix;
            this.s = 0.0d;
        }

        public Cache(int ix, double s)
        {
            this.ix = ix;
            this.s = s;
        }
    }*/

    /*
    !!!!!!!! OBS
    !!!!!!!! DUNNO WHAT'S UP WITH IX_MAP AND THE LAST FEW ALGOS
    !!!!!!!! GONNA NEED TO FIGURE OUT WHAT THE PYTHON CODE OF SOGEBU DOES
    !!!!!!!! CHECK WITH THE LITERATURE AS WELL
    !!!!!!!! THEN FIGURE OUT HOW TO IMPLEMENT THE SYSTEM IN CSHARP?
    update: I think it's mostly just a matter of every object having a worldline which is stored in its own context.
    When, say, "player" needs to calculate stuff on an object's worldline, the worldline itself is the list of 4-vectors
    */
    public class WorldLine
    {
        public int n;
        public List<Vector4D> line;
        public List<Quat> state;
        public Dictionary<long, double> ix_map; 
        public int last;

        public void Init(PhaseSpace P, Quat Q)
        {
            this.line[0] = P.X.Copy();
            this.state[0] = Q;
            this.ix_map = new Dictionary<long, double>();
            this.n = 1;
            this.last = -1;
        }

        public void SetId(long ix)
        {
            this.ix_map.Add(ix, 0.0d);
        }

        public void DelId(long ix)
        {
            if (this.ix_map.ContainsKey(ix))
            {
                ix_map.Remove(ix);
            }
        }

        public void Add(PhaseSpace P, Quat Q)
        {
            this.line.Append(P.X.Copy());
            this.state.Append(Q);
            this.n += n;
        }

        public void Cut()
        {
            int imin = 0;
            foreach (long i in ix_map.Keys)
            {
                if (i < imin)
                {
                    imin = (int)i;
                }
            }
            if (imin > 0)
            {
                this.line.RemoveRange(0, imin);
                this.state.RemoveRange(0, imin);
                this.n -= imin;
            }
        }

        public int SearchPositionOnPLC(Vector4D Xp, long ix)
        {
            double Xpt = Xp.t;
            //Dictionary<long, double>.KeyCollection keyColl = ix_map.Keys;
            int i = (int)ix;
            long start = ix_map.Keys.ElementAt(i);
            Vector4D X = new Vector4D();

            for (int j = (int)start; j >= this.n; j++)
            {
                X = this.line[j];
                if (X.t > Xpt || Xp.SquaredNormTo(X) > 0.0d)
                {
                    if (this.ix_map.Keys.ElementAt(j) < 1 )
                    {
                        this.ix_map[i] = 0;
                    }
                    else
                    {
                        i -= 1;
                    }
                    return i;
                }
            }
            return -1;
        }

        // Theories on what FP stands for:
        // Future Past
        // (F_P in equation 81, in sogebu et al) which is newtonian force
        // what about Xp_py? why that name... gotta grumble over it...
        /*public Vector4D GetX_FP(Vector4D Xp_py, double w = 0.5d)
        {
            long ix = 
        }*/
        
        /*
        get_XU_on_PLC()
        get_State_on_PLC()
        get_last()
        len()
          */

    }
}