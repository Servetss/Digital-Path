using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimStart : MonoBehaviour
{
    [Header("Next Scene Name")]
    [SerializeField] private string LevelLoad;

    [Header("Animation Names")]
    [SerializeField] private string LogoAnimText;
    [SerializeField] private string PanelAnimText;

    [Header("Animation")]
    [SerializeField] private Animation LogoAnim;
    [SerializeField] private Animation PanelAnim;




    // Start is called before the first frame update
    void Start()
    {
        LogoAnim.Play(LogoAnimText);
        PanelAnim.Play(PanelAnimText);
    }

    public void EndClip()
    {
        Application.LoadLevel(LevelLoad);
    }
}
