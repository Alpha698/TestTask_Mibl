using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;

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

        if (input.JumpPressed && isGrounded)
        {
            Jump();
            animator.SetTrigger("Jump");
            input.Reset(); // <== вот это обязательно, сбрасываем JumpPressed после прыжка
        }

        if (input.ShootPressed)
        {
            Shoot();
            input.Reset(); // если нужно сбрасывать стрельбу тоже
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
            animator.SetTrigger("Run");        // начал бежать
        else if (!isRunning && wasRunning)
            animator.SetTrigger("Idle");       // остановился

        wasRunning = isRunning;

    }

    // FixedUpdate better to work with physics than Update
    private void FixedUpdate() => rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

    private void Jump()
    {
        Debug.Log("Jump!");
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }


    private void Shoot()
    {
        Debug.Log("Shoot!");
        
        var bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().Init(lookDirection);

        //Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
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
