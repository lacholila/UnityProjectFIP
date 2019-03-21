using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{

    #region ----------VARIABLES----------

    [SerializeField] private CharacterModel characterModel;

    [SerializeField] private int playerIndex;

    private Animator characterAnimator;
    private Rigidbody2D rb2d;
    public SpriteRenderer spriteRenderer;

    private string characterName;
    
    private float characterMaxSpeed, characterAcceleration, characterFriction, characterGravity;
    private float characterAccelerationRatio, characterFrictionRatio, characterGravityRatio;
    private float characterJumpSpeed;

    private float characterPush;

    private float hspd;

    private int characterTotalJumps, characterCurrentJumps;
    private int characterTotalHits, characterCurrentHits;

    private bool isInGround, isInWall, isInWallRight, isInWallLeft, isSliding;
    private bool canMoveHorizontal, camJump, canDash, canSlide;

    private RaycastHit2D[] resultsD = new RaycastHit2D[10];
    private RaycastHit2D[] resultsL = new RaycastHit2D[10];
    private RaycastHit2D[] resultsR = new RaycastHit2D[10];
    
    [SerializeField] private GameObject characterPushObject;
    
    float inputHorizontalMovement;
    bool inputJump, inputDash, inputPush;

    #endregion


    #region ----------MÉTODOS----------

    private void Awake()
    {
        //Coger todos los cmponentes y variables del scriptable object
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        characterName = characterModel.characterName;
        //characterAnimator = characterModel.characterAnimator;

        characterMaxSpeed = characterModel.characterSpeed;
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
        canDash = true;
        canSlide = true;

        characterAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        //Determinar input
        inputHorizontalMovement = Input.GetAxisRaw("P" + playerIndex + "_Horizontal");

        if (!inputJump)
            inputJump = Input.GetButtonDown("P" + playerIndex + "_Jump");

        if (!inputDash)
            inputDash = Input.GetButtonDown("P" + playerIndex + "_Dash");

        if (!inputPush)
            inputPush = Input.GetButtonDown("P" + playerIndex + "_Push");

        //Variables del animator    
        characterAnimator.SetFloat("Speed", Mathf.Abs(hspd));
        characterAnimator.SetBool("Grounded", isInGround);
        characterAnimator.SetBool("WallR", isInWallRight && isSliding);
        characterAnimator.SetBool("WallL", isInWallLeft && isSliding);
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


        /*
        //Determiar variables del mundo 
        characterGravity = WorldPhysicsValues.worldGravity;

        //Determinar variables de material
        characterAccelerationRatio = WorldPhysicsValues.worldAcceleration;
        characterFrictionRatio = WorldPhysicsValues.worldFriction;
        */

        //En el suelo
        if (isInGround)
        {
            //Voltear el sprite al caminar
            if(inputHorizontalMovement>0)
            {
                spriteRenderer.flipX = false;
            }
                
            if (inputHorizontalMovement < 0)
            {
                spriteRenderer.flipX = true;
            }
                
            //Resetear saltos
            characterCurrentJumps = characterTotalJumps;

            //Frenar al tocar la pared
            if (isInWall)
            {
                hspd = 0;
            }

            //Salir del estado deslizar
            isSliding = false;

            /*----------
            CÓDIGO QUE DETECTE SI EL MATERIAL TIENE UN SCRIPT "physicMaterial".
            EN CASO DE QUE HAYA, SE COMPROBARÁ SI SE USAN LAS PROPIEDADES DEL MATERIAL O LAS DEL MUNDO.
            EN CASO DE QUE NO HAYA SCRIPT, SE PONDRÁN LAS DEL SCRIPT DE "worldPhysicsController" DIRECTAMENTE.
            ----------

            characterGravity = 1f;
            characterAcceleration = 0.5f;
            characterFriction = 1f;
        
            
            //Determinar gravedad
            rb2d.gravityScale = characterGravity;//1f;

            //Determinar fricción
            switch (resultsD[0].transform.tag)
            {
                case "Ground":
                default:
                    characterAcceleration = characterGroundAcceleration; //0.5f
                    characterFriction = characterGroundFriction; //1f
                    break;
                case "Ice":
                    characterAcceleration = characterGroundAcceleration; //0.02f;
                    characterFriction = characterGroundFriction;//0.003f;
                    break;
            }
            */
        }
        //En una pared (izquierda)
        else if (isInWallLeft)
        {
            //Entrar en el estado de deslizar
            if (hspd < 0)
            {
                if (!isSliding)
                {
                    spriteRenderer.flipX = true;
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
                        characterGravity = 0.2f;
                        break;
                    case "Ice":
                        characterGravity = 0.8f;
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
                hspd = 0;

                if (!isSliding)
                {
                    spriteRenderer.flipX = false;
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
                        characterGravity = 0.2f;
                        break;
                    case "Ice":
                        characterGravity = 0.8f;
                        break;
                }
            }
        }
        //En el aire
        else
        {
            if (inputHorizontalMovement > 0)
                spriteRenderer.flipX = false;
            if (inputHorizontalMovement < 0)
                spriteRenderer.flipX = true;

            //Determinar gravedad
            characterGravity = 1f;

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
                if (Mathf.Abs(hspd + characterAcceleration * inputHorizontalMovement) < characterMaxSpeed)
                {
                    hspd += characterAcceleration * inputHorizontalMovement;
                }
                //Aplicar velocidad máxima
                else
                {
                    hspd = characterMaxSpeed * inputHorizontalMovement;
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
            //Poner el salto a false para evitar que se ponga a false en el Update y evitar el error de que no salte algunas veces
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
                StartCoroutine(DisableHorizontalMovement(0.1f));

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


        //Dash
        if (inputDash && canDash)
        {
            //Poner el dash a false para evitar que se ponga a false en el Update y evitar el error de que no dashee algunas veces
            inputDash = false;

            hspd = 10f;
            StartCoroutine(DisableHorizontalMovement(0.1f));
        }


        //Aplicar el movimiento
        rb2d.velocity = new Vector2(hspd, rb2d.velocity.y);


        //Puñetazo
        if (Input.GetKeyDown(KeyCode.J))
        { 
            //ator.SetTrigger("squared");
            Instantiate(characterPushObject);
        }

    }

    //Desactivar el control del movimiento horizontal durante un tiempo al saltar en la pared (Evitar escalada)
    private IEnumerator DisableHorizontalMovement(float time)
    {
        canMoveHorizontal = false;

        yield return new WaitForSeconds(time);

        canMoveHorizontal = true;
    }
    #endregion
}