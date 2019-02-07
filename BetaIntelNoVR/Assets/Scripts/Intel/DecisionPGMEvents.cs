using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionPGMEvents : MonoBehaviour {

    int currentState = 5;
    int previousState = 5;

    public string pgmSerialWrite = "0";

    void Update()
    {

        currentState = Intel.sharedInstance.ExerciseState;

        if (currentState == 0 && currentState != previousState)
        {
            if (Intel.sharedInstance.resistanceMode == false)
            {
                pgmSerialWrite = "1";
            }
            else
            {
                pgmSerialWrite = "0";
            }
        }


        if (currentState == 2 && currentState != previousState)
        {
            if (Intel.sharedInstance.resistanceMode == false)
            {
                pgmSerialWrite = "0";
            }
            else
            {
                pgmSerialWrite = "1";
            }

        }

        previousState = currentState;

    }
}
