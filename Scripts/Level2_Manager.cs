using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level2_Manager : MonoBehaviour
{
    public static Level2_Manager instance;

    public GameObject playerObject;
    public GameObject dialogueCamera;
    public GameObject laterThatDayPanel;
    public TMP_Text timerText;
    public TMP_Text lifeText;
    public GameObject gameOverPanel;
    public GameObject endPanel;
    public GameObject studentsGameObject;

    public List<GameObject> studentsObject;

    public bool gameOn;

    public float timeValue = 120f;
    public int playerLife = 3;

    protected virtual void Awake()
    {
        instance = this;
    }

    protected virtual void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        studentsGameObject = GameObject.Find("Students");

        StartCoroutine(FirstDialogues());
    }

    protected virtual void Update()
    {
        if(timeValue >= 0 && gameOn)
        {
            timeValue -= Time.deltaTime;
        }
        else if(timeValue <= 0 && gameOn)
        {
            StartCoroutine(SecondDialogues());
        }

        int minutes = Mathf.FloorToInt(timeValue / 60);
        int seconds = Mathf.FloorToInt(timeValue - minutes * 60);
        
        string timeString = string.Format("{0:0}:{1:00}", minutes, seconds);
        timerText.text = timeString;

        if(playerLife <= 0 && gameOn)
        {
            GameOver();
        }

        string lifeString = playerLife.ToString();
        lifeText.text = "Remaining Life: " + lifeString;
    }

    public virtual IEnumerator FirstDialogues()
    {
        laterThatDayPanel.SetActive(true);
        GameManager.instance.gameState = GameManager.GameState.Interaction;
        yield return new WaitForSeconds(5f);

        dialogueCamera.SetActive(true);
        SoundManager.instance.PlayMultipleDialogueWithFreeze(11,19);
        playerObject.GetComponent<PlayerController>().playerState = PlayerController.PlayerState.Talking;

        while(SoundManager.instance.dialoguePlayer.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }

        laterThatDayPanel.SetActive(false);
        dialogueCamera.SetActive(false);
        playerObject.GetComponent<PlayerController>().playerState = PlayerController.PlayerState.Idle;
        HUDController.instance.objectiveDisplayObject.SetActive(true);
        timerText.gameObject.SetActive(true);
        lifeText.gameObject.SetActive(true);
        gameOn = true;

        PickPlayer();
    }

    public virtual IEnumerator SecondDialogues()
    {
        gameOn = false;
        HUDController.instance.objectiveDisplayObject.SetActive(false);
        timerText.gameObject.SetActive(false);
        lifeText.gameObject.SetActive(false);

        GameManager.instance.gameState = GameManager.GameState.Interaction;
        yield return new WaitForSeconds(1f);

        playerObject.transform.position = new Vector3(-9.8f, -4.5f, 65.5f);
        playerObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        dialogueCamera.SetActive(true);

        SoundManager.instance.PlayMultipleDialogueWithFreeze(20, 23);
        playerObject.GetComponent<PlayerController>().playerState = PlayerController.PlayerState.Talking;

        while(SoundManager.instance.dialoguePlayer.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }

        dialogueCamera.SetActive(false);
        studentsGameObject.SetActive(false);
        playerObject.GetComponent<PlayerController>().playerState = PlayerController.PlayerState.Idle;
        yield return new WaitForSeconds(2f);


        GameManager.instance.gameState = GameManager.GameState.Interaction;
        endPanel.SetActive(true);
        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene("MainMenu");
    }

    public virtual void PickPlayer()
    {
        StartCoroutine(randomPlayer());
    }

    public virtual IEnumerator randomPlayer()
    {
        int RandomIndex = Random.Range(0, 8);
        yield return new WaitForSeconds(10f);

        studentsObject[RandomIndex].GetComponent<AiController>().aiState = AiController.AiState.SittingTalking; 
    }

    public virtual void GameOver()
    {
        GameManager.instance.gameState = GameManager.GameState.Gameover;
        Time.timeScale = 0f;    
        timerText.gameObject.SetActive(false);     
        gameOverPanel.SetActive(true); 
    }
}
