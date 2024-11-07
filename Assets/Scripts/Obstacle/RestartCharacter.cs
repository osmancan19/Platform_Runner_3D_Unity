using UnityEngine;

public class RestartCharacter : MonoBehaviour, IObstacle
{
    private void OnCollisionEnter(Collision collision)
    {
        ICharacter character = collision.gameObject.GetComponent<ICharacter>();
  
        if (character != null)
        {
            OnHit(character);
        }
    }

    public void OnHit(ICharacter character)
    {
        if (character.GetCharacterType() == CharacterType.Player)
        {
            UIFailCountManager.instance.IncreaseFailCount();
            SoundManager.instance.PlayFailSound();
        }
        character.ResetPosition(); // Çarpan karakteri sıfırlar
    }
}
