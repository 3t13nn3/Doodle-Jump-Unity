using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;

    private Rigidbody2D rb;
    private bool isFalling;
    private bool isBended;
    private float bendedTimer;
    private bool isAttacking;
    private float attackTimer;
    private int currentSpriteIndex;
    private int lastSide;

    // two types of height when player jump
    private readonly float defaultJumpHeight = 5f;
    private readonly float springJumpHeight = 10f;
    private readonly int LEFT_SIDE_WITH_BENDED_LEG = 0;
    private readonly int LEFT_SIDE = 1;
    private readonly int HEAD_UP_WITH_FOOT = 2;
    private readonly int HEAD_UP_WITHOUT_FOOT = 3;
    private readonly int RIGHT_SIDE_WITH_BENDED_LEG = 4;
    private readonly int RIGHT_SIDE = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        lastSide = RIGHT_SIDE;
        currentSpriteIndex = RIGHT_SIDE;

        rb = GetComponent<Rigidbody2D>();
        isFalling = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.LeftArrow) == true)
        {
            movement += Vector3.left;
            lastSide = LEFT_SIDE;
            if (isBended)
                changeSprite(LEFT_SIDE_WITH_BENDED_LEG);
            else
                changeSprite(LEFT_SIDE);
        }

        if (Input.GetKey(KeyCode.RightArrow) == true)
        {
            movement += Vector3.right;
            lastSide = RIGHT_SIDE;
            if (isBended)
                changeSprite(RIGHT_SIDE_WITH_BENDED_LEG);
            else
                changeSprite(RIGHT_SIDE);
        }

        if (Input.GetKey(KeyCode.UpArrow) == true)
        {
            if (isBended)
                changeSprite(HEAD_UP_WITH_FOOT);
            else
                changeSprite(HEAD_UP_WITHOUT_FOOT);
            shoot();
        }

        if (isBended && bendedTimer >= 0.25f)
        {
            isBended = false;
            bendedTimer = 0f;
            if (isAttacking)
                changeSprite(HEAD_UP_WITHOUT_FOOT);
            else
                changeSprite(lastSide);
        }
        
        if (isAttacking && attackTimer >= 0.25f)
        {
            isAttacking = false;
            attackTimer = 0f;
            changeSprite(lastSide);
        }

        bendedTimer += Time.deltaTime;
        attackTimer += Time.deltaTime;
        transform.position += movement * 2.5f * Time.deltaTime;
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
                changeSprite(currentSpriteIndex - 1);
            }
            // else destroy tiles
            // ...
        }
    }

    // player jump function
    void jump(float height)
    {
        isBended = true;
        bendedTimer = 0f;
        Vector2 velocity = rb.velocity;
        velocity.y = height;
        rb.velocity = velocity;
    }

    // player shoot function
    void shoot()
    {
        isAttacking = true;
        attackTimer = 0f;
    }

    /*
     * change player's sprite
     * 
     * 0 -> left with leg bended, 1 -> left
     * 2 -> head up when gounded, 3 -> head up while in the air
     * 4 -> right with leg bended, 5 -> right
     * 
     */
    void changeSprite(int spriteIndex)
    {
        Debug.Log(spriteIndex);
        currentSpriteIndex = spriteIndex;
        spriteRenderer.sprite = sprites[spriteIndex];
    }
}
