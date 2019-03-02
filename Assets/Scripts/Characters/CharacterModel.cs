using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultCharacter", menuName = "Models/Characters/Default")]
public class CharacterModel : ScriptableObject {

    public string characterName = "Default";
    public Animator characterAnimator;

    public float characterSpeed = 3f;
    [HideInInspector] public float charcterAcceleration = 0.2f;

    public float characterJumpSpeed = 7f;
    public int characterTotalJumps = 2;

    public float characterFallSpeed = 10f;

    public float characterSlideSpeed = 2f;
    [HideInInspector] public float characterSlideAcceleration = 0.05f;

    [HideInInspector] public float characterGroundFriction = 1f;
    [HideInInspector] public float characterAirFriction = 0.5f;

    public float characterPush = 10f;

    public int characterTotalHits = 3;
}