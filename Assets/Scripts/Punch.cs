using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour {

    public GameObject onda;
    public GameObject player;
    public bool izquierda = true;

    //private SoundController sc;
    private float hMovement;
    private SpriteRenderer spr;

    // Use this for initialization
    void Start () {
       // sc = GameObject.FindGameObjectWithTag("SoundController").GetComponent<SoundController>();
      //  sc.GetComponent<AudioSource>().clip = sc.sonidos[0];
      //  Destroy(gameObject, 0.2f);
        player = GameObject.Find("Player");
        spr = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey("a"))
        {          // al pulsar la a, el puñetazo mirara hacia la izquierda
            izquierda = true;
        }

        if (Input.GetKey("d"))
        {          // al pulsar la d, mirara ahcia la derecha
            izquierda = false;
        }

        if (Input.GetKey("j"))        // al atacar, el colider se movera un poco hacia al derecha o izquierda dependiendo de donde mire el jugador, ademas de que se reproducira un sonido
        {
            if (izquierda == false)
            {
                onda.transform.position = player.transform.position + Vector3.right * 1.2f;
               // sc.GetComponent<AudioSource>().clip = sc.sonidos[0];
               // sc.GetComponent<AudioSource>().Play();
            }
        }
        if (Input.GetKey("j"))
        {
            if (izquierda == true)
            {
                onda.transform.position = player.transform.position + Vector3.right * -1.2f;
                //sc.GetComponent<AudioSource>().clip = sc.sonidos[0];            // recupera el clip de audio numero 0 en la array del SoundController
                //sc.GetComponent<AudioSource>().Play();  // ejecuta el clip recuperado en la linea anterior
                spr.flipX = true;
            }

        }
    }
}
