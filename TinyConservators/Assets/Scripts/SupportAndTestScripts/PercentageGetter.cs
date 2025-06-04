using TMPro;
using UnityEngine;
using System;
using System.Globalization;

public class PercentageGetter : MonoBehaviour
{ 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StatTracker s;
        GameObject g = GameObject.FindGameObjectWithTag("StatTracker");

        if (g != null)
        {
            s = g.GetComponent<StatTracker>();
            GetComponent<TextMeshProUGUI>().text = $"{(s.GetDamagePercentage() * 100):F2}%";
        }
        else 
        {
            Debug.Log("Setting text to 100%");
            GetComponent<TextMeshProUGUI>().text = $"{100.00:F2}%";
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
