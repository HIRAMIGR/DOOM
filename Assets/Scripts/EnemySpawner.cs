using UnityEngine;
using UnityEngine.Events;
public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private EnemySpawn[] enemySpawns;
    [SerializeField]
    private UnityEvent onEnemiesSpawned;
    [SerializeField]
    private UnityEvent onAllEnemiesDefeated;
    private int enemiesRemaining;
    public void SpawnEnemies()
    {
        enemiesRemaining = enemySpawns.Length;
        foreach (var spawn in enemySpawns)
        {
            Enemy enemy = Instantiate(spawn.enemyPrefab, spawn.spawnPoint.position,
                spawn.spawnPoint.rotation).GetComponent<Enemy>();
            enemy.OnDied.AddListener(OnEnemyDied);
        }
        onEnemiesSpawned?.Invoke();
    }
    private void OnEnemyDied()
    {
        enemiesRemaining--;
        if(enemiesRemaining <= 0)
        {
            onAllEnemiesDefeated?.Invoke();
        }
    }
}
[System.Serializable]
public class EnemySpawn
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;
}