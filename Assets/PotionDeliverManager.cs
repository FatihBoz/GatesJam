using System;
using UnityEngine;
using UnityEngine.UI;

public class PotionDeliverManager : MonoBehaviour
{
    [SerializeField] private Button deliverButton;
    [SerializeField] private Transform shopScene;
    [SerializeField] private Transform positionInShop;

    private GameObject currentBottle;

    private void Start()
    {
        deliverButton.onClick.AddListener(DeliverPotion);
    }

    private void DeliverPotion()
    {
        if(currentBottle == null)
            return;

        currentBottle.transform.SetParent(shopScene, true);
        currentBottle.transform.position = positionInShop.position;
        currentBottle.transform.position = new Vector3(currentBottle.transform.position.x, currentBottle.transform.position.y, positionInShop.localPosition.z);
    }

    private void OnEnable()
    {
        Bottle.OnBottleFilled += OnBottleFilled;
    }

    private void OnBottleFilled(Bottle bottle)
    {
        deliverButton.gameObject.SetActive(true);
        currentBottle = bottle.gameObject;
    }

    private void OnDisable()
    {
        Bottle.OnBottleFilled -= OnBottleFilled;
    }
}
