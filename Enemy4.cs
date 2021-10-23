using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4 : MonoBehaviour
{
    // Audio
    AudioSource audioSource;

    [SerializeField] AudioClip fireSound;

    // Prefabs
    [SerializeField] private GameObject projectilePrefab;

    // Attributes
    private float moveSpeed = 3f;
    private float nextFire;
    private float fireRate = 1.0f;
    private int health = 2;
    
    // Component
    GameObject playerGameObject;
    Player player;
    Transform playerTransform;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        // Get Components
        audioSource = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        playerGameObject = GameObject.Find("Player");
        player = playerGameObject.GetComponent<Player>();

        if (playerGameObject != null)
        {
            playerTransform = playerGameObject.transform;

        }
    }

    // Update is called once per frame
    void Update()
    {
        Die();
        DestroyWhenOutOfBoundaries();
        MoveY();

        if (Time.time > nextFire)
        {
            Fire();

        }

        // ===== This code is not working properly at the moment: Under development. ===== 

        /*if (transform.position.y > 0.4f || playerTransform == null)
        {
            MoveY();
        }
        else if (transform.position.y <= 0.4f)
        {
            if (Time.time > nextFire)
            {
                Fire();
                
            }

            AlwaysBeInFrontOfThePlayer();
        }*/
    }

    // ===== This method is not working properly at the moment: Under development. ===== 

    /*private void AlwaysBeInFrontOfThePlayer()
    {
        if (playerTransform != null)
        {
            transform.position = new Vector3(playerTransform.position.x, transform.position.y, 0);
            

        }

    }*/

    private void MoveY()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
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

    private void Die()
    {
        if (health <= 0)
        {
            player.GetPoint(200);
            gameManager.BlowUpEffect(this.transform.position);
            Destroy(this.gameObject);
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
        if (other.tag == "PlayerProjectile")
        {
            health--;
            Destroy(other.gameObject);
        }
        else if (other.tag == "PlayerBomb")
        {
            player.GetPoint(200);
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
