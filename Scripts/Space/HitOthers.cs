using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitOthers : MonoBehaviour
{
    bool crash;
    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        crash = transform.parent.gameObject.transform.parent.GetComponent<AI>().crashed;
    }
    void OnCollisionEnter(Collision col) {
        if (!crash)
        {
            if (col.transform.parent && col.transform.parent.gameObject.tag == "enemy") {
                return;
            }
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
