using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float speed;
    private new Rigidbody2D rigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.velocity  = new Vector2(speed, 0);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        speed = speed * 0.95f;
        rigidbody2D.velocity = new Vector2(speed, Mathf.Abs(speed));
        if (collision.gameObject.CompareTag("Ground"))
        {
            
            Destroy(gameObject);// tự hủy
           // Destroy(collision.gameObject, 3); //hủy cái va chạm với fireball    
        }
        if (collision.gameObject.CompareTag("Pile"))
        {

            Destroy(gameObject);// tự hủy
                                   // Destroy(collision.gameObject, 3); //hủy cái va chạm với fireball    
        }
        if (collision.gameObject.CompareTag("ItemBox"))
        {

            Destroy(gameObject, 2);// tự hủy
                                   // Destroy(collision.gameObject, 3); //hủy cái va chạm với fireball    
        }
        if (collision.gameObject.CompareTag("Brick"))
        {

            Destroy(gameObject, 2);// tự hủy
                                   // Destroy(collision.gameObject, 3); //hủy cái va chạm với fireball    
        }
        if (collision.gameObject.CompareTag("Enemy_Mushroom"))
        {
            Destroy(collision.gameObject); //hủy cái va chạm với fireball    

        }
    }
    public void SetSpeed(float value)
    {
        speed = value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
