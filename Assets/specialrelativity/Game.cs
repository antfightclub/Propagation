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
    [SerializeField]
    public Vector3 vel = new Vector3(0.0f, 0.0f, 0.0f);
    float timer = 1.0f;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        jumpAction = InputSystem.actions.FindAction("Jump");
        interactAction = InputSystem.actions.FindAction("Interact");
        crouchAction = InputSystem.actions.FindAction("Crouch");
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
        if (jumpAction.IsInProgress())
        {
            Teleport1kMeters();
        }
        if (crouchAction.IsInProgress())
        {
            m_Transform.position = new Vector3(0.0f, 0.0f, 0.0f);
        }
        if (interactAction.IsPressed())
        {
            rb.AddForce(vel);
        }

        UpdateTextDisplay();

    }

    void Teleport1kMeters()
    {
        Vector3 v = m_Transform.position;
        Vector3 u = new Vector3(1000.0f, 1000.0f, 1000.0f);
        v.x += u.x;
        v.y += u.y;
        v.z += u.z;
        m_Transform.position = v;

        //Console.WriteLine("The tele method was called :3");
    }

    void UpdateTextDisplay()
    {
        string x, y, z;
        x = m_Transform.position.x.ToString("F7");
        y = m_Transform.position.y.ToString("F7");
        z = m_Transform.position.z.ToString("F7");
        m_TextComponent.text = $"x: {x} \ny: {y} \nz: {z}";
    }


}
