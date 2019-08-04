using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeaponController : MonoBehaviour
{
    
   
    public GameObject       shotAxePrefab;
    public Transform        spawnRageWeapon;
    private JointMotor2D    changedMotor;

    private Animator        playerAnimator;

    private bool        facingRight;
    public float        speedShot = 14f;
    public float        delayShot = 0.2f;
    private bool        shoted;
    
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {   

        facingRight = GameObject.FindGameObjectWithTag("Player").GetComponent<MechanicsPlayerController>().facingRight;

        if(Input.GetKeyDown(KeyCode.B)){
            if(!shoted)
            {
                StartShotAxe();
            }
        }
    }

    void StartShotAxe()
    {
        shoted = true;
        StartCoroutine("delayNextShot");

        GameObject tempAxeShot = Instantiate(shotAxePrefab);
        tempAxeShot.transform.position = spawnRageWeapon.position;
        playerAnimator.SetBool("isThrowing",true);

        if(!facingRight)
        {
            tempAxeShot.GetComponent<SpriteRenderer>().flipX = true;
            tempAxeShot.GetComponent<Rigidbody2D>().velocity = new Vector2(-speedShot, 0);
            changedMotor.maxMotorTorque = 10000000;
            changedMotor.motorSpeed = -1200;
            tempAxeShot.GetComponent<WheelJoint2D>().motor = changedMotor;
        }else{
            tempAxeShot.GetComponent<SpriteRenderer>().flipX = false;
            tempAxeShot.GetComponent<Rigidbody2D>().velocity = new Vector2(speedShot, 0);
            changedMotor.maxMotorTorque = 10000000;
            changedMotor.motorSpeed = 1200;
            tempAxeShot.GetComponent<WheelJoint2D>().motor = changedMotor;
        }

        Invoke("StopThrowing", 0.66f);
    }

    IEnumerator delayNextShot()
    {
        yield return new WaitForSeconds(delayShot);
        shoted = false;
    }

    void StopThrowing()
    {
        playerAnimator.SetBool("isThrowing",false);
    }
}
