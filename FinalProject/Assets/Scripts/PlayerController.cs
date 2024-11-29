using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //rotation
    float mouseX;
    float mouseY;
    public GameObject cameraTurn;
    float cameraRange;
    public float lookSpeed;
    //movement
    CharacterController cc;
    float moveSpeed;
    float moveX;
    float moveZ;
    Vector3 locarDirection;
    //gravity&jump
    float radiusOfGroundCheck;
    public LayerMask groundLayerMask;
    public Transform hightOfSphearAboveGround;
    public bool groundCheck;
    float gravity;
    Vector3 gravityMove;
    //shooting
    Vector3 origin;
    RaycastHit hit;
    float maxDistance;
    

    void Start()
    {
        lookSpeed = 200f;
        moveSpeed = 50;
        cc = GetComponent<CharacterController>();
        groundCheck = false;
        radiusOfGroundCheck = 0.5f;
        gravity = -9.81f;
        maxDistance = 500f;
    }

    // Update is called once per frame
    void Update()
    {
        Rotation();
        Movement();
        GravityControl();

    }

    void Rotation()
    {

        mouseX = Input.GetAxis("Mouse X") * lookSpeed * Time.deltaTime;
        transform.Rotate(0, mouseX, 0);
        mouseY = Input.GetAxis("Mouse Y") * lookSpeed * Time.deltaTime;
        cameraRange -= mouseY;
        cameraRange = Mathf.Clamp(cameraRange, -45, 45);
        cameraTurn.transform.localRotation = Quaternion.Euler(cameraRange, 0, 0);
    }

    void Movement()
    {
        if (!HealthBar.Instance.isAlive)
        {
            return;
        }
        moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        moveZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        locarDirection = transform.forward * moveZ + transform.right * moveX;
        cc.Move(locarDirection);
        
        

    }

    void GravityControl()
    {
        if (Physics.CheckSphere(hightOfSphearAboveGround.position, radiusOfGroundCheck, groundLayerMask))
        {
            groundCheck = true;
        }
        else
        {
            groundCheck = false;

        }

        if (!groundCheck)
        {
            gravityMove.y += gravity * Time.deltaTime;
        }
        else
        {
            gravityMove.y = 0;
        }
        if (groundCheck  && Input.GetButtonDown("Jump"))
        {
            gravityMove.y += 5;

        }
        cc.Move(gravityMove * Time.deltaTime);
    }

    void ShootingControlles()
    {
        if(Input.GetMouseButton(0))
        {
            origin = cameraTurn.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));

            if(Physics.Raycast(origin,cameraTurn.transform.forward,out hit,maxDistance))
            {
                hit.transform.GetComponent<MeshRenderer>().material.color = Color.red;
            }
        }
    }
}
