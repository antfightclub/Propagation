using NUnit.Framework;
using UnityEngine;

namespace propagation
{
    public class WorldGen : MonoBehaviour
    {

        public GameObject prefab;

        [SerializeField]
        int amount = 128;

        [SerializeField]
        float maxRange = 100.0f;

        [SerializeField]
        float minRange = -100.0f;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            var positions = StarGenerator.GenerateStars(amount, minRange, maxRange);
            for (int i = 0; i < amount; i++)
            {
                Vector3 pos = positions[i];
                Instantiate(prefab, pos, Quaternion.identity);
            }
        }


        // Update is called once per frame
        void Update()
        {

        }
    }

}