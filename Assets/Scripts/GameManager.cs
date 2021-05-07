using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool GamePaused = false;
    public int Level = 0;
    public int Score = 0;
    public int Answer = 0;
    public int ScoreAddition = 0;
    public int ScoreMulti;
    private int InputFromPrompt;
    private int AstSpawned = 0;
    [SerializeField] private int AsteroidsDestroyed = 0;
    [SerializeField] private int AsteroidTarget = 0;
    [SerializeField] private int AlreadySpawned = 0;
    public int AstMax;
    [SerializeField] private double SpawnTime = 0;
    public double SpawnNow;
    public float PromptTime = 0f;
    public float MaxTime = 0f;
    public bool NowGamingMode = true;

    public GameObject MathPrompt;
    public GameObject AsteroidObj;
    private GameObject AsteroidInPrompt;
    public GameObject Ship;
    public GameObject Text1;
    public GameObject Text2;
    public GameObject Text3;
    public GameObject TextFlash;
    public GameObject TimeAmount;
    public GameObject ScoreGauge;
    public TMP_InputField TextField;

    private TextMeshProUGUI Number1;
    private TextMeshProUGUI Number2;
    private TextMeshProUGUI Function;
    private TextMeshProUGUI TimeText;
    private TextMeshProUGUI ScoreText;

    [SerializeField] private bool PromptOn;
    [SerializeField] private bool PromptInputFinished = false;
    [SerializeField] private bool CorrectAnswer;

    void Start()
    {
        Ship = GameObject.Find("ship");
        Number1 = Text1.GetComponent<TextMeshProUGUI>();
        Number2 = Text2.GetComponent<TextMeshProUGUI>();
        Function = Text3.GetComponent<TextMeshProUGUI>();
        TimeText = TimeAmount.GetComponent<TextMeshProUGUI>();
        ScoreText = ScoreGauge.GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        if (GamePaused == true)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        if (Input.GetKeyDown("a"))
        {
            SpawnAsteroid();
        }
        TimeText.text = System.Math.Round(PromptTime, 0).ToString();
        ScoreText.text = "Score: " + Score.ToString();
        MathPromptManager();
        SpawningHandler();
        if (PromptOn == true && Input.GetKeyDown("enter")){
            PromptInput();
        }
        if (AsteroidsDestroyed == AsteroidTarget)
        {
            CompleteLevel();
        }
    }
    public void SetPaused()
    {
        if (GamePaused == false)
        {
            GamePaused = true;
        }
        else
        {
            GamePaused = false;
        }
    }

    public void EnablePrompt(int N1, int N2, int F, GameObject ast, float Max)
    {
        MathPrompt.SetActive(true);
        Number1.text = N1.ToString();
        Number2.text = N2.ToString();
        switch (F)
        {
            case 0:
                Function.text = "+";
                Answer = N1 + N2;
                break;
            case 1:
                Function.text = "-";
                Answer = N1 - N2;
                break;
            case 2:
                Function.text = "X";
                Answer = N1 * N2;
                break;

        }
        MaxTime = Max;
        PromptTime = MaxTime;
        PromptOn = true;

        AsteroidInPrompt = ast;
        AstSpawned -= 1;

    }

    void SpawnAsteroid()
    {
        float SpawnOffset = 0f;
        bool SpawnRight = true;

        if (Random.value >= 0.5)
        {
            SpawnRight = true;
        }
        else
        {
            SpawnRight = false;
        }
        if (SpawnRight) SpawnOffset = 11f;
        else SpawnOffset = -11f;

        AlreadySpawned += 1;
        Vector3 asttransform = Vector3.zero;
        float YOffset = Random.value * 4f;
        asttransform = asttransform + (Vector3.right * SpawnOffset) + (Vector3.up * YOffset);
        Instantiate(AsteroidObj, asttransform, Quaternion.Euler(0f, 0f, 0f));

    }

    void CompleteLevel()
    {
        Debug.Log("Level Complete");

    }

    void MathPromptManager()
    {
        if (PromptOn)
        {
            GamePaused = true;
            PromptTime -= Time.unscaledDeltaTime;

        }

        if (PromptTime <= 0f || PromptInputFinished)
        {
            if (CorrectAnswer == true)
            {
                CalculateAnswer();
                Score += ScoreAddition;
            }
            else
            {
            }
            GamePaused = false;
            PromptOn = false;
            MathPrompt.SetActive(false);
            Destroy(AsteroidInPrompt);
            TextField.text = "";
            PromptInputFinished = false;
        }

    }
    private void CalculateAnswer()
    {
        float ScoreBeforeInt = ScoreMulti * Mathf.Clamp(PromptTime / MaxTime, 0.25f, 1);
        ScoreAddition = Mathf.RoundToInt(ScoreBeforeInt);
    }

    void SpawningHandler()
    {
        if (SpawnTime >= SpawnNow && AstMax > AstSpawned && AlreadySpawned != AsteroidTarget)
        {
            SpawnTime = 0;
            SpawnAsteroid();
            AstSpawned += 1;
        }
        else
        {
            SpawnTime += Time.deltaTime;
        }
    }

    public void PromptInput()
    {
        InputFromPrompt = int.Parse(TextField.text);
        if (InputFromPrompt == Answer)
        {
            CorrectAnswer = true;
        }
        else if (InputFromPrompt != Answer)
        {
            CorrectAnswer = false;
        }
        AsteroidsDestroyed += 1;
        PromptInputFinished = true;

    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
