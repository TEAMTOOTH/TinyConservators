using System.Collections;
using TMPro;
using UnityEngine;

public class PointsReceiver : MonoBehaviour
{
    [SerializeField] TextMeshPro pointsText;
    [SerializeField] TextMeshPro addedPointsPopUp;
    [SerializeField] float popUpTime;
    int points;

    public void AddPoints(int pointsToAdd)
    {
        points += pointsToAdd;
        if (addedPointsPopUp != null)
        {
            ShowAddedPoints(pointsToAdd);
        }
        pointsText.text = points.ToString();
    }

    void ShowAddedPoints(int pointsToDisplay)
    {
        StartCoroutine(ShowAndHidePointsAdded());
        IEnumerator ShowAndHidePointsAdded()
        {
            addedPointsPopUp.text = $"+{pointsToDisplay}";
            yield return new WaitForSeconds(popUpTime);
            addedPointsPopUp.text = $"";
        }
    }

    public int GetPoints()
    {
        return points;
    }
}
