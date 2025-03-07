using UnityEngine;

public class ConstrainCircle : MonoBehaviour
{
    public float radius = 5f;  // Büyük cismin yarýçapý, küçük cismin sýnýrý
    private Transform parentTransform;  // Büyük cismin Transform'u

    void Start()
    {
        // Küçük cismin parent'ýný al
        parentTransform = transform.parent;
    }

    void Update()
    {
        // Küçük cismin pozisyonu ve büyük cismin (parent) merkezine olan mesafesi
        Vector3 directionToParent = transform.position - parentTransform.position;
        float distance = directionToParent.magnitude;

        // Eðer küçük cisim büyük cismin sýnýrýný aþarsa
        if (distance > radius)
        {
            // Küçük cismi, büyük cismin sýnýrýna yerleþtir
            Vector3 newPosition = parentTransform.position + directionToParent.normalized * radius;
            transform.position = newPosition;
        }
    }
}
