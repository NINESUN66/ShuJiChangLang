//using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    Ready = 0,
    Running = 1,
    GameOver = 2
}
public class Ryunm_GameManager : MonoBehaviour
{

    public Ryunm_RedLine redLineInstance;
    public static Ryunm_GameManager _instance;

    public GameObject[] books;
    public Transform startPos;

    public GameObject currentBook;
    public GameState gameState = GameState.Ready;

    public int currentScore;
    public Text currentScoreTxt;
    public Text highestScoreTxt;

    public AudioSource floorAudio;
    public AudioSource combineAudio;

    private Vector3 touchStartPosition;

    private void Awake()
    {
        _instance = this;
        if (PlayerPrefs.HasKey("dy"))
        {
            currentScore = PlayerPrefs.GetInt("dy");
            currentScoreTxt.text = currentScore.ToString();
        }
        else
        {
            currentScore = 0;
        }
    }

    void Start()
    {
        GameStart();
    }

    void Update()
    {
        if (gameState != GameState.Running) return;

        if (currentBook != null)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    touchStartPosition = touch.position;
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    Vector3 touchDeltaPosition = (Vector3)touch.position - (Vector3)touchStartPosition;
                    float clampedX = Mathf.Clamp(currentBook.transform.position.x + touchDeltaPosition.x * Time.deltaTime, -2.5f, 2.5f);
                    currentBook.transform.position = new Vector3(clampedX, currentBook.transform.position.y, currentBook.transform.position.z);
                    touchStartPosition = touch.position;
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    DropBook();
                }
            }
        }
    }

    void GameStart()
    {
        CreateNewBook();
        gameState = GameState.Running;
        float historyHighestScore = PlayerPrefs.GetFloat("HCSJ_HighestScore");
        highestScoreTxt.text = historyHighestScore.ToString();
    }

    public void GameOver()
    {
        float historyHighestScore = PlayerPrefs.GetFloat("HCSJ_HighestScore");
        if (currentScore > historyHighestScore)
        {
            PlayerPrefs.SetFloat("HCSJ_HighestScore", currentScore);
        }
    }

    public void DropBook()
    {
        if (currentBook != null)
        {
            currentBook.GetComponent<Rigidbody2D>().gravityScale = 1;
            currentBook.GetComponent<Ryunm_Book>().bookState = Bookstate.Falling;
            currentBook = null;
            Invoke("CreateNewBook", 1);
        }
    }

    void CreateNewBook()
    {
        if (books.Length == 0 || startPos == null)
        {
            redLineInstance.isOver = true;
            return;
        }
        float randomValue = Random.Range(0, 3);
        int index = (int)randomValue;
        GameObject bookPrefabs = books[index];
        GameObject newBook = Instantiate(bookPrefabs, startPos.position, bookPrefabs.transform.rotation);

        Rigidbody2D rigid = newBook.GetComponent<Rigidbody2D>();
        if (rigid)
        {
            rigid.gravityScale = 0;
        }
        currentBook = newBook;
        currentBook.GetComponent<Ryunm_Book>().bookState = Bookstate.Standby;

    }

    public void CombineNewBook(BookType currentType, Vector2 pos)
    {
        if (currentType == BookType.Seven) return;

        int index = (int)currentType + 1;
        GameObject bookObj = books[index];
        GameObject newBook = Instantiate(bookObj, pos, bookObj.transform.rotation);

        currentScore += (int)currentType + 1;
        if (currentScoreTxt)
        {
            currentScoreTxt.text = currentScore.ToString();
        }
        PlayCombineAudio();
        PlayerPrefs.SetInt("dy", currentScore);
    }

    public void PlayFloorAudio()
    {
        if (floorAudio)
        {
            floorAudio.Play();
        }
    }

    public void PlayCombineAudio()
    {
        if (combineAudio)
        {
            combineAudio.Play();
        }
    }
}
