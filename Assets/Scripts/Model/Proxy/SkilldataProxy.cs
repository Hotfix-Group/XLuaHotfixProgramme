using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 技能数据代理
/// </summary>
/// 
    public enum DiamondEvent
    {
        None,
        Boss = -3,
        Chest = -2,
        Fish = -1,
        Ice = 1,
        Fire = 2,
        SG = 3
 
    }

    public class SkilldataProxy : PureMVC.Patterns.Proxy
{
    public new static string NAME = "SkillData";

 

    public SkilldataModel SkillData;
    public PlayerdataModel playerdata;

    public override void OnRegister()
    {
        base.OnRegister();
        Debug.Log("SkillDataProxy OnRegister");
    }
    public SkilldataProxy(string name) : base(name, null)
    {
        SkillData = new SkilldataModel();
    }
    //技能花费钻石
    public void CostDiamond(DiamondEvent DiamondEvent,int costDiamondNumber)
    {
        switch(DiamondEvent)
        {
            case DiamondEvent.Ice:
                {
                   playerdata.DiamondNum -= SkillData.IceCost;
                }
                break;
            case DiamondEvent.Fire:
                {
                    playerdata.DiamondNum -= SkillData.FireCost;
                }
                break;
            case DiamondEvent.SG:
                {
                    playerdata.DiamondNum -= SkillData.SGCost;
                }
                break;
            default:
                return;
        }
    }
    public SkilldataModel GetSkillData
    {
        get
        {
            return Data as SkilldataModel;
        }
    }

    public override void OnRemove()
    {
        Debug.Log("SkillProxy OnRemove");
    }
}
