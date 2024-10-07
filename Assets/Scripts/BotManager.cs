using System.Threading.Tasks;
using UnityEngine;

public class BotManager : CharacterManager
{
    private async void Awake() 
    {
        Setup();
        await Task.Delay(5);
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
