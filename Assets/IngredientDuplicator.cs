using UnityEngine;

public class IngredientDuplicator : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Camera mainCam;
    private GameObject currentIngredient;
    private bool isDragging;

    private void OnMouseDown()
    {
        print("on mouse down");

        currentIngredient = Instantiate(prefab, transform.position, Quaternion.identity);
        isDragging = true;
    }

    private void Update()
    {
        if (isDragging)
        {
            currentIngredient.transform.position = mainCam.ScreenToWorldPoint(Input.mousePosition);
            currentIngredient.transform.position = new Vector3(currentIngredient.transform.position.x, currentIngredient.transform.position.y, 0);
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }
}
