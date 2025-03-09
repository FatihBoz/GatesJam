using System;
using UnityEngine;

public class SuccessRateUI : MonoBehaviour
{
    [SerializeField] private Color l;
}

[Serializable]
public class SuccessRate
{
    public float successRateThreshold;
    public Color color;
}
