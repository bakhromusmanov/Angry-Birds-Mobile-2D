using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bird : MonoBehaviour
{

    Camera mainCamera;
    Rigidbody2D currentRigidBody;
    Animator birdAnimator;

    [SerializeField] GameObject birdPrefab;
    public bool isDragging;

    private void Awake() 
    {
        
    }
    void Start()
    {
        mainCamera = Camera.main;
        currentRigidBody = birdPrefab.GetComponent<Rigidbody2D>();
        birdAnimator = birdPrefab.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentRigidBody==null) {return;}

        if(!Touchscreen.current.primaryTouch.press.isPressed)//check if user touches the screen
        {
            birdAnimator.SetBool("isLaunching", false);
            currentRigidBody.isKinematic = false;
            if (isDragging){LaunchBird();}
            isDragging=false;
            return;
        }

        isDragging=true;
        currentRigidBody.isKinematic=true;
        Vector2 touchScreenPosition = Touchscreen.current.primaryTouch.position.ReadValue(); //store the user value in Vector2
        Vector3 touchWorldPosition = mainCamera.ScreenToWorldPoint(touchScreenPosition); //to store user input in world position
        
        currentRigidBody.position = touchWorldPosition; //move the bird to whatever player touched
        birdAnimator.SetBool("isLaunching", true);
    }
    void LaunchBird()
    {
        currentRigidBody = null;
    }

}
