using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class PlayerController : MonoBehaviour
{
    public PlayerAnimState playerAnimState;

    [Header("Object Properties")]
    public Animator playerAnimator;
    public SpriteRenderer playerSpriteRenderer;
    public Rigidbody2D playerRigidBody;

    [Header("Physics Properties")]
    public float moveForce;
    public float jumpForce;

    public bool isGrounded;
    public Transform groundTarget;

    public AudioSource jumpSound;

    public Vector2 maximumVelocity = new Vector2(10.0f, 12.0f);

    // Start is called before the first frame update
    void Start()
    {
        playerAnimState = PlayerAnimState.IDLE;
    }

    // Update is called once per frame
    void Update()
    {
        // isGrounded = Physics2D.Linecast(
        //     transform.position, groundTarget.position,
        //     1 << LayerMask.NameToLayer("Ground"));


        isGrounded = Physics2D.BoxCast(
            transform.position, new Vector2(2.0f, 1.0f), 0.0f, Vector2.down,
            1.0f, 1 << LayerMask.NameToLayer("Ground"));


        
        //Idle
        if (Input.GetAxis("Horizontal") == 0)
        {
            playerAnimState = PlayerAnimState.IDLE;
            playerAnimator.SetInteger("AnimState", (int)PlayerAnimState.IDLE);
        }

        //Move Right
        if (Input.GetAxis("Horizontal") > 0)
        {
            playerSpriteRenderer.flipX = false;
           // if(isGrounded)
           // {
                playerAnimState = PlayerAnimState.RUN;
                playerAnimator.SetInteger("AnimState", (int)PlayerAnimState.RUN);
                playerRigidBody.AddForce(Vector2.right * moveForce);
            //}
        }


        //Move Left
        if (Input.GetAxis("Horizontal") < 0)
        {
            playerSpriteRenderer.flipX = true;
           //if(isGrounded)
           //{
                playerAnimState = PlayerAnimState.RUN;
                playerAnimator.SetInteger("AnimState", (int)PlayerAnimState.RUN);
                playerRigidBody.AddForce(Vector2.left * moveForce);
            //}
        }

        //Jump
        if(Input.GetAxis("Jump") > 0 && (isGrounded))
        {
            playerAnimState = PlayerAnimState.JUMP;
            playerAnimator.SetInteger("AnimState", (int)PlayerAnimState.JUMP);
            playerRigidBody.AddForce(Vector2.up * jumpForce);
            isGrounded = false;
            jumpSound.Play();
        }

        playerRigidBody.velocity = new Vector2(
            Mathf.Clamp(playerRigidBody.velocity.x, -maximumVelocity.x, maximumVelocity.x),
            Mathf.Clamp(playerRigidBody.velocity.y, -maximumVelocity.y, maximumVelocity.y)
            );
    }
}
