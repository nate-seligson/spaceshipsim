
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Destroy : MonoBehaviour
{
   public GameObject[] aster;
    float scaleObj;
    public GameObject Breakoff;
    public GameObject missile;
    float totalSize;
    Vector3 ship;
    // Start is called before the first frame update
    void Start()
    {
        scaleObj = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Spaceship"))
        {
            Debug.Log("found");
            ship = GameObject.Find("Spaceship").transform.position;
            if (Vector3.Distance(ship, transform.position) > 5000)
            {
                Destroy(gameObject);
            }
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.transform.gameObject.tag == "missile")
        {
            StartCoroutine("destroy");
        }
    }
    IEnumerator destroy()
    {
        ParticleSystem ps = Breakoff.GetComponent<ParticleSystem>();
        var main = ps.main;
        main.startSize = scaleObj * 4;
        GameObject breaky = Instantiate(Breakoff, transform.position, Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
        for (var i = 0; totalSize<=(scaleObj * 4); i++)
        {
            Debug.Log(totalSize + " , " + scaleObj);
            GameObject rubble = Instantiate(aster[Random.Range(0,aster.Length)], transform.position, Quaternion.Euler(Random.Range(0,360), Random.Range(0, 360), Random.Range(0, 360)));
            rubble.GetComponent<Collider>().enabled = false;
            rubble.AddComponent<Crunch>();
            float scaled = Random.Range(0.01f, scaleObj * 0.5f);
            totalSize += scaled;
            rubble.transform.localScale = new Vector3(scaled, scaled, scaled);
        }
        Destroy(gameObject);
        yield return new WaitForSeconds(2f);
        Destroy(breaky);
    }
}