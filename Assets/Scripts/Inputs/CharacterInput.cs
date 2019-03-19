using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultInput", menuName = "Models/Inputs/Default")]
public class CharacterInput : ScriptableObject, IInputController
{
    public float inputHorizontalMovement { get { throw new System.NotImplementedException(); } set { throw new System.NotImplementedException(); } }
    public bool inputJump { get { throw new System.NotImplementedException(); } set { throw new System.NotImplementedException(); } }
    public bool inputDash { get { throw new System.NotImplementedException(); } set { throw new System.NotImplementedException(); } }
    public bool inputPush { get { throw new System.NotImplementedException(); } set { throw new System.NotImplementedException(); } }
    }
