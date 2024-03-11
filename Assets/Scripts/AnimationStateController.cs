using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{

    Animator animator;
    int isWalkingHash;
    int isRunningHash;

    void Start()
    {
        // Get the Animator component attached to the same GameObject
        animator = GetComponent<Animator>();

        // Cache the hash codes for "IsWalking" and "IsRunning" parameters
        // to improve performance when accessing them later
        isWalkingHash = Animator.StringToHash("IsWalking");
        isRunningHash = Animator.StringToHash("IsRunning");
    }

    void Update()
    {
        // Check if the "W" key is pressed
        bool forwardPressKey = Input.GetKey("w");

        // Check if the "Q" key is pressed
        bool runPressKey = Input.GetKey("q");

        // Get the current state of the "IsWalking" parameter
        bool isWalking = animator.GetBool(isWalkingHash);

        // Get the current state of the "IsRunning" parameter
        bool isRunning = animator.GetBool(isRunningHash);

        // If the character is not walking and the "W" key is pressed,
        // set the "IsWalking" parameter to true
        if (!isWalking && forwardPressKey)
        {
            animator.SetBool(isWalkingHash, true);
        }

        // If the character is walking and the "W" key is released,
        // set the "IsWalking" parameter to false
        if (isWalking && !forwardPressKey)
        {
            animator.SetBool(isWalkingHash, false);
        }

        // If the character is not running and both "W" and "Q" keys are pressed,
        // set the "IsRunning" parameter to true
        if (!isRunning && (forwardPressKey && runPressKey))
        {
            animator.SetBool(isRunningHash, true);
        }

        // If the character is running and either "W" or "Q" key is released,
        // set the "IsRunning" parameter to false
        if (isRunning && (!forwardPressKey || !runPressKey))
        {
            animator.SetBool(isRunningHash, false);
        }
    }
}
