using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 技能数据模型
/// </summary>
public class SkilldataModel
{
    public int IceCost = -5;//寒冰技能消耗
    public int IceTime { get; set; }//寒冰持续时间
    public float IceCD { get; set; }//寒冰冷却时间
    public int FireCost = -10;//火焰技能消耗
    public int FireTime { get; set; }//寒冰持续时间
    public float FireCD { get; set; }//寒冰冷却时间
    public int SGCost = -15; //霰弹技能消耗
    public int SGTime { get; set; }//寒冰持续时间
    public float SGCD { get; set; }//寒冰冷却时间


}
