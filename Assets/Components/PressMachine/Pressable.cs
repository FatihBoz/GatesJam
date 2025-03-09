using UnityEngine;

public class Pressable : MonoBehaviour
{
    //public Sprite[] sprites;
    //private int currentStage = 0;
    private SpriteRenderer spriteRenderer;
    
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Press()
    {
        Color color = GetComponent<InventoryItem>().ingredient.color;

        spriteRenderer.sprite = InventorySystem.instance.spriteOfPressedItem;
        spriteRenderer.color = color;   

        ChangePropertiesAfterPress();
        /*
        if (currentStage<sprites.Length)
        {
            spriteRenderer.sprite = sprites[currentStage];
            currentStage++;
        }
        */
    }

    /*
    public bool IsOnLastStage()
    {
        return currentStage == sprites.Length - 1;
    }
    */

    void ChangePropertiesAfterPress()
    {


        if( Mathf.Abs(GetComponent<InventoryItem>().ingredient.Healthy) > 0.5 )
        {
            GetComponent<Sliceable>().percentage += 1f;
        }
        if (Mathf.Abs(GetComponent<InventoryItem>().ingredient.Acidity) > 0.5)
        {
            GetComponent<Sliceable>().percentage += 1f;
        }
        if (Mathf.Abs(GetComponent<InventoryItem>().ingredient.Logically) > 0.5)
        {
            GetComponent<Sliceable>().percentage += 1f;
        }
        if (Mathf.Abs(GetComponent<InventoryItem>().ingredient.Sweetness) > 0.5)
        {
            GetComponent<Sliceable>().percentage += 1f;
        }

    }
}
