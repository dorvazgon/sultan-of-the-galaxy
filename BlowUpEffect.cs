using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowUpEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyAfterLaunch());

    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator DestroyAfterLaunch()
    {
        yield return new WaitForSeconds(0.7f);
        Destroy(this.gameObject);
    }
}
