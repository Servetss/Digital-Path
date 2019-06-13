using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuild : MonoBehaviour
{
    [HideInInspector] public List<CirclesSpawn> CS_Level;
    [HideInInspector] public int LevelButtonID;

    [HideInInspector] public float RecordTime;
    [HideInInspector] public int  Stars;

    [HideInInspector] public int LevelMode;
    [HideInInspector] public List<float> LevelTimeRecords;

    [HideInInspector] public List<float> CirclesRotationPosZ;

    [HideInInspector] public int StartTimeLevel;

    [HideInInspector] public Vector2 PlayerVecorMove;

    [HideInInspector] public int LevelBlock;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }
}


