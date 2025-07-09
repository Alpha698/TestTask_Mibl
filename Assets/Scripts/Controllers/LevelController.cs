using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelController : MonoBehaviour
{
    [Inject] private DiContainer container;
    [Inject] private UIScreenManager screenManager;

    [Header("Player")]
    [SerializeField] private CharacterController playerPrefab;
    [SerializeField] private Transform playerSpawnPoint;

    [Header("Enemies")]
    [SerializeField] private EnemySpawnData[] enemies;

    private CharacterController player;

    private List<EnemyController> activeEnemies = new();

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

            enemy.Initialize(player.transform, data.patrolPoints, this);
            activeEnemies.Add(enemy);
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


    public void NotifyEnemyKilled(EnemyController enemy)
    {
        activeEnemies.Remove(enemy);

        if (activeEnemies.Count == 0)
        {
            Debug.Log("All enemies defeated. Victory!");
            screenManager.ShowWinScreen(true);
            Time.timeScale = 0f;
        }
    }
}

[System.Serializable]
public class EnemySpawnData
{
    public EnemyController enemyPrefab;
    public Transform spawnPoint;
    public Transform[] patrolPoints;
}