using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_demo2 : MonoBehaviour {

    public float speed = 6.0f;
    public float gravity = 9.8f;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController m_CharacterControler;

	void Start ()
    {
        m_CharacterControler = GetComponent <CharacterController>();	
	}

    void Update ()
    {
		if(m_CharacterControler.isGrounded)
            moveDirection = Vector3.forward * speed;

        moveDirection.y -= gravity * Time.deltaTime;

        m_CharacterControler.Move(moveDirection * Time.deltaTime);
	}


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        // don't move the rigibody if the character is on top of it
        if (body == null || body.isKinematic)
            return;
        
        body.AddForceAtPosition(m_CharacterControler.velocity,
                                hit.point,
                                ForceMode.Impulse);      
    }
}
