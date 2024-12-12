using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1_Manager : MonoBehaviour
{
    public static Level1_Manager instance;

    public GameObject playerObject;
    public GameObject afterExamPanel;
    public GameObject dialogueCamera;
    public GameObject studentsObject;
    public GameObject testPapersObject;

    public int testPapersGiven;

    protected virtual void Awake()
    {
        instance = this;
    }

    protected virtual void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        studentsObject = GameObject.Find("Students");
        testPapersObject = GameObject.Find("Test Papers");

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        StartCoroutine(FirstDialogues());
    }

    public virtual IEnumerator FirstDialogues()
    {
        SoundManager.instance.PlayMultipleDialogueWithFreeze(0,6);
        playerObject.GetComponent<PlayerController>().playerState = PlayerController.PlayerState.Talking;
        dialogueCamera.SetActive(true);

        while(SoundManager.instance.dialoguePlayer.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }
        
        HUDController.instance.objectiveDisplayObject.SetActive(true);
        playerObject.GetComponent<PlayerController>().playerState = PlayerController.PlayerState.Idle;
        dialogueCamera.SetActive(false);
    }
    
    public virtual void AfterWhileFunction()
    {
        StartCoroutine(AfterWhile());
    }

    public virtual IEnumerator AfterWhile()
    {
        GameManager.instance.gameState = GameManager.GameState.Interaction;
        HUDController.instance.objectiveDisplayObject.SetActive(false);
        yield return new WaitForSeconds(2f);

        afterExamPanel.SetActive(true);
        yield return new WaitForSeconds(2f);

        playerObject.transform.position = new Vector3(-9.8f, -4.5f, 65.5f);
        playerObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        dialogueCamera.SetActive(true);
        yield return new WaitForSeconds(2f);

        SoundManager.instance.PlayMultipleDialogueWithFreeze(7,10);
        playerObject.GetComponent<PlayerController>().playerState = PlayerController.PlayerState.Talking;

        while(SoundManager.instance.dialoguePlayer.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }

        studentsObject.SetActive(false);
        testPapersObject.SetActive(false);

        dialogueCamera.SetActive(false);
        GameManager.instance.gameState = GameManager.GameState.Gameplay;
        playerObject.GetComponent<PlayerController>().playerState = PlayerController.PlayerState.Idle;
        yield return new WaitForSeconds(2f);
        
        HUDController.instance.NewLevel();
    }
}
