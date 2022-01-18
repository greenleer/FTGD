using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewPlayer : PhysicsObject
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private float jumpPower;
    private SpriteRenderer playerSprite;
    public int coinsCollected;

    public Text coinsText;

    // Start is called before the first frame update
    void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();

        coinsText = GameObject.Find("Coins").GetComponent<Text>();
        
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
    //Update UI element
    public void UpdateUI()
    {
        //Converts the coinsCollected integer to a string and assigns that string to the text component of the CoinsUI object.
        coinsText.text = coinsCollected.ToString();
        
    }
}