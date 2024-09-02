using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // 设置要跳转到的目标场景名称
    public string sceneToLoad;
    public GameObject StartPanel;
    // 当按钮被点击时调用的方法
    public void OnButtonClick()
    {

        PlayerPrefs.SetInt("over_Pintu", 1);

        // 使用场景管理器加载目标场景
        SceneManager.LoadScene(sceneToLoad);
    }
    public void OnButtonClick1()
    {
        StartPanel.SetActive(true);
        // 使用场景管理器加载目标场景
        SceneManager.LoadScene(sceneToLoad);
    }
}
