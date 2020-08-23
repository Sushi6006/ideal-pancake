using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour {

    public PlayerController pc;

    void Start() {
            
    }
    
    public void pause() {
        pc.onPause();
    }

}
