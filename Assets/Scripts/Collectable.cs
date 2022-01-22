using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    // Creates a enumeration of collectable item types called Item Type
    enum ItemType { Coin, Health, Ammo, Inventory }
    [SerializeField] private ItemType itemType;

    // Instatiates a NewPlayer class for the sake of accessing the NewPlayer script.
    private NewPlayer newPlayer;

    // Start is called before the first frame update
    void Start()
    {
        newPlayer = GameObject.Find("Player").GetComponent<NewPlayer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Determines if the player touches the collectable
        if (collision.gameObject.name == "Player")
        {
           if (itemType == ItemType.Coin)
            {
                newPlayer.coinsCollected += 1;
            }
           else if (itemType == ItemType.Health)
            {
                if (newPlayer.health < 100)
                {
                    newPlayer.health += 1;
                }
            }
           else if (itemType == ItemType.Ammo)
            {
                newPlayer.ammo += 1;
            }
           else
            {
                newPlayer.AddInventoryItem(gameObject.name, gameObject.GetComponent<SpriteRenderer>().sprite);
                Debug.Log(gameObject.name);
            }
            newPlayer.UpdateUI();
            Destroy(gameObject);
        }
    }
}
