using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuild : MonoBehaviour
{
    public List<CirclesSpawn> CS_Level;
    public int LevelButtonID;
    public float RecordTime;
    public int  Stars;
    public int LevelMode;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }
}


