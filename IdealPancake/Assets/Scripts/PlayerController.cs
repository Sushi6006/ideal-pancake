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

    }

    void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
        // this.rigidbody.AddForce(movement * speed);
        this.transform.position += movement * speed;
        this.arrowObject.transform.position = this.transform.position;
    }
}
