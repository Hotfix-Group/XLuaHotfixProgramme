using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XLua;
/// <summary>
/// 冰冻
/// </summary>
[LuaCallCSharp]
[Hotfix]
public class Ice : MonoBehaviour
{


    private float timeVal = 10;
    private bool canUse = true;

    public Slider cdSlider;
    private float totalTime = 10;
    Button but;
    private AudioSource fireAudio;
    private int reduceDiamands;

    private DiamondEvent DiamondEvent = DiamondEvent.Ice;

    private SkilldataProxy skillData = ApplicationFacade.Instance.RetrieveProxy(SkilldataProxy.NAME) as SkilldataProxy;
    private PlayerdataProxy playerData = ApplicationFacade.Instance.RetrieveProxy(PlayerdataProxy.NAME) as PlayerdataProxy;
    // Use this for initialization
    private void Awake()
    {
        but = transform.GetComponent<Button>();
        but.onClick.AddListener(ice);
        fireAudio = GetComponent<AudioSource>();
    }

    void Start()
    {
        reduceDiamands = 10;
    }

    private void Update()
    {
        if (timeVal >= 10)
        {
            timeVal = 10;
        }
        cdSlider.value = timeVal / totalTime;
        if (timeVal >= 10)
        {
            cdSlider.transform.Find("Background").gameObject.SetActive(false);
            canUse = true;
        }
        else
        {

            timeVal += Time.deltaTime;
        }
    }

    private void ice()
    {
        //必杀的方法
        if (canUse)
        {
            if (!Gun.Instance.Fire && !Gun.Instance.Ice)
            {

                //if (Gun.Instance.diamands <= reduceDiamands)
                //{
                //    return;
                //}
                if (playerData.PlayerData.DiamondNum <= reduceDiamands)
                {
                    return;
                }
                if (fireAudio.isPlaying)
                {
                    return;
                }
                fireAudio.Play();
                ApplicationFacade.Instance.SendNotification(ApplicationFacade.ALTER_DIAMOND, new AlterDiamondCommand.Data(DiamondEvent, skillData.SkillData.IceCost));
                //Gun.Instance.DiamandsChange(-reduceDiamands);
                Gun.Instance.Ice = true;
                canUse = false;
                cdSlider.transform.Find("Background").gameObject.SetActive(true);
                timeVal = 0;
                Invoke("CloseIce", 4);
            }
        }

    }

    //关闭必杀的方法
    private void CloseIce()
    {

        Gun.Instance.Ice = false;
    }
}
