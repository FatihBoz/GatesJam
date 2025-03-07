using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{

    public DialogueObject dialogueObject;
    public TMP_Text dialogueText;
    public float charactersPerSecond = 30f;

    private string currentMessage;
    private Coroutine typingCoroutine;
    private bool isTyping = false;

    private void Start()
    {
        StartDialogue(dialogueObject.dialogue);
    }

    void Update()
    {
        // Skip typing animation when player presses Confirm input (Space/Return)
        //if (isTyping && Input.GetButtonDown("Submit"))
        //{
        //    CompleteDialogue();
        //}
    }

    public void StartDialogue(string message)
    {
        currentMessage = message;

        // Stop existing typing if any
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypeText());
    }
    IEnumerator TypeText()
    {
        isTyping = true;
        dialogueText.text = "";

        float delay = 1 / charactersPerSecond;

        foreach (char letter in currentMessage.ToCharArray())
        {
            dialogueText.text += letter;

            // Wait for next character
            yield return new WaitForSecondsRealtime(delay);
        }

        isTyping = false;
    }

    public void SetTextSpeed(float newSpeed)
    {
        charactersPerSecond = newSpeed;
    }


}
