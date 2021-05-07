using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CompleteScreen : MonoBehaviour
{
    public bool GameWin;
    [SerializeField] private int Score;
    private TMP_InputField InputField;
    [SerializeField] private GameObject ScrollBar;
    [SerializeField] private float ScrollBarOffset;
    [SerializeField] private List<Entries> LBData;
    [SerializeField] private GameObject LBPanel;
    [SerializeField] private GameObject LastEntry;
    [SerializeField] private GameObject LBEntryObj;
    [SerializeField] private List<GameObject> LBEntryList;
    [SerializeField] private Transform ListParentObject;
    [SerializeField] private GameObject ListStartPos;
    [SerializeField] private Vector3 EntryOffset;
    [SerializeField] private TMP_ColorGradient GameLose;

    public void Start()
    {
        Score = PlayerPrefs.GetInt("scoreAddition") + PlayerPrefs.GetInt("scoreGlobal");
        TMP_Text Title = GameObject.Find("TitleText").GetComponent<TMP_Text>();
        TMP_Text GreatJob = GameObject.Find("GreatJobText").GetComponent<TMP_Text>();
        TMP_Text Rank = GameObject.Find("RankText").GetComponent<TMP_Text>();
        
        switch (PlayerPrefs.GetInt("completeset"))
        {
            
            case 0:
                Title.text = "Congratulations";
                GreatJob.text = "You are the Math Master";
                break;
            case 1:
                Title.text = "Game Over";
                GreatJob.text = "Never give up! You can do it!";
                Title.colorGradientPreset= GameLose;
                break;
        }
        Leaderboard data = SaveSystem.LoadLB();
        int RankNum = 0;
        if (data != null)
        {
            LBData = data.GetList();
            for (int i = 0; i < LBData.Count; i++)
            {
                if (Score > LBData[i].GetScore())
                {
                    RankNum = i + 1;
                    break;
                }
            }
        }
        else RankNum = 1;
        
        
        Rank.text = "Your rank is #" + RankNum.ToString();
        InputField = GameObject.Find("InitialInput").GetComponent<TMP_InputField>();
        UpdateLeaderBoard();

    }
    public void inputName()
    {
        string Initials = InputField.text;
        LBData.Add(new Entries(Initials, Score));
        LBData.Sort(delegate (Entries x, Entries y)
        {
            return y.GetScore().CompareTo(x.GetScore());
        });
        UpdateLeaderBoard();
        SaveSystem.SaveLB(LBData);
    }

    public void NewGame(bool Newgame)
    {
        PlayerPrefs.SetInt("scoreGlobal", 0);
        PlayerPrefs.SetInt("scoreAddition", 0);
        PlayerPrefs.SetInt("levelGlobal", 0);
        PlayerPrefs.SetInt("Item1", 0);
        PlayerPrefs.SetInt("Item2", 0);
        PlayerPrefs.SetInt("Item3", 0);
        if (Newgame)
        {
            SceneManager.LoadScene(1);

        }
        else
        {
            SceneManager.LoadScene(0);

        }

    }

    public void ScrollBarUpdate()
    {
        float panelheight = Screen.height + LBPanel.GetComponent<RectTransform>().sizeDelta.y;
        float scrollbarvalue = ScrollBar.GetComponent<Scrollbar>().value;
        float Listoffset  = (LBEntryList.Count * EntryOffset.y) + panelheight - 20;
        if(panelheight < Listoffset) Listoffset = 0;
        Debug.Log(panelheight.ToString() + "  "+ Listoffset.ToString());
        float finaloffset = Listoffset * scrollbarvalue;
        var index = 0;
        foreach(GameObject i in LBEntryList)
        {
            i.transform.position = (ListStartPos.transform.position + ((EntryOffset.y * index) - finaloffset) * transform.up);
            index++;
        }
    }

    void UpdateLeaderBoard()
    {
        for (int i = 0; i < LBData.Count-1; i++)
        {
            LBEntryList.Clear();
            GameObject obj = GameObject.Find("Entry" + i);
            GameObject.Destroy(obj);

        }

        for (int i = 0; i < LBData.Count; i++)
        {
           
            GameObject LBListEntry = Instantiate(LBEntryObj, Vector3.zero, Quaternion.Euler(Vector3.zero), ListParentObject);
            if (i == 0)LBListEntry.transform.position = ListStartPos.transform.position;
            else LBListEntry.transform.position = LastEntry.transform.position + EntryOffset;
            LBListEntry.GetComponent<LBEntry>().SetText(i + 1, LBData[i].GetInitials(), LBData[i].GetScore());
            LBListEntry.gameObject.name = "Entry" + i;
            LBEntryList.Add(LBListEntry);
            LastEntry = LBListEntry;
        }
    }
}
