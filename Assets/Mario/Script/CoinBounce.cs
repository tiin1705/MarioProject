using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBounce : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 0.5f);
        transform.position += new Vector3(0,0.5f,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
