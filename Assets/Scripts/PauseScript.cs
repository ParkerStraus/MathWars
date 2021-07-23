using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    [SerializeField] public bool Paused;
    [SerializeField] private bool usingUI;
    [SerializeField] private PauseUI PauseUi;
    [SerializeField] private GameObject PauseUiObject;
    [SerializeField] private GameObject gh;
    [SerializeField] private int UISetting;
    // Start is called before the first frame update
    void Start()
    {
        gh = GameObject.Find("GameHandler");
        PauseUi = PauseUiObject.GetComponent<PauseUI>();
        if (usingUI)
        {
            UISetting = PauseUi.GetUI();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Cancel"))
        {
            PauseHandler();
        }
    }

    public void PauseHandler()
    {
        if (gh.GetComponent<GameHandler>().AmIFinished() == false)
        {
            Paused = !Paused;

            if (Paused && UISetting == 0)
            {
                Pause();
            }
            else
            {
                Unpause();
            }
        }
    }

    void Pause()
    {
        gh.GetComponent<GameHandler>().SetPaused(true);
        Time.timeScale = 0;
        PauseUiObject.SetActive(true);
    }

    public bool GetStatus()
    {
        return Paused;
    }

    void Unpause()
    {
        gh.GetComponent<GameHandler>().SetPaused(false);
        Time.timeScale = 1;
        PauseUiObject.SetActive(false);
    }
}
