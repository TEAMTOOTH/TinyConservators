using System.Collections;
using UnityEngine;

public class FakePickup : MonoBehaviour
{
    AttackPoint damageOwner;
    [SerializeField] float radiusOfExitSpot;

    [SerializeField] Vector2 endPosition;
    public void Spawn(int colorIndex, float timeToEat)
    {
        ChooseColor(colorIndex);
        StartCoroutine(WaitBeforeFix(timeToEat));
    }

    IEnumerator WaitBeforeFix(float timeToEat)
    {
        yield return new WaitForSeconds(timeToEat);
        Fix();
    }

   

    void ChooseColor()
    {
        Animator colorAnimation = GetComponent<Animator>();
        AnimationClip[] clips = colorAnimation.runtimeAnimatorController.animationClips;

        if (clips.Length == 0) return;

        string clipName = clips[Random.Range(0, clips.Length)].name;

        colorAnimation.Play(clipName);
    }

    void ChooseColor(int index)
    {
        Animator colorAnimation = GetComponent<Animator>();
        AnimationClip[] clips = colorAnimation.runtimeAnimatorController.animationClips;

        if (clips.Length == 0) return;

        string clipName = clips[index].name;

        colorAnimation.Play(clipName);
    }




    public void SetOwner(GameObject objectToFix)
    {
        damageOwner = objectToFix.GetComponent<AttackPoint>();

    }

    public void SetEndPosition(Vector2 endPosition)
    {
        this.endPosition = endPosition;

    }

    public void Fix()
    {
        StartCoroutine(TravelToDamagePoint());
        IEnumerator TravelToDamagePoint()
        {
            GetComponent<SoundController>().Play();

         
            gameObject.layer = LayerMask.NameToLayer("NoCollision");


            float moveDuration = 0.6f;
            float time = 0f;

            Vector2 endPos = Vector2.zero;
            //Vector2 endPos = damageOwner.transform.position;

            Vector2 startPos = transform.position;
            
            // Calculate direction and midpoint
            Vector2 direction = endPos - startPos;
            Vector2 midPoint = (startPos + endPos) / 2f;

            // Create large perpendicular offset for the whip arc
            Vector2 offset = Vector2.Perpendicular(direction).normalized * 10f;
            offset *= Random.value > 0.5f ? 1 : -1;

            Vector2 controlPoint = midPoint + offset;

            // Clamp control point to keep it on screen (orthographic + static camera)
            Camera cam = Camera.main;
            Vector2 screenMin = cam.ViewportToWorldPoint(Vector3.zero);
            Vector2 screenMax = cam.ViewportToWorldPoint(Vector3.one);

            // Keep a 1-unit margin inside screen edges
            float margin = 1f;
            controlPoint.x = Mathf.Clamp(controlPoint.x, screenMin.x + margin, screenMax.x - margin);
            controlPoint.y = Mathf.Clamp(controlPoint.y, screenMin.y + margin, screenMax.y - margin);

            // Animate along Bezier curve with strong acceleration
            while (time < moveDuration)
            {
                time += Time.deltaTime;
                float t = time / moveDuration;

                // Aggressive acceleration for whip feel
                float easedT = Mathf.Pow(t, 4);

                // Quadratic Bezier curve formula
                Vector2 pos = Mathf.Pow(1 - easedT, 2) * startPos +
                              2 * (1 - easedT) * easedT * controlPoint +
                              Mathf.Pow(easedT, 2) * endPos;

                transform.position = pos;

                yield return null;
            }

            transform.position = endPos;

            Destroy(gameObject);
            //Destroy(gameObject);
        }


    }

    void Sparkle()
    {
        StartCoroutine(SparkleAndDestroy());
        IEnumerator SparkleAndDestroy()
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            if (GetComponentInChildren<ParticleSystem>() != null)
            {
                GetComponentInChildren<ParticleSystem>().Play();

                //Play sparkle SFX
                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/sparkleClean");
            }

            yield return new WaitForSeconds(1f);
            Destroy(gameObject);

        }
    }

}
