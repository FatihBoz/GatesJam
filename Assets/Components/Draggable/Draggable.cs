using UnityEngine;

public class Draggable : MonoBehaviour
{
    public bool isDragging;
    private Rigidbody2D rb;

    public bool canMove=true;
    public bool doesStartDragging = true;
    void Awake()
    {
        canMove = true;
        rb =GetComponent<Rigidbody2D>();
        isDragging = doesStartDragging;
    }
    private void OnMouseDown()
    {
        if (canMove)
        {
            isDragging = true;
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
    private void Update()
    {
        if (isDragging)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
            }
        }
    }
    private void OnMouseUp()
    {

    }
}
