using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class DialogSystem : MonoBehaviour {

    Image bgrd;
    public Text dialogText;
    public Text enterText;
    public bool visible;
    public Player player;

    private bool isNext;
    private StreamReader sr;
    private string line;
    private bool catchline;

	// Use this for initialization
	void Start () {
        bgrd = GetComponent<Image>();
        isNext = false;
        catchline = false;
        HideThis();
	}

    void Update()
    {
        if (visible)
        {
            if (Input.GetKeyUp(KeyCode.Return))
            {
                if (isNext)
                {
                    ShowNextLine();
                }
                else
                {
                    if (catchline)
                    {
                        player.AnimEnd();
                    }
                    HideThis();
                }
            }
        }
    }

    public void ShowTextLine(string boxAddress)
    {
#if UNITY_EDITOR
        sr = new StreamReader("Assets/Scripts/Descriptions/" + boxAddress + ".txt");
#else
        sr = new StreamReader("TextResources/" + boxAddress + ".txt");  
#endif
        line = sr.ReadLine();
        if (line.StartsWith(":"))
        {
            SpeakerSwitch();
        }
        dialogText.text = line;
        ShowThis();
        line = sr.ReadLine();
        if (line != null)
        {
            isNext = true;
        }
    }

    private void SpeakerSwitch()
    {
        switch(line)
        {
            case ":angie": dialogText.color = Color.white; break;
            case ":guard": dialogText.color = Color.magenta; break;
            case ":guido": dialogText.color = Color.cyan; break;
            default: dialogText.color = Color.black; break;
        }
        line = sr.ReadLine();
    }

    private void ShowNextLine()
    {
        if (line.StartsWith(":"))
        {
            SpeakerSwitch();
        }
        dialogText.text = line;
        line = sr.ReadLine();
        if (line == null)
        {
            isNext = false;
        }
        
    }

    public void Caught(bool t)
    {
        if (t)
            catchline = true;
        else
            catchline = false;
    }
    

    public void HideThis()
    {
        bgrd.enabled = false;
        dialogText.enabled = false;
        enterText.enabled = false;
        visible = false;
    }

    public void ShowThis()
    {
        bgrd.enabled = true;
        dialogText.enabled = true;
        enterText.enabled = true;
        visible = true;
    }
}
