using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void QuitGame(){
       Debug.Log("QUIT THE GAME!");
       Application.Quit();
   }


   public void RestartGame(){
       Debug.Log("Restart!!!!");
   }
}
