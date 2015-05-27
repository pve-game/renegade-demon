using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{
    Animator anim = null;

    public float speed = 3.0f;
    public float acceleration = 8.5f;
    public float gravity = -9.81f;
    public float jumpSpeed = 50.0f;
    public float dampingSpeed = 5.0f;

    Quaternion m_targetRotation = Quaternion.identity;
    CharacterController m_charController;

    float m_actSpeed = 0.0f;
    float m_ySpeed = 0.0f;
    float m_groundNormal = 0.0f;
    bool m_isGrounded = false;

    private float runSpeed = 0.0f;
    private float walkSpeed = 0.0f;
    private float h = 0.0f;
    private float v = 0.0f;
    private bool run = false;
    public float H {private get; set;}
    public bool Run {private get; set;}
    public float V {private get; set;}



    private void Start()
    {
        m_charController = GetComponent<CharacterController>();
        m_targetRotation = transform.rotation;
        anim = GetComponent<Animator>();
        runSpeed = speed * 2;
        walkSpeed = speed;
    }

    private void Update()
    {

        anim.SetFloat("speed", v);
        anim.SetBool("running", Run);
        anim.SetFloat("direction", h);
        h = H;
        v = V;
        if (Run && v>0 && (h>-0.2||h<0.2))
            speed = runSpeed;
        else
            speed = walkSpeed;
        Rotation();
        if (!m_charController.isGrounded)
        {
            m_ySpeed += (gravity) * Time.deltaTime;
        }
        if (m_isGrounded)
        {
            h *= speed;
            v *= speed;
            anim.SetBool("jump", false);
        }
        if (!m_isGrounded)
        {
            anim.SetFloat("speed", 0.0f);
        }    
        if (m_charController.isGrounded)
        {
            m_ySpeed = 0.0f;
            m_groundNormal = transform.position.y;
        }

        if (Input.GetButtonDown("Jump") && m_isGrounded)
        {
            m_ySpeed += jumpSpeed;
            anim.SetBool("jump", true);
        }

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
