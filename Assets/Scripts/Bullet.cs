using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float maxDistance = 20f;

    private int direction = 1;
    private Vector3 startPosition;

    public void Init(int lookDir)
    {
        direction = lookDir;
        startPosition = transform.position;

        //Destroy(gameObject, 2f); // защита от висящих пуль
    }

    private void Update()
    {
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

        if (Vector3.Distance(startPosition, transform.position) > maxDistance)
        {
            Destroy(gameObject); // защита от зависания
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("by collider");
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("by enemy");
            var enemy = other.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.Die();
            }

            Destroy(gameObject);
        }
        else if (!other.isTrigger)
        {
            Destroy(gameObject);
        }
    }
}
