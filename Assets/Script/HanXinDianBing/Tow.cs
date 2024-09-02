using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tow : MonoBehaviour
{
    
    
    private bool state_drag = false;//drag
    private Vector2 offset;



    private void OnMouseDown()
    {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                // ���������λ�ú��������ĵ�ƫ����
                offset = (Vector2)transform.position - mousePosition;
            state_drag = true;
            }



    }

    private void OnMouseUp()
    {

        state_drag = false;
    }

    private void Update()
    {
        if (state_drag)
        {
            // ������λ������Ϊ���λ�ü���ƫ����
            Vector2 CurPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = CurPosition + offset;
        }
    }




}
