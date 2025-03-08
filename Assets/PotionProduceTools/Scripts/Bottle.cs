using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading.Tasks;
using System;

public class Bottle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IFillable
{
    [SerializeField] private Image liquid;
    [SerializeField] private Transform fillPoint;
    [SerializeField] private Animator animator;

    private bool isFilled = false;
    private bool isHovering;
    private float successRate;

    public static Action<Bottle> OnBottleFilled;
    public bool IsHoveringOnBottle => isHovering;
    public Transform FillPoint => fillPoint;

    public bool IsFilled => isFilled;

    public float SuccessRate => successRate;

    public async void Fill(Color color, float delay, float fillTime, float successRate)
    {
        this.successRate = successRate;
        print(this.successRate);
        animator.SetBool("StopperOut", true);
        liquid.color = color;
        await Task.Delay((int)(delay * 1000));

        liquid.DOFillAmount(1, fillTime).OnComplete(OnFillCompleted);
        isFilled = true;
    }

    void OnFillCompleted()
    {
        animator.SetBool("StopperOut", false);
        OnBottleFilled?.Invoke(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        Debug.Log("Pointer Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        Debug.Log("Pointer Exit");
    }

}


