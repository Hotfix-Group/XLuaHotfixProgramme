using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Interfaces;

public class ChestPanelMediator : PureMVC.Patterns.Mediator
{
    public const string ChestPanelMediatorName = "ChestPanelMediator";

    private ChestPanel view;

    public ChestPanelMediator(object viewComponent) : base(NAME, viewComponent)
    {
        GameObject chestPanel = GameObject.Find("Canvas/UI_ChestPanel");
        if(chestPanel == null)
        {
            Debug.Log("cannot find chest view,init error");
        }
        view = ((GameObject)ViewComponent).GetComponent<ChestPanel>();

        //view.BtnChest.onClick.AddListener();

        Debug.Log("Chest panel mediator");
    }

    //public void OnClickChest()
    //{
    //    Debug.Log("Open chest");
    //    SendNotification(ApplicationFacade.ALTER_COIN)
    //}
}
