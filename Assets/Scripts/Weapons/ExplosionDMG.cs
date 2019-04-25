using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDMG : MonoBehaviour {

    private void Start()
    {
        Destroy(gameObject, 0.1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            print("Ha esho pupa al jugador");
        }
    }
}
