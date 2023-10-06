using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    Vector3 camLog;
    public static bool engine = false;
    public Transform target;
    float xPos;
    float yPos;
    float zPos;
    float xRot;
    float yRot;
    float zRot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!FPController.InShip)
        {
            GetComponent<Camera>().enabled = false;
        }
        else
        {
            GetComponent<Camera>().enabled = true;
        }
        if (Respawn.cameraReset)
        {
            Respawn.cameraReset = false;
            Destroy(gameObject);
        }
    }
    void LateUpdate()
    {
        if (Controller.crash)
        {
            return;
        }
        if (!FPController.InShip)
        {
            return;
        }
        if (!engine)
        {
            if ( target && camLog == Vector3.zero)
            {
                    camLog = target.position;
                    transform.position = camLog;
            }

            transform.parent = null;

        }
        else if (engine == true)
        {
            
            if(Quaternion.Angle(transform.rotation, target.rotation)<5 && Vector3.Distance(transform.position, target.position)<5)
            {
                transform.position = target.position;
                transform.rotation = target.rotation;
                transform.parent = target;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * 300 + Controller.thrust);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, Time.deltaTime * 300);
            }
            if (Controller.landed && Controller.thrust>10) {
                Controller.landed = false;
            }
            camLog = Vector3.zero;
            
        }

        //cut engine
        if (Input.GetKeyDown(KeyCode.X))
        {
            engine = !engine;
        }
    }
}
