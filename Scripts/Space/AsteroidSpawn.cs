

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AsteroidSpawn : MonoBehaviour
{
    public GameObject[] asteroids;
    GameObject parenter;
    Vector3 logPos = Vector3.zero;
    float range = 1500;
    GameObject logAs;
    public GameObject[] enemy;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(logPos, transform.position) > range * 2) {
            Spawn();
            Debug.Log(logPos);
        }
    }
    void Spawn()
    {
        parenter = GameObject.Find("Asteroids");
        logPos = transform.parent.position;
        Vector3 parent = transform.InverseTransformDirection(transform.parent.position);
        float spawnNum = Random.Range(50f, 100f);
        for (var i = 0; i < spawnNum; i++) 
        {
            Vector3 asteroidPos = transform.TransformDirection(Random.Range(parent.x - range, parent.x+ range), Random.Range(parent.y - range, parent.y + range), Random.Range(parent.z - range, parent.z + (range*10)));
            float scale = Random.Range(1f, 50f);

           
            GameObject asteroid = Instantiate(asteroids[Random.Range(0, asteroids.Length)], asteroidPos, Quaternion.identity);
            if (Vector3.Distance(asteroidPos, transform.position) < 1000 || Vector3.Distance(asteroidPos, Vector3.zero) < 1000)
            {
                asteroid.GetComponent<Collider>().enabled = false;
                asteroid.layer = LayerMask.NameToLayer("AsteroidIgnore");
            }
            asteroid.transform.localScale = new Vector3(scale, scale, scale);
            asteroid.transform.parent = parenter.transform;
            
        }
        Vector3 enemyPos = new Vector3(Random.Range(transform.position.x - range/2, transform.position.x + range/2), Random.Range(transform.position.y - range/2, transform.position.y + range/2), Random.Range(transform.position.z - range/2, transform.position.z + range * 2));
        if (Random.value > 0.5)
        {
            Instantiate(enemy[Random.Range(0,enemy.Length)], enemyPos, transform.rotation);
        }
    }
}
