using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaceMovement : MonoBehaviour
{

    public class GroundWallChecks
    {

        GameObject mace;
        float width;
        float height;
        float length;

        //CONSTRUCTOR - making an offset for raycasts using player dimensions
        public GroundWallChecks(GameObject maceRef)
        {
            mace = maceRef;
            width = mace.GetComponent<Collider2D>().bounds.extents.x + 0.1f;
            height = mace.GetComponent<Collider2D>().bounds.extents.y + 0.2f; // extends is always half the size
            length = 0.05f; // this is raycast length
        }

        //Returns whether or not player is touching ground
        public bool IsGround()
        {
            bool down = Physics2D.Raycast(new Vector2(mace.transform.position.x, mace.transform.position.y - height), -Vector2.up, length);
            if (down)
                return true;
            else
                return false;
        }

    }


    private GroundWallChecks groundWallChecks;

    Vector3 startPoint;
    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.position;
        Debug.Log(startPoint);
        groundWallChecks = new GroundWallChecks(transform.gameObject); //check to see if touching ground or wall
    }

    // Update is called once per frame
    bool moveDown = true;
    void Update()
    {

        if (groundWallChecks.IsGround())
            moveDown = false;

        if (checkPosition())
            moveDown = true;
    }

    public float speed = 5f;
    float inverse = 1f;
    void FixedUpdate()
    {
        if (moveDown)
            inverse = 1;
        else
            inverse = -1f;

        moveMace(inverse);
    }


    void moveMace(float x)
    {
        transform.position += Vector3.down * x * speed * Time.deltaTime;
    }

    Vector3 currentPoint;
    bool checkPosition()
    {
        currentPoint = transform.position;
        return currentPoint == startPoint;
    }
}
