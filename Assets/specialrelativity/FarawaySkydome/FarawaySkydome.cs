using UnityEngine;

public class FarawaySkydome : MonoBehaviour
{
    public Vector3 Origin;
    public Vector3 end;
    public Vector3 start;
    public Vector3 sphereCenter;
    float sphereRadius;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Origin = new Vector3(0, 0, 0);
        GameObject emptyGO = 

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 RaycastToSphere_Simple(Vector3 origin, Vector3 end, Vector3 sphereCenter, float sphereRadius)
    {
        ref Vector3 A = ref origin;
        ref Vector3 B = ref end;
        ref Vector3 S = ref sphereCenter;
        ref float r = ref sphereRadius;

        Vector3 AS = S - A;
        Vector3 nAB = (B - A).normalized;

        float r2 = r * r;
        float as2 = AS.sqrMagnitude;
        float ac = Vector3.Dot(AS, nAB);
        float ac2 = ac * ac;
        float sc2 = as2 - ac2;
        float cd = Mathf.Sqrt(r2 - sc2);
        float ad = ac - cd;

        Vector3 D = A + nAB * ad;
        return D;
    }

}
