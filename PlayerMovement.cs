using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;
using UnityEngine.Events; 

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance; // can only be ONE instance of this play in entire unity

    public float moveSpeed;
    public float jumpForce;
    [SerializeField] private LayerMask m_WhatIsGround; // detemrine what is ground?
    [SerializeField] private Transform m_GroundCheck; // position marking where to check if player is grounded
    const float k_GroundedRadius = .2f; // radius to determine if grounded
    private bool m_Grounded; // Whether grounded or not

    private Rigidbody2D rb;
    private bool facingRight = true;
    private float moveDirection;
    private bool isJumping = false;
    public UnityEvent OnLandEvent; 
    // private GameObjec[]players;
    public Animator animator; 
   
    private void Awake()
    {
        //Awake is called after all objects are initalized. Called in a random order.
        rb = GetComponent<Rigidbody2D>(); // look for component on this GameObject of type RigidBody2d
        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent(); 
    }
    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null) // check if more than 1 player instance
        {
            Destroy(this.gameObject);
            return; 
        }
        Instance = this; //gameObject 
        DontDestroyOnLoad(this.gameObject);//when moving next scene Player won't be deleted.
    }

    // Update is called once per frame
    void Update()
    {
        //get inputs
        ProcessInputs();
        // animate
        Animate();
    }
   
    private void FixedUpdate() // handling physics
    {
        Move(); // Move
        bool wasGrounded = m_Grounded;
        m_Grounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke(); 
            }
        }
    }
    private void Animate()
    {
        if (moveDirection > 0 && !facingRight)
        {
            FlipCharacter();
        }
        else if (moveDirection < 0 && facingRight)
        {
            FlipCharacter();
        }
    }
    private void Move(){
        rb.velocity = new Vector2(moveDirection* moveSpeed, rb.velocity.y);
        if (m_Grounded && isJumping) // if player should jump :)
        {
            //rb.AddForce(new Vector2(0f, jumpForce));
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        isJumping = false;
       
    }
    private void ProcessInputs()
    {
        moveDirection = Input.GetAxis("Horizontal"); // scale of -1 
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jump button is pressed down.");
            isJumping = true;
            animator.SetBool("IsJumping", true);
        }
       
    }
    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }
    private void FlipCharacter()
    {
        facingRight = !facingRight; //inverse bool
                            transform.Rotate(0f, 180, 0f);
       // GetComponent<SpriteRenderer>().flipX = !facingRight;
    }
    private void OnLevelWasLoaded(int level)
    {
        FindStartPos();
        // players = GameObject.FindObjectsWithTag("Player)";
        // if(players.Length > 1)
        // Destroy(players[1]); // second player
    }
    void FindStartPos()
    {
        transform.position = GameObject.FindWithTag("StartPos").transform.position;
    }
}
