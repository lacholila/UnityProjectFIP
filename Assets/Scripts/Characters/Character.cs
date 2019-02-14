using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    protected Rigidbody2D rb2d;
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    
    protected enum character { Daniel, Sergio, Xavi }
    protected character characterName;
    protected Animator characterAnimator;

    protected bool grounded;
    protected bool jump;

    private RaycastHit2D[] resultsD = new RaycastHit2D[10];

    protected float acceleration = 50f;

    [SerializeField] protected float playerSpeed, playerJump, playerPush;
    [SerializeField] protected int jumpNum;
    

    protected void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //animator.SetBool("Grounded", grounded);
        //animator.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
        //animator.SetFloat("Air", Mathf.Sign(rb2d.velocity.y));

        if (Input.GetButtonDown("Jump"))
        {
            if (grounded) jump = true;
        }
    }

    private void FixedUpdate()
    {
        /*int nResultsD = rb2d.Cast(Vector2.down, resultsD, 0.01f);
        grounded = (nResultsD > 0);

        for (int i = 0; i < nResultsD; i++)
        {
            if (resultsD[i].transform.gameObject.tag == "MovingPlatform")
            {
                transform.parent = resultsD[i].transform;
            }
        }

        if (!grounded) transform.parent = null;

        Vector3 fixedVelocity = rb2d.velocity;
        fixedVelocity.x *= 0.75f;
        if (grounded) rb2d.velocity = fixedVelocity;*/

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
        }
    }
}