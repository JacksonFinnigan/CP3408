using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// PROBLEMS!
    /// 
    /// - Add acceleration
    /// - Only half of jump animation is showing
    /// 
    /// </summary>


       
    
    public class GroundWallChecks
    {

        GameObject player;
        float width;
        float height;
        float length;

        //CONSTRUCTOR - making an offset for raycasts using player dimensions
        public GroundWallChecks(GameObject playerRef)
        {
            player = playerRef;
            width = player.GetComponent<Collider2D>().bounds.extents.x + 0.1f;
            height = player.GetComponent<Collider2D>().bounds.extents.y + 0.2f; // extends is always half the size
            length = 0.05f; // this is raycast length
        }

        //Returns whether or not player is touching wall.
        public bool IsWalled()
        {
            bool left = Physics2D.Raycast(new Vector2(player.transform.position.x - width, player.transform.position.y), -Vector2.right, length); // start, direction, length
            bool right = Physics2D.Raycast(new Vector2(player.transform.position.x + width, player.transform.position.y), Vector2.right, length);

            if (left || right)
                return true;
            else
                return false;
        }

        //Returns whether or not player is touching ground
        public bool IsGround()
        {
            bool down = Physics2D.Raycast(new Vector2(player.transform.position.x, player.transform.position.y - height), -Vector2.up, length);
            if (down)
                return true;
            else
                return false;
        }

        //Returns direction of wall use as well as 
        public int WallDirection()
        {
            bool left = Physics2D.Raycast(new Vector2(player.transform.position.x - width, player.transform.position.y), -Vector2.right, length);
            bool right = Physics2D.Raycast(new Vector2(player.transform.position.x + width, player.transform.position.y), Vector2.right, length);

            if (left)
                return -1;    
            else if (right)  
                return 1;
            else
                return 0;
        }
    }

    private GroundWallChecks groundWallChecks;
    public Animator animator;
    Rigidbody2D rb;

    bool moveVertical = false;
    float moveHorizontal = 0f;
    bool facingRight = true;
    int jumpCount = 0;


    // Use this for initialization
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundWallChecks = new GroundWallChecks(transform.gameObject); //check to see if touching ground or wall
    }

    void Update()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetKeyDown("space");

        //changing animation if on the ground 
        if (groundWallChecks.IsGround())
        {
            jumpCount = 1;
            animator.SetBool("Grounded", true);
        }

        // flips char sprite when changing direction
        if ((moveHorizontal > 0 && !facingRight) || (moveHorizontal < 0 && facingRight)) { Flip(); }
    }



    public float maxSpeed = 10;

    // FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {

        // if grounded, can automatically double jump, if collision with wall, can't double jump
        if(moveHorizontal != 0)
        {
            rb.velocity = new Vector2(maxSpeed * moveHorizontal, rb.velocity.y);
            animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));
        }

        if (moveVertical)
        {

            // checks jump counter and jumps
            if (jumpCount != 0)
            {
                Jump();
                animator.SetBool("Grounded", false);
            }

            // jumps away from wall and flips char
            if (groundWallChecks.IsWalled())
            {
                rb.velocity = new Vector2(-groundWallChecks.WallDirection() * maxSpeed * 0.5f, 0);
                rb.velocity = new Vector2(rb.velocity.x, maxSpeed * 0.8f); //Add force opposite to the wall
                Flip();

                animator.SetBool("Walled", false);
            }

            moveVertical = false;
        }

    }


    void Jump()
    {
        rb.velocity = new Vector2(maxSpeed * moveHorizontal, 0);
        rb.velocity = new Vector2(maxSpeed * moveHorizontal, maxSpeed * 0.8f);
        jumpCount--;
    }


    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(Vector3.up * 180);
    }



    public static int coinCount = 0;

    //coins are triggers to prevent them affecting movement
    void OnTriggerEnter2D(Collider2D theCollision)
    {
        if (theCollision.gameObject.name == "Coin")
        {
            theCollision.gameObject.SetActive(false);
            coinCount++;
        }

    }


}
