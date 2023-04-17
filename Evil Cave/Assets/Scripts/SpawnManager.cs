using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public GameObject bossPrefab;
    public int bossRound;

    private readonly float spawnRange = 9f;
    public int enemyCount;
    public int waveNumber = 1;

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
            Instantiate(enemyPrefab[enemyIndex], GenerateSpawnPosition(false), transform.rotation);
        }

        if (waveNumber % 5 == 0)
        {
            Instantiate(bossPrefab, GenerateSpawnPosition(true), transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPosition(bool boss)
    {
        float spawnPosX = Random.Range(spawnRange, -spawnRange);
        float spawnPosZ = Random.Range(spawnRange, -spawnRange);
        

        if(boss)
        {
            Vector3 randomBossPos = new Vector3(spawnPosX, 1.57f, spawnPosZ);
            return randomBossPos;
        }
        
        else
        {
            Vector3 randomPos = new Vector3(spawnPosX, 0.77f, spawnPosZ);
            return randomPos;
        }
    }

    void SpawnBossWave(int currentRound)
    {
        if (bossRound != 0)
        {
        }
        else
        {
        }

        var boss = Instantiate(bossPrefab, GenerateSpawnPosition(true), bossPrefab.transform.rotation);
    }
}
