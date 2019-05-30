using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenExitInteractible : Interactible {

    private bool slidin = false;
    public float speed = 3f;
    public GameObject exdoor;

    private void Update()
    {
        if (slidin)
        {
            var move = new Vector3(1, 0, 0);
            transform.position += move * speed * Time.deltaTime;
        }
    }

    public override void Effect()
    {
        player.FreezeForMoment();
        slidin = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (string.Equals(collision.gameObject.tag, "stoppoint"))
        {
            slidin = false;
            GetComponent<BoxCollider2D>().enabled = false;
            exdoor.GetComponent<BoxCollider2D>().enabled = true;
            player.Unfreeze();
        }
    }
}
