using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DrawManager : MonoBehaviour
{

    [SerializeField] private Line linePrefab;
    
    public UnityEvent OnFinishDrawing;

    private Line currentLine;
    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {

        Vector2 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity, transform);
            currentLine.AddPosition(mousePos);
        }

        if (Input.GetMouseButton(0))
        {
            if(!currentLine) return;
            
            // 如果能加新的点就接着加
            if (currentLine.CanAppend(mousePos))
            {
                currentLine.AddPosition(mousePos);
            }
            else if(currentLine.IsAcuteAngle(mousePos))
            {
                Vector2 lastPos = currentLine.LastPos; 
                currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity, transform);
                currentLine.AddPosition(lastPos);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            OnFinishDrawing?.Invoke();
        }
    }

    public void ClearAllLines() // 清除所有的笔迹
    {
        for (int i = 0; i < transform.childCount; i++)
            Destroy(transform.GetChild(i).gameObject);
    }
}
