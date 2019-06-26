using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalSuspenseGroundMove : MonoBehaviour
{
    
    private bool                    colidde = false;

    public float                    move = 2;

    public GameObject               thisEnemy;
    
    

    void Update()
    {
        thisEnemy.GetComponent<Rigidbody2D>().velocity = new Vector2(move, thisEnemy.GetComponent<Rigidbody2D>().velocity.y);
    }

    public void Flip()
    {
        move *= -1;
        Vector3 theScale = thisEnemy.GetComponent<Transform>().localScale;
        theScale.x *= -1;
        thisEnemy.GetComponent<Transform>().localScale = theScale;
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Ground")){
            Flip();
        }
    }
}
