using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchGuard : MonoBehaviour {

    public DialogSystem dialsys;
    public GuardCommander gc;
    public GameObject watchGuard2;
    public string panicLine;

    private BoxCollider2D boxer;
    private BoxCollider2D boxer2;
    private bool panic;

    private void Start()
    {
        boxer = GetComponent<BoxCollider2D>();
        boxer2 = watchGuard2.GetComponent<BoxCollider2D>();
        panic = false;
    }

    private void Update()
    {
        if (panic)
        {
            if (!dialsys.visible)
            {
                boxer.enabled = false;
                boxer2.enabled = false;
                panic = false;
                dialsys.ShowTextLine(panicLine);
            }
        }
    }

    public void PanicNow()
    {
        panic = true;
    }

}
