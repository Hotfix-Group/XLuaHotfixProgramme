using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 技能数据代理
/// </summary>
public class SkilldataProxy : PureMVC.Patterns.Proxy
{
    public new static string NAME = "VersionData";

    public enum SkillType
    {
        None,
        Ice = 1,
        Fire = 2,
        SG = 3
    }

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
    public void CostDiamond(SkillType skillType,int costDiamondNumber)
    {
        switch(skillType)
        {
            case SkillType.Ice:
                {
                   playerdata.DiamondNum -= SkillData.IceCost;
                }
                break;
            case SkillType.Fire:
                {
                    playerdata.DiamondNum -= SkillData.FireCost;
                }
                break;
            case SkillType.SG:
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
