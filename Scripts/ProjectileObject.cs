using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileObject : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {     
        if(collision.gameObject.GetComponent<AiController>() != null)
        {
            if(collision.gameObject.GetComponent<AiController>().aiState == AiController.AiState.SittingTalking)
            {
                collision.gameObject.GetComponent<AiController>().aiState = AiController.AiState.SittingDisbelief;
                
                if(!SoundManager.instance.dialoguePlayer.isPlaying)
                {
                    SoundManager.instance.Say(24);
                }
                
                Level2_Manager.instance.PickPlayer();          
            }
   
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }        
    }
}
