using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerialControlBio : MonoBehaviour
{

    public SerialHandlerBio serialHandlerBio;

    void Start()
    {
        serialHandlerBio.OnDataReceived += OnDataReceived;
    }

    //void Updata()
    //{
    //    serialHandler.Write("0");
    //}

    void OnDataReceived(string message)
    {
        var data = message.Split(
                new string[] { "\t" }, System.StringSplitOptions.None);
        if (data.Length < 2) return;

        try
        {
            string bioSignals = string.Join("", data);
            Debug.Log(bioSignals);
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }
}
