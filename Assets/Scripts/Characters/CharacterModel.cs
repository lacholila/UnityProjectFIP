using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultCharacter", menuName = "Models/Characters/Default")]
public class CharacterModel : ScriptableObject
{

    public string characterName;
    public Animator characterAnimator;

    public float characterSpeed;
    
    public float characterJumpSpeed;
    public int characterTotalJumps;

    public float characterDashSpeed;

    public float characterPunchImpulse;
    public float charcaterPunchStunTime;

    public int characterTotalHits;
}