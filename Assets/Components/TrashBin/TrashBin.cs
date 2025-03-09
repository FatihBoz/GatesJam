using UnityEngine;

public class TrashBin : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Draggable>(out var draggable))
        {
            if (!draggable.isDragging)
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
