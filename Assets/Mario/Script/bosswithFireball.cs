using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bosswithFireball : MonoBehaviour
{
    private Rigidbody2D rb;
    public float movementSpeed = 1f;
    public float right;
    public float left;
    bool isRight;
    Vector3 vector3;
    GameObject Player;
    // Start is called before the first frame update

    private float timeSpawn; // thời gian delay bắn đạn
    private float time; //biến đếm thời gian
    public GameObject fireBall;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timeSpawn = 2;
        time = timeSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        float positionX = transform.position.x;
        if (positionX < left)
        {
            isRight = true;


        }
        if (positionX > right)
        {
            isRight = false;

        }
        if (isRight)
        {

            vector3 = new Vector3(1, 0, 0);
        }
        else
        {

            vector3 = new Vector3(-1, 0, 0);
        }

        transform.Translate(vector3 * movementSpeed * Time.deltaTime);
        time -= Time.deltaTime;
        if (time < 0)
        {
            time = timeSpawn;
            GameObject fb = Instantiate(fireBall);
            fb.transform.position = new Vector3(
                   transform.position.x + -1f,
                   transform.position.y
               );
            fb.GetComponent<BossFireBall>().SetSpeed(-20);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("lava"))
        {
            Destroy(gameObject);
        }
    }
    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
}
