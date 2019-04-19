using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapons  {

    public Cozitax playerWeaponController;
    public WeaponModel weaponModel;

    public virtual void PickObject() { }
    public virtual void UseObject() { }
    public virtual void ThrowObject() { }

    public virtual void CarryObject()
    {

    }

    public virtual Sprite weaponSprite
    {
        get
        {
            return weaponSprite;
        }

        set
        {
            weaponSprite = value;
        }
    }
}
