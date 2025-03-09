using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PotionDeliverManager : MonoBehaviour
{
    [SerializeField] private Button deliverButton;
    [SerializeField] private Transform shopScene;
    [SerializeField] private Transform labScene;
    [SerializeField] private Transform positionInShop;
    [SerializeField] private GameObject bottlePrefab;

    [SerializeField] private float timeBetweenBottleReplacements = .5f;

    private GameObject currentBottle;

    private void Start()
    {
        deliverButton.gameObject.SetActive(false);
        InstantiateEmptyBottle();
        deliverButton.onClick.AddListener(DeliverPotion);
    }

    private async void DeliverPotion()
    {
        if(currentBottle == null)
            return;

        TransferFilledBottle();
        deliverButton.gameObject.SetActive(false);
        await Task.Delay(TimeSpan.FromSeconds(timeBetweenBottleReplacements));
        InstantiateEmptyBottle();
        
    }

    void TransferFilledBottle()
    {
        currentBottle.transform.SetParent(shopScene, true);
        currentBottle.transform.position = positionInShop.position;
        currentBottle.transform.position = new Vector3(currentBottle.transform.position.x, currentBottle.transform.position.y, positionInShop.localPosition.z);
    }

    void InstantiateEmptyBottle()
    {
        GameObject obj = Instantiate(bottlePrefab, labScene);
        obj.transform.position = positionInShop.position;
        obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, positionInShop.localPosition.z);
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
