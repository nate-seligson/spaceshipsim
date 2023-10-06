using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPController : MonoBehaviour
{
    public static bool InShip = false;
    float horizontal;
    float vertical;
    Rigidbody rb;
    bool ground;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        name = "Player";
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * Time.deltaTime * 5 * horizontal);
        transform.Translate(Vector3.forward * Time.deltaTime * 5 * vertical);
        if (Input.GetKeyDown(KeyCode.Space)) {
            rb.AddForce(Vector3.up * 250);
            ground = false;
        }


        //raycast
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit,3))
        {
            string name = hit.transform.gameObject.name;
            if(name == "Spaceship")
            {
                if (Input.GetKeyDown(KeyCode.F)) {
                    InShip = true;
                    Destroy(gameObject);
                }
            }
        }


    }
    void OnCollisionEnter(Collision col)
    {
        ground = true;
    }
}
