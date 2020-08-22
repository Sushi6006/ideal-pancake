using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // player physics
    public float speed = 1;

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
    // TODO: scores to be added


    // Start is called before the first frame update
    void Start() {
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

        }
    }

    // physical update: 
    //   - arrow keys
    private void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        this.transform.position += movement * speed;
        this.arrowObject.transform.position = this.transform.position;

    }

    // for obstacles
    private void OnCollisionEnter(Collision other) {
        Debug.Log("entered: " + other.gameObject);

        if (other.gameObject.CompareTag("obstacle")) {
            
            // play sound???

            // stick to the obstacle
            this.rb.velocity = new Vector3(0, 0, 0);
            this.rb.angularVelocity = new Vector3(0, 0, 0);

            // reset control
            this.isArrowRotating = true;
            this.isMoving = false;
        }
    }

}
