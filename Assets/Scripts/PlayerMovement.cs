using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Components
    private Rigidbody2D rb;
    private SpriteRenderer playerSprite;
    private GameObject playerFeet;//For ground check
    private Animator spriteAnim;

    //Ground check layer
    private LayerMask groundLayer;
    
    //Movement
    [Header("Movement")]
    [SerializeField]
    private float speed;

    [SerializeField]
    private float jumpForce;

    //Inputs
    private float vAxis = 0;
    private float hAxis = 0;
    private bool jumpInput;

    
    private Vector3 jumpVector;



    // Start is called before the first frame update
    void Start()
    {
        jumpInput = false;
        rb = GetComponent<Rigidbody2D>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
        spriteAnim = GetComponentInChildren<Animator>();
        //playerSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        playerFeet = transform.GetChild(1).gameObject;
        groundLayer = LayerMask.GetMask("Ground");

    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
        MovePlayer();
        CalculateJump();
        ChangeAnim();
    }

    private void FixedUpdate()
    {
        Jump();
    }
    /// <summary>
    /// Print the vertical and horizontal axis from Input
    /// </summary>
    private void GetInputs()
    {
        vAxis = Input.GetAxisRaw("Vertical");
        hAxis = Input.GetAxisRaw("Horizontal");
        jumpInput = Input.GetAxisRaw("Jump") > 0;

        //print("h:" + hAxis + " v:" + vAxis + " jumping:" + jumpInput);
    }
    /// <summary>
    /// Uses axis and speed to move player
    /// </summary>
    private void MovePlayer()
    {
        //rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);
        transform.position += Vector3.right * hAxis * speed * Time.deltaTime;
        //Change player sprite flip with horizontal movement
        if (hAxis != 0)
        {
            playerSprite.flipX = Input.GetAxis("Horizontal") < 0;
        }
    }
    /// <summary>
    /// Check if the player is grounded
    /// </summary>
    /// <returns>
    /// is grounded
    /// </returns>
    private bool IsGrounded()
    {
        Debug.DrawRay(playerFeet.transform.position, Vector2.down*0.5f, Color.red);
        return Physics2D.Raycast(playerFeet.transform.position, Vector2.down,0.5f,groundLayer);
    }

    /// <summary>
    /// Calculate the vector 3 for the jump
    /// </summary>
    private void CalculateJump()
    {
        if (jumpInput && IsGrounded())
        {
            jumpVector = Vector3.up * jumpForce;
        } else
        {
            jumpVector = Vector3.zero;
        }
    }
    /// <summary>
    /// Generate the physicis jump movement
    /// </summary>
    private void Jump()
    {
        rb.AddForce(jumpVector, ForceMode2D.Impulse);
    }
    /// <summary>
    /// Change the animation in order to action
    /// </summary>
    private void ChangeAnim()
    {
        //Jumping
        if (!IsGrounded())
        {
            spriteAnim.Play("PlayerJump");
        } else if(IsGrounded() && hAxis != 0) 
        {
            spriteAnim.Play("PlayerRun");
        } else if(IsGrounded() && hAxis == 0)
        {
            spriteAnim.Play("PlayerIdle");
        }
    }

    //This would go on its own code but I put it in here to make it quicker
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("PowerUp"))
        {
            Destroy(collision.gameObject);
            GameManager.instance.GetPoint(1);
        }
    }
}
