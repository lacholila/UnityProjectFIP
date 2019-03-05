using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    #region ----------VARIABLES----------

    [SerializeField] private CharacterModel characterModel;

    private Animator characterAnimator;
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;

    private string characterName;

    private float characterSpeed, characterAcceleration;
    private float characterJumpSpeed;
    private float characterFallSpeed;
    private float characterSlideSpeed, characterSlideAcceleration;
    private float characterGroundFriction, characterAirFriction;
    private float characterGroundRatio, characterAirRatio;
    private float characterPush;

    private float limitedSpeedX, limitedSpeedY;
    private float hspd, vspd;

    private int characterTotalJumps, characterCurrentJumps;
    private int characterTotalHits, characterCurrentHits;

    private bool isInGround, isInWallRight, isInWallLeft;

    private RaycastHit2D[] resultsD = new RaycastHit2D[10];
    private RaycastHit2D[] resultsL = new RaycastHit2D[10];
    private RaycastHit2D[] resultsR = new RaycastHit2D[10];

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

        characterFallSpeed = characterModel.characterFallSpeed;

        characterSlideSpeed = characterModel.characterSlideSpeed;
        characterSlideAcceleration = characterModel.characterSlideAcceleration;

        characterGroundFriction = characterModel.characterGroundFriction;
        characterAirFriction = characterModel.characterAirFriction;

        characterPush = characterModel.characterPush;

        characterTotalHits = characterModel.characterTotalHits;
    }

    private void Start()
    {
        //Determinar valor de algunas variables al inicio
        characterCurrentJumps = characterTotalJumps;
        isInGround = true;
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
        inputJump = (Input.GetButtonDown("Jump"));
    }

    private void FixedUpdate()
    {
        //Determinar estado del personaje
        int nResultsD = rb2d.Cast(Vector2.down, resultsD, 0.01f);
        isInGround = (nResultsD > 0);

        int nResultsL = rb2d.Cast(Vector2.left, resultsL, 0.01f);
        isInWallLeft = ((nResultsL > 0) && !isInGround);

        int nResultsR = rb2d.Cast(Vector2.right, resultsR, 0.01f);
        isInWallRight = ((nResultsR > 0) && !isInGround);

        if (inputHorizontalMovement != 0)
        {
            hspd += characterAcceleration * inputHorizontalMovement;

            if (Mathf.Abs(hspd) >= characterSpeed)
            {
                hspd = characterSpeed * inputHorizontalMovement;
            }
        }
        else
        {
            if (Mathf.Abs(hspd) >= characterGroundFriction)
            {
                hspd -= Mathf.Sign(rb2d.velocity.x) * characterGroundFriction;
            }
            else
            {
                hspd = 0;
            }
        }

        vspd = -2;
        rb2d.velocity = new Vector2(hspd, vspd);
        




        /*

        //Estado de "en el suelo"

        //Caminar
        if (inputHorizontalMovement != 0)
        {
            rb2d.AddForce(Vector2.right * characterAcceleration * inputHorizontalMovement * Time.deltaTime);

            limitedSpeedX = Mathf.Clamp(rb2d.velocity.x, -characterSpeed, characterSpeed);
            limitedSpeedY = Mathf.Clamp(rb2d.velocity.y, -characterFallSpeed, characterJumpSpeed);

            rb2d.velocity = new Vector2(limitedSpeedX, limitedSpeedY);
        }
        else
        {
            if (rb2d.velocity.x > characterGroundFriction)
            {

            }
        }
        

        transform.localScale = new Vector2(Mathf.Sign(inputHorizontalMovement), transform.localScale.y);
            
        //Salto
        if (inputJump && characterCurrentJumps > 0)
        {
            //characterCurrentJumps--;
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            rb2d.AddForce(Vector2.up * characterJumpSpeed, ForceMode2D.Impulse);
        }

        //Limitar velocidades
        
        
        /*
        //Estado de "en una pared"
        else if (isInWallRight)
        {
            //Deslizarse
            if (inputHorizontalMovement == 0)
            {
                rb2d.AddForce(Vector2.down * characterSlideAcceleration);

                //Limitar velocidades
                limitedSpeedX = 0f;
                limitedSpeedY = Mathf.Clamp(rb2d.velocity.y, -characterSlideSpeed, 0f);
            }
        }


        //Estado de "en el aire"

        //Aplicar
        */
    }

    #endregion
}












/*
public class Character : MonoBehaviour {

    protected Rigidbody2D rb2d;
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    
    protected enum character { Daniel, Sergio, Xavi }
    protected character characterName;
    protected Animator characterAnimator;

    protected bool grounded;
	protected bool walltouch;
    protected bool walltouchL;

    protected bool jump;

    private RaycastHit2D[] resultsD = new RaycastHit2D[10];
	private RaycastHit2D[] resultsL = new RaycastHit2D[10];
	private RaycastHit2D[] resultsR = new RaycastHit2D[10];

    protected float acceleration = 50f;
    protected int jumps;

    [SerializeField] protected float playerSpeed, playerJump, playerPush;
    [SerializeField] protected int jumpNum;
    

    protected void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        jumps = jumpNum;
    }

    private void Update()
    {
        animator.SetBool("Grounded", grounded);
        animator.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
        animator.SetFloat("Air", Mathf.Sign(rb2d.velocity.y));
		animator.SetBool("WallTouch", walltouch);
        animator.SetBool("WallTouchL", walltouchL);

        if (Input.GetButtonDown("Jump"))
        {
			if ((grounded) || (walltouch) || (jumps > 0))
                jump = true;
        }



    }

    private void FixedUpdate()
    {
        int nResultsD = rb2d.Cast(Vector2.down, resultsD, 0.01f);
        grounded = (nResultsD > 0);

		int nResultsL = rb2d.Cast(Vector2.left, resultsL, 0.01f);
		walltouchL = (nResultsL > 0);

		if (nResultsL > 0)
		print ("izquierda");

		int nResultsR = rb2d.Cast(Vector2.right, resultsR, 0.01f);
		walltouch = (nResultsR > 0);



		if (nResultsR > 0) {
			spriteRenderer.flipX = true;
		} else {
			spriteRenderer.flipX = false;
		}

		if (nResultsL > 0) {
			spriteRenderer.flipX = true;
		}


        Vector3 fixedVelocity = rb2d.velocity;
        fixedVelocity.x *= 0.75f;
        if (grounded)
        {
            rb2d.velocity = fixedVelocity;
            jumps = jumpNum;
        }

        float h = Input.GetAxis("Horizontal");
        rb2d.AddForce(Vector2.right * acceleration * h);
        float limitedSpeed = Mathf.Clamp(rb2d.velocity.x, -playerSpeed, playerSpeed);
        rb2d.velocity = new Vector2(limitedSpeed, rb2d.velocity.y);

        if (h > 0.01f) transform.localScale = new Vector3(1f, 1f, 1f);
        if (h < -0.01f) transform.localScale = new Vector3(-1f, 1f, 1f);

        if (jump)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            rb2d.AddForce(Vector2.up * playerJump, ForceMode2D.Impulse);
            jump = false;
            jumps--;
        }
    }
}

*/
