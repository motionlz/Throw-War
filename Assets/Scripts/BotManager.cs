using System;
using System.Threading.Tasks;
using UnityEngine;

public class BotManager : CharacterManager
{
    
    protected override void Awake() 
    {
        base.Awake();
        Init();
    }
    protected override void Setup()
    {
        base.Setup();
    }

    protected override void Init()
    {
        animationController.AssignTo(this);
        hitBoxManager.AssignTo(this);
        lifeManager.AssignTo(this);
    }
}
