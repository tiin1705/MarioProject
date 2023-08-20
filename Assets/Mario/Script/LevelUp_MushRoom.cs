using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp_MushRoom : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    public float right;
    public float left;
    public float movementSpeed = 1f;
    bool isRight;
    
    Vector3 vector3;
    public bool isLevelup = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();

    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            
            Destroy(gameObject, 0.2f);
        };
        if (collision.collider.tag == "Pile" && collision.contacts[0].normal.x < 0)
        {
           Debug.Log(">>>>>(Dung Pile)");
        }
        else
        {
           
        }
        transform.Translate(vector3 * movementSpeed * Time.deltaTime);



    }
    private void EnemyMovement()
    {
        float positionX = transform.position.x;
        if (positionX < left)
        {
            isRight = false;


        }
        if (positionX > right)
        {
            isRight = true;

        }
        if (isRight)
        {
            Vector2 scale = transform.localScale;
            scale.x *= scale.x > 0 ? -1 : 1;
            transform.localScale = scale;
            vector3 = new Vector3(1, 0, 0);
        }
        else
        {
            Vector2 scale = transform.localScale;
            scale.x *= scale.x > 0 ? 1 : -1;
            transform.localScale = scale;
            vector3 = new Vector3(-1, 0, 0);

        }

        transform.Translate(vector3 * movementSpeed * Time.deltaTime);
    }
   
}
