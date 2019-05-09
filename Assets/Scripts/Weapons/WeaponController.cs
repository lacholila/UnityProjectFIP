using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private Rigidbody2D rb2d;

    public WeaponModel weaponModel;

    public bool isItem;
    public bool puedeCogerObjeto;

    public int itemIndex;

    private void Awake()
    {
        isItem = true;

        rb2d = GetComponent<Rigidbody2D>();

        Destroy(gameObject, 10f);
    }

    private void Update()
    {
        switch (weaponModel.weaponName)
        {
            case "Bottle":
                if (!isItem && rb2d.velocity.magnitude <= 0f) {
                    if ((Mathf.Abs(transform.localRotation.eulerAngles.z - 180) < 5f) || (Mathf.Abs(transform.localRotation.eulerAngles.z - 360) < 5f) || (Mathf.Abs(transform.localRotation.eulerAngles.z) < 5f))
                    {
                        StartCoroutine(TiempoExplosion(0f));
                    }
                    else
                    {
                        StartCoroutine(TiempoExplosion(2.5f));
                        Debug.Log("DestroyWeapon");
                    }
                }
                break;

            case "Orange":
                
                break;

            case "Boli":

                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        switch (weaponModel.weaponName)
        {
            case "Bottle":
                
                break;

            case "Orange":
                if (!isItem)
                {
                    //ecsplozion
                    GameObject orangeExp = Instantiate(weaponModel.explosionEffect, transform.position, Quaternion.identity) as GameObject;
                    GameObject orangePart = Instantiate(weaponModel.explosionParticles, transform.position, Quaternion.identity) as GameObject;

                    Destroy(gameObject);
                    Destroy(orangeExp, 0.1f);
                    Destroy(orangePart, 1f);
                }
                break;

            case "Boli":

                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            switch (weaponModel.weaponName)
            {
                case "Bottle":
                    
                    break;

                case "Orange":
                    if (!isItem)
                    {
                        //ecsplozion
                        GameObject orangeExp = Instantiate(weaponModel.explosionEffect, transform.position, Quaternion.identity) as GameObject;
                        GameObject orangePart = Instantiate(weaponModel.explosionParticles, transform.position, Quaternion.identity) as GameObject;

                        Destroy(gameObject);
                        Destroy(orangeExp, 0.1f);
                        Destroy(orangePart, 1f);
                    }
                    break;
            }
        }
    }

    private void DestroyWeapon()
    {
        Destroy(gameObject);
    }

    public IEnumerator EnfriamientoCogerObjeto()
    {
        yield return new WaitForSeconds(2f);
        puedeCogerObjeto = true;
    }

    public IEnumerator TiempoExplosion(float tiempo)
    {
        GetComponent<Animator>().SetBool("Activar", true);
        yield return new WaitForSeconds(tiempo);
        //ecsplozion
        GameObject bottleExp = Instantiate(weaponModel.explosionEffect, transform.position, Quaternion.identity) as GameObject;
        GameObject bottlePart = Instantiate(weaponModel.explosionParticles, transform.position, Quaternion.identity) as GameObject;
        Destroy(gameObject);
        Destroy(bottleExp, 0.1f);
        Destroy(bottlePart, 1f);
    }
}