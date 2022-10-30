using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;
using System.Threading;


public class ArduinoPort : MonoBehaviour
{
    const string PORT = "COM3";
    const int BAUD_RATES = 115200;
    SerialPort serialPort = new SerialPort(PORT, BAUD_RATES);

    public string value;
    Thread myThread;
    float m;

    public string data;


    // open the port 
    void Start()
    {
        OpenPort();
    }

    void Update()
    {
        ReadData();
    }


    public void OpenPort()
    {
        // check whether port is open 
        try
        {
            serialPort.Open();
            myThread = new Thread(new ThreadStart(GetArduino)); //物件宣告及呼叫GetArduino
            myThread.Start(); //這邊用thread，類似開啟另外工作站處理收集資料
        }
        catch (Exception e)
        {
            print(e.Message);
        }
    }

    public void ClosePort()
    {
        try
        {
            serialPort.Close();
            myThread.Abort();
        }
        catch (Exception ex)
        {
            print(ex.Message);
        }
    }

    void OnApplicationQuit()
    {
        serialPort.Close();
        myThread.Abort();
    }

    private void GetArduino()
    {
        while (myThread.IsAlive && serialPort.IsOpen)
        {
            value = serialPort.ReadLine();
            print(value);
        }
    }

    public void ReadData()
    {

        float.TryParse(value, out m);
        //print(m);
        // m = m / 5 + 1;
        // this.transform.localScale = Vector3.Lerp(this.transform.localScale, new Vector3(m, m, m), Time.deltaTime); //必須把Script放置在某遊戲物件上
        // string a = serialPort.ReadExisting();
            // data = a;
            // print(data);
            // //valueText.text = a;
            // b = "a";
            // Thread.Sleep(100);              // delay 0.5s to read the data

    }


}