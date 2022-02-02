using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewPlayer : PhysicsObject
{
    // Physics -- running and jumping
    [SerializeField] private float maxSpeed;
    [SerializeField] private float jumpPower;

    // Reference to the sprite dedicated to the player object
    private SpriteRenderer playerSprite;

    // Coin collection tracking, and the UI text that displays it
    public int coinsCollected;
    public Text coinsText;

    // Ammo collection tracking, and the UI text that displays is
    public int ammo;

    // Inventory Dictionary that contains the inventory name and sprite, and the UI image to display it.
    public Dictionary<string, Sprite> inventory = new Dictionary<string, Sprite>();
    public Image inventoryItemImage;
    public Sprite InventoryItemBlank;

    // Health tracking, UI elements, and the Vector2 that represents 100% health bar size.
    public int maxHealth = 100;
    public int health;
    public Image healthBar;
    [SerializeField] private Vector2 healthBarOrigSize;

    //Player Singleton instatiation
    private static NewPlayer instance;
    public static NewPlayer Instance
    {
        get
        {
            if (instance == null) instance = GameObject.FindObjectOfType<NewPlayer>();
            return instance;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        /*
        // Sets player health to 100;
        health = 100;
        */

        // Sets health bar original Vector2 size.
        healthBarOrigSize = healthBar.rectTransform.sizeDelta;

        // Updates UI -- primarily to reflect 100 health at start.
        UpdateUI();

    }

    // Update is called once per frame
    void Update()
    {
        // Moves player along horizontal axis
        targetVelocity = new Vector2(Input.GetAxis("Horizontal") * maxSpeed, 0);

        // If the player presses the spacebar, and is grounded, then jump.
        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpPower;
        }

        // Flip the orientation of the sprite if the player moves to the left.
        if (Input.GetAxis("Horizontal") < 0)
        {
            playerSprite.flipX = true;
        }

        // Utilize default sprite orientation if the player moves to the right.
        if (Input.GetAxis("Horizontal") > 0)
        {
            playerSprite.flipX = false;
        }
    
    }
    //Updates UI elements
    public void UpdateUI()
    {
        // Updates the Text UI to match coins collected
        coinsText.text = coinsCollected.ToString();

        //Adjusts health bar width based on the percentage of current health compared to maxhealth
        healthBar.rectTransform.sizeDelta = new Vector2(healthBarOrigSize.x * ((float)health / maxHealth), healthBar.rectTransform.sizeDelta.y);
    }

    // Updates the Inventory UI image associated with the active collected inventory item.
    public void AddInventoryItem(string inventoryName, Sprite image = null)
    {
        inventory.Add(inventoryName, image);
        inventoryItemImage.sprite = inventory[inventoryName];
    }

    public void RemoveInventoryItem(string inventoryName)
    {
        inventory.Remove(inventoryName);
        inventoryItemImage.sprite = InventoryItemBlank;
    }
}