using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{ 
    // Структура которая собирает уровень
    [SerializeField]
    private List<CirclesSpawn> CS_Level;

    [Header("Time")]
    [SerializeField] private List<float> TimeBonuses;

    public float PlayerTime;


    [Header("Stars Obj")]
    [SerializeField] private List<GameObject> Stars;


    private int LevelMode;  // 0 - First Level Mode, 1 - Second Level Mode

    private bool levelWin = false;
    public int StarsCount;

    private static int StarSumm = 0;
    LevelBuild LB;

    private void Awake()
    {
        //LevelMenu(Clone)
        //SecondLevelMenu(Clone)
        if (transform.parent.parent.parent.parent.name == "LevelMenu(Clone)")
            LevelMode = 0;
        else if (transform.parent.parent.parent.parent.name == "SecondLevelMenu(Clone)")
            LevelMode = 1;


        ////Reset
        //PlayerPrefs.SetFloat("SelfTime" + gameObject.name.Substring(6) + LevelMode, 0);
        //PlayerPrefs.SetString("Wins" + gameObject.name.Substring(6) + LevelMode, "");
        ////End Reset

        LB = GameObject.Find("SaveData").GetComponent<LevelBuild>();
        //if (LB.LevelMode == LevelMode)
        //{
            levelWin = PlayerPrefs.GetString("Wins" + gameObject.name.Substring(6) + LevelMode) == "True" ? true : false;


            if (LB.LevelButtonID != -1 && LB.LevelButtonID == Int32.Parse(gameObject.name.Substring(6)) && LB.LevelMode == LevelMode)
            {
                PlayerTime = PlayerTime > PlayerPrefs.GetFloat("Timer" + LevelMode) || PlayerTime <= 0 ? PlayerPrefs.GetFloat("Timer" + LevelMode) : PlayerTime;

                PlayerPrefs.SetFloat("Timer" + LevelMode, 0);
                PlayerPrefs.SetFloat("SelfTime" + gameObject.name.Substring(6) + LevelMode, PlayerTime);

                LB.LevelButtonID = -1;
            }


            PlayerTime = PlayerPrefs.GetFloat("T" + gameObject.name.Substring(6) + LevelMode);


            try
            {
                string[] words = PlayerTime.ToString().Split(new char[] { '.' });
                float second = Mathf.Round(Convert.ToSingle(words[1]));
            }
            catch { }


            // Star On Level
            int count = 0;

            foreach (float time in TimeBonuses)
            {
                if (levelWin && PlayerTime >= time * 0.1f)
                {
                    Stars[count].SetActive(true);
                    count++;
                }
            }

            StarsCount = count;
            StarSumm += StarsCount;
        //}
    }




    public void ButtonClick()
    {
        //print(PlayerTime);

        LB.CS_Level = CS_Level;
        LB.RecordTime = PlayerTime;
        LB.Stars = StarsCount;
        LB.LevelMode = LevelMode;
        string Level_ID = gameObject.name.Substring(6);  

        LB.LevelButtonID = Int32.Parse(Level_ID);

        //Level
        //SecondMode
        switch (LevelMode)
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
}
