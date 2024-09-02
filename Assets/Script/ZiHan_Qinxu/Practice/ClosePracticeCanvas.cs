using UnityEngine;

public class ClosePracticeCanvas : MonoBehaviour
{
    [SerializeField] private Transform practiceArea;
    [SerializeField] private ShangPreview shangPreview;
    [SerializeField] private DrawManager drawManager;
    [SerializeField] private GameObject outPracticeButton;
    [SerializeField] private OpenPracticeCanvas openPracticeCanvas;

    private Vector3 targetPos;
    [SerializeField] private float moveSpeed = 0.5f; // 移动速度
    private bool isMoving = false;
    private float moveProgress = 0f;

    public void UpArea()
    {
        targetPos = GameObject.Find("ClickEvent").GetComponent<OpenPracticeCanvas>().getPosition();
        isMoving = true;
        shangPreview.enabled = true;
        drawManager.ClearAllLines();
        outPracticeButton.SetActive(false);
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
                Debug.Log("上升结束");
            }
        }
    }
}
