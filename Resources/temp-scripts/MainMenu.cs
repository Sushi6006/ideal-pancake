using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartPlay(){
        Debug.Log("START THE GAME");
    }

    public void QuitGame(){
       Debug.Log("QUIT THE GAME!");
       Application.Quit();
   }
}
