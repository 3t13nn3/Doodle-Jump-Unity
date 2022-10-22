
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Moving : MonoBehaviour
{
    public float speed = 0.3f ;

    private Vector3 initialPosition;
    // Update is called once per frame

    public void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        float step = speed * Time.deltaTime;
        //Debug.Log(initialPosition);
        Vector3 target =  new Vector3(UnityEngine.Random.Range(initialPosition.x -2f, initialPosition.x +2f), UnityEngine.Random.Range(initialPosition.y -2f, initialPosition.y + 2f), 0);
        //Debug.Log("target : " + target);
        transform.position = Vector3.Lerp(transform.position, target, step);
       
    }


}

 

    
