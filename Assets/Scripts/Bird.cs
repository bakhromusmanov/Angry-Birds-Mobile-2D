using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bird : MonoBehaviour
{
    [SerializeField] GameObject bird1Prefab;
    [SerializeField] GameObject bird2Prefab;
    [SerializeField] Rigidbody2D pivot;
    [SerializeField] float detachDelay;
    [SerializeField] float respawnDelay;

    Camera mainCamera;
    Rigidbody2D currentRigidBody;
    Animator birdAnimator;
    SpringJoint2D currentSpring;
    bool isDragging;
    GameObject[] Birds = new GameObject[2];
    GameObject birdInstance;
    


    void Awake()
    {
        mainCamera = Camera.main;
        Birds[0] =bird1Prefab;
        Birds[1] =bird2Prefab;
        SpawnBird();
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
        Invoke(nameof(DetachSpring), detachDelay);
    }

    void DetachSpring ()
    {
        birdAnimator.SetBool("isLaunched", true);
        currentSpring.enabled = false;
        currentSpring = null;
        Destroy(birdInstance,3f);
        Invoke(nameof(SpawnBird), respawnDelay);
    }
    
    void SpawnBird()
    {
        birdInstance = Instantiate(Birds[Random.Range(0,2)], pivot.transform.position-new Vector3(2,2,0), Quaternion.identity, GameObject.Find("Birds").transform);
        currentRigidBody = birdInstance.GetComponent<Rigidbody2D>();
        birdAnimator = birdInstance.GetComponent<Animator>();

        currentSpring = birdInstance.GetComponent<SpringJoint2D>();
        currentSpring.enabled = true;
        currentSpring.connectedBody=pivot;

        birdAnimator = birdInstance.GetComponent<Animator>();
    }

}
