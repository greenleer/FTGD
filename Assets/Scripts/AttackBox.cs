using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBox : MonoBehaviour
{

    [SerializeField] private int attackBoxDamage;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>())
        {
            Debug.Log("Enemy punched!");
            collision.GetComponent<Enemy>().enemyHealth -= NewPlayer.Instance.attackPower;
        }
    }
}
