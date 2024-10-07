using SpecialRelativity;
using UnityEditor;
using UnityEngine;

public class Vector3DTests : MonoBehaviour
{
    [SerializeField]
    public double spos_x = 0.0d;
    [SerializeField]
    public double spos_y = 0.0d;
    [SerializeField]
    public double spos_z = 0.0d;
    [SerializeField]
    public double dir_x = 1.0d;
    [SerializeField]
    public double dir_y = 1.0d;
    [SerializeField]
    public double dir_z = 1.0d;
    

    public Vector3D startPos = new Vector3D(0.0d, 0.0d, 0.0d);
    public Vector3D direction = new Vector3D(1.0d, 1.0d, 1.0d);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos.x = spos_x;
        startPos.y = spos_y;
        startPos.z = spos_z;
        direction.x = dir_x;
        direction.y = dir_y;
        direction.z = dir_z;
    }

    // Update is called once per frame
    void Update()
    {
        startPos.x = spos_x;
        startPos.y = spos_y;
        startPos.z = spos_z;
        direction.x = dir_x;
        direction.y = dir_y;
        direction.z = dir_z;
       
    }

    private void OnGUI()
    {
        HandleUtility.Repaint();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(startPos.ToUnityVec3(), direction.ToUnityVec3());
    }

}
