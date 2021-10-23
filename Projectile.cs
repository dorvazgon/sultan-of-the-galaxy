using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Attributes
    [SerializeField] private float moveSpeed = 20f;

    // Owner Check
    private bool isEnemyProjectile = false;

    // Player Component
    Player player;
    


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>().GetComponent<Player>();

        if (isEnemyProjectile == true)
        {
            moveSpeed = 5f;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (isEnemyProjectile == false)
        {
            MoveUp();
        }
        else if (isEnemyProjectile == true)
        {
            MoveDown();
        }
    }

    private void MoveUp()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

        // Destroy When Out Of The + Y Boundary

        float yBoundary = 5.45f;

        if (transform.position.y > yBoundary)
        {
            Destroy(this.gameObject);
        }
    }

    private void MoveDown()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);

        // Destroy When Out Of The - Y Boundary 

        float yBoundary = 5.45f;

        if (transform.position.y < -yBoundary)
        {
            Destroy(this.gameObject);
        }

}

    public void AssignToTheEnemy()
    {
        isEnemyProjectile = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnemyBomb")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        else if (other.tag == "EnemyProjectile")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        else if (other.tag == "Shield" && isEnemyProjectile == true)
        {
            player.DeactivateShield();
            Destroy(this.gameObject);
        }
    }

}
