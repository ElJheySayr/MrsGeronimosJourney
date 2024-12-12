using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiController : MonoBehaviour
{
    public AiState aiState;
    private Animator animator;
    private float timeValue = 5f;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        UpdateAnimations();

        if(aiState == AiState.SittingTalking && Level2_Manager.instance.gameOn)
        {
            timeValue -= Time.deltaTime;

            if(timeValue <= 0)
            {
                Level2_Manager.instance.playerLife--; 
                Level2_Manager.instance.PickPlayer();    
                timeValue = 5f;
                SoundManager.instance.PlaySFX(0);
                aiState = AiState.Typing;
            }
        }
    }

    public enum AiState
    {
        SittingIdle,
        SittingTalking,
        SittingDisbelief,
        Typing,
        Writing
    }

    public virtual void UpdateAnimations()
    {
        animator.SetBool("isIdle", aiState == AiState.SittingIdle);
        animator.SetBool("isSittingTalking", aiState == AiState.SittingTalking);
        animator.SetBool("isSittingDisbelief", aiState == AiState.SittingDisbelief);
        animator.SetBool("isTyping", aiState == AiState.Typing);
        animator.SetBool("isWriting", aiState == AiState.Writing);
    }
}
