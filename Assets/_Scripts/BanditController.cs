using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

//BanditController
//Andrew Trinidad
//301021154
//Last Modified: Oct 4, 2019
//Program Description: This controller functions the Bandit
//and gives him basic enemy A.I

public class BanditController : MonoBehaviour
{
    public BanditAnimState banditAnimState;

    public Animator banditAnimator;

    public SpriteRenderer banditSpriteRenderer;

    public Rigidbody2D banditRigidBody;

    public bool isGrounded;

    public bool hasGroundAhead;

    public bool hasWallAhead;

    public Transform lookAhead;

    public Transform wallAhead;

    public bool isFacingRight = true;

    public float movementSpeed;

    void Start()
    {
        banditAnimState = BanditAnimState.WALK;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        isGrounded = Physics2D.BoxCast(
            transform.position, new Vector2(2.0f, 1.0f), 0.0f, Vector2.down,
            1.0f, 1 << LayerMask.NameToLayer("Ground"));

        hasGroundAhead = Physics2D.Linecast(
            transform.position,
            lookAhead.position,
            1 << LayerMask.NameToLayer("Ground"));

        hasWallAhead = Physics2D.Linecast(
            transform.position,
            wallAhead.position,
            1 << LayerMask.NameToLayer("Wall"));

        if (isGrounded)
        {
            if(isFacingRight)
            {
                banditRigidBody.velocity = new Vector2(movementSpeed, 0.0f);
            }

            if (!isFacingRight)
            {
                banditRigidBody.velocity = new Vector2(-movementSpeed, 0.0f);
            }

            if(!hasGroundAhead || hasWallAhead)
            {
                transform.localScale = new Vector3(-transform.localScale.x, 0.3f, 1.0f);
                isFacingRight = !isFacingRight;
            }
        }
    }
}
