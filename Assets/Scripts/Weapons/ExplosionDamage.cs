using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDamage : MonoBehaviour {

    private int frames;

    private void Start()
    {
        frames = 0;
    }

    private void FixedUpdate()
    {
        if (frames > 0)
        {
            Destroy(gameObject);
        }

        frames++;
    }
}
