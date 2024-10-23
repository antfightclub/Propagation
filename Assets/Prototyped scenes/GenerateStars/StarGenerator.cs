using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;


namespace propagation
{
    public class StarGenerator
    {
        static List<Vector3> positions = new List<Vector3>();


        public static List<Vector3> GenerateStars(int amt, float minRange, float maxRange)
        {
            positions.Clear();

            for (int i = 0; i < amt; i++)
            {
                var pos = new Vector3(Random.Range(minRange, maxRange), Random.Range(minRange, maxRange), Random.Range(minRange, maxRange));
                positions.Add(pos);
            }

            return positions;
        }
    }
}
