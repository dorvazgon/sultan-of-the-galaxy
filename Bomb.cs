using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    // Attributes
    private float moveSpeed = 4f;

    // Owner Check
    private bool isEnemyBomb = false;

    // Player Component
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>().GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnemyBomb == true)
        {
            MoveDown();
        }
        else if (isEnemyBomb == false)
        {
            MoveUp();
        }
    }

    private void MoveUp()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

        // Destroy When Out Of Boundaries

        if (transform.position.y > 5.45f)
        {
            Destroy(this.gameObject);
        }
    }

    private void MoveDown()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);

        // Destroy When Out Of Boundaries

        if (transform.position.y < -5.45f)
        {
            Destroy(this.gameObject);
        }
    }

    public void AssignToEnemyBomb()
    {
        isEnemyBomb = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Shield" && isEnemyBomb == true)
        {
            player.DeactivateShield();
            Destroy(this.gameObject);

        }
    }
}
