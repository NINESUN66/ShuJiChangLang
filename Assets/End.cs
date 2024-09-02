using UnityEngine;

public class End : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject Win_pannel;
    public GameObject Btn_back_game;
    public GameObject how;
    public GameObject Music;
    public GameObject close;
    public GameObject Joystick;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Btn_back_game.SetActive(false);
        Win_pannel.SetActive(true);
        Music.SetActive(false);
        close.SetActive(false);
        how.SetActive(false);
        Joystick.SetActive(false);
    }
}
