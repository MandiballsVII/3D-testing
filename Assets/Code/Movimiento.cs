using UnityEngine;
using UnityEngine.InputSystem;

public class Movimiento : MonoBehaviour
{
    public Rigidbody rb;
    
    public float speed = 5.0f;
    private Vector3 direccion;

    public InputActionReference moveAction;
    void Start()
    {
        
    }

    void Update()
    {
        direccion = moveAction.action.ReadValue<Vector2>();
        print(moveAction.action.ReadValue<Vector2>());
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(direccion.x * speed, rb.linearVelocity.y, direccion.z * speed);
    }
}
