using UnityEngine;

public class CharacterMovement : MonoBehaviour, ICharacter
{
    protected float speed;
    protected float rotationSpeed; 
    protected float fallThreshold;
    protected Animator animator;
    protected bool isFell;
    protected bool canMove;
    protected CharacterType characterType;
    protected Rigidbody rb;
    protected bool hasFinished = false;
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        isFell = false;
        canMove = false;
        fallThreshold = -1.45f;
        speed = 9f;
        rotationSpeed = 720f;
    }

    public void SetRunningAnimation(bool isRunning)
    {
        animator.SetBool("IsRunning", isRunning);
    }
    public void SetFallingAnimation(bool isFell)
    {
        animator.SetBool("IsFell", isFell);
    }
    public void DisableMovement()
    {
        canMove = false;
    }
    public void EnableMovement()
    {
        canMove = true;
    }

    public float GetSpeed()
    {
        return speed;
    }
    public float GetRotationSpeed()
    {
        return rotationSpeed;
    }

    public CharacterType GetCharacterType()
    {
        return characterType;
    }
    public bool GetHasFinished()
    {
        return hasFinished;
    }
    public void SetHasFinished( bool hasFinished )
    {
        this.hasFinished = hasFinished;
    }
    
    public virtual  void ResetPosition(){}
    public virtual  void OnStandUpComplete(){}
    public virtual  void CheckFalling(){}
}
