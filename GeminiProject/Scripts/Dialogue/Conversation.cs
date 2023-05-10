using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class Conversation
{
    public List<Dialogue> dialogues;

    public Dialogue GetDialogue(int id)
    {
        return dialogues.FirstOrDefault(dialogue => dialogue.id == id);
    }
}
