using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("destroy");
        Destroy(collision.gameObject);
    }
}
