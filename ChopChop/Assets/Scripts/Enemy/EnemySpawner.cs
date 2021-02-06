using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    List<SpawnPath> paths;

    public static EnemySpawner Instance;

    [SerializeField]
    int noOfEnemies = 3;

    [SerializeField]
    private EnemyAI enemyPrefab;
    // Start is called before the first frame update
    int curSpawnIndex = 0;

    List<int> spawnPositionIndex;

    [SerializeField]
    float timeBetweenSpawnMax = 20;

    [SerializeField]
    float timeBetweenSpawnMin = 10;
    float timeBetweenNextSpawn;

    public Coroutine spawnCoroutine;

    public bool spawnActive = false;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Debug.Log($"{this.gameObject.name} + gameobject destroyed duplicate singleton!");
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        int noOfPaths = paths.Count;
        curSpawnIndex = 0;
        spawnPositionIndex = new List<int>();
        for (int i = 0; i < noOfEnemies; i++)
        {
            spawnPositionIndex.Add(UnityEngine.Random.Range(0,noOfPaths));
        }

        ResetSpawnTimer(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnActive)
        {
            timeBetweenNextSpawn -= Time.deltaTime;
            if (timeBetweenNextSpawn <= 0)
            {
                Spawn();
            }
        }
    }

    public void ResetSpawnTimer(float forceTime = -1)
    {
        //Reset SpawnTimer
        if(forceTime < 0)
        timeBetweenNextSpawn = UnityEngine.Random.Range(timeBetweenSpawnMin, timeBetweenSpawnMax);
        Debug.Log($"Next Spawn in {timeBetweenNextSpawn}s");
        spawnActive = true;
    }
    void Spawn()
    {
        int pathIndex = spawnPositionIndex[curSpawnIndex];
        Debug.Log($"Spawn  enemy in position {pathIndex}");
        SpawnPath spawnPath = paths[pathIndex];
        EnemyAI enemy = Instantiate(enemyPrefab, spawnPath.startPosition.position, Quaternion.identity);
        enemy.transform.SetParent(this.transform);
        enemy.gameObject.name = "Enemy " + curSpawnIndex;
        enemy.Spawn(spawnPath.startPosition.position, spawnPath.endPosition.position);
        curSpawnIndex += 1;
        spawnActive = false;

        if (curSpawnIndex != spawnPositionIndex.Count )
        {
            ResetSpawnTimer();
        }
        else
        {
            Debug.Log("Spawn ended");
        }
    }
    [Serializable]
    public struct SpawnPath
    {
        public Transform startPosition;
        public Transform endPosition;
    }
}
