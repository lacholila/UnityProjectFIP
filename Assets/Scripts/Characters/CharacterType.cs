using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterType", menuName = "Character")]
public class CharacterType : ScriptableObject, ICharacter
{
    //Tipo de personaje
    public enum player { Daniel, Sergio, Xavi }

    public player playerName;

    //Animaciones
    public Animator playerAnimator;

    //Estadísticas
    private float playerSpeed, playerJumpForce, playerPushForce;
   
   public void Awake()
    {
        switch(playerName)
        {
            case (player.Daniel):
                playerSpeed = 20f;
                playerJumpForce = 10f;
                playerPushForce = 10f;
            break;

            case (player.Sergio):
                playerSpeed = 10f;
                playerJumpForce = 20f;
                playerPushForce = 10f;
            break;

            case (player.Xavi):
                playerSpeed = 10f;
                playerJumpForce = 10f;
                playerPushForce = 20f;
            break;
        }
    }
}