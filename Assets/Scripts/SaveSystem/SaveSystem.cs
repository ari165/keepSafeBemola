using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveStats(int coins, int highScore)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        
        string path = Application.persistentDataPath + "/Stats.ari";
        FileStream stream = new FileStream(path, FileMode.Create);

        StatsData data = new StatsData(coins, highScore);
        
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static StatsData LoadStats()
    {
        string path = Application.persistentDataPath + "/Stats.ari";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            StatsData data = formatter.Deserialize(stream) as StatsData;
            stream.Close();
            
            return data;
        } 
        else
        {
            return null;
        }
    }
    

    public static void SaveOwned(int[] playerIDs, int currentPlayer, Color32 color)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        
        string path = Application.persistentDataPath + "/Owned.ari";
        FileStream stream = new FileStream(path, FileMode.Create);

        OwnedData data = new OwnedData(playerIDs, currentPlayer, color);
        
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static OwnedData LoadOwned()
    {
        string path = Application.persistentDataPath + "/Owned.ari";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            OwnedData data = formatter.Deserialize(stream) as OwnedData;
            stream.Close();
            
            return data;
        } 
        else
        {
            return null;
        }
    }
}
