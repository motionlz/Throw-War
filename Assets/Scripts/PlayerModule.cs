using UnityEngine;

public abstract class PlayerModule : MonoBehaviour
{
    protected CharacterManager characterManager { get; private set; }

    public void AssignTo(CharacterManager target) 
    {
        characterManager = target;
    }
}
