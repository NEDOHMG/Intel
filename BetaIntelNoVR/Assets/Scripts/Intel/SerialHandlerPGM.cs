using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;

public enum SerialPortConnectionPGM
{
    COM3,
    COM4,
    COM5,
    COM6,
    COM7,
    COM8
}

public enum BaudRateValuePGM
{
    _9600,
    _38400,
    _115200
}

public class SerialHandlerPGM : MonoBehaviour
{

    // Create a serial object
    private SerialPort myData;

    // Actual SerialPort
    public SerialPortConnectionPGM portNamePGM = SerialPortConnectionPGM.COM3;
    private string _portNamePGM;

    // Baud rate
    public BaudRateValuePGM baudRatePGM = BaudRateValuePGM._115200;
    private int _baudRatePGM;

    // Create a thread
    private Thread thread_;
    private bool isRunning_ = false;

    // Message variables
    private string message_;
    private bool isNewMessageReceived_ = false;

    void Awake()
    {
        OpenPort();
    }

    void OnDestroy()
    {
        myData.Close();
    }

    string ChoosePort(SerialPortConnectionPGM port)
    {

        if (port == SerialPortConnectionPGM.COM3)
        {
            _portNamePGM = "COM3";
        }
        else if (port == SerialPortConnectionPGM.COM4)
        {
            _portNamePGM = "COM4";
        }
        else if (port == SerialPortConnectionPGM.COM5)
        {
            _portNamePGM = "COM5";
        }
        else if (port == SerialPortConnectionPGM.COM5)
        {
            _portNamePGM = "COM6";
        }
        else if (port == SerialPortConnectionPGM.COM5)
        {
            _portNamePGM = "COM7";
        }
        else if (port == SerialPortConnectionPGM.COM5)
        {
            _portNamePGM = "COM8";
        }

        return _portNamePGM;

    }

    int ChooseBaudRate(BaudRateValuePGM baudRatePGM)
    {

        if (baudRatePGM == BaudRateValuePGM._9600)
        {
            _baudRatePGM = 9600;
        }
        else if (baudRatePGM == BaudRateValuePGM._38400)
        {
            _baudRatePGM = 38400;
        }
        else if (baudRatePGM == BaudRateValuePGM._115200)
        {
            _baudRatePGM = 115200;
        }

        return _baudRatePGM;

    }

    // Use this for initialization
    void OpenPort()
    {

        myData = new SerialPort(ChoosePort(portNamePGM), ChooseBaudRate(baudRatePGM), Parity.None, 8, StopBits.One);

        myData.Open();
        isRunning_ = true;

    }

    private void Close()
    {
        isNewMessageReceived_ = false;
        isRunning_ = false;

        if (thread_ != null && thread_.IsAlive)
        {
            thread_.Join();
        }

        if (myData != null && myData.IsOpen)
        {
            myData.Close();
            myData.Dispose();
        }
    }

    public void Write(string message)
    {
        try
        {
            myData.Write(message);
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }
}
