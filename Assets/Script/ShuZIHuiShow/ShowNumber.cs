using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowNumber : MonoBehaviour
{
    [SerializeField] private Text targetNumber;
    public int TestTimes;
    public List<int> randomNumbers = new List<int>();
    private System.Random random = new System.Random();
    public int currentIndex = 0;

    public List<int> GenerateRandomNumbers(int count) // 生成不重复数的数组
    {
        List<int> generatedNumbers = new List<int>();

        while (generatedNumbers.Count < count)
        {
            int randomNumber = random.Next(0, 10);
            if (!generatedNumbers.Contains(randomNumber))
            {
                generatedNumbers.Add(randomNumber);
            }
        }

        return generatedNumbers;
    }

    private void Start()
    {
        TestTimes = 8;
        randomNumbers = GenerateRandomNumbers(TestTimes);
    }

    private void Update()
    {
        targetNumber.text = randomNumbers[currentIndex].ToString(); // 显示当前的阿拉伯数字
    }
}
