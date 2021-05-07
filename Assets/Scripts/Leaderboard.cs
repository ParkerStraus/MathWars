using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Leaderboard
{
    public List<Entries> List = new List<Entries>(capacity: 10);
    public Leaderboard(List<Entries> List)
    {
        this.List = List;
    }

    public List<Entries> GetList()
    {
        return List;
    }
}
