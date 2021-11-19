using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APP : MonoBehaviour
{
    private void Awake()
    {
        ApplicationFacade.GetInstance().Launch();
    }
}
