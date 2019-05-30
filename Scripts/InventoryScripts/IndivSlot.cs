using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndivSlot : MonoBehaviour {

    public Text description;
    public bool isFilled;
    private Pickable storedItem;

	// Use this for initialization
	void Start () {
        description.text = "";
        description.color = Color.gray;
        isFilled = false;
	}

    //Wkładanie przedmiotu
    public void StoreItem(Pickable it)
    {
        storedItem = it;
        description.text = storedItem.indivName;
        isFilled = true;
    }

    //Wyciąganie przedmiotu
    public Pickable WithdrawItem()
    {
        Pickable it = storedItem;
        Empty();
        return it;
    }

    //Opróżnianie kieszeni
    public void Empty()
    {
        storedItem = null;
        isFilled = false;
        description.text = "";
    }

    //Do przesuwania
    public void Transfer(IndivSlot newslot)
    {
        storedItem = newslot.storedItem;
        description.text = newslot.description.text;
        isFilled = newslot.isFilled;
        newslot.Empty();
    }
}
