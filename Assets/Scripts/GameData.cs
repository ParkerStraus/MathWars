using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int level;
    public int score;
    public int[] items = new int[3];

    public GameData (int level, int score, int[]items)
    {
        this.level = level;
        this.score = score;

        this.items = items;
    }
}
