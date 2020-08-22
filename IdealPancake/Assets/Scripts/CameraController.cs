using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start() {
        this.offset = this.transform.position - player.transform.position;
    }

    // LateUpdate is called every frame, if the Behaviour is enabled.
    // It is called after all Update functions have been called.
    void LateUpdate() {
        if (player != null) {
            // this.transform.position = this.offset + new Vector3(
            //     player.transform.position.x,
            //     player.transform.position.y,
            //     player.transform.position.z);
            this.transform.position = this.offset + new Vector3(player.transform.position.x, 0.0f, 0.0f);
        }
    }
}
