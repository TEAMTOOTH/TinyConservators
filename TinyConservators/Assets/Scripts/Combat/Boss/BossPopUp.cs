using System.Collections;
using UnityEngine;

public class BossPopUp : MonoBehaviour
{
    [SerializeField] Vector2 startPos;
    [SerializeField] Vector2 endPos;
    public void PopUp(float movementTime, float waitTime, Sprite visual, GameObject owner, AudioClip shout)
    {
        //transform.position = popUpPositions[UnityEngine.Random.Range(0, popUpPositions.Length)]; Skip for test

        StartCoroutine(Move());

        IEnumerator Move()
        {
            GetComponent<BossMovement>().Move(movementTime, startPos, endPos);
            yield return new WaitForSeconds(movementTime);
            GetComponent<AudioSource>().clip = shout;
            GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(waitTime);
            GetComponent<BossMovement>().Move(movementTime, endPos, startPos);
            yield return new WaitForSeconds(movementTime);

            if(owner != null)
            {
                owner.GetComponent<BossPopUpLevelFlow>()?.FinishSection();
            }
        }
    }

    public void SetVisual(Sprite sprite)
    {
        GetComponentInChildren<SpriteRenderer>().sprite = sprite;
    }
}
