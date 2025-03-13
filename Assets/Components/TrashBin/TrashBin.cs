using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashBin : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static Action OnBottleDestroyed;
    public static bool isOverTrashBin;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isOverTrashBin = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isOverTrashBin = false;
    }

}
