[System.Serializable]
public class QuestItem
{
    public string name;
    public int amount;

    public QuestItem(string name, int amount)
    {
        this.name = name;
        this.amount = amount;
    }
}
