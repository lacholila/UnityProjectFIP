using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultCharacter", menuName = "Models/Characters/Default")]
public class CharacterModel : ScriptableObject {

    public string characterName = "Default";
    public Animator characterAnimator;

    public float characterSpeed = 3f;
    public float charcterAcceleration = 0.5f;

    public float characterJumpSpeed = 7f;
    public int characterTotalJumps = 2;

    public float characterFallSpeed = 10f;
    public float characterFallAcceleration = 0.5f;

    public float characterSlideSpeed = 2f;
    public float characterSlideAcceleration = 0.05f;

    public float characterGroundFriction = 0.2f;
    public float characterAirFriction = 0.5f;

    public float characterPush = 10f;

    public int characterTotalHits = 3;
}