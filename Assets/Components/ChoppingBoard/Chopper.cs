using UnityEngine;

public class Chopper : MonoBehaviour
{
    public ChoppingBoard choppingBoard;
    private Draggable draggable;
    private void Awake()
    {
        draggable=gameObject.AddComponent<Draggable>();
    }
    void Start()
    {
        draggable.isDragging = false;
    }

    void Update()
    {
        
    }
    public bool IsChopperOnHand()
    {
        return draggable.isDragging;
    }

}
