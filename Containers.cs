using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Containers : MonoBehaviour
{

    private void Start()
    {
        
    }

    void Update()
    {
        DestroyParent();
    }

    private void DestroyParent()
    {
        // Destroy the container of multiple projectiles/bombs when they are already destroyed.
        if (gameObject.transform.childCount <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
