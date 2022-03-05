using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitUI : MonoBehaviour
{
    // 切换场景完毕后立刻初始化UI，并注册Mediator
    private void Awake()
    {
        LuaManager.GetInstance().GameStart();

        ApplicationFacade.GetInstance().InitGame();
    }
}
