using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponAtackController : MonoBehaviour
{
    
    private GameObject[] collidersOfMeleeWeaponAtack;
    private Animator playerAnimator;
    private bool isSlashing = false;

    void Start()
    {
        collidersOfMeleeWeaponAtack = GameObject.FindGameObjectsWithTag("MeleeWeaponArea");
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.V) && !isSlashing)
        {
            PlayMeleeAtack();
        }
    }

    void PlayMeleeAtack(){
        
        playerAnimator.SetBool("isSlashing", true);
        isSlashing = true;
        Invoke("EnableMeleeWeaponArea", 0.22f);
    }

    void EnableMeleeWeaponArea()
    {
        foreach (GameObject item in collidersOfMeleeWeaponAtack)
        {
            item.GetComponent<PolygonCollider2D>().enabled = true;
        }
        Invoke("DisableMeleeWeaponArea", 0.22f);
    }

    void DisableMeleeWeaponArea()
    {
        foreach (GameObject item in collidersOfMeleeWeaponAtack)
        {
            item.GetComponent<PolygonCollider2D>().enabled = false;
        }
        Invoke("StopMeleeAtack", 0.22f);
    }

    void StopMeleeAtack()
    {
        playerAnimator.SetBool("isSlashing", false);
        isSlashing = false;
    }
}
