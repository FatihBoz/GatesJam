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

    void Update()
    {
        if (pressed && Time.time >= timer + pressTimer)
        {
            presser.DOMove(defaultPosition, 1f);
            pressed = false;
        }
    }
    public void Press()
    {
        defaultPosition = presser.position;
        presser.DOMoveY(0.5f, 0.1f);
        pressed = true;
        timer=Time.time;

    }

    public bool IsObjectStillPressable()
    {
        if (!isObjectInPress)
        {
            return false;
        }
        return PressingObject.IsOnLastStage();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Pressable>(out var pressable))
        {
            Debug.Log("girdim");
            PressingObject = pressable;
            isObjectInPress = true;
            PressingObject.transform.position = pressingPoint.position;
        }
    }
}
