using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private GameObject shopScene;
    [SerializeField] private GameObject labScene;

    bool isInLab = false;

    public void OnSceneChangeButtonPressed()
    {
        shopScene.SetActive(isInLab);
        labScene.SetActive(!isInLab);
        isInLab = !isInLab;
    }

}
