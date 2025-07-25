﻿using UnityEngine;
using Zenject;

public class CharacterController : MonoBehaviour
{
    [Inject] private IInputService input;
    [Inject] private UIScreenManager screenManager;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;

    [Space]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius = 0.1f;
    [SerializeField] private LayerMask groundLayer;

    // for animation
    private static readonly int HashSpeed = Animator.StringToHash("Speed");
    private static readonly int HashGrounded = Animator.StringToHash("IsGrounded");
    private static readonly int HashJump = Animator.StringToHash("Jump");


    private Rigidbody2D rb;
    private bool isGrounded;
    private float horizontalInput;
    private int lookDirection = 1; // rotation by movement
    private bool wasRunning;

    private void Awake() => rb = GetComponent<Rigidbody2D>();

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        horizontalInput = input.Horizontal;

        animator.SetBool(HashGrounded, isGrounded);
        animator.SetFloat(HashSpeed, Mathf.Abs(horizontalInput));

        if (input.JumpPressed && isGrounded)
        {
            Jump();
            animator.SetTrigger(HashJump);
            input.Reset(); 
        }

        if (input.ShootPressed)
        {
            Shoot();
            input.Reset(); 
        }


        if (horizontalInput > 0)
        {
            lookDirection = 1;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (horizontalInput < 0)
        {
            lookDirection = -1;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        bool isRunning = Mathf.Abs(horizontalInput) > 0.01f;

        if (isRunning && !wasRunning)
            animator.SetTrigger("Run");        
        else if (!isRunning && wasRunning)
            animator.SetTrigger("Idle");     

        wasRunning = isRunning;

    }

    // FixedUpdate better to work with physics than Update
    private void FixedUpdate() => rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        animator.SetTrigger("Idle");
    }


    private void Shoot()
    {
        Debug.Log("Shoot!");
        
        var bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().Init(lookDirection);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Player touched: Game Over");

            screenManager.ShowGameOverScreen(true);

            Time.timeScale = 0f;

        }
    }
}
