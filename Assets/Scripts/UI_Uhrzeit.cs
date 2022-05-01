using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Uhrzeit : MonoBehaviour
{
    public float sekunden;
    public int minuten;
    public int stunden = 8;
    public int tage = 1;
    public int wochentag = 1;
    private string[] tagwort = { "Mon", "Tue", "Wed", "Thr", "Fri", "Sat", "Sun" };

    public TMPro.TMP_Text UITage;
    public TMPro.TMP_Text UIUhrzeit;

    // Update is called once per frame
    void FixedUpdate()
    {
        sekunden += Time.fixedDeltaTime * 36f;
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
            wochentag += 1;
            if(wochentag == 8)
            {
                wochentag = 1;
            }
        }

        UITage.text = tagwort[wochentag-1] + " Tag " + tage;
        UIUhrzeit.text = string.Format("{0:00}:{1:00}", stunden, minuten);
    }
}
