using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MathEnemy : MonoBehaviour
{
    [SerializeField] private byte MathType;
    [SerializeField] private int Num1;
    [SerializeField] private int Num2;
    [SerializeField] private int Ans;
    [SerializeField] private TextMeshPro text;
    [SerializeField] private GameObject HandlerObject;
    [SerializeField] private int pointvalue;

    void Start()
    {
        text = GetComponent<TextMeshPro>();
        switch (MathType)
        {
            //Add
            case 0:
                Ans = Num1 + Num2;
                text.text = Num1 + "+" + Num2;
                break;
            //Subtract
            case 1:
                Ans = Num1 - Num2;
                text.text = Num1 + "-" + Num2;
                break;
            //Multi
            case 2:
                Ans = Num1 * Num2;
                text.text = Num1 + "X" + Num2;
                break;
            //Divide
            case 3:
                Ans = Num1 / Num2;
                text.text = Num1 + "÷" + Num2;
                break;
        }
    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int value = collision.GetComponent<ShipBlast>().Value;
        collision.GetComponent<ShipBlast>().Kill();
        if (Ans == value)
        {
            Debug.Log("Collided with " + collision.ToString());
            Destroy(gameObject, 0.25f);
            HandlerObject.GetComponent<GameHandler>().MathEnemykilled(pointvalue);
        }
    }

}
