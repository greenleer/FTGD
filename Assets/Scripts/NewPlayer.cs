using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayer : PhysicsObject
{
    [SerializeField] private float maxSpeed = 1;
    [SerializeField] private float jumpPower = 10;
    private SpriteRenderer playerSprite;

    // Start is called before the first frame update
    void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // Moves the player along the X axis, based on player input
        targetVelocity = new Vector2(Input.GetAxis("Horizontal") * maxSpeed, 0);

        // If the player presses "Jump", and the player is grounded, set the Y velocity to a jump power value, and flip the sprite upside down.
        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpPower;
        }

        // Flip the orientation of the sprite if the player moves to the left.
        if (Input.GetAxis("Horizontal") < 0)
        {
            playerSprite.flipX = true;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            playerSprite.flipX = false;
        }
    
    }
}