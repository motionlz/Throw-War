using System.Threading.Tasks;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private async void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag == GlobalTag.THROW_OBJECT)
        {
            await Task.Delay(100);
            other.gameObject.SetActive(false);
            GameManager.Instance.actionToNextQueue((next) => 
            {
                next.animationController.PlayAnimation(AnimationKey.MISS_ANIMATION, 1500);
            });
        }
    }
}
