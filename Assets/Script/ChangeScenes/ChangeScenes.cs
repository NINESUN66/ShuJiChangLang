using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
    public void Exit()
    {
        Application.Quit();
    }
    public void ChangeScene(string ScenceName)
    {
        SceneManager.LoadScene(ScenceName);
    }
}
