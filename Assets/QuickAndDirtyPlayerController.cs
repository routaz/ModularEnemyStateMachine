using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuickAndDirtyPlayerController : MonoBehaviour
{
    private Rigidbody2D                 rigidbody2D;

    [SerializeField] private float      movespeed;
    [SerializeField] private float      jumpForce;
    public bool                        isGrounded;
    [SerializeField] private float      groundCheckLenght;
    [SerializeField] private LayerMask  groundLayer;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        
        var moveDirection = Mathf.Sign(Input.GetAxis("Horizontal"));
        var movementX = 0f;
        
        if (Input.GetAxis("Horizontal") != 0)
        {
            movementX = moveDirection * movespeed * Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                HandleJump();
            }
        }

        rigidbody2D.velocity = new Vector2(movementX, rigidbody2D.velocity.y);

    }

    private void HandleJump()
    {
        rigidbody2D.AddForce(Vector2.up * jumpForce,ForceMode2D.Impulse); 
    }
    
    public bool GroundCheck()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckLenght, groundLayer);
        return isGrounded;
    }
    
}
