using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileShoot : MonoBehaviour
{
    public GameObject explode;
    void Start()
    {
        StartCoroutine("destroy");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * (1500 + Controller.thrust) * Time.deltaTime);
    }
    IEnumerator destroy()
    {
        yield return new WaitForSeconds(15f);
        Destroy(gameObject);
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.transform.gameObject.tag != "Player")
        {
            Instantiate(explode, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
