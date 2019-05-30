using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {

    public float dir;

    // Update is called once per frame
    void Update () {

        if (dir < 0)
		    if (Input.GetAxis("Horizontal") < -0.3)
                Destroy(this.gameObject);
        if (dir > 0)
            if (Input.GetAxis("Horizontal") > 0.3)
                Destroy(this.gameObject);
	}
}
