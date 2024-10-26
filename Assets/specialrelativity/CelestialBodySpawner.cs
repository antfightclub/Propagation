using UnityEngine;
using System;

namespace SpecialRelativity
{
    public class CelestialBodySpawner : MonoBehaviour
    {
        public GameObject entityToSpawn;
        public CelestialBodies spawnManagerValues;
        int instanceNumber = 1;

        System.Random rand = new System.Random();

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            SpawnEntities();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void SpawnEntities()
        {

            for (int i = 0; i < spawnManagerValues.numberOfBodiesToCreate; i++)
            {
                GameObject currentEntity = Instantiate(entityToSpawn, new Vector3(0, 0, 0), Quaternion.identity);
                StarActorController s = currentEntity.GetComponent<StarActorController>();
                s.posX = rand.NextDouble() * rand.Next(-100, 100);
                s.posY = rand.NextDouble() * rand.Next(-100, 100);
                s.posZ = rand.NextDouble() * rand.Next(-100, 100);
                s.Radius = rand.NextDouble();
                s.MaxDist = 300f;
            }
        }

    }

}
