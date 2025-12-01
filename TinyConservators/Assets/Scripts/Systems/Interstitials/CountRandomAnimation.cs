using System.Collections;
using UnityEngine;

public class CountRandomAnimation : MonoBehaviour
{
    [SerializeField] Animator countAnimator;
    [SerializeField] string[] countDanceAnimation;
    [SerializeField] float timeBetweenDances;

    int previousIndex = -1;

    private void OnEnable()
    {
        ChooseDance();
    }

    int GetRandomDifferent(int min, int max)
    {
        int newNumber = Random.Range(min, max);

        // If the new number matches the last one, call recursively again
        if (newNumber == previousIndex)
            return GetRandomDifferent(min, max);

        previousIndex = newNumber;
        return newNumber;
    }

    IEnumerator RandomDance(int newDance)
    {
        Debug.Log("Play dance " + newDance);
        countAnimator.Play(countDanceAnimation[newDance]);
        yield return new WaitForSeconds(timeBetweenDances);
        ChooseDance();
    }

    void ChooseDance()
    {
        StartCoroutine(RandomDance(GetRandomDifferent(0, countDanceAnimation.Length)));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
