using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public GameObject bossPrefab;
    public GameObject miniEnemyPrefab;
    public int bossRound;
    private readonly float spawnRange = 9f;
    public int enemyCount;
    public int waveNumber = 1;
    public bool bossActive;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if(enemyCount == 0)
        {
            waveNumber++;
            if (waveNumber % bossRound == 0)
            {
                SpawnBossWave(waveNumber);
            }
            else
            {
                SpawnEnemyWave(waveNumber);
            }
        }
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int enemyIndex = Random.Range(0, enemyPrefab.Length);
            Instantiate(enemyPrefab[enemyIndex], GenerateSpawnPosition(), transform.rotation);
        }

        if (waveNumber % 10 == 0)
        {
            Instantiate(bossPrefab, GenerateSpawnPosition(), transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(spawnRange, -spawnRange);
        float spawnPosZ = Random.Range(spawnRange, -spawnRange);

        if(bossPrefab)
        {
            Vector3 randomBossPos = new Vector3(spawnPosX, 1.57f, spawnPosZ);
            return randomBossPos;
        }

        if(miniEnemyPrefab)
        {
            Vector3 randomMiniPos = new Vector3(spawnPosX, 0.162f, spawnPosZ);
            return randomMiniPos;
        }
        else
        {
            Vector3 randomBossPos = new Vector3(spawnPosX, 0.77f, spawnPosZ);
            return randomBossPos;
        }
    }

    void SpawnBossWave(int currentRound)
    {
        int miniEnemysToSpawn;

        if (bossRound != 0)
        {
            bossActive = true;
            miniEnemysToSpawn = currentRound / bossRound;
        }
        else
        {
            miniEnemysToSpawn = 1;
        }

        var boss = Instantiate(bossPrefab, GenerateSpawnPosition(), bossPrefab.transform.rotation);
        boss.GetComponent<Enemy>().miniEnemySpawnCount = miniEnemysToSpawn;
    }

    public void SpawnMiniEnemy(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            Instantiate(miniEnemyPrefab, GenerateSpawnPosition(), miniEnemyPrefab.transform.rotation);
        }
    }
}
