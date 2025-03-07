using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/DialogueObject")]
public class DialogueObject : ScriptableObject
{
    // can be added npc name, portrait, etc.

    [TextAreaAttribute]
    public string dialogue;
}
