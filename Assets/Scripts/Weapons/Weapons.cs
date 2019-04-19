using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapons  {

    public CharacterWeaponController playerWeaponController;
    public WeaponModel weaponModel;
    
    public Sprite WeaponSprite;

    public virtual void PickObject() { }
    public virtual void UseObject() { }
    public virtual void ThrowObject() { }

    public virtual void CarryObject()
    {

    }
}
