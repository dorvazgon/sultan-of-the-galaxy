using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    private float moveSpeed;

    private void Start()
    {
        moveSpeed = Random.Range(1f, 8f);
    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);

        if (transform.position.y < -5.45f)
        {
            Destroy(this.gameObject);
        }
    }
}
