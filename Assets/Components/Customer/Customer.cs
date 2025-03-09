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
    [SerializeField] private float timeBetweenScientistPopUps = 2f;

    private SpriteRenderer spriteRenderer;   

    [Header("Influence Points")]
    [SerializeField] private float scientistInfluencePoints = 25;
    [SerializeField] private float ordinaryNPCInfluencePoints = 10;

    public static Action<float> CustomerPotionReceived;

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

        SetCustomer(customerSelector.GetRandomScientist());
    }

    void SetCustomer(CustomerSO customer)
    {
        DialogueManager.Instance.StartDialogue(customer.DialogueObject);
        spriteRenderer.sprite = customer.CustomerSprite;
        transform.DOLocalMoveY(targetCustomerPosition.localPosition.y, popUpTime);
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (DialogueManager.Instance.WaitingForPotion)
        {
           
        // degisicek
        if (collision.TryGetComponent<Bottle>(out var bottle))
            {

            if (!bottle.GetComponent<Draggable>().isDragging)
                {
                    CustomerPotionReceived?.Invoke(bottle.SuccessRate);
                    Destroy(bottle.gameObject);
                }
            }


        }
    }
}
