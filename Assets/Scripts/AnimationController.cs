using UnityEngine;
using Spine.Unity;
using System.Threading.Tasks;

public class AnimationController : PlayerModule
{
    [SerializeField] SkeletonAnimation skeletonAnimation;

    public async void PlayAnimation(string ani, int timeToCancle = 0)
    {
        if (skeletonAnimation.AnimationName != AnimationKey.IDLE_ANIMATION) return;
        skeletonAnimation.AnimationState.SetAnimation(0, ani, true);

        if (timeToCancle == 0) return;
        await Task.Delay(timeToCancle);
        skeletonAnimation.AnimationState.SetAnimation(0, AnimationKey.IDLE_ANIMATION, true);
    }

    public void PlayOverrideAnimation(string ani)
    {
        skeletonAnimation.AnimationState.SetAnimation(0, ani, true);
    }
}

public static class AnimationKey
{
    public const string IDLE_ANIMATION = "Idle Friendly 1";
    public const string HEAD_HIT_ANIMATION = "Moody Friendly";
    public const string BODY_HIT_ANIMATION = "Idle UnFriendly 2";
    public const string MISS_ANIMATION = "Happy Friendly";
    public const string WIN_ANIMATION = "Cheer Friendly";
    public const string LOSE_ANIMATION = "Moody UnFriendly";
}
