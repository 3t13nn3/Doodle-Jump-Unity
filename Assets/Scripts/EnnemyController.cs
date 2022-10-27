using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyController : MonoBehaviour
{   
    private bool side = true;
    private float speed;
    void Start()
    {}

    // Update is called once per frame
    void Update()
    {
        // Animation
        
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        
        if(side && transform.position.x - spriteRenderer.bounds.size.x / 2 > -1.55f) {
            transform.position = new Vector3(transform.position.x - 0.005f, transform.position.y, 0f);
        } else if (!side && transform.position.x + spriteRenderer.bounds.size.x / 2 < 1.55f) {
            transform.position = new Vector3(transform.position.x + 0.005f, transform.position.y, 0f);
        }

        if (side && transform.position.x - spriteRenderer.bounds.size.x / 2 < -1.55f || transform.position.x + spriteRenderer.bounds.size.x / 2 > 1.55f) {
            side = !side;
        }
    }
}
