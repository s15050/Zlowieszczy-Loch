using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour {

    private bool hidden;
    private Animator anim;
    private int selected;
    private int filled;
    private bool hidable;

    public DialogSystem dialSys;

    public GameObject slot0;
    public GameObject slot1;
    public GameObject slot2;
    public GameObject slot3;
    public GameObject slot4;

    private List<GameObject> slots;


	void Start () {
        slots = new List<GameObject>();
        slots.Add(slot0);
        slots.Add(slot1);
        slots.Add(slot2);
        slots.Add(slot3);
        slots.Add(slot4);

        selected = 0;
        filled = -1;
        SetSlotActive(selected, true);
        hidden = true;
        hidable = true;
        anim = GetComponent<Animator>();
        anim.enabled = false;

	}

    //Obsługa
    //------------------------------------------
    void Update () {
        if ((!dialSys.visible) && hidable)
        {
            //Otwieranie/zamykanie spacją
            if (hidden && Input.GetKeyUp(KeyCode.Space))
                ShowMe();
            else if (!hidden && Input.GetKeyUp(KeyCode.Space))
                HideMe();
        }

        //Przeglądanie przedmiotów
        if (!hidden)
        {
            if (Input.GetKeyDown(KeyCode.A) && selected > 0)
            {
                SetSlotActive(selected, false);
                selected--;
                SetSlotActive(selected, true);
            }
            else if (Input.GetKeyDown(KeyCode.D) && selected < 4)
            {
                SetSlotActive(selected, false);
                selected++;
                SetSlotActive(selected, true);
            }
        }
	}

    //Wysuwanie i chowanie inv.
    //------------------------------------------
    private void ShowMe()
    {
        anim.enabled = true;
        SetSlotActive(selected, false);
        selected = 0;
        SetSlotActive(selected, true);
        anim.Play("SlideIn");
        hidden = false;
    }

    private void HideMe()
    {
        hidden = true;
        foreach (GameObject s in slots)
        {
            Text desc = s.GetComponent<IndivSlot>().description;
            desc.color = Color.grey;
        }
        anim.Play("SlideOut");
    }

    public void SetCaught()
    {
        hidable = false;
    }

    public void SetUncaught()
    {
        hidable = true;
    }

    //Zmiana koloru slotu
    //------------------------------------------
    private void SetSlotActive(int i, bool active) //Zmiana slotu (i) na aktywny (true) lub nieaktywny (false)
    {
        Image sel = slots[i].GetComponent<Image>();
        Text desc = slots[i].GetComponent<IndivSlot>().description;
        if (active)
        {
            sel.color = Color.grey;
            desc.color = Color.white;
        }
        else
        {
            sel.color = Color.black;
            desc.color = Color.grey;
        }
    }

    //Interakcje z graczem
    //------------------------------------------
    public bool isHidden()
    {
        return hidden;
    }

    //Dodanie przedmiotu
    public void Put(Pickable newThing)
    {
        IndivSlot place = slots[filled+1].GetComponent<IndivSlot>();
        place.StoreItem(newThing);
        SetSlotActive(filled + 1, false);
        filled++;
    }

    public void RepickActive()
    {
        SetSlotActive(selected, true);
        if (hidden)
        {
            foreach (GameObject s in slots)
            {
                Text desc = s.GetComponent<IndivSlot>().description;
                desc.color = Color.grey;
            }
        }
    }

    //Wyciągnięcie przedmiotu
    public Pickable Withdraw()
    {
        IndivSlot place = slots[selected].GetComponent<IndivSlot>();
        if (place.isFilled)
        {
            Pickable ret = place.WithdrawItem();
            Rearrange();
            return ret;
        }
        else return null;
    }

    //Przesuwanie przedmiotów po wyciągnięciu
    private void Rearrange()
    {
        for (int i = selected; i<filled; i++)
        {
            slots[i].GetComponent<IndivSlot>().Transfer(slots[i + 1].GetComponent<IndivSlot>());
        }
        slots[4].GetComponent<IndivSlot>().Empty();
        filled--;
    }
}
