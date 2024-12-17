using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [Header("Player Settings")]
    [SerializeField] private float moveSpeed = 40f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float gravityMultiplier = 1.5f;

    [Header("Markers")]
    [SerializeField] private Transform leftMarker;
    [SerializeField] private Transform middleMarker;
    [SerializeField] private Transform rightMarker;

    [Header("Ground Settings")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;

    [Header("Local Variables")]
    private bool isGrounded;
    private bool canMove = true;
    private Transform currentPosition;
    private bool isInvisible;
    private List<PowerUpType> activePowerUps = new List<PowerUpType>();

    [Header("References")]
    private Rigidbody rb;
    private Animator animator;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        currentPosition = middleMarker;
        transform.position = GetTargetPosition();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && canMove)
        {
            if (currentPosition != leftMarker)
                currentPosition = currentPosition == rightMarker ? middleMarker : leftMarker;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && canMove)
        {
            if (currentPosition != rightMarker)
                currentPosition = currentPosition == leftMarker ? middleMarker : rightMarker;
        }

        ApplyMovement();

        isGrounded = IsGrounded();
        animator.SetBool("isGrounded", isGrounded);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space) && canMove)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        if (!IsGrounded() && rb.velocity.y < 0)
        {
            rb.AddForce(Vector3.down * gravityMultiplier, ForceMode.Acceleration);
        }
    }

    public bool GetIsInvisible()
    {
        return isInvisible;
    }

    public void ActivatePowerUp(PowerUpType powerUpType)
    {
        activePowerUps.Add(powerUpType);
        StartCoroutine(HandlePowerUp(powerUpType));
    }

    public void OnGameOver()
    {
        animator.SetBool("gameOver", true);
        canMove = false;
    }

    private IEnumerator HandlePowerUp(PowerUpType powerUpType)
    {
        ApplyPowerUpEffect(powerUpType);

        yield return new WaitForSeconds(10f);

        RemovePowerUpEffect(powerUpType);
        activePowerUps.Remove(powerUpType);
    }

    private void ApplyPowerUpEffect(PowerUpType powerUpType)
    {
        switch (powerUpType)
        {
            case PowerUpType.DOUBLE_SCORE:
                if (GameManager.instance)
                {
                    GameManager.instance.ActivateDoubleScore();
                }
                if (UIManager.instance)
                {
                    UIManager.instance.SetDoubleScorePowerupIcon(true);
                }
                break;
            case PowerUpType.INVISIBLE:
                isInvisible = true;
                if (UIManager.instance)
                {
                    UIManager.instance.SetInvisiblePowerupIcon(true);
                }
                break;
        }
    }

    private void RemovePowerUpEffect(PowerUpType powerUpType)
    {
        switch (powerUpType)
        {
            case PowerUpType.DOUBLE_SCORE:
                if (UIManager.instance)
                {
                    UIManager.instance.SetDoubleScorePowerupIcon(false);
                }
                break;
            case PowerUpType.INVISIBLE:
                isInvisible = false;
                if (UIManager.instance)
                {
                    UIManager.instance.SetInvisiblePowerupIcon(false);
                }
                break;
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void ApplyMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, GetTargetPosition(), moveSpeed * Time.deltaTime);
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private Vector3 GetTargetPosition()
    {
        return new Vector3(currentPosition.position.x, transform.position.y, currentPosition.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        SpawnableObjectController spawnableObjectController = other.GetComponent<SpawnableObjectController>();
        if (spawnableObjectController && !isInvisible)
        {
            spawnableObjectController.OnCollision();
        }
    }
}
