using UnityEngine;
using System.IO.Ports;
using System.Threading;

public enum SerialPortConnectionBio
{
    COM3,
    COM4,
    COM5,
    COM6,
    COM7,
    COM8
}

public enum BaudRateValueBio
{
    _9600,
    _38400,
    _115200
}


public class SerialHandlerBio : MonoBehaviour
{
    public delegate void SerialDataReceivedEventHandler(string message);
    public event SerialDataReceivedEventHandler OnDataReceived;

    // Create a serial object
    private SerialPort myData;

    // Actual SerialPort
    public SerialPortConnectionBio portName = SerialPortConnectionBio.COM5;
    private string _portName;

    // Baud rate
    public BaudRateValueBio baudRate = BaudRateValueBio._115200; 
    private int _baudRate;

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

    void Update()
    {
        if (isNewMessageReceived_)
        {
            OnDataReceived(message_);
        }
        isNewMessageReceived_ = false;
    }

    void OnDestroy()
    {
        for (int i = 0; i < 5; i++) // To reset the microcontroller once the program is closed 
        {
            Debug.Log("1");
            myData.Write("1");
        }
        myData.Close();
    }

    string ChoosePort(SerialPortConnectionBio port)
    {

        if (port == SerialPortConnectionBio.COM3)
        {
            _portName = "COM3";
        }
        else if (port == SerialPortConnectionBio.COM4)
        {
            _portName = "COM4";
        }
        else if (port == SerialPortConnectionBio.COM5)
        {
            _portName = "COM5";
        }

        return _portName;

    }

    int ChooseBaudRate(BaudRateValueBio baudRate)
    {

        if (baudRate == BaudRateValueBio._9600)
        {
            _baudRate = 9600;
        }
        else if (baudRate == BaudRateValueBio._38400)
        {
            _baudRate = 38400;
        }
        else if (baudRate == BaudRateValueBio._115200)
        {
            _baudRate = 115200;
        }

        return _baudRate;

    }

    // Use this for initialization
    void OpenPort()
    {
        // This will just print the devices connected in the port 
        //foreach (string str in SerialPort.GetPortNames())
        //{
        //    Debug.Log(string.Format("port : {0}", str));
        //}

        myData = new SerialPort(ChoosePort(portName), ChooseBaudRate(baudRate), Parity.None, 8, StopBits.One);

        myData.Open();
        isRunning_ = true;

        thread_ = new Thread(Read);
        thread_.Start();

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

    void Read()
    {
        while (isRunning_ && myData != null && myData.IsOpen)
        {
            try
            {
                message_ = myData.ReadLine();
                isNewMessageReceived_ = true;
                Debug.Log(message_);
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e.Message);
            }
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

