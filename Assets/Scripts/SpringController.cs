using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private float spriteTimer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[0];
        spriteTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (spriteTimer > 0.0f)
            spriteTimer -= Time.deltaTime;
        else
            spriteRenderer.sprite = sprites[0];
    }

    public void changeSprite()
    {
        spriteRenderer.sprite = sprites[1];
        spriteTimer = 0.5f;
    }
}
