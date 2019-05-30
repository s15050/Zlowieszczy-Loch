using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupInteractible : Interactible {

    public override void Effect()
    {
        gameObject.tag = "guard-only";
    }

}
