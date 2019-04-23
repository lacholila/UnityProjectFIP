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
        Destroy(gameObject, punchDuration);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Character_Controller targetController = other.gameObject.GetComponent<Character_Controller>();
            targetController.StartCoroutine(targetController.DisableInputActions(punchStunTime));
            targetController.hspd = (punchForce / 2) * transform.right.x;
            targetController.GetComponent<Rigidbody2D>().AddForce(Vector2.up * (punchForce / 2), ForceMode2D.Impulse);
        }
    }
}