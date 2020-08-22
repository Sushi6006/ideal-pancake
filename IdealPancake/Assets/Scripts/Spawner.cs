using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{   

    /* Logic to be implemented
     * generate in the coming block
     * delete if passed certain block
     * each block ends with a house - to be generated
    */

    public int blockSize = 60;

    // amount of each species to be generated
    public int MIN_AMOUNT = 10;
    public int MAX_AMOUNT = 20;

    public GameObject edibleBear;
    public GameObject inedibleBear;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {

        // generate a new block (if needed)

        // delete passed blocks
        
    }

    private void generateBlock() {

        // generate amount of each type

        // generate positios of each bear

        // generate GameObjects

        // put on houses

    }

}
