using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    Rigidbody playerRB;

    InputAction moveAction;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move/Up");
        playerRB = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        Vector3 movetime = moveAction.ReadValue<Vector3>();
        playerRB.AddForce(playerRB.transform.TransformDirection(Vector3.forward) * movetime.sqrMagnitude, ForceMode.VelocityChange);     
        
    }


}
