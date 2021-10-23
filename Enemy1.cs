using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    // Audio
    AudioSource audioSource;

    [SerializeField] AudioClip fireSound;
    [SerializeField] AudioClip takeDamageSound;

    // Prefabs
    [SerializeField] private GameObject projectilePrefab;

    // Component
    Player player;
    GameManager gameManager;

    // Attributes
    private float moveSpeed = 2f;
    private float nextFire;
    private float fireRate = 3f;
    private int health = 2;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        player = FindObjectOfType<Player>().GetComponent<Player>();

        // Null Checks

        if (projectilePrefab == null)
        {
            Debug.LogError("Projectile on Enemy1 is NULL!");
        }

        if (audioSource == null)
        {
            Debug.LogError("AudioSource on Enemy1 is NULL!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Die();

        if (nextFire < Time.time)
        {
            Fire();
            
        }
        
    }

    private void Move()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);

        // Destroy When Out Of Boundaries

        if (transform.position.y < -5.7f)
        {
            Destroy(this.gameObject);
        }
    }

    private void Fire()
    {
        audioSource.PlayOneShot(fireSound);
        nextFire = Time.time + fireRate;
        Vector3 projectileOffset = new Vector3(0.05f, -0.41f, 0);
        GameObject projectile = Instantiate(projectilePrefab, transform.position + projectileOffset, Quaternion.identity);
        Projectile projectileComponent = projectile.GetComponent<Projectile>();
        projectileComponent.AssignToTheEnemy();
    }

    private void TakeDamage()
    {
        audioSource.PlayOneShot(takeDamageSound);
        health--;
    }

    private void Die()
    {
        if (health <= 0)
        {
            player.GetPoint(100);
            gameManager.BlowUpEffect(this.transform.position);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerProjectile")
        {
            TakeDamage();
            Destroy(other.gameObject);
        }
        else if (other.tag == "Player")
        {
            TakeDamage();
        }
        else if (other.tag == "PlayerBomb")
        {
            player.GetPoint(100);
            gameManager.BlowUpEffect(this.transform.position);
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        else if (other.tag == "Shield")
        {
            player.GetPoint(200);
            gameManager.BlowUpEffect(this.transform.position);
            player.DeactivateShield();
            Destroy(this.gameObject);
        }
    }
}
