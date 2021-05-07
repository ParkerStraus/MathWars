using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LBEntry : MonoBehaviour
{
    // Start is called before the first frame update
    public void SetText(int pos, string initials, int score)
    {
        this.gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = pos.ToString();
        this.gameObject.transform.GetChild(1).GetComponent<TMP_Text>().text = score.ToString();
        this.gameObject.transform.GetChild(2).GetComponent<TMP_Text>().text = initials.ToString();
    }
}
