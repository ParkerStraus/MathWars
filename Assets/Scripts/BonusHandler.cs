using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BonusHandler : MonoBehaviour
{
    public bool FunctDrawn = false;
    public int NumAmt;
    public int Number1;
    public int Number2;
    public int Function;
    public int Answer;
    public TMP_Text NumbText1;
    public TMP_Text NumbText2;
    public TMP_Text FuncText;
    public TMP_Text PromptText;
    public GameObject[] Cards;
    public GameObject[] CardBacks;
    public GameObject NumberCard;
    public GameObject FunctionCard;
    public GameObject AnswerBoxObj;
    public TMP_InputField AnswerBox;
    // Start is called before the first frame update
    void Start()
    {
        switch (PlayerPrefs.GetInt("levelGlobal"))
        {
            case 2:
                Number1 = Random.Range(1, 10);
                Number2 = Random.Range(1, 10);
                Function = 0;
                break;
            case 5:
                Number1 = Random.Range(1, 10);
                Number2 = Random.Range(1, 10);
                Function = 1;
                break;
            case 8:
                Number1 = Random.Range(1, 10);
                Number2 = Random.Range(1, 10);
                Function = Random.Range(0, 1);
                break;
            case 11:
                Number1 = Random.Range(1, 20);
                Number2 = Random.Range(1, 20);
                Function = 2;
                break;
            case 14:
                Number1 = Random.Range(20, 60);
                Number2 = Random.Range(20, 60);
                Function = Random.Range(0, 2);
                break;
            case 17:
                Number1 = Random.Range(40, 80);
                Number2 = Random.Range(40, 80);
                Function = Random.Range(0, 2);
                break;
        }
        switch (Function)
        {
            case 0:
                FuncText.text = "+";
                Answer = Number1 + Number2;
                break;
            case 1:
                FuncText.text = "-";
                Answer = Number1 - Number2;
                break;
            case 2:
                FuncText.text = "X";
                Answer = Number1 * Number2;
                break;
        }
        NumbText1.text = Number1.ToString();
        NumbText2.text = Number2.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void DrawCard(bool IsFunction)
    {
        if (IsFunction == true)
        {
            FunctDrawn = true;
            FunctionCard.SetActive(false);
            Cards[1].SetActive(true);
        }
        else if(IsFunction == false)
        {

            if (NumAmt == 1)
            {
                NumberCard.SetActive(false);
            }
            if (NumAmt < 2)
            {
                if(NumAmt == 0)
                {
                    Cards[0].SetActive(true);
                }
                else if(NumAmt == 1)
                {
                    Cards[2].SetActive(true);
                }
                NumAmt++;
            }
        }
        
        if((NumAmt == 2) && (FunctDrawn == true))
        {
            foreach(GameObject back in CardBacks)
            {
                back.SetActive(false);
            }
            AnswerBoxObj.SetActive(true);
            PromptText.text = "What's the answer?";

        }
    }
    public void CheckAnswer()
    {
        if (Answer== int.Parse(AnswerBox.text))
        {
            EndScreen(true);
        }
        else
        {
            EndScreen(false);
        }
    }

    void EndScreen(bool correct)
    {
        switch (correct)
        {
            case true:
                PlayerPrefs.SetInt("scoreAddition", 1000);
                SceneManager.LoadScene(2);
                break;
            case false:
                PlayerPrefs.SetInt("scoreAddition", 0);
                SceneManager.LoadScene(2);
                break;
        }
    }
}
