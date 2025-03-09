using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMove : MonoBehaviour
{
    public List<RectTransform> patrolLocs = new List<RectTransform>();
    public float speed = 100f; // Birim/saniye cinsinden sabit hız
    public Ease easeType = Ease.Linear;

    private RectTransform rectTransform;
    private int currentTargetIndex = 0;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        if (patrolLocs.Count > 0)
        {
            MoveToNextPoint();
        }
        else
        {
            Debug.LogError("No patrol points assigned!");
        }
    }

    void MoveToNextPoint()
    {
        RectTransform target = patrolLocs[currentTargetIndex].GetComponent<RectTransform>();
        Vector2 currentPos = rectTransform.anchoredPosition;
        Vector2 targetPos = target.anchoredPosition;

        // Mesafe hesapla
        float distance = Vector2.Distance(currentPos, targetPos);

        // Süreyi mesafe/hız oranından belirle
        float dynamicDuration = distance / speed;

        rectTransform.DOAnchorPos(targetPos, dynamicDuration)
            .SetEase(easeType)
            .OnComplete(() =>
            {
                currentTargetIndex = (currentTargetIndex + 1) % patrolLocs.Count;
                MoveToNextPoint();
            });
    }
}