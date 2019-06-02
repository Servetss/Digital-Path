using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle_Type_5 : MainCircle
{
    [Header("Personal Settings")]
    [SerializeField]
    private float Acceleration;
    private float Angle = 0;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!AfterClickVariable)
        {
            Speed = Mathf.Sin(Angle += Time.deltaTime);
            Speed = Speed < 0 ? Speed * -Acceleration : Speed * Acceleration;
            CircleMove();
        }
    }
}
