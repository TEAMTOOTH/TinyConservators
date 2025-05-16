using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SceneLoader : MonoBehaviour
{
    int currentSceneIndex;
    AsyncOperation sceneLoad;
    bool loading;

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    public async void LoadScene(float length)
    {
        if (!loading)
        {
            loading = true;
            sceneLoad = SceneManager.LoadSceneAsync(currentSceneIndex + 1);
            sceneLoad.allowSceneActivation = false;
            //await Task.Delay((int)length*1000);
            await Awaitable.WaitForSecondsAsync(length);
            sceneLoad.allowSceneActivation = true;
        }
        
    }
}
