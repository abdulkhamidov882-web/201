using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class Player : MonoBehaviour



public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10f;   //playerni tezlanishi

    [SerializeField]
    private GameObject _laserPrefab; 
    [SerializeField]
    private GameObject _tripleShotPrefab; 
    [SerializeField]
    private float _fireRate = 0.1f;   //o'qilarni nechidur sekunda keyin chiqishi
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3; // 3 ta jon
    private  SpawnManager _spawnManager;
    [SerializeField]
    private bool _isTripleShotActive = false;
    
    void Start()
    {    
        transform.position = new Vector3(0, 0, 0); 
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();    //find the object. Get the componet  
        if (_spawnManager == null){
            Debug.LogError("The Spawn Manager is Null.");//ask gpt what's this big????? 
        }
    }
    
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
        
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 3.5f), 0);

        if (transform.position.x > 11f) 
        {
            transform.position = new Vector3(-11f, transform.position.y, 0);
        }
        else if (transform.position.x < -11f)
        {
            transform.position = new Vector3(11f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        
        if(_isTripleShotActive == true) {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity); 
        }
        else{
             Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }



       
    }

    public void Damage()
    {
        _lives--; // Jonni 1 taga kamaytirish

        if (_lives < 1)
        {
            //Communicate with spawn Manger 
            //Let them know to stop spawing 
            _spawnManager.OnPlayerDeath();            
            Destroy(this.gameObject); // Jon tugasa o'yinchini yo'q qilish
        }
    }
    
    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerdDownRoutine());
        //start the power down coroutine for triple shot 
    }


    // IEnumerator TripleShotPowerdDownRoutine
    // wait 5 seconds
    // set the triple shot to false 

    IEnumerator TripleShotPowerdDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }
}