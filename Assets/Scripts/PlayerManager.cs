using System;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerManager : CharacterManager
{
    public PlayerController playerController { get; private set; }

    protected override void Awake() 
    {
        base.Awake();
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

        powerUpManager.AssignTo(this);
    }
}
