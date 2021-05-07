using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public int Level;
    public int Number1;
    public int Number2;
    public int Function;
    public float MoveSpeed;
    public float MaxTime;
    public GameObject HandlerObj;
    public GameObject MathPrompt;
    public bool IsRight;

    private PromptScript promptScript;
    private GameHandler Handler;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "bullet"){
            Handler.EnablePrompt(Number1, Number2, Function, this.gameObject, MaxTime);
        }
    }

    private void Awake()
    {
        HandlerObj = GameObject.Find("Main Camera");
        Handler = HandlerObj.GetComponent<GameHandler>();
        Level = Handler.Level;
        
    }
    void Start()
    {
        transform.name = transform.name.Replace("(Clone)", "").Trim();
        if (transform.position.x < 0)
        {
            IsRight = true;
        }
        else
        {
            IsRight = false;
        }

        if (3 > Level && Level >= 0)
        {
            Debug.Log("Level Set 1");
            Number1 = Random.Range(1, 10);
            Number2 = Random.Range(1, 10);
            Function = 0;
            MaxTime = 30;
        }
        else if (Level < 6 && Level >= 3)
        {
            Debug.Log("Level Set 2");
            Number1 = Random.Range(1, 10);
            Number2 = Random.Range(1, 10);
            Function = 1;
            MaxTime = 15;
        }
        else if (Level < 9 && Level >= 6)
        {
            Debug.Log("Level Set 3");
            Number1 = Random.Range(1, 10);
            Number2 = Random.Range(1, 10);
            Function = Random.Range(0, 1);
            MaxTime = 10;
        }
        else if (Level < 12 && Level >= 9)
        {
            Debug.Log("Level Set 4");
            Number1 = Random.Range(1, 20);
            Number2 = Random.Range(1, 20);
            Function = 2;
            MaxTime = 10;
        }
        else if (Level < 15 && Level >= 12)
        {
            Debug.Log("Level Set 5");
            Number1 = Random.Range(20, 60);
            Number2 = Random.Range(20, 60);
            Function = Random.Range(0, 2);
            MaxTime = 10;
        }
        else if (Level < 18 && Level >= 15)
        {
            Debug.Log("Level Set 6");
            Number1 = Random.Range(40, 80);
            Number2 = Random.Range(40, 80);
            Function = Random.Range(0, 2);
            MaxTime = 5;
        }
        else
        {
            Debug.Log("Level Set 7");
            Number1 = Random.Range(60, 100);
            Number2 = Random.Range(60, 100);
            Function = Random.Range(0, 2);
            MaxTime = 5;
        }
    }

    private void Update()
    {
        if (IsRight == true)
        {
            transform.position += Vector3.right * (MoveSpeed * Time.deltaTime);
        }
        else transform.position += Vector3.left * (MoveSpeed * Time.deltaTime);
        if (transform.position.x > 11 || transform.position.x < -11)
        {
                transform.position = (Vector3.up * transform.position.y) + (Vector3.right * transform.position.x * -1);
            
        }
    }

}
