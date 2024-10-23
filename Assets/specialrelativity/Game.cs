using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;
using SpecialRelativity;

public class Game : MonoBehaviour
{
    //GameObject go;
    Transform NearbyPlanet;
    Rigidbody rb;
    private TMP_Text m_TextComponent;
    private Transform m_Transform;
    private InputAction jumpAction;
    private InputAction interactAction;
    private InputAction crouchAction;
    //[SerializeField]
    public Vector3 vel = new Vector3(0.0f, 0.0f, 0.0f);
    float timer = 1.0f;

    double local_subdiv_size = 9999.9d;

    public static double c = 299_792_458.0d;

    //[SerializeField]
    public double ls_x_coord = 0.0d;
    //[SerializeField]
    public double ls_y_coord = 0.0d;
    //[SerializeField]
    public double ls_z_coord = 0.0d;
    Vector3D ls_position;

    //double ls_x_vel = 0.0d;
    //double ls_y_vel = 0.0d;
    //double ls_z_vel = 0.0d;
    Vector3D ls_vel;

    [SerializeField]
    public float x_vel_meters_sec = 0.0f;
    [SerializeField]
    public float y_vel_meters_sec = 0.0f;
    [SerializeField]
    public float z_vel_meters_sec = 0.0f;

    double MoonDiameter = MetersToLightSeconds(3_474_000.0d);
    Vector3D MoonPos = new Vector3D(0.0d, 0.0d, MetersToLightSeconds(384_400_000.0d));
        

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        jumpAction = InputSystem.actions.FindAction("Jump");
        interactAction = InputSystem.actions.FindAction("Interact");
        crouchAction = InputSystem.actions.FindAction("Crouch");
        ls_position = new Vector3D(ls_x_coord, ls_y_coord, ls_z_coord);
        ls_vel = MetersPerSecNormalizedToC(x_vel_meters_sec, y_vel_meters_sec, z_vel_meters_sec);
    }

    private void Awake()
    {
        //go = GetComponent<GameObject>();

        m_Transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        //rb.AddForce(new Vector3(0.0f, 0.0f, 0.0f));

        m_TextComponent = GetComponentInChildren<TMP_Text>();
        //m_TextComponent.text = "yeag :3";
        //rb.AddForce(new Vector3(2.0f, 2.0f, 0.3f));
        
    }

    // Update is called once per frame
    void Update()
    {

        // m_TextComponent.text = "text.";
        //bool clickVal = clickAction.ReadValue<bool>();

        vel = new Vector3(x_vel_meters_sec, y_vel_meters_sec, z_vel_meters_sec);
        ls_vel = MetersPerSecNormalizedToC(vel.x, vel.y, vel.z);
        ls_position = UpdateLightSecPos(ls_position, ls_vel);

        //m_Transform.position = ToLocalSpace_Unity(ls_position); 

        UpdateTextDisplay();

    }


    public Vector3 ToLocalSpace_Unity(Vector3D v)
    {
        double l_x = (v.x * c) % local_subdiv_size;
        double l_y = (v.y * c) % local_subdiv_size;
        double l_z = (v.z * c) % local_subdiv_size;
        return new Vector3((float)l_x, (float)l_y, (float)l_z);
    }

    public Vector3D UpdateLightSecPos(Vector3D pos, Vector3D vel)
    {
        ls_x_coord += vel.x;
        ls_y_coord += vel.y;
        ls_z_coord += vel.z;
        return new Vector3D(
            pos.x + vel.x,
            pos.y + vel.y,
            pos.z + vel.z);
    }

    public Vector3D MetersPerSecNormalizedToC(float x_vel,  float y_vel, float z_vel)
    {
        double x = (double)x_vel / c;
        double y = (double)y_vel / c;
        double z = (double)z_vel / c;
        return new Vector3D(x, y, z);
    }

    void UpdateTextDisplay()
    {
        string x_loc, y_loc, z_loc;
        x_loc = m_Transform.position.x.ToString("F7");
        y_loc = m_Transform.position.y.ToString("F7");
        z_loc = m_Transform.position.z.ToString("F7");

        string x_ls, y_ls, z_ls;
        x_ls = ls_position.x.ToString("F20");
        y_ls = ls_position.y.ToString("F20");
        z_ls = ls_position.z.ToString("F20");

        string dist_moon_x, dist_moon_y, dist_moon_z;
        dist_moon_x = (ls_position.x - MoonPos.x).ToString("F20");
        dist_moon_y = (ls_position.y - MoonPos.y).ToString("F20");
        dist_moon_z = (ls_position.z - MoonPos.z).ToString("F20");

        m_TextComponent.text = $"Local subspace coords:" +
            $"\nx: {x_loc} " +
            $"\ny: {y_loc} " +
            $"\nz: {z_loc} " +
            $"\nLightSecond space coords:" +
            $"\nx: {x_ls} light seconds" +
            $"\ny: {y_ls} light seconds" +
            $"\nz: {z_ls} light seconds" +
            $"\nDistance to the moon" +
            $"\nx: {dist_moon_x}" +
            $"\ny: {dist_moon_y}" +
            $"\nz: {dist_moon_z}";
    }

    public Vector3D MetersToLightSeconds(Vector3 m)
    {
        return new Vector3D(
            (double)m.x / c,
            (double)m.y / c,
            (double)m.z / c);
    }
    public static double MetersToLightSeconds(double m)
    {
        return m / c;
    }

    

}
