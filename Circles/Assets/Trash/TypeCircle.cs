using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeCircle : MonoBehaviour
{
    public GameObject Circle1;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(Circle1, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
