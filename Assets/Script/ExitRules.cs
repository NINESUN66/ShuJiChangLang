using UnityEngine;

public class TimeController : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 0;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Time.timeScale = 1;

            gameObject.SetActive(false);
        }
    }
}
