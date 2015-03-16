using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

    CharacterController controller;

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 forward = Vector3.zero;
    private Vector3 right = Vector3.zero;

	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {

        forward = transform.forward;
        right = new Vector3(forward.z, 0, -forward.x);

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 targetDirection = horizontalInput * right + verticalInput * forward;

        moveDirection = Vector3.RotateTowards(moveDirection, targetDirection, 200 * Mathf.Deg2Rad * Time.deltaTime, 1000);

        Vector3 movement = moveDirection * Time.deltaTime * 2;
        controller.Move(movement);

        if (targetDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }
	}

}
