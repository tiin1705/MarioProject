using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickUp : MonoBehaviour
{

    public float speed; //tốc độ    
    public float bounce; //độ nẩy
    public Vector2 originalPosition; //vị trí ban đầu
    public Sprite emptyItemBox;
    private bool canChange; // Va chạm khối
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        canChange = true;

    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.CompareTag("Player") && collision.contacts[0].normal.y > 0)
        {


            //nẩy lên và rơi xuống
            StartCoroutine(BounceUpandDown());


        }
    }
    IEnumerator BounceUpandDown()
    {
        //nẩy lên
        while (true)
        {
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y + speed * Time.deltaTime
                );
            if (transform.position.y > originalPosition.y + bounce)
                break;
            yield return null;

        }
        //rơi xuống
      
    }
}
