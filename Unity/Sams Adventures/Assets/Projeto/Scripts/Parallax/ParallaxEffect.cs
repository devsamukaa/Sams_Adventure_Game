using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParallaxEffect : MonoBehaviour
{

    private Transform player;
    public Image parallaxBackground;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = new Vector2(player.position.x/250,0);
        parallaxBackground.material.mainTextureOffset = offset;
    }
}
