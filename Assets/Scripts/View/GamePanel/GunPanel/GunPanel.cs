using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunPanel : MonoBehaviour
{
    public Text CoinNumber;//金币显示
    public Text DiamondNumber;//钻石显示

    private void Start()
    {
        CoinNumber = transform.Find("CoinTable/Text").GetComponent<Text>();
        DiamondNumber = transform.Find("DiamondTable/Text").GetComponent<Text>();

        PlayerdataProxy playerdataProxy = ApplicationFacade.Instance.RetrieveProxy(PlayerdataProxy.NAME) as PlayerdataProxy;
        PlayerdataModel playerData = playerdataProxy.GetPlayerData;
        CoinNumber.text = playerData.CoinNum.ToString();
        DiamondNumber.text = playerData.DiamondNum.ToString();
    }    
   
}
