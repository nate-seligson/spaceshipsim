using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public GameObject lookPoint;
    bool pathBlocked = true;
    Vector3 direction;
    public float speed = 100;
    public GameObject explosion;
    public GameObject explosionT;
    public GameObject player;
    public ParticleSystem ps;
    bool move;
    string dir = "";
    public float tspd = 75;
    public float TopSpeed = Controller.maxThrust - 25;
    public bool wingL;
    public bool wingR;
    public bool body;
    public bool crashed;
    public bool thruster;
    Quaternion newRot;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var main = ps.main;
        if (player == null)
        {
            player = GameObject.Find("Spaceship");
        }
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        RaycastHit hit;
        if (!crashed) {
        if (Physics.Raycast(lookPoint.transform.position, Vector3.forward, out hit, 500f))
        {
            if (pathBlocked)
            {
                direction = new Vector3(Random.value, Random.value, Random.value);
                pathBlocked = false;
            }
            transform.Rotate(direction * Time.deltaTime * 100);
        }
        else
        {
            direction = Vector3.zero;
            pathBlocked = true;
        }
        if (player)
        {
            if (Vector3.Distance(player.transform.position, gameObject.transform.position) < 500)
            {
                StartCoroutine("Moveset");
                if (speed < TopSpeed)
                {
                    speed += Time.deltaTime * 200 * (Controller.thrust/4);
                    main.startSize = Time.deltaTime * 100;
                }
                else {
                    speed = Controller.maxThrust - 25;
                }
                if (move)
                {
                    float opt = Mathf.Round(Random.Range(0f, 4f));
                    switch(opt){
                        case 1:
                            if (dir == "")
                            {
                                newRot = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z - 90);
                            }
                            dir = "left";
                            break;
                        case 2:
                            if (dir == "")
                            {
                                newRot = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + 90);
                            }
                            dir = "right";
                            break;
                        case 3:
                            if (dir == "")
                            {
                                newRot = Quaternion.Euler(transform.rotation.x, transform.rotation.y-90, transform.rotation.z);
                            }
                            dir = "up";
                            break;
                        case 4:
                            if (dir == "")
                            {
                                newRot = Quaternion.Euler(transform.rotation.x, transform.rotation.y+90, transform.rotation.z - 90);
                            }
                            dir = "down";
                            break;
                    }
                    opt = 0;
                    move = false;
                }
            }
        }
            if (dir != "")
            {
                if (dir == "left")
                {
                    if (Quaternion.Angle(newRot, transform.rotation) > 10)
                    {
                        transform.Rotate(Vector3.back * tspd * Time.deltaTime);
                    }
                    else
                    {
                        newRot = transform.rotation;
                    }
                    transform.Rotate(Vector3.left * (tspd / 2) * Time.deltaTime);
                    StartCoroutine("stop");

                }
                else if (dir == "right")
                {
                    if (Quaternion.Angle(newRot, transform.rotation) > 10)
                    {
                        transform.Rotate(Vector3.forward * tspd * Time.deltaTime);
                    }
                    else
                    {
                        newRot = transform.rotation;
                    }
                    transform.Rotate(Vector3.left * (tspd / 2) * Time.deltaTime);
                    StartCoroutine("stop");

                }
                else if (dir == "up")
                {
                    if (Quaternion.Angle(newRot, transform.rotation) > 10)
                    {
                        transform.Rotate(Vector3.left * tspd * Time.deltaTime);
                    }
                    else
                    {
                        newRot = transform.rotation;
                    }
                    transform.Rotate(Vector3.left * (tspd / 2) * Time.deltaTime);
                    StartCoroutine("stop");

                }
                else if (dir == "down")
                {
                    Debug.Log(newRot);
                    if (Quaternion.Angle(newRot, transform.rotation) > 10)
                    {
                        transform.Rotate(Vector3.right * tspd * Time.deltaTime);
                    }
                    else
                    {
                        newRot = transform.rotation;
                    }
                    transform.Rotate(Vector3.right * (tspd / 2) * Time.deltaTime);
                    StartCoroutine("stop");

                }
            }
        }
        //spinout
        if (wingL)
        {
            transform.Rotate(Vector3.back * Random.Range(500, 1000) * Time.deltaTime);
            transform.Translate(Vector3.right * Random.Range(100, 500) * Time.deltaTime);
            StartCoroutine("explode");
        }
        if (wingR)
        {
            transform.Rotate(Vector3.forward * Random.Range(500, 1000) * Time.deltaTime);
            transform.Translate(Vector3.left * Random.Range(100, 500) * Time.deltaTime);
            StartCoroutine("explode");
        }
        if (body)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            texting.shipsKilled += 1;
            Destroy(gameObject);
        }
        if (thruster)
        {
            transform.Rotate(Vector3.right * 500 * Time.deltaTime);
            transform.Translate(Vector3.forward * 500 * Time.deltaTime, Space.World);
            StartCoroutine("explodeT");
        }
    }
    IEnumerator Moveset()
    {
        if (dir == "")
        {
            yield return new WaitForSeconds(Random.Range(5f, 20f));
            move = true;
        }
    }
    IEnumerator stop() {
        yield return new WaitForSeconds(Random.Range(5f, 10f));
        dir = "";
    }
    IEnumerator explode() {
        yield return new WaitForSeconds(Random.Range(3f, 5f));
        Instantiate(explosion, transform.position, transform.rotation);
        texting.shipsKilled += 1;
        Destroy(gameObject);
    }
    IEnumerator explodeT()
    {
        if (crashed)
        {
            Instantiate(explosionT, transform.position, transform.rotation);
            crashed = false;
        }
        var main = ps.main;
        main.startSize = 0;
        yield return new WaitForSeconds(Random.Range(3f, 5f));
        Instantiate(explosion, transform.position, transform.rotation);
        texting.shipsKilled += 1;
        Destroy(gameObject);
    }
    void OnCollisionEnter(Collision col)
    {
        Instantiate(explosion, transform.position, transform.rotation);
        texting.shipsKilled += 1;
        Destroy(gameObject);
    }

}
