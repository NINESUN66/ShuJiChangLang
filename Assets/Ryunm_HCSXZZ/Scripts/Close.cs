using UnityEngine;
using UnityEngine.SceneManagement;
public class Close : MonoBehaviour
{
    GameObject book1;
    GameObject book2;
    GameObject book3;
    GameObject book4;
    GameObject book5;
    GameObject book6;
    GameObject book7;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        book1 = GameObject.Find("Canvas1"); 
        book2 = GameObject.Find("Canvas2"); 
        book3 = GameObject.Find("Canvas3"); 
        book4 = GameObject.Find("Canvas4"); 
        book5 = GameObject.Find("Canvas5"); 
        book6 = GameObject.Find("Canvas6"); 
        book7 = GameObject.Find("Canvas7"); 
    }

    public void change()
    {
        book1.SetActive(false);
        book2.SetActive(false);
        book3.SetActive(false);
        book4.SetActive(false);
        book5.SetActive(false);
        book6.SetActive(false);
        book7.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
