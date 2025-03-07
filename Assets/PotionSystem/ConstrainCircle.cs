using UnityEngine;

public class ConstrainCircle : MonoBehaviour
{
    public float radius = 5f;  // B�y�k cismin yar��ap�, k���k cismin s�n�r�
    private Transform parentTransform;  // B�y�k cismin Transform'u

    void Start()
    {
        // K���k cismin parent'�n� al
        parentTransform = transform.parent;
    }

    void Update()
    {
        // K���k cismin pozisyonu ve b�y�k cismin (parent) merkezine olan mesafesi
        Vector3 directionToParent = transform.position - parentTransform.position;
        float distance = directionToParent.magnitude;

        // E�er k���k cisim b�y�k cismin s�n�r�n� a�arsa
        if (distance > radius)
        {
            // K���k cismi, b�y�k cismin s�n�r�na yerle�tir
            Vector3 newPosition = parentTransform.position + directionToParent.normalized * radius;
            transform.position = newPosition;
        }
    }
}
