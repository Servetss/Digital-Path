using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle_Type_4 : MainCircle
{
    private SpriteRenderer SR;
    private float Angle = 0;

    // Start is called before the first frame update
    void Start()
    {
        SR = CircleSize.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Move
        CircleMove();

        // Change Color
        Angle += Time.deltaTime;
        CircleSize.GetComponent<SpriteRenderer>().color = new Color(SR.color.r, SR.color.g, SR.color.b, Mathf.Sin(Angle));
    }
}
