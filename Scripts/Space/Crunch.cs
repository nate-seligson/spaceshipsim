

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crunch : MonoBehaviour
{
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("FlyAndDie");
        float scale = transform.localScale.x;
        if (scale < 1)
        {
            speed = Random.Range(1000, 2000);
        }
        if (scale > 5)
        {
            speed = Random.Range(500, 1000);
        }
        if (scale > 10)
        {
            speed = Random.Range(100, 500);
        }
        if (scale > 20)
        {
            speed = Random.Range(10, 100);
        }
        Destroy(GetComponent<Collider>());
        gameObject.AddComponent<BoxCollider>();
        if (!gameObject.GetComponent<Rigidbody>())
        {
            Rigidbody rb = gameObject.AddComponent<Rigidbody>();
            rb.useGravity = false;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        GetComponent<Collider>().enabled = true;
    }
    IEnumerator FlyAndDie()
    {
        yield return new WaitForSeconds(Random.Range(1f,10f));
        Destroy(gameObject);
    }
}
