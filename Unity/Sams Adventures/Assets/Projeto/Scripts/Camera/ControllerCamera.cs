using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCamera : MonoBehaviour
{
    private Vector2     velocidade;
    private Transform   player;

    public float        smoothTimeX = 0.5f;
    public float        smoothTimeY = 0.5f;

    float posX;

    float posY;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("SamKarina").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        posX = Mathf.SmoothDamp(transform.position.x, player.position.x, ref velocidade.x, smoothTimeX);
        posY = Mathf.SmoothDamp(transform.position.y, player.position.y+3, ref velocidade.y, smoothTimeY);
        transform.position = new Vector3(posX, posY, transform.position.z);
    }   
}
