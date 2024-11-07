public enum CharacterType
{
    Player,
    AI
}
public interface ICharacter
{
    CharacterType GetCharacterType();
    void ResetPosition();
    void SetRunningAnimation(bool isRunning);
    void SetFallingAnimation(bool isFell);
    void OnStandUpComplete();
    void CheckFalling();
    void DisableMovement();
    void EnableMovement();
}
