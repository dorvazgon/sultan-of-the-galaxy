using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField] AudioClip fireSound;
    [SerializeField] AudioClip takeDamageSound;

    // Prefabs
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject bombPrefab;

    // Components
    Player player;
    GameManager gameManager;

    // Attributes
    private float moveSpeed = 1.5f;
    private int health = 3;
    
    private float nextFire;
    private float fireRate = 4f;

    private float nextBomb;
    private float bombRate = 6f;



    // Start is called before the first frame update
    void Start()
    {
        // GetComponents
        audioSource = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        player = FindObjectOfType<Player>().GetComponent<Player>();

        if (projectilePrefab == null)
        {
            Debug.LogError("Projectile is NULL");
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

        if (nextBomb < Time.time)
        {
            LaunchBomb();
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

    private void Die()
    {
        if (health <= 0)
        {
            player.GetPoint(300);
            gameManager.BlowUpEffect(this.transform.position);
            Destroy(this.gameObject);
        }
    }

    private void TakeDamage()
    {
        audioSource.PlayOneShot(takeDamageSound);
        health--;
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
            player.GetPoint(300);
            gameManager.BlowUpEffect(this.transform.position);
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        else if (other.tag == "Shield")
        {
            player.GetPoint(300);
            player.DeactivateShield();
            gameManager.BlowUpEffect(this.transform.position);
            Destroy(this.gameObject);
        }
    }

    private void Fire()
    {
        audioSource.PlayOneShot(fireSound);
        nextFire = Time.time + fireRate;
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile[] projectileComponents = projectile.GetComponentsInChildren<Projectile>();

        for (int i = 0; i < projectileComponents.Length; i++)
        {
            projectileComponents[i].AssignToTheEnemy();
        }

    }

    private void LaunchBomb()
    {
        audioSource.PlayOneShot(fireSound);
        nextBomb = Time.time + bombRate;

        Vector3 bombOffset = new Vector3(0.029f, -0.61f, 0);

        GameObject bomb = Instantiate(bombPrefab, transform.position + bombOffset, Quaternion.identity);
        Bomb bombComponent = bomb.GetComponent<Bomb>();
        bombComponent.AssignToEnemyBomb();
    }


}
