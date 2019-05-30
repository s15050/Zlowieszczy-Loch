using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidoInteractible : Interactible {

    private bool effects = false;

    private void Update()
    {
        if (effects)
        {
            if (!dialSys.visible)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public override void Effect()
    {
        effects = true;
    }
}
