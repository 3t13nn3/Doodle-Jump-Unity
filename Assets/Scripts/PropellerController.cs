using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public Sprite[] sprites;

    private float spriteTimer;

    private float lastTimer;

    private bool fly;

    private int spriteIndex;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[0];
        spriteTimer = 3.5f;
        lastTimer = spriteTimer;
        fly = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (fly)
        {
            if (spriteTimer > 0.0f)
            {
                if (spriteTimer > 3.4f || spriteTimer < 0.1f)
                {
                    spriteRenderer.sprite = sprites[0];
                }
                else if (spriteTimer > 0.1f)
                {
                    Vector3 pos = transform.parent.position;
                    if (spriteIndex == 1)
                        pos.Set(pos.x - 0.15f, pos.y + 0.07f, pos.z);
                    else if (spriteIndex == 2)
                        pos.Set(pos.x, pos.y + 0.2f, pos.z);
                    else
                        pos.Set(pos.x, pos.y + 0.2f, pos.z);
                    transform.position = pos;
                    spriteRenderer.sprite = sprites[spriteIndex];
                    spriteIndex = spriteIndex + 1 > 3 ? 1 : spriteIndex + 1;
                }
                spriteTimer -= Time.deltaTime;
            }
            else
            {
                if (transform.parent != null)
                {
                    Vector3 pos = transform.parent.position;
                    pos.Set(pos.x + 0.25f, pos.y - 0.2f, pos.z);
                    transform.position = pos;
                    transform.parent = null;
                    Rigidbody2D rb = GetComponent<Rigidbody2D>();
                    rb.velocity = -transform.up * 3.0f;
                }
            }
        }
    }

    public void activate()
    {
        fly = true;
    }
}
