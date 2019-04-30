using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour {

    public Transform target;
    public float speed;

    private Vector3 start, end;

    private void Start()
    {
        if(target != null)
        {
            target.parent = null;
            start = transform.position;
            end = target.position;
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
		if (target != null)
        {
            float fixedSpeed = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed);
        }

        if ( transform.position == target.position)
        {
            target.position = (target.position == start) ? end: start;
        }
	}

    
}
