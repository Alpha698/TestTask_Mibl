using UnityEngine;
using Zenject;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float chaseRange = 3f;

    private int currentPointIndex = 0;
    private bool isChasing = false;

    private Transform playerTransform;

    public void Initialize(Transform player, Transform[] newPatrolPoints)
    {
        playerTransform = player.transform;
        patrolPoints = newPatrolPoints;
    }

    private void Update()
    {
        if (playerTransform == null) return;

        if (Vector2.Distance(transform.position, playerTransform.position) <= chaseRange)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

        if (isChasing)
            ChasePlayer();
        else
            Patrol();
    }

    private void Patrol()
    {
        if (patrolPoints == null || patrolPoints.Length == 0) return;

        Transform target = patrolPoints[currentPointIndex];
        MoveTowards(target.position);

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        }
    }

    private void ChasePlayer()
    {
        MoveTowards(playerTransform.position);
    }

    private void MoveTowards(Vector2 target)
    {
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player touched: Game Over");
            // Здесь можно вызвать GameOver через UIScreenManager (если его инжектить)
        }
    }
}