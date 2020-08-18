using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed = 1;

    private Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start() {
        this.rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
        // this.rigidbody.AddForce(movement * speed);
        this.transform.position += movement * speed;
    }
}
