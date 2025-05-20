using TMPro;
using UnityEngine;
using System;
using System.Globalization;

public class PercentageGetter : MonoBehaviour
{ 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StatTracker s = GameObject.FindGameObjectWithTag("StatTracker").GetComponent<StatTracker>();
        GetComponent<TextMeshProUGUI>().text = $"{s.GetDamagePercentage().ToString("P")}%";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
