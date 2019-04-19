using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeaponController : MonoBehaviour {

    public Transform objectPosition;
    private bool tienesUnObjeto = false;

    public Weapons objetoActual;

    private string weaponName;
    private Sprite weaponSprite;

    // Update is called once per frame
    void Update ()
    {
        if (tienesUnObjeto)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                print("Objeto soltado");

                tienesUnObjeto = false;

                weaponName = "";
                weaponSprite = null;
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                print("Objeto usado");


            }

            objectPosition.gameObject.GetComponent<SpriteRenderer>().sprite = weaponSprite;// objetoActual.WeaponSprite;
        }
	}
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!tienesUnObjeto)
        {
            if (other.gameObject.tag == "Weapon")
            {
                print("Objeto cogido");

                tienesUnObjeto = true;

                weaponName = other.gameObject.GetComponent<WeaponController>().weaponModel.weaponName;
                weaponSprite = other.gameObject.GetComponent<WeaponController>().weaponModel.weaponSprite;

                Destroy(other.gameObject);
            }
        }
    }
}
