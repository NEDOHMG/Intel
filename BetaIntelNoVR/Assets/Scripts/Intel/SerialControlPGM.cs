using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerialControlPGM : MonoBehaviour
{

    public SerialHandlerPGM serialHandlerPGM;
    public DecisionPGMEvents decisionPGMEvents;

    void Update()
    {
        serialHandlerPGM.Write(decisionPGMEvents.pgmSerialWrite);
    }

}


