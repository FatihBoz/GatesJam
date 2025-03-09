using UnityEngine;

public class DraggableWithoutRb : MonoBehaviour
{
    private bool isDragging;

    private void OnMouseDown()
    {
        isDragging = true;
    }

    private void Update()
    {
        if (isDragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x, mousePos.y, 0);
        }
    }


    private void OnMouseUp()
    {
        isDragging=false;
    }
}
