using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    // Audio
    AudioSource audioSource;
    [SerializeField] AudioClip fireSound;
    [SerializeField] AudioClip powerUpSound;
    [SerializeField] AudioClip takeDamageSound;

    // Prefabs
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private GameObject shieldPrefab;
    
    // Attributes
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] public int health = 3;

    private float nextFire;
    private float fireRate = 0.10f;
    private float bombPowerUpTimer = 5f;
    public int point;
    
    // PowerUps
    private bool isBombPowerUpActive = false;
    private bool isShieldActive = false;

    // Start is called before the first frame update
    void Start()
    {
        // GetComponent
        audioSource = GetComponent<AudioSource>();

        // Start Position
        transform.position = new Vector3(0, -4, 0);

        // Null Checks
        if (projectilePrefab == null)
        {
            Debug.LogError("Projectile Object Is NULL!");
        }

        if (bombPrefab == null)
        {
            Debug.LogError("Bomb Object is NULL!");
        }

        if (shieldPrefab == null)
        {
            Debug.LogError("Shield Object is NULL!");
        }

        if (audioSource == null)
        {
            Debug.LogError("AudioSource on Player is NULL!");
        }

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Die();

        if (Input.GetKeyDown(KeyCode.Space) && nextFire < Time.time)
        {
            Fire();

            if (isBombPowerUpActive == true)
            {
                LaunchBomb();
            }
        }
    }

    private void Move()
    {
        // Movement By WASD/Arrow Keys

        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(xAxis, yAxis, 0);

        transform.Translate(direction * moveSpeed * Time.deltaTime);

        // ==== Make The Player Play Inside The Screen ====

        // Game Screen Boundaries

        float xBoundary = 3.35f;
        float yBoundary = 4.45f;

        // Wrapping On X Axis

        if (transform.position.x > xBoundary)
        {
            transform.position = new Vector3(-xBoundary, transform.position.y, 0);
        }
        else if (transform.position.x < -xBoundary)
        {
            transform.position = new Vector3(xBoundary, transform.position.y, 0);
        }

        // Make The Player Stay On The Screen On Y Axis

        if (transform.position.y > yBoundary)
        {
            transform.position = new Vector3(transform.position.x, yBoundary, 0);
        }
        else if (transform.position.y < -yBoundary)
        {
            transform.position = new Vector3(transform.position.x, -yBoundary, 0);
        }
        
    }

    private void Fire()
    {
        audioSource.PlayOneShot(fireSound);
        nextFire = Time.time + fireRate;

        Vector3 projectileOffset = new Vector3(0, 0.61f, 0);
        Instantiate(projectilePrefab, transform.position + projectileOffset, Quaternion.identity);
    }

    private void LaunchBomb()
    {
        if (isBombPowerUpActive == true)
        {
            Instantiate(bombPrefab, transform.position, Quaternion.identity);

        }
    }

    public void TakeDamage(int damageValue)
    {
        audioSource.PlayOneShot(takeDamageSound);
        health -= damageValue;
    }

    private void Die()
    {
        if (health <= 0)
        {
            // Stop the game when player dies.
            Time.timeScale = 0;

            Destroy(this.gameObject);
        }
    }

    IEnumerator ActivateBombPowerUp()
    {
        isBombPowerUpActive = true;
        yield return new WaitForSeconds(bombPowerUpTimer);

        // Deactivate after the timer ends
        isBombPowerUpActive = false;
    }

    public void GetPoint(int pointValue)
    {
        point += pointValue;
    }

    public void DeactivateShield()
    {
        isShieldActive = false;
        shieldPrefab.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "EnemyProjectile")
        {
            TakeDamage(1);
            Destroy(other.gameObject);
        }
        else if (other.tag == "Enemy")
        {
            TakeDamage(2);
            Destroy(other.gameObject);
        }
        else if (other.tag == "EnemyBomb")
        {
            TakeDamage(2);
            Destroy(other.gameObject);
        }
        else if (other.tag == "ShieldPowerUp")
        {
            audioSource.PlayOneShot(powerUpSound);
            isShieldActive = true;

            if (isShieldActive == true)
            {
                shieldPrefab.gameObject.SetActive(true);
            }
            
            Destroy(other.gameObject);
        }
        else if (other.tag == "1Up")
        {
            audioSource.PlayOneShot(powerUpSound);
            if (health < 3 && health > 0)
            {
                health += 1;
            }
            else
            {
                GetPoint(1000);
            }
            
            Destroy(other.gameObject);
        }
        else if (other.tag == "BombPowerUp")
        {
            audioSource.PlayOneShot(powerUpSound);
            if (isBombPowerUpActive == true && bombPowerUpTimer < 5f)
            {
                bombPowerUpTimer = 5f;
            }

            StartCoroutine(ActivateBombPowerUp());
            Destroy(other.gameObject);
        }


    }

   



}
