using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using UnityEngine.

public class HowToSolve : MonoBehaviour
{
    public GameObject M0,M1,M2,M3,M4;
    public int idx = 0;
    public GameObject LBP, RBP;


    public Button next;
    public Button prev;
    public Button Close;

    public void Start()
    {
        M1.SetActive(true);
        M2.SetActive(false);
        M3.SetActive(false);
        M4.SetActive(false);
        M0.SetActive(false);
        LBP.SetActive(false);
        RBP.SetActive(true);
    }


    public void GoToChosenGame(string ScenceName)
    {
        SceneManager.LoadScene(ScenceName);
    }
    public void nextPanel()
    {
        if(idx<=3)idx++;
    }
    public void prevPanel() {
        
        if(idx > 0)idx--; 
    }

    public void Update()
    {
        Debug.Log("idx is: " + idx);
        switch (idx)
        {

            case 0: 
                Zero();break;
            case 1:
                One();break;
            case 2:
                Two(); break;
            case 3:
                Three(); break;
            case 4:
                Four(); break;
            
        }
    }
    public void Zero()
    {
        M0.SetActive(true);
        M1.SetActive(false);
        M2.SetActive(false);
        M3.SetActive(false);
        M4.SetActive(false);
        
        LBP.SetActive(false);
        RBP.SetActive(true);
    }
    public void One()
    {
        M0.SetActive(false);
        M1.SetActive(true);
        M2.SetActive(false);
        M3.SetActive(false);
        M4.SetActive(false);
        LBP.SetActive(true);
        RBP.SetActive(true);

    }
    public void Two()
    {
        M0.SetActive(false);
        M1.SetActive(false);
        M2.SetActive(true);
        M3.SetActive(false);
        M4.SetActive(false);

        LBP.SetActive(true);
        RBP.SetActive(true);
    }

    public void Three()
    {
        M1.SetActive(false);
        M2.SetActive(false);
        M3.SetActive(true);
        M4.SetActive(false);
        M0.SetActive(false);

        LBP.SetActive(true);
        RBP.SetActive(true);
    }
    public void Four()
    {
        M1.SetActive(false);
        M2.SetActive(false);
        M3.SetActive(false);
        M4.SetActive(true);
        M0.SetActive(false);

        LBP.SetActive(true);
        RBP.SetActive(false);
    }

    



}
