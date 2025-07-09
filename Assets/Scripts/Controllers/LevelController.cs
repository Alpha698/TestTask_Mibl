using UnityEngine;
using Zenject;

public class LevelController : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private CharacterController playerPrefab;
    [SerializeField] private Transform playerSpawnPoint;

    [Header("Enemies")]
    [SerializeField] private EnemySpawnData[] enemies;

    private CharacterController player;
    [Inject] private DiContainer container;


    public void SetPlayer(CharacterController newPlayer)
    {
        player = newPlayer;
    }

    public void SpawnEnemies()
    {
        foreach (var data in enemies)
        {
            var enemy = container.InstantiatePrefabForComponent<EnemyController>(
                data.enemyPrefab,
                data.spawnPoint.position,
                Quaternion.identity,
                this.gameObject.transform
            );

            enemy.Initialize(player.transform, data.patrolPoints);
        }

    }

    public CharacterController SpawnPlayer()
    {
        var newPlayer = container.InstantiatePrefabForComponent<CharacterController>(
            playerPrefab,
            playerSpawnPoint.position,
            Quaternion.identity,
            this.gameObject.transform
        );

        player = newPlayer;
        return player;
    }
}

[System.Serializable]
public class EnemySpawnData
{
    public EnemyController enemyPrefab;
    public Transform spawnPoint;
    public Transform[] patrolPoints;
}