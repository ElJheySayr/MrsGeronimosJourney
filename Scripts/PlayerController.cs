using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    private Rigidbody rb;
    private Animator animator;
    private Camera playerCamera;

    [Header("Attributes")]
    public float speed = 25f;

    [Header("Camera")]
    public float lookSpeed = 1f;
    private const float lookXLimit = 45f;  
    private float rotationX = 0;

    [Header("Sounds")]
    public AudioSource footstepAudio;

    [Header("General Conditions")]
    public bool canMove = true;
    public PlayerState playerState;  
    private Vector3 moveDirection  = Vector3.zero;
    private bool isMoving;

    [Header("Interaction Components")]
    public float playerReach = 12f;
    private Interactable currentInteractable;
    private KeyCode interactKey = KeyCode.F;

    protected virtual void Awake()
    {
        instance = this;    
    }

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerCamera = Camera.main;
    }

    protected virtual void FixedUpdate()
    {
        if(!CanMoveInCurrentState()) return;
        PlayerMovement();   
    }

    protected virtual void Update()
    {
        UpdateAnimations();  
        FootstepHandler();    
        CheckInteractionFunction();
    }

    protected virtual void LateUpdate()
    {
        if(!CanMoveInCurrentState()) return;
        HandleCameraRotation();
    }

    public enum PlayerState
    {
        Idle,
        Walking,
        Talking
    }

    private bool CanMoveInCurrentState()
    {
        return canMove &&
            (GameManager.instance.gameState == GameManager.GameState.Gameplay);
    }

    private void UpdateAnimations()
    {
        animator.SetBool("isIdle", playerState == PlayerState.Idle);
        animator.SetBool("isWalking", playerState == PlayerState.Walking);
        animator.SetBool("isTalking", playerState == PlayerState.Talking);
    }

    private void PlayerMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        playerState = moveDirection.magnitude > 0 ? PlayerState.Walking : PlayerState.Idle;

        moveDirection = new Vector3(horizontal, 0, vertical).normalized;
        isMoving = moveDirection.magnitude > 0;

        Vector3 move = moveDirection * speed * Time.deltaTime;
        transform.Translate(move, Space.Self);
    }

    private void FootstepHandler()
    {
        if (isMoving && GameManager.instance.gameState == GameManager.GameState.Gameplay)
        {
            footstepAudio.enabled = true;
        }
        else
        {
            footstepAudio.enabled = false;
        }
    }
    private void HandleCameraRotation()
    {
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }

    private void CheckInteractionFunction()
    {
        CheckInteraction();

        if (Input.GetKeyDown(interactKey) && currentInteractable != null)
        {
            currentInteractable.Interaction();
        }
    }

    private void CheckInteraction()
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if(Physics.Raycast(ray, out hit, playerReach) && CanMoveInCurrentState())
        {
            if(hit.collider.tag == "Interactable")
            {
                Interactable newInteractable = hit.collider.GetComponent<Interactable>();

                if (currentInteractable && newInteractable != currentInteractable)
                {
                    currentInteractable.DisableOutline();
                }
                if (newInteractable.enabled)
                {
                    SetNewCurrentInteractable(newInteractable);
                }
                else
                {
                    DisableCurrentInteractable();
                }
            }
            else
            {
                DisableCurrentInteractable();
            }
        }
        else
        {
            DisableCurrentInteractable();
        }
    }

    private void SetNewCurrentInteractable(Interactable newInteractable)
    {
        currentInteractable = newInteractable;
        currentInteractable.EnableOutline();
        HUDController.instance.EnableInteractionText(currentInteractable.message);
    }

    private void DisableCurrentInteractable()
    {
        HUDController.instance.DisableInteractionText();
        if (currentInteractable)
        {
            currentInteractable.DisableOutline();
            currentInteractable = null;
        }
    }
}