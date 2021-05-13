using System;
using UnityEngine;
using System.Collections;
public class Move : MonoBehaviour
{
    public static float speed = 0.2f;

    void FixedUpdate()
    {
        // Move Avancer et reculer
        if (Input.GetKey(KeyCode.Z))
            transform.position += Vector3.ClampMagnitude(transform.forward, speed);
        else if (Input.GetKey(KeyCode.S))
            transform.position -= Vector3.ClampMagnitude(transform.forward, speed);

        // Move gauche et  droite
        if (Input.GetKey(KeyCode.Q))
            transform.position -= Vector3.ClampMagnitude(transform.right, speed);
        else if (Input.GetKey(KeyCode.D))
            transform.position += Vector3.ClampMagnitude(transform.right, speed);

        // Move haut et bas
        if (Input.GetKey(KeyCode.Space))
            transform.position += new Vector3(0, speed, 0);
        else if (Input.GetKey(KeyCode.LeftShift))
            transform.position -= new Vector3(0, speed, 0);


    }
    void OnGUI()
    {
        GUI.Label(new Rect(10, 15, 300, 100), "x:" + transform.position.x + "\ny:" + transform.position.y + "\nz:" + transform.position.z);
        GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 100, 100), "O");
        //GUI.Label(new Rect(Screen.width/2,0,300,100),"x:"+transform.forward.x+"\ny:"+transform.forward.y+"\nz:"+transform.forward.z);
    }

    
}