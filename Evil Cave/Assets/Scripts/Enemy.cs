using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float enemyMoveSpeed = 5.0f;
    private GameObject player;
    private PlayerController playerController;
    public int health = 5;

    [SerializeField] private Rigidbody _enemyRb;
    [SerializeField] private float _enemySpeed = 5;
    [SerializeField] private float _enemyTurnSpeed = 360;

    public Transform target;
    [SerializeField]

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Look();

        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        
        _enemyRb.AddForce(lookDirection * _enemySpeed);

        Vector3 relativePos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);

        Quaternion current = transform.localRotation;

        transform.localRotation = Quaternion.Slerp(current, rotation, Time.deltaTime * _enemySpeed);
    }

    void FixedUpdate()
    {
        Move();
    }

    void Look()
    {
        if(playerController._input != Vector3.zero)
        {
            var relative = (transform.position + playerController._input) - transform.position;
            var rot = Quaternion.LookRotation(relative,Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation,rot,_enemyTurnSpeed * Time.deltaTime);
        }
    }

    void Move()
    {
        _enemyRb.MovePosition(transform.position + (transform.forward * playerController._input.magnitude) * _enemySpeed * Time.deltaTime);
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
