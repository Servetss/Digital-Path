using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCircle : MonoBehaviour
{
    [Header("Default Settings")]
    public float Additionaly_Time;
    [SerializeField] protected float Speed; // Default Speed
    [SerializeField] private Color32 CircleColor;
    [SerializeField] private CirclesEnum CircleEvent; // Type Circle (Click, Hold, Skip)

    protected GameObject CircleSize; // Circle Size Renderer
    protected bool AfterClickVariable = false; // Event After Click (Don`t necessarily)

    public bool EventClick = false; // Event Tuch to screen or Mouse Click (Don`t necessarily)  

    private int ID; // Personal identification Circle

    protected Player Player_S;

    // Methods

    private void Start()
    {
        Player_S = GameObject.Find("Player").GetComponent<Player>();

        //
        BeforClick();
    }
    private void FixedUpdate()
    {
        CircleMove();
    }

    // Set ID
    public void SetID(int _ID)
    { ID = _ID; }

    // Movable
    protected void CircleMove()
    {
        if(ID % 2 == 0)
            transform.rotation *= Quaternion.Euler(0, 0, Speed);
        else
            transform.rotation *= Quaternion.Euler(0, 0, -Speed);
    }
    protected void ChangeSpeed(float NewSpeed)
    { Speed = NewSpeed; }


    // Circle Renderer
    public void SetCircleSize(GameObject _CircleSize)
    {
        // Create Circle and Set Position
        CircleSize = Instantiate(_CircleSize, transform);
        GameObject child = transform.GetChild(0).gameObject;
        child.GetComponent<SpriteRenderer>().color = CircleColor;
    }


    // Checked which Type
    public bool GetCircleEnum(CirclesEnum _CircleEvent)
    {
        return CircleEvent == _CircleEvent ? true : false;
    }


    // Event Click
    protected virtual void BeforClick()
    {  }
    public virtual void AfterClick()
    {
        AdditionalTime();

        AfterClickVariable = true;
        Speed = 0;
    }

    public void AdditionalTime()
    {
        if (Player_S.sec + Additionaly_Time >= 60)
        {
            Player_S.min++;
            Player_S.sec = (Player_S.sec + Additionaly_Time) - 60;
        }
        else
            Player_S.sec += Additionaly_Time;
    }
}
