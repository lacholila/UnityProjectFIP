using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticle : MonoBehaviour {

    public float timeToDestroy;

	void Start ()
    {
        Destroy(gameObject, timeToDestroy);	
	}
}
