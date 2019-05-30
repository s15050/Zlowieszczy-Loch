using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorGuard : Guard {

    public GameObject cup;

    public override void FollowingAction()
    {
        Destroy(cup);
        Destroy(this.gameObject);
    }

    public override void Unseat()
    {
        gameObject.tag = "Untagged";
    }

}
