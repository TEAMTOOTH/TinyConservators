using UnityEngine;

public class TestAnimationScript : MonoBehaviour
{
    [SerializeField] Animator animationController;
    [SerializeField] string nameOfAnimation;

    [SerializeField] bool playAnim = false;

    // Update is called once per frame
    void Update()
    {
        if (playAnim)
        {
            animationController.Play(nameOfAnimation);
            playAnim = false;
        }
    }
}
