using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelComHand : MonoBehaviour
{
    public int Score;
    public int Level;
    public int Time;
    public bool BonusDone;
    [SerializeField] private int Item1;
    [SerializeField] private int Item2;
    [SerializeField] private int Item3;
    public TMP_Text ScoreText;
    public TMP_Text TimeText;
    public TMP_Text NLText;
    public TMP_Text ItemShopScoreText;
    public TMP_Text Item1Text;
    public TMP_Text Item2Text;
    public TMP_Text Item3Text;
    public AudioClip BuySound;
    public AudioClip NoBuySound;

    public int[] itemPrice;
    void Start()
    {

        Item1 = PlayerPrefs.GetInt("Item1");
        Item2 = PlayerPrefs.GetInt("Item2");
        Item3 = PlayerPrefs.GetInt("Item3");
        Level = PlayerPrefs.GetInt("levelGlobal");
        Score = PlayerPrefs.GetInt("scoreGlobal") + PlayerPrefs.GetInt("scoreAddition");
        if (PlayerPrefs.GetInt("levelGlobal") != 0 && (PlayerPrefs.GetInt("levelGlobal")+2)% 3 == 0)
        {
            NLText.text = "Next Level: Bonus Level";
        }
        else NLText.text = "Next Level: Level " + (Level+2).ToString();
    }

    public void NextLevel()
    {
        PlayerPrefs.SetInt("Item1", Item1);
        PlayerPrefs.SetInt("Item2", Item2);
        PlayerPrefs.SetInt("Item3", Item3);
        PlayerPrefs.SetInt("levelGlobal", Level + 1);
        PlayerPrefs.SetInt("scoreGlobal", Score);
        if (PlayerPrefs.GetInt("levelGlobal") != 0 && (PlayerPrefs.GetInt("levelGlobal") + 1) % 3 == 0)
        {
            SceneManager.LoadScene(3);
        }
        else SceneManager.LoadScene(1);
    }

    void Update()
    {
        ScoreText.text = "Score: " + Score.ToString();
        Item1Text.text = "SlowDown\n" + itemPrice[0] + " Points\nYou Have " + Item1;
        Item2Text.text = "Instakill\n" + itemPrice[1] + " Points\nYou Have " + Item2;
        Item3Text.text = "Bombs\n" + itemPrice[2] + " Points\nYou Have " + Item3;
        ItemShopScoreText.text = "Points Left: " + Score;
    }

    public void BuyItem(int item)
    {

        if (Score >= itemPrice[item])
        {
            Score -= itemPrice[item];
            this.GetComponent<AudioSource>().PlayOneShot(BuySound);
            switch (item)
            {
                case 0:
                    ++Item1;
                    break;
                case 1:
                    ++Item2;
                    break;
                case 2:
                    ++Item3;
                    break;
            }
        }
        else
        {
            this.GetComponent<AudioSource>().PlayOneShot(NoBuySound);
        }
        
    }

    public void ExitToMenu()
    {
        int[] Items = new int[]{Item1, Item2, Item3};
        SaveSystem.SaveData(Level, Score, Items);
        SceneManager.LoadScene(0);

    }

}



