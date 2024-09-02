using UnityEngine;
using UnityEngine.SceneManagement;
public class sl1 : MonoBehaviour
{ 
     GameObject gameObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        gameObject = GameObject.Find("Canvas1");
    }
    public void load()
    {
        gameObject.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
