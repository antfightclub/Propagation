using NUnit.Framework;
using SpecialRelativity;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class FarawaySkydome : MonoBehaviour
{

    // Should now build a way to encapsulate the world's objects so that this script can iterate over them
    // truthfully this might end up being more efficient if using Unity DOTS / an ECS dogma, but for now
    // in prototyping it is *probably* gonna be fine to just iterate swiftly. Could all this in truth
    // be done in shader code? I wonder if I can keep all object positions in VRAM and just perform the
    // trigonometry there; should be much faster than to get all object positions, put all those object
    // positions in a CPU texture, and then push that CPU texture to the GPU every frame.

    int sizex = 512;
    int sizey = 512;

    //UnityEngine.Logger logger = null;

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
        for(int i = 0; i < sizex; i++)
        {
            for(int j = 0; j < sizey; j++)
            {
                texture.SetPixel(i, j, new Color(0.0f, 0.0f, 0.0f));
            }
        }
        texture.Apply(true, false);

        //logger = new UnityEngine.Logger();

    }

    // Update is called once per frame
    void Update()
    {
        //Vector4D pos = UpdatePos();
        //var celestialObjects = CelestialObjectController.Instances;
        //Vector2 star = MapLatLong.GetST(origin, pos);
        UpdateTexture();
        DrawTempTexture();
        texture.Apply(true, false);
    }

    public void UpdateTexture()
    {
        // Make texture all the way black first
        /*for (int i = 0; i < sizex; i++)
        {
            for (int j = 0; j < sizey; j++)
            {
                texture.SetPixel(i, j, new Color(0.0f, 0.0f, 0.0f));
            }
        }*/
        
        
        //Debug.Log("------ CELESTIAL OBJECTS ------");
        int iter = 0;
        var celestialObjects = CelestialObjectController.Instances;
        foreach (var trans in celestialObjects)
        {
            //Debug.Log("Object no. " + iter + " is called " + trans.gameObject.name);
            Vector3 vec3 = trans.Trans.transform.position;
            Vector4D vec4d = new Vector4D(1.0d, (double)vec3.x, (double)vec3.y, (double)vec3.z);
            Vector2 texturePos = MapLatLong.GetST(origin, vec4d);
            int S = Mathf.FloorToInt(texturePos.x * sizex);
            int T = Mathf.FloorToInt(texturePos.y * sizey);
            texture.SetPixel(T, S, Color.white);
            iter++;
        }

        //Debug.Log("------ END CELESTIAL OBJECTS LIST -------");
        
        // Multiply by size and get the floor to convert to integer index... hopefully
       // int S = Mathf.FloorToInt(star.x * sizex);
        //int T = Mathf.FloorToInt(star.y * sizex);
        // Set the pixel to white
        
        // send it off to the GPU
        //texture.Apply(true, false);
    }


    private void DrawTempTexture()
    {
        for (int i  = 0; i < sizex; i++)
        {
            texture.SetPixel( i, i, Color.white);
            texture.SetPixel(-i, i, Color.white);
        }
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
