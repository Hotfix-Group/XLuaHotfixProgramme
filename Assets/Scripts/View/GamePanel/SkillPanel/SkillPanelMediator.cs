using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Interfaces;
/// <summary>
/// 技能面板中介
/// </summary>

//对于UI面板而言，应该只需要知道数值何时发生了变化，变化的值是多少。
//但对于游戏逻辑而言，开宝箱和开技能使数值发生变化的性质又是不一样的。
//同时涉及UI和游戏逻辑的内容，MVC应该如何进行管理？
//打个比方，冰冻技能的冰冻逻辑是否应该放进Command里？
public class SkillPanelMediator : PureMVC.Patterns.Mediator
{
    public const string SkillPanelMediatorName = "SkillPanelMediator";

    private SkillPanel view;

    public DiamondEvent DiamondEvent;

    SkilldataProxy skillData;

    public SkillPanelMediator(object viewComponent) : base(NAME, viewComponent)
    {
        //此处应检查View是否初始化，才可以进行事件绑定
        GameObject obj = GameObject.Find("Canvas/UI_Skills");
        if (obj == null)
        {
            Debug.Log("cannot find skill view,init error");
        }
        skillData = Facade.RetrieveProxy(SkilldataProxy.NAME) as SkilldataProxy;
        view = ((GameObject)ViewComponent).GetComponent<SkillPanel>();
        Debug.Log("Skill panel mediator");

       

        //view.BtnIce.onClick.AddListener(OnClickIce);
        //view.BtnFire.onClick.AddListener(OnClickFire);
        //view.BtnSG.onClick.AddListener(OnClickSG);
    }
    //
    //public void OnClickIce()
    //{
    //    DiamondEvent = (DiamondEvent)1;
    //    Debug.Log("MVCIce" + skillData.SkillData.IceCost);
    //    SendNotification(ApplicationFacade.ALTER_DIAMOND,new AlterDiamondCommand.Data(DiamondEvent,skillData.SkillData.IceCost));
    //}
    //public void OnClickFire()
    //{
    //    DiamondEvent = (DiamondEvent)2;
    //    Debug.Log("MVCFire"+skillData.SkillData.FireCost);
    //    SendNotification(ApplicationFacade.ALTER_DIAMOND, new AlterDiamondCommand.Data(DiamondEvent, skillData.SkillData.FireCost));
    //}
    //public void OnClickSG()
    //{
    //    DiamondEvent = (DiamondEvent)3;
    //    Debug.Log("MVCSG" + skillData.SkillData.SGCost);
    //    SendNotification(ApplicationFacade.ALTER_DIAMOND, new AlterDiamondCommand.Data(DiamondEvent, skillData.SkillData.SGCost));
    //}
}
