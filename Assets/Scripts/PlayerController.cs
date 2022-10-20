using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public Collider2D coll;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 movement = Vector3.zero;

        //if (Input.GetKey(KeyCode.LeftArrow) == true)
        //    movement += Vector3.left;
        //if (Input.GetKey(KeyCode.RightArrow) == true)
        //    movement += Vector3.right;
        //if (Input.GetKey(KeyCode.UpArrow) == true)
        //    movement += Vector3.up;
        //if (Input.GetKey(KeyCode.DownArrow) == true)
        //    movement += Vector3.down;

        //transform.position += movement * 2.5f * Time.deltaTime;
    }

    void jump()
    {
        Debug.Log("Jump");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //if (coll.isTrigger)
        //{
        //    jump();
        //    Debug.Log(other.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        //    rb.velocity += new Vector2(0, 1);
        //}
        if (other.gameObject.CompareTag("green_tile")) {
            jump();
            Debug.Log(other.gameObject.name + " : " + gameObject.name + " : " + Time.time);
            rb.velocity += new Vector2(0, 10.1f);
        }
    }
}
