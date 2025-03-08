using UnityEngine;
using UnityEngine.UI;

public class InfluenceBar : MonoBehaviour
{
    public Slider influenceSlider; // UI'daki Slider
    public static float influence; // De�i�en etki de�eri
    public float maxInfluence = 100f; // Maksimum de�er

    void Start()
    {

        influenceSlider.maxValue = maxInfluence;
        influenceSlider.value = Mathf.Clamp(influence, 0, maxInfluence);
    }

    void Update()
    {
        influence = Mathf.Clamp(influence, 0, maxInfluence);

        // Slider'� g�ncelle
        influenceSlider.value = influence;
    }
    public static void increaseInfluence()
    {
        influence += 20;

    }

    public static void decreaseInfluence()
    {
        influence -= 20;
    }

}
