using System.Collections;
using UnityEngine;

public class BossPopUp : MonoBehaviour
{
    [SerializeField] Vector2 startPos;
    [SerializeField] Vector2 endPos;
    public void PopUp(float movementTime, float waitTime, Sprite visual, GameObject owner)
    {
        //transform.position = popUpPositions[UnityEngine.Random.Range(0, popUpPositions.Length)]; Skip for test

        StartCoroutine(Move());

        IEnumerator Move()
        {
            GetComponent<BossMovement>().Move(movementTime, startPos, endPos);
            yield return new WaitForSeconds(movementTime + waitTime);
            GetComponent<BossMovement>().Move(movementTime, endPos, startPos);
            yield return new WaitForSeconds(movementTime);

            if(owner != null)
            {
                owner.GetComponent<BossPopUpLevelFlow>()?.FinishSection();
            }
        }
    }
}
