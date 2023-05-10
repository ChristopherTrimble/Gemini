using System.Collections.Generic;

[System.Serializable]
public class DialogueOption
{
    public string text;
    public List<bool> conditions;
    public int nextId;
    public bool isValid = true;
}
