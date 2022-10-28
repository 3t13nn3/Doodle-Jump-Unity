using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public SpriteRenderer mouthSpriteRenderer;

    public Sprite[] sprites;

    public GameObject doodle;

    public GameObject hole;

    public GameObject projectilePrefab;

    public AudioClip[] audioClipArray;

    public AudioSource audioSource;

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

    private readonly float springJumpHeight = 8f;

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
        audioSource = GetComponent<AudioSource>();

        mouthSpriteRenderer.enabled = false;
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
                ChangeSprite(LEFT_SIDE_WITH_BENDED_LEG);
            else
                ChangeSprite(LEFT_SIDE);
        }

        if (Input.GetKey(KeyCode.RightArrow) == true)
        {
            movement += Vector3.right;
            lastSide = RIGHT_SIDE;
            if (isBended)
                ChangeSprite(RIGHT_SIDE_WITH_BENDED_LEG);
            else
                ChangeSprite(RIGHT_SIDE);
        }

        if (Input.GetKey(KeyCode.UpArrow) == true)
        {
            if (isBended)
                ChangeSprite(HEAD_UP_WITH_FOOT);
            else
                ChangeSprite(HEAD_UP_WITHOUT_FOOT);
            if (attackTimer >= 0.15f)
            {
                Shoot();
            }
        }

        if (isBended && bendedTimer >= 0.25f)
        {
            isBended = false;
            bendedTimer = 0f;
            if (isAttacking)
                ChangeSprite(HEAD_UP_WITHOUT_FOOT);
            else
                ChangeSprite(lastSide);
        }

        if (isAttacking && attackTimer >= 0.25f)
        {
            isAttacking = false;
            attackTimer = 0f;
            ChangeSprite (lastSide);
        }

        bendedTimer += Time.deltaTime;
        attackTimer += Time.deltaTime;
        transform.position += movement * 2.5f * Time.deltaTime;
        isFalling = rb.velocity.y <= 0;

        // Handling Overflow only for the init one
        OverflowHandle();
    }

    // function which check the collision of the player with other gameobject
    void OnTriggerEnter2D(Collider2D other)
    {
        // player enter in collision only when he is falling
        if (isFalling)
        {
            // jump only when tiles are green, blue or white
            if ((other.gameObject.CompareTag("green_tile")) || (other.gameObject.CompareTag("blue_tile")))
            {
                Jump(defaultJumpHeight);
                audioSource.PlayOneShot(audioClipArray[0]);
                ChangeSprite(currentSpriteIndex - 1);
            }
            else if (other.gameObject.CompareTag("brown_tile"))
            {
                audioSource.PlayOneShot(audioClipArray[4]);
                ((BrownTileController)other.gameObject.GetComponent(typeof(BrownTileController))).destroy();
            }
            else if (other.gameObject.CompareTag("spring"))
            {
                Jump(springJumpHeight);
                ((SpringController)other.gameObject.GetComponent(typeof(SpringController))).changeSprite();
                audioSource.PlayOneShot(audioClipArray[3]);
                ChangeSprite(currentSpriteIndex - 1);
            }
        }

        if (other.gameObject.CompareTag("hole")) {

            //rb.velocity = new Vector3(0f, 0f, 0f);
            rb.bodyType = RigidbodyType2D.Static;
            doodle.transform.position = Vector3.MoveTowards(doodle.transform.position, hole.transform.position, 20 * Time.deltaTime);    
        }

    }

    // player jump function
    void Jump(float height)
    {
        isBended = true;
        bendedTimer = 0f;

        Vector2 velocity = rb.velocity;
        velocity.y = height;
        rb.velocity = velocity;
    }

    // player shoot function
    void Shoot()
    {
        Vector3 pos = transform.position;
        pos.Set(pos.x, pos.y + 0.5f, pos.z);
        audioSource.PlayOneShot(audioClipArray[1]);
        Instantiate(projectilePrefab, pos, Quaternion.identity);
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
    void ChangeSprite(int spriteIndex)
    {
        currentSpriteIndex = spriteIndex;
        spriteRenderer.sprite = sprites[spriteIndex];
        mouthSpriteRenderer.enabled = (spriteIndex == 2 || spriteIndex == 3);
    }

    void OverflowHandle()
    {
        if (transform.position.x + spriteRenderer.bounds.size.x / 2 < -1.55f)
        {
            transform.position = new Vector3(1.55f, transform.position.y, 0f);
        }
        else if (transform.position.x - spriteRenderer.bounds.size.x / 2 > 1.55f
        )
        {
            transform.position = new Vector3(-1.55f, transform.position.y, 0f);
        }
    }
}
