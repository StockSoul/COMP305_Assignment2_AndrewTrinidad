using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;
using UnityEngine.SceneManagement;

//PlayerController
//Andrew Trinidad
//301021154
//Last Modified: Oct 4, 2019
//Program Description: This controller allows the player to move and
//restricts player to certain actions. Also controls all of the sounds.

public class PlayerController : MonoBehaviour
{
    public PlayerAnimState playerAnimState;

    public GameController gameController;

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
    public AudioSource coinSound;
    public AudioSource hurtSound;

    public Vector2 maximumVelocity = new Vector2(10.0f, 12.0f);

    // Start is called before the first frame update
    void Start()
    {
        playerAnimState = PlayerAnimState.IDLE;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
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
            transform.localScale = new Vector3(0.2f, 0.2f, 1.0f);
            if (isGrounded)
            {
                playerAnimState = PlayerAnimState.RUN;
                playerAnimator.SetInteger("AnimState", (int)PlayerAnimState.RUN);
                playerRigidBody.AddForce(new Vector2(1, 0.1f) * moveForce);
            }
        }


        //Move Left
        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.localScale = new Vector3(-0.2f, 0.2f, 1.0f);
            if (isGrounded)
            {
                playerAnimState = PlayerAnimState.RUN;
                playerAnimator.SetInteger("AnimState", (int)PlayerAnimState.RUN);
                playerRigidBody.AddForce(new Vector2(-1, 0.1f) * moveForce);
            }
        }

        //Jump
        if (Input.GetAxis("Jump") > 0 && (isGrounded))
        {
            playerAnimState = PlayerAnimState.JUMP;
            playerAnimator.SetInteger("AnimState", (int)PlayerAnimState.JUMP);
            playerRigidBody.AddForce(Vector2.up * jumpForce);
            isGrounded = false;
            jumpSound.Play();
        }


        //Cap Player Speed
        playerRigidBody.velocity = new Vector2(
            Mathf.Clamp(playerRigidBody.velocity.x, -maximumVelocity.x, maximumVelocity.x),
            Mathf.Clamp(playerRigidBody.velocity.y, -maximumVelocity.y, maximumVelocity.y)
            );


    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //If a player collides with this they either gain score or finish the level
        if(other.gameObject.tag == "Coin")
        {
            Destroy(other.gameObject);
            gameController.Score += 10;
            coinSound.Play();
        }
        if(other.gameObject.tag == "Goal")
        {
            coinSound.Play();
            SceneManager.LoadScene("Finish");
        }

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //If player collides with any of these they lose a Life
        if (other.gameObject.tag == "Enemy")
        {
            gameController.Lives -= 1;
            hurtSound.Play();
        }
        if (other.gameObject.tag == "DeathPlane")
        {
            gameController.Lives -= 1;
            hurtSound.Play();
        }
        if (other.gameObject.tag == "Spike")
        {
            gameController.Lives -= 1;
            hurtSound.Play();
        }
    }


}
