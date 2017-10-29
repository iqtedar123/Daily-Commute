using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class carController : MonoBehaviour {
    public float carSpeed;
    public bool powerupActive;
    //public float minPosition;
    public float minMaxPosition = 2.3f;
    Vector3 position;
   public  UIManager ui;
    Rigidbody2D rb;
    bool currentPlatformAndroid = false;
    public bool MoveLeftBool = false;
    public bool MoveRightBool = false;
    float powerupTimerExpire = 5.0f;
    float powerupTimer = 0.0f;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Use this for initialization
    void Start () {
        position = transform.position;
        if (currentPlatformAndroid == true)
        {
            Debug.Log("Android");
        }
        else {
            Debug.Log("Windows");
        }
	}
	
	// Update is called once per frame
	void Update () {


        //    position.x += Input.GetAxis("Horizontal") * carSpeed * Time.deltaTime;
        //    position.x = Mathf.Clamp(position.x, -2.3f, 2.3f);
        //    transform.position = position;
        //position.x = Mathf.Clamp(position.x, -2.3f, 2.3f);
        //transform.position = position;
        Movement();
        Debug.Log("Car speed is:" + carSpeed);
        if (powerupActive == true)
        {
            if (powerupTimer < powerupTimerExpire)
            {
                powerupTimer += Time.deltaTime;
            }
            else
            {
                powerupActive = false;
                carSpeed = carSpeed - 10.0f;
                powerupTimer = 0.0f;
                Debug.Log("Car speed is:" + carSpeed);
            }
        }
    }
    void checkIfPowerup()
    {
        if (powerupActive == true)
        {
            if (powerupTimer <= powerupTimerExpire)
            {
                powerupTimer += Time.deltaTime;
            }
            else
            {
                powerupActive = false;
                carSpeed = carSpeed - 15.0f;
                powerupTimer = 0.0f;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //Destroys the player
            ui.gameOver();
            //Destroy(gameObject);
            gameObject.SetActive(false);
            SceneManager.LoadScene("GameOver");
        }
        else if (collision.gameObject.tag == "Powerup")
        {
            //Hit a power up. 
            //Increase the speed. 
            if (powerupActive == false) { 
                carSpeed = carSpeed + 10.0f;
                collision.gameObject.SetActive(false);
                powerupActive = true;
            }
            else
            {
                powerupTimer = 0.0f;//Reset cooldown. 
            }
        }else if(collision.gameObject.tag == "Fuel")
        {
            collision.gameObject.SetActive(false);
            UIManager.score = UIManager.score + 30;
        }
    }
    public void MoveLeft()
    {
        // rb.velocity = new Vector2(-carSpeed, 0);
        MoveRightBool = false;
        MoveLeftBool = true;
        
    }
    public void goRight()
    {
        MoveLeftBool = false;
        MoveRightBool = true;
       

    }
    public void SetVelocityZero()
    {
        rb.velocity = Vector2.zero;
    }
    void TouchMove()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            float middle = Screen.width / 2;
            if(touch.position.x < middle && touch.phase == TouchPhase.Began)
            {
                MoveLeft();
            }else if(touch.position.x > middle && touch.phase == TouchPhase.Began)
            {
                goRight();
            }
        }
        else
        {
            SetVelocityZero();
        }
    }
    void Movement()
    {
        //Touch Controls

        if (MoveRightBool == true)
        {
            transform.Translate(Vector2.right * (carSpeed / 2) * Time.deltaTime);
            transform.eulerAngles = new Vector2(0, 0);
            position = transform.position;
            position.x = Mathf.Clamp(position.x, -2.3f, 2.3f);
            transform.position = position;
        }
        //Left Movement triggered
        if (MoveLeftBool == true)
        {
            
            transform.Translate(Vector2.right * (carSpeed / 2) * Time.deltaTime);
            transform.eulerAngles = new Vector2(0, 180);
            position = transform.position;
            position.x = Mathf.Clamp(position.x, -2.3f, 2.3f);
            transform.position = position;
        }
    }
}

