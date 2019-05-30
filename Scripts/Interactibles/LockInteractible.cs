using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockInteractible : Interactible {

	public override void Effect()
    {
        player.SwapLayers();
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
