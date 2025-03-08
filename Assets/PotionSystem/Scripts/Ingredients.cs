using UnityEngine;

[CreateAssetMenu(fileName = "Ingredients", menuName = "Scriptable Objects/Ingredients")]
public class Ingredients : ScriptableObject
{
    public string ingrName;

    public GameObject ingrPrefab;

    [Range(-1f, 1f)]
    public float Logically;
    [Range(-1f, 1f)]
    public float Healthy;
    [Range(-1f, 1f)]
    public float Sweetness;
    [Range(-1f, 1f)]
    public float Acidity;

}
