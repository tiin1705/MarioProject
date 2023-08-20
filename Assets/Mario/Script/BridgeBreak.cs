using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class BridgeBreak : MonoBehaviour
{
    private new Rigidbody2D rb;
    public GameObject[] bridge;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }


    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log(">>>>>>>>>>DestroyBridge");
            DestroyGameObjects("Bridge");
            Destroy(gameObject);
        }
    }
    public void DestroyGameObjects(string tag)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);
        foreach(GameObject go in bridge)
        {
            GameObject.Destroy(go);
        }
    }
    void Update()
    {
        
    }
}
