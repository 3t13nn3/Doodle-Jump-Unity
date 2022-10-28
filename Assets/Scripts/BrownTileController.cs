using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownTileController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public Sprite[] sprites;

    private float spriteTimer;

    private bool destroyed;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[0];
        spriteTimer = 0.5f;
        destroyed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (spriteTimer < 0.5f) spriteRenderer.sprite = sprites[1];
        if (spriteTimer < 0.25f) spriteRenderer.sprite = sprites[2];
        if (spriteTimer <= 0.0f)
        {
            Destroy (gameObject);
            GameElementController.decreaseNbTiles();
        }
        if (destroyed)
        {
            transform.position += Vector3.down * 0.25f * Time.deltaTime;
            spriteTimer -= Time.deltaTime;
        }
    }

    public void destroy()
    {
        destroyed = true;
    }
}
