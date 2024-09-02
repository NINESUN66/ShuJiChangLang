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

    //显示面板

    //Button
    //切换到韩信题解
    public void ChangeTheScence(string ScenceName)
    {
        SceneManager.LoadScene(ScenceName);
        //Debug.Log("跳到新场景");
    }
    //切换到chosenGame
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
