using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactible : MonoBehaviour {

    public Player player;
    public DialogSystem dialSys;
    public string matchingString;
    public string correctDialog;
    public string incorrectDialog;

	public bool TestPickable(string matcher)
    {
        if (string.Equals(matcher, matchingString))
        {
            dialSys.ShowTextLine(correctDialog);
            Effect();
            return true;
        }
        else
        {
            dialSys.ShowTextLine(incorrectDialog);
            return false;
        }
    }

    public abstract void Effect();
}
