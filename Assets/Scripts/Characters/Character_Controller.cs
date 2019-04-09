using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{

    #region ----------VARIABLES----------

    [SerializeField] private CharacterModel characterModel;
    
    [SerializeField] private int playerIndex;

    private SpriteRenderer spriteRenderer;
    
    private Rigidbody2D rb2d;
    private Animator characterAnimator;
        
    private string characterName;
    
    private float characterMaxSpeed, characterAcceleration, characterFriction, characterGravity;
    private float characterAccelerationRatio, characterFrictionRatio, characterGravityRatio;
    private float characterJumpSpeed;
    private float characterPunchImpulse, characterPunchDuration, characterPunchStunTime;
    private float characterDashSpeed;

    public float hspd;
    private int characterDir;
    private Quaternion characterDirection;

    private int characterTotalJumps, characterCurrentJumps;
    private int characterTotalHits, characterCurrentHits;

    private bool isInGround, isInWall, isInWallRight, isInWallLeft, isSliding;
    private bool canMoveHorizontal, canJump, canDash, canSlide, canPunch;

    private RaycastHit2D[] resultsD = new RaycastHit2D[10];
    private RaycastHit2D[] resultsL = new RaycastHit2D[10];
    private RaycastHit2D[] resultsR = new RaycastHit2D[10];

    [SerializeField] private ContactFilter2D characterCollide;
    [SerializeField] private GameObject characterPushObject;
    
    float inputHorizontalMovement;
    bool inputJump, inputDash, inputPunch;

    private Vector2 ssss;

    #endregion


    #region ----------MÉTODOS----------

    private void Awake()
    {
        //Coger todos los cmponentes y variables del scriptable object
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        characterAnimator = GetComponent<Animator>();

        characterName = characterModel.characterName;
        
        characterMaxSpeed = characterModel.characterSpeed;
        characterAcceleration = characterModel.charcterAcceleration;

        characterJumpSpeed = characterModel.characterJumpSpeed;
        characterTotalJumps = characterModel.characterTotalJumps;

        characterFriction = characterModel.characterGroundFriction;

        characterDashSpeed = characterModel.characterDashSpeed;

        characterPunchImpulse = characterModel.characterPunchImpulse;
        characterPunchDuration = characterModel.characterPunchDuration;
        characterPunchStunTime = characterModel.charcaterPunchStunTime;

        characterTotalHits = characterModel.characterTotalHits;
    }

    private void Start()
    {
        //Determinar valor de algunas variables al inicio
        hspd = 0;
        characterDir = 1;
        characterDirection = Quaternion.Euler(0, 0, 0);
        characterCurrentJumps = characterTotalJumps;
        canMoveHorizontal = true;
        canJump = true;
        canDash = true;
        canSlide = true;
        canPunch = true;
    }

    private void Update()
    {
        //Determinar input
        inputHorizontalMovement = Input.GetAxisRaw("P" + playerIndex + "_Horizontal");

        if (!inputJump)
            inputJump = Input.GetButtonDown("P" + playerIndex + "_Jump");

        if (!inputDash)
            inputDash = Input.GetButtonDown("P" + playerIndex + "_Dash");

        if (!inputPunch)
            inputPunch = Input.GetButtonDown("P" + playerIndex + "_Punch");

        //Variables del animator    
        characterAnimator.SetFloat("Speed", Mathf.Abs(hspd));
        characterAnimator.SetBool("Grounded", isInGround);
        characterAnimator.SetBool("WallR", isInWallRight && isSliding);
        characterAnimator.SetBool("WallL", isInWallLeft && isSliding);
    }

    private void FixedUpdate()
    {
        //Determinar estado del personaje
        int nResultsD = rb2d.Cast(Vector2.down, characterCollide, resultsD, 0.02f);
        isInGround = (nResultsD > 0);

        int nResultsL = rb2d.Cast(Vector2.left, characterCollide, resultsL, 0.02f);
        isInWallLeft = ((nResultsL > 0) && !isInGround);

        int nResultsR = rb2d.Cast(Vector2.right, characterCollide, resultsR, 0.02f);
        isInWallRight = ((nResultsR > 0) && !isInGround);

        isInWall = ((nResultsL > 0) || (nResultsR > 0));


        //En el suelo
        if (isInGround)
        {
            //Voltear el sprite al caminar
            if (inputHorizontalMovement>0)
            {
                //spriteRenderer.flipX = false;
                characterDir = 1;
                characterDirection = Quaternion.Euler(0, 0, 0);
                gameObject.transform.rotation = characterDirection;
            }
                
            if (inputHorizontalMovement < 0)
            {
                //spriteRenderer.flipX = true;
                characterDir = -1;
                characterDirection = Quaternion.Euler(0, 180, 0);
                gameObject.transform.rotation = characterDirection;
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
                //spriteRenderer.flipX = true;
                characterDir = 1;
                characterDirection = Quaternion.Euler(0, 0, 0);
                gameObject.transform.rotation = characterDirection;

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
                    isSliding = true;
                    rb2d.velocity = new Vector2(0, 0);
                }
            }

            //Deslizando
            if (isSliding)
            {
                //spriteRenderer.flipX = false;
                characterDir = -1;
                characterDirection = Quaternion.Euler(0, 180, 0);
                gameObject.transform.rotation = characterDirection;

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
            //Voltear el sprite 
            if (inputHorizontalMovement > 0)
            {
                //spriteRenderer.flipX = false;
                characterDir = 1;
                characterDirection = Quaternion.Euler(0, 0, 0);
                gameObject.transform.rotation = characterDirection;
            }
            if (inputHorizontalMovement < 0)
            {
                //spriteRenderer.flipX = true;
                characterDir = -1;
                characterDirection = Quaternion.Euler(0, 180, 0);
                gameObject.transform.rotation = characterDirection;
            }

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

            //Clampear velocidad máxima
            if (Mathf.Abs(hspd) >= characterMaxSpeed)
            {
                hspd = characterMaxSpeed * inputHorizontalMovement;
            }
        }

        //Salto
        if (inputJump) {

            //Poner el salto a false para evitar que se ponga a false en el Update y evitar el error de que no salte algunas veces
            inputJump = false;

            if (canJump)
            {
                //Suelo o doble salto en el aire
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
                    StartCoroutine(DisableInputWallJump(0.1f));

                    //Salir del estado de deslizar
                    isSliding = false;

                    if (isInWallLeft)
                    {
                        isInWallLeft = false;
                        hspd = characterJumpSpeed * Mathf.Cos(Mathf.Deg2Rad * 60);
                        //spriteRenderer.flipX = true;
                        characterDir = 1;
                        characterDirection = Quaternion.Euler(0, 0, 0);
                        gameObject.transform.rotation = characterDirection;


                    }
                    else if (isInWallRight)
                    {
                        isInWallRight = false;
                        hspd = -characterJumpSpeed * Mathf.Cos(Mathf.Deg2Rad * 60);
                        //spriteRenderer.flipX = false;
                        characterDir = -1;
                        characterDirection = Quaternion.Euler(0, 180, 0);
                        gameObject.transform.rotation = characterDirection;
                    }
                }
            }
        }

        //Dash
        if (inputDash)
        {
            //Poner el dash a false para evitar que se ponga a false en el Update y evitar el error de que no dashee algunas veces
            inputDash = false;

            if (canDash)
            {
                hspd = characterDashSpeed * characterDir;
                StartCoroutine(DisableInputDash(0.05f, 1f));
            }
        }

        //Aplicar el movimiento
        rb2d.velocity = new Vector2(hspd, rb2d.velocity.y);

        //Puñetazo
        if (inputPunch)
        {
            //Poner el punch a false para evitar que se ponga a false en el Update y evitar el error de que no lo haga algunas veces
            inputPunch = false;

            if (canPunch)
            {
                StartCoroutine(DisableInputPunch(characterPunchDuration, 1f));

                
                GameObject punch = Instantiate(characterPushObject, transform.position, characterDirection) as GameObject;
                Punch punchController = punch.GetComponent<Punch>();

                punchController.punchIndex = playerIndex;
                punchController.punchForce = characterPunchImpulse;
                punchController.punchDuration = characterPunchDuration;
                punchController.punchStunTime = characterPunchStunTime;
            }
        }
    }

    //Desactivar el control del movimiento durante un tiempo
    public IEnumerator DisableInputWallJump(float time) //, bool hMov, bool jump, bool dash, bool slide, bool punch)
    {
        canMoveHorizontal = false;

        yield return new WaitForSeconds(time);

        canMoveHorizontal = true;
    }

    public IEnumerator DisableInputDash(float time, float cooldown)
    {
        canMoveHorizontal = false;
        canJump = false;
        canDash = false;
        canSlide = false;
        canPunch = false;

        yield return new WaitForSeconds(time);

        canMoveHorizontal = true;
        canJump = true;
        canSlide = true;
        canPunch = true;

        yield return new WaitForSeconds(cooldown);

        canDash = true;
    }

    public IEnumerator DisableInputPunch(float time, float cooldown)
    {
        canMoveHorizontal = false;
        canJump = false;
        canDash = false;
        canSlide = false;
        canPunch = false;

        yield return new WaitForSeconds(time);

        canMoveHorizontal = true;
        canJump = true;
        canDash = true;
        canSlide = true;

        yield return new WaitForSeconds(cooldown);

        canPunch = true;
    }

    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * 3f * characterDir);
    }
}