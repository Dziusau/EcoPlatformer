using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public static class SaveSystem
{
    public static string path = Application.persistentDataPath + "players.data";
     public static void SavePlayer(string _level, int _score, float _time)
    {
        List<PlayerData> playerDataList = LoadPlayersData();
        PlayerData updatedData = new(_level, _score, _time);

        if (playerDataList != null)
        {
            // Find the index of the existing data for the same level, if it exists
            int existingIndex = playerDataList.FindIndex(data => data.levelName == _level);

            if (existingIndex != -1)
            {
                // Replace the existing data with the updated data
                playerDataList[existingIndex] = updatedData;
            }
            else
            {
                // Add the new data to the list
                playerDataList.Add(updatedData);
            }
        }
        else
        {
            // Create a new list with the updated data if no existing data is found
            playerDataList = new List<PlayerData>() { updatedData };
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, playerDataList);
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
