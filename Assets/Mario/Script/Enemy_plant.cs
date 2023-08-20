using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Enemy_plant : MonoBehaviour
{


    private Rigidbody2D rb;
    public float movementSpeed = 1f;
    public float right;
    public float left;
    bool isRight;
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
        float positionY = transform.position.y;
        if (positionY < left)
        {
            isRight = true;


        }
        if (positionY > right)
        {
            isRight = false;

        }
        if (isRight)
        {
          
            vector3 = new Vector3(0, 1, 0);
        }
        else
        {
           
            vector3 = new Vector3(0, -1, 0);
        }

        transform.Translate(vector3 * movementSpeed * Time.deltaTime);
    }
    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
}
