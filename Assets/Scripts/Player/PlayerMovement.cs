using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script to handle the movment of the player
public class PlayerMovement : MonoBehaviour
{
    //Reference to CharacterController component in player
    private CharacterController m_characterController;
    //
    private Vector3 m_velocity = Vector3.zero;
    //Maximum speed of the player for movement
    public float Speed = 1f;
    //Maximum height value for the player
    public float JumpHeight = 3f;

    void Start() {
        m_characterController = GetComponent<CharacterController>();
    }

    //Method to process the movement input's in the player
    public void ProcessMovement(Vector2 input) {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;

        m_characterController.Move(Speed * Time.deltaTime * transform.TransformDirection(moveDirection));
        m_velocity += Physics.gravity * Time.fixedDeltaTime;
        if (m_characterController.isGrounded && m_velocity.y < 0) {
            m_velocity.y = -1f;
        }
        m_characterController.Move(m_velocity * Time.fixedDeltaTime);
    }

    //Method to process when the jump input is activated
    public void Jump() {
        if (m_characterController.isGrounded) {
            m_velocity.y = Mathf.Sqrt(JumpHeight * -JumpHeight * Physics.gravity.y);
        }
    }
}
