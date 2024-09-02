using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Ryunm_RedLine : MonoBehaviour
{
    public bool isOver = false;
    public float speed = -2;
    GameObject book1;
    GameObject book2;
    GameObject book3;
    GameObject book4;
    GameObject book5;
    GameObject book6;
    GameObject book7;
    GameObject last;
    // Start is called before the first frame update
    void Start()
    {
        book1 = GameObject.Find("Canvas1");book1.SetActive(false);
        book2 = GameObject.Find("Canvas2");book2.SetActive(false);
        book3 = GameObject.Find("Canvas3"); book3.SetActive(false);
        book4 = GameObject.Find("Canvas4"); book4.SetActive(false);
        book5 = GameObject.Find("Canvas5"); book5.SetActive(false);
        book6 = GameObject.Find("Canvas6"); book6.SetActive(false);
        book7 = GameObject.Find("Canvas7"); book7.SetActive(false);
        last = GameObject.Find("victory");last.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isOver)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
            Invoke("Jump", 6);
        }
        if (Ryunm_GameManager._instance.currentScore>=50&&PlayerPrefs.GetInt("isShowed1")==1)
        {
            PlayerPrefs.SetInt("isShowed1", 0);
            book1.SetActive(true);
        }
        if (Ryunm_GameManager._instance.currentScore >= 100 && PlayerPrefs.GetInt("isShowed2") == 1)
        {
            PlayerPrefs.SetInt("isShowed2", 0);
            book2.SetActive(true);
        }
        if (Ryunm_GameManager._instance.currentScore >= 150 && PlayerPrefs.GetInt("isShowed3") == 1)
        {
            PlayerPrefs.SetInt("isShowed3", 0);
            book3.SetActive(true);
        }
        if (Ryunm_GameManager._instance.currentScore >= 200 && PlayerPrefs.GetInt("isShowed4") == 1)
        {
            PlayerPrefs.SetInt("isShowed4", 0);
            book4.SetActive(true);
        }
        if (Ryunm_GameManager._instance.currentScore >= 250 && PlayerPrefs.GetInt("isShowed5") == 1)
        {
            PlayerPrefs.SetInt("isShowed5", 0);
            book5.SetActive(true);
        }
        if (Ryunm_GameManager._instance.currentScore >= 300 && PlayerPrefs.GetInt("isShowed6") == 1)
        {
            PlayerPrefs.SetInt("isShowed6", 0);
            book6.SetActive(true);
        }
        if (Ryunm_GameManager._instance.currentScore >= 350 && PlayerPrefs.GetInt("isShowed7") == 1)
        {
            PlayerPrefs.SetInt("isShowed7", 0);
            book7.SetActive(true);
        }
        if(Ryunm_GameManager._instance.currentScore >= 400)
        {
           last.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Book"))
        {
            if (collision.gameObject.GetComponent<Ryunm_Book>().bookState == Bookstate.Collisioned)
            {
                Ryunm_GameManager._instance.gameState = GameState.GameOver;
                Invoke("SetOver", 1);
            }
        }
        if (Ryunm_GameManager._instance.gameState == GameState.GameOver && isOver)
        {
            Ryunm_GameManager._instance.currentScore += (int)collision.gameObject.GetComponent<Ryunm_Book>().bookType + 1;
            Ryunm_GameManager._instance.currentScoreTxt.text = Ryunm_GameManager._instance.currentScore.ToString();
            Destroy(collision.gameObject);

        }
        if (collision.gameObject.CompareTag("Floor"))
        {
            Ryunm_GameManager._instance.GameOver();
        }
    }
    public void SetOver()
    {
        isOver = true;
    }

    void Jump()
    {
        SceneManager.LoadScene("The_End");
    }
}