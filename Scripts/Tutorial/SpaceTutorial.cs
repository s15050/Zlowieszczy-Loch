using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceTutorial : MonoBehaviour {

    private bool first;

	// Use this for initialization
	void Start () {
        first = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (first)
                first = false;
            else
                Destroy(this.gameObject);
        }
	}
}
