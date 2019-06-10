using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawn : MonoBehaviour
{
    [Range(50, 100)]
    [SerializeField] private float ProcentXScreenSize = 80;   //Can not click below 80% Screnn Resolution
    [Range(50, 100)]
    [SerializeField] private float ProcentYScreenSize = 80;   //Can not click below 80% Screnn Resolution
    private Vector3 vec;  // Vectore Screen Resolution

    // Размеры кружков
    [SerializeField] private List<GameObject> SizeCircles;

    // Главный герой
    [SerializeField] private GameObject player;

    // Камера с главным скриптом
    [SerializeField] private LevelScripts LS;

    [SerializeField] private Text CircleName;

    // Класс главного героя
    MainCircle MC;
    LevelBuild LB;


    private Vector3 dir = Vector3.zero;
    public List<GameObject> LevelCircles;
    private Vector3 screenPos;

    private int LevelMode;

    // Start is called before the first frame update
    void Awake()
    {
        LB = GameObject.Find("SaveData").GetComponent<LevelBuild>();
        LevelMode = LB.LevelMode;

        LevelCircles = new List<GameObject>(LB.CS_Level.Count);
        int PositionSpawn = 0;
        int count = 0;
        foreach (CirclesSpawn Circle in LB.CS_Level)
        {
            if (PositionSpawn < SizeCircles.Count)
            {
                GameObject CircleObj = Instantiate(Circle.CircleType, transform.position, transform.rotation);
                LevelCircles.Add(CircleObj);
                CircleObj.GetComponent<MainCircle>().SetCircleSize(SizeCircles[PositionSpawn]);
                CircleObj.GetComponent<MainCircle>().SetID(PositionSpawn);
                if(LB.CirclesRotationPosZ.Count > 0) CircleObj.transform.rotation = Quaternion.Euler(0,0, LB.CirclesRotationPosZ[count]);
                    
                if(LevelMode == 1)  CircleObj.transform.localScale *= 1 + (float)PositionSpawn / 10;
                PositionSpawn++;
            }
            else
                break;

            count++;
        }

        CircleName.text = LB.CS_Level[0].CircleType.ToString();

        float ResolX = Screen.height * (ProcentXScreenSize / 100);
        float ResolY = Screen.width * (ProcentYScreenSize / 100);
        
        vec = new Vector3(ResolY, ResolX);
        
    }

    int Clickkk = 0; // Delete After
    public int PositionClick = 0;
    private void Update()
    {
        screenPos = Input.mousePosition;

        if (PositionClick < LB.CS_Level.Count)
        {
            MC = LevelCircles[PositionClick].GetComponent<MainCircle>();

            // Object Click
            if (MC.GetCircleEnum(CirclesEnum.Clickable))
            {
                EventClickable(MC);
            }
            // Object Hold
            else if (MC.GetCircleEnum(CirclesEnum.Hold))
            {
                EventHold(MC);
            }
            // Object Skip
            else if (MC.GetCircleEnum(CirclesEnum.Skiper))
            {
                EventSkipe(MC);
            }
        }
        else if (PositionClick == LB.CS_Level.Count)
        {
            if (!(screenPos.x > vec.x && screenPos.y > vec.y) && !LS.Pause && Input.GetMouseButtonDown(0))
            {
                PositionClick++;
                Vector2 m_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                dir = (m_pos - (Vector2)transform.position).normalized;

                player.GetComponent<Player>().PlayerShoot(dir);
            }
        }
    }

    //Input.GetKeyDown(KeyCode.Mouse0)

    private void EventClickable(MainCircle _MC)
    {
        if (!(screenPos.x > vec.x && screenPos.y > vec.y) && !LS.Pause && Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Default 
            PositionClick++;

            ChangeCircleName();

            if (LevelMode == 0)
            { _MC.AfterClick(); }
            else if (LevelMode == 1)
            {
                _MC.AdditionalTime();
                player.GetComponent<Player>().GoToNextCircle();
            }
        }
    }

    private void EventHold(MainCircle _MC)
    {
        if (!(screenPos.x > vec.x && screenPos.y > vec.y))
        {
            if (!LS.Pause && Input.GetKeyDown(KeyCode.Mouse0) && !_MC.EventClick)
            {
                _MC.EventClick = true;
            }
            if (!LS.Pause && Input.GetMouseButtonUp(0) && _MC.EventClick)
            {
                _MC.EventClick = false;
                _MC.AfterClick();
                PositionClick++;
            }

            ChangeCircleName();
        }
    }

    private void ChangeCircleName()
    {
        if (PositionClick < LB.CS_Level.Count)
        {
            CircleName.text = LB.CS_Level[PositionClick].CircleType.ToString();
        }
        else
            CircleName.text = "GO!";
    }

    private void EventSkipe(MainCircle _MC)
    {
        PositionClick++;
    }

    public int GetCirclesCount()
    { return LB.CS_Level.Count; }
}
