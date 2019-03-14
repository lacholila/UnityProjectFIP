using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour {
    

    public GameObject player;
    private bool izquierda;

    public Character_Controller hspd;

    //private SoundController sc;
    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start () {
        // sc = GameObject.FindGameObjectWithTag("SoundController").GetComponent<SoundController>();
        // sc.GetComponent<AudioSource>().clip = sc.sonidos[0];
        Destroy(gameObject, 0.2f);
        player = GameObject.Find("Sergio");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    

    void Update()
    {
        hspd = GameObject.FindGameObjectWithTag("Player").GetComponent<Character_Controller>();

        if (hspd.spriteRenderer.flipX == false)
        {
            izquierda = false;
        }

        if (hspd.spriteRenderer.flipX == true)
        {
            izquierda = true;
        }
        

        if (Input.GetKey("j") && (izquierda == true))
        {
            transform.position = player.transform.position + Vector3.right * -1.2f;
            //sc.GetComponent<AudioSource>().clip = sc.sonidos[0];			// recupera el clip de audio numero 0 en la array del SoundController
            //sc.GetComponent<AudioSource>().Play();  // ejecuta el clip recuperado en la linea anterior
            spriteRenderer.flipX = true;

        }

        if (Input.GetKey("j") && (izquierda == false))        // al atacar, el colider se movera un poco hacia al derecha o izquierda dependiendo de donde mire el jugador, ademas de que se reproducira un sonido
        {
            transform.position = player.transform.position + Vector3.right * 1.2f;
            //sc.GetComponent<AudioSource>().clip = sc.sonidos[0];
            //sc.GetComponent<AudioSource>().Play();
        }
    }
}
