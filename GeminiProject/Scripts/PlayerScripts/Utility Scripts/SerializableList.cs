// Author: Christopher Trimble
using System.Collections.Generic;

[System.Serializable]
public class SerializableList<T> : List<T>
{
    public List<T> stageInfo;
}
