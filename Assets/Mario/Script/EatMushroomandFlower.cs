using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;




public class EatMushroomandFlower : MonoBehaviour
{
    GameObject Player;
    // Start is called before the first frame update
    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag== "Player")
        {
            if(Player.GetComponent<PlayerMovement>().Level < 2)
            {
                Player.GetComponent<PlayerMovement>().Level = 1;
                Player.GetComponent<PlayerMovement>().Trans = true;

            }
            
            
           
            
        }
    }
}
