using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public GameObject bossPrefab;
    public GameObject miniEnemyPrefab;
    public int bossRound;
    private readonly float spawnRange = 9f;
    public int enemyCount;
    public int waveNumber = 1;
    public GameObject[] powerupPrefabs;
    private PlayerController playerController;
    public bool bossDefeated = false;
    public bool bossActive;
    public TextMeshProUGUI wavesText;
    public TextMeshProUGUI bossWaveText;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if(!playerController.gameOver && !bossDefeated)
        {
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
                int randomPowerup = Random.Range(0, powerupPrefabs.Length);
                Instantiate(powerupPrefabs[randomPowerup], GenerateSpawnPosition(), powerupPrefabs[randomPowerup].transform.rotation);
            }
        }
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int enemyIndex = Random.Range(0, enemyPrefab.Length);
            Instantiate(enemyPrefab[enemyIndex], GenerateSpawnPosition(), transform.rotation);
            wavesText.gameObject.SetActive(true);
            bossWaveText.gameObject.SetActive(false);
            wavesText.text = "Wave: " + waveNumber;
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
        
        Vector3 randomPos = new Vector3(spawnPosX, 0.77f, spawnPosZ);
        return randomPos;
        
    }

    void SpawnBossWave(int currentRound)
    {
        int miniEnemysToSpawn;
        wavesText.gameObject.SetActive(false);
        bossWaveText.gameObject.SetActive(true);


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
