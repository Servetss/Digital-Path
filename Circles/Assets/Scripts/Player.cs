using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private Spawn _Spawn;
    
    [SerializeField] private Text Timer;
    [SerializeField] private Text RecordTime;
    [SerializeField] private List<GameObject> Stars;

    [Range(0.1f, 2f)]
    public float DistanceToFirstCircle; // Дистанция которую должен пройти герой, чтобы добратся до первого кружка 
    [Range(0.1f, 1f)]
    public float DistanceBetween;       // Дистанция между кружками


    private bool TimerWork = true;
    public float sec = 5f, min, hour;   //Time for Timer
    private string sec_s, min_s;
    private Vector3 dir;                // Vector Move
    private Vector3 MoveDirection;      // Player Start Move in Second Level Mode 
    private bool Move = false;          // Move (First Mode)    
    private bool MoveToNext = false;    // Move To Next Circle (Second Mode)
    private bool GameOverMove = false;   // Player Move When Second Mode Level Ended
    private float PositionToWin;        // Circles Count On the Level
    private Vector3 startPos;           // Start Player Position
    private float distance;             // Distance Player Pos and Start Pos Move
    

    float minuteRec;
    float secondRec;
    // End
    private bool end = false;
    private Vector3 _EndMove;

    LevelBuild LB;
    LineRenderer LR;
    Vector3 vec;


    private void Start()
    {
        LB = GameObject.Find("SaveData").GetComponent<LevelBuild>();
        if (LB.LevelMode == 1)
        {
            LR = gameObject.GetComponent<LineRenderer>();
            LR.SetPosition(0, transform.position);
            vec = transform.position.normalized;

            float y = UnityEngine.Random.Range(-1f, 1f);
            float x = UnityEngine.Random.Range(-1f, 1f);

            vec.y += y;
            vec.x += x;
            vec = vec.normalized;
            LR.SetPosition(1, vec);
        }

        try
        {
            if (LB.RecordTime == 0)
            {
                minuteRec = 0;
                secondRec = 0;
            }
            else
            {
                string[] words = LB.RecordTime.ToString().Split(new char[] { '.' });
                minuteRec = Convert.ToSingle(words[0]);
                secondRec = Mathf.Round(Convert.ToSingle(words[1]));
            }

            RecordTime.text = minuteRec + " : " + secondRec;
            

            for (int i = 0; i < LB.Stars; i++)
            {
                Stars[i].SetActive(true);
            }
            
        }
        catch { }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Move) FuncMove(true); // First Level Mode (true)

        if (MoveToNext) FuncMove(false); // First Level Mode (false - Second Mode)
        else if (GameOverMove) EndMove();

        if (!end)
        {
            if (distance >= DistanceToFirstCircle + (DistanceBetween * (_Spawn.GetCirclesCount() - 3)))  // (_Spawn.GetCirclesCount() - 1)
            {
                GameOver(true);   // Win?
            }

            TimerFunk();
        }
    }

    private void FuncMove(bool FirstMode)
    {
        if (FirstMode)
            transform.Translate(dir * (Speed * 0.01f));
        else
            transform.Translate(vec * (Speed * 0.01f));

        Vector3 Player_Vec = gameObject.transform.position - startPos;
        distance = Mathf.Sqrt((Player_Vec.x * Player_Vec.x) + (Player_Vec.y * Player_Vec.y) + (Player_Vec.z * Player_Vec.z));


        if (!FirstMode) // If Second Level Mode
        {
            if (_Spawn.PositionClick == 1 && DistanceToFirstCircle <= distance)
            {
                gameObject.transform.SetParent(_Spawn.LevelCircles[_Spawn.PositionClick - 1].transform);
                LR.enabled = false;
                MoveToNext = false;
            }
            else if (_Spawn.PositionClick > 1 && (DistanceToFirstCircle + (DistanceBetween * (_Spawn.PositionClick - 1))) <= distance )
            {
                gameObject.transform.SetParent(_Spawn.LevelCircles[_Spawn.PositionClick - 1].transform);
                MoveToNext = false;
            }
        }
    }

    private void EndMove()
    {
        transform.Translate(_EndMove * (Speed * 0.01f));
    }


    private void TimerFunk()
    {
        if (TimerWork)
        {
            if (sec >= 60)
            {
                sec = 0;

                min = min == 0 ? 0 : min - 1;
            }
            else if (min == 0 && sec <= 0)
            {
                TimerWork = false;
                min = 0;
                sec = 0;
            }
            else
                sec -= Time.deltaTime;

            sec_s = sec < 10 ? "0" + Mathf.Round(sec).ToString() : Mathf.Round(sec).ToString();
            min_s = min < 10 ? "0" + min.ToString() : min.ToString();
            Timer.text = min_s + ":" + sec_s;
        }
    }

    // Second Mode Level
    public void GoToNextCircle()
    {
        MoveToNext = true;
    }


    // First Mode Level
    public void PlayerShoot(Vector3 _PositionToMove)
    {
        dir = _PositionToMove;
        Move = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Circle")
        {
            if (!end)
            {
                Speed = 0;
                GameOver(false);
            }
        }
    }

    public void GameOver(bool win)
    {
        end = true;
        if (!win)
        {
            Move = false;
            MoveToNext = false;
        }
        else
        {
            gameObject.transform.SetParent(Camera.main.transform);
            _EndMove = transform.position - new Vector3(0, 0, 0);
            MoveToNext = false;
            GameOverMove = true;
        }

        if (win)
        {
            PlayerPrefs.SetFloat("Timer" + LB.LevelMode, min + Convert.ToSingle(sec_s) * 0.1f);

            PlayerPrefs.SetFloat("T" + LB.LevelButtonID + LB.LevelMode, min + Convert.ToSingle(sec_s) * 0.1f);
            PlayerPrefs.SetString("Wins" + LB.LevelButtonID + LB.LevelMode, "True");
        }
        else
        {
            PlayerPrefs.SetFloat("T" + LB.LevelButtonID + LB.LevelMode, min + Convert.ToSingle(sec_s) * 0.1f);

            PlayerPrefs.SetFloat("Timer" + LB.LevelMode, minuteRec + (secondRec * 0.1f));

            if (PlayerPrefs.GetString("Wins" + LB.LevelButtonID + LB.LevelMode) != "True")
            {
                PlayerPrefs.SetString("Wins" + LB.LevelButtonID + LB.LevelMode, "False");
            }
        }

        string endText = win ? "Win !" : "Game Over!";
        Camera.main.GetComponent<LevelScripts>().PauseClick(endText);
    }
}
