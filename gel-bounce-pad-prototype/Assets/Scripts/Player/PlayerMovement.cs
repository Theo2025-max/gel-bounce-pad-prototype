using UnityEngine;
using UnityEngine.Playables;
using static UnityEngine.Timeline.DirectorControlPlayable;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 12f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;
    public CharacterController controller;

    public float minJumpHeight = 1f;
    public float minJumpHeight2 = 1.5f;
    public float minJumpHeight3 = 2f;
    public float maxJumpHeight = 4f;
    public float maxJumpHeight2 = 7f;
    public float maxJumpHeight3 = 10f;
    public float maxChargeTime = 2f;

    PlayerControls controls;
    Vector2 move;
    public bool jumpHeld;
    bool isCharging;
    float chargeTime;
    MouseMovement MouseMovement;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public LayerMask padMask, pad2Mask, pad3Mask;

    Vector3 velocity;
    bool isGrounded, onPad, onPad2, onPad3;
    GelBlock currentGelBlock;

    private void Awake()
    {
        MouseMovement = GetComponentInParent<MouseMovement>();
        controls = new PlayerControls();
        controls.Player.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => move = Vector2.zero;
        controls.Player.Jump.performed += ctx => jumpHeld = true;
        controls.Player.Jump.canceled += ctx => jumpHeld = false;
        groundMask = LayerMask.GetMask("GelSurface");
        padMask = LayerMask.GetMask("GelPad");
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        onPad = Physics.CheckSphere(groundCheck.position, groundDistance, padMask);
        onPad2 = Physics.CheckSphere(groundCheck.position, groundDistance, pad2Mask);
        onPad3 = Physics.CheckSphere(groundCheck.position, groundDistance, pad3Mask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y += gravity * Time.deltaTime;
        }

        Vector3 forward = transform.forward;
        forward.y = 0f;
        forward.Normalize();

        Vector3 right = transform.right;
        right.y = 0f;
        right.Normalize();

        Vector3 moveDir = (right * move.x + forward * move.y).normalized;
        controller.Move(moveDir * speed * Time.deltaTime);

        //check if the player is on the ground so he can jump


        if (isGrounded)
        {
            if (jumpHeld)
            {
                velocity.y = Mathf.Sqrt(minJumpHeight * -2f * gravity);

                isCharging = false;
                chargeTime = 0f;
            }
        }
        else if (onPad)
        {
            if (jumpHeld)
            {
                // Charging up
                isCharging = true;
                chargeTime += Time.deltaTime;
                chargeTime = Mathf.Min(chargeTime, maxChargeTime);
            }
            else if (isCharging)
            {
                // Released - launch the jump
                float t = chargeTime / maxChargeTime;
                float jumpHeight = Mathf.Lerp(minJumpHeight, maxJumpHeight, t);
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

                isCharging = false;
                chargeTime = 0f;
            }
        }
        else if (onPad2)
        {
            if (jumpHeld)
            {
                // Charging up
                isCharging = true;
                chargeTime += Time.deltaTime;
                chargeTime = Mathf.Min(chargeTime, maxChargeTime);
            }
            else if (isCharging)
            {
                // Released - launch the jump
                float t = chargeTime / maxChargeTime;
                float jumpHeight = Mathf.Lerp(minJumpHeight2, maxJumpHeight2, t);
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

                isCharging = false;
                chargeTime = 0f;
            }
        }
        else if (onPad3)
        {
            if (jumpHeld)
            {
                // Charging up
                isCharging = true;
                chargeTime += Time.deltaTime;
                chargeTime = Mathf.Min(chargeTime, maxChargeTime);
            }
            else if (isCharging)
            {
                // Released - launch the jump
                float t = chargeTime / maxChargeTime;
                float jumpHeight = Mathf.Lerp(minJumpHeight3, maxJumpHeight3, t);
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

                isCharging = false;
                chargeTime = 0f;
            }
        }
        else
        {
            isCharging = false;
            chargeTime = 0f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }
}
