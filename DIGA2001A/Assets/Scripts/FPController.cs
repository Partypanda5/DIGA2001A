using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class FPController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;

    [Header("Look Settings")]
    public Transform cameraTransform;
    public float lookSensitivity = 2f;
    public float verticalLookLimit = 90f;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform gunPoint;

    [Header("Crouch Settings")]
    public float crouchHeight = 1f;
    public float standHeight = 2f;
    public float crouchSpeed = 2.5f;
    private float originalMoveSpeed;

    [Header("Pickup Settings")]
    public float pickupRange = 3f;
    public Transform holdPoint;
    private PickUpObject heldObject;
    public TMP_Text pickupText;

    [Header("Throw Settings")]
    public float throwForce = 10f;     
    public float throwUpwardBoost = 1f;

    [Header("Interaction Settings")]
    public float interactRange = 3f;  

    private CharacterController controller;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private Vector3 velocity;
    private float verticalRotation = 0f;

    [Header("UI SETTINGS")]
    public TextMeshProUGUI pickUpText;
    public Image healthBar;
    public float healthDamageAmount = 0.25f;
    private float healAmount = 0.5f;

    [Header("ANIMATION SETTINGS")]
    public Animator animator;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        originalMoveSpeed = moveSpeed;

      //  Cursor.lockState = CursorLockMode.Locked;
      //  Cursor.visible = false;
    }

    private void Update()
    {
        HandleMovement();
        HandleLook();

        if (heldObject != null)
        {
            heldObject.MoveToHoldPoint(holdPoint.position);
        }

        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, pickupRange))
        {
            PickUpObject pickUp = hit.collider.GetComponent<PickUpObject>();
            if (pickUp != null)
            {
                pickupText.text = pickUp.gameObject.name;
                return;
            }
        }

        // Clear text if not looking at a pickup
        pickupText.text = "";
    }

    public void TakeDamage()
    {
        healthBar.fillAmount -= healthDamageAmount;
        if (healthBar.fillAmount < 0f)
            healthBar.fillAmount = 0f;
    }
    public void Heal()
    {
        healthBar.fillAmount += healAmount;
        if (healthBar.fillAmount > 1f)
            healthBar.fillAmount = 1f;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context) 
    {
        if (context.performed && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Shoot();
        }
    }
    public void OnPickUp(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (heldObject == null)
        {
            Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, pickupRange))
            {
                PickUpObject pickUp = hit.collider.GetComponent<PickUpObject>();
                if (pickUp != null)
                {
                    pickUp.PickUp(holdPoint);
                    heldObject = pickUp;
                }
            }
        }
        else
        {
            heldObject.Drop();
            heldObject = null;
        }
    }

    public void OnThrow(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (heldObject == null) return;

        Vector3 dir = cameraTransform.forward;
        Vector3 impulse = dir * throwForce + Vector3.up * throwUpwardBoost;

        heldObject.Throw(impulse);
        heldObject = null;
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            controller.height = crouchHeight;
            moveSpeed = crouchSpeed;
        }
        else if (context.canceled)
        {
            controller.height = standHeight;
            moveSpeed = originalMoveSpeed;
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactRange))
        {
            // Only allow objects tagged as "Switchable"
            if (hit.collider.CompareTag("Switchable"))
            {
                var switcher = hit.collider.GetComponent<MaterialSwitcher>();
                if (switcher != null)
                {
                    switcher.ToggleMaterial();
                }
            }

            //if the object has the Movable tag
            if (hit.collider.CompareTag("Movable"))
            {
                var mover = hit.collider.GetComponent<Mover>();
                if (mover != null)
                {
                    mover.TriggerMoveUp();
                }
            }

            if (hit.collider.CompareTag("Door")) // Check if the object is a door
            {
                Animator doorAnimator = hit.collider.GetComponent<Animator>();
                AudioSource doorAudio = hit.collider.GetComponent<AudioSource>();

                // Toggle between open and close animations using a bool parameter
                bool isOpen = doorAnimator.GetBool("isOpen");

                if (isOpen)
                {
                    doorAnimator.SetBool("isOpen", false);
                    doorAudio.Play();
                }
                else
                {
                    doorAnimator.SetBool("isOpen", true);
                    doorAudio.Play();
                }
            }
        }
    }

    private void Shoot()
    {
        if (bulletPrefab != null && gunPoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, gunPoint.position, gunPoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddForce(gunPoint.forward * 1000f); // Adjust force value as needed
                Destroy(bullet, 3); // delete the bulet from the scene after 3 seconds
            }
        }
    }

    public void HandleMovement()
    {
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.Move(move * moveSpeed * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        float movementMagnitude = new Vector2(moveInput.x, moveInput.y).magnitude; // Calculate how "strong" the player's movement input is (0 when idle, up to 1 when moving fully)
        animator.SetFloat("Speed", movementMagnitude); //change speed float in animator controller

    }

    public void HandleLook()
    {
        float mouseX = lookInput.x * lookSensitivity;
        float mouseY = lookInput.y * lookSensitivity;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalLookLimit, verticalLookLimit);

        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
