using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe : MonoBehaviour
{

    private PlayerController playerController;
    public GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy" && playerController.isAttacking)
        {
            //Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Debug.Log("Pickaxe destroyed: " + other.gameObject.name);
        }
    }
}
