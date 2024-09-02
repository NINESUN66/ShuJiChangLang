using UnityEngine;
using UnityEngine.SceneManagement;

public class returnToChosenGames : MonoBehaviour
{
    public void ChangeScene(string ScenceName)
    {
        SceneManager.LoadScene(ScenceName);
    }
}
