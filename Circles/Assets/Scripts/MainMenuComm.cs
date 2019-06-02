using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuComm : MonoBehaviour
{

    [SerializeField] private GameObject Canvas;

    // Panels in the Canvas
    [SerializeField] public GameObject LevelPanel;
    [SerializeField] public GameObject SecondLevelPanel;
    [SerializeField] public GameObject MainPanel;


    private Button Buttons;

    private void Awake()
    {
        CreateLevelMenu();
        CreateMainPanel();
    }

    private void Start()
    {
        Time.timeScale = 1;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    #region CretePanelsMM
    private void CreateLevelMenu()
    {
        // Create Panel
        LevelPanel = Instantiate(LevelPanel);
        LevelPanel.transform.SetParent(Canvas.transform);
        LevelPanel.transform.localPosition = new Vector3(0, 0, 0);

        // Back Buttons
        Button btn_back = GameObject.Find("Back_B").GetComponent<Button>();
        btn_back.onClick.AddListener(delegate () { BackToMainMenu(); });
        LevelPanel.SetActive(false);



        // Create Panel
        SecondLevelPanel = Instantiate(SecondLevelPanel);
        SecondLevelPanel.transform.SetParent(Canvas.transform);
        SecondLevelPanel.transform.localPosition = new Vector3(0, 0, 0);

        // Back Buttons
        Button btn_back_second = GameObject.Find("Back_Sec_B").GetComponent<Button>();
        btn_back_second.onClick.AddListener(delegate () { BackToMainMenu(); });
        SecondLevelPanel.SetActive(false);
    }

    private void CreateMainPanel()
    {
        // Create Panel
        MainPanel = Instantiate(MainPanel);
        MainPanel.transform.SetParent(Canvas.transform);
        MainPanel.transform.localPosition = new Vector3(0, 0, 0);
    }
    #endregion

    public void OpenLevePanel()
    {

        // Active - Level Panel
        LevelPanel.SetActive(true);

        // Unactive - other
        MainPanel.SetActive(false);
    }

    public void OpenSecondLevelPanel()
    {
        // Active - Second Level Mode Panel
        SecondLevelPanel.SetActive(true);

        // Unactive - other
        MainPanel.SetActive(false);
    }

    public void BackToMainMenu()
    {
        // Active MainMenu
        MainPanel.SetActive(true);

        // Close other 
        LevelPanel.SetActive(false);
        SecondLevelPanel.SetActive(false);
    }
}
