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
    private int lookDirection = 1;

    private LevelController level;

    public void Initialize(Transform player, Transform[] newPatrolPoints, LevelController levelController)
    {
        playerTransform = player.transform;
        patrolPoints = newPatrolPoints;
        level = levelController;
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

        if (direction.x > 0.01f)
        {
            lookDirection = 1;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (direction.x < -0.01f)
        {
            lookDirection = -1;
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }

    public void Die()
    {
        Debug.Log("DIE");
        level.NotifyEnemyKilled(this);
        Destroy(this.gameObject);
    }

}