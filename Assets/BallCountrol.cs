using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCountrol : MonoBehaviour
{
    // Start is called before the first frame update
    public UDPReceive udpReceive;
    public GameObject ballPos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string data = udpReceive.data;
        data = data.Remove(0, 1);
        data = data.Remove(data.Length - 1, 1);
        string[] points = data.Split(',');
        // print("8: " + points[24] + ", " + points[25] + ", " + points[26]);
        // print("5: " + points[15] + ", " + points[16] + ", " + points[17]);

        string state;
        float differenceY = float.Parse(points[16]) - float.Parse(points[25]);
        float differenceX = float.Parse(points[15]) - float.Parse(points[24]);
        print(differenceX);
        

        
        
        

        float x = 0.0f;
        float y = 0.0f;
        float z = 0.0f;

        // backward
        if (differenceY > 140) {
            z = -0.001f;
            if (differenceX > 40) {
                x = 0.001f;
            } else if (differenceX < -30) {
                x = -0.001f;
            } else {
                x = 0.0f;
            }
        } 
        // forward
        else if (differenceY < 0) {
            z = 0.001f;
            if (differenceX > 40) {
                x = 0.001f;
            } else if (differenceX < -30) {
                x = -0.001f;
            } else {
                x = 0.0f;
            }
        } 
        // stay
        else {
            if (differenceX > 40) {
                x = 0.001f;
            } else if (differenceX < -30) {
                x = -0.001f;
            } else {
                x = 0.0f;
            }
        }
        ballPos.transform.localPosition = ballPos.transform.localPosition + new Vector3(x, y, z);
       


        // if (float.Parse(points[26]) - float.Parse(points[17]) > -75) {
        //     print("move forward");
        //     ballPos.transform.localPosition = ballPos.transform.localPosition + new Vector3(x, y, z);
        // }
    }
}
