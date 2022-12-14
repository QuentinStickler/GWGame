using System;
using System.Collections.Generic;
using cakeslice;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Vector2 move;
    private Vector3 direction;
    private CharacterController controller;
    private Vector3 gravity;
    public bool canMove;
    public bool isInGame;
    private PlayerMovement playerMovement;
    private InputAction hideUi;
    private InputAction scan;

    public GameObject ui;

    private void Awake()
    {
        playerMovement = new PlayerMovement();
    }

    private void OnEnable()
    {
        hideUi = playerMovement.Specials.HideUi;
        hideUi.Enable();
        hideUi.performed += HideUi;
        
        scan = playerMovement.Specials.Scan;
        scan.Enable();
        scan.performed += Scan;
    }

    private void OnDisable()
    {
        hideUi.Disable();
        hideUi.performed -= HideUi;
        
        scan.Disable();
        scan.performed -= Scan;
    }

    private void HideUi(InputAction.CallbackContext context)
    {
        ui.SetActive(!ui.activeSelf);
    }

    private void Scan(InputAction.CallbackContext context)
    {
        Debug.Log("Scanning");
    }
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        GameEvents.OnInteractingWithMiniGame += DisableMovement;
        canMove = true;
        isInGame = false;
    }

    public void DisableMovement(bool isPlaying)
    {
        canMove = isPlaying;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
        Vector3 convert = new Vector3(move.x, 0, move.y);
        direction = IsoVectorConvert(convert);
    }

    private void Update()
    {
        if(canMove)
            Move();
        ApplyGravity();
    }

    private void LateUpdate()
    {
        if(!isInGame)
            LookForInteractableObjects();
        else if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            isInGame = false;
            canMove = true;
            GameEvents.OnStopInteractingWithMiniGame?.Invoke();
        }
    }

    public void LookForInteractableObjects()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 6f, LayerMask.GetMask("Interactable"));
        if (colliders.Length <= 0) return;
        colliders[0].gameObject.GetComponent<Outline>().eraseRenderer = false;
        if (!Keyboard.current.eKey.wasPressedThisFrame) return;
        colliders[0].gameObject.GetComponent<IInteractable>().Interact();
        isInGame = true;
    }
    private void Move()
    {
        controller.Move(direction * speed * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        gravity.y += -10f * Time.deltaTime;
        controller.Move(gravity * Time.deltaTime);
    }

    private Vector3 IsoVectorConvert(Vector3 vector)
    {
        Quaternion rotation = Quaternion.Euler(0, 45, 0);
        Matrix4x4 isoMatrix = Matrix4x4.Rotate(rotation);
        Vector3 result = isoMatrix.MultiplyPoint3x4(vector);
        return result;
    }
    
    
}