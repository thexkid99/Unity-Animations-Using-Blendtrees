using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateControllerUsingBlendtree : MonoBehaviour
{
    Animator animator;
    float speed = 0.0f; // Initialize the speed to 0
    public float acceleration = 0.1f; // The acceleration rate when moving forward
    public float deceleration = 0.5f; // The deceleration rate when not moving forward
    int speedHash;

    void Start()
    {
        // Get the Animator component attached to the same GameObject
        animator = GetComponent<Animator>();

        // Cache the hash code for the "Speed" parameter to improve performance
        speedHash = Animator.StringToHash("Speed");
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the "W" key is pressed
        bool forwardPressKey = Input.GetKey("w");

        // Check if the "Q" key is pressed
        bool runPressKey = Input.GetKey("q");

        // If the "W" key is pressed and the current speed is less than 1,
        // increase the speed by the acceleration rate
        if (forwardPressKey && speed < 1.0f)
        {
            speed += Time.deltaTime * acceleration;
        }

        // If the "W" key is not pressed and the current speed is greater than 0,
        // decrease the speed by the deceleration rate
        if (!forwardPressKey && speed > 0.0f)
        {
            speed -= Time.deltaTime * deceleration;
        }

        // If the "W" key is not pressed and the current speed is less than 0,
        // set the speed to 0 to prevent negative values
        if (!forwardPressKey && speed < 0.0f)
        {
            speed = 0.0f;
        }

        // Set the "Speed" parameter in the Animator component with the current speed value
        animator.SetFloat(speedHash, speed);
    }
}
