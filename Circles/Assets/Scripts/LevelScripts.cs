using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelScripts : MonoBehaviour
{
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private Player _Player;
    [SerializeField] private Text PauseText;
    public bool Pause = false;
    private LevelBuild LB;


    private void Start()
    {
        LB = GameObject.Find("SaveData").GetComponent<LevelBuild>();
    }


    public void BackClick()
    {
        Time.timeScale = 1;
        _Player.GameOver(false);
        Application.LoadLevel("MainMenu");
    }

    public void RestartClick()
    {
        Time.timeScale = 1;
        switch (LB.LevelMode)
        {
            case 0:
                Application.LoadLevel("Level");
                break;
            case 1:
                Application.LoadLevel("SecondMode");
                break;
            default:
                break;
        }
    }

    public void PauseClick(string _TextPause)
    {
        if (!PausePanel.activeSelf)
        {
            Pause = true;
            Time.timeScale = _TextPause == "Win !" ? 0.4f : 0;

            PauseText.text = _TextPause;
            PausePanel.SetActive(true);

        }
    }

    public void ResumeClick()
    {
        PausePanel.SetActive(false);
        Pause = false;
        Time.timeScale = 1;
    }

    public bool GetPause()
    { return !Pause; }

}
