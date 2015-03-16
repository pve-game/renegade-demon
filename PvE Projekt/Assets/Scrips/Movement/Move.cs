using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

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

        Vector3 mv = transform.forward * m_actSpeed;
        Vector3 movement = new Vector3(mv.x, m_ySpeed, mv.z) + transform.right * h;
        m_charController.Move(movement * Time.deltaTime);


        m_isGrounded = (transform.position.y < m_groundNormal + m_charController.stepOffset) &&
                       (transform.position.y > m_groundNormal - m_charController.stepOffset);

        Vector3 mouse = Input.mousePosition;
        Ray mouseRay = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;

        Vector3 mouseW = transform.position;
        if (Physics.Raycast(mouseRay, out hit))
            mouseW = hit.point;

        mouseW.y = transform.position.y;

        Vector3 dir = (mouseW - transform.position).normalized;

        if (dir != Vector3.zero && !(hit.transform.gameObject.layer == LayerMask.NameToLayer("Player")))
            m_targetRotation = Quaternion.LookRotation(dir);

        transform.rotation = Quaternion.Slerp(transform.rotation,
            m_targetRotation, dampingSpeed * Time.deltaTime);


    }
}
