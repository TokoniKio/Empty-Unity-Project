using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour

{
    public GameObject pickaxe;
    public bool isAttacking;
    public bool isCoolDown = false;
    public float coolDown = 1f;
    private Coroutine pickaxeDefaultCountdown;
    private int lives;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _powerUpSpeed = 10;
    [SerializeField] private float _turnSpeed = 360;
    public Vector3 _input;
    public bool speedPowerup = false;
    public bool gameOver = true;

    public TextMeshProUGUI livesText;

    public bool hasPowerup = false; 
    public PowerUpType currentPowerUp = PowerUpType.None;
    private Coroutine powerupCountdown;

    // Start is called before the first frame update

    void Start()
    {
        UpdateLives(100);
    }

    // Update is called once per frame
    void Update()
    {
        GatherInput();
        Look();
        
        if(Input.GetMouseButtonDown(0))
        {
                if(isCoolDown == false)
            {
                    Debug.Log("Pickaxe Used");
                    isAttacking = true;
                    pickaxe.transform.Rotate(Vector3.forward, 50.0f);
                    pickaxeDefaultCountdown = StartCoroutine(PickaxeCountdown());
            }
        }

        if (currentPowerUp == PowerUpType.Speed)
        {
            speedPowerup = true;
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        speedPowerup = false;
        currentPowerUp = PowerUpType.None;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player collided with: " + other.gameObject.name);

        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            currentPowerUp = other.gameObject.GetComponent<PowerUp>().powerUpType;
            Destroy(other.gameObject);

            if(powerupCountdown != null)
            {
                StopCoroutine(powerupCountdown);
            }
            powerupCountdown = StartCoroutine(PowerupCountdownRoutine());
        }

        if (other.gameObject.name[0] == 'H' && other.gameObject.name[7] == 'P')
        {
            UpdateLives(5);
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

    void FixedUpdate()
    {
        Move();
    }

    void GatherInput()
    {
        _input = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"));
    }

    void Look()
    {
        if(_input != Vector3.zero)
        {
            var relative = (transform.position + _input) - transform.position;
            var rot = Quaternion.LookRotation(relative,Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation,rot,_turnSpeed * Time.deltaTime);
        }
    }

    void Move()
    {
        if(!speedPowerup)
        {
            _rb.MovePosition(transform.position + (transform.forward * _input.magnitude) * _speed * Time.deltaTime);
        }
        else
        {
        _rb.MovePosition(transform.position + (transform.forward * _input.magnitude) * _powerUpSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Player collided with: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Enemy") && !isAttacking)
        {
            UpdateLives(-1);
        }

        if (collision.gameObject.CompareTag("Boss") && !isAttacking)
        {
            UpdateLives(-2);
        }
    }

    public void UpdateLives(int livesToChange)
    {
        lives+= livesToChange;
        livesText.text = "Health: " + lives;
        if (lives <= 0)
        {
            Debug.Log("Game Over!");
        }
    }
}
