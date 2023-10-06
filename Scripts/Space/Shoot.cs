using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("destroy");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * (2000 + Controller.thrust) * Time.deltaTime);
    }
    IEnumerator destroy()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
    void OnCollisionEnter(Collision col) {
        string name = col.transform.gameObject.name;
        AI enemy = null;
        if (col.transform.parent && col.transform.parent.gameObject.tag == "enemy")
        {
            enemy = col.transform.parent.gameObject.transform.parent.gameObject.GetComponent<AI>();
        }
        if (col.transform.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
        if (name == "Body" && enemy)
        {
            enemy.body = true;
            enemy.crashed = true;
        }
        if (name == "Wing L" && enemy)
        {
            enemy.wingL = true;
            enemy.crashed = true;
        }
        if (name == "Wing R" && enemy)
        {
            enemy.wingR = true;
            enemy.crashed = true;
        }
        if (name == "Thruster" && enemy)
        {
            enemy.thruster = true;
            enemy.crashed = true;
        }
    }
}
