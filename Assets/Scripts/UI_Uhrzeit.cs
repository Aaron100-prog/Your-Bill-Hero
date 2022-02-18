using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Uhrzeit : MonoBehaviour
{
    public float sekunden;
    public int minuten;
    public int stunden;
    public int tage;

    public TMPro.TMP_Text UITage;
    public TMPro.TMP_Text UIUhrzeit;

    // Update is called once per frame
    void FixedUpdate()
    {
        sekunden += Time.fixedDeltaTime;

        if(sekunden >= 60)
        {
            sekunden = 0;
            minuten += 1;
        }
        if(minuten >= 60)
        {
            minuten = 0;
            stunden += 1;
        }
        if(stunden >= 24)
        {
            stunden = 0;
            tage += 1;
        }

        UITage.text = "Tag " + tage;
        UIUhrzeit.text = string.Format("{0:00}:{1:00}", stunden, minuten);
    }
}
