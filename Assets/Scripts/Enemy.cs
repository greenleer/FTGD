using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PhysicsObject
{
    [SerializeField] private float maxSpeed;
    private int direction = 1;

    //Ledge Raycast detection
    private RaycastHit2D rightLedgeRaycastHit;
    private RaycastHit2D leftLedgeRaycastHit;

    //Wall Raycast detection
    private RaycastHit2D rightWallRaycastHit;
    private RaycastHit2D leftWallRaycastHit;

    //Serialized Raycast modification
    [SerializeField] private Vector2 raycastOffset;
    [SerializeField] private float raycastLength = 2;
    [SerializeField] private LayerMask raycastLayerMask;

    [SerializeField] private int attackPower;
    public int enemyHealth = 100;
    [SerializeField] private int maxEnemyHealth = 100;

    private SpriteRenderer enemySprite;

    // Start is called before the first frame update
    void Start()
    {
        enemySprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        targetVelocity = new Vector2(maxSpeed * direction, 0);

        //Check for right ledge
        rightLedgeRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x + raycastOffset.x, transform.position.y + raycastOffset.y), Vector2.down, raycastLength);
        Debug.DrawRay(new Vector2(transform.position.x + raycastOffset.x, transform.position.y + raycastOffset.y), Vector2.down * raycastLength, Color.yellow);

        //Check for left ledge
        leftLedgeRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x - raycastOffset.x, transform.position.y + raycastOffset.y), Vector2.down, raycastLength);
        Debug.DrawRay(new Vector2(transform.position.x - raycastOffset.x, transform.position.y + raycastOffset.y), Vector2.down * raycastLength, Color.blue);

        //Check for right wall
        rightWallRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.right, raycastLength, raycastLayerMask);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), Vector2.right * raycastLength, Color.yellow);

        //Check for left wall
        leftWallRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.left, raycastLength, raycastLayerMask);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), Vector2.left * raycastLength, Color.blue);

        //right ledge raycast debug
        if (rightLedgeRaycastHit.collider == null || rightWallRaycastHit.collider != null)
        {
            direction = -1;
            enemySprite.flipX = true;

        }

        //left raycast debug
        if (leftLedgeRaycastHit.collider == null || leftWallRaycastHit.collider != null)
        {
            direction = 1;
            enemySprite.flipX = false;
        }

        //Destroy me if my health falls to 0;
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    //If I collide with the player, deal damage, and update the Health UI.
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject == NewPlayer.Instance.gameObject)
        {
            NewPlayer.Instance.health -= attackPower;
            NewPlayer.Instance.UpdateUI();
        }
    }
}
