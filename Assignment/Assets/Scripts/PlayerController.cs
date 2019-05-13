using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// PROBLEMS!
    /// 
    /// - Only half of jump animation is showing
    /// - animations wrong
    /// - death screen (ish) for restart
    /// - lock all key inputs on level restart
    /// - fix purple man from where he shoots, also flip him to face player if needed
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
    bool facingRight = true;
    float moveHorizontal = 0f;

    float time = 0f;
    bool keyDisabled = false;

    // Use this for initialization
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundWallChecks = new GroundWallChecks(transform.gameObject); //check to see if touching ground or wall
    }


    public float maxSpeed = 10;


    void Update()
    {

        if (!keyDisabled)
        {
            moveHorizontal = Input.GetAxis("Horizontal");
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                moveVertical = true;
            }
        }

        // Only allowing player to move away from wall- NEEDS WORK
        int test = groundWallChecks.WallDirection();
        if (test == -1 && moveHorizontal < 0)
            moveHorizontal = 0;
        else if(test == 1 && moveHorizontal > 0)
            moveHorizontal = 0;

        // press shift to run when on the ground
        if (Input.GetKey(KeyCode.LeftShift) && groundWallChecks.IsGround()) //TODO When jumping in air and let go of shift, it slows down
            maxSpeed = 15f;
        else
            maxSpeed = 10f;
            
        //changing animation if on the ground 
        if (groundWallChecks.IsGround())
        {
            animator.SetBool("Grounded", true);
        }

        // flips char sprite when changing direction
        if ((moveHorizontal > 0 && !facingRight) || (moveHorizontal < 0 && facingRight)) { Flip(); }
    }



    bool canDoubleJump = false;
    public float jumpScale = 1f;

    // FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {

        //Timer to prevent spamming wall jump
        time += Time.deltaTime;
        if (time > 0.5f && keyDisabled)
        {
            keyDisabled = false;
            time = 0;
        }

        // move horizontal
        if(moveHorizontal != 0)
        {
            rb.velocity = new Vector2(maxSpeed * moveHorizontal, rb.velocity.y);
            animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));
        }

        if (moveVertical)
        {

            // jumps away from wall and flips char
            if (groundWallChecks.IsWalled())
            {
                Flip();
                keyDisabled = true;
                animator.SetBool("Walled", false);
                rb.velocity = new Vector2(-groundWallChecks.WallDirection() * maxSpeed * 0.5f, 10 * jumpScale); //Add force opposite to the wall  
            }

            // checks jump counter and jumps
            if (groundWallChecks.IsGround())
            {
                Jump();
                canDoubleJump = true;
            }
            else
            {
                if (canDoubleJump)
                {
                    Jump();
                    canDoubleJump = false;
                }
                
            }
            moveVertical = false;
        }

    }

    
    void Jump()
    {
        animator.SetBool("Grounded", false);
        rb.velocity = new Vector2(maxSpeed * moveHorizontal, 0); //To canel out gravity force
        rb.velocity = new Vector2(maxSpeed * moveHorizontal, 10 * jumpScale);
    }


    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(Vector3.up * 180);
    }

    private void ClearForces()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
    }



    public static int coinCount = 0;
    public Text countText;
    //public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    //public Image damageImage;

    //coins are triggers to prevent them affecting movement
    void OnTriggerEnter2D(Collider2D theCollision)
    {
        if (theCollision.gameObject.CompareTag("Coin"))
        {
            theCollision.gameObject.SetActive(false);
            coinCount++;
            countText.text = coinCount.ToString() + "/1";
        }

        if (theCollision.gameObject.CompareTag("Hazard"))
        {
            //damageImage.color = new Color(1f, 0f, 0f, 0.1f);
            //FlashScreen();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (theCollision.gameObject.CompareTag("Goal"))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        

    }


    

    void FlashScreen()
    {
        //damageImage.color = Color.Lerp(damageImage.color, Color.clear, 0.1f * Time.deltaTime);
    }


}
