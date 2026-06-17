using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CatController : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float gravity = -20f;
    public float jumpHeight = 1.5f;

    private CharacterController controller;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float speed = Input.GetKey(KeyCode.LeftShift)
            ? runSpeed
            : walkSpeed;

        Vector3 move =
            transform.right * h +
            transform.forward * v;

        controller.Move(move * speed * Time.deltaTime);

        if (controller.isGrounded)
        {
            if (velocity.y < 0)
                velocity.y = -2f;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}