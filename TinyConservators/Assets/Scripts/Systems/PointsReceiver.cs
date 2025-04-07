using TMPro;
using UnityEngine;

public class PointsReceiver : MonoBehaviour
{
    int points;

    public void AddPoints(int pointsToAdd)
    {
        points += pointsToAdd;
        //This is incredibly temporary, find a better way soon!!!!
        GetComponentInChildren<TextMeshPro>().text = points.ToString();
    }

    public int GetPoints()
    {
        return points;
    }
}
