using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blaster : MonoBehaviour
{
    public GameObject blast;
    public GameObject missile;
    bool ready = true;
    float timer = 0;
    public GameObject bar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(blast, transform.position, transform.rotation);
        }
        if (Input.GetKeyDown(KeyCode.Q) && ready)
        {
            timer = 0;
            Instantiate(missile, transform.position, transform.rotation);
            ready = false;
            StartCoroutine("reload");
        }
        if (!ready)
        {
            bar.GetComponent<RawImage>().material.SetColor("_Color", Color.white);
            timer += Time.deltaTime;
            bar.transform.localScale = new Vector3(timer / 10, 0.05f, 1);
        }
        else
        {
            bar.GetComponent<RawImage>().material.SetColor("_Color", Color.red);
        }
    }
    IEnumerator reload()
    {
        yield return new WaitForSeconds(3f);
        ready = true;
    }
}
