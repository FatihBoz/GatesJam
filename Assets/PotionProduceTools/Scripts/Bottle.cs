using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading.Tasks;

public class Bottle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IFillable
{
    [SerializeField] private Image liquid;
    [SerializeField] private Transform fillPoint;
    [SerializeField] private Animator animator;

    private bool isFilled = false;
    private bool isHovering;
    public bool IsHoveringOnBottle => isHovering;
    public Transform FillPoint => fillPoint;

    public bool IsFilled => isFilled;

    public async void Fill(Color color, float delay, float fillTime)
    {
        animator.SetBool("StopperOut", true);
        await Task.Delay((int)(delay * 1000));

        liquid.DOFillAmount(1, fillTime).OnComplete(() => animator.SetBool("StopperOut",false));
        isFilled = true;
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


