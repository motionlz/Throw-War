using System;
using UnityEngine;

[RequireComponent(typeof(AnimationController), typeof(HitBoxManager), typeof(LifeManager))]
public abstract class CharacterManager : MonoBehaviour
{
    public AnimationController animationController { get; private set; }
    public HitBoxManager hitBoxManager { get; private set; }
    public LifeManager lifeManager { get; private set; }
    public PowerUpManager powerUpManager{ get; private set; }
    public event Action<LifeManager, PowerUpManager> OnInit;

    protected virtual void Setup() 
    {
        if (animationController == null)
            animationController = GetComponent<AnimationController>();
        if (hitBoxManager == null)
            hitBoxManager = GetComponent<HitBoxManager>();
        if (lifeManager == null)
            lifeManager = GetComponent<LifeManager>();
        if (powerUpManager == null)
            powerUpManager = GetComponent<PowerUpManager>();
    }

    protected virtual void Init()
    {

    }
    protected virtual void Awake()
    {
        Setup();
        OnInit?.Invoke(lifeManager, powerUpManager);
    }
}
