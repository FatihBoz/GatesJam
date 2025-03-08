using UnityEngine;

public class Cauldron : MonoBehaviour, ICauldron
{
    [SerializeField] private int maxScoopCountToFill = 3;

    private int currentScoopCount;

    private void Start()
    {
        currentScoopCount = maxScoopCountToFill;
    }

    public bool DecreaseScoopCount()
    {
        if(currentScoopCount > 0)
        {
            currentScoopCount--;
            return true;
        }
        return false;
    }

    public Color GetPotionColor()
    {
        return Color.magenta;
    }
}
