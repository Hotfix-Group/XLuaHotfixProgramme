using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VersionProxy : PureMVC.Patterns.Proxy
{
    public new static string NAME = "VersionData";

    public VersionModel VersionData;

    public override void OnRegister()
    {
        Debug.Log("VersionDataProxy OnRegister");
    }
    public VersionProxy(string name) : base(name, null)
    {
        VersionData = new VersionModel();
    }

    /// <summary>
    /// Called by the Model when the Proxy is removed
    /// </summary>
    public override void OnRemove()
    {
        Debug.Log("VersionDataProxy OnRemove");
    }
}
