using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public WeaponModel weaponModel;

    public bool isItem;

    public int itemIndex;

    private void Awake()
    {
        isItem = true;

        Destroy(gameObject, 10f);
    }

    private void Update()
    {
        switch (weaponModel.weaponName)
        {
            case "Bottle":
                if (!isItem && GetComponent<Rigidbody2D>().velocity.magnitude == 0) {
                    if ((Mathf.Abs(transform.localRotation.eulerAngles.z - 180) < 5f) || (Mathf.Abs(transform.localRotation.eulerAngles.z - 360) < 5f) || (Mathf.Abs(transform.localRotation.eulerAngles.z) < 5f))
                    {
                        //ecsplozion
                        GameObject bottleExp = Instantiate(weaponModel.explosionEffect, transform.position, Quaternion.identity) as GameObject;
                        GameObject bottlePart = Instantiate(weaponModel.explosionParticles, transform.position, Quaternion.identity) as GameObject;

                        Destroy(gameObject);
                        Destroy(bottleExp, 0.1f);
                        Destroy(bottlePart, 1f);
                    }
                    else
                    {
                        Invoke("DestroyWeapon", 2f);
                        Debug.Log("DestroyWeapon");
                    }
                }
                else
                {
                    CancelInvoke("DestroyWeapon");
                    Debug.Log("Cancelao DestroyWeapon");
                }
                break;

            case "Orange":
                
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
}