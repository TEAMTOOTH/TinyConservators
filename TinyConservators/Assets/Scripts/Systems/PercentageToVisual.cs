using UnityEngine;

public class PercentageToVisual : MonoBehaviour
{
    [SerializeField] Sprite[] visuals;

    /// <summary>
    /// Give a percentage to set a spriteIndex
    /// </summary>
    /// <param name="percentage"></param>
    public void SetVisual(float percentage)
    {
        int index = Mathf.FloorToInt(percentage * visuals.Length);
        index = Mathf.Clamp(index, 0, visuals.Length - 1);

        GetComponent<SpriteRenderer>().sprite = visuals[index];
    }
}
