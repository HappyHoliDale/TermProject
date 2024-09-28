using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragNDrop : Block
{
    private bool draggin = false;


    private void Update()
    {
        if (draggin)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(mousePosition.x, mousePosition.y);
            Debug.DrawRay(transform.position,transform.right* lineSize, Color.red);
            Debug.DrawRay(transform.position, transform.right *-1* lineSize, Color.red);
            Debug.DrawRay(transform.position, transform.up * lineSize, Color.red);
            Debug.DrawRay(transform.position, transform.up *-1* lineSize, Color.red);

            //�����ֵ� �ϴٵ����ͼ� �Ÿ����ϰ� ���� ª�� �Ÿ��� �ִ� ������ ���̱� 
        }
    }


    

    private void OnMouseDown()
    {
        draggin = true;
    }

    private void OnMouseUp()
    {
        draggin = false;

    }
}
