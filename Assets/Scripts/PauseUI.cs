using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private GameObject[] Ui;
    [SerializeField] private int UISetting;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetUI(int ui)
    {
        for(int i = 0; i < Ui.Length; i++)
        {
            Ui[i].SetActive(false);
        }
        Ui[ui].SetActive(true);
        UISetting = ui;
    }

    public int GetUI()
    {
        return UISetting;
    }
}
