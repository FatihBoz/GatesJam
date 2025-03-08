using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private Canvas labCanvas;

    [SerializeField] private int labCanvasSortingOrderMin;
    [SerializeField] private int labCanvasSortingOrderMax;
    bool isInLab = false;

    public void OnSceneChangeButtonPressed()
    {
        if (isInLab)
        {
            labCanvas.sortingOrder = labCanvasSortingOrderMin;
        }
        else
        {
            labCanvas.sortingOrder = labCanvasSortingOrderMax;
        }
        isInLab = !isInLab;
    }

}
