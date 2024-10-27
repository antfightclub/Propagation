using UnityEngine;

public class WorldLineListener : MonoBehaviour
{
    public PlayerWorldlineSO playerWorldline;
    private static ILogger logger = Debug.unityLogger;
    private static string kTAG = "Monaplinkert";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*logger.Log(kTAG, ">>>>>==STARTENTRIES==<<<<<");
        for (int i = 0; i < playerWorldline.Line.Count; i++)
        {
            string m = "\ntime " + i 
                + ", x = " + playerWorldline.Line[i].x 
                + ", y = " + playerWorldline.Line[i].y
                + ", z = " + playerWorldline.Line[i].z;
            logger.Log(kTAG, m);
        }
        logger.Log(kTAG, ">>>>>==ENDENTRIES==<<<<<");*/
    }
}
