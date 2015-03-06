using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

    public float speed = 3.0f;
    public float acceleration = 8.5f;
    public float gravity = -9.81f;
    public float jumpSpeed = 20.0f;

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
    }

    private void Update()
    {
        if (m_charController.isGrounded)
        {
            h = Input.GetAxis("Horizontal") * speed;
            v = Input.GetAxis("Vertical");
        }
        if ((v > 0.1f || v < -0.1f) && m_actSpeed <= speed && m_actSpeed >= -speed)
        {
            m_actSpeed += v * acceleration * Time.deltaTime;
        }
        else
        {
            if (m_actSpeed > 0.1f)
            {
                m_actSpeed -= acceleration * Time.deltaTime;
            }
            else if (m_actSpeed < -0.1f)
            {
                m_actSpeed += acceleration * Time.deltaTime;
            }
            else
            {
                m_actSpeed = 0.0f;
            }
        }
        if (m_charController.isGrounded)
        {
            m_ySpeed = 0.0f;
            m_groundNormal = transform.position.y;
        }
        else
            m_ySpeed += gravity * Time.deltaTime;

        if (Input.GetButtonDown("Jump") && m_isGrounded)
            m_ySpeed += jumpSpeed;

        m_charController.Move(new Vector3(h, m_ySpeed, m_actSpeed) * Time.deltaTime);

        m_isGrounded = (transform.position.y < m_groundNormal + m_charController.stepOffset) &&
                       (transform.position.y > m_groundNormal - m_charController.stepOffset);
    }
}
