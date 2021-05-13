using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public static float sensitivityHor = 9f;
    public static float sensitivityVert = 9f;

    public float minmumVert = -60f;
    public float maxmumVert = 60f;

    public bool activeOpt = true;

    private float _rotationX = 0;
    // pour inisialisation
    void Start()
    {
        //print("Hello World");
    }

    // update pour chaque frame 
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            activeOpt = !activeOpt;

        }
        if (activeOpt)
        {
            
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            
            if (axes == RotationAxes.MouseX)
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
            }
            else if (axes == RotationAxes.MouseY)
            {
                _rotationX = _rotationX - Input.GetAxis("Mouse Y") * sensitivityVert;
                _rotationX = Mathf.Clamp(_rotationX, minmumVert, maxmumVert);

                float rotationY = transform.localEulerAngles.y;

                transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
            }
            else
            {
                _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
                _rotationX = Mathf.Clamp(_rotationX, minmumVert, maxmumVert);

                float delta = Input.GetAxis("Mouse X") * sensitivityHor;
                float rotationY = transform.localEulerAngles.y + delta;

                transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
            }
        }
        
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        

    }

}