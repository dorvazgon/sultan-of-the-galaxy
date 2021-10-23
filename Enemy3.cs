using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    // Attributes
    private float moveSpeed = 3f;

    // Player Component
    GameObject playerGameObject;
    Player player;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        playerGameObject = FindObjectOfType<Player>().gameObject;
        player = playerGameObject.GetComponent<Player>();
        
    }

    // Update is called once per frame
    void Update()
    {
        DiveIntoTheEnemy();
        DestroyWhenOutOfBoundaries();
    }

    private void DiveIntoTheEnemy()
    {
        // When the player is alive dive into it. If not, just move to down. 
        if (playerGameObject != null)
        {
            float speed = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, playerGameObject.transform.position, speed);
        }
        else
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }
        
    }

    private void DestroyWhenOutOfBoundaries()
    {
        if (transform.position.y < -5.45f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerBomb" || other.tag == "PlayerProjectile")
        {
            player.GetPoint(100);
            gameManager.BlowUpEffect(this.transform.position);
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        else if (other.tag == "Shield")
        {
            player.GetPoint(100);
            gameManager.BlowUpEffect(this.transform.position);
            player.DeactivateShield();
            Destroy(this.gameObject);
        }
    }
}
