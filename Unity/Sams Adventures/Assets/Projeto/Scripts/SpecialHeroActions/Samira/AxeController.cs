using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        
        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        
        GameObject child = gameObject.transform.GetChild(0).gameObject;
        child.GetComponent<Rigidbody2D>().constraints &= RigidbodyConstraints2D.None;

        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,800));

        Invoke("DestroyThis", 2f);

    }

    void DestroyThis()
    {
        Destroy(this.gameObject);
    }

    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
