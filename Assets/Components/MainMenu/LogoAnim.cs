using UnityEngine;
using DG.Tweening;

public class LogoAnim : MonoBehaviour
{
    public float startY = 1000f; // Başlangıç yüksekliği
    public float endY = 0f; // Bitiş noktası
    public float duration = 1f; // Animasyon süresi
    public Ease easeType = Ease.OutBounce; // Animasyon eğrisi
    public bool doOnStart = true;

    void Start()
    {
        if (doOnStart)
        {
            AnimStartt();
        }
    }
    public void AnimStartt()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, startY);

        rectTransform.DOAnchorPosY(endY, duration).SetEase(easeType);
    }
    public void AnimReverse()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.DOAnchorPosY(startY, duration).SetEase(easeType).OnComplete(()=>gameObject.SetActive(false));
    }
}