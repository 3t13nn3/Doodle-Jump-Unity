using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueTileController : MonoBehaviour
{
    private float x_lim_min;
    private float x_lim_max;
    private Vector3 source;
    private Vector3 target;
    private bool goTarget;

    // Start is called before the first frame update
    void Start()
    {
        x_lim_min = -1.3f;
        x_lim_max = 1.3f;
        goTarget = true;
        float x_source = Random.Range(x_lim_min, x_lim_max);
        float x_target = Random.Range(x_lim_min, x_lim_max);
        while (Mathf.Abs(x_source - x_target) < 0.4f)
        {
            x_target = Random.Range(x_lim_min, x_lim_max);
        }
        source = new Vector3(x_source, transform.position.y, transform.position.z);
        target = new Vector3(x_target, transform.position.y, transform.position.z);
        transform.position = source;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(target);
        Debug.Log(source);
        if (goTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, 0.5f * Time.deltaTime);
            goTarget = transform.position != target;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, source, 0.5f * Time.deltaTime);
            goTarget = transform.position == source;
        }
    }
}
