using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManegarSettings : MonoBehaviour
{
    public int indexScene;

    public void LoadSceneIndex()
    {
        SceneManager.LoadScene(indexScene);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}