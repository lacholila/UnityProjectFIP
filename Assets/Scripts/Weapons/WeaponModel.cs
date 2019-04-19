using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultWeapon", menuName = "Models/Weapons/Default")]
public class WeaponModel : ScriptableObject {

    public string weaponName = "Default";
    public Sprite weaponSprite;

}
