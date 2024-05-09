using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringDetach : MonoBehaviour
{
    SpringJoint2D currentSpring;
    Animator animator;
    Bird bird;
    private void Awake() {
        
    }
    private void Start() {
        currentSpring = GetComponent<SpringJoint2D>();
        animator = GetComponent<Animator>();
        bird = FindObjectOfType<Bird>();
    }
     private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Catapult")
        {
            if(currentSpring!=null && !bird.isDragging)
            {
                animator.SetBool("isLaunched", true);
                DetachSpring ();
            }
        }
    }
    void DetachSpring ()
    {
        currentSpring.enabled = false;
        currentSpring = null;
    }
}
