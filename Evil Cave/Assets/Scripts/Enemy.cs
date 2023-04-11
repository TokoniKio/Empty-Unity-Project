using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float enemyMoveSpeed = 5.0f;
    private Rigidbody enemyRb;
    private GameObject player;
    private PlayerController playerController;
    public int health = 10;

    public Transform target;
    [SerializeField]

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
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
    }

    public void UpdateHealth(int healthToChange)
    {
        health+= healthToChange;
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
