using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    public CharacterMovement characterMovement; // PlayerMovement referansı

    public void OnStandUpComplete()
    {
        if (characterMovement != null)
        {
            characterMovement.OnStandUpComplete();
        }
    }
}