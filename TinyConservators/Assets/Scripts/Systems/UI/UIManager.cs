using UnityEngine;
using Unity.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    //All of this is garbage and need to be written better, but getting ready for a test on the 06.04, so need to be a bit janky.
    [SerializeField] FruitSpawner fruitSpawner;
    [SerializeField] GameObject startPanel;
    [SerializeField] TextMeshProUGUI countdown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
    public void ButtonClicked()
    {
        fruitSpawner.TestStartMethod();
        startPanel.SetActive(false);

        //Make counter go down.
    }
}
