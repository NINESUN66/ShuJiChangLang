using JetBrains.Annotations;
using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_Next : MonoBehaviour
{

    public GameObject M0, M1, M2;
    public string ScenceName;

    public int idx = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        idx = 0;
        M0.SetActive(true);
        M1.SetActive(false);
        M2.SetActive(false);
    }

    public void buttonOn()
    {
        idx++;
        if(idx==3) {
            SceneManager.LoadScene(ScenceName);//SampleScene


        }
    }
    public void Zero()
    {
        M0.SetActive(true);
    }
    public void One()
    {
        M1.SetActive(true);

    }
    public void Two()
    {
        M2.SetActive(true);

    }
    /*public void Three()
    {
        M3.SetActive(true);

    }*/
    /*public void Four(string ScenceName)
    {
        SceneManager.LoadScene(ScenceName);
    }*/

    
    // Update is called once per frame
    void Update()
    {
        
            switch (idx)
            {
                case 0:
                    Zero(); break;
                case 1:
                    One(); break;
                case 2:
                    Two(); break;
                default:
                    break;
                /*case 3:
                    Three(); break;*/
                /*case 4:
                    Four(ScenceName); break;*/


            }
        
    }
}
