using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy0_movement : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    public float right;
    public float left;
    public float movementSpeed= 1f;
    bool isRight;
    public Sprite Death;
    Vector3 vector3;
    GameObject Player;
   
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        float positionX = transform.position.x;
        if(positionX < left)
        {
            isRight = true;
          
            
        }
        if(positionX > right)
        {
            isRight = false;
            
        }
        if(isRight)
        {
            Vector2 scale = transform.localScale;
            scale.x *= scale.x > 0 ? -1 :1 ;
            transform.localScale = scale;
            vector3 = new Vector3(1,0,0);
        }
        else
        {
            Vector2 scale = transform.localScale;
            scale.x *= scale.x > 0 ? 1 : -1;
            transform.localScale = scale;
            vector3 = new Vector3(-1,0,0);

        }

        transform.Translate(vector3 * movementSpeed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && collision.contacts[0].normal.y < 0)
        {
           
            Destroy(gameObject);
        }
       
      
      
    }
        private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
}
