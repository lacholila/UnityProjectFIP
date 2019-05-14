using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeaponController : MonoBehaviour {

    private Character_Controller characterController;

    public Transform weaponIconPosition;
    public Transform weaponUsePosition;

    public bool tienesUnObjeto = false;

    public Weapons objetoActual;

    public string weaponName;
    public Sprite weaponSprite;

    public GameObject instantiateObject;
    public GameObject tintaPrefab;

    private Animator characterAnimator;

    private void Awake()
    {
        characterController = GetComponent<Character_Controller>();
        characterAnimator = GetComponent<Animator>();
    }

    void Update ()
    {
        if (tienesUnObjeto)
        {
            objetoActual.CarryObject();

            //Tirar arma
            if (characterController.inputPickWeapon)
            {
                objetoActual.ThrowObject();

                tienesUnObjeto = false;

                GameObject gameObj = Instantiate(instantiateObject, weaponUsePosition.position, transform.rotation) as GameObject;
                Rigidbody2D gameObjrb = gameObj.GetComponent<Rigidbody2D>();
                WeaponController gameObjWc = gameObj.GetComponent<WeaponController>();

                gameObjWc.itemIndex = characterController.playerIndex;

                gameObjrb.AddForce(new Vector2(5f * characterController.characterDir, 5f), ForceMode2D.Impulse);
                gameObjrb.AddTorque(Random.Range(0.1f, 0.3f) * -characterController.characterDir, ForceMode2D.Impulse);

                weaponName = "";
                weaponSprite = null;
                instantiateObject = null;

                gameObjWc.StartCoroutine("EnfriamientoCogerObjeto");
            }

            

            //Usar arma
            if (characterController.inputUseWeapon)
            {
                objetoActual.UseObject();
                
                //Variables del animator    
                characterAnimator.SetTrigger("ThrowObject");

                switch (weaponName)
                {
                    case "Bottle":
                        GameObject bottle = Instantiate(instantiateObject, weaponUsePosition.position, transform.rotation) as GameObject;
                        Rigidbody2D bottlerb = bottle.GetComponent<Rigidbody2D>();
                        WeaponController bottlewc = bottle.GetComponent<WeaponController>();
                        
                        bottlerb.AddForce(new Vector2(10f * characterController.characterDir, 5f), ForceMode2D.Impulse);
                        bottlerb.AddTorque(Random.Range(0.1f, 0.5f) * characterController.characterDir, ForceMode2D.Impulse);

                        bottlewc.isItem = false;
                        bottlewc.itemIndex = characterController.playerIndex;

                        break;

                    case "Orange":
                        GameObject orange = Instantiate(instantiateObject, weaponUsePosition.position, transform.rotation) as GameObject;
                        Rigidbody2D orangerb = orange.GetComponent<Rigidbody2D>();
                        WeaponController orangewp = orange.GetComponent<WeaponController>();

                        orangerb.AddForce(new Vector2(10f * characterController.characterDir, 10f), ForceMode2D.Impulse);
                        orangerb.AddTorque(Random.Range(0.1f, 0.5f) * -characterController.characterDir, ForceMode2D.Impulse);

                        orangewp.isItem = false;
                        orangewp.itemIndex = characterController.playerIndex;

                        break;

                    case "Boli":
<<<<<<< HEAD

=======
                        //Boli
                        GameObject boli = Instantiate(instantiateObject, weaponUsePosition.position, transform.rotation) as GameObject;
                        Rigidbody2D bolirb = boli.GetComponent<Rigidbody2D>();

                        bolirb.AddForce(new Vector2(5f * characterController.characterDir, 5f), ForceMode2D.Impulse);
                        bolirb.AddTorque(Random.Range(0.1f, 0.3f) * -characterController.characterDir, ForceMode2D.Impulse);

                        Destroy(boli, 2f);

                        //Tinta
                        GameObject tinta = Instantiate(tintaPrefab, (transform.position + new Vector3(1f, 0f, 0f)) * characterController.characterDir, transform.rotation) as GameObject;
                        TintaController tintaC = tinta.GetComponent<TintaController>();
                        tintaC.DireccionMovimiento(characterController.characterDir);
>>>>>>> Chumas

                        break;
                }

                tienesUnObjeto = false;

                weaponName = "";
                weaponSprite = null;
                instantiateObject = null;
            }

            weaponIconPosition.gameObject.GetComponent<SpriteRenderer>().sprite = weaponSprite;
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!tienesUnObjeto)
        {
            //Coger arma
            if (other.gameObject.tag == "Weapon")
            {
                if (other.gameObject.GetComponent<WeaponController>().isItem)
                {
                    if(other.gameObject.GetComponent<WeaponController>().itemIndex == characterController.playerIndex)
                    {
                        if (other.gameObject.GetComponent<WeaponController>().puedeCogerObjeto)
                        {
                            tienesUnObjeto = true;

                            other.GetComponent<WeaponController>().puedeCogerObjeto = false;

                            weaponName = other.gameObject.GetComponent<WeaponController>().weaponModel.weaponName;
                            weaponSprite = other.gameObject.GetComponent<WeaponController>().weaponModel.weaponSprite;
                            instantiateObject = other.gameObject.GetComponent<WeaponController>().weaponModel.instantiateObject;

                            switch (weaponName)
                            {
                                case "Bottle":
                                    objetoActual = new WeaponBottle();
                                    break;

                                case "Orange":
                                    objetoActual = new WeaponOrange();
                                    break;

                                case "Boli":
                                    objetoActual = new WeaponBoli();
                                    break;
                            }

                            objetoActual.PickObject();

                            Destroy(other.gameObject);
                        }
                    }
                    else
                    {
                        tienesUnObjeto = true;

                        other.GetComponent<WeaponController>().puedeCogerObjeto = false;

                        weaponName = other.gameObject.GetComponent<WeaponController>().weaponModel.weaponName;
                        weaponSprite = other.gameObject.GetComponent<WeaponController>().weaponModel.weaponSprite;
                        instantiateObject = other.gameObject.GetComponent<WeaponController>().weaponModel.instantiateObject;

                        switch (weaponName)
                        {
                            case "Bottle":
                                objetoActual = new WeaponBottle();
                                break;

                            case "Orange":
                                objetoActual = new WeaponOrange();
                                break;

                            case "Boli":
                                objetoActual = new WeaponBoli();
                                break;
                        }

                        objetoActual.PickObject();

                        Destroy(other.gameObject);
                    }
                }
            }
        }
    }
}