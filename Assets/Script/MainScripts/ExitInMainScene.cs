using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ExitInMainScene : MonoBehaviour
{
    public GameObject ButtonOnPanel;
    public Button GetInButton;
    public Button GetOutButton;

    

    void Start()
    {
        ButtonOnPanel.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Enter_Next(string ScenceName)
    {
        SceneManager.LoadScene(ScenceName);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
