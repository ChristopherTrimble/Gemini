// Author: Lauren Davis
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using PlayerScripts.SaveLoadSystem;

public static class SaveSystem
{
    public static void SaveCharacter(PlayerData playerSave)
    {
        string saveData = JsonConvert.SerializeObject(playerSave);
        if (!Directory.Exists(Application.persistentDataPath + "/saves")) 
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        File.WriteAllText(Application.persistentDataPath + "/saves/" + playerSave.saveNumber + ".json", saveData);
    }
    
    public static T LoadCharacter<T>(string fileName)
    {
        return JsonConvert.DeserializeObject<T>(File.ReadAllText(Application.persistentDataPath + "/saves/" + fileName + ".json"));
    }
}
