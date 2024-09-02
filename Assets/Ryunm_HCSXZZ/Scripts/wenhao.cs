using UnityEngine;

public class wenhao : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    GameObject shuoming;
    void Start()
    {
        shuoming = GameObject.Find("shuomingPanel");
    }

    public void open()
    {
        shuoming.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
