using System;
using UnityEngine;

public class TrashBin : MonoBehaviour
{
    public static Action OnBottleDestroyed;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Draggable>(out var draggable))
        {
            if (!draggable.isDragging)
            {
                OnBottleDestroyed?.Invoke();
                Destroy(collision.gameObject);
            }
        }
    }
}
