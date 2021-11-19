using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Interfaces;
/// <summary>
/// 技能面板中介
/// </summary>

public class SkillPanelMediator : PureMVC.Patterns.Mediator
{
    public const string SkillPanelMediatorName = "SkillPanelMediator";

    private SkillPanel view;

    public SkilldataProxy.SkillType skillType;

    SkilldataProxy skillData;

    public SkillPanelMediator(object viewComponent) : base(NAME, viewComponent)
    {
        view = ((GameObject)ViewComponent).GetComponent<SkillPanel>();
        skillData = Facade.RetrieveProxy(SkilldataProxy.NAME) as SkilldataProxy;
        view.BtnIce.onClick.AddListener(OnClickIce);
        view.BtnFire.onClick.AddListener(OnClickFire);
        view.BtnSG.onClick.AddListener(OnClickSG);
    }
    //
    public void OnClickIce()
    {
        skillType = (SkilldataProxy.SkillType)1;
        Debug.Log("MVCIce");
        SendNotification(ApplicationFacade.COST_DIAMOND,new CostDiamondCommand.Data(skillType,skillData.GetSkillData.IceCost));
    }
    public void OnClickFire()
    {
        skillType = (SkilldataProxy.SkillType)2;
        Debug.Log("MVCFire");
        SendNotification(ApplicationFacade.COST_DIAMOND, new CostDiamondCommand.Data(skillType, skillData.GetSkillData.IceCost));
    }
    public void OnClickSG()
    {
        skillType = (SkilldataProxy.SkillType)3;
        Debug.Log("MVCSG");
        SendNotification(ApplicationFacade.COST_DIAMOND, new CostDiamondCommand.Data(skillType, skillData.GetSkillData.IceCost));
    }
}
