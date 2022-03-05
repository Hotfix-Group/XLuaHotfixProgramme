using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Interfaces;

public class SkillIceCommand : PureMVC.Patterns.SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
    }
}
