using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    bool isGrounded = true;
    bool isWalled = false;

    Rigidbody2D rb;

    public float maxSpeed = 10;
    public float jumpSpeed = 8;
    public float jumpDuration;

    public bool doubleJump = true;
    public bool wallHitDoubleJumpOverride = true;

    


    // internal checks


    float jmpDuration;
    bool jumpKeyDown = false;
    bool canVariableJump = false;


    // Use this for initialization
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        bool moveVertical = Input.GetKeyDown("space");

        //TODO Add acceleration here?
        // if grounded, can automatically double jump, if collision with wall, can't double jump
        rb.velocity = new Vector2 (maxSpeed * moveHorizontal, rb.velocity.y);

        if (moveVertical)
        {
            if (isGrounded == true)
            {
                isGrounded = false;
                rb.velocity = new Vector2(maxSpeed * moveHorizontal, 0);
                rb.velocity = new Vector2(maxSpeed * moveHorizontal, jumpSpeed);
                doubleJump = true;

            }
            else
            {
                if (doubleJump == true)
                {
                    rb.velocity = new Vector2(maxSpeed * moveHorizontal, 0);
                    rb.velocity = new Vector2(maxSpeed * moveHorizontal, jumpSpeed);
                    doubleJump = false;
                }

                if (isWalled == true)
                {
                    rb.velocity = new Vector2(maxSpeed * moveHorizontal, jumpSpeed);
                    isWalled = false;
                }

            }

            
        }

    }
 
    void Update()
    {

        
    }

    //checking for collision with floor or wall
    void OnCollisionEnter2D(Collision2D theCollision)
    {
       
        if (theCollision.gameObject.name == "Floor")
        {
            isGrounded = true;
        }
        if (theCollision.gameObject.name == "Wall")
        {
            isWalled = true;
        }
    }


}
