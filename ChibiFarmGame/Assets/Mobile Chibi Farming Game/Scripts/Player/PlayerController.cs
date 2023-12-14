using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    [Header(" Elements ")]
    [SerializeField] private MobileJoystick joystick;
    private CharacterController characterController;

    [Header(" Settings ")] 
    [SerializeField] private float moveSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        ManageMovement();
    }

    private void ManageMovement()
    {
        Vector3 moveVector = joystick.GetMoveVector() * moveSpeed * Time.deltaTime / Screen.width;

        moveVector.z = moveVector.y;
        moveVector.y = 0;
        
        characterController.Move(moveVector);
    }
}
