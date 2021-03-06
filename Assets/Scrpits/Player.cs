using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //public or private reference
    //data type (int, float, bool, string)
    //every variable has a name
    //optional value assigned
    [SerializeField]
    private float _speed = 3.5f;
    
    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private GameObject _SpeedUpPrefab;

    [SerializeField]
    private GameObject _ShieldPrefab;
    
    [SerializeField]
    private Vector3 laserOffset = new Vector3(0, 0.99f, 0);
    
    [SerializeField]
    private float _fireRate = 0.25f;
    
    [SerializeField]
    private float _canFire = -1f;
    
    [SerializeField]
    private float _nextFire = 0.5f;
    
    [SerializeField]
    private int _lives = 3;

    private SpawnManager _spawnManager;

    //variable IstripleShot Active
    [SerializeField]
    private bool _isTripleShotActive = false;

    [SerializeField]
    private bool _isSpeedActive = false;

    [SerializeField]
    private bool _isShieldActive = false;

    


        // Start is called before the first frame update
    void Start()
    {
                //take current position = new position (0, 0, 0)
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {

            Debug.LogError("The Spawn Manager is NULL.");
            
        }


    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }

    
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // new Vector3(1, 0, 0) * 5 * real time
        // Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        // transform.Translate(direction * _speed * Time.deltaTime); Does the same thing as below
        transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);

        //if player position on the y is greater than 0
        //y position = 0
        //else if position on the y is less than -3.8f
        //y pos = -3.8f

        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -3.8f)
        {
            //this will run
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }

        //if player on the x > 11.25
        //x pos = -11.25
        //else if player on the x is less than -11.25
        //x pos = 11.25

        if (transform.position.x > 11.25f)
        {
            transform.position = new Vector3(-11.25f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.25f)
        {
            //this will run
            transform.position = new Vector3(11.25f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        if (_isTripleShotActive == true)
        {

            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            
            
        }
        else
        {

            Instantiate(_laserPrefab, transform.position + laserOffset, Quaternion.identity);

        }
            
            
            
    }

    public void Damage()
    {
    
        _lives --;

        //check if dead
        //destroy us

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            
            Destroy(this.gameObject);
        
        }
    
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
        
    }

    IEnumerator TripleShotPowerDownRoutine()
    {

        yield return new WaitForSeconds(7.0f);
        _isTripleShotActive = false;

    }

    public void SpeedActive()
    {
        _isSpeedActive = true;
        StartCoroutine(SpeedActiveRoutine());

    }

    IEnumerator SpeedActiveRoutine()
    {

        yield return new WaitForSeconds(7.0f);
        _isSpeedActive = true;

    }

    public void ShieldActive()
    {

        _isShieldActive = true;
        StartCoroutine(ShieldActiveRoutine());

    }

    IEnumerator ShieldActiveRoutine()
    {

        yield return new WaitForSeconds(7.0f);

    }

}
