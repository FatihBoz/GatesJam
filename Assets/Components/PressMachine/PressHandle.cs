using DG.Tweening;
using UnityEngine;

public class PressHandle : MonoBehaviour
{
    public PressMachine pressMachine;

    private Vector3 defaultRotation;
    private bool rotated = false;
    private float timer;
    private float comeTimer = 2f;


    private void Start()
    {
        comeTimer = 2f;
    }

    private void Update()
    {
        if (rotated && Time.time >= timer + comeTimer)
        {
            transform.parent.DORotate(defaultRotation, 0.5f);
            rotated = false;
        }
    }

    private void OnMouseDown()
    {
        if (!rotated)
        {
            defaultRotation = transform.parent.localEulerAngles;
            transform.parent.DORotate(new Vector3(0, 0, -75), 0.5f).OnComplete(()=>
            {
                pressMachine.Press();
            });
            timer = Time.time;
            rotated = true;
        }
    }
}
