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
            float dis = 0;
            if (!Start)
            {
                Start = true;
                MousePosStart = Input.mousePosition;
                
            }
            
            dis = ((MousePosStart.y - Input.mousePosition.y) + (MousePosStart.x - Input.mousePosition.x)) / 2;

            transform.rotation = Quaternion.Euler(0, 0, dis);

            //transform.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        }
    }
}
