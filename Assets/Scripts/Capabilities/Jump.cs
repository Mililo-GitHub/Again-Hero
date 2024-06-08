using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private InputController input = null;
    [SerializeField, Range(0f, 10f)] private float jumpHeight = 3f;
    [SerializeField, Range(0, 5)] private int maxAirJumps = 0;
    [SerializeField, Range(0f, 5f)] private float downwardMovementMultiplier = 3f;
    [SerializeField, Range(0f, 5f)] private float upwardMovementMultiplier = 1.7f;
    [SerializeField, Range(0f, 1f)] private float coyoteTime = 0.2f;
    [SerializeField, Range(0f, 1f)] private float jumpBufferTime = 0.2f;

    private Rigidbody2D body;
    private Ground ground;
    private Vector2 velocity;

    private int jumpPhase;
    private float defaultGravityScale, jumpSpeed, coyoteCounter, jumpBufferCounter;

    private bool desiredJump, onGround, isJumping;

    // Start is called before the first frame update
    void Awake()
    {

        body = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();

        defaultGravityScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        desiredJump |= input.RetrieveJumpInput();
    }

    private void FixedUpdate()
    {
        onGround = ground.onGround;
        velocity = body.velocity;

        if (onGround && body.velocity.y == 0)
        {
            jumpPhase = 0;
            coyoteCounter = coyoteTime;
            isJumping = false;
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
        }

        if (desiredJump)
        {
            desiredJump = false;
            jumpBufferCounter = jumpBufferTime;
        }
        else if (!desiredJump && jumpBufferCounter > 0)
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if ( jumpBufferCounter > 0 ) { JumpAction(); }

        if (input.RetrieveJumpHoldInput() && body.velocity.y > 0)
        {
            body.gravityScale =upwardMovementMultiplier;
        }
        else if (!input.RetrieveJumpHoldInput() || body.velocity.y < 0)
        {
            body.gravityScale = downwardMovementMultiplier;
        }
        else if (body.velocity.y == 0)
        {
            body.gravityScale = defaultGravityScale;
        }

        body.velocity = velocity;
    }

    private void JumpAction()
    {
        if (coyoteCounter > 0f|| jumpPhase < maxAirJumps && isJumping)
        {

            if (isJumping)
            {
                jumpPhase += 1;
            }
            coyoteCounter = 0;
            jumpBufferCounter = 0;
            jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * jumpHeight);
            isJumping = true;
            if (velocity.y > 0f)
            {
                jumpSpeed = Mathf.Max(jumpSpeed - velocity.y, 0f);
            }
            else if (velocity.y < 0f)
            {
                jumpSpeed += Mathf.Abs(body.velocity.y);
            }
            velocity.y += jumpSpeed;
        }
    }

}
