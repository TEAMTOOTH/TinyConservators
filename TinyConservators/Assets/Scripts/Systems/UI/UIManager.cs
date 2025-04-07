using UnityEngine;
using Unity.UI;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour
{

    //All of this is garbage and need to be written better, but getting ready for a test on the 06.04, so need to be a bit janky.
    [SerializeField] FruitSpawner fruitSpawner;
    [SerializeField] GameObject startPanel;
    [SerializeField] TextMeshProUGUI countdown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
    public void ButtonClicked()
    {
        startPanel.SetActive(false);
        fruitSpawner.TestStartMethod();

        StartCoroutine(StartCountdown());
        
        //Make counter go down.
    }

    //This is temporary and trash, fix soon!
    void TempCountdown()
    {
        int.TryParse(countdown.text, out int result);
        if(result > 0)
        {
            countdown.text = $"{result - 1}";
        }
        
    }

    IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(1);
        countdown.text = 2.ToString();
        yield return new WaitForSeconds(1);
        countdown.text = 1.ToString();
        yield return new WaitForSeconds(1);
        countdown.text = 60.ToString();
        InvokeRepeating("TempCountdown", 1f, 1f);
    }
}
