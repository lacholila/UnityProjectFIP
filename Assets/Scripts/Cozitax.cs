using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cozitax : MonoBehaviour {

    private bool tienesUnObjeto = false;

    public Weapons objetoActual;

    private SpriteRenderer sprRenderer;

    // Use this for initialization
    void Start ()
    {
        sprRenderer = GetComponent<SpriteRenderer>();
        //sprRenderer.sprite = objetoActual.weaponSprite;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (tienesUnObjeto)
        {
            sprRenderer.sprite = objetoActual.weaponSprite;

            if (Input.GetKeyDown(KeyCode.K))
            {

            }
        }
	}
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("ay1");
        if (!tienesUnObjeto)
        {
            print("ay2");
            if (collision.gameObject.tag == "Object")
            {
                print("ay3");
                objetoActual = new WeaponBottle();
                
            }
        }
    }
}
