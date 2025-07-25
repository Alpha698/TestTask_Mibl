﻿using UnityEngine;

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

    }

    private void Update()
    {
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

        if (Vector3.Distance(startPosition, transform.position) > maxDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
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
