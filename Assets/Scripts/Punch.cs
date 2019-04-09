using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour {

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

        //Comprobar que objeto es (solo afecta a players? objetos?)

        Character_Controller targetController = other.gameObject.GetComponent<Character_Controller>();
        targetController.StartCoroutine(targetController.DisableInputWallJump(punchStunTime));
        other.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(100f, 0f);
        
    }






        /*
        public GameObject player;
        private bool izquierda;

        public Character_Controller hspd;
        public AreaEffector2D propDireccion;

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
            propDireccion = GameObject.FindGameObjectWithTag("Punch").GetComponent<AreaEffector2D>();


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
                propDireccion.forceAngle = 180;
                transform.position = player.transform.position + Vector3.right * -0.5f;
                transform.position = transform.position + Vector3.up * -0.1f;
                //sc.GetComponent<AudioSource>().clip = sc.sonidos[0];			// recupera el clip de audio numero 0 en la array del SoundController
                //sc.GetComponent<AudioSource>().Play();  // ejecuta el clip recuperado en la linea anterior
                spriteRenderer.flipX = true;


            }

            if (Input.GetKey("j") && (izquierda == false))        // al atacar, el colider se movera un poco hacia al derecha o izquierda dependiendo de donde mire el jugador, ademas de que se reproducira un sonido
            {
                propDireccion.forceAngle = 0;
                transform.position = player.transform.position + Vector3.right * 0.5f;
                transform.position = transform.position + Vector3.up * -0.1f;
                //sc.GetComponent<AudioSource>().clip = sc.sonidos[0];
                //sc.GetComponent<AudioSource>().Play();
            }
        }
        */
    }
