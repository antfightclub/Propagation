using SpecialRelativity;
using UnityEngine;
using System;

public class WobbleABit : MonoBehaviour
{
    private Transform m_Transform;
    private float offsetx;
    private float offsety;
    private float offsetz;
    private float randphase_x;
    private float randphase_y;
    private float randphase_z;
    private float tickScalar_x;
    private float tickScalar_y;
    private float tickScalar_z;

    public Vector3 initPos;
    private System.Random r = new System.Random();

    [SerializeField]
    public float damping = 0.0005f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        

    }

    private void Awake()
    {
        m_Transform = GetComponent<Transform>();
        //m_Transform.localPosition = new Vector3(5.87f, 0.4f, 25.0f);        
        initPos = m_Transform.localPosition;
        randphase_x = (float)r.NextDouble();
        randphase_y = (float)r.NextDouble();
        randphase_z = (float)r.NextDouble();
        tickScalar_x = (float)r.NextDouble() * 0.007f;
        tickScalar_y = (float)r.NextDouble() * 0.007f;
        tickScalar_z = (float)r.NextDouble() * 0.007f;
        
    }

    // Update is called once per frame
    void Update()
    {
        offsetx = Mathf.Sin(Environment.TickCount * tickScalar_x + randphase_x * Mathf.PI) * damping;
        offsety = Mathf.Sin(Environment.TickCount * tickScalar_y + randphase_y * Mathf.PI) * damping;
        offsetz = Mathf.Sin(Environment.TickCount * tickScalar_z + randphase_z * Mathf.PI) * damping;

        m_Transform.localPosition = new Vector3(initPos.x + offsetx, initPos.y + offsety, initPos.z + offsetz);
    }
}
