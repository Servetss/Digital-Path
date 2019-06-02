using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle_Type_3 : MainCircle
{
    [Header("Personal Settings")]
    [SerializeField]
    private float SpeedAfter;

    public override void AfterClick()
    {
        ChangeSpeed(SpeedAfter);
    }
}
