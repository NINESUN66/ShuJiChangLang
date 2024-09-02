using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using Mono.Data.Sqlite;
using System;

public class EndingControl : MonoBehaviour
{
    [SerializeField] private GameObject EndingPanel;
    [SerializeField] private Text tipsText;
    [SerializeField] private Text timer;
    [SerializeField] private DifficultMode difficultMode;
    private string sceneName;

    public void Ending(bool is_win) // 显示游戏结束的panel和提示词
    {
        EndingPanel.SetActive(true);
        Scene scene = SceneManager.GetActiveScene();
        sceneName = scene.name;
        tipsText.text = GetTips(is_win);
    }

    double GetTime()
    {
        return double.Parse(timer.text.Replace(":","."));
    }

    // 连接到sqlite
    [SerializeField] private string dbFileName = "ShangMa.db";
    private string path;
    private SqliteConnection con;
    private SqliteCommand command;
    private void ConnectDB()
    {
        path = Path.Combine(Application.streamingAssetsPath, dbFileName);
        con = new SqliteConnection("URI=file:" + path);
        con.Open();
    }

    double GetDBTime(bool diff) // 读取数据库中的最快时间
    {
        ConnectDB();
        string query;
        if (!diff) query = "SELECT Min(EasyTime) FROM Score";
        else query = "SELECT Min(DifficultTime) FROM Score";
        command = new SqliteCommand(query, con);
        object result = command.ExecuteScalar();
        double dbTime = result != null ? Convert.ToDouble(result) : 99.0;
        con.Close();
        return dbTime;
    }

    private void SaveToDB(double time, bool diff)
    {
        ConnectDB();
        string query;
        if (!diff) query = $"UPDATE Score SET EasyTime={time}";
        else query = $"UPDATE Score SET DifficultTime={time}";
        command = new SqliteCommand(query, con);
        command.ExecuteScalar();
        con.Close();
    }

    string GetTips(bool is_Win)
    {
        string res = "";
        if (is_Win && sceneName != "ShuZiHuiShow")
        {
            var time = GetTime();
            var dbtime = GetDBTime(difficultMode.is_Difficult);
            res += $"恭喜您胜利啦！用时{time}s";
            var timeDiff = dbtime - time;
            if (timeDiff < 0)
            {
                res += $"\n距离最快成绩只差{timeDiff}s!";
            }
            else
            {
                res += "\n是您最快的一次哦！";
                if (timeDiff != 0) SaveToDB(time, difficultMode.is_Difficult);
            }
        }
        else if (is_Win && sceneName == "ShuZiHuiShow") res += "恭喜你胜利啦！";
        else if (!is_Win) res += "很遗憾，请继续努力。";

        return res;
    }
}
