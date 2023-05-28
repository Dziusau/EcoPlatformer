using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public static class SaveSystem
{
    public static string path = Application.persistentDataPath + "players.data";
     public static void SavePlayer(string _level, int _score, float _time)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);

        PlayerData playerData = new PlayerData(_level, _score, _time);

        formatter.Serialize(stream, playerData);
        stream.Close();
    }  

    public static List<PlayerData> LoadPlayersData()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            List<PlayerData> data = formatter.Deserialize(stream) as List<PlayerData>;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found: " + path);
            return null;
        }
    } 
}
