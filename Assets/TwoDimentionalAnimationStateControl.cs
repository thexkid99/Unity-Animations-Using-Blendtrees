using UnityEngine;

public class TwoDimentionalAnimationStateControl : MonoBehaviour
{
    // Reference to the Animator component attached to the same GameObject
    Animator animator;

    // Variables to store the current velocity in the X and Z directions
    float velocityX = 0.0f;
    float velocityZ = 0.0f;

    // Acceleration and deceleration rates
    public float acceleration = 2.0f;
    public float deceleration = 1.0f;

    // Maximum velocity when running and walking
    public float MaxRunVelocity = 2.0f;
    public float MaxWalkVelocity = 0.5f;

    int VelocityZHash;
    int VelocityXHash;

    private void Start()
    {
        // Get the Animator component attached to the same GameObject
        animator = GetComponent<Animator>();

        VelocityZHash = Animator.StringToHash("Velocity Z");
        VelocityXHash = Animator.StringToHash("Velocity X");

    }

    private void Update()
    {
        // Check if the respective keys are pressed
        bool forwardPressKey = Input.GetKey(KeyCode.W);
        bool runPressKey = Input.GetKey(KeyCode.LeftShift);
        bool leftPressKey = Input.GetKey(KeyCode.A);
        bool rightPressKey = Input.GetKey(KeyCode.D);

        // Determine the current maximum velocity based on whether the run key is pressed
        float CurrentMaxVelocity = runPressKey ? MaxRunVelocity : MaxWalkVelocity;

        // Increase velocity in the Z direction if the forward key is pressed and velocity is below the maximum
        if (forwardPressKey && velocityZ < CurrentMaxVelocity)
        {
            velocityZ += Time.deltaTime * acceleration;
        }

        // Decrease velocity in the X direction if the left key is pressed and velocity is above the negative maximum
        if (leftPressKey && velocityX > -CurrentMaxVelocity)
        {
            velocityX -= Time.deltaTime * acceleration;
        }

        // Increase velocity in the X direction if the right key is pressed and velocity is below the maximum
        if (rightPressKey && velocityX < CurrentMaxVelocity)
        {
            velocityX += Time.deltaTime * acceleration;
        }

        // Decelerate velocity in the Z direction if the forward key is not pressed and velocity is positive
        if (!forwardPressKey && velocityZ > 0.0f)
        {
            velocityZ -= Time.deltaTime * deceleration;
        }

        // Reset velocity in the Z direction if the forward key is pressed and velocity is negative
        if (forwardPressKey && velocityZ < 0.0f)
        {
            velocityZ = 0.0f;
        }

        // Decelerate velocity in the X direction if the left key is not pressed and velocity is negative
        if (!leftPressKey && velocityX < 0.0f)
        {
            velocityX += Time.deltaTime * deceleration;
        }

        // Decelerate velocity in the X direction if the right key is not pressed and velocity is positive
        if (!rightPressKey && velocityX > 0.0f)
        {
            velocityX -= Time.deltaTime * deceleration;
        }

        // Reset velocity in the X direction if neither left nor right key is pressed and velocity is within a small range
        if (!leftPressKey && !rightPressKey && velocityX != 0.0f && (velocityX > -0.5f && velocityX < 0.5f))
        {
            velocityX = 0.0f;
        }

        // Clamp velocity in the Z direction to the maximum if the forward key is not pressed but the run key is pressed
        if (!forwardPressKey && runPressKey && velocityZ > CurrentMaxVelocity)
        {
            velocityZ = CurrentMaxVelocity;
        }
        // Decelerate velocity in the Z direction if it exceeds the maximum
        else if (forwardPressKey && velocityZ > CurrentMaxVelocity)
        {
            velocityZ -= Time.deltaTime * deceleration;
            // Clamp velocity to the maximum if it's within a small range above the maximum
            if (velocityZ > CurrentMaxVelocity && velocityZ < (CurrentMaxVelocity + 0.5f))
            {
                velocityZ = CurrentMaxVelocity;
            }
        }
        // Clamp velocity to the maximum if it's within a small range below the maximum
        else if (forwardPressKey && velocityZ < CurrentMaxVelocity && velocityZ > (CurrentMaxVelocity - 0.5f))
        {
            velocityZ = CurrentMaxVelocity;
        }

        // Update the Animator parameters with the current velocities
        animator.SetFloat(VelocityZHash, velocityZ);
        animator.SetFloat(VelocityXHash, velocityX);
    }
}
