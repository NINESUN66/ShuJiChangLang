using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SubmitButton : FindAreaCreat
{


        
    private void Start()
    {
        

    }
    private void Update()
    {
        
    }

    //��ʾ���

    //Button
    //�л����������
    public void ChangeTheScence(string ScenceName)
    {
        SceneManager.LoadScene(ScenceName);
        //Debug.Log("�����³���");
    }
    //�л���chosenGame
    public void ChangeScenceToChonsenGame(string ScenceName)
    {
        SceneManager.LoadScene(ScenceName);
    }
    public void dispear()
    {
        SuccessedPanel.SetActive(false);
    }


    public void appear()
    {
        
        SuccessedPanel.SetActive(true);
    }

    
}
