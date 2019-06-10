using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnapScrolling : MonoBehaviour
{
    [Range(1,50)]
    [Header("Controller")]
    public int PanelCount;
    [Range(0, 500)]
    public int PanelOffset;
    [Range(0f,20f)]
    public float snapSpeed;
    [Range(0f, 5f)]
    public float ScaleOffset;
    [Range(1f, 20f)]
    public float SetSpeedScale;

    [Header("Other objects")]
    [SerializeField] private GameObject PanelPref;

    private GameObject[] instPans;
    private Vector2[] PanelPos;
    private Vector2[] pansScale;

    private Image[] img;

    private RectTransform contentRect;
    private Vector2 contentVector;

    [SerializeField] private int selectedPanID;
    private int SaveSelectedPanID;
    [SerializeField] private bool IsScrolling;
    [SerializeField] private MainMenuComm MMC;

    [Space]
    [Header("Level Mode")]
    [SerializeField] private GameObject LevelModeGroupe; // Open Level Mode When Clicked New Game
    [SerializeField] private Button ClassicModeButton;
    [SerializeField] private Button SecondModeButton;

    [Space]
    [Header("PageSelecter")]
    [SerializeField] private Page page;

    [Space]
    [Header("ButtonsName")]
    [SerializeField] private List<Sprite> ButtonImage;

    // Start is called before the first frame update
    void Start()
    {
        MMC = GameObject.Find("Main Camera").GetComponent<MainMenuComm>();
        contentRect = GetComponent<RectTransform>();

        // Set Function On Level Mode Panel
        ClassicModeButton.onClick.AddListener(delegate () { MMC.OpenLevePanel(); }); // MMC.OpenLevePanel(); 
        SecondModeButton.onClick.AddListener(delegate() { MMC.OpenSecondLevelPanel();  });

        // End


        SaveSelectedPanID = selectedPanID;

        // Initialization Buttons On the Main Manu
        instPans = new GameObject[PanelCount];
        PanelPos = new Vector2[PanelCount];
        pansScale = new Vector2[PanelCount];
        img = new Image[PanelCount];

        for (int i = 0; i < PanelCount; i++)
        {
            instPans[i] = Instantiate(PanelPref, transform, false);
            instPans[i].SetActive(true);
            SetButtons(instPans[i].transform.GetChild(0).gameObject, i);

            if (i == 0) continue;
            instPans[i].transform.localPosition = new Vector2(instPans[i - 1].transform.localPosition.x + PanelPref.GetComponent<RectTransform>().sizeDelta.x + PanelOffset, instPans[i].transform.localPosition.y);
            PanelPos[i] = -instPans[i].transform.localPosition;
        }
        // End
    }

    private void SetButtons(GameObject Button, int IDButton)
    {
        Button.GetComponent<Image>().sprite = ButtonImage[IDButton];


        Button btn_lvl = Button.GetComponent<Button>();
        switch (IDButton)
        {
            case 0: // Open Level
                btn_lvl.onClick.AddListener(delegate () { LevelMode(); }); // MMC.OpenLevePanel(); 
                break;
            case 1:  
                btn_lvl.onClick.AddListener(delegate () { OpenCirclePanel(); }); // MMC.OpenLevePanel(); 
                break;
            case 2:
                btn_lvl.onClick.AddListener(delegate () { AppClose(); });
                break;
            default:
                break;
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        float nearestPos = float.MaxValue;
 
        for (int i = 0; i < PanelCount; i++)
        {
            float distance = Mathf.Abs(contentRect.anchoredPosition.x - PanelPos[i].x);
            if (distance < nearestPos)
            {
                nearestPos = distance;
                selectedPanID = i;


                if (contentRect.anchoredPosition.x > -100 && SaveSelectedPanID != 0)
                {
                    SaveSelectedPanID = 0;
                    page.SetCurrentPage(SaveSelectedPanID);
                }
                else if ((contentRect.anchoredPosition.x <= -100 && contentRect.anchoredPosition.x > -400) && SaveSelectedPanID != 1)
                {
                    SaveSelectedPanID = 1;
                    page.SetCurrentPage(SaveSelectedPanID);
                }
                else if (contentRect.anchoredPosition.x <= -400 && SaveSelectedPanID != 2)
                {
                    SaveSelectedPanID = 2;
                    page.SetCurrentPage(SaveSelectedPanID);
                }

            }
            float scale = Mathf.Clamp(1 / (distance / PanelOffset) * ScaleOffset, 0.5f, 1f);

            //Set Scale
            pansScale[i].x = Mathf.SmoothStep(instPans[i].transform.localScale.x, scale, SetSpeedScale * Time.fixedDeltaTime);
            pansScale[i].y = Mathf.SmoothStep(instPans[i].transform.localScale.y, scale, SetSpeedScale * Time.fixedDeltaTime);
            instPans[i].transform.localScale = pansScale[i];

            //Set Alfa Color
            Color c = instPans[i].transform.GetChild(0).GetComponent<Image>().color;
            c.a = Mathf.SmoothStep(instPans[i].transform.GetChild(0).GetComponent<Image>().color.a, scale, SetSpeedScale * Time.fixedDeltaTime);
            instPans[i].transform.GetChild(0).GetComponent<Image>().color = c;
        }
        if (IsScrolling) return;
        contentVector.x = Mathf.SmoothStep(contentRect.anchoredPosition.x, PanelPos[selectedPanID].x, snapSpeed * Time.fixedDeltaTime);
        contentRect.anchoredPosition = contentVector;
    }

    public void Scrolling(bool scroll)
    {
        IsScrolling = scroll; 
    }

    private void MainButtonsHide(bool Hide)
    {
        for (int i = 0; i < PanelCount; i++)
        {
            instPans[i].SetActive(!Hide);
        }
    }

    private void LevelMode()
    {
        MainButtonsHide(true);
        LevelModeGroupe.SetActive(true);
    }


    private void OpenCirclePanel()
    {
        MMC.CircleInfoPanel.SetActive(true);
        //MainButtonsHide(true);
    }

    public void Back_LevelMode()
    {
        MMC.CircleInfoPanel.SetActive(false);
        LevelModeGroupe.SetActive(false);

        MainButtonsHide(false);
    }


    private void AppClose()
    {
        Application.Quit();
    }

}