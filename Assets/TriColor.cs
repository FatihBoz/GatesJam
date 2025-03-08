using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class TriColor
{
    public int r;
    public int g;
    public int b;

    private int counter = 0;

    public TriColor(int r, int g, int b)
    {
        this.r = r;
        this.g = g;
        this.b = b;
    }

    public void AddColor(Color32 color)
    {
        r += color.r;
        g += color.g;
        b += color.b;
        ++counter;

        Debug.Log("r:" + r + " g: " + g + " b: " + b + " counter: " + counter);
    }

    public void ResetColor()
    {
        r = 0;
        g = 0;
        b = 0;
        counter = 0;
    }   

    public Color32 GetAverageColor()
    {
        Color32 c = new Color32((byte)(r / counter), (byte)(g / counter), (byte)(b / counter), 255);
        Debug.Log(c.ToString());
        return c;
    }
}




