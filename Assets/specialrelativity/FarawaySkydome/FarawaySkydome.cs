using NUnit.Framework;
using SpecialRelativity;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class FarawaySkydome : MonoBehaviour
{
    int sizex = 256;
    int sizey = 256;

    [SerializeField]
    double pos_x;
    [SerializeField]
    double pos_y;
    [SerializeField]
    double pos_z;

    Vector4D origin = new Vector4D(0.0d, 0.0d, 0.0d, 0.0d);

    private Renderer renderer;
    //Material material;
    Texture2D texture;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        texture = new Texture2D(sizex, sizey);
        renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = texture;
        for(int i = 0; i < 256; i++)
        {
            for(int j = 0; j < 256; j++)
            {
                texture.SetPixel(i, j, new Color(0.0f, 0.0f, 0.0f));
            }
        }
        texture.Apply(true, false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector4D pos = UpdatePos();
        Vector2 star = MapLatLong.GetST(origin, pos);
        UpdateTexture(star);
    }

    public void UpdateTexture(Vector2 star)
    {
        // Make texture all the way black first
        for (int i = 0; i < 256; i++)
        {
            for (int j = 0; j < 256; j++)
            {
                texture.SetPixel(i, j, new Color(0.0f, 0.0f, 0.0f));
            }
        }
        
        // Multiply by size and get the floor to convert to integer index... hopefully
        int S = Mathf.FloorToInt(star.x * sizex);
        int T = Mathf.FloorToInt(star.y * sizex);
        // Set the pixel to white
        texture.SetPixel(S, T, Color.white);
        // send it off to the GPU
        texture.Apply(true, false);
    }

    Vector4D UpdatePos()
    {
        return new Vector4D(
            0.0d,
            this.pos_x,
            this.pos_y,
            this.pos_z);
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
