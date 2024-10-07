using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerManager : CharacterManager
{
    public PlayerController playerController { get; private set; }

    private async void Awake() 
    {
        Setup();
        await Task.Delay(5);
        Init();
    }
    protected override void Setup()
    {
        base.Setup();
        if (playerController == null)
            playerController = GetComponent<PlayerController>();
    }
    protected override void Init()
    {
        animationController.AssignTo(this);

        hitBoxManager.AssignTo(this);

        lifeManager.AssignTo(this);
        lifeManager.SetLife(GlobalKey.PLAYER_HP);

        playerController.AssignTo(this);
    }
}
