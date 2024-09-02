using UnityEngine;

public class ChoseChallengeMode : MonoBehaviour
{
    [SerializeField] private GameObject choseMode;

    public void OpenPanel()
    {
        choseMode.SetActive(true);
    }
    public void ClosePanel()
    {
        choseMode.SetActive(false);
    }
}
