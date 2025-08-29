using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float playerspeed = 5f;

    public Transform oritentation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        UpdateInput();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void UpdateInput()
    {
        // Input
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        // Calculate movement direction
        moveDirection = oritentation.forward * verticalInput + oritentation.right * horizontalInput;

        // Move player
        rb.AddForce(moveDirection.normalized * playerspeed * 10f, ForceMode.Force);
    }
}
