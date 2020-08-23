using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    // for boundaries
    public const float Y_MAX = 5.0f;
    public const float Y_MIN = -5.0f;
    private float pancakeY;
    private bool onBoundDetected = false;

    // player physics
    public float speed = 1;
    public float gravity = -10.0f;

    // physics
    private Vector3 movement;       // the movement of the pancake
    private Rigidbody rb;
    private bool isMoving = false;  // true if the pancake is moving

    // arrow
    public GameObject arrowObject;
    public float arrowAngle;
    private bool isArrowRotating = true;

    // sounds
    public AudioSource dieSfx;
    public AudioSource eatSfx;
    public AudioSource towallSfx;
    public AudioSource jumpSfx;
    public AudioSource lightSfx;

    // scores
    private int bearsEaten = 0;
    private int houseLit = 0;
    public GameObject scoreText;

    // pausing mechanics
    public GameObject pauseButton;
    private bool isPaused = false;
    public GameObject resumeText;


    // Start is called before the first frame update
    void Start() {

        Physics.gravity = new Vector3(0.0f, gravity, 0.0f);

        this.rb = GetComponent<Rigidbody>();
        this.isMoving = false;
        this.isArrowRotating = true;

        this.scoreText.GetComponent<Text>().text = "TEDDY BEARS EATEN: 0\nHOUSE LIT: 0";

        this.pancakeY = this.GetComponent<Collider>().bounds.size.y;

        // pauseButton.SetActive(false);
        resumeText.SetActive(false);

    }


    // Update is called once per frame
    void Update() {

        if (this.isArrowRotating && (!this.isMoving)) {
            this.arrowObject.transform.Rotate((this.arrowAngle * Time.deltaTime) * Vector3.back);
        }

        if (!isPaused) {

            float inputPositionY = 0;
            bool tapped = false;

            if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began)) {
                inputPositionY = Input.GetTouch(0).position.y;
                tapped = true;
            }

            if (Input.GetMouseButtonDown(0)) {
                inputPositionY = Input.mousePosition.y;
                tapped = true;
            }

            if ((tapped) && (inputPositionY < Screen.height * 0.8)) {
                Debug.Log("InputY: " + inputPositionY + "; ScreenY: " + Screen.height);
                startMoving();
            }

        } else {
            onResume();
        }

    }


    // Physical update
    private void FixedUpdate() {
        // this.rb.AddForce(movement * speed);
        this.transform.position += movement * speed;
        this.arrowObject.transform.position = this.transform.position;

        float maxBound = (float)(Y_MAX - this.pancakeY * 0.5);
        float minBound = (float)(Y_MIN + this.pancakeY * 0.5);
        if (this.transform.position.y <= minBound) {
            this.transform.position = new Vector3(
                this.transform.position.x,
                (float)minBound,
                this.transform.position.z
            );
            stopMoving();
            if (!onBoundDetected) {
                onBoundDetected = true;
                towallSfx.Play();
            }
        }
        if (this.transform.position.y >= maxBound) {
            this.transform.position = new Vector3(
                this.transform.position.x,
                (float)maxBound,
                this.transform.position.z
            );
            stopMoving();
            if (!onBoundDetected) {
                onBoundDetected = true;
                towallSfx.Play();
            }
        }
    }


    // pause
    public void onPause() {
        stopMoving();
        isPaused = true;
        Time.timeScale = 0;
        resumeText.SetActive(true);
        Debug.Log("Paused");
    }


    // resume
    public void onResume() {
        resumeText.SetActive(false);
        this.movement = new Vector3(0.0f, 0.0f, 0.0f);
        startMoving();
        isPaused = false;
        Time.timeScale = 1;
        Debug.Log("Resume");
    }


    // shoots the pancake
    private void startMoving() {

        Debug.Log("Start moving");

        jumpSfx.Play();
        this.onBoundDetected = false;

        this.isArrowRotating = false;
        this.isMoving = true;
        this.movement = this.arrowObject.transform.up;

        this.arrowObject.SetActive(false);

    }

    // stops the pancake
    private void stopMoving() {

        this.movement = new Vector3(0, 0, 0);
        this.rb.velocity = new Vector3(0, 0, 0);
        this.rb.angularVelocity = new Vector3(0, 0, 0);
        this.arrowObject.SetActive(true);

        // reset control
        this.isArrowRotating = true;
        this.isMoving = false;

    }

   
    // collision detection for obstacles
    private void OnCollisionEnter(Collision other) {
        Debug.Log("Collision Entered: " + other.gameObject);

        if (other.gameObject.CompareTag("obstacle")) {
            
            towallSfx.Play();

            // stick to the obstacle
            this.movement = new Vector3(0, 0, 0);
            this.rb.velocity = new Vector3(0, 0, 0);
            this.rb.angularVelocity = new Vector3(0, 0, 0);
            this.arrowObject.SetActive(true);

            // reset control
            this.isArrowRotating = true;
            this.isMoving = false;
        }
    }


    // trigger detection for bears and houses
    void OnTriggerEnter(Collider other) {

        Debug.Log("Trigger Entered: " + other);

        if (other.gameObject.CompareTag("edible")) {
            removeObject(other.gameObject);
            this.bearsEaten++;
            this.scoreText.GetComponent<Text>().text = "TEDDY BEARS EATEN: " + this.bearsEaten + "\nHOUSE LIT: " + this.houseLit;
            eatSfx.Play();
        } else if (other.gameObject.CompareTag("inedible")) {
            removeObject(this.gameObject);
            removeObject(this.arrowObject.gameObject);
            dieSfx.Play();
            endGame();
        } else if (other.gameObject.CompareTag("house")) {
            // light up the house (change the image)
            this.houseLit++;
            this.scoreText.GetComponent<Text>().text = "TEDDY BEARS EATEN: " + this.bearsEaten + "\nHOUSE LIT: " + this.houseLit;
            lightSfx.Play();
        }
    }

    private void endGame() {
        Debug.Log("game ends");
        SceneManager.LoadScene("GameScene");
    }

    private void removeObject(GameObject obj) {
        if (obj != null) {
            Destroy(obj, 0.0f);
        }
    }

}
