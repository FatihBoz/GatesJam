using UnityEngine;

public class Pressable : MonoBehaviour
{
    public Sprite[] sprites;
    private int currentStage = 0;
    private SpriteRenderer spriteRenderer;
    
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Press()
    {
        if (currentStage<sprites.Length-1)
        {
            currentStage++;
            spriteRenderer.sprite = sprites[currentStage];
        }
    }
    public bool IsOnLastStage()
    {
        return currentStage == sprites.Length - 1;
    }
}
