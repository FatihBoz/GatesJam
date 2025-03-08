using UnityEngine;

public interface IFillable
{
    public bool IsHoveringOnBottle { get; }

    public bool IsFilled { get; }

    public Transform FillPoint { get; }

    public void Fill(Color color, float delay, float fillTime, float successRate);
}
