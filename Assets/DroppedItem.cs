using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : Clickable
{
    public string identifier;
    public AudioSource audioSource;
    public override void Click()
    {
        Debug.Log("Apfel eingesammelt");
        audioSource.Play();
        //Destroy(gameObject);
    }
}
