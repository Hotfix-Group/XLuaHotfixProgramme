﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitUI : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        LuaManager.GetInstance().GameStart(); 
    }
}