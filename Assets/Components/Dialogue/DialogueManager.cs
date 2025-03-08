using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum CustomerStates
{
    CustomerArrived,
    WaitingForPotion,
    ReceivedPotion,
    CustomerResponseToPotion,
    CustomerSentAway,
    CustomerFullyGone,
    NextCustomerWaiting
}


public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }
    public float textSpeed = 0.1f;

    public GameObject potionScreen;
    public Button nayButton;

    public GameObject alchemistPanelGo;
    public GameObject customerPanelGo;

    public TMP_Text alchemistDialogueText;
    public TMP_Text customerNameText;
    public TMP_Text customerDialogueText;

    public DialogueObject exObject;

    private DialogueObject currentDialogue;

    private bool isTyping;
    private Coroutine typingCoroutine;
    private CustomerStates currentState;

    public static Action CustomerGoneEvent;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
    //   StartDialogue(exObject);
    }

    public void StartDialogue(DialogueObject dialogueObject)
    {
        alchemistPanelGo.SetActive(false);
        customerPanelGo.SetActive(true);
        customerNameText.text = dialogueObject.customerName + ":";
        currentState = CustomerStates.CustomerArrived;
        currentDialogue = dialogueObject;
        potionScreen.SetActive(false);

        SetTypingCoroutine(currentDialogue.startConv, customerDialogueText);
    }
    public void SetTypingCoroutine(string text, TMP_Text target)
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeText(text, target));
    }

    private IEnumerator TypeText(string text, TMP_Text target)
    {
        isTyping = true;
        target.text = "";

        foreach (char c in text)
        {
            target.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;
    }

    private void Update()
    {
        if (currentDialogue == null)
        {
            return;
        }
        switch (currentState)
        {
            case CustomerStates.CustomerArrived:
                if (!isTyping)
                {
                    currentState = CustomerStates.WaitingForPotion;
                    // enable potion screen
                    potionScreen.SetActive(true);
                    nayButton.onClick.AddListener(NayButtonEvent);
                }
                break;
            case CustomerStates.WaitingForPotion:
             //   Debug.Log("waiting potion");
                // if receveid potion, if answer no go to customer sent away state
                if (Input.GetKeyDown(KeyCode.Space)) // gived potion
                {
                    // return if potion is bad or goods

                    currentState = CustomerStates.ReceivedPotion;

                    customerPanelGo.SetActive(false);
                    alchemistPanelGo.SetActive(true);
                    potionScreen.SetActive(false);
                    SetTypingCoroutine(currentDialogue.alchmResponse, alchemistDialogueText);
                }
                break;
            case CustomerStates.ReceivedPotion:
              //  Debug.Log("received potion");
                if (!isTyping && Input.GetMouseButtonDown(0))
                {
                    currentState = CustomerStates.CustomerFullyGone;
                    customerPanelGo.SetActive(true);
                    alchemistPanelGo.SetActive(false);
                    potionScreen.SetActive(false);
                    SetTypingCoroutine(currentDialogue.goodAnswer, customerDialogueText); // good or bad answer will change

                }
                // check potion bad or good. answer according to that.
                break;
            case CustomerStates.CustomerResponseToPotion:
               // Debug.Log("customer response to potion");
                // if answer no in waiting for potion state, go to customer sent away state
                if (!isTyping && Input.GetMouseButtonDown(0))
                {
                    currentState = CustomerStates.CustomerFullyGone;
                    customerPanelGo.SetActive(true);
                    alchemistPanelGo.SetActive(false);
                    potionScreen.SetActive(false);
                    SetTypingCoroutine(currentDialogue.badAnswer, customerDialogueText);
                    // TODO: good or bad answer will change

                }
                break;
            case CustomerStates.CustomerSentAway:
                //Debug.Log("customer sent away");
                // if answer no in waiting for potion state, go to customer sent away state
                if (!isTyping && Input.GetMouseButtonDown(0))
                {
                    currentState = CustomerStates.NextCustomerWaiting;
                    CustomerGoneEvent?.Invoke();
                    customerPanelGo.SetActive(false);
                    alchemistPanelGo.SetActive(false);
                    potionScreen.SetActive(false);

                }
                break;
            case CustomerStates.CustomerFullyGone:
                if (!isTyping && Input.GetMouseButtonDown(0))
                {
                    customerPanelGo.SetActive(false);
                    alchemistPanelGo.SetActive(false);
                    potionScreen.SetActive(false);

                    currentState = CustomerStates.NextCustomerWaiting;
                    CustomerGoneEvent?.Invoke();
                }
                break;
        }

    }
    public void NayButtonEvent()
    {
        currentState = CustomerStates.CustomerSentAway;

        customerPanelGo.SetActive(true);
        alchemistPanelGo.SetActive(false);

        SetTypingCoroutine(currentDialogue.badAnswer, customerDialogueText);

        nayButton.onClick.RemoveListener(NayButtonEvent);
        potionScreen.SetActive(false);
    }




}
