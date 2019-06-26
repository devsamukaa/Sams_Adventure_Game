using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColetableBoxController : MonoBehaviour
{
    public GameObject[] blockParticles;
    public GameObject explosionParticle;

    private void OnCollisionEnter2D(Collision2D other) {
        
        if(other.gameObject.CompareTag("Player")){

            Transform blockTransform = gameObject.GetComponent<Transform>();
            Transform playerTransform = other.gameObject.GetComponent<Transform>();
            if(playerTransform.position.y < (blockTransform.position.y-2f)){
                BrokeBlock();
            }            
        }
        
    }

    void BrokeBlock(){
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        foreach (GameObject item in blockParticles)
        {
            Rigidbody2D rigidBodyItem = item.GetComponent<Rigidbody2D>();
            rigidBodyItem.bodyType = RigidbodyType2D.Dynamic;
        }
        explosionParticle.GetComponent<PointEffector2D>().enabled = true;
        Invoke("StopPointEffector",0.2f);
        Invoke("DestroyBlock",0.8f);
    }

    void StopPointEffector(){
        explosionParticle.GetComponent<PointEffector2D>().enabled = false;
    }

    void DestroyBlock(){
        Destroy(gameObject);
    }
}
