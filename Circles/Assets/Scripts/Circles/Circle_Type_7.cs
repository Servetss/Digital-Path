using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle_Type_7 : MainCircle
{
    private Vector3 MousePosStart;
    private Vector3 MoysePosEnd;

    bool Start = false;
    private void FixedUpdate()
    {
        if (EventClick)
        {
            if (!Start)
            {
                Start = true;
                MousePosStart = Input.mousePosition;
            }

            transform.rotation = Quaternion.Euler(0, 0, Vector3.Distance(MousePosStart, Input.mousePosition));
        }
    }
}
