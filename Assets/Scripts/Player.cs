using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private float f_speed = 5f;
    [SerializeField] private float f_speedRotation = 5f;
    [SerializeField] private float f_playerRadius = 1f;
    [SerializeField] private float f_playerHeight = 1f;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private float interactDistance = 3f;
    [SerializeField] private LayerMask counterLayerMask;


    [SerializeField] private Transform kitchenObjectHoldPoint;
    private KitchenObject kitchenObject;


    public static Player Instance { get; private set; }

    public event EventHandler OnPickedSomething;
    public event EventHandler <OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCouner;
    }

    private BaseCounter selectedCounter;
    private Vector3 lastInteraction;
    private bool isWalking = false;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one player instance");
        }
        Instance = this;
    }
    private void Start()
    {
        playerInput.OnInteractAction += PlayerInput_OnInteractAction;
        playerInput.OnInteractAlternateAction += PlayerInput_OnInteractAlternateAction;
    }

    private void PlayerInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void PlayerInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;
        if (selectedCounter != null) {
            selectedCounter.Interact(this);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        HandleMovement();
        HandleInteraction();
    }
    public bool IsWalking()
    {
        return isWalking;
    }

    public void HandleInteraction()
    {
        Vector3 inputVector = playerInput.GetMovementVectorNormalized();
        Vector3 movDir = new Vector3(inputVector.x, 0, inputVector.y).normalized;

        if (movDir != Vector3.zero)
        {
            lastInteraction = movDir;
        }
        Debug.DrawRay(transform.position, lastInteraction * interactDistance, Color.red);
        if (Physics.Raycast(transform.position, lastInteraction, out RaycastHit hitInfo, interactDistance, counterLayerMask)) // counterLayerMask
        {
            Debug.DrawRay(transform.position, lastInteraction * interactDistance, Color.green);
            Debug.Log(hitInfo.transform);
            if (hitInfo.transform.TryGetComponent<BaseCounter>(out BaseCounter baseCounter))
            {
                if (baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }
    public void HandleMovement()
    {
        Vector3 inputVector = playerInput.GetMovementVectorNormalized();
        Vector3 movDir = new Vector3(inputVector.x, 0, inputVector.y).normalized;

        float movDistance = f_speed * Time.deltaTime;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * f_playerHeight, f_playerRadius, movDir, movDistance);
        if (!canMove)
        {
            Vector3 movDirX = new Vector3(movDir.x, 0, 0).normalized;
            canMove = (movDir.x < -.5f || movDir.x > +.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * f_playerHeight, f_playerRadius, movDirX, movDistance);
            if (canMove)
            {
                movDir = movDirX;
            }
            else
            {
                Vector3 movDirZ = new Vector3(0, 0, movDir.z).normalized;
                canMove = (movDir.z < -.5f || movDir.z > +.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * f_playerHeight, f_playerRadius, movDirZ, movDistance);
                if (canMove)
                {
                    movDir = movDirZ;
                }
            }
        }
        if (canMove)
        {
            transform.position += movDir.normalized * movDistance;
        }
        if (movDir == Vector3.zero)
        {
            isWalking = false;
        }
        else
        {
            isWalking = true;
        }

        transform.forward = Vector3.Slerp(transform.forward, movDir, Time.deltaTime * f_speedRotation);
    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCouner = selectedCounter,
        });
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if (kitchenObject != null)
        {
            OnPickedSomething?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject() { return kitchenObject; }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
