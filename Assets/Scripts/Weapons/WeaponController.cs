﻿using System.Collections;
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
    }

    private void Update()
    {
        switch (weaponModel.weaponName)
        {
            case "Bottle":
                if (!isItem && GetComponent<Rigidbody2D>().velocity.magnitude == 0 && ((Mathf.Abs(transform.localRotation.eulerAngles.z - 180) < 1f) || (Mathf.Abs(transform.localRotation.eulerAngles.z - 360) < 1f) || (Mathf.Abs(transform.localRotation.eulerAngles.z) < 1f)))
                {
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