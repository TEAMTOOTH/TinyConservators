using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //All of this is temporary for a test on 07.04
    public void DetermineWinner()
    {
        int highestScore = 0;
        int highestScoringPlayer = 0; 
        
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        for(int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<WalkingMovement>().SetCanMove(false);
            PointsReceiver pr = players[i].GetComponent<PointsReceiver>();

            if(pr.GetPoints() > highestScore)
            {
                highestScore = pr.GetPoints();
                highestScoringPlayer = i;
            }

            players[i].GetComponentInChildren<TextMeshPro>().text = "Loser";
        }

        players[highestScoringPlayer].GetComponentInChildren<TextMeshPro>().text = "Winner";

    }
}
