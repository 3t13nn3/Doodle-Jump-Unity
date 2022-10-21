using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{

    private float var;

    public float vitesse = 100; 
    // Update is called once per frame
    void Update()
    {
        if (var ==0)
        {
            var = Time.deltaTime;
        }
        var = var * -1;
        transform.Translate(new Vector3(4, 4) *vitesse* var);
     
    }

}

 

    
