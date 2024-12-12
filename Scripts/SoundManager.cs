using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource dialoguePlayer;  

    public DialogueLine[] dialogue;              
    public AudioClip[] sfx;

    protected virtual void Awake()
    {
        instance = this;
    }

    protected virtual void Start()
    {
        dialoguePlayer = Camera.main.gameObject.GetComponent<AudioSource>();
    }

    public virtual void Say(int dialogueIndex)
    {
        StartCoroutine(Play(dialogue[dialogueIndex]));
    }

    public virtual void SayWithFreeze(int dialogueIndex)
    {
        StartCoroutine(PlayWithFreeze(dialogue[dialogueIndex]));
    }

    public virtual void PlayMultipleDialogue(int startIndex, int endIndex)
    {
        StartCoroutine(PlayOneByOne(startIndex, endIndex, 0));
    }

    public virtual void PlayMultipleDialogueWithFreeze(int startIndex, int endIndex)
    {
        StartCoroutine(PlayOneByOne(startIndex, endIndex, 2));
    }

    public virtual void PlaySFX(int sfxIndex)
    {
        StartCoroutine(PlayOneSFX(sfx[sfxIndex]));
    }

    public virtual void PlaySFXWithFreeze(int sfxIndex)
    {
        StartCoroutine(PlaySFXWithFreeze(sfx[sfxIndex]));
    }

    public virtual void PlayMultipleSFX(int startIndex, int endIndex)
    {
        StartCoroutine(PlayOneByOne(startIndex, endIndex, 1));
    }

    public virtual void PlayMultipleSFXWithFreeze(int startIndex, int endIndex)
    {
        StartCoroutine(PlayOneByOne(startIndex, endIndex, 3));
    }

    public virtual IEnumerator PlayOneSFX(AudioClip sfx)
    {
        dialoguePlayer.PlayOneShot(sfx);
        yield return new WaitForSeconds(sfx.length);
    }

    public virtual IEnumerator Play(DialogueLine dialogue)
    {
        HUDController.instance.SetSubtitle(dialogue.subtitle, dialogue.clip.length);
        dialoguePlayer.PlayOneShot(dialogue.clip);

        while (dialoguePlayer.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }
    }

    public virtual IEnumerator PlayWithFreeze(DialogueLine dialogue)
    {
        GameManager.instance.gameState = GameManager.GameState.Interaction;
        HUDController.instance.SetSubtitle(dialogue.subtitle, dialogue.clip.length);
        dialoguePlayer.PlayOneShot(dialogue.clip);

        while (dialoguePlayer.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }

        GameManager.instance.gameState = GameManager.GameState.Gameplay;
    }

    public virtual IEnumerator PlaySFXWithFreeze(AudioClip sfx)
    {
        GameManager.instance.gameState = GameManager.GameState.Interaction;
        dialoguePlayer.PlayOneShot(sfx);
        
        while (dialoguePlayer.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }

        GameManager.instance.gameState = GameManager.GameState.Gameplay;
    }

    public virtual IEnumerator PlayOneByOne(int startIndex, int endIndex, int setNum)
    {
        if (setNum == 0)
        {
            for (int i = startIndex; i <= endIndex; i++)
            {
                dialoguePlayer.clip = dialogue[i].clip;
                dialoguePlayer.Play();
                HUDController.instance.SetSubtitle(dialogue[i].subtitle, dialogue[i].clip.length);

                while (dialoguePlayer.isPlaying)
                {
                    yield return new WaitForEndOfFrame();
                }
            }
        }
        else if (setNum == 1)
        {
            for (int i = startIndex; i <= endIndex; i++)
            {
                dialoguePlayer.clip = sfx[i];
                dialoguePlayer.Play();

                while (dialoguePlayer.isPlaying)
                {
                    yield return new WaitForEndOfFrame();
                }
            }
        }
        else if (setNum == 2)
        {
            for (int i = startIndex; i <= endIndex; i++)
            {
                dialoguePlayer.clip = dialogue[i].clip;
                dialoguePlayer.Play();
                HUDController.instance.SetSubtitle(dialogue[i].subtitle, dialogue[i].clip.length);
                
                GameManager.instance.gameState = GameManager.GameState.Interaction;

                while (dialoguePlayer.isPlaying)
                {
                    yield return new WaitForEndOfFrame();
                }

                GameManager.instance.gameState = GameManager.GameState.Gameplay;
            }
        }
        else if (setNum == 3)
        {
            for (int i = startIndex; i <= endIndex; i++)
            {
                dialoguePlayer.clip = sfx[i];
                dialoguePlayer.Play();

                GameManager.instance.gameState = GameManager.GameState.Interaction;

                while (dialoguePlayer.isPlaying)
                {
                    yield return new WaitForEndOfFrame();
                }

                GameManager.instance.gameState = GameManager.GameState.Gameplay;
            }
        }
    }
}
