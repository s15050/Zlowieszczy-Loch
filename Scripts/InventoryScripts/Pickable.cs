using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickable : MonoBehaviour {

    public string indivName;
    public string matchingTrig;
    public string dialogFile;
    public string guardTrigger;
    public Image avatar;
    public Transform player;
    public DialogSystem talk;
    public GuardCommander gc; 

    private SpriteRenderer spren;
    private BoxCollider2D boxer;
    private AudioSource asource;
    private bool held;
    private float dis;
    private bool firstpick;

    private void Update()
    {
        if (held)
        {
            gameObject.transform.position = new Vector2(player.transform.position.x, player.transform.position.y + dis);
        }
    }

    private void Start()
    {
        spren = GetComponent<SpriteRenderer>();
        boxer = GetComponent<BoxCollider2D>();
        asource = gc.GetComponent<AudioSource>();
        held = false;
        firstpick = true;
        dis = 0;
    }

    public void PickMeUp() //Do podnoszenia przedmiotu z ziemi
    {
        if (firstpick)
        {
            talk.ShowTextLine(dialogFile);
            firstpick = false;
        }
        gameObject.transform.position = new Vector2(gameObject.transform.position.x, player.transform.position.y - 10);
        dis = -10f;
        held = true;
        boxer.enabled = false;
    }

    public void TakeMeOut() //Do wyciągnięcia przedmiotu
    {
        Vector2 parpos = player.position;
        gameObject.transform.position = new Vector2(parpos.x, parpos.y + 2);
        dis = 2f;
        spren.sortingLayerName = "Interaction-front";
        spren.sortingOrder = 4;
    }

    public void UseMe(string trig) //Do użycia przedmiotu
    {
        if (string.Equals(trig, matchingTrig))
        {
            if (!string.Equals(guardTrigger, ""))
            {
                gc.Trig(guardTrigger);
            }
            asource.Play();
            Destroy(this.gameObject);
        }
        else
        {
            player.gameObject.GetComponent<Player>().ReStore(this);
        }
    }
}
