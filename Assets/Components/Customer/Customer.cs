using DG.Tweening;
using System;
using System.Threading.Tasks;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] private CustomerSelectorSO customerSelector;
    [SerializeField] private Transform initialCustomerPosition;
    [SerializeField] private Transform targetCustomerPosition;


    [Header("Scientist")]
    [SerializeField] private float popUpTime = .5f;
    [SerializeField] private int scientistCounterLimit = 2;
    [SerializeField] private float timeBetweenScientistPopUps = 2f;

    private SpriteRenderer spriteRenderer;
    private int scientistCounter = 0;
    

    [Header("Influence Points")]
    [SerializeField] private float scientistInfluencePoints = 25;
    [SerializeField] private float ordinaryNPCInfluencePoints = 10;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void Start()
    {
        SelectCustomer(timeBetweenScientistPopUps / 2);
    }


    async void SelectCustomer(float time)
    {
        await Task.Delay(TimeSpan.FromSeconds(time));

        if (scientistCounter >= scientistCounterLimit)
        {
            SetCustomer(customerSelector.GetRandomOrdinaryNPC());
            scientistCounter = 0;
            return;
        }

        SetCustomer(customerSelector.GetRandomScientist());
    }

    void SetCustomer(CustomerSO customer)
    {
        DialogueManager.Instance.StartDialogue(customer.DialogueObject);
        spriteRenderer.sprite = customer.CustomerSprite;
        transform.DOLocalMoveY(targetCustomerPosition.localPosition.y, popUpTime);
        ++scientistCounter;
    }



    private void Customer_CustomerGone()
    {
        transform.DOLocalMoveY(initialCustomerPosition.localPosition.y, popUpTime);
        SelectCustomer(timeBetweenScientistPopUps);
    }


    private void OnEnable()
    {
        DialogueManager.CustomerGoneEvent += Customer_CustomerGone;
        //MÜÞTERÝ GÝTTÝ EVENT ABONELÝÐÝ
    }

    private void OnDisable()
    {
        DialogueManager.CustomerGoneEvent -= Customer_CustomerGone;
        //MÜÞTERÝ GÝTTÝ EVENT ABONELÝÐÝ KALDIR
    }

}
