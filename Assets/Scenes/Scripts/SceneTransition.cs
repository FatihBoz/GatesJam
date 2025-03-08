using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private GameObject labScene;
    [SerializeField] private GameObject shopScene;

    bool isInLab = false;

    public void OnSceneChangeButtonPressed()
    {
        DialogueManager.Instance.SceneTransition();
        shopScene.SetActive(isInLab);
        labScene.SetActive(!isInLab);
        isInLab = !isInLab;
    }

    public void FinishLevel()
    {
        if (TableUI.accuracy > 70f)
        {
            InfluenceBar.increaseInfluence();

        }
        else
        {
            InfluenceBar.decreaseInfluence();
        }
    }

    public void StartLevel()
    {

    }

}
