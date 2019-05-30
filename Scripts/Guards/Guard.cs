using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Guard : MonoBehaviour {

    public bool inMotion;
    public bool postMotion;
    public SpriteRenderer spren;

    public float guardSpeed = 5;
    public DialogSystem dialsys;
    public string arrivalString;

    private void Start()
    {
        spren = GetComponent<SpriteRenderer>();
        inMotion = false;
    }

    // Update is called once per frame
    void Update () {
		if (inMotion)
        {
            var move = new Vector3(1, 0, 0);
            transform.position += move * guardSpeed * Time.deltaTime;
        }
        else if (postMotion)
        {
            if (!dialsys.visible)
            {
                FollowingAction();
                postMotion = false;
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (string.Equals(collision.gameObject.name, "Guardstuff"))
        {
            dialsys.ShowTextLine(arrivalString);
            inMotion = false;
            postMotion = true;
        }
    }

    public abstract void FollowingAction();
    public abstract void Unseat();

    public void FlipGuard()
    {
        if (spren.flipX)
            spren.flipX = false;
        else
            spren.flipX = true;
    }

    public void ShiftMotion(bool a)
    {
        FlipGuard();
        Unseat();
        inMotion = a;
    }
}
