using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y > transform.position.y)
        {
            Vector3 newPos = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
            transform.position = newPos;
        }
    }
}
