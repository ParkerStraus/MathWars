using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using TMPro;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public bool GamePaused = false;
    public int Level;
    public int Score = 0;
    public int Answer = 0;
    public int ScoreAddition = 0;
    public int ScoreMulti;
    private int InputFromPrompt;
    [SerializeField] private int AstSpawned = 0;
    [SerializeField] private int AsteroidsDestroyed = 0;
    [SerializeField] private int AsteroidTarget = 0;
    [SerializeField] private int AlreadySpawned = 0;
    [SerializeField] private int Item1 = 0;
    [SerializeField] private int Item2 = 0;
    [SerializeField] private int Item3 = 0;
    public float TimeLeft = 0;
    public int AstMax;
    [SerializeField]private double SpawnTime = 0;
    public double SpawnNow;
    private bool Instakill = false;
    private bool Slowdown = false;
    public float PromptTime = 0f;
    public float MaxTime = 0f;
    public bool NowGamingMode = true;
    public bool DisableMove = false;

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
    public GameObject TimeObject;
    public TextMeshProUGUI TimeGauge;
    public GameObject Timeline;
    public TMP_InputField TextField;
    public GameObject Item1Panel;
    public GameObject Item2Panel;
    public GameObject Item3Panel;
    public TextMeshProUGUI Item1Text;
    public TextMeshProUGUI Item2Text;
    public TextMeshProUGUI Item3Text;

    private TextMeshProUGUI Number1;
    private TextMeshProUGUI Number2;
    private TextMeshProUGUI Function;
    private TextMeshProUGUI TimeText;
    public GameObject PauseButton;
    private TextMeshProUGUI ScoreText;
    [SerializeField] private GameObject BorderLeft;
    [SerializeField] private GameObject BorderRight;
    [SerializeField] private GameObject UiLeft;
    [SerializeField] private GameObject UiRight;

    private PlayableDirector timeline;
    [SerializeField] private AudioClip[] Sounds = new AudioClip[3];
    [SerializeField] private bool PromptOn;
    [SerializeField] private bool PromptInputFinished = false;
    [SerializeField] private bool CorrectAnswer;

    void Start()
    {
        Level = PlayerPrefs.GetInt("levelGlobal");
        Ship = GameObject.Find("ship");
        Number1 = Text1.GetComponent<TextMeshProUGUI>();
        Number2 = Text2.GetComponent<TextMeshProUGUI>();
        Function = Text3.GetComponent<TextMeshProUGUI>();
        TimeText = TimeAmount.GetComponent<TextMeshProUGUI>();
        ScoreText = ScoreGauge.GetComponent<TextMeshProUGUI>();
        timeline = Timeline.GetComponent<PlayableDirector>();
        Item1 = PlayerPrefs.GetInt("Item1");
        Item2 = PlayerPrefs.GetInt("Item2");
        Item3 = PlayerPrefs.GetInt("Item3");
        TimeLeft = 300;

        PauseButton.transform.position = new Vector2(Camera.main.WorldToScreenPoint(UiLeft.transform.position).x, PauseButton.transform.position.y);
        TimeObject.transform.position = new Vector2(Camera.main.WorldToScreenPoint(UiRight.transform.position).x, TimeObject.transform.position.y);

        if (3 > Level && Level >= 0)
        {
            AsteroidTarget = 5;
            AstMax = 1;
            SpawnNow = 2;
        }
        else if (Level < 6 && Level >= 3)
        {
            AsteroidTarget = 8;
            AstMax = 3;
            SpawnNow = 1.75;
        }
        else if (Level < 9 && Level >= 6)
        {
            AsteroidTarget = 10;
            AstMax = 5;
            SpawnNow = 1.5;
        }
        else if (Level < 12 && Level >= 9)
        {
            AsteroidTarget = 12;
            AstMax = 8;
            SpawnNow = 1;
        }
        else if (Level < 15 && Level >= 12)
        {
            AsteroidTarget = 15;
            AstMax = 9;
            SpawnNow = 0.8;
        }
        else if (Level < 18 && Level >= 15)
        {
            AsteroidTarget = 20;
            AstMax = 12;
            SpawnNow = 0.75;
        }
        else
        {
            AsteroidTarget = 25;
            AstMax = 15;
            SpawnNow = 0.5;
        }
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

        /*if (Input.GetKeyDown("a"))
        {
            SpawnAsteroid();
        }*/

        if (Input.GetKeyDown(KeyCode.Return) && PromptOn){
            PromptInput();
        }
        TimeText.text = System.Math.Round(PromptTime,0).ToString();
        ScoreText.text = "Score: " + Score.ToString();
        MathPromptManager();
        SpawningHandler();
        if (!PromptOn)
        {
            if (Input.GetKeyDown("q") && !Slowdown)
            {
                ItemUse(0);
            }
            if (Input.GetKeyDown("w") && !Instakill)
            {
                ItemUse(1);
            }
            if (Input.GetKeyDown("e"))
            {
                ItemUse(2);
            }
        }
        Item1Text.text = ": " + Item1;
        Item2Text.text = ": " + Item2;
        Item3Text.text = ": " + Item3;
        TimeGauge.text = Mathf.FloorToInt(TimeLeft / 60).ToString("0") + ":" + Mathf.FloorToInt(TimeLeft % 60).ToString("00");

        if (AsteroidsDestroyed == AsteroidTarget)
        {
            PlayerPrefs.SetInt("Item1", Item1);
            PlayerPrefs.SetInt("Item2", Item2);
            PlayerPrefs.SetInt("Item3", Item3);
            PlayerPrefs.SetInt("scoreAddition", Score);
            CompleteLevel();
        }
        else
        {
            TimeLeft -= Time.unscaledDeltaTime;
        }
        if (TimeLeft <= 0f)
        {
            PlayerPrefs.SetInt("scoreAddition", Score);
            PlayerPrefs.SetInt("completeset", 1);
            SceneManager.LoadScene(4);
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
        AsteroidInPrompt = ast;
        if (Instakill == true)
        {
            PlaySound(Sounds[1]);
            Item2Panel.GetComponent<Image>().color = new Color(256, 0, 0);
            Instakill = false;
            Destroy(AsteroidInPrompt);
            AsteroidsDestroyed += 1;
            AstSpawned -= 1;
        }
        else
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
            if (Slowdown == true)
            {
                MaxTime = 2 * Max;
            }
            else MaxTime = Max;
            Slowdown = false;
            PromptTime = MaxTime;
            PromptOn = true;

            AstSpawned -= 1;
        }
    }

    void SpawnAsteroid()
    {
        float SpawnOffset = 0f;
        bool SpawnRight = true;

        if(Random.value >= 0.5)
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
        Instantiate(AsteroidObj,asttransform,Quaternion.Euler(0f,0f,0f));

    }

    void CompleteLevel()
    {
        PlayerPrefs.SetInt("scoreAddition", Score);
        timeline.Play();
        Ship.GetComponent<ShipController>().GameFinished = true;
    }

    public void LoadComplete()
    {
        if (Level == 21)
        {
            SceneManager.LoadScene(5);
            PlayerPrefs.SetInt("completeset", 0);
        }
        else SceneManager.LoadScene(2);

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
                TextFlash.GetComponent<FlashTextBehavior>().Activate(true);
            }
            else
            {
                TextFlash.GetComponent<FlashTextBehavior>().Activate(false);
            }
            GamePaused = false;
            PromptOn = false;
            MathPrompt.SetActive(false);
            PlaySound(Sounds[1]);
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

    void ItemUse(int item)
    {
        switch (item)
        {
            case 0:
                if (Item1 > 0)
                {
                    PlaySound(Sounds[2]);
                    Item1Panel.GetComponent<Image>().color = new Color(8, 243, 0);
                    Slowdown = true;
                    --Item1;
                }
                break;
            case 1:
                if (Item2 > 0)
                {
                    PlaySound(Sounds[2]);
                    Item2Panel.GetComponent<Image>().color = new Color(8, 243, 0);
                    Instakill = true;
                    --Item2;
                }
                break;
            case 2:
                if (Item3 > 0)
                {
                    PlaySound(Sounds[0]);
                    GameObject[] Asteroids;
                    Asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
                    foreach (GameObject asteroid in Asteroids)
                    {
                        Destroy(asteroid);
                        AsteroidsDestroyed += 1;
                        AstSpawned = 0;
                    }
                    --Item3;
                }
                break;
        }
    }

    public void PromptInput()
    {
        Item1Panel.GetComponent<Image>().color = new Color(256, 0, 0);
        InputFromPrompt = int.Parse(TextField.text);
        if (InputFromPrompt == Answer)
        {
            CorrectAnswer = true;

            PlayerPrefs.SetInt("scoreAddition", Score);
        }
        else
        {
            CorrectAnswer = false;
        }
        AsteroidsDestroyed += 1;
        PromptInputFinished = true;

    }
    public void PlaySound(AudioClip sound)
    {
        GetComponent<AudioSource>().PlayOneShot(sound);
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
