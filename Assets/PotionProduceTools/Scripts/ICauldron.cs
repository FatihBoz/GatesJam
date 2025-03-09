using UnityEngine;

public interface ICauldron
{
    public Color GetPotionColor();

    public bool DecreaseScoopCount();

    public int ScoopCount { get; }  
}
