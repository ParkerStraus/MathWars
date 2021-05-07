using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveSystem
{
    public static void SaveData (int Level, int Score, int[] items)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string FilePath = Application.persistentDataPath + "/data.dic";
        FileStream stream = new FileStream(FilePath, FileMode.Create);

        GameData data = new GameData(Level, Score, items);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameData LoadData()
    {
        string FilePath = Application.persistentDataPath + "/data.dic";
        if (File.Exists(FilePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(FilePath, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();

            return data;

        }
        else
        {
            return null;
        }
    }

    public static void SaveLB(List<Entries> LeaderBoardData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string FilePath = Application.persistentDataPath + "/leaderboard.dic";
        FileStream stream = new FileStream(FilePath, FileMode.Create);
        Leaderboard Data = new Leaderboard(LeaderBoardData); 
        formatter.Serialize(stream, Data);
        stream.Close();
    }

    public static Leaderboard LoadLB()
    {
        string FilePath = Application.persistentDataPath + "/leaderboard.dic";
        if (File.Exists(FilePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(FilePath, FileMode.Open);

            Leaderboard LeaderBoardData = formatter.Deserialize(stream) as Leaderboard;
            stream.Close();

            return LeaderBoardData;

        }
        else
        {
            return null;
        }
    }
}
