using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO.Ports;
using System;
using System.Threading;

public class BallCountrol : MonoBehaviour
{
    const string PORT = "COM3";
    const int BAUD_RATES = 115200;

    public UDPReceive udpReceive;

    public GameObject ballPos;

    SerialPort serialPort = new SerialPort(PORT, BAUD_RATES);

    string value;
    Thread myThread;
    float angle;

    void Start()
    {
        OpenPort();
    }

    // Update is called once per frame
    void Update()
    {
        float x = 0.0f;
        float y = 0.0f;
        float z = 0.0f;

        // Rotation
        float.TryParse(value, out angle);
        print(angle);
        // left
        if (angle < -20)
        {
            y = -0.05f;
        }
        // Right
        else if (angle > 20)
        {
            y = 0.05f;
        }
        else { }

        ballPos.transform.Rotate(0.0f, y, 0.0f);
        //Move
        string data = udpReceive.data;
        data = data.Remove(0, 1);
        data = data.Remove(data.Length - 1, 1);
        string[] points = data.Split(',');
        // print("8: " + points[24] + ", " + points[25] + ", " + points[26]);
        // print("5: " + points[15] + ", " + points[16] + ", " + points[17]);

        float differenceY = float.Parse(points[16]) - float.Parse(points[25]);
        float differenceX = float.Parse(points[15]) - float.Parse(points[24]);
        //print(differenceX);


        // backward
        if (differenceY > 150)
        {
            z = -0.001f;
        }
        // forward
        else if (differenceY < 0)
        {
            z = 0.001f;
        }
        // stay
        else
        {
        }


        //ballPos.transform.localPosition = ballPos.transform.localPosition + new Vector3(0.0f, 0.0f, z);
        ballPos.transform.Translate(0.0f, 0.0f, z);



        // if (float.Parse(points[26]) - float.Parse(points[17]) > -75) {
        //     print("move forward");
        //     ballPos.transform.localPosition = ballPos.transform.localPosition + new Vector3(x, y, z);
        // }
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
            //print(value);
        }
    }

}
