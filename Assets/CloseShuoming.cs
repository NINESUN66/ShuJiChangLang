using UnityEngine;

public class CloseShuoming : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    GameObject shuoming;
    void Start()
    {
        shuoming = GameObject.Find("shuomingPanel");
    }

    public void close()
    {
        shuoming.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
