using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// PROBLEMS!
    /// 
    /// - stuck in walking and jumping anim when touching walls
    /// - Add acceleration
    /// - bouncing idle anim not showing
    /// - Only half of jump animation is showing
    /// 
    /// 
    /// </summary>


    public float maxSpeed = 10;
    public float jumpSpeed = 8;

    public bool doubleJump = true;

    public Animator animator;


    bool isGrounded = true;
    bool isWalled = false;
    bool facingRight = true;
    bool moveVertical = false;
    float moveHorizontal = 0f;


    Rigidbody2D rb;

    // Use this for initialization
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetKeyDown("space");

    }

    // FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        

        // if grounded, can automatically double jump, if collision with wall, can't double jump
        if (moveHorizontal != 0)
        {
            rb.velocity = new Vector2(maxSpeed * moveHorizontal, rb.velocity.y);
            animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));
        }

        // flips char sprite when changing direction
        if ((moveHorizontal > 0 && !facingRight) || (moveHorizontal < 0 && facingRight)){ Flip(); }


        if (moveVertical)
        {
            animator.SetBool("Grounded", false);

            // Set DJ to true if touching the ground
            if (isGrounded == true)
            {
                Jumping(isGrounded);
                doubleJump = true;               
            }
            // if doesn't have DJ the only SJ is enabled
            else
            {
                if (doubleJump == true)
                {
                    Jumping(doubleJump);
                }

                if (isWalled == true)
                {
                    Jumping(isWalled);
                    animator.SetBool("Walled", false);
                }

            }

            moveVertical = false;
        }

    }


    void Jumping(bool changed)
    {
        rb.velocity = new Vector2(maxSpeed * moveHorizontal, 0);
        rb.velocity = new Vector2(maxSpeed * moveHorizontal, jumpSpeed);
        changed = false;
    }


    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(Vector3.up * 180);
    }




    //checking for collision with floor or wall
    void OnCollisionEnter2D(Collision2D theCollision)
    {
       
        if (theCollision.gameObject.name == "Floor")
        {
            isGrounded = true;
            animator.SetBool("Grounded", isGrounded);
        }
        if (theCollision.gameObject.name == "Wall")
        {
            isWalled = true;
            animator.SetBool("Grounded", isWalled);
        }
    }


}
