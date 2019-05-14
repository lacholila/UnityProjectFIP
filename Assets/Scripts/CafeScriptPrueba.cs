using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CafeScriptPrueba : MonoBehaviour {

    private BoxCollider2D collider;

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player")
        {
            print("hola");
            //dar mas velocidad

            Destroy(gameObject);
        }

    }
}
