using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // ����Ҫ��ת����Ŀ�곡������
    public string sceneToLoad;
    public GameObject StartPanel;
    // ����ť�����ʱ���õķ���
    public void OnButtonClick()
    {

        PlayerPrefs.SetInt("over_Pintu", 1);

        // ʹ�ó�������������Ŀ�곡��
        SceneManager.LoadScene(sceneToLoad);
    }
    public void OnButtonClick1()
    {
        StartPanel.SetActive(true);
        // ʹ�ó�������������Ŀ�곡��
        SceneManager.LoadScene(sceneToLoad);
    }
}
