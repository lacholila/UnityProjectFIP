using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    protected enum character { Daniel, Sergio, Xavi }
    protected character characterName;
    protected Animator characterAnimator;
    [SerializeField] protected float playerSpeed, playerJump, playerPush;   
}
