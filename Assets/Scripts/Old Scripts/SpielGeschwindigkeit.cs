using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpielGeschwindigkeit : MonoBehaviour
{
    public void SpielGeschwindigkeit�ndern(float Geschwindigkeit)
    {
        Time.timeScale = Geschwindigkeit;
    }
}
