using UnityEngine;
using UnityEngine.SceneManagement;
public class back : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Back()
    {
        SceneManager.LoadScene("ChoseGames");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
