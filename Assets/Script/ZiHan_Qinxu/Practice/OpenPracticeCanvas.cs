using UnityEngine;

public class OpenPracticeCanvas : MonoBehaviour
{
    [SerializeField] private Transform practiceArea;
    [SerializeField] private ShangPreview shangPreview;
    [SerializeField] private DrawManager drawManager;
    [SerializeField] private GameObject outPracticeButton;

    private Vector3 tempVec;
    private Vector3 targetPos;
    [SerializeField] private float moveSpeed = 0.5f; // 移动速度
    public bool isMoving = false;
    private float moveProgress = 0f;

    private void Start()
    {
        targetPos = Vector3.zero;
        tempVec = practiceArea.position;
        Debug.Log("初始位置" + tempVec);
    }

    public Vector3 getPosition()
    {
        return tempVec;
    }

    public void DownArea()
    {
        isMoving = true;
        shangPreview.enabled = false;
        drawManager.ClearAllLines();
        outPracticeButton.SetActive(true);
    }

    private void Update()
    {
        if (isMoving)
        {
            moveProgress += Time.deltaTime * moveSpeed;
            practiceArea.position = Vector3.Lerp(practiceArea.position, targetPos, moveProgress);

            if (moveProgress >= 1f || practiceArea.position == targetPos)
            {
                practiceArea.position = targetPos;
                isMoving = false;
                moveProgress = 0f;
                Debug.Log("下降结束");
            }
        }
    }
}
