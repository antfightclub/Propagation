using UnityEngine;
using SpecialRelativity;
using System.Collections.Generic;
using System;

public class planetaryorbitmb : MonoBehaviour
{
    public double baryX, baryY, baryZ;
    public double radius;
    public double timeToOrbit;
    public double increment;
    public GameObject entityToSpawn;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector3D bary = new Vector3D(baryX, baryY, baryZ);
        List<Vector4D> list = PlanetaryOrbitGenerator.GeneratePlanetaryOrbit(bary, radius, timeToOrbit, increment);
        for (int i = 0; i < list.Count; i++)
        {
            string m = "time: " + list[i].t + ", posx: " + list[i].x + ", posy: " + list[i].y + ", posz: " + list[i].z;
            Debug.unityLogger.Log(m);
        }
        SpawnEntities(list);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnEntities(List<Vector4D> list)
    {

        for (int i = 0; i < list.Count; i++)
        {
            GameObject currentEntity = Instantiate(entityToSpawn, new Vector3(0, 0, 0), Quaternion.identity);
            StarActorController s = currentEntity.GetComponent<StarActorController>();
            s.posX = list[i].x;
            s.posY = list[i].y;
            s.posZ = list[i].z;
            s.Radius = 1.0d;
            s.MaxDist = 300f;
        }
        entityToSpawn.SetActive(false);
    }


}
