using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController2D characterController = null; // We set it to null by default to avoid warnings
    [SerializeField] private Animator animator = null;

    // This allows you to get input values from this script just by typing "... = playerController.MoveInput"
    public float MoveInput { get; private set; } // This variable can be read from anywhere but only this script can SET it
    public bool Jump { get; private set; }
    public bool Crouch { get; private set; }

    public bool waitsForJump;
    public bool wasJumpLastTime;

    private void Update()
    {
        characterController.Move(MoveInput, Crouch, waitsForJump);
        waitsForJump = false; // We have to reset jump so our player won't jump all the time
    }


    // Those methods are called by Player Input on the same game object
    public void OnMove(InputValue value)
    {
        MoveInput = value.Get<float>(); // Gets movement value in -1..1 scale
    }

    public void OnJump(InputValue value)
    {
        Jump = value.isPressed; // If Jump is pressed, we set jump to true

        // If we pressed jump, we wait tell our class to jump as soon as possible
        if (!wasJumpLastTime && Jump)
            waitsForJump = true;

        wasJumpLastTime = Jump;
    }

    public void OnCrouch(InputValue value)
    {
        Crouch = value.isPressed; // We check if crouch is held down or not
    }


    // Used for animations
    public void SetCrouchOnAnimator(bool state)
    {
        animator.SetBool("Crouch", state);
    }
}
