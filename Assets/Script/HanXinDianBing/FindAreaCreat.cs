using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class FindAreaCreat : MonoBehaviour
{
   

//探测
    public Text PersonNumber;
    public const string ChineseNumber = "当前人数:";
    private void Start()
    {
        SuccessedText[0] = "恭 喜 你 答 对 啦 !\n";
        SuccessedText[1] = "点 击 右 下 角 了 解 一 下 中 国 剩 余 定 理\n";
        SuccessedText[2] = "点 击 左 下 角 退 出 该 游 戏 \n";
        SuccessedText[3] = "抱 歉 人 数 不 对 哦 ，再 检 查 检 查\n点 击 右 上 方 叉 号 关 闭 提 示 页 面\n实 在 不 会 就 点 击 右 下 角 查 看 题 解 吧";
        SuccessedPanel.SetActive(false);
    }


    //参数方程：x=a*cos q;y=b*sin q

    public const int SumPoints = 10000; 
    //x为长轴，y是短轴
    public float rX = 5.8f;
    public float rY = 2.8f; 

    public string targetTag = "Soldier"; 

   //建椭圆
    private void OnDrawGizmos()
    {
        // 算点
        Vector3[] points = new Vector3[SumPoints + 1];
        for (int i = 0; i < SumPoints; i++)
        {
            float angle = (float)i / SumPoints * 360f * Mathf.Deg2Rad;
            float x = Mathf.Sin(angle) * rX;
            float y = Mathf.Cos(angle) * rY;
            points[i] = transform.position + new Vector3(x, y, 0f);
        }
        points[SumPoints] = points[0];

        // 画范围
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

            if (IsIn_TuoYuan(soldierPosition))  soldierCount++;//在就加一
            
        }
                            //当前人数是：  xxx人
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
        //Debug.Log(" 士兵人数： " + count);

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

    //子类  提交按钮
    public GameObject SuccessedPanel;
    public string[] SuccessedText = new string[4];

    public Text SuceessedText1;//显示的

    public Button submitButton;
    public Button nextScence;
    public Button returnScence;
    public Button Close;
    //public string UnSuccessedText;


}