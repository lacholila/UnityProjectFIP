﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{

    #region ----------VARIABLES----------

    [SerializeField] private CharacterModel characterModel;

    private Animator characterAnimator;
    private Rigidbody2D rb2d;
    public SpriteRenderer spriteRenderer;

    private string characterName;

    private float characterSpeed, characterAcceleration;
    private float characterJumpSpeed;
    private float characterFallSpeed, characterFallAcceleration;
    private float characterSlideSpeed, characterSlideAcceleration;
    private float characterFriction, characterAirFriction;
    private float characterGroundRatio, characterAirRatio;
    private float characterPush;

    private float limitedSpeedX, limitedSpeedY;
    private float hspd, vspd;

    private int characterTotalJumps, characterCurrentJumps;
    private int characterTotalHits, characterCurrentHits;

    private bool isInGround, isInWall, isInWallRight, isInWallLeft, isSliding;
    private bool canMoveHorizontal, camJump;

    private RaycastHit2D[] resultsD = new RaycastHit2D[10];
    private RaycastHit2D[] resultsL = new RaycastHit2D[10];
    private RaycastHit2D[] resultsR = new RaycastHit2D[10];

    private Animator ator;
    public GameObject puño1;                // gameobject que intanciara el puñetazo

    float inputHorizontalMovement;
    bool inputJump;

    #endregion


    #region ----------MÉTODOS----------

    private void Awake()
    {
        //Coger todos los cmponentes y variables del scriptable object
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        characterName = characterModel.characterName;
        //characterAnimator = characterModel.characterAnimator;

        characterSpeed = characterModel.characterSpeed;
        characterAcceleration = characterModel.charcterAcceleration;

        characterJumpSpeed = characterModel.characterJumpSpeed;
        characterTotalJumps = characterModel.characterTotalJumps;

        characterFriction = characterModel.characterGroundFriction;

        characterPush = characterModel.characterPush;

        characterTotalHits = characterModel.characterTotalHits;
    }

    private void Start()
    {
        //Determinar valor de algunas variables al inicio
        characterCurrentJumps = characterTotalJumps;
        canMoveHorizontal = true;
        camJump = true;
    }

    private void Update()
    {
        //Determinar valor de las variables del animator
        /*characterAnimator.SetBool("Grounded", isInGround);
        characterAnimator.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
        characterAnimator.SetFloat("Air", Mathf.Sign(rb2d.velocity.y));
        characterAnimator.SetBool("WallTouch", isInWallRight);
        characterAnimator.SetBool("WallTouchL", isInWallLeft);*/

        //Determinar input
        inputHorizontalMovement = (Input.GetAxisRaw("Horizontal"));
        if (!inputJump)
            inputJump = (Input.GetButtonDown("Jump"));
    }

    private void FixedUpdate()
    {
        //Determinar estado del personaje
        int nResultsD = rb2d.Cast(Vector2.down, resultsD, 0.02f);
        isInGround = (nResultsD > 0);

        int nResultsL = rb2d.Cast(Vector2.left, resultsL, 0.02f);
        isInWallLeft = ((nResultsL > 0) && !isInGround);

        int nResultsR = rb2d.Cast(Vector2.right, resultsR, 0.02f);
        isInWallRight = ((nResultsR > 0) && !isInGround);

        isInWall = ((nResultsL > 0) || (nResultsR > 0));

        //En el suelo
        if (isInGround)
        {
            //Resetear saltos
            characterCurrentJumps = characterTotalJumps;

            //Determinar gravedad
            rb2d.gravityScale = 1f;

            //Salir del estado deslizar
            isSliding = false;

            //Determinar fricción
            switch (resultsD[0].transform.tag)
            {
                case "Ground":
                default:
                    characterAcceleration = 0.5f;
                    characterFriction = 1f;
                    break;
                case "Ice":
                    characterAcceleration = 0.02f;
                    characterFriction = 0.003f;
                    break;
            }
        }
        //En una pared (izquierda)
        else if (isInWallLeft)
        {
            //Entrar en el estado de deslizar
            if (hspd < 0)
            {
                if (!isSliding)
                {
                    isSliding = true;
                    rb2d.velocity = new Vector2(0, 0);
                }
            }

            //Deslizando
            if (isSliding)
            {
                //Determinar velocidad horizontal
                hspd = 0;

                //Poner los saltos a 1 (salto de pared)
                characterCurrentJumps = 1;

                //Determinar fricción
                characterAcceleration = 0.3f;
                characterFriction = 0.1f;

                //Determinar gravedad
                switch (resultsL[0].transform.tag)
                {
                    case "Ground":
                    default:
                        rb2d.gravityScale = 0.2f;
                        break;
                    case "Ice":
                        rb2d.gravityScale = 0.8f;
                        break;
                }
            }
        }
        //En una pared (derecha)
        else if (isInWallRight)
        {
            //Entrar en el estado de deslizar
            if (hspd > 0)
            {
                if (!isSliding)
                {
                    isSliding = true;
                    rb2d.velocity = new Vector2(0, 0);
                }
            }

            //Deslizando
            if (isSliding)
            {
                //Determinar velocidad horizontal
                hspd = 0;

                //Poner los saltos a 1 (salto de pared)
                characterCurrentJumps = 1;

                //Determinar fricción
                characterAcceleration = 0.3f;
                characterFriction = 0.1f;

                //Determinar gravedad
                switch (resultsR[0].transform.tag)
                {
                    case "Ground":
                    default:
                        rb2d.gravityScale = 0.2f;
                        break;
                    case "Ice":
                        rb2d.gravityScale = 0.8f;
                        break;
                }
            }
        }
        //En el aire
        else
        {
            //Determinar gravedad
            rb2d.gravityScale = 1f;

            //Salir del estado deslizar
            isSliding = false;

            //Determinar fricción
            characterAcceleration = 0.3f;
            characterFriction = 0.1f;
        }


        //Movimiento horizontal
        if (canMoveHorizontal)
        {
            //Al pulsar una dirección
            if (inputHorizontalMovement != 0)
            {
                //Aplicar aceleración
                if (Mathf.Abs(hspd + characterAcceleration * inputHorizontalMovement) < characterSpeed)
                {
                    hspd += characterAcceleration * inputHorizontalMovement;
                }
                //Aplicar velocidad máxima
                else
                {
                    hspd = characterSpeed * inputHorizontalMovement;
                }
            }
            else
            {
                //Aplicar fricción
                if (Mathf.Abs(hspd) >= characterFriction)
                {
                    hspd -= characterFriction * Mathf.Sign(hspd);
                }
                //Frenar
                else
                {
                    hspd = 0;
                }
            }
        }



        //Salto
        if (inputJump && camJump)
        {
            inputJump = false;
            //Normal o en el aire
            if ((isInGround || characterCurrentJumps > 0) && !isSliding)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                rb2d.AddForce(Vector2.up * characterJumpSpeed, ForceMode2D.Impulse);
                characterCurrentJumps--;
            }
            //En pared
            else if (isSliding && !isInGround)
            {
                characterCurrentJumps--;
                rb2d.velocity = new Vector2(0, 0);
                rb2d.AddForce(Vector2.up * characterJumpSpeed * Mathf.Sin(Mathf.Deg2Rad * 60), ForceMode2D.Impulse);
                StartCoroutine("ApplyWallJump");

                //Salir del estado de deslizar
                isSliding = false;

                if (isInWallLeft)
                {
                    isInWallLeft = false;
                    hspd = characterJumpSpeed * Mathf.Cos(Mathf.Deg2Rad * 60);
                }
                else if (isInWallRight)
                {
                    isInWallRight = false;
                    hspd = -characterJumpSpeed * Mathf.Cos(Mathf.Deg2Rad * 60);
                }
            }
        }

        rb2d.velocity = new Vector2(hspd, rb2d.velocity.y);


        //PUÑETAZO
        if (Input.GetKeyDown("j"))
            {

            //ator.SetTrigger("squared");
            Instantiate(puño1);
        }

    }

    //Desactivar el control del movimiento horizontal durante un tiempo al saltar en la pared (Evitar escalada)
    private IEnumerator ApplyWallJump()
    {
        canMoveHorizontal = false;

        yield return new WaitForSeconds(0.1f);

        canMoveHorizontal = true;
    }

    #endregion
}