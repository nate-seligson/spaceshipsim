using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("die");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator die() {
        yield return new WaitForSeconds(20f);
        Destroy(gameObject);
    }
}
