using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bird : MonoBehaviour
{

    Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(!Touchscreen.current.press.isPressed)//check if user touches the screen
        {return;}

        Vector2 touchScreenPosition = Touchscreen.current.primaryTouch.position.ReadValue(); //store the user value in Vector2
        Vector3 touchWorldPosition = mainCamera.ScreenToWorldPoint(touchScreenPosition); //to store user input in world position

    }
}
