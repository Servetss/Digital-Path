using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCircles : MonoBehaviour
{
    [Range(0f,1f)]
    [SerializeField] private float Speed;

    [SerializeField] private List<GameObject> Circles;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int ID = 0;
        foreach (GameObject Round in Circles)
        {
            if (ID % 2 == 0)
                Round.transform.rotation *= Quaternion.Euler(0, 0, (ID + 1) * Speed);
            else
                Round.transform.rotation *= Quaternion.Euler(0, 0, (ID + 1) * - Speed);

            ID++;
        }
    }
}
