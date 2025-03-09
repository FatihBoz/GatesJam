using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/DialogueObject")]
public class DialogueObject : ScriptableObject
{
    public string customerName;
    [TextArea(3, 10)]
    public string startConv;
    [TextArea(3, 10)]
    public string alchmResponse;
    [TextArea(3, 10)]
    public string goodAnswer;
    [TextArea(3, 10)]
    public string badAnswer;
    public bool isMale = true;
}

