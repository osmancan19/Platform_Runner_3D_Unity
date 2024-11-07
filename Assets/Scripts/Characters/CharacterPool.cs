using System.Collections.Generic;
using UnityEngine;

public class CharacterPool : MonoBehaviour
{
    public static CharacterPool instance;
    public List<CharacterMovement> characterMovements;
    void Awake()
    {
        instance = this;
    }
    public void EnableMovement()
    {
        foreach (CharacterMovement characterMovement in characterMovements)
        {
            characterMovement.EnableMovement();
        }
    }
    public void disableMovement()
    {
        foreach (CharacterMovement characterMovement in characterMovements)
        {
            characterMovement.DisableMovement();
        }
    }
    public void GameOver()
    {
        for(int i = 1; i <=9 ;i++)
        {
            Destroy(characterMovements[i].gameObject); 
        }
        characterMovements.Clear();
    }
}
