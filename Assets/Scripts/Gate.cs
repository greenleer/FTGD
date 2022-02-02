using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] string requiredInventoryItemString;

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
        if (collision.gameObject == NewPlayer.Instance.gameObject)
        {
            //If the player inventory has "key1", destroy the gate.
            if (NewPlayer.Instance.inventory.ContainsKey(requiredInventoryItemString))
            {
                NewPlayer.Instance.RemoveInventoryItem(requiredInventoryItemString);
                Destroy(gameObject);
            }
        }
    }
}
