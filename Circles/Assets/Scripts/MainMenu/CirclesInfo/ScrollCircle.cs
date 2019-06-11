using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollCircle : MonoBehaviour
{
    [SerializeField] private Button Back_B;
    [SerializeField] private List<MainCircle> Circles;
    [SerializeField] private GameObject Content;

    private void Start()
    {
        Back_B.onClick.AddListener(delegate () {  Camera.main.gameObject.GetComponent<MainMenuComm>().BackToMainMenu(); });

        int count = 0;
        foreach (MainCircle Circle in Circles)
        {
            bool Open = count - 1 <= PlayerPrefs.GetFloat("UnlockCircle") ? true : false;


            Content.transform.GetChild(count).GetComponent<CircleDescription>().Init(Circle.GetDescription(), Circle.GetColor(), Circle.GetName(), Open);
            count++;
        }
    }
}