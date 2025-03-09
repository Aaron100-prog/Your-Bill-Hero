using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryStorageManager : MonoBehaviour
{
    public int maxdifferentitems;
    public int maxitems;
    public List<personalinventoryentry> Personalinventoryentries = new List<personalinventoryentry>();

    public static InventoryStorageManager instance;
    public void AddItemtoPersonalinventory(string itemidentifier, int amount)
    {
        bool founditem = false;
        for (int i = 0; i < Personalinventoryentries.Count; i++)
        {
            if (Personalinventoryentries[i].identifier == itemidentifier)
            {
                founditem = true;
                Personalinventoryentries[i].amount = Personalinventoryentries[i].amount + amount;
            }
        }

        if (!founditem)
        {
            personalinventoryentry newentry = new personalinventoryentry();
            newentry.identifier = itemidentifier;
            newentry.amount = amount;

            Personalinventoryentries.Add(newentry);
        }
    }

    public class personalinventoryentry
    {
        public string identifier;
        public int amount;
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
}
