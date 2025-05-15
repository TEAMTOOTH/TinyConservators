using UnityEngine;
using TMPro;
using System.Collections;

public class CountdownLevelFlow : MonoBehaviour, ILevelFlowComponent
{

    [SerializeField] TextMeshProUGUI countDownText;
    [SerializeField] int countDownAmount;

    LevelFlowManager owner;
    public void FinishSection()
    {
        owner.ProgressFlow();
    }

    public void StartSection(LevelFlowManager flowManager)
    {
        Debug.Log("Starting countdown");
        owner = flowManager;
        countDownText.gameObject.SetActive(true);
        countDownText.text = countDownAmount.ToString();
        CountDown();
        
    }

    //A bit messy of an method, but works fine.
    void CountDown()
    {
        StartCoroutine(Counting());
        IEnumerator Counting()
        {
            float totalCountDown = countDownAmount;
            float timePassedInSecond = 0;

            while(totalCountDown > 0)
            {
                totalCountDown -= Time.deltaTime;
                timePassedInSecond += Time.deltaTime;
                if(timePassedInSecond > 1 && Mathf.Round(totalCountDown) != 0)
                {
                    timePassedInSecond = 0;
                    countDownText.text = Mathf.Round(totalCountDown).ToString();
                }
                yield return null;
            }
            
            
            //for(int i = countDownAmount; i > 0; i--) 
            //{
            //    yield return new WaitForSeconds(1);
            //    countDownText.text = i.ToString();
            //}
            
            countDownText.gameObject.SetActive(false);
            FinishSection();
        }
    }

    
}
