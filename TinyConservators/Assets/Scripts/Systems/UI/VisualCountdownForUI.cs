using UnityEngine;
using TMPro;

public class VisualCountdownForUI : MonoBehaviour
{
    float counter;
    TextMeshProUGUI text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;

        if(counter > 1)
        {
            counter = 0;
            text.text = (int.Parse(text.text) - 1).ToString();
        }
    }
}
