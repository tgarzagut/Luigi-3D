using UnityEngine;

public class UnderwaterMovement : MonoBehaviour
{
    public float speed = 5f;
    public float verticalSpeed = 3f;
    public float rotationSpeed = 100f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveZ = Input.GetAxis("Vertical"); // W/S
        float moveY = 0f;

        // Vertical movement (Tab = up, C/CapsLock = down)
        if (Input.GetKey(KeyCode.Tab))
        {
            moveY = 1f;
        }
        else if (Input.GetKey(KeyCode.CapsLock) || Input.GetKey(KeyCode.C))
        {
            moveY = -1f;
        }

        // Rotation with A/D
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -rotationSpeed * Time.fixedDeltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.fixedDeltaTime);
        }

        // Forward/backward + vertical movement
        Vector3 movement = new Vector3(0f, moveY * verticalSpeed / speed, moveZ);
        rb.velocity = transform.TransformDirection(movement) * speed;
    }
}
