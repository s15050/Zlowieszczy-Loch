using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockInteractible : Interactible {

	public override void Effect()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
