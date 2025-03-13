using DG.Tweening;
using UnityEngine;

public class PressMachine : MonoBehaviour
{
    public Transform presser;
    private bool pressed;
    private float timer;
    public float pressTimer = 1f;
    private Vector3 defaultPosition;

    public Pressable PressingObject;
    public bool isObjectInPress;

    public Transform pressingPoint;
    public bool placed;


    void Update()
    {

        if (PressingObject!=null && placed)
        {
            Draggable draggable = PressingObject.GetComponent<Draggable>();
            if (!draggable.isDragging)
            {
                isObjectInPress = true;

                if (PressingObject.TryGetComponent<Rigidbody2D>(out var rb))
                {
                    rb.bodyType = RigidbodyType2D.Kinematic;
                    rb.linearVelocity = Vector2.zero;
                    rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                }


                PressingObject.transform.eulerAngles = Vector3.zero;
                PressingObject.transform.position = pressingPoint.position;
                placed = false;
            }
        }
        if (pressed && Time.time >= timer + pressTimer)
        {
            presser.DOMove(defaultPosition, 1f);
            pressed = false;
        }

    }
    public void Press()
    {
        if (isObjectInPress)
        {
            Draggable draggable = PressingObject.GetComponent<Draggable>();
            draggable.canMove = false;
            defaultPosition = presser.position;
            presser.DOLocalMoveY(-1f, 0.1f).OnComplete(() =>
            {
                draggable.canMove = true;
            });
            PressingObject.Press();
        }
        else
        {
            defaultPosition = presser.position;
            presser.DOLocalMoveY(-1f, 0.1f);
        }
        
        pressed = true;
        timer=Time.time;

    }
    /*
    public bool IsObjectStillPressable()
    {
        if (!isObjectInPress)
        {
            return false;
        }
        return PressingObject.IsOnLastStage();

    }
    */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Pressable>(out var pressable) && !isObjectInPress)
        {
            PressingObject = pressable;
            placed = true;
        }
    }
   
    private void OnTriggerExit2D(Collider2D collision)
    {
       
        if (collision.TryGetComponent<Pressable>(out var pressable))
        {
            if (PressingObject == pressable)
            {
                PressingObject = null;
                placed = false;
                isObjectInPress = false;

            }
        }
    }
}
