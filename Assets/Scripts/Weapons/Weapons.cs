using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapons  {

    public CharacterWeaponController playerWeaponController;

    public Sprite WeaponSprite;
    
    public virtual void PickObject()
    {
        Debug.Log("Weapon cogida");
    }

    public virtual void UseObject()
    {
        Debug.Log("Weapon usada");
    }

    public virtual void ThrowObject()
    {
        Debug.Log("Weapon tirada");
    }

    public virtual void CarryObject()
    {
        Debug.Log("LLevando weapon");
    }
}