using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Scoop : MonoBehaviour
{
    [SerializeField] private Transform initialScoopPosition;
    [SerializeField] private SpriteRenderer liquid;
    [SerializeField] private Camera mainCam;
    [SerializeField] private ParticleSystem pourEffect;
    [SerializeField] private float initialPosReturnTime = 2f;
    [SerializeField] private Animator anim;
    [SerializeField] private Vector3 pourOffset;

    private IFillable fillableObj;
    private bool isDragging;
    private bool isPouring = false;
    private bool isFilled = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("trigger giriþ");
        if (collision.TryGetComponent<ICauldron>(out var cauldron))
        {
            if (!isFilled && cauldron.DecreaseScoopCount())
            {
                liquid.gameObject.SetActive(true);
                liquid.color = cauldron.GetPotionColor();
                isFilled = true;
            }

        }

        if(collision.TryGetComponent<IFillable>(out var fillable))
        {
            fillableObj = fillable;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IFillable>(out var fillable))
        {
            fillableObj = null;
        }
    }


    private void OnMouseDown()
    {
        if (isPouring)
            return;

        isDragging = true;
    }

    private void OnMouseUp()
    {
        isDragging = false;

        if (fillableObj != null && !isPouring &&
            isFilled && !fillableObj.IsFilled)
        {
            anim.SetBool("isPouring", true);    
            isPouring = true;
            transform.position = fillableObj.FillPoint.position + pourOffset;
            pourEffect.Play();
            fillableObj.Fill(liquid.color,0.5f,2f);
            liquid.gameObject.SetActive(false);
            isFilled = false;
            StartCoroutine(PourCooldown());
        }
        else
        {
            ReturnInitialPosition();
        }
    }

    void ReturnInitialPosition()
    {

        transform.DOLocalMove(initialScoopPosition.localPosition, 1f);
    }


    private void Update()
    {
        if (isDragging)
        {
            Vector2 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
        }

    }




    private IEnumerator PourCooldown()
    {
        yield return new WaitForSeconds(pourEffect.main.duration);
        anim.SetBool("isPouring", false);   

        isPouring = false;  
        ReturnInitialPosition();
    }


}
