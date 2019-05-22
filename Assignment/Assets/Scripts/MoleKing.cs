using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleKing : MonoBehaviour
{

    public float speed;

    private Transform player;
    private Vector2 target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        player = GameObject.FindGameObjectWithTag("Player").transform;

        target = new Vector2(player.position.x, player.position.y);

        transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.x, transform.position.y), speed * Time.deltaTime);

        // need to add flip by taking player x axis from current x axis
    }

    void Flip()
    {
        //facingRight = !facingRight;
        transform.Rotate(Vector3.up * 180);
    }


}
