using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandlepointInteractible : Interactible {

    public Sprite blackcandle;

    public override void Effect()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = blackcandle;
        Transform bot = gameObject.transform;
        bot.localScale = new Vector2(0.1f, 0.1f);
        bot.localPosition -= new Vector3(0, 2f);
        GetComponent<SpriteRenderer>().enabled = true;
    }
}
