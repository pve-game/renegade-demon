using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{

    public float speed = 3.0f;
    public float acceleration = 8.5f;
    public float gravity = -9.81f;
    public float jumpSpeed = 20.0f;
    public float dampingSpeed = 5.0f;

    Quaternion m_targetRotation = Quaternion.identity;
    CharacterController m_charController;

    float m_actSpeed = 0.0f;
    float m_ySpeed = 0.0f;
    float m_groundNormal = 0.0f;
    bool m_isGrounded = false;

    float h = 0.0f;
    float v = 0.0f;


    private void Start()
    {
        m_charController = GetComponent<CharacterController>();
        m_targetRotation = transform.rotation;
    }

    private void Update()
    {
        Rotation();
        if (!m_charController.isGrounded)
        {
            m_ySpeed += gravity * Time.deltaTime;
        }
        m_isGrounded = (transform.position.y < m_groundNormal + m_charController.stepOffset) &&
                      (transform.position.y > m_groundNormal - m_charController.stepOffset);
    }

    public void Movement(float h, float v)
    {
        if (m_isGrounded)
        {
            h *= speed;
            v *= speed;
        }

        if (m_charController.isGrounded)
        {
            m_ySpeed = 0.0f;
            m_groundNormal = transform.position.y;
        }
        
        if (Input.GetButtonDown("Jump") && m_isGrounded)
            m_ySpeed += jumpSpeed;

        Vector3 mv = transform.forward * v;
        Vector3 movement = new Vector3(mv.x, m_ySpeed, mv.z) + transform.right * h;
        m_charController.Move(movement * Time.deltaTime);

        m_isGrounded = (transform.position.y < m_groundNormal + m_charController.stepOffset) &&
                      (transform.position.y > m_groundNormal - m_charController.stepOffset);
    }


    public void Rotation()
    {
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * 50 * Time.deltaTime);
    }

}
