using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private string CurrentNum;
    [SerializeField] private TextMeshProUGUI numText;
    [SerializeField] private bool numberReady = false;
    [SerializeField] private int Killcount = 0;
    [SerializeField] private bool Paused;
    [SerializeField] private bool GameFinished = false;
    // Start is called before the first frame update
    void Start()
    {
        Paused = gameObject.GetComponent<PauseScript>().GetStatus();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            gameObject.GetComponent<PauseScript>().PauseHandler();
        }
        if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0))
        {
            CurrentNum += "0";
            numberReady = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            CurrentNum += "1";
            numberReady = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            CurrentNum += "2";
            numberReady = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            CurrentNum += "3";
            numberReady = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            CurrentNum += "4";
            numberReady = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
        {
            CurrentNum += "5";
            numberReady = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
        {
            CurrentNum += "6";
            numberReady = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7))
        {
            CurrentNum += "7";
            numberReady = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8))
        {
            CurrentNum += "8";
            numberReady = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9))
        {
            CurrentNum += "9";
            numberReady = true;
        }
        else if (Input.GetKeyDown(KeyCode.KeypadMinus) || Input.GetKeyDown(KeyCode.Minus)) 
        {
            if ("-" != CurrentNum.Substring(0, 1) || CurrentNum.Length == 0) CurrentNum = "-" + CurrentNum;
        }
        numText.text = CurrentNum;

    }
    public string GetMathNum()
    {
        if (CurrentNum != null)
        {
            string newstring = CurrentNum;
            CurrentNum = null;
            numberReady = false;
            return newstring;
        }
        else
        {
            return null;
        }
    }

    public void MathEnemykilled(int points)
    {
        Killcount++;
    }

    public bool CheckNumberReady()
    {
        return numberReady;
    }

    public void EndGame()
    {
        GameFinished = true;
        Debug.Log("You fiddled the riddle");
    }

    public void SetPaused(bool pause)
    {
        Paused = pause;
    }

    public bool AmIInGame()
    {
        if (GameFinished)
        {
            return false;
        }
        if (Paused)
        {
            return false;
        }
        else return true;
    }

    public bool AmIFinished()
    {
        return GameFinished;
    }
}
