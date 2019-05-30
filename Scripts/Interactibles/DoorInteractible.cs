using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractible : Interactible {

    public string newtag;

	public override void Effect()
    {
        this.gameObject.tag = newtag;
    }
}
