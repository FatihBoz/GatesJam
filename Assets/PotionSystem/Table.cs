using UnityEngine;

public class Table : MonoBehaviour
{

    Vector2 LogicallyVec;
    Vector2 HealthyVec;
    Vector2 SweatnessVec;
    Vector2 AcidityVec;

    Vector2 LVec;
    Vector2 HVec;
    Vector2 SVec;
    Vector2 AVec;

    Vector2 ResultVec;

    float offset = 3f;

    public Ingredients Ingredients;

    float sqrt2;
    private void Awake()
    {
        sqrt2 = Mathf.Sqrt(2);

        LogicallyVec = new Vector2(sqrt2, sqrt2).normalized;
        HealthyVec = new Vector2(1.0f, 0f).normalized;
        SweatnessVec = new Vector2(0f, 1f).normalized;
        AcidityVec = new Vector2(-sqrt2, sqrt2).normalized;
    }


    private void Start()
    {
        IngredientsToTable(Ingredients);
    }



    public void IngredientsToTable(Ingredients ingr)
    {
        LVec = ingr.Logically * LogicallyVec;
        HVec = ingr.Healthy * HealthyVec;
        SVec = ingr.Sweetness * SweatnessVec;
        AVec = ingr.Acidity * AcidityVec;

        Debug.Log(ingr.Sweetness);

        ingr.ToString();

        ResultVec = LVec + HVec + SVec + AVec;


        transform.position += new Vector3(ResultVec.x, ResultVec.y, transform.position.z) * offset;



    }

    void OnDrawGizmos()
    {
        // Baþlangýç noktasý ile vektörün bitiþ noktasý arasýna çizgi çizin
        Gizmos.color = Color.green; // Çizginin rengini ayarla
        Gizmos.DrawLine(Vector2.zero, Vector2.zero + ResultVec); // Baþlangýç noktasýndan yön vektörüne kadar çizgi çiz
    }
}
