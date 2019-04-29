using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPunch : MonoBehaviour {

    public int punchIndex;

    public float punchForce;
    public float punchDuration;
    public float punchStunTime;

    private void Start()
    {
        //Destroy(gameObject, punchDuration);
    }
}