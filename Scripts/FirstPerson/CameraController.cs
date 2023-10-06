using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivityX = 50f;
    public float mouseSensitivityY = 30f;
    Transform player;
    float xRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (FPController.InShip) {
            GetComponent<Camera>().enabled = false;
        }
        else
        {
            GetComponent<Camera>().enabled = true;
        }
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime * 100;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime * 100;
        if (GameObject.Find("Player"))
        {
            player = GameObject.Find("Player").transform;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -75f, 90f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            player.Rotate(Vector3.up * mouseX);
        }

    }
}
