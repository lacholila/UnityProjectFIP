using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{

    #region ----------VARIABLES----------

    public CharacterModel characterModel;

    private CharacterWeaponController characterWeaponController;
    
    public int playerIndex;

    private SpriteRenderer spriteRenderer;
    
    private Rigidbody2D rb2d;
    private Animator characterAnimator;
        
    private string characterName;
    
    private float characterMaxSpeed, characterAcceleration, characterFriction, characterGravity;
    private float characterJumpSpeed;
    private float characterPunchImpulse, characterPunchDuration, characterPunchStunTime;
    private float characterDashSpeed;
    float cantidadMaximaVelocidadTemporalItem;
    float cantidadMaximaSaltoTemporalItem;
    float cantidadPuñetazoTemporalItem;

    public float hspd;
    public int characterDir;
    private Quaternion characterDirection;

    private int characterTotalJumps, characterCurrentJumps;
    public int characterTotalHits, characterCurrentHits;

    private bool isInGround, isInWall, isInWallRight, isInWallLeft, isSliding, isInHit, isInStun;
    private bool canMoveHorizontal, canJump, canDash, canSlide, canPunch;

    private RaycastHit2D[] resultsD = new RaycastHit2D[10];
    private RaycastHit2D[] resultsL = new RaycastHit2D[10];
    private RaycastHit2D[] resultsR = new RaycastHit2D[10];

    [SerializeField] private ContactFilter2D characterCollide;
    [SerializeField] private GameObject characterPushObject;

    float inputHorizontalMovement;
    public bool inputJump, inputDash, inputPunch, inputUseWeapon, inputPickWeapon;

    public GameObject particlesGround, particlesWall, particlesDash, particlesDeath;

    Vector3 initialPosition;

    #endregion


    #region ----------MÉTODOS----------

    private void Awake()
    {
        //Coger todos los cmponentes y variables del scriptable object
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        characterAnimator = GetComponent<Animator>();
        characterWeaponController = GetComponent<CharacterWeaponController>();

        characterName = characterModel.characterName;
        
        characterMaxSpeed = characterModel.characterSpeed;
        characterAcceleration = 0.5f;
        characterFriction = 0.2f;

        characterJumpSpeed = characterModel.characterJumpSpeed;
        characterTotalJumps = characterModel.characterTotalJumps;

        characterDashSpeed = characterModel.characterDashSpeed;

        characterPunchImpulse = characterModel.characterPunchImpulse;
        characterPunchDuration = 0.1f;
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

        initialPosition = transform.position;
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

        inputUseWeapon = Input.GetButtonDown("P" + playerIndex + "_Use");

        inputPickWeapon = Input.GetButtonDown("P" + playerIndex + "_Pick");   
        
       // Input.GetKeyDown(KeyCode.jo)

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
            //Voltear el objeto
            if (inputHorizontalMovement > 0)
            {
                characterDir = 1;
                characterDirection = Quaternion.Euler(0, 0, 0);
                gameObject.transform.rotation = characterDirection;
            }

            if (inputHorizontalMovement < 0)
            {
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
                if (!isSliding && canSlide)
                {
                    isSliding = true;
                    rb2d.velocity = new Vector2(0, 0);
                }
            }

            //Deslizando
            if (isSliding)
            {
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
                characterGravity = 0.2f;
            }
        }
        //En una pared (derecha)
        else if (isInWallRight)
        {
            //Entrar en el estado de deslizar
            if (hspd > 0)
            {
                hspd = 0;

                if (!isSliding && canSlide)
                {
                    isSliding = true;
                    rb2d.velocity = new Vector2(0, 0);
                }
            }

            //Deslizando
            if (isSliding)
            {
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
                characterGravity = 0.2f;
            }
        }
        //En el aire
        else
        {
            //Voltear el objeto 
            if (inputHorizontalMovement > 0)
            {
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
        else
        {
            hspd = rb2d.velocity.x;
        }

        //Salto
        if (inputJump)
        {
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
                        characterDir = 1;
                        characterDirection = Quaternion.Euler(0, 0, 0);
                        gameObject.transform.rotation = characterDirection;


                    }
                    else if (isInWallRight)
                    {
                        isInWallRight = false;
                        hspd = -characterJumpSpeed * Mathf.Cos(Mathf.Deg2Rad * 60);
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

                GameObject punch = Instantiate(characterPushObject, transform) as GameObject;
                CharacterPunch punchController = punch.GetComponent<CharacterPunch>();

                punchController.punchIndex = playerIndex;
                punchController.punchForce = characterPunchImpulse;
                punchController.punchDuration = characterPunchDuration;
                punchController.punchStunTime = characterPunchStunTime;
            }
        }

        //Al morir, las coordenadas son las de la cámara para no interferir en el punto medio
        if (characterCurrentHits >= characterTotalHits)
        {
            gameObject.transform.position = Camera.main.gameObject.transform.position;
            gameObject.SetActive(false);
        }

        //Particulitas
        particlesGround.SetActive(isInGround && Mathf.Abs(hspd) > 0.1f);
        particlesWall.SetActive(isSliding);
    }

    //Detectar golpe, caída o explosión
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Punch")
        {
            CharacterPunch otherPunch = other.GetComponent<CharacterPunch>();
            StartCoroutine(DisableInputActions(otherPunch.punchStunTime));
            rb2d.AddForce(new Vector2((otherPunch.punchForce * Mathf.Cos(Mathf.Deg2Rad * 45) * otherPunch.transform.right.x), otherPunch.punchForce * Mathf.Sin(Mathf.Deg2Rad * 45)), ForceMode2D.Impulse);
        }

        if (other.gameObject.tag == "Void")
        {
            LoseLife();
        }

        if (other.gameObject.tag == "Explosion")
        {  
            StartCoroutine(DisableInputActions(1f));
        }

        if (other.gameObject.tag == "Cafe")
        {
            cantidadMaximaVelocidadTemporalItem = characterMaxSpeed;
            characterMaxSpeed += 5;
            spriteRenderer.color = new Color(0.3F, 0.3F, 1F, 1F);
            Invoke("DelayPowerUpCafe", 3f);
        }

        if (other.gameObject.tag == "Snac")
        {
            cantidadPuñetazoTemporalItem = characterPunchImpulse;
            characterPunchImpulse += 5;
            spriteRenderer.color = new Color(0.3F, 1F, 0.3F, 1F);
            Invoke("DelayPowerUpSnac", 3f);
        }

        if (other.gameObject.tag == "Cola")
        {
            cantidadMaximaSaltoTemporalItem = characterJumpSpeed;
            characterJumpSpeed += 4;
            spriteRenderer.color = new Color(1F, 0.3F, 0.3F, 1F);
            Invoke("DelayPowerUpCola", 3f);
        }

        if (other.gameObject.tag == "Carnet")
        {
            
        }

        if (other.gameObject.tag == "Tenfe")
        {
            if (characterCurrentHits > 0)
            {
                characterCurrentHits--;
                spriteRenderer.color = new Color(1F, 0.6F, 0F, 1F);
                Invoke("DelayPowerUpCola", 0.3f);
            }
        }

    }

    //Delay de power ups
    void DelayPowerUpCafe()
    {
        characterMaxSpeed = cantidadMaximaVelocidadTemporalItem;
        spriteRenderer.color = Color.white;
    }

    //Delay de power ups
    void DelayPowerUpSnac()
    {
        characterPunchImpulse = cantidadPuñetazoTemporalItem;
        spriteRenderer.color = Color.white;
    }

    //Delay de power ups
    void DelayPowerUpCola()
    {
        characterJumpSpeed = cantidadMaximaSaltoTemporalItem;
        spriteRenderer.color = Color.white;
    }

    //Delay de power ups
    void DelayPowerUpTenfe()
    {
        spriteRenderer.color = Color.white;
    }

    //Quitar vida
    public void LoseLife()
    {
        characterCurrentHits++;

        particlesDeath.SetActive(true);

        characterWeaponController.tienesUnObjeto = false;
        characterWeaponController.weaponName = "";
        characterWeaponController.weaponSprite = null;
        characterWeaponController.instantiateObject = null;

        characterWeaponController.weaponIconPosition.gameObject.GetComponent<SpriteRenderer>().sprite = characterWeaponController.weaponSprite;

        StartCoroutine(Respawn());
    }

    //Respawn
    public IEnumerator Respawn()
    {
        if (characterCurrentHits < characterTotalHits)
        {
            StartCoroutine(DisableInputActions(2f));
            rb2d.velocity = Vector2.zero;

            yield return new WaitForSeconds(2f);

            transform.position = initialPosition;
            rb2d.velocity = Vector2.zero;
            particlesDeath.SetActive(false);
        }
    }

    //Desactivar el control del movimiento durante un tiempo
    public IEnumerator DisableInputActions(float time)
    {
        canMoveHorizontal = false;
        canSlide = false;
        canJump = false;
        canDash = false;
        canPunch = false;

        yield return new WaitForSeconds(time);

        canMoveHorizontal = true;
        canSlide = true;
        canJump = true;
        canDash = true;
        canPunch = true;
    }

    public IEnumerator DisableInputWallJump(float time) 
    {
        canMoveHorizontal = false;

        yield return new WaitForSeconds(time);

        canMoveHorizontal = true;
    }

    public IEnumerator DisableInputDash(float time, float cooldown)
    {
        canMoveHorizontal = false;
        canDash = false;
        canPunch = false;
        particlesDash.SetActive(true);

        yield return new WaitForSeconds(time);

        canMoveHorizontal = true;
        canPunch = true;

        yield return new WaitForSeconds(cooldown);

        canDash = true;
        particlesDash.SetActive(false);
    }

    public IEnumerator DisableInputPunch(float time, float cooldown)
    {
        canMoveHorizontal = false;
        canPunch = false;
        canDash = false;

        yield return new WaitForSeconds(time);

        canMoveHorizontal = true;
        canDash = true;

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
