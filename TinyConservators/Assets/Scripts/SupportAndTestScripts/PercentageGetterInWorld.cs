using TMPro;
using UnityEngine;
using System;
using System.Globalization;

public class PercentageGetterInWorld : MonoBehaviour
{
    [SerializeField] string preText;
    [SerializeField] int statToGet;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StatTracker s;
        GameObject g = GameObject.FindGameObjectWithTag("StatTracker");

        if (g != null)
        {
            s = g.GetComponent<StatTracker>();
            GetComponent<TextMeshPro>().text = $"{preText}\n{(s.GetDamagePercentage(statToGet) * 100):F2}%";
        }
        else 
        {
            Debug.Log("Setting text to 100%");
            GetComponent<TextMeshPro>().text = $"{preText}\n{100.00:F2}%";
        }
        
        
    }
}
