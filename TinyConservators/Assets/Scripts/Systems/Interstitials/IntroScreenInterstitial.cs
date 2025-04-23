using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScreenInterstitial : MonoBehaviour, IInterstitial
{
    [SerializeField] float length;
    int currentSceneIndex;
    AsyncOperation sceneLoad;
    public void StartInterstitial()
    {
        gameObject.SetActive(true);
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(currentSceneIndex);
        //Maybe just move the camera in

        //Starting to load the scene async, so it is ready when the scene finishes
        LoadScene();

        StartCoroutine(TestOfLoading());
        IEnumerator TestOfLoading()
        {
            yield return new WaitForSeconds(length);
            EndInterstitial();
        } 
    }
    public void EndInterstitial()
    {
        //Allow activation, so scene will switch when done loading.
        sceneLoad.allowSceneActivation = true;
    }

    public float GetLength()
    {
        return length;
    }

    //Probably unececcary, but nice to have it ready to load and jump straight into the next scene.
    public async void LoadScene()
    {
        sceneLoad = SceneManager.LoadSceneAsync(currentSceneIndex + 1);
        sceneLoad.allowSceneActivation = false;
        //await Task.Delay((int)length*1000);
        await Awaitable.WaitForSecondsAsync(length);
        sceneLoad.allowSceneActivation = true;
    }

    
}
