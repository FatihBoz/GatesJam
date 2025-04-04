using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] private List<ParticleSystem> particleList;

    private IFillable fillableObj;
    private bool isDragging;
    private bool isPouring = false;
    private bool isFilled = false;
    private float successRate;

    public static Action OnCauldronIsEmpty;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<ICauldron>(out var cauldron))
        {
            if (!isFilled && cauldron.DecreaseScoopCount())
            {
                successRate = TableUI.accuracy;
                liquid.gameObject.SetActive(true);
                liquid.color = cauldron.GetPotionColor();
                ChangeParticleSystemColors();
                isFilled = true;

                if (cauldron.ScoopCount <= 1)
                {
                    print("scoop count 0");
                    OnCauldronIsEmpty?.Invoke();
                }
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

    void ChangeParticleSystemColors()
    {
        foreach (var particle in particleList)
        {
            var main = particle.main;
            main.startColor = liquid.color;
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
            fillableObj.Fill(liquid.color,0.5f,2f, successRate);
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
