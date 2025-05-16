using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public UnityEvent onStart;
    private void Start()
    {
        onStart.Invoke();
    }
}
