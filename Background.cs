using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private GameObject[] stars;

    private float spawnRate;
    private float nextSpawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawn)
        {
            SpawnStars();
        }

    }

    private void SpawnStars()
    {
        int randomStar = Random.Range(0, stars.Length);
        spawnRate = Random.Range(0.25f, 0.75f);
        nextSpawn = Time.time + spawnRate;

        Vector3 randomStarPos = new Vector3(Random.Range(-2f, 2f), 6f, 0);

        Instantiate(stars[randomStar], randomStarPos, Quaternion.identity);
    }
}
