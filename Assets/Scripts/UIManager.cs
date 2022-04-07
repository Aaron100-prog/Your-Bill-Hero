using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMPro.TMP_Dropdown TilemapDropdown;

    public GameObject Tilemap1;

    public void ChangeTilemap()
    {
        if(TilemapDropdown.value == 1)
        {
            Tilemap1.SetActive(true);
        }
        else
        {
            Tilemap1.SetActive(false);
        }
    }
}
