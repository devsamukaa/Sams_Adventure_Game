using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicsPlayerController : MonoBehaviour
{

    private CollisionController         coll;
    private Rigidbody2D                 playerRigidBody;
    public  Transform                   groundCheck;
    private Animator                    playerAnimator;

    [Space]
    [Header("Stats")]
    public float                    speed = 10;
    public float                    jumpForce = 50;
    public float                    slideSpeed = 5;
    public float                    wallJumpLerp = 50;

    [Space]
    [Header("Boleans")]
    public bool                     wallJumped;
    public bool                     wallGrab;
    public bool                     canMove;
    public bool                     wallSlide;
    public bool                     isGrounded;
    public bool                     wallSliding;
    public bool                     isColiderInBridgeWood;

    [Space]
    [Header("Variables")]
    float                           x;
    float                           xRaw;
    public int                      side = 1;
    Vector2                         dir;
    public int                      maxJumps = 2;
    public int                      numberJumps = 0; 

    public bool                     facingRight = true;

    void Start()
    {
        coll = GetComponent<CollisionController>();
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        canMove = true;
    }

    void Update()
    {
        x = Input.GetAxis("Horizontal");
        xRaw = Input.GetAxisRaw("Horizontal");
        dir = new Vector2(x , 0);

       if(x != 0)
       {
           Walk(dir);
       }
        
        isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        playerAnimator.SetBool("isGrounded", isGrounded);
        PlayAnimations();

        if(coll.onWall && !isGrounded && playerRigidBody.velocity.y < 0 && !isColiderInBridgeWood)
        { 

            wallSliding = true;
            numberJumps = 0;

            if(wallSliding)
            {
                WallSlide();
                playerAnimator.SetBool("onWall", true);
            }
            
        }else{
            playerAnimator.SetBool("onWall", false);
            wallSliding = false;
        }

        if(coll.onWall)
        {
            wallJumped = false;
        }else{
            wallJumped = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (coll.onWall && !isGrounded && numberJumps < maxJumps && !isColiderInBridgeWood)
                WallJump();

            else if (numberJumps < maxJumps )
                Jump(Vector2.up, false);
        }

        if(isGrounded){
            wallJumped = false;
            numberJumps = 0;
        }

        if(x > 0)
        {
            side = 1;
        }
        if (x < 0)
        {
            side = -1;
        }
    }

    private void Walk(Vector2 dir)
    {
        if (!canMove)
            return;

        if (!wallJumped)
        {
            playerRigidBody.velocity = new Vector2(dir.x * speed, playerRigidBody.velocity.y);
        }
        else
        {
            playerRigidBody.velocity = Vector2.Lerp(playerRigidBody.velocity, (new Vector2(dir.x * speed, playerRigidBody.velocity.y)), wallJumpLerp * Time.deltaTime);
        }

        if(x < 0 && facingRight || (x > 0 && !facingRight)){
            Flip();
        }
    }

    private void WallSlide()
    {
        if (!canMove)
            return;

        bool pushingWall = false;
        if((playerRigidBody.velocity.x > 0 && coll.onRightWall) || (playerRigidBody.velocity.x < 0 && coll.onLeftWall))
        {
            pushingWall = true;
        }
        float push = pushingWall ? 0 : playerRigidBody.velocity.x;

        playerRigidBody.velocity = new Vector2(push, -slideSpeed);
    }

    private void Jump(Vector2 dir, bool wall)
    {

        playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, 0);
        playerRigidBody.velocity += dir * jumpForce;
        Invoke("DelayAddJump", 0.1f);
    }

    private void WallJump()
    {
        if(xRaw == 0)
        {
            Flip();
        }
        
        playerRigidBody.velocity = new Vector2(0,0);
        StopCoroutine(DisableMovement(0));
        StartCoroutine(DisableMovement(.1f));

        Vector2 wallDir = coll.onRightWall ? Vector2.left : Vector2.right;

        Jump((Vector2.up / 0.8f + wallDir / 1.5f), true);

        wallJumped = true;
    }

    IEnumerator DisableMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    void PlayAnimations()
    {
        playerAnimator.SetFloat("velocityY", playerRigidBody.velocity.y);
        playerAnimator.SetBool("walking", x != 0f && isGrounded );
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void DelayAddJump()
    {
        numberJumps++;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        
        if(other.gameObject.CompareTag("BridgeWood")){
            isColiderInBridgeWood = true;
        }

    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.CompareTag("BridgeWood")){
            Invoke("DisableColisionBridgeWood", 1f);
        }
    }

    void DisableColisionBridgeWood(){
        isColiderInBridgeWood = false;
    }
}
