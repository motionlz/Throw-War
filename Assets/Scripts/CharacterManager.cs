using UnityEngine;

[RequireComponent(typeof(AnimationController), typeof(HitBoxManager), typeof(LifeManager))]
public abstract class CharacterManager : MonoBehaviour
{
    public AnimationController animationController { get; private set; }
    public HitBoxManager hitBoxManager { get; private set; }
    public LifeManager lifeManager { get; private set; }

    protected virtual void Setup() 
    {
        if (animationController == null)
            animationController = GetComponent<AnimationController>();
        if (hitBoxManager == null)
            hitBoxManager = GetComponent<HitBoxManager>();
        if (lifeManager == null)
            lifeManager = GetComponent<LifeManager>();
    }

    protected virtual void Init()
    {

    }
}
