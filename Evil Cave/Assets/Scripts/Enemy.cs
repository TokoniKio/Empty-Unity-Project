using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;


public class Enemy : MonoBehaviour
{
    public float enemyMoveSpeed = 5.0f;
    private Rigidbody enemyRb;
    private GameObject player;
    private PlayerController playerController;
    public int health;
    public TextMeshProUGUI healthText;


    public bool isBoss = false;
    public float spawnInterval;
    private float nextSpawn;
    public int miniEnemySpawnCount;
    private SpawnManager spawnManager;

    private Transform target;
    [SerializeField]

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        if (CompareTag("Boss"))
        {
            UpdateHealth(Random.Range(50,100));
        }
        
        if(CompareTag("Enemy"))
        {
            UpdateHealth(Random.Range(1,10));
        }

        if(CompareTag("Mini Enemy"))
        {
            UpdateHealth(Random.Range(1,5));
        }
        target = player.transform;

        if (isBoss)
        {
            spawnManager = FindObjectOfType<SpawnManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {        
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        
        enemyRb.AddForce(lookDirection * enemyMoveSpeed);

        Vector3 relativePos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);

        Quaternion current = transform.localRotation;

        transform.localRotation = Quaternion.Slerp(current, rotation, Time.deltaTime * enemyMoveSpeed);

        if(isBoss)
        {
            if(Time.time > nextSpawn)
            {
                nextSpawn = Time.time + spawnInterval;
                spawnManager.SpawnMiniEnemy(miniEnemySpawnCount);
            }
        }
    }

    public void UpdateHealth(int healthToChange)
    {
        health+= healthToChange;
        healthText.text = "Health: " + health;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (playerController.isAttacking)
        {
            UpdateHealth(-1);
        }
    }
}
