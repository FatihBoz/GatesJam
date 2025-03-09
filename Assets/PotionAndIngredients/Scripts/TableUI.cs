using TMPro;
using UnityEngine;

public class TableUI : MonoBehaviour
{
    public RectTransform pointer; // UI Elemanı (Image gibi) - Vektör sonucu bu UI elementini kaydıracak
    private RectTransform outerCircle;
    public RectTransform target;

    public float offset;

    float pointerRadius;
    float maxRadius; // UI içindeki dairenin sınırı

    public Ingredients Ingredients;

    float areaOfPointers;

    Vector2 LogicallyVec;
    Vector2 HealthyVec;
    Vector2 SweetnessVec;
    Vector2 AcidityVec;

    Vector2 LVec;
    Vector2 HVec;
    Vector2 SVec;
    Vector2 AVec;

    Vector2 ResultVec;
    float sqrt2;

    public TextMeshProUGUI percentageOut;

    public static float accuracy;

    private Vector2 pointerInitialPos;

    int scale = 5;

    private void Awake()
    {
        sqrt2 = Mathf.Sqrt(2);

        LogicallyVec = new Vector2(sqrt2, sqrt2).normalized;
        HealthyVec = new Vector2(1.0f, 0f).normalized;
        SweetnessVec = new Vector2(0f, 1f).normalized;
        AcidityVec = new Vector2(-sqrt2, sqrt2).normalized;
    }

    private void Start()
    {
        pointerInitialPos = pointer.anchoredPosition;

        outerCircle = GetComponent<RectTransform>();

        IngredientsToTable(Ingredients);

        if (outerCircle != null)
        {
            maxRadius = outerCircle.rect.width / 2f;
        }

        if(pointer != null)
        {
            pointerRadius = pointer.rect.width / 2f;
        }

        areaOfPointers = GetCircleArea(pointerRadius);

    }

    public void PlaceTargetRandomly()
    {
        //outerCircle = outerCircle.position;
        //outerCircle.GetComponent<RectTransform>().rect.width = radius;


        float angle = Random.Range(0f, 2f * Mathf.PI);

        float distance = Random.Range(0f, maxRadius);

        float x = distance * Mathf.Cos(angle);
        float y = distance * Mathf.Sin(angle);


        target.localPosition = new Vector3(0 + x, 0 + y);
    }

    private void Update()
    {
        accuracy = CalculateAccuracy();

        percentageOut.text = accuracy.ToString("F0") + "%";

        ConstrainCircle();

    }

    public void IngredientsToTable(Ingredients ingr)
    {
        IngredientsToTable(ingr,1f);
    }
    public void IngredientsToTable(Ingredients ingr,float percentage)
    {
        LVec = ingr.Logically * LogicallyVec * percentage;
        HVec = ingr.Healthy * HealthyVec * percentage;
        SVec = ingr.Sweetness * SweetnessVec * percentage;
        AVec = ingr.Acidity * AcidityVec * percentage;

        ResultVec = (LVec + HVec + SVec + AVec) * offset;

        // Eğer vektör maksimum yarıçapı aşarsa, normalize et ve sınırda tut
        if (ResultVec.magnitude > 1)
        {
            ResultVec.Normalize(); // Birim vektöre çevir
        }

        // UI'deki pointer'ı güncelle
        if (pointer != null)
        {
            pointer.anchoredPosition += ResultVec * maxRadius;
        }
    }


    void ConstrainCircle()
    {
        // Outer circle'ın dünya pozisyonunu al
        Vector2 center = outerCircle.position;

        // Outer circle'ın dünya boyutlarını almak için RectTransform'un boyutlarını çevir
        Vector2 outerCircleSize = outerCircle.rect.size;
        Vector2 worldOuterCircleSize = outerCircle.TransformVector(outerCircleSize); // Dünya boyutlarına dönüştür

        // Pointer'ın dünya pozisyonunu al
        Vector2 pointerPosition = pointer.position;

        // Outer circle ile pointer arasındaki mesafeyi hesapla
        Vector2 directionToParent = pointerPosition - center;
        float distance = directionToParent.magnitude;

        // Eğer pointer outer circle'ın sınırını aşarsa
        if (distance > worldOuterCircleSize.x / 2) // X veya Y'yi kullanabilirsin, hangisi büyüyorsa
        {
            // Direction'ı normalize et ve outer circle'ın sınırına yerleştir
            Vector2 newPosition = center + directionToParent.normalized * (worldOuterCircleSize.x / 2); // Yarıçapı kullan

            // Pointer'ın yeni pozisyonunu set et
            pointer.position = newPosition;
        }
    }

    float CalculateLocalDistance(RectTransform rect1, RectTransform rect2)
    {
        // RectTransform'ların yerel pozisyonlarını al
        Vector2 rect1LocalPosition = rect1.localPosition;
        Vector2 rect2LocalPosition = rect2.localPosition;

        // Yerel pozisyonlar arasındaki mesafeyi hesapla
        float distance = Vector2.Distance(rect1LocalPosition, rect2LocalPosition);
        return distance;
    }

    public float CalculateAccuracy()
    {
        return Mathf.Max(0, 100 - (CalculateLocalDistance(pointer, target) / pointerRadius) * 100);
    }
    public static float GetCircleArea(float r)
    {
        return (float)(Mathf.PI * r * r); // Alan formülü: π * r^2
    }

    public void ResetTableUI()
    {
        pointer.anchoredPosition = pointerInitialPos;
        PlaceTargetRandomly();
    }

    private void OnEnable()
    {
        Scoop.OnCauldronIsEmpty += ResetTableUI;
    }

    private void OnDisable()
    {
        Scoop.OnCauldronIsEmpty -= ResetTableUI;
    }

}
