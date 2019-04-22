using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFalling : MonoBehaviour {

    private Rigidbody2D rb2d;
    private EdgeCollider2D pc2d;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        pc2d = GetComponent<EdgeCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            rb2d.isKinematic = false;
            pc2d.isTrigger = true;
            Destroy(gameObject, 2f);
        }
    }
}
