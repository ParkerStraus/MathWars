using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Entries
{
    string Initials;
    int Score;
    public Entries(string Initials, int Score)
    {
        this.Initials = Initials;
        this.Score = Score;
    }

    public int GetScore()
    {
        return Score;
    }

    public string GetInitials()
    {
        return Initials;
    }
    // Start is called before the first frame update
    
}
