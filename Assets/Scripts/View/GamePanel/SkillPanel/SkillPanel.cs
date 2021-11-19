using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SkillPanel
{
    public Button BtnFire;
    public Button BtnIce;
    public Button BtnSG;

    public Action FireAction = null;
    public Action IceAction = null;
    public Action SGAction = null;  
}
