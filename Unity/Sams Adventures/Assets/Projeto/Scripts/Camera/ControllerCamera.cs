using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ControllerCamera : MonoBehaviour
{
    private Rigidbody2D   playerRigidBody;
    private float       timeStopped;
    private float       ortoGraphicSize;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        ortoGraphicSize = GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerRigidBody.velocity.x == 0 && playerRigidBody.velocity.x == 0){
            timeStopped += Time.deltaTime;
        }else
        {
            timeStopped = 0;
        }

        if(timeStopped > 2f){
            ortoGraphicSize = Mathf.Lerp(ortoGraphicSize, 6, Time.deltaTime/2);
            GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = ortoGraphicSize;
        }else{
            ortoGraphicSize = Mathf.Lerp(ortoGraphicSize, 7f, Time.deltaTime/2);
            GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = ortoGraphicSize;
        }
    }
}
