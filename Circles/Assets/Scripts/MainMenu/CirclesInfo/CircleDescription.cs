using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleDescription : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Text CircleName;
    [SerializeField] private Text TextInfo;
    [SerializeField] private Image CircleRenderer;


    public void Init(string _Description, Color32 _Color, string _name, bool Open)
    {
        if (Open)
        {
            CircleName.text = _name;
            TextInfo.text = _Description;
            CircleRenderer.color = _Color;
        }
        else
        {
            CircleName.text = "";
            TextInfo.text = "???";
            CircleRenderer.color = Color.black;
        }
    }
}
