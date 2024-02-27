using JetBrains.Rider.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] InputAction moveAction;
    [SerializeField] InputAction jumpAction;

    PlayerControllerMappings mappings;

    Rigidbody rb;

    [SerializeField] float jumpForce = 500f;

    const float Speed = 5.5f;

    private void Awake()
    {
        mappings = new PlayerControllerMappings();

        rb= GetComponent<Rigidbody>();

        moveAction = mappings.Player.Move;
        jumpAction = mappings.Player.Jump;

        
    }

    private void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
        jumpAction.performed += Jump;
    }

    private void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
        jumpAction.performed -= Jump;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //returns a vector2 w/ calues ofthe format (x,y)
        //x represents our input from A and D
        //y represents our input from W and S
        //on a range from -1 to 1
        Vector2 input = moveAction.ReadValue<Vector2>();
        input *= Speed;

        //adds input x to x transform
        //adds input y to z transform (z is forward/backward in unity)
        //sets y transform as 0
        //transform.position = new Vector3(transform.position.x + input.x,
        //                                transform.position.y,
        //                                transform.position.z + input.y);
        rb.velocity = new Vector3(input.x, rb.velocity.y, input.y);
    }

    void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Jumped");
        rb.AddForce(Vector3.up * jumpForce);
    }
}
