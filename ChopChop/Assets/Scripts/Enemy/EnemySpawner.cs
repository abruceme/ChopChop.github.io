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

    [SerializeField]
    private Transform player;
    // Start is called before the first frame update
    int curSpawnIndex = 0;

    List<int> spawnPositionIndex;

    [SerializeField]
    float timeBetweenSpawnMax = 20;

    [SerializeField]
    float timeBetweenSpawnMin = 10;
    float timeBetweenNextSpawn;

    public Coroutine spawnCoroutine;
    [HideInInspector]
    public bool spawnActive = false;
    public bool hasWaves = false;
    public float speedIncreaseIncrement = 0.1f;
    public int waveNumber = 1;
    private int enemyCount = 0;
    /*void Awake()
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
    }*/

    private void Start()
    {
        int noOfPaths = paths.Count;
        curSpawnIndex = 0;
        spawnPositionIndex = new List<int>();
        for (int i = 0; i < noOfEnemies; i++)
        {
            spawnPositionIndex.Add(UnityEngine.Random.Range(0, noOfPaths));
        }
        if (hasWaves)
        {
            StartCoroutine(ShowWave());
        }
        ResetSpawnTimer(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnActive)
        {
            //timeBetweenNextSpawn -= Time.deltaTime;
            if (timeBetweenNextSpawn <= 0)
            {
                Spawn();
            }
        }
    }

    public void ResetSpawnTimer(float forceTime = -1)
    {
        //Reset SpawnTimer
        if (forceTime < 0)
            timeBetweenNextSpawn = UnityEngine.Random.Range(timeBetweenSpawnMin, timeBetweenSpawnMax);
        Debug.Log($"Next Spawn in {timeBetweenNextSpawn}s");
        spawnActive = true;
    }
    void Spawn()
    {
        int pathIndex = spawnPositionIndex[curSpawnIndex];
        Debug.Log($"Spawn  enemy in position {pathIndex}");
        SpawnPath spawnPath = paths[pathIndex];
        EnemyAI ai = Instantiate(enemyPrefab, spawnPath.startPosition.position, Quaternion.identity);
        ai.transform.SetParent(this.transform);
        ai.transform.LookAt(player);
        ai.gameObject.name = "Enemy " + curSpawnIndex;
        ai.enemy.curState = EnemyController.EnemyStates.RUNNING;
        ai.enemy.currentWeapon = (GameCharacterController.WeaponStates)UnityEngine.Random.Range(1, 4);
        if (hasWaves)
        {
            ai.enemy.SetCharacterHealth(50 * ((waveNumber / 2) + 1));
            ai.enemy.IncreaseAttackSpeed(waveNumber * speedIncreaseIncrement);
        }
        else
        {
            ai.enemy.SetAttackSpeed();
        }
        ai.Spawn(spawnPath.startPosition.position, spawnPath.endPosition.position);
        curSpawnIndex += 1;
        spawnActive = false;

        if (curSpawnIndex != spawnPositionIndex.Count)
        {
            ResetSpawnTimer();
        }
        else
        {
            Debug.Log("Spawn ended");
        }
    }
    public void ResetTimeBetweenNextSpawn()
    {
        timeBetweenNextSpawn = 0;
        if (hasWaves)
        {
            enemyCount++;
            if (enemyCount % 3 == 0)
            {
                waveNumber++;
                StartCoroutine(ShowWave());
                Debug.Log("You made it to Wave " + (waveNumber));
            }
        }
    }
    [Serializable]
    public struct SpawnPath
    {
        public Transform startPosition;
        public Transform endPosition;
    }
    IEnumerator ShowWave()
    {
        GameObject waveNumberText = GameObject.Find("WaveNumber");
        waveNumberText.GetComponent<TMPro.TextMeshProUGUI>().text = "Wave " + waveNumber;
        waveNumberText.GetComponent<Animator>().SetBool("Fade",true);
        yield return new WaitForSeconds(2.5f);
        GameObject.Find("WaveNumber").GetComponent<Animator>().SetBool("Fade",false);
    }
}
