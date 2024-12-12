using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractItem : MonoBehaviour
{
    public virtual void TestPapers()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        Level1_Manager.instance.testPapersGiven++;
        gameObject.GetComponent<Interactable>().enabled = false;
        gameObject.GetComponent<Outline>().enabled = false;

        if(!SoundManager.instance.dialoguePlayer.isPlaying)
        {
            SoundManager.instance.PlaySFX(0);
        }

        if(Level1_Manager.instance.testPapersGiven >= 12)
        {
            Level1_Manager.instance.AfterWhileFunction();
        }
    }
}
