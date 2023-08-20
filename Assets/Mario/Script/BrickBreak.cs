using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class BrickBreak : MonoBehaviour
{
    public UnityEvent hit;
    public GameObject boom;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            var direction = collision.GetContact(0).normal;
            //nẩy lên
            if (Mathf.Round(direction.y) == 1)
            {
                hit.Invoke();
                Instantiate(boom,transform.position,Quaternion.identity);
            }
        }
    }
}
