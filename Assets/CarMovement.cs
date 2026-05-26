using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float MoveSpeed = 50f;
    private Vector3 MoveForce; 
    public float MaxSpeed = 100f;
    public float Drag = 0.01f;
    public float SteerAngle = 30f;
    public float Traction = 1f;

    // Speedboost variables
    public float BoostSpeed = 150f;          // Extra speed during boost
    public float BoostDuration = 0.5f;       // How long boost lasts (in seconds)
    public float BoostCooldown = 2f;         // How long until you can boost again
    private float boostTimer = 0f;           // Current boost time remaining
    private float cooldownTimer = 0f;        // Current cooldown time remaining

    void FixedUpdate()
    {
        // Handle boost cooldown timer
        if (cooldownTimer > 0)
            cooldownTimer -= Time.deltaTime;

        // Handle boost duration timer
        if (boostTimer > 0)
            boostTimer -= Time.deltaTime;

        // Check if player pressed Shift and cooldown is ready
        if (Input.GetKeyDown(KeyCode.LeftShift) && cooldownTimer <= 0)
        {
            boostTimer = BoostDuration;      // Start boost
            cooldownTimer = BoostCooldown;   // Start cooldown
        }

        //Moving - Acceleration
        float VerticalInput = Input.GetAxis("Vertical");
        float currentSpeed = MoveSpeed;

        // If boost is active, add boost speed
        if (boostTimer > 0)
            currentSpeed += BoostSpeed;

        MoveForce += transform.forward * currentSpeed * VerticalInput * Time.deltaTime;
        transform.position += MoveForce * Time.deltaTime;

        //steering 
        float SteerInput = Input.GetAxis("Horizontal");
        transform.Rotate(0, SteerInput * MoveForce.magnitude * SteerAngle * Time.deltaTime, 0);

        //drag 
        MoveForce *= (1f - Drag);

        //speed limit using built in function ClampMagnitude
        MoveForce = Vector3.ClampMagnitude(MoveForce, MaxSpeed);

        //adding traction using lerpping towards the forward direction of the car
        Debug.DrawRay(transform.position, MoveForce, Color.red);
        MoveForce = Vector3.Lerp(MoveForce.normalized, transform.forward, Traction * Time.deltaTime);
            




    }
}
