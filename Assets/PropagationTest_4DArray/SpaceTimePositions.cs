using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class SpaceTimePositions : MonoBehaviour
{
    
    List<Vector4> spaceTimeArray = new List<Vector4>();
    Rigidbody rb;
    float timeSinceStart;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //spaceTimeArray.Capacity = 10000;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceStart = Time.realtimeSinceStartup;
        Vector3 pos = rb.transform.position;
        AddPositionToArray(timeSinceStart, pos);
    }

    void AddPositionToArray(float t, Vector3 pos)
    {
        Vector4 element = new Vector4();
        element.w = t;
        element.x = pos.x;
        element.y = pos.y;
        element.z = pos.z;
        spaceTimeArray.Add(element);
    }

    void PrintToLog(Vector4 element)
    {
    }

}
