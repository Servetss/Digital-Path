using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : MonoBehaviour
{
    [SerializeField] private GameObject CurrentPageImage;

    public void SetCurrentPage(int _PageSelected) // 0, 1, 2
    {
        switch (_PageSelected)
        {
            case 0:
                CurrentPageImage.transform.localPosition = new Vector2 (-50, 0); // Set Marker on the left Position
                break;
            case 1:
                CurrentPageImage.transform.localPosition = new Vector2(0, 0); // Set Marker on the Center
                break;
            case 2:
                CurrentPageImage.transform.localPosition = new Vector2(50, 0); // Set Marker on the right Position
                break;
            default:
                break;
        }
    }

}
