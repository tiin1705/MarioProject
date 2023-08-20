using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioFireBallScript : MonoBehaviour
{
    public float speed;
    private new Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            rigidbody2D.velocity = new Vector2(speed, 0);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
