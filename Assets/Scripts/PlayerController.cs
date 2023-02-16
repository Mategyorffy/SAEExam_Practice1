﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PlayerController : MonoBehaviour
{
    [SerializeField] Camera MainCam;
    [SerializeField] float rotationPower = 3f;
    [SerializeField] float walkSpeed = 1f;
    [SerializeField] float sprintSpeed = 3f;
    [SerializeField] Transform followTransform;
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody rigidbody;
    public LayerMask itemMask, playerMask;
    public List<GameObject> items;
    public Canvas invetoryCanvas;


    public bool IsItemInRange;

    Vector2 moveInput;
    Vector2 lookInput;
    float sprintInput;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        
    }

    private void FixedUpdate()
    {
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        lookInput = new Vector2(Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"));
        sprintInput = Input.GetAxis("Sprint");

        UpdateFollowTargetRotation();
     
        float speed = 0;

        speed = Mathf.Lerp(walkSpeed, sprintSpeed, sprintInput);
        Vector3 movement = (transform.forward * moveInput.y * speed) + (transform.right * moveInput.x * speed);
        rigidbody.velocity = new Vector3(movement.x, rigidbody.velocity.y, movement.z);

        animator.SetFloat("MovementSpeed", moveInput.y * (speed / walkSpeed));

        //only rotate the player when moving, allows user to look at the player when idle
        if (moveInput.magnitude > 0.01f)
        {
            //Set the player rotation based on the look transform
            transform.rotation = Quaternion.Euler(0, followTransform.eulerAngles.y, 0);
            //reset the y rotation of the look transform
            followTransform.localEulerAngles = new Vector3(followTransform.localEulerAngles.x, 0, 0);
        }

        Ray ray = MainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

         if (Physics.Raycast(ray, out hit, itemMask))
         {
            
            Collider colliderObj = hit.collider;
            Debug.Log($"{colliderObj.gameObject.name}, ITEM IS HIT");

            if(colliderObj.tag == "Item")
            {
                StartCoroutine(colliderObj.gameObject.GetComponentInParent<Items>().DisplayInfo());

                if (Input.GetKey(KeyCode.E))
                {
                    animator.SetTrigger("PickUp");
                    colliderObj.gameObject.GetComponentInParent<Items>().DestroyThis();
                    
                }
            }
         }

        DisplayInventory();
    }


    private void UpdateFollowTargetRotation()
    {
        //Update follow target rotation
        followTransform.rotation *= Quaternion.AngleAxis(lookInput.x * rotationPower, Vector3.up);
        followTransform.rotation *= Quaternion.AngleAxis(lookInput.y * rotationPower, Vector3.right);

        var angles = followTransform.localEulerAngles;
        angles.z = 0;
        var angle = angles.x;

        //Clamp the Up/Down rotation
        if (angle > 180 && angle < 340)
        {
            angles.x = 340;
        }
        else if (angle < 180 && angle > 40)
        {
            angles.x = 40;
        }
        followTransform.localEulerAngles = angles;
    }

    public void DisplayInventory()
    {

        if (Input.GetKey(KeyCode.Tab))
        {
           
            invetoryCanvas.gameObject.SetActive(true);
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            
            invetoryCanvas.gameObject.SetActive(false);
        }

    }



}
