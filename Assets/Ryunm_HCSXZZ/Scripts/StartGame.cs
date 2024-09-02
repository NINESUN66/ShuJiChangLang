using UnityEngine;
using UnityEngine.SceneManagement;
public class StartGame : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerPrefs.SetInt("isShowed1", 1);
        PlayerPrefs.SetInt("isShowed2", 1);
        PlayerPrefs.SetInt("isShowed3", 1);
        PlayerPrefs.SetInt("isShowed4", 1);
        PlayerPrefs.SetInt("isShowed5", 1);
        PlayerPrefs.SetInt("isShowed6", 1);
        PlayerPrefs.SetInt("isShowed7", 1);
        PlayerPrefs.SetInt("dy",0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void start(string str)
    {
        SceneManager.LoadScene(str);
    }
}
