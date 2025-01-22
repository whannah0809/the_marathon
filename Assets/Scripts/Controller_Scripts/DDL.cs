using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDL : MonoBehaviour
{
    //For presistent manangers carried on to other scenes
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
