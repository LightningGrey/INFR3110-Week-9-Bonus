using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    public bool isGrounded;
    [SerializeField] private float horizontalForce;
    [SerializeField] private float verticalForce;
    
    private Rigidbody2D m_rigidBody2D;
    private SpriteRenderer m_spriteRenderer;
    private Animator m_animator;

    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidBody2D = GetComponent<Rigidbody2D>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isGrounded)
        {
            m_rigidBody2D.AddForce(direction * horizontalForce * Time.deltaTime);
        }
        
        if (Mathf.Abs(direction.x) < 0.1f) 
        {
            m_animator.SetInteger("AnimState", 0);
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        //Debug.Log("Move Activated");
        direction = context.ReadValue<Vector2>();

        switch (context.control.name)
        {
            case "a":
            case "leftArrow":
                m_spriteRenderer.flipX = true;
                //m_rigidBody2D.AddForce(Vector2.left * horizontalForce);
                break;
            case "d":
            case "rightArrow":
                m_spriteRenderer.flipX = false;
                //m_rigidBody2D.AddForce(Vector2.right * horizontalForce);
                break;
        }

        m_animator.SetInteger("AnimState", 1);
       
    }

    private void _MoveLeft()
    {
        m_rigidBody2D.AddForce(Vector2.left * horizontalForce);
    }
    private void _MoveRight()
    {
        m_rigidBody2D.AddForce(Vector2.right * horizontalForce);
    }


    public void OnJump(InputAction.CallbackContext context)
    {
        m_rigidBody2D.AddForce(Vector2.up * verticalForce);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        isGrounded = true;
    }

    public void OnCollisionExit2D(Collision2D other)
    {
        isGrounded = false;
    }
}
