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
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _turnSpeed = 360;
    private Vector3 _input;
    
    // Start is called before the first frame update

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        UpdateLives(10);
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
        _rb.MovePosition(transform.position + (transform.forward * _input.magnitude) * _speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Player collided with: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Enemy") && !isAttacking)
        {
            //Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            //Vector3 awayFromPlayer = collision.gameObject.transform.position - playerRb.transform.position;

            //enemyRb.AddForce(awayFromPlayer * strength, ForceMode.Impulse);
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
