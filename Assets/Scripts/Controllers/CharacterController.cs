using UnityEngine;
using Zenject;

public class CharacterController : MonoBehaviour
{
    [Inject] private IInputService input;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    //[SerializeField] private Transform firePoint;
    //[SerializeField] private GameObject bulletPrefab;

    [Space]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius = 0.1f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float horizontalInput;

    private void Awake() => rb = GetComponent<Rigidbody2D>();

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        horizontalInput = input.Horizontal;

        if (input.JumpPressed && isGrounded)
        {
            Jump();
            input.Reset(); // <== вот это обязательно, сбрасываем JumpPressed после прыжка
        }

        if (input.ShootPressed)
        {
            //Shoot();
            input.Reset(); // если нужно сбрасывать стрельбу тоже
        }

    }

    private void FixedUpdate() => rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

    private void Jump()
    {
        Debug.Log("Jump!");
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }


    //private void Shoot()
    //{
    //    Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
    //}
}
