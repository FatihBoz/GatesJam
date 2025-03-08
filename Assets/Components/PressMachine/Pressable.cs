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
        if (currentStage<sprites.Length)
        {
            spriteRenderer.sprite = sprites[currentStage];
            currentStage++;
        }
    }
    public bool IsOnLastStage()
    {
        return currentStage == sprites.Length - 1;
    }
}
