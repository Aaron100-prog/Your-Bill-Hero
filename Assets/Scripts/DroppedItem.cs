using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallenfruit : Clickable
{
    public string identifier;
    public AudioSource audioSource;
    public Treefruitspawner origintree;
    public override void Click()
    {
        audioSource.Play();
        InventoryStorageManager.instance.AddItemtoPersonalinventory(identifier, 1);
        origintree.currentfruits--;
        Destroy(gameObject);
    }
}
