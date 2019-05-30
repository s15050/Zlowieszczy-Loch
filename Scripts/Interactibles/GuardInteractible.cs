using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardInteractible : Interactible {

    public GameObject cellKey;
    public Sprite sleep;

    public override void Effect()
    {
        cellKey.transform.position = this.gameObject.transform.position;
        Destroy(gameObject);
    }
}
