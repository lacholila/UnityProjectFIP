﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public WeaponModel weaponModel;

    public bool isItem;

    public int itemIndex;

    public GameObject expArea, partBotella;

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
                if (!isItem && GetComponent<Rigidbody2D>().velocity.magnitude == 0 && ((Mathf.Abs(transform.localRotation.eulerAngles.z - 180) < 1f) || (Mathf.Abs(transform.localRotation.eulerAngles.z - 360) < 1f) || (Mathf.Abs(transform.localRotation.eulerAngles.z) < 1f)))
                {
                    //ecsplozion
                    Instantiate(expArea, transform.position, Quaternion.identity);
                    Instantiate(partBotella, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                    print("ameisin borel flep");
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
                if (!isItem)
                {
                    Destroy(gameObject, 3f);
                }
                break;

            case "Orange":
                if (!isItem)
                {

                    Destroy(gameObject);
                }
                break;
        }
    }
}