using SpecialRelativity;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    private Transform_float64 doubleTrans;
    public Transform_float64 DoubleTrans => doubleTrans;

    [SerializeField] private Transform trans;
    public Transform Trans => trans;
    
    private Player player;
    public Player Player => player;

    private World world;
    public World World => world;

    public PlayerWorldlineSO playerWorldline;

    [Header("Position of player")]
    private double t;
    public double x;
    public double y;
    public double z;

    [Header("Velocity of player")]
    public double velx;
    public double vely;
    public double velz; // in ls

    [Header("Acceleration of player")]
    public double accelx;
    public double accely;
    public double accelz;

    private InputAction jumpAction;

    System.Random rnd = new System.Random();



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!trans) trans = GetComponent<Transform>();
        player = new Player(new Vector4D(0.0d, 0.0d, 0.0d, 0.0d));
        //player = Player(World world, );
        jumpAction = InputSystem.actions.FindAction("Jump");
        //_instance = this;
        PhaseSpace phaseSpace = new PhaseSpace(new Vector4D(t, x, y, z), new Vector4D(1.0d, velx, vely, velz));
        Quat q = new Quat();
        playerWorldline.WorldLine = new WorldLine(phaseSpace, q);
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        

        //this.x += velx;
        //this.y += vely;
        //this.z += velz;
        //
        //Player.Position = new Vector4D(1.0d, this.x, this.y, this.z);
        //if (jumpAction.WasPressedThisFrame())
        //{
        //    PerformJump();
        //}

        //playerWorldline.Line.Add(Player.Position);

    }

    private void LateUpdate()
    {
        t += Time.deltaTime;
        PhaseSpace p = new PhaseSpace(new Vector4D(t, x, y, z), new Vector4D(1.0d, velx, vely, velz));
        //Vector4D pos = new Vector4D(t, x, y, z);
        Vector4D accel = new Vector4D(1.0d, accelx, accely, accelz);
        p.Transform(accel, Time.deltaTime);
        Quat q = new Quat();
        playerWorldline.WorldLine.Add(p, q);
        UpdatePos(p.X.t, p.X.x, p.X.y, p.X.z);
        UpdateVel(p.U.x, p.U.y, p.U.z);
        
    }
    private void UpdatePos(double _t, double _x, double _y, double _z)
    {
        t = _t; x = _x; y = _y; z = _z;
        Player.Position = new Vector4D(this.t, this.x, this.y, this.z);
    }
    private void UpdateVel(double x, double y, double z)
    {
        velx = x; vely = y; velz = z;
    }
    private void PerformJump()
    {
        this.x += rnd.NextDouble() * rnd.Next(-10, 10);
        this.y += rnd.NextDouble() * rnd.Next(-10, 10);
        this.z += rnd.NextDouble() * rnd.Next(-10, 10);
        Player.Position = new Vector4D(1.0d, this.x, this.y, this.z);

    }

    private void OnDestroy()
    {
        //_instance.Remove(this);
    }



}
