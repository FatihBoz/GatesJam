using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChoppingBoard : MonoBehaviour
{
    SliceManager sliceManager;
    public List<Sliceable> slicedObjectsOnBoard=new List<Sliceable>();

    public bool placed;
    public bool isObjectInBoard;

    public Transform choppingPoint;

    public bool isChopperOnHand;
    public Chopper chopper;
    public Sliceable newSliceable;

    private void Update()
    {
        if (newSliceable!=null && placed)
        {
            Draggable draggable = newSliceable.GetComponent<Draggable>();
            if (!draggable.isDragging)
            {
                AddItem(newSliceable);
            }
        }
    }
    public void AddItem(Sliceable theSliceable)
    {
        if (!slicedObjectsOnBoard.Contains(theSliceable))
        {
            isObjectInBoard = true;
            sliceManager.slicedObjects.Add(theSliceable.gameObject);
            Rigidbody2D newsLrb = theSliceable.GetComponent<Rigidbody2D>();
            newsLrb.bodyType = RigidbodyType2D.Kinematic;
            newsLrb.linearVelocity = Vector2.zero;
            newsLrb.constraints = RigidbodyConstraints2D.FreezeRotation;
            Vector3 tempPos = theSliceable.transform.position;
            tempPos.y = choppingPoint.position.y;
            theSliceable.transform.position = tempPos;
            theSliceable.transform.eulerAngles = new Vector3(0, 0, -50f);
            slicedObjectsOnBoard.Add(theSliceable);
            placed = false;
            theSliceable = null;
            sliceManager.sliceEnabled = true;

        }
    }

    void Start()
    {
        sliceManager = GetComponent<SliceManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Sliceable>(out var sliceable))
        {
            newSliceable = sliceable;
            placed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Sliceable>(out var sliceable))
        {
            if (slicedObjectsOnBoard.Contains(sliceable))
            {
                slicedObjectsOnBoard.Remove(sliceable);
                sliceManager.slicedObjects.Remove(sliceable.gameObject);
                sliceable.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                placed = false;
                if (slicedObjectsOnBoard.Count>0)
                {
                    isObjectInBoard = true;
                }
                else
                {
                    sliceManager.sliceEnabled = false;
                }
            }
        }
    }
}
