using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointCollect : MonoBehaviour
{
    [SerializeField] private GameObject LevelsContent; // Content withs All Levels on The Level Panel
    [SerializeField] private Text AllStars_T;
    [SerializeField] private Text NeedStars_T;

    public int LevelsEtap;
    public int LevelGroupe;

    private List<GameObject> Levels;
    private int Points = 0;

    private LevelBuild LB;

    // Start is called before the first frame update
    void Start()
    {
        Levels = new List<GameObject>();
        LB = GameObject.Find("SaveData").GetComponent<LevelBuild>();

        for (int i = 0; i < LevelsContent.transform.childCount; i++)
        {
            GameObject Level_B = LevelsContent.transform.GetChild(i).gameObject;
            LevelManager LM = Level_B.GetComponent<LevelManager>();
            Points += LM.StarsCount;

            Levels.Insert(i, Level_B);
        }

        // When find Level block - Set true
        bool FirstLevelBlock = false;
        // Unlocks levels and set "Next Points"
        bool SetNextStar = false;
        for (int i = 0; i <= LevelGroupe; i++)
        {
            for(int q = (i * LevelsEtap); q <= (i * LevelsEtap) + (LevelsEtap - 1); q++)
            {
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

                        if (!FirstLevelBlock) { FirstLevelBlock = true; LB.LevelBlock = q; } 


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
