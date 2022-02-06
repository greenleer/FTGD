using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    // Creates a enumeration of collectable item types
    enum ItemType { Coin, Health, Ammo, Inventory }

    // Provides Unity editor access to the ItemType enum within
    [SerializeField] private ItemType itemType;

    [SerializeField] private string inventoryStringName;
    [SerializeField] private Sprite inventorySprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Determines if the player touches the collectable
        if (collision.gameObject == NewPlayer.Instance.gameObject)
        {
           if (itemType == ItemType.Coin)
            {
                // Increments coinsCollected within the newPlayer script
                NewPlayer.Instance.coinsCollected += 1;
            }
           else if (itemType == ItemType.Health)
            {
                // Increments health within the newPlayer script, if the value is less than 100
                if (NewPlayer.Instance.health < 100)
                {
                    NewPlayer.Instance.health += 10;
                }
            }
           else if (itemType == ItemType.Ammo)
            {
                // Increments ammo within the newPlayer script
                NewPlayer.Instance.ammo += 1;
            }
           else
            {
                // Passes the collectible name, and sprite to the AddInventoryItem function in the newPlayer script
                NewPlayer.Instance.AddInventoryItem(inventoryStringName, inventorySprite);
            }

            // Updates the UI elements in the newPlayer script, based on interaction with collectable
            NewPlayer.Instance.UpdateUI();

           // Destroys the collectable game object for the sake of housekeeping
           Destroy(gameObject);
        }
    }
}
