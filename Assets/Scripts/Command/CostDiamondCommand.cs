using PureMVC.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CostDiamondCommand : PureMVC.Patterns.SimpleCommand
{
    //技能消耗的数据，对应技能编号枚举以及技能消耗
    public class Data
    {
        public SkilldataProxy.SkillType skillType = SkilldataProxy.SkillType.None;
        public int costDiamondNum = 0;
        public Data(SkilldataProxy.SkillType tempSkillType,int tempCostDiamondNum)
        {
            skillType = tempSkillType;
            costDiamondNum = tempCostDiamondNum;
        }
    }
    //收到释放技能的通知后，发送消耗钻石的通知
    public override void Execute(INotification notification)
    {
        base.Execute(notification);

        Data data = notification.Body as Data;
        if(data == null)
        {
            return;
        }

        SkilldataProxy skilldataProxy = (SkilldataProxy)ApplicationFacade.Instance.RetrieveProxy(SkilldataProxy.NAME);
        skilldataProxy.CostDiamond(data.skillType, data.costDiamondNum);
        SkilldataModel SkillData = skilldataProxy.GetSkillData;
        switch (data.skillType)
        {
           // case SkilldataProxy.SkillType.Ice:
             //   ApplicationFacade.Instance.SendNotification(notification.)
        }
    }

}
