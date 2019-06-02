using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle_Type_6 : MainCircle
{

    private void FixedUpdate()
    {
        if (EventClick)
        { CircleMove(); }
    }
}
