using System.Collections.Generic;

[System.Serializable]
public class Dialogue
{
    public int id;
    public string speaker;
    public string text;
    public string condition;
    public bool hasOptions;
    public bool isTerminal;
    public bool hasEvent;
    public string eventString;
    public bool isRepeatable;
    public int nextValidId;
    public List<DialogueOption> options;
}
