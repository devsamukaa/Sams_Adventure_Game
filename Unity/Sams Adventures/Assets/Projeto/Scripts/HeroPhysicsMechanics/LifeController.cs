using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    public float forceHurtX = 10;
    public float forceHurtY = 10;
    public float timeBlinking = 0;
    public bool isBlinking;
    private GameObject bloodEfector;
    private Rigidbody2D playerRigidBody;
    private MechanicsPlayerController playerMechanics;
    private Collider2D playerColider;
    private Animator playerAnimator;
    private Spriter2UnityDX.EntityRenderer playerRender;
    private Color redColor, normalColor;

    void Start()
    {
        bloodEfector = transform.Find("Blood").gameObject;
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerMechanics = GetComponent<MechanicsPlayerController>();
        playerColider = GetComponent<Collider2D>();
        playerRender = GetComponent<Spriter2UnityDX.EntityRenderer>();
        playerAnimator = GetComponent<Animator>();

        redColor = new Color(255, 0, 0, 255);
        normalColor = new Color(255, 255, 255, 255);
    }

    // Update is called once per frame
    void Update()
    {
        if(isBlinking){
            blinkPlayer();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Enemy")){
            StartHurt();
        }
    }

    void StartHurt()
    {
        bloodEfector.SetActive(true);
        playerAnimator.SetBool("isHurting", true);
        playerAnimator.SetTrigger("hurt");

        playerRigidBody.velocity = new Vector2(0 , 0);
        if(playerMechanics.facingRight){
            playerRigidBody.velocity = new Vector2(-forceHurtX, forceHurtY);
        }else{
            playerRigidBody.velocity = new Vector2(forceHurtX, forceHurtY);
        }
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {   
            Physics2D.IgnoreCollision(playerColider, enemy.GetComponent<Collider2D>());
        }
        isBlinking = true;

        Invoke("StopHurt", 2.5f);
        Invoke("StopAnimation", 0.66f);
    }

    void blinkPlayer(){
        
        timeBlinking += Time.deltaTime*4;
        if((Mathf.Round(timeBlinking)%2) == 0){
            playerRender.Color = redColor;
        }else{
            playerRender.Color = normalColor;
        }
    }

    void SetNormalColor()
    {
        playerRender.Color = normalColor;
    }

    void StopAnimation()
    {
        playerAnimator.SetBool("isHurting", false);
    }

    void StopHurt(){
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {   
            Physics2D.IgnoreCollision(playerColider, enemy.GetComponent<Collider2D>(), false);
        }
        isBlinking = false;
        SetNormalColor();
    }
}
