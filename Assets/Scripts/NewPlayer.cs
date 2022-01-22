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
    public int ammo;

    public Dictionary<string, Sprite> inventory = new Dictionary<string, Sprite>();
    public Image inventoryItemImage;

    public int maxHealth = 100;
    public int health;
    public Image healthBar;
    [SerializeField] private Vector2 healthBarOrigSize;

    // Start is called before the first frame update
    void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();

        health = 100;
        healthBarOrigSize = healthBar.rectTransform.sizeDelta;

        UpdateUI();

    }

    // Update is called once per frame
    void Update()
    {
        // Moves player along horizontal axis
        targetVelocity = new Vector2(Input.GetAxis("Horizontal") * maxSpeed, 0);

        // Controls player jump
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
        // Updates the Text UI to match coins collected
        coinsText.text = coinsCollected.ToString();

        //Adjusts health bar width based on the percentage of current health compared to maxhealth
        healthBar.rectTransform.sizeDelta = new Vector2(healthBarOrigSize.x * ((float)health / maxHealth), healthBar.rectTransform.sizeDelta.y);
    }

    public void AddInventoryItem(string inventoryName, Sprite image)
    {
        inventory.Add(inventoryName, image);
        inventoryItemImage.sprite = inventory[inventoryName];
    }
}