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

}
