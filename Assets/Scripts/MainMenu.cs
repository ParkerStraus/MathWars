using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] AudioClip SoundSel;
    [SerializeField] AudioClip SoundBac;
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

    public void Start()
    {

        Leaderboard data = SaveSystem.LoadLB();
        if (data != null)
        {
            LBData = data.GetList();

        }

        for (int i = 0; i < LBData.Count; i++)
        {
            
            GameObject LBListEntry = Instantiate(LBEntryObj, Vector3.zero, Quaternion.Euler(Vector3.zero), ListParentObject);
            if (i == 0) LBListEntry.transform.position = ListStartPos.transform.position;
            else LBListEntry.transform.position = LastEntry.transform.position + EntryOffset;
            LBListEntry.GetComponent<LBEntry>().SetText(i + 1, LBData[i].GetInitials(), LBData[i].GetScore());
            LBListEntry.gameObject.name = "Entry" + i;
            LBEntryList.Add(LBListEntry);
            LastEntry = LBListEntry;
        }

    }
    public void PlayGame(bool newgame)
    {
        if (newgame) { 
        PlayerPrefs.SetInt("scoreGlobal", 0);
        PlayerPrefs.SetInt("scoreAddition", 0);
        PlayerPrefs.SetInt("levelGlobal", 0);
        PlayerPrefs.SetInt("Item1", 0);
        PlayerPrefs.SetInt("Item2", 0);
        PlayerPrefs.SetInt("Item3", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            GameData data = SaveSystem.LoadData();
            PlayerPrefs.SetInt("scoreGlobal", data.score);
            PlayerPrefs.SetInt("scoreAddition", 0);
            PlayerPrefs.SetInt("levelGlobal", data.level);
            PlayerPrefs.SetInt("Item1", data.items[0]);
            PlayerPrefs.SetInt("Item2", data.items[1]);
            PlayerPrefs.SetInt("Item3", data.items[2]);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }
    }


    public void ScrollBarUpdate()
    {
        float panelheight = LBPanel.GetComponent<RectTransform>().sizeDelta.y;
        float scrollbarvalue = ScrollBar.GetComponent<Scrollbar>().value;
        float Listoverhang = (LBData.Count * EntryOffset.y) + panelheight -20;
        if (Listoverhang > 0) Listoverhang = 0;
        float finaloffset = Listoverhang * scrollbarvalue;
        var index = 0;
        foreach (GameObject i in LBEntryList)
        {
            i.transform.position = (ListStartPos.transform.position + ((EntryOffset.y * index) - finaloffset) * transform.up);
            index++;
        }
    }
    public void PlayAudio(bool forward)
    {
        if (forward == true)
        {
            GetComponent<AudioSource>().PlayOneShot(SoundSel);
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(SoundBac);
        }
    }
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
