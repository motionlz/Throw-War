using UnityEngine;

public abstract class PlayerModule : MonoBehaviour
{
    protected CharacterManager playerManager { get; private set; }

    public void AssignTo(CharacterManager target) 
    {
        playerManager = target;
    }
}
