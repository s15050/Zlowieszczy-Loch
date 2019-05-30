using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    private Animator anim;
    private bool isRight = false;
    private string currentRoom;
    private bool holdingItem = false;
    private Pickable heldItem;
    private bool alive = true;
    private SpriteRenderer enemySprite;
    private SpriteRenderer spren;
    private bool enemyFlipper = true;
    private AudioSource aSource;
    private bool inExit;

    private bool inTrigger;
    private Collider2D koliz;

    public float speed = 2f;
    public Follow cam;
    public Text iText;
    public InventoryPanel pocket;
    public DialogSystem dialSys;
    public AudioClip mgsalert;
    public AudioClip success;
    public GuardCommander gc;
    

	// Use this for initialization
	void Awake () {
        inExit = false;
        anim = GetComponent<Animator>();
        spren = GetComponent<SpriteRenderer>();
        aSource = GetComponent<AudioSource>();
        spren.sortingLayerName = "Interaction-back";
        spren.sortingOrder = 0;
        transform.position = ScreenSwitches.getPlayerStarter();
        currentRoom = "prison";
        iText.enabled = false;
        koliz = null;
	}

    // Update is called once per frame
    void Update() {
        if (inExit)
        {
            if (!dialSys.visible)
            {
                LoadLvl.LoadEndScreen();
            }
        }
        if (alive)
        {
            float hor = Input.GetAxis("Horizontal");
            if (pocket.isHidden() && (!dialSys.visible)) //Jeśli inv. i dialog są zamknięte, postać może się poruszać
            {
                if (hor < 0 && !isRight)
                {
                    Flip();
                    isRight = true;
                }
                if (hor > 0 && isRight)
                {
                    Flip();
                    isRight = false;
                }
                var move = new Vector3(hor, 0, 0);
                anim.SetFloat("speedy", Mathf.Abs(hor));
                transform.position += move * speed * Time.deltaTime;
                if (inTrigger)
                    CollisionHandling(koliz);
            }
            else if (!pocket.isHidden())
            {
                anim.SetFloat("speedy", 0); //Do otwierania inv. w biegu
                if (Input.GetKeyUp(KeyCode.E)) //Do wyciągania przedmiotu z inv
                {
                    if (!holdingItem)
                    {
                        heldItem = pocket.Withdraw();
                        if (heldItem != null)
                        {
                            heldItem.TakeMeOut();
                            holdingItem = true;
                        }
                    }
                    else
                    {
                        heldItem.UseMe("nothing"); //chowanie przedmiotu (zmienić)
                        heldItem = null;
                        holdingItem = false;
                    }
                }
            }
            else
            {
                anim.SetFloat("speedy", 0);
            }
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((string.Equals(spren.sortingLayerName, "Interaction-front"))&&(string.Equals(collision.gameObject.tag, "prison-wall")))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (string.Equals(collision.gameObject.tag, "guard"))
        {
            aSource.Play();
            enemySprite = collision.gameObject.GetComponent<SpriteRenderer>();
            if (enemyFlipper)
            {
                FlipEnemy();
            }
            GetCaught();
        }
        else
        if (string.Equals(collision.gameObject.tag, "invis"))
            gc.Trig("corridor");
        else
        {
            iText.enabled = true;
            inTrigger = true;
            koliz = collision;
        }
    }

    private void CollisionHandling(Collider2D collision)
    {
        //iText.enabled = true;
        if (string.Equals(collision.gameObject.tag, "guard-only"))
        {
            iText.enabled = false;
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            if (string.Equals(collision.gameObject.tag, "escape"))
            {
                dialSys.ShowTextLine("finale");
                inExit = true;
            }
            else if (string.Equals(collision.gameObject.tag, "guido"))
            {
                Interactible sys = collision.GetComponent<Interactible>();
                sys.TestPickable("guido");
            }
            else if (pocket.isHidden() && (!holdingItem))
            {
                if (string.Equals(collision.gameObject.tag, "door-corridor")) //Przejście na korytarz
                {
                    transform.position = ScreenSwitches.getPlayerIntoCorridor(currentRoom);
                    cam.ChangeRoom("corridor");
                    currentRoom = "corridor";
                }
                else if (collision.gameObject.tag.Contains("door")) //Przejście do innego pokoju
                {
                    string doorcode = collision.gameObject.tag;
                    switch (doorcode)
                    {
                        case "door-prison": SwapRoom("prison"); break;
                        case "door-ritual": SwapRoom("ritual"); break;
                        case "door-watch": SwapRoom("watch"); break;
                        case "door-storage": SwapRoom("storage"); break;
                        default: SwapRoom("prison"); break;
                    }

                }
                else if (string.Equals(collision.gameObject.tag, "pickable")) //Podniesienie przedmiotu
                {
                    Pickable pick = collision.gameObject.GetComponent<Pickable>();
                    pick.PickMeUp();
                    pocket.Put(pick);
                    iText.enabled = false;
                }
                else if (string.Equals(collision.gameObject.tag, "interactible"))
                {
                    Interactible sys = collision.GetComponent<Interactible>();
                    sys.TestPickable("nothing");
                }
            }
            else if (holdingItem && (string.Equals(collision.gameObject.tag, "interactible")))
            {
                Interactible sys = collision.GetComponent<Interactible>();
                if (sys.TestPickable(heldItem.matchingTrig))
                {
                    heldItem.UseMe(sys.matchingString);
                    heldItem = null;
                    holdingItem = false;
                }
                else
                {
                    heldItem.UseMe("nothing");
                    heldItem = null;
                    holdingItem = false;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        iText.enabled = false;
        inTrigger = false;
        koliz = null;
    }

    private void SwapRoom(string to)
    {
        transform.position = ScreenSwitches.getPlayerEntry(currentRoom, to);
        cam.ChangeRoom(to);
        currentRoom = to;
    }

    private void FlipEnemy()
    {
        if (enemySprite.flipX)
            enemySprite.flipX = false;
        else
            enemySprite.flipX = true;
        enemyFlipper = false;
    }

    private void Flip()
    {
        Vector3 scaling = transform.localScale;
        scaling.x *= -1;
        transform.localScale = scaling;
    }

    public void ReStore(Pickable a)
    {
        a.PickMeUp();
        pocket.Put(a);
        pocket.RepickActive();
        holdingItem = false;
    }

    public void SwapLayers()
    {
        spren.sortingLayerName = "Interaction-front";
        spren.sortingOrder = 2;
    }

    public void GetCaught()
    {
        alive = false;
        pocket.SetCaught();
        anim.SetFloat("speedy", 0);
        dialSys.Caught(true);
        dialSys.ShowTextLine("caught");
        anim.ResetTrigger("respawn");
        anim.SetTrigger("caught");
    }

    public void FreezeForMoment()
    {
        anim.SetFloat("speedy", 0);
        alive = false;
    }

    public void Unfreeze()
    {
        alive = true;
    }

    public void AnimEnd()
    {
        transform.position = ScreenSwitches.getPlayerRelive(currentRoom);
        if (string.Equals(currentRoom, "watch"))
        {
            cam.ChangeRoom("corridor");
            currentRoom = "corridor";
        }
        dialSys.Caught(false);
        anim.ResetTrigger("caught");
        anim.SetTrigger("respawn");
        FlipEnemy();
        pocket.SetUncaught();
        enemyFlipper = true;
        alive = true;
    }
}
