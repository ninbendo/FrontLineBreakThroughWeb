using UnityEngine;

public sealed class EnemySpawner : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private GameObject enemyPrefab;

    [Header("Spawn")]
    [SerializeField] private float intervalSec = 1.0f;
    [SerializeField] private float spawnY = 6.0f;
    [SerializeField] private float spawnXMin = -3.0f;
    [SerializeField] private float spawnXMax =  3.0f;

    [Header("Safety")]
    [SerializeField] private int maxSpawnPerFrame = 3;

    private float _acc;

    private void Update()
    {
        if (enemyPrefab == null) return;

        _acc += Time.deltaTime;

        int spawned = 0;
        while (_acc >= intervalSec && spawned < maxSpawnPerFrame)
        {
            _acc -= intervalSec;
            SpawnOne();
            spawned++;
        }
    }

    private void SpawnOne()
    {
        float x = Random.Range(spawnXMin, spawnXMax);
        Vector3 pos = new Vector3(x, spawnY, 0f);
        Instantiate(enemyPrefab, pos, Quaternion.identity);
    }
}
