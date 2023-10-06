using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject spaceship;
    public static bool respawned;
    public static bool cameraReset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (respawned == true) {
            StartCoroutine("respawn");
            respawned = false;
        }
    }
    IEnumerator respawn()
    {
        yield return new WaitForSeconds(2f);
        cameraReset = true;
        GameObject spacer = Instantiate(spaceship, Vector3.zero, Quaternion.identity);
        spacer.name = "Spaceship";
        Controller.crash = false;
        Controller.thrust = 0;
    }
}
