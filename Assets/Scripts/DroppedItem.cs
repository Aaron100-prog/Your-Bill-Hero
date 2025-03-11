using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallenfruit : Clickable
{
    public string identifier;
    public AudioSource audioSource;
    public Treefruitspawner origintree;
    float maxpickupdistance = 3f;

    public override void Click(bool inCharacterMode)
    {
        if (inCharacterMode)
        {
            if ((transform.position - playercontroller.instance.transform.position).magnitude < maxpickupdistance)
            {
                audioSource.Play();
                InventoryStorageManager.instance.AddItemtoPersonalinventory(identifier, 1);
                origintree.currentfruits--;
                Destroy(gameObject);
            }
        }
    }
}
