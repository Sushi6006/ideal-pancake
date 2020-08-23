using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

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
    // TODO: sound to be added

    // scores
    private int bearsEaten = 0;
    private int houseLit = 0;
    public GameObject scoreText;


    // Start is called before the first frame update
    void Start() {

        Physics.gravity = new Vector3(0.0f, gravity, 0.0f);

        this.rb = GetComponent<Rigidbody>();
        this.isMoving = false;
        this.isArrowRotating = true;
    }


    // Update is called once per frame
    void Update() {
        if (this.isArrowRotating && (!this.isMoving)) {
            this.arrowObject.transform.Rotate((this.arrowAngle * Time.deltaTime) * Vector3.back);
        }

        // tapping part
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) ||  // touch screen
            (Input.GetMouseButtonDown(0))) {  // mouse button

            this.isArrowRotating = false;
            this.isMoving = true;
            this.movement = this.arrowObject.transform.up;

            this.arrowObject.SetActive(false);

        }
    }

    // Physical update
    private void FixedUpdate() {
        this.transform.position += movement * speed;
        this.arrowObject.transform.position = this.transform.position;
    }

    // for obstacles
    private void OnCollisionEnter(Collision other) {
        Debug.Log("Collision Entered: " + other.gameObject);

        if (other.gameObject.CompareTag("obstacle")) {
            
            // play sound

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


    // OnTriggerEnter is called when the Collider other enters the trigger.
    void OnTriggerEnter(Collider other) {

        Debug.Log("Trigger Entered: " + other);

        if (other.gameObject.CompareTag("edible")) {
            removeObject(other.gameObject);
            this.bearsEaten++;

            // TODO: add score text
        } else if (other.gameObject.CompareTag("inedible")) {
            removeObject(this.gameObject);
            removeObject(this.arrowObject.gameObject);
            // TODO: play sound
            endGame();
        } else if (other.gameObject.CompareTag("house")) {
            // light up the house (change the image)
            this.houseLit++;
            // TODO: add score text
        }
    }

    private void endGame() {
        // TODO: ends the game

        Debug.Log("game ends");
    }

    private void removeObject(GameObject obj) {
        if (obj != null) {
            Destroy(obj, 0.0f);
        }
    }

}
