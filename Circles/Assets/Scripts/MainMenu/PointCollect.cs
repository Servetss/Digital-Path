using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointCollect : MonoBehaviour
{
    [SerializeField] private GameObject LevelsContent; // Content withs All Levels on The Level Panel
    [SerializeField] private Text AllStars_T;
    [SerializeField] private Text NeedStars_T;

    [SerializeField] public int LevelsEtap;
    [SerializeField] private int LevelGroupe;
    private List<GameObject> Levels;
    private int Points = 0;

    // Start is called before the first frame update
    void Start()
    {
        Levels = new List<GameObject>();

        //int LevelGroupe = (int)(LevelsContent.transform.childCount * 0.2f);
        
        for (int i = 0; i < LevelsContent.transform.childCount; i++)
        {
            GameObject Level_B = LevelsContent.transform.GetChild(i).gameObject;
            LevelManager LM = Level_B.GetComponent<LevelManager>();
            Points += LM.StarsCount;

            Levels.Insert(i, Level_B);
        }


        // Разблокировать уровни и установить Next Points
        bool SetNextStar = false;
        for (int i = 0; i <= LevelGroupe; i++)
        {
            for(int q = (i * LevelsEtap); q <= (i * LevelsEtap) + (LevelsEtap - 1); q++)
            { 
            //for (int q = (i * 10); q <= (i * 10) + 9; q++)
            //{
                if (Levels.Count > q)
                {
                    if (Points >= (i * (LevelsEtap * 2)))
                        Levels[q].SetActive(true);
                    else
                    {
                        if (!SetNextStar)
                        {
                            SetNextStar = true;
                            NeedStars_T.text = (i * (LevelsEtap * 2)).ToString();
                        }
                        Levels[q].SetActive(false);
                    }
                }
                else
                    break;
            }
        }

        AllStars_T.text = Points.ToString();
    }
}
