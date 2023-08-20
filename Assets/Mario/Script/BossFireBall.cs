using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireBall : MonoBehaviour
{
    private float speed;
    private new Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed, 0);
        Destroy(gameObject, 3);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Plant"))
        {
            Destroy(gameObject);
        }   
        if (collision.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);
          }
        if (collision.gameObject.CompareTag("Pile"))
        {
            Destroy(gameObject); 
        }
     
    }

    // Update is called once per frame
    public void SetSpeed(float value)
    {
        speed = value;
    }
}
