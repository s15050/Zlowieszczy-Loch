using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardCommander : MonoBehaviour {

    public Guard guardPrison;
    public Guard guardCorridor;
    public WatchGuard guardWatch1;
    public GameObject blackCoat;
    public BoxCollider2D corridorWall;

    private bool prisonTrigged;
    private bool corridorTrigged;
    private bool watchTrigged;
    private bool cupTrigged;
    private int counter;

    private void Start()
    {
        prisonTrigged = false;
        corridorTrigged = false;
        watchTrigged = false;
        cupTrigged = false;
        counter = 0;
        guardPrison.FlipGuard();
    }

    // Update is called once per frame
    void Update () {
        if (prisonTrigged)
        {
            guardPrison.ShiftMotion(true);
            prisonTrigged = false;
        }

        if (corridorTrigged)
        {
            guardCorridor.ShiftMotion(true);
            corridorTrigged = false;
        }

        if (cupTrigged)
        {
            corridorWall.enabled = true;
        }

        if (watchTrigged)
        {
            if (Input.GetKeyUp(KeyCode.Return))
                counter++;
            if (counter >= 2)
            {
                blackCoat.GetComponent<SpriteRenderer>().enabled = true;
                guardWatch1.PanicNow();
                watchTrigged = false;
            }
        }
	}

    public void Trig(string wut)
    {
        switch (wut)
        {
            case "prison": prisonTrigged = true; break;
            case "corridor": corridorTrigged = true; break;
            case "watch": watchTrigged = true; break;
            case "cup": cupTrigged = true; break;
            default: break;
        }
    }
}
