using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewPlayer : PhysicsObject
{
    // Physics -- running, jumping, attacking
    [SerializeField] private float maxSpeed;
    [SerializeField] private float jumpPower;
    [SerializeField] private GameObject attackBox;
    [SerializeField] private float attackDuration;
    public int attackPower = 25;

    // Collectables tracking
    public int coinsCollected;
    public int ammo;

    // Inventory Dictionary that contains the inventory name and sprite, and the UI image to display it.
    public Dictionary<string, Sprite> inventory = new Dictionary<string, Sprite>();
    

    // Health tracking
    public int maxHealth = 100;
    public int health;
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

    private void Awake()
    {
        if (GameObject.Find("New Player"))
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        gameObject.name = "New Player";

        // Sets health bar original Vector2 size.
        healthBarOrigSize = GameManager.Instance.healthBar.rectTransform.sizeDelta;

        // Updates UI: health, coins inventory.
        UpdateUI();

        SetSpawnPosition();

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
        if (targetVelocity.x < -.01)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else if (targetVelocity.x > .01)
        {
            transform.localScale = new Vector2(1, 1);
        }

        //Activates the attack box, when "Fire1" input is pressed, otherwise leaves deactivated.
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(ActivateAttack());
        }

        if (health <= 0)
        {
            Die();
        }
    }

    // Activate attack function
    public IEnumerator ActivateAttack()
    {
        attackBox.SetActive(true);
        yield return new WaitForSeconds(attackDuration);
        attackBox.SetActive(false);
    }


    //Updates UI elements
    public void UpdateUI()
    {
        // Updates the Text UI to match coins collected
        GameManager.Instance.coinsText.text = coinsCollected.ToString();

        //Adjusts health bar width based on the percentage of current health compared to maxhealth
        GameManager.Instance.healthBar.rectTransform.sizeDelta = new Vector2(healthBarOrigSize.x * ((float)health / maxHealth), GameManager.Instance.healthBar.rectTransform.sizeDelta.y);
    }

    public void SetSpawnPosition()
    {
        transform.position = GameObject.Find("SpawnLocation").transform.position;
    }

    // Updates the Inventory UI image associated with the active collected inventory item.
    public void AddInventoryItem(string inventoryName, Sprite image = null)
    {
        inventory.Add(inventoryName, image);
        GameManager.Instance.inventoryItemImage.sprite = inventory[inventoryName];
    }

    public void RemoveInventoryItem(string inventoryName)
    {
        inventory.Remove(inventoryName);
        GameManager.Instance.inventoryItemImage.sprite = GameManager.Instance.InventoryItemBlank;
    }

    //Reload Level1 scene when the Die function is called.
    public void Die()
    {
        // reload the active scene
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);

        // reset player health, coins collected, spawn position, UI, and inventory
        health = maxHealth;
        coinsCollected = 0;
        inventory.Clear(); // this clears the dictionary
        GameManager.Instance.inventoryItemImage.sprite = GameManager.Instance.InventoryItemBlank;
        UpdateUI();
        SetSpawnPosition();
    }
}