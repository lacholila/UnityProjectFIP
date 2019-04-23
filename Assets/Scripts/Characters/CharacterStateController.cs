using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class CharacterStateController : MonoBehaviour {

    private float h;
    private float speedFactorX;
    private float maxSpeedX;
    private Rigidbody2D rb2d;

    public enum PlayerStates
    {
        InGround, InAir, InWall
    }

    [HideInInspector] public PlayerStates playerState;

    private void Start()
    {
        playerState = PlayerStates.InGround;
    }

    private void Update()
    {
        switch(playerState)
        {
            case PlayerStates.InGround:
                UpdateInGroundState();
                break;
            case PlayerStates.InAir:
                UpdateInAirState();
                break;
        }
    }

    private void FixedUpdate()
    {
        switch (playerState)
        {
            case PlayerStates.InGround:
                FixedUpdateInGroundState();
                break;
            case PlayerStates.InAir:
                FixedUpdateInAirState();
                break;
        }
    }

    private void LateUpdate()
    {
        
    }

    private void UpdateInGroundState()
    {
        h = Input.GetAxisRaw("Horizontal");
    }

    private void UpdateInAirState()
    {

    }

    private void FixedUpdateInGroundState()
    {
        rb2d.AddForce(Vector2.right * h * speedFactorX, ForceMode2D.Force);
        float speedX = Mathf.Clamp(rb2d.velocity.x, -maxSpeedX, maxSpeedX);
        rb2d.velocity = new Vector2(speedX, rb2d.velocity.y);
    }

    private void FixedUpdateInAirState()
    {
        
    }
}
