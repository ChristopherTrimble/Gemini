using System.IO;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveCharacter(PlayerSaveInfo playerSave)
    {
        string saveData = JsonUtility.ToJson(playerSave);
        if (!Directory.Exists(Application.persistentDataPath + "/saves")) 
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        File.WriteAllText(Application.persistentDataPath + "/saves/" + playerSave.saveNumber + ".json", saveData);
    }

    public static T LoadCharacter<T>(string fileName)
    {
        return JsonUtility.FromJson<T>(File.ReadAllText(Application.persistentDataPath + "/saves/" + fileName + ".json"));
    }
}
