using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TintaController : MonoBehaviour {

    private float playerDir;
    private bool mover;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    public void Update()
    {       
        transform.position += new Vector3((Time.deltaTime * 10f) * playerDir, 0, 0);               
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Character_Controller characterC = collision.GetComponent<Character_Controller>();
            SpriteRenderer characterSR = collision.GetComponent<SpriteRenderer>();
            characterC.cantidadMaximaVelocidadTemporalItem = characterC.characterMaxSpeed;
            characterC.characterMaxSpeed -= 5;
            characterSR.color = new Color(136F, 149F, 197F, 1F); ;
            characterC.Invoke("DelayPowerDownTinta", 3f);
            Destroy(gameObject);
        }
    }

    public void DireccionMovimiento(float direction)
    {
        playerDir = direction;
        mover = true;
    }
}
