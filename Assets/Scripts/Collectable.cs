using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    // Creates a enumeration of collectable item types called Item Type
    enum ItemType { Coin, Health, Ammo }
    [SerializeField] private ItemType itemType;

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
        if (collision.gameObject.name == "Player")
        {
            NewPlayer newPlayer = GameObject.Find("Player").GetComponent<NewPlayer>();

            // Increments coinsCollected count in the NewPlayer script.
            newPlayer.coinsCollected += 1;

            // Calls the UpdateUI function in the NewPlayer script.
            newPlayer.UpdateUI();

            // Destroys the coin gameObject.
            Destroy(gameObject);
        }
    }
}
