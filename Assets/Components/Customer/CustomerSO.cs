using UnityEngine;

[CreateAssetMenu(fileName = "New Customer", menuName = "Scriptable Objects/Customer")]
public class CustomerSO : ScriptableObject
{
    [SerializeField] private string customerName;
    [SerializeField] private Sprite customerSprite;
    [SerializeField] private bool hasNewspaper;

    [TextArea]
    [SerializeField] private string newspaperText;

    [SerializeField] private DialogueObject dialogueObject;

    public string CustomerName { get => customerName;}
    public Sprite CustomerSprite { get => customerSprite;}
    public string NewspaperText { get => newspaperText; }
    public DialogueObject DialogueObject { get => dialogueObject; }
}
