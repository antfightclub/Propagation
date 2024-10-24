using SpecialRelativity;
using UnityEngine;
using System.Collections.Generic;

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


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!trans) trans = GetComponent<Transform>();
        player = new Player(new Vector4D(0.0d, 0.0d, 0.0d, 0.0d));
        //player = Player(World world, );
        //_instance = this;
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
        
    }

    private void OnDestroy()
    {
        //_instance.Remove(this);
    }
}
