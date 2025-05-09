using UnityEngine;

public class UnderwaterMovement : MonoBehaviour
{
    public float speed = 5f;
    public float verticalSpeed = 3f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Input
        float moveX = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float moveZ = Input.GetAxis("Vertical");   // W/S or Up/Down
        float moveY = 0f;

        // Vertical movement
        if (Input.GetKey(KeyCode.Tab))
        {
            moveY = 1f; // Go up
        }
        else if (Input.GetKey(KeyCode.CapsLock) || Input.GetKey(KeyCode.C))
        {
            moveY = -1f; // Go down
        }

        Vector3 movement = new Vector3(moveX, moveY * verticalSpeed / speed, moveZ);
        rb.velocity = transform.TransformDirection(movement) * speed;
    }
}
