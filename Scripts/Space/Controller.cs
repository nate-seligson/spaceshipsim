using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    float horizontalAxis;
    float verticalAxis;
    public static float thrust = 0;
    public static float maxThrust = 800;
    public static bool crash = false;
    public ParticleSystem particle;
    Vector3 velLog = Vector3.zero;
    public GameObject camera;
    float rotSpeed = 75;
    float rotSpeedNoEngine;
    public GameObject explosion;
    float sizeLog = 0;
    bool extra = false;
    public static bool landed = true;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        var main = particle.main;
        main.startSize = 0f;
        rotSpeedNoEngine = rotSpeed * 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (!FPController.InShip) {
            return;
        }
        if (Input.GetKeyDown(KeyCode.F)) {
            Instantiate(player, transform.position, transform.rotation);
            FPController.InShip = false;
        }
        var main = particle.main;
        if (crash == false)
        {
            horizontalAxis = Input.GetAxis("Horizontal");
            verticalAxis = Input.GetAxis("Vertical");
            //rotations
            transform.Rotate(Vector3.left * verticalAxis * (rotSpeed) * Time.deltaTime);
            transform.Rotate(Vector3.back * horizontalAxis * ((rotSpeed + 100)) * Time.deltaTime);
            //thrust
            if (Input.GetKey(KeyCode.O) && Engine.engine == true)
            {
                if (thrust < maxThrust)
                {
                    thrust += Time.deltaTime * 25 + (thrust/50);
                    main.startSize = thrust / maxThrust;
                }
                else if (thrust >= maxThrust)
                {
                    thrust = maxThrust + 300;
                    main.startSize = 1.5f;
                    camera.GetComponent<Camera>().fieldOfView = 77;
                    extra = true;
                    Debug.Log(extra);
                }
                if (camera.GetComponent<Camera>().fieldOfView < 75)
                {
                    camera.GetComponent<Camera>().fieldOfView += Time.deltaTime + thrust/(maxThrust * 2);
                }
            }
            if (Input.GetKeyUp(KeyCode.O) && extra) {
                Debug.Log(extra);
                thrust = maxThrust;
                main.startSize = thrust / maxThrust;
                camera.GetComponent<Camera>().fieldOfView = 75;
                extra = false;
            }
            if (Engine.engine && extra)
            {
                rotSpeed = 40;
            }
            else if (Engine.engine && !extra)
            {
                rotSpeed = 75;
            }
            else
            {

            }
                if (Input.GetKey(KeyCode.K) && Engine.engine == true)
            {
                if(camera.GetComponent<Camera>().fieldOfView > 45)
                {
                    camera.GetComponent<Camera>().fieldOfView -= thrust/(maxThrust/2);
                }
                else
                {
                    camera.GetComponent<Camera>().fieldOfView =45;
                }
                if (thrust > 0)
                {
                    thrust -= Time.deltaTime * 50 * (thrust/10);
                    main.startSize = thrust / maxThrust;
                }
                else
                {
                    thrust = 0;
                }
            }
            //move
            if (Engine.engine == true)
            {
                transform.Translate(Vector3.forward * thrust * Time.deltaTime);
                velLog = Vector3.zero;
                if (sizeLog != 0)
                {
                    main.startSize = sizeLog;
                    sizeLog = 0;
                }
            }
            else
            {
                if (sizeLog == 0)
                {
                    sizeLog = thrust/maxThrust;
                }
                rotSpeed = rotSpeedNoEngine;
                main.startSize = 0f;    
                if(velLog == Vector3.zero)
                {
                    velLog = transform.TransformDirection(Vector3.forward);
                }
                transform.Translate(velLog * thrust * Time.deltaTime, Space.World);
                camera.transform.Translate(velLog * thrust * Time.deltaTime, Space.World);
            }
        }


        
        
    }
   void OnCollisionEnter(Collision col)
    {
        if (!landed)
        {
            string tag = col.transform.gameObject.tag;
            if (tag != "missile" && tag != "blast")
            {
                camera.transform.parent = null;
                crash = true;
                texting.shipsKilled = 0;
                Engine.engine = false;
                Instantiate(explosion, transform.position, transform.rotation);
                Respawn.respawned = true;
                Destroy(gameObject);
            }
        }
    }
}
