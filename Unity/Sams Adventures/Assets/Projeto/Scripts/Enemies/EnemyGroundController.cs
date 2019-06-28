using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroundController : MonoBehaviour
{
    
    private GameObject player;
    private Animator myAnimator;
    private Rigidbody2D myRigidBody;
    private BoxCollider2D myCollider;
    private HorizontalSuspenseGroundMove myMovementController;
    public CollisionController collision;
    public BoxCollider2D groundCheck;

    private bool isAlive = true;
    private bool facingRight;
    public bool suspenseGroundMode = true;
    public bool startMoveToRight = true;
    


    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
        myMovementController = GetComponentInChildren(typeof(HorizontalSuspenseGroundMove)) as HorizontalSuspenseGroundMove;

        if(!suspenseGroundMode)
        {
            groundCheck.enabled = false;
        }
        
        if(startMoveToRight){
            facingRight = true;
        }else{
            myMovementController.Flip();
            myMovementController.move = -2;
            facingRight = false;
        }
    }

    void Update() {
        
        if(collision.onRightWall && facingRight || (collision.onLeftWall && !facingRight)){
            myMovementController.Flip();
            facingRight = !facingRight;
        }

    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;
            if(player.GetComponent<Transform>().position.y > transform.position.y+1.2f)
            {
                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 750));
                player.GetComponent<MechanicsPlayerController>().numberJumps--;
                Die();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("MeleeWeaponArea"))
        {
            if(isAlive)
            {
                Die();
            }
        }
    }

    void Die()
    {
        isAlive = false;
        myRigidBody.AddForce(new Vector2(0, 30000));
        myCollider.enabled = false;
        myAnimator.SetBool("isAlive", false);
        Invoke("DestroyEnemy",1.8f);
    }

    void DestroyEnemy(){
        Destroy(gameObject);
    }
}
