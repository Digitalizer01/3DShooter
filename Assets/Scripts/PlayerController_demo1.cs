using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_demo1 : MonoBehaviour {

    public float speed = 6.0f;
    public float gravity = 9.8f;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

	void Start ()
    {
        controller = GetComponent <CharacterController>();	
	}

    void Update ()
    {
		if(controller.isGrounded)
            moveDirection = Vector3.forward * speed;

        moveDirection.y -= gravity * Time.deltaTime;

        controller.Move(moveDirection * Time.deltaTime);
	}
}
