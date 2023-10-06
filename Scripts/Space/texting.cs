using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class texting : MonoBehaviour
{
    public Text txt;
    public TextMesh engineWarning;
    public static float shipsKilled = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!FPController.InShip) {
            engineWarning.characterSize = 0;
        return;
        }
        if (Engine.engine)
        {
            engineWarning.characterSize = 0;
        }
        else {
            engineWarning.characterSize = 0.5f;
        }
        txt.text = shipsKilled + " Ships Killed";
    }
}
