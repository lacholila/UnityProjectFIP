using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "DefaultCharacter", menuName = "Models/Characters/Default")]
public class CharacterModel : ScriptableObject
{
    public string characterName;
    
    public Color characterColor;
    public Sprite characterIcon;

    public float characterSpeed;
    
    public float characterJumpSpeed;
    public int characterTotalJumps;

    public float characterDashSpeed;

    public float characterPunchImpulse;
    public float charcaterPunchStunTime;

    public int characterTotalHits;
}