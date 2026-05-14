using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    // Geschwindigkeiten für Gehen und Rennen
    public float walkSpeed = 6f;
    public float sprintSpeed = 12f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    // Physik & Springen
    public float gravity = -9.81f;
    public float jumpHeight = 3f; // Wie hoch der Charakter springt
    Vector3 velocity;

    void Update()
    {
        Shader.SetGlobalVector("_Player", transform.position + Vector3.up * controller.radius);
        // Schwerkraft & Boden-Check
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // 2. Tasten-Eingaben lesen (WASD oder Pfeiltasten)
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // 3. Bewegen und Drehen
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            // Sprint-Check:
            float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

            controller.Move(moveDir.normalized * currentSpeed * Time.deltaTime);
        }

        // Springen (Leertaste)
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            // Physik-Formel für eine Sprungkurve
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Schwerkraft
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}