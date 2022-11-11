using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;


public class Player : MonoBehaviour
{
    //public or private reference
    //data type (int, float, bool, string)
    //every variable has a name
    //optional value assigned

    [SerializeField]
    private float _speed = 5.0f;

    private float _speedMultiplier = 2f;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _SpeedUpPrefab;
    [SerializeField]
    private GameObject _SpeedUpVizualizer;
    [SerializeField]
    private GameObject _ShieldPrefab;
    [SerializeField]
    private GameObject _ShieldVisualizer;

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

    [SerializeField]
    private bool _isTripleShotActive = false;

    [SerializeField]
    private bool _isSpeedBoostActive = false;

    [SerializeField]
    private bool _isShieldActive = false;

    [SerializeField]
    private int _score;

    private UIManager _uiManager;

    

    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {

            Debug.LogError("The Spawn Manager is NULL.");
            
        }

        if (_uiManager == null)
        {

            Debug.LogError("The UI Manager is NULL.");

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
        
        Vector3 direction = new Vector3(horizontalInput, verticalInput);

       
        transform.Translate(direction * _speed * Time.deltaTime);
            
        
        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -3.8f)
        {
            
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }

        
        if (transform.position.x > 11.25f)
        {
            transform.position = new Vector3(-11.25f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.25f)
        {
            
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
        
        if (_isShieldActive == true)
        {

            _isShieldActive = false;
            _ShieldVisualizer.SetActive(false);
            return;

        }
        
        _lives --;

        //if lives is 2
        //enable right engine
        //else if lives is 1
        //enable left engine

        _uiManager.UpdateLives(_lives);

        if (_lives <= 0)
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

        yield return new WaitForSeconds(Random.Range(5.0f, 7.0f));
        _isTripleShotActive = false;

    }

    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {

        yield return new WaitForSeconds(Random.Range(5.0f, 8.0f));
        _isSpeedBoostActive = false;
        _speed /= _speedMultiplier;

    }


    public void ShieldActive()
    {

        _isShieldActive = true;
        StartCoroutine(ShieldActiveRoutine());
        _ShieldVisualizer.SetActive(true);

    }

    IEnumerator ShieldActiveRoutine()
    {

        yield return new WaitForSeconds(Random.Range(6.0f, 8.0f));
        _isShieldActive = false;

    }


    public void AddScore(int points)
    {

        _score += points;
        _uiManager.UpdateScore(_score);
        


    }
    //method to add 10 to score!
    //communicate with the UI to update the score!
    

}
