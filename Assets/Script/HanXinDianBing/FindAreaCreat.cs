using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class FindAreaCreat : MonoBehaviour
{
   

//̽��
    public Text PersonNumber;
    public const string ChineseNumber = "��ǰ����:";
    private void Start()
    {
        SuccessedText[0] = "�� ϲ �� �� �� �� !\n";
        SuccessedText[1] = "�� �� �� �� �� �� �� һ �� �� �� ʣ �� �� ��\n";
        SuccessedText[2] = "�� �� �� �� �� �� �� �� �� Ϸ \n";
        SuccessedText[3] = "�� Ǹ �� �� �� �� Ŷ ���� �� �� �� ��\n�� �� �� �� �� �� �� �� �� �� ʾ ҳ ��\nʵ �� �� �� �� �� �� �� �� �� �� �� �� �� ��";
        SuccessedPanel.SetActive(false);
    }


    //�������̣�x=a*cos q;y=b*sin q

    public const int SumPoints = 10000; 
    //xΪ���ᣬy�Ƕ���
    public float rX = 5.8f;
    public float rY = 2.8f; 

    public string targetTag = "Soldier"; 

   //����Բ
    private void OnDrawGizmos()
    {
        // ���
        Vector3[] points = new Vector3[SumPoints + 1];
        for (int i = 0; i < SumPoints; i++)
        {
            float angle = (float)i / SumPoints * 360f * Mathf.Deg2Rad;
            float x = Mathf.Sin(angle) * rX;
            float y = Mathf.Cos(angle) * rY;
            points[i] = transform.position + new Vector3(x, y, 0f);
        }
        points[SumPoints] = points[0];

        // ����Χ
        Gizmos.color = Color.yellow;
        for (int i = 0; i < SumPoints; i++)
        {
            Gizmos.DrawLine(points[i], points[i + 1]);
        }
    }

    public int soldierCount = 0;
    public int count = 0;
    private void Update()
    {
        
        
        GameObject[] soldiers = GameObject.FindGameObjectsWithTag("Soldier");
        soldierCount = 0;

        for (int i = 0; i < soldiers.Length; i++)
        {
            Vector3 soldierPosition = soldiers[i].transform.position;

            if (IsIn_TuoYuan(soldierPosition))  soldierCount++;//�ھͼ�һ
            
        }
                            //��ǰ�����ǣ�  xxx��
        PersonNumber.text = ChineseNumber + soldierCount.ToString();
      //  Debug.Log(ChineseNumber + soldierCount);
        count = soldierCount;

        if (count == 23)
        {
            SuceessedText1.text = SuccessedText[0] + SuccessedText[1] + SuccessedText[2];
        }
        else
        {

            SuceessedText1.text = SuccessedText[3];
            //SuccessedPanel.SetActive(true);

        }
        //Debug.Log(" ʿ�������� " + count);

    }

    public int GetSoldier()
    {
        return soldierCount;
    }
    
    private bool IsIn_TuoYuan(Vector3 point)
        {
            float X = (point.x - transform.position.x) / rX;
            float Y = (point.y - transform.position.y) / rY;

            return (X * X + Y * Y) <= 1;
        
        }

    /////////////////

    //����  �ύ��ť
    public GameObject SuccessedPanel;
    public string[] SuccessedText = new string[4];

    public Text SuceessedText1;//��ʾ��

    public Button submitButton;
    public Button nextScence;
    public Button returnScence;
    public Button Close;
    //public string UnSuccessedText;


}