using DG.Tweening;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] private CustomerSelectorSO customerSelector;
    [SerializeField] private Transform initialCustomerPosition;
    [SerializeField] private Transform targetCustomerPosition;
    [SerializeField] private float popUpTime = .5f;  

    private SpriteRenderer spriteRenderer;

    [Header("Influence Points")]
    [SerializeField] private float scientistInfluencePoints = 25;
    [SerializeField] private float ordinaryNPCInfluencePoints = 10;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void Start()
    {
        SetCustomer(customerSelector.GetRandomScientist()); //Random scientist
        //DialogueManager.Instance.StartDialogue(customerSO.DialogueObject);
    }

    void SetCustomer(CustomerSO customer)
    {
        spriteRenderer.sprite = customer.CustomerSprite;
        transform.DOLocalMoveY(targetCustomerPosition.localPosition.y, popUpTime).SetEase(Ease.OutBounce);
    }


    private void OnEnable()
    {
        //MÜÞTERÝ GÝTTÝ EVENT ABONELÝÐÝ
    }

    private void OnDisable()
    {
        //MÜÞTERÝ GÝTTÝ EVENT ABONELÝÐÝ KALDIR
    }
}
