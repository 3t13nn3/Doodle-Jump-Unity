using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isFalling;

    // two types of height when player jump
    private readonly float defaultJumpHeight = 5f;
    private readonly float springJumpHeight = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isFalling = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.LeftArrow) == true)
            movement += Vector3.left;
        if (Input.GetKey(KeyCode.RightArrow) == true)
            movement += Vector3.right;
        if (Input.GetKey(KeyCode.UpArrow) == true)
            shoot();

        transform.position += movement * 0.5f * Time.deltaTime;
        isFalling = rb.velocity.y <= 0;
    }

    // function which check the collision of the player with other gameobject
    void OnTriggerEnter2D(Collider2D other)
    {
        // player enter in collision only when he is falling
        if (isFalling)
        {
            // jump only when tiles are green, blue or white
            if ( (other.gameObject.CompareTag("green_tile")) || (other.gameObject.CompareTag("blue_tile")) || (other.gameObject.CompareTag("white_tile")))
            {
                jump(defaultJumpHeight);
            }
            // else destroy tiles
            // ...
        }
    }

    // player jump function
    void jump(float height)
    {
        Debug.Log("Jump");
        Vector2 velocity = rb.velocity;
        velocity.y = height;
        rb.velocity = velocity;
    }

    // player shoot function
    void shoot()
    {
        Debug.Log("Shoot");
    }
}
