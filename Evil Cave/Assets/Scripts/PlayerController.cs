using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour

{
    private Rigidbody playerRb;
    public float playerMoveSpeed = 5.0f;
    public float rotationSpeed;
    public float horizontalInput;
    public float verticalInput;
    public GameObject pickaxe;
    public bool isAttacking;
    public bool isCoolDown = false;
    public float coolDown = 1f;
    private Coroutine pickaxeDefaultCountdown;
    public float strength = 15.0f;
    private int lives;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        UpdateLives(10);
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();
        transform.Translate(movementDirection * playerMoveSpeed * Time.deltaTime, Space.World);

        if(movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        if(Input.GetMouseButtonDown(0))
        {
            {
                if(isCoolDown == false)
                {
                    Debug.Log("Pickaxe Used");
                    isAttacking = true;
                    pickaxe.transform.Rotate(Vector3.forward, 50.0f);
                    pickaxeDefaultCountdown = StartCoroutine(PickaxeCountdown());
                }
            }
        }

        IEnumerator PickaxeCountdown()
        {
        isCoolDown = true;
            yield return new WaitForSeconds(.3f);
            isAttacking = false;
        pickaxe.transform.Rotate(Vector3.forward, -50.0f);
        isCoolDown = false;
        }

        if(isAttacking == false)
        {
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Player collided with: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Enemy") && !isAttacking)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - playerRb.transform.position;

            enemyRb.AddForce(awayFromPlayer * strength, ForceMode.Impulse);
            UpdateLives(-1);
        }
    }

    public void UpdateLives(int livesToChange)
    {
        lives+= livesToChange;
        if (lives <= 0)
        {
            Debug.Log("Game Over!");
        }
    }
}
